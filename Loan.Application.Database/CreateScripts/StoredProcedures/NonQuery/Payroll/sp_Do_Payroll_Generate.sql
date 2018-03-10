USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Payroll_Generate]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Payroll_Generate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Payroll_Generate]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Payroll_Generate]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*****************************************************************************
MODIFIED:
JS 01/26/2013	REPLACED HARDCODED CODE5 WITH TABLE SOURCE
JS 07/27/2013	PREVENT STOP OF RENEWED LOANS WITHIN THE CUTOFF
*****************************************************************************/

CREATE PROCEDURE [dbo].[Do_Payroll_Generate]
@dateFrom date,
@dateTo date
as

declare @myproc_date as date
declare @sysdate as date

declare @sysdate_gomo as date
declare @dateTo_gomo as date

declare @codes table (
	LOAN_TYPE varchar(3),
	code varchar(6),
	mbat varchar(3)
)

create table #tmp (
	PN_NO varchar(7),
	code varchar(6),
	mbat varchar(3),
	datedue_gomo date
)

create clustered index #tmp_PnNo on #tmp(PN_NO)

create table #tloans (
	PN_NO varchar(7),
	KBCI_NO varchar(7),														-- JS 07/27/2013
	LOAN_TYPE varchar(3),													--		|
	LOAN_STAT varchar(1)													-- JS 07/27/2013
)

create nonclustered index #tloans_PnNo on #tloans(PN_NO)
create nonclustered index #tloans_KbciNoLoanType on #tloans(KBCI_NO, LOAN_TYPE)

select	top 1
		@myproc_date = [PROC],
		@sysdate = SYSDATE
from	dbo.CTRL

set		@sysdate_gomo = DATEADD(DD, 0 - (DAY(@sysdate)-1), DATEADD(M, 1, @sysdate))
set		@dateTo_gomo = DATEADD(DD, 0 - (DAY(DATEADD(M, 2, @dateTo))), DATEADD(M, 2, @dateTo))

insert	#tloans (
		PN_NO,
		KBCI_NO,															-- JS 07/27/2013
		LOAN_TYPE,															--		|
		LOAN_STAT															-- JS 07/27/2013
		)
select	lon.PN_NO,
		mem.KBCI_NO,
		lon.LOAN_TYPE,
		lon.LOAN_STAT
from	dbo.LOANS
			as lon
		inner join dbo.MEMBERS
			as mem
			on lon.KBCI_NO = mem.KBCI_NO
where	lon.MOD_PAY like '%1%' and
		mem.MEM_STAT != 'S' and
		lon.CHKNO_DATE <= @dateTo and
		(
			(lon.CHKNO_DATE <= @dateTo and lon.LOAN_STAT = 'R') or
			(lon.CHG_DATE >= @dateFrom and lon.LOAN_STAT in ('F', 'T'))
		)
		
delete	#tloans																-- JS 07/27/2013
where	PN_NO in															--		|
		(																	--		|
			select	tlon.PN_NO												--		|
			from	#tloans tlon											--		|
						inner join											--		|
					dbo.LOANS lon on										--		|
						lon.LOAN_TYPE = tlon.LOAN_TYPE and					--		|
						lon.KBCI_NO = tlon.KBCI_NO and						--		|
						lon.LOAN_STAT = 'R'									--		|
			where	tlon.LOAN_STAT in ('F', 'T')							--		|
		)																	-- JS 07/27/2013

insert	@codes (
		LOAN_TYPE,
		code,
		mbat
		)
select	LOAN_TYPE,															-- JS 01/26/2013
		CODE5,																--		|
		MBAT																--		|
from	dbo.LOAN_TYPE														--		|
where	CODE5 is not null													-- JS 01/26/2013

insert	#tmp (
		PN_NO,
		code,
		mbat,
		datedue_gomo
		)
select	lon.PN_NO,
		case
			when codes.code is not null then codes.code
			else '******'
			end,
		case
			when codes.mbat is not null then codes.mbat
			else '   '
			end,
		DATEADD(DD, 0 - (DAY(DATEADD(M, 1, lon.DATE_DUE))), DATEADD(M, 1, lon.DATE_DUE))
from	dbo.LOANS
			as lon
		left join @codes
			as codes
			on codes.LOAN_TYPE = lon.LOAN_TYPE

truncate table dbo.ADVICE
truncate table dbo.[STOP]

-- IF code # '******' and m.chkno_date is within dt1 and dt2, include
insert	dbo.ADVICE (
		EMPNO,
		WAGE_TYPE,
		BEGDA,
		ENDDA,
		AMOUNT
		)
select	mem.CB_EMPNO,
		tmp.code,
		dbo.func_FormatCCYYMMDD(@sysdate_gomo),
		dbo.func_FormatCCYYMMDD(tmp.datedue_gomo),
		lon.AMORT_AMT
from	dbo.LOANS
			as lon
		inner join dbo.MEMBERS 
			as mem
			on lon.KBCI_NO = mem.KBCI_NO
		inner join #tmp
			as tmp
			on tmp.PN_NO = lon.PN_NO
		inner join #tloans
			as tln
			on tln.PN_NO = lon.PN_NO
where	lon.LOAN_STAT not in ('F', 'T') and
		tmp.code != '******' and
		lon.CHKNO_DATE between @dateFrom and @dateTo

-- IF code # '******' and m.chkno_date is not within dt1 and dt, inclusive
-- IF  (m.arrear_p+m.arrear_i+m.arrear_oth) # 0
insert	dbo.ADVICE (
		EMPNO,
		WAGE_TYPE,
		BEGDA,
		ENDDA,
		AMOUNT
		)
select	mem.CB_EMPNO,
		tmp.code,
		dbo.func_FormatCCYYMMDD(@sysdate_gomo),
		case
			when lon.DATE_DUE < @sysdate_gomo then dbo.func_FormatCCYYMMDD(@dateTo_gomo)
			else dbo.func_FormatCCYYMMDD(tmp.datedue_gomo)
			end,
		ISNULL(lon.AMORT_AMT,0) + ISNULL(lon.ARREAR_P,0) + ISNULL(lon.ARREAR_I,0) + ISNULL(lon.ARREAR_OTH,0)
from	dbo.LOANS
			as lon
		inner join dbo.MEMBERS 
			as mem
			on lon.KBCI_NO = mem.KBCI_NO
		inner join #tmp
			as tmp
			on tmp.PN_NO = lon.PN_NO
		inner join #tloans
			as tln
			on tln.PN_NO = lon.PN_NO
where	lon.LOAN_STAT not in ('F', 'T') and
		tmp.code != '******' and
		not(lon.CHKNO_DATE between @dateFrom and @dateTo) and
		(ISNULL(lon.ARREAR_P,0) + ISNULL(lon.ARREAR_I,0) + ISNULL(lon.ARREAR_OTH,0)) != 0

-- else
insert	dbo.ADVICE (
		EMPNO,
		WAGE_TYPE,
		BEGDA,
		ENDDA,
		AMOUNT
		)
select	mem.CB_EMPNO,
		tmp.code,
		dbo.func_FormatCCYYMMDD(@sysdate_gomo),
		case
			when lon.DATE_DUE < @sysdate_gomo then dbo.func_FormatCCYYMMDD(@dateTo_gomo)
			else dbo.func_FormatCCYYMMDD(tmp.datedue_gomo)
			end,
		lon.AMORT_AMT
from	dbo.LOANS
			as lon
		inner join dbo.MEMBERS 
			as mem
			on lon.KBCI_NO = mem.KBCI_NO
		inner join #tmp
			as tmp
			on tmp.PN_NO = lon.PN_NO
		left join dbo.MO_DEDN
			as dedn
			on dedn.EMPNO = mem.CB_EMPNO and dedn.LOAN_TYPE = lon.LOAN_TYPE
		--left join dbo.MO_DEDNO
		left join (
			select	A1.*
			from	dbo.MO_DEDNO
						as A1
					inner join (
						select	MIN(MO_DEDNO_ID) AS MO_DEDNO_ID,
								EMPNO,
								LOAN_TYPE
						from	dbo.MO_DEDNO
						--where	PN_NO is not null
						group
						by		EMPNO,
								LOAN_TYPE
						)
						as A2
						on A2.MO_DEDNO_ID = A1.MO_DEDNO_ID
			)
			as dedno
			on dedno.EMPNO = mem.CB_EMPNO and dedno.LOAN_TYPE = lon.LOAN_TYPE
		inner join #tloans
			as tln
			on tln.PN_NO = lon.PN_NO
where	lon.LOAN_STAT not in ('F', 'T') and
		tmp.code != '******' and
		not(lon.CHKNO_DATE between @dateFrom and @dateTo) and
		(ISNULL(lon.ARREAR_P,0) + ISNULL(lon.ARREAR_I,0) + ISNULL(lon.ARREAR_OTH,0)) = 0 and
		lon.AMORT_AMT != isnull(dedn.DEDUCTION, 0) + isnull(dedno.DEDUCTION, 0)

set		@sysdate_gomo = DATEADD(DD, 0 - (DAY(@sysdate)), DATEADD(M, 1, @sysdate))

insert	dbo.[STOP] (
		EMPNO,
		WAGE_TYPE,
		ENDDA
		)
select	mem.CB_EMPNO,
		left(tmp.code, 4),
		dbo.func_FormatCCYYMMDD(@sysdate_gomo)
from	dbo.LOANS
			as lon
		inner join dbo.MEMBERS 
			as mem
			on lon.KBCI_NO = mem.KBCI_NO
		inner join #tloans
			as tln
			on tln.PN_NO = lon.PN_NO
		inner join #tmp
			as tmp
			on tmp.PN_NO = lon.PN_NO
where	lon.LOAN_STAT in ('F', 'T') and
		tmp.code != '******'

insert	dbo.[STOP] (
		EMPNO,
		WAGE_TYPE,
		ENDDA
		)
select	dedn.EMPNO,
		left(codes.code, 4),
		dbo.func_FormatCCYYMMDD(@sysdate_gomo)
from	dbo.MO_DEDN
			as dedn
		inner join @codes
			as codes
			on dedn.LOAN_TYPE = codes.LOAN_TYPE
where	dedn.LOAN_TYPE != ' MA' and
		dedn.PN_NO is null and
		dedn.EMPNO + codes.code not in (select EMPNO + code from dbo.[STOP])

delete	dbo.[STOP]
where	exists
		(
		select	1
		from	dbo.ADVICE
		where	EMPNO = [STOP].EMPNO and
				WAGE_TYPE = [STOP].WAGE_TYPE
		)

drop table #tmp
drop table #tloans

GO


