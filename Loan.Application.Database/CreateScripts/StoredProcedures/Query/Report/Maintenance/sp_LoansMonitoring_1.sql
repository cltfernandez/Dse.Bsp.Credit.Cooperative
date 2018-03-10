USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Maintenance_LoansMonitoring]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Maintenance_LoansMonitoring]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Maintenance_LoansMonitoring]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Maintenance_LoansMonitoring]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*****************************************************************************
MODIFIED:
JS 09/14/2012		TABLE-DRIVEN LOAN TYPE
JS 05/11/2013		ADDED CTD AMOUNT
JS 08/30/2014		CORRECTED MEMBERSHIP TERM
JS 04/11/2015		FIXED LOANS AS OF DATE
*****************************************************************************/

CREATE PROCEDURE [dbo].[Report_Maintenance_LoansMonitoring]
@kbci_no as varchar(7),
@my_user as varchar(8),
@asOfDate as date = null,
@suppressIfNoOutput as bit = 1
as

declare @CTD_AMOUNT numeric(11, 2)
declare @MEM_DATE date

declare @sysdate date
declare @sysusername varchar(30)
declare @yy int
declare @mm int
declare @dd int

/* Get system information */

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)
	
set @asOfDate = isnull(@asOfDate, @sysdate)

select
	top 1
	@sysusername = [NAME]
from
	dbo.[USER] with(nolock)
where
	USERNAME = @my_user
order by
	[USER_ID]

/* Get CTD amount */

select
	@CTD_AMOUNT = sum(PRINCIPAL)									-- JS 05/11/2013
from																--		|
	dbo.CTD with(nolock)											--		|
where																--		|
	KBCI_NO = @kbci_no and											--		|
	[STATUS] = 'NEW' and											--		|
	CTD_NO is not null and											--		|
	ltrim(rtrim(CTD_NO)) <> ''										--		|
group by															--		|
	KBCI_NO															-- JS 05/11/2013
	
/* Calculate membership duration */
	
select
	@MEM_DATE = MEM_DATE
from
	dbo.MEMBERS
where
	KBCI_NO = @kbci_no

set @mm = datediff(mm, @MEM_DATE, @sysdate)
set @dd = datediff(dd, dateadd(mm, @mm, @MEM_DATE), @sysdate)

if @dd < 0
begin
	set @mm = @mm - 1
	set @dd = datediff(dd, dateadd(mm, @mm, @MEM_DATE), @sysdate)
end

set @yy = floor(@mm/12)
set @mm = @mm - (@yy * 12)

/* Build loan data */

if OBJECT_ID('tempdb..#LOANS') is not null 
begin
	truncate table #LOANS
	
	insert into #LOANS
	(
		LOANS_ID, PN_NO, KBCI_NO, APP_DATE, APP_NO, DATE_GRANT, BY_WHOM, DATE_DUE, CHKNO_BANK, CHKNO, CHKNO_AMT, CHKNO_DATE, CHKNO_RELS, CHKNO_ACK, MOD_PAY, AMORT_AMT, PAY_START, RATE, TERM, FREQ, PRINCIPAL, LED_TYPE, ADV_INTE, AFT_INTE, ACCU_PAYP, YTD_I, LOAN_TYPE, LOAN_STAT, ARREAR_I, ARREAR_P, ARREAR_OTH, ARREAR_AS, COLLATERAL, DED_BAL, ADD_DATE, CHG_DATE, [USER], P_BAL, I_BAL, O_BAL, REC_STAT, RENEW, ADVANCE, LRI_IND, NDUE, L_EXT, PD, LRI_DUE, BALANCE
	)
	select
		LOANS_ID, lon.PN_NO, KBCI_NO, APP_DATE, APP_NO, DATE_GRANT, BY_WHOM, DATE_DUE, CHKNO_BANK, CHKNO, CHKNO_AMT, CHKNO_DATE, CHKNO_RELS, CHKNO_ACK, MOD_PAY, AMORT_AMT, PAY_START, RATE, TERM, FREQ, PRINCIPAL, LED_TYPE, ADV_INTE, AFT_INTE, ACCU_PAYP, YTD_I, LOAN_TYPE, LOAN_STAT, ARREAR_I, ARREAR_P, ARREAR_OTH, ARREAR_AS, COLLATERAL, DED_BAL, ADD_DATE, CHG_DATE, [USER], P_BAL, I_BAL, O_BAL, REC_STAT, RENEW, ADVANCE, LRI_IND, NDUE, L_EXT, PD, LRI_DUE, BALANCE
	from
		dbo.LOANS lon
			cross apply
		dbo.func_BalanceAsOf(lon.PN_NO, '1900-01-01', @asOfDate, @asOfDate) drvz
	where
		KBCI_NO = @kbci_no
end
else
begin
	select
		LOANS_ID, lon.PN_NO, KBCI_NO, APP_DATE, APP_NO, DATE_GRANT, BY_WHOM, DATE_DUE, CHKNO_BANK, CHKNO, CHKNO_AMT, CHKNO_DATE, CHKNO_RELS, CHKNO_ACK, MOD_PAY, AMORT_AMT, PAY_START, RATE, TERM, FREQ, PRINCIPAL, LED_TYPE, ADV_INTE, AFT_INTE, ACCU_PAYP, YTD_I, LOAN_TYPE, LOAN_STAT, ARREAR_I, ARREAR_P, ARREAR_OTH, ARREAR_AS, COLLATERAL, DED_BAL, ADD_DATE, CHG_DATE, [USER], P_BAL, I_BAL, O_BAL, REC_STAT, RENEW, ADVANCE, LRI_IND, NDUE, L_EXT, PD, LRI_DUE, BALANCE
	into
		#LOANS
	from
		dbo.LOANS lon
			cross apply
		dbo.func_BalanceAsOf(lon.PN_NO, '1900-01-01', @asOfDate, @asOfDate) drvz
	where
		KBCI_NO = @kbci_no
end

if @suppressIfNoOutput = 0 and not exists (select top 1 'exists' from #LOANS where BALANCE > 0)
begin
	insert into #LOANS (LOANS_ID, PN_NO, KBCI_NO) values (0, '(None)', @kbci_no)
end

/* Final select */

select	
	@asOfDate as ASODATE,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) as BORROWER,
	dbo.func_Format241(mem.KBCI_NO) as ACCOUNT_NO,
	mem.DEPT,
	mem.OFF_TEL,
	mem.B_DATE,
	mem.FD_AMOUNT,
	sdm.ACCTOBAL,
	isnull(@CTD_AMOUNT, 0) CTD_AMOUNT,								-- JS 05/11/2013
	datediff(YYYY, mem.B_DATE, getdate()) as AGE,
	(
		case
			when lon.PD = 1 then 'PD - '
			else ''
			end +
		replace(isnull(p.LOAN_DESC, ''), ' Loan', '')
	)as LOAN_APPLIED,	-- JS 09/14/2012
	lon.PRINCIPAL as AMOUNT,
	lon.CHKNO_DATE as DATE_GRANTED,
	lon.DATE_DUE,
	convert(varchar(3), lon.TERM) + lon.FREQ as TF,
	case
		when lon.MOD_PAY <> '1' then isnull(lon.AMORT_AMT, 0)		-- JS 05/11/2013
		else 0
		end as DIRECT,
	case
		when lon.MOD_PAY = '1' then isnull(lon.AMORT_AMT, 0)		-- JS 05/11/2013
		else 0
		end as PAYROLL,
	--(																-- JS 05/11/2013
	--	isnull(lon.PRINCIPAL, 0) -									--		|
	--	isnull(drvZ.PAYMENT, 0)										--		|
	--) as BALANCE,													--		|
	lon.BALANCE,																	-- JS 04/11/2015
	(																--		|
		isnull(lon.ARREAR_P, 0) +									--		|
		isnull(lon.ARREAR_I, 0) +									--		|
		isnull(lon.ARREAR_OTH, 0)									--		|
	) as ARREARS,													-- JS 05/11/2013
	lon.PN_NO,
	lon.LOAN_TYPE as TYP,
	lon.RATE,
	lon.TERM,
	lon.FREQ,
	lon.ARREAR_P,
	lon.ARREAR_I,
	lon.ARREAR_OTH,
	DATENAME(M, mem.MEM_DATE) + ' ' + DATENAME(DD, mem.MEM_DATE) + ', ' + DATENAME(YYYY, mem.MEM_DATE) MEM_DATE,
	case
		when mem.MEM_DATE is null then '** no membership date, please check !!!'
		else
			--CONVERT(varchar(4), (DATEDIFF(M, mem.MEM_DATE, @sysdate) - 1) / 12) + ' years and ' +																-- JS 08/30/2014
			--CONVERT(varchar(4), (DATEDIFF(M, mem.MEM_DATE, @sysdate) - 1) % 12) + ' months'																	--		|
			--case
			--	when
			--		datepart(day, mem.MEM_DATE) < datepart(day, @sysdate)
			--	then
			--		convert(varchar(7), floor(datediff(month, mem.MEM_DATE, @sysdate) / 12)) + ' year(s) and ' +														--		|
			--		convert(varchar(7), datediff(month, mem.MEM_DATE, @sysdate) - (floor(datediff(month, mem.MEM_DATE, @sysdate) / 12) * 12)) + ' month(s) and ' +		--		|
			--		convert(varchar(7), datediff(day, dateadd(M, datediff(month, mem.MEM_DATE, @sysdate), mem.MEM_DATE), @sysdate)) + ' day(s)'							-- JS 08/30/2014
			--	else
			--		convert(varchar(7), floor(datediff(month, mem.MEM_DATE, @sysdate) / 12)) + ' year(s) and ' +														--		|
			--		convert(varchar(7), datediff(month, mem.MEM_DATE, @sysdate) - (floor(datediff(month, mem.MEM_DATE, @sysdate) / 12) * 12) - 1) + ' month(s) and ' +		--		|
			--		convert(varchar(7), datediff(day, dateadd(M, datediff(month, mem.MEM_DATE, @sysdate) - 1, mem.MEM_DATE), @sysdate)) + ' day(s)'							-- JS 08/30/2014
			--	end
			convert(varchar(10),@yy) + ' year(s) ' + convert(varchar(10),@mm) + ' month(s) '  + convert(varchar(10),@dd) + ' day(s)'
		end MEM_TERM,
	@sysusername PROCESSOR,
	DATENAME(M, @sysdate) + ' ' + DATENAME(DD, @sysdate) + ', ' + DATENAME(YYYY, @sysdate) REPORT_DATE
from
	dbo.MEMBERS mem with(nolock)
		left join
	#LOANS lon with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO --and											-- JS 04/11/2015
		--lon.LOAN_STAT = 'R'													-- JS 04/11/2015
		left join																-- JS 09/14/2012
	dbo.LOAN_TYPE p with(nolock) on												-- JS 09/14/2012
		p.LOAN_TYPE = lon.LOAN_TYPE												-- JS 09/14/2012
		left join
	dbo.SDMASTER sdm with(nolock) on
		sdm.KBCI_NO = mem.KBCI_NO and
		sdm.ACCTSTAT = 'A'
	-- JS 04/11/2015 START
	--	left join
	--(
	--	select
	--		lon.PN_NO,
	--		sum (
	--			case
	--				when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('ADJ', 'PAY') then ISNULL(led.CR, 0) - ISNULL(led.DR, 0)
	--				else 0
	--				end
	--			) as PAYMENT
	--	from
	--		dbo.LOANS lon with(nolock)
	--			left join
	--		dbo.LEDGER led with(nolock)
	--			on lon.PN_NO = led.PN_NO
	--	where
	--		lon.KBCI_NO = @kbci_no and								-- JS 05/11/2013
	--		led.[DATE] <= isnull(@asOfDate, '2099-01-01')
	--	group by
	--		lon.PN_NO
	--) as drvZ
	--	on drvZ.PN_NO = lon.PN_NO
	-- JS 04/11/2015 STOP
where
	mem.KBCI_NO = @kbci_no and
	(
		lon.PN_NO = '(None)' or
		(
			lon.LOAN_STAT not in ('F', 'T') and
			lon.BALANCE > 0
		)
	)
order by
	lon.PD,
	lon.ADD_DATE desc