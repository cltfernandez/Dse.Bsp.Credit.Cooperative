USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Common_GetDetails]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Common_GetDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Common_GetDetails]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Common_GetDetails]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**************************************************************************************************
JFCS	10/03/2015	Multi SML
**************************************************************************************************/


CREATE PROCEDURE [dbo].[s3p_Common_GetDetails]
@QueryType varchar(50),
@v1 varchar(50) = null,
@v2 varchar(50) = null,
@v3 varchar(50) = null,
@v4 varchar(50) = null,
@dt1 datetime = null,
@xrenewSTL bit = 0
as

declare @XBPRIN numeric(10, 2)
declare @XPINT numeric(10, 2)
declare @XOPRI numeric(10, 2)
declare @XLASTD date
declare @XLASPD date
declare @SYSDATE date
declare @PAY_DAY date
declare @AXGOMO int
declare @AXFREQ int
declare @PN_NO varchar(7)
declare @KBCI_NO varchar(7)
declare @FEBTC_SA varchar(10)

if @QueryType = ''

	select null

else if @QueryType = 'Comaker'

	select
		*
	from
		dbo.COMAKER with(nolock)
	where
		PN_NO like @v1

else if @QueryType = 'Control'
	
	select top 1
		*
	from
		dbo.[CTRL] with(nolock)

else if @QueryType = 'Collaterals'

	select
		CTD_NO,
		KBCI_NO,
		RATE,
		DUE_DATE,
		PRINCIPAL,
		case
			when COLLATERAL = 1 then 'YES'
			else ''
			end COLLATERAL,
		NAME,
		[STATUS]
	from
		dbo.CTD with(nolock)
	where
		KBCI_NO = @v1 and
		[STATUS] = 'NEW' and
		CTD_NO is not null and
		ltrim(rtrim(CTD_NO)) <> ''
	order by
		CTD_NO

else if @QueryType = 'Deductions'
begin

	-- @v1	KBCI_NO
	-- @v2	LOAN_TYPE
	-- @v3	RENEW STL
	-- @v4	MY_USER
	
	declare @SDEDBAL table
	(
		PN_TAG bit,
		RENEW bit,
		YEARLY_LRI numeric (9, 2),
		PN_NO varchar(7),
		LOAN_TYPE varchar(3),
		LOAN_STAT varchar(1),
		DATE_GRANT date,
		DATE_DUE date,
		KBCI_NO varchar(7),
		PRINCIPAL numeric(14, 2),
		OUTSBAL numeric(14, 2),
		ARREARS numeric(14, 2),
		LRI_DUE numeric(9, 2),
		PAY_TAG varchar(1),
		PAY_AMT numeric(14, 2)
	)
	
	insert	@SDEDBAL
	(
		PN_TAG,
		RENEW,
		YEARLY_LRI,
		PN_NO,
		LOAN_TYPE,
		LOAN_STAT,
		DATE_GRANT,
		DATE_DUE,
		KBCI_NO,
		PRINCIPAL,
		OUTSBAL,
		ARREARS,
		LRI_DUE,
		PAY_TAG,
		PAY_AMT
	)
	select
		convert(bit, case 
			when lon.LOAN_TYPE not in ('STL','SML') and lon.LOAN_TYPE = @v2 then 1						-- JFCS 10/03/2015
			else 0
			end),
		convert(bit, case 
			when lon.LOAN_TYPE not in ('STL','SML') and lon.LOAN_TYPE = @v2 then 1						-- JFCS 10/03/2015
			else 0
			end),
		isnull(lri.LRI_DUE_Y, 0),
		lon.PN_NO,
		lon.LOAN_TYPE,
		lon.LOAN_STAT,
		lon.DATE_GRANT,
		lon.DATE_DUE,
		lon.KBCI_NO,
		lon.PRINCIPAL,
		dbo.func_J_Preterm('C', lon.PN_NO, lon.LOAN_TYPE, @v4),
		lon.ARREAR_P + lon.ARREAR_I + lon.ARREAR_OTH,
		lon.LRI_DUE,
		' ',
		0
	from
		dbo.LOANS lon with(nolock)
			left outer join
		dbo.LRIDUE lri with(nolock)
			on lon.PN_NO = lri.PN_NO
	where
		lon.KBCI_NO = @v1 and
		lon.LOAN_STAT = 'R'
	
	if @v3 = 'True'
	begin
		update
			@SDEDBAL
		set	
			[PN_TAG] = 1,
			[PAY_TAG] = '1',
			[PAY_AMT] = OUTSBAL,
			[LRI_DUE] = src.LRI_DUE_C
		from
			@SDEDBAL tgt
				inner join
			(
				select
					sded.PN_NO,
					isnull(max(lri.LRI_DUE_C), 0) LRI_DUE_C
				from
					@SDEDBAL sded
						left outer join
					dbo.LRIDUE lri with(nolock) on
						sded.PN_NO = lri.PN_NO
				where
					sded.LOAN_TYPE = 'STL'
				group by
					sded.PN_NO
			) src on
				src.PN_NO = tgt.PN_NO
	end	
	
	select
		PN_TAG,
		RENEW,
		YEARLY_LRI,
		PN_NO,
		LOAN_TYPE,
		LOAN_STAT,
		DATE_GRANT,		-- GRANTED
		DATE_DUE,		-- DUE
		KBCI_NO,
		PRINCIPAL,		-- LOAN AMOUNT
		OUTSBAL,		-- BALANCE
		ARREARS,
		LRI_DUE,
		PAY_TAG,		-- DEDUCT
		PAY_AMT			-- DEDUCTION
	froM
		@SDEDBAL
	order by
		PN_NO,
		LOAN_TYPE
	
	--select	sum(case
	--			when PN_TAG = 1 then YEARLY_LRI
	--			else 0
	--			end) YLRI
	--from	dbo.SDEDBAL
	--where	ID = @v3
	
end

else if @QueryType = 'FixedDeposit'

	select
		*
	from
		dbo.FD with(nolock)
	where
		KBCI_NO = @v1
		
else if @QueryType = 'LoanTypeDetails'

	select
		lt.LOAN_TYPE_ID,
		p.PARAM_ID,
		lt.LOAN_TYPE,
		lt.LOAN_DESC,
		lt.CODE5,
		lt.MBAT,
		p.TERM,
		p.FREQ,
		p.RATE,
		p.[MAX],
		p.[MIN]
	from
		dbo.LOAN_TYPE lt with(nolock)
			inner join
		dbo.[PARAM] p with(nolock) on 
			p.LOAN_TYPE = lt.LOAN_TYPE
	order by
		lt.LOAN_TYPE

else if @QueryType = 'LoanHold'

	select
		*
	from
		LNHOLD with(nolock)
	where
		ACCTNO = @v1
		
else if @QueryType = 'Loan'

	select
		*
	from
		dbo.LOANS with(nolock)
	where
		PN_NO = @v1

else if @QueryType = 'LoanCount'

	select
		COUNT(PN_NO) Value
	from
		dbo.LOANS with(nolock)

else if @QueryType = 'LoanList'

	select
		PN_NO
	from
		dbo.LOANS with(nolock)
	order by
		PN_NO

else if @QueryType = 'LoanReleaseInsurance'

	select
		*
	from
		dbo.LRIDUE with(nolock)
	where
		PN_NO like @v1

else if @QueryType = 'MemberLoans'

	select
		*
	from
		dbo.LOANS with(nolock)
	where
		KBCI_NO like @v1
	order by
		CHKNO_DATE desc,
		LOAN_TYPE

else if @QueryType = 'MemberList'

	select
		KBCI_NO,
		dbo.func_FullName(LNAME, FNAME, MI) FULL_NAME
	from
		dbo.MEMBERS with(nolock)
	where
		KBCI_NO like @v1
	order by
		FULL_NAME

else if @QueryType = 'MemberCount'

	select
		count(KBCI_NO) Value
	from
		dbo.MEMBERS with(nolock)
	
else if @QueryType = 'Member'
	
	select
		*
	from
		dbo.MEMBERS with(nolock)
	where
		KBCI_NO like @v1

else if @QueryType = 'MonthlyDeduction'

	select
		*
	from
		dbo.MO_DEDN
	where
		PN_NO like @v1 and
		[DATE] = @dt1

else if @QueryType = 'OffCycleDeduction'

	select
		*
	from
		dbo.MO_DEDNO
	where
		PN_NO like @v1 and
		[DATE] = @dt1

else if @QueryType = 'SavingsControl'
	
	select top 1
		*
	from
		dbo.[CTRL_S] with(nolock)

else if @QueryType = 'SavingsDeposit'

	select
		*
	from
		dbo.SD with(nolock)
	where
		KBCI_NO = @v1

else if @QueryType = 'SavingsDepositMaster'

	select
		*
	from
		dbo.SDMASTER with(nolock)
	where
		'KBCI_NO' + KBCI_NO = @v1 or
		'ACCTNO'  + ACCTNO  = @v1 or
		KBCI_NO = @v1 or
		ACCTNO  = @v1

else if @QueryType = 'User'

	select
		USERNAME,
		USERPASS,
		[LEVEL]
	from
		dbo.[USER] with(nolock)
	where
		USERNAME = @v1 and
		USERPASS = @v2

else if @QueryType = 'CTRL'
	
	select top 1
		*
	from
		dbo.[CTRL]

else if @QueryType = 'PARAM'
	select	top 1 
			convert(numeric(3, 0), TERM) as TERM,
			convert(numeric(7, 4), RATE) as RATE, 
			convert(varchar(1), FREQ) as FREQ,
			[MAX],
			[MIN]
	from	dbo.[PARAM]
	where	LOAN_TYPE = @v1		
	
else if @QueryType = 'LRIDUE'
	select	LRI_DUE_C,
			LRI_DUE_P
	from	dbo.LRIDUE
	where	PN_NO = @v1

else if @QueryType = 'SDMASTER'
	select	ACCTNO,
			ACCTNAME,
			isnull(ACCTABAL, 0) as ACCTABAL
	from	dbo.SDMASTER
	where	ACCTNO = @v1 OR
			KBCI_NO = @v1

else if @QueryType = 'CTD'
	select	*
	from	dbo.CTD
	where	KBCI_NO = @v1

else if @QueryType = 'GEN_LAPP'
	exec s3p_J_Gen_Lapp @v1

else if @QueryType = 'ODEDBAL'
	select	OPN_NO,
			ODOX_TYPE,
			OREF,
			ODR,
			OCR,
			ORMK,
			OACCT_TYPE,
			ODEDBAL_ID
	from	dbo.ODEDBAL
	where	ID = @v1

else if @QueryType = 'SDEDBAL_INIT'
begin

	-- @v	KBCI_NO
	-- @v2	LOAN_TYPE
	-- @v3	ID
	-- @v4	MY_USER
	
	delete	dbo.SDEDBAL
	where	ID like @v4 + '%'

	delete	dbo.ODEDBAL
	where	ID like @v4 + '%'
	
	insert	dbo.SDEDBAL (
			ID,
			PN_TAG,
			RENEW,
			YEARLY_LRI,
			PN_NO,
			LOAN_TYPE,
			LOAN_STAT,
			DATE_GRANT,
			DATE_DUE,
			KBCI_NO,
			PRINCIPAL,
			OUTSBAL,
			ARREARS,
			LRI_DUE,
			PAY_TAG,
			PAY_AMT
			)
	select	@v3,
			convert(bit, case 
				when lon.LOAN_TYPE not in ('STL','SML') and lon.LOAN_TYPE = @v2 then 1
				else 0
				end),
			convert(bit, case 
				when lon.LOAN_TYPE not in ('STL','SML') and lon.LOAN_TYPE = @v2 then 1
				else 0
				end),
			isnull(lri.LRI_DUE_Y, 0),
			lon.PN_NO,
			lon.LOAN_TYPE,
			lon.LOAN_STAT,
			lon.DATE_GRANT,
			lon.DATE_DUE,
			lon.KBCI_NO,
			lon.PRINCIPAL,
			dbo.func_J_Preterm('C', lon.PN_NO, lon.LOAN_TYPE, @v4),
			lon.ARREAR_P + lon.ARREAR_I + lon.ARREAR_OTH,
			lon.LRI_DUE,
			' ',
			0
	from	dbo.LOANS lon
				left outer join
			dbo.LRIDUE lri
				on lon.PN_NO = lri.PN_NO
	where	lon.KBCI_NO = @v1 and
			lon.LOAN_STAT = 'R'
	
	if @xrenewSTL = 1
		update	dbo.SDEDBAL
		set		[PN_TAG] = 1,
				[PAY_TAG] = '1',
				[PAY_AMT] = OUTSBAL,
				[LRI_DUE] = a.LRI_DUE_C
		from	(
				select	sded.PN_NO, isnull(max(lri.LRI_DUE_C), 0) LRI_DUE_C
				from	dbo.SDEDBAL sded
							left outer join
						dbo.LRIDUE lri
							on sded.PN_NO = lri.PN_NO
				where	sded.ID = @v3 and
						sded.LOAN_TYPE = 'STL'
				group
				by		sded.PN_NO
				) a, SDEDBAL b
		where	ID = @v3 and
				a.PN_NO = b.PN_NO
	
	select	PN_TAG as [TAG],
			KBCI_NO,
			LOAN_TYPE as [TYPE],
			PN_NO,
			OUTSBAL as [BALANCE],						
			DATE_GRANT as [GRANTED],
			DATE_DUE as [DUE],
			PRINCIPAL as [LOAN_AMOUNT],			
			ARREARS,
			LRI_DUE,
			YEARLY_LRI,
			case PAY_TAG
				when ' ' then '    '
				when '1' then 'FULL'
				when '2' then 'ARR'
				when '4' then 'LRI'
				else 'OTH'
				end as [DEDUCT],
			PAY_AMT as [DEDUCTION],
			LOAN_STAT,
			SDEDBAL_ID,
			RENEW			
	from	dbo.SDEDBAL
	where	ID = @v3
	order
	by		PN_NO, LOAN_TYPE
	
	select	sum(case
				when PN_TAG = 1 then YEARLY_LRI
				else 0
				end) YLRI
	from	dbo.SDEDBAL
	where	ID = @v3
end

else if @QueryType = 'SDEDBAL_ADD'
begin
	-- @v	PN_NO
	-- @v2
	-- @v3	ID
	-- @v4	MY_USER
	
	if not exists (select 'X' from dbo.LOANS where PN_NO = @v1 and LOAN_STAT = 'R') return
	
	insert	dbo.SDEDBAL (
			ID,
			PN_TAG,
			RENEW,
			PN_NO,
			LOAN_TYPE,
			LOAN_STAT,
			DATE_GRANT,
			DATE_DUE,
			KBCI_NO,
			PRINCIPAL,
			OUTSBAL,
			ARREARS,
			LRI_DUE,
			PAY_TAG,
			PAY_AMT
			)	
	select	@v3,
			convert(bit, case 
				when LOAN_TYPE != 'STL' and LOAN_TYPE = @v2 then 1
				else 0
				end),
			convert(bit, case 
				when LOAN_TYPE != 'STL' and LOAN_TYPE = @v2 then 1
				else 0
				end),
			PN_NO,
			LOAN_TYPE,
			LOAN_STAT,
			DATE_GRANT,
			DATE_DUE,
			KBCI_NO,
			PRINCIPAL,
			dbo.func_J_Preterm('C', PN_NO, LOAN_TYPE, @v4),
			ARREAR_P + ARREAR_I + ARREAR_OTH,
			LRI_DUE,
			' ',
			0
	from	dbo.LOANS
	where	PN_NO = @v1
	
	select	PN_TAG as [TAG],
			RENEW,
			KBCI_NO,
			LOAN_TYPE as [TYPE],
			PN_NO,
			DATE_GRANT as [GRANTED],
			DATE_DUE as [DUE],
			PRINCIPAL as [LOAN_AMOUNT],
			OUTSBAL as [BALANCE],						
			ARREARS,
			LRI_DUE,
			YEARLY_LRI,
			case PAY_TAG
				when ' ' then '    '
				when '1' then 'FULL'
				when '2' then 'ARR'
				when '4' then 'LRI'
				else 'OTH'
				end as [DEDUCT],
			PAY_AMT as [DEDUCTION],
			LOAN_STAT,
			SDEDBAL_ID
	from	dbo.SDEDBAL
	where	ID = @v3 and 
			PN_NO = @v1

end

else if @QueryType = 'LA-1'
begin
	select	top 1
			SYSDATE,
			PAY_DAY,
			[CEILING]
	from	dbo.CTRL

	select	*
	from	dbo.LOANS
	where	KBCI_NO = @v1 and
			LOAN_TYPE = 'STL'
end

else if @QueryType = 'LA-2'
begin
	select	round
			(
				(
					isnull(PRINCIPAL, 0) + 
					isnull(ACCU_PAYP, 0) + 
					isnull(ARREAR_P, 0) + 
					isnull(ARREAR_I, 0) + 
					isnull(ARREAR_OTH, 0) + 
					isnull(P_BAL, 0) + 
					isnull(I_BAL, 0) + 
					isnull(O_BAL, 0)
				),
				2
			) XPLNAMT
	from	dbo.LOANS
	where	LOAN_TYPE + KBCI_NO + LOAN_STAT = @v1
	
	select	round(sum(PRINCIPAL), 2) XTOTAMT
	from	dbo.LOANS
	where	KBCI_NO = substring(@v1, 4, 7) and
			LOAN_STAT = 'R' and
			LOAN_TYPE != 'STL'	
end

else if @QueryType = 'LA-3'
begin
	select	top 1
			@SYSDATE = SYSDATE
	from	dbo.CTRL
	
	select	isnull(sum(case HOLDTYPE
				when 'CM' then isnull(HOLDAMT, 0)
				else isnull(HOLDAMT, 0) * -1
				end),
				0) XSAVBAL
	from	dbo.LNHOLD
	where	ACCTNO = @v1 and
			HOLDDATE = @SYSDATE
end

else if @QueryType = 'LA-5'
	begin
	select	top 1
			@SYSDATE = SYSDATE
	from	dbo.CTRL
	
	select	c.CTD_NO,
			c.KBCI_NO as _KBCI_NO_,
			c.NAME,
			c.PRINCIPAL,
			c.RATE,
			c.DUE_DATE,
			c.PRINCIPAL,
			c.PRINCIPAL - ISNULL(SUM(ch.CTD_AMTH), 0) CTD_AMT			
	from	dbo.CTD c
				left join
			dbo.CTDHLON ch
				on c.CTD_NO = ch.CTD_NO
	where	c.CTD_NO is not null and
			ltrim(rtrim(c.CTD_NO)) <> '' and
			-- c.DUE_DATE >= @dt1 and
			c.[STATUS] = 'NEW' and
			c.COLLATERAL = 1 and
			c.DUE_DATE >= @SYSDATE
	group
	by		c.CTD_NO, c.KBCI_NO, c.NAME, c.RATE, c.DUE_DATE, c.PRINCIPAL
	order
	by		c.CTD_NO
	end

else if @QueryType = 'LB-1'
	select	LOANS_ID, KBCI_NO, PN_NO, LOAN_TYPE, LOAN_STAT as cboLoanStatus, DATE_DUE, PRINCIPAL
	from	DBO.LOANS
	where	KBCI_No = @v1 AND
			LOAN_STAT = 'R'	

else if @QueryType = 'LB-2'
	select	top 1 
			a.LOANS_ID, 
			a.KBCI_NO, 
			a.PN_NO, 
			a.LOAN_TYPE as cboLoanType, 
			a.LOAN_STAT as cboLoanStatus, 
			a.DATE_DUE, 
			a.PRINCIPAL, 
			a.TERM as txtTerm, 
			a.RATE as txtRate, 
			a.FREQ as cboFrequency, 
			a.AMORT_AMT as txtAmortization, 
			a.MOD_PAY as cboPaymentMode, 
			a.PD, 
			a.CHKNO_DATE as txtReleaseDate, 
			a.PAY_START as txtPayStart, 
			a.DATE_DUE as txtDateDue, 			
			a.NDUE as txtNewDueDate,
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.' , '') MEMBER,
			b.FEBTC_SA
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	a.PN_No = @v1

else if @QueryType = 'LB-3'
	select	top 1
			a.LOANS_ID, 
			isnull(b.FNAME + ' ', '') + isnull(b.MI + '. ', '') + isnull(b.LNAME, '') MEMBERNAME,
			a.KBCI_NO,
			b.FEBTC_SA,
			a.COLLATERAL,
			a.LOAN_TYPE cboLoanType,
			a.LOAN_STAT cboLoanStatus,
			a.PN_NO,
			a.DATE_GRANT,
			a.DATE_DUE,
			a.RATE,
			a.ARREAR_AS,
			a.ARREAR_P,
			a.ARREAR_I,
			a.ARREAR_OTH,
			a.O_BAL,
			(select top 1 SYSDATE from DBO.CTRL) SYSDATE
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	a.PN_NO = @v1

else if @QueryType = 'LB-4'
	select	a.LOANS_ID, 
			a.KBCI_NO, 
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.', '') MEMBER,
			a.PN_NO, 
			a.LOAN_TYPE, 
			a.LOAN_STAT, 
			a.DATE_DUE, 
			a.PRINCIPAL
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	ORDER	BY MEMBER, PN_NO

else if @QueryType = 'LB-5'
	select	KBCI_NO,
			isnull(LNAME + ', ', '') + isnull(FNAME + ' ', '') + isnull(MI + '.', '') FULL_NAME,
			FEBTC_SA AS SAVINGS_ACCOUNT
	from	dbo.MEMBERS
	where	LTRIM(RTRIM(ISNULL(FEBTC_SA, ''))) != ''
	order
	by		FULL_NAME

else if @QueryType = 'LB-7'
	select	lon.KBCI_NO,
			isnull(LNAME + ', ', '') + isnull(FNAME + ' ', '') + isnull(MI + '.', '') FULL_NAME,
			lon.PN_NO,
			lon.LOAN_TYPE, 
			lon.PRINCIPAL
	from	dbo.LOANS lon
				inner join
			dbo.MEMBERS mem on
				mem.KBCI_NO = lon.KBCI_NO
	where	lon.LOAN_STAT = 'R'
	order
	by		FULL_NAME

else if @QueryType = 'LB-8'
	select	
		lon.KBCI_NO,
		isnull(LNAME + ', ', '') + isnull(FNAME + ' ', '') + isnull(MI + '.', '') FULL_NAME,
		lon.PN_NO,
		lon.LOAN_TYPE, 
		lon.PRINCIPAL,
		lon.DATE_GRANT,
		lon.DATE_DUE,
		MAX(
			CASE
				WHEN LOAN_STAT = 'F' THEN led.[DATE]
				END 
		) AS DATE_PAID
	from
		dbo.LOANS lon
			inner join
		dbo.MEMBERS mem on
			mem.KBCI_NO = lon.KBCI_NO
			left join
		dbo.LEDGER led on
			led.PN_NO = lon.PN_NO and
			led.ACCT_TYPE = 'PAY' and
			led.ACCT_CODE = 'PRI'
	where
		mem.KBCI_NO like @v1 and
		lon.LOAN_STAT = 'R'
	group by
		lon.KBCI_NO,
		isnull(LNAME + ', ', '') + isnull(FNAME + ' ', '') + isnull(MI + '.', ''),
		lon.PN_NO,
		lon.LOAN_TYPE, 
		lon.PRINCIPAL,
		lon.DATE_GRANT,
		lon.DATE_DUE			
	order by		
		FULL_NAME,
		lon.LOAN_TYPE,
		lon.DATE_GRANT

else if @QueryType = 'LC-1'
begin	
	select	@XBPRIN = SUM(case 
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ', 'TER', 'REP') THEN DR
				else 0
				end) -
			SUM(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ', 'TER', 'REP') THEN CR
				else 0
				end),
			@XPint = SUM(case
				when ACCT_CODE = 'int' THEN CR
				else 0
				end) -
			SUM(case
				when ACCT_CODE = 'int' THEN DR
				else 0
				end),
			@XLASTD = MAX(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ') THEN [DATE]
				else null
				end)
	from	dbo.LEDGER
	where	PN_NO = @v1
	
	select	top 1
			a.LOANS_ID, 
			a.KBCI_NO,
			a.APP_DATE as txtDateApplied,
			a.PN_NO,
			b.LNAME as txtLName,
			b.FNAME as txtFName,
			b.MI as txtMI,
			a.LOAN_TYPE as cboLoanType,
			a.LED_TYPE as cboLedgerType,
			a.TERM as txtTerm,
			a.FREQ as cboFrequency,
			a.RATE as txtRate,
			a.AMORT_AMT as txtAmortization,
			a.MOD_PAY as cboPaymentMode,
			a.CHKNO_DATE as txtReleaseDate,
			a.PD,
			a.DATE_DUE as txtDateDue,
			a.LOAN_STAT cboLoanStatus,
			a.PRINCIPAL as txtLoanAmount,
			a.PRINCIPAL + @XBPRIN as txtXBPrin,
			a.PAY_START as txtPayStart,
			isnull(@XLASTD, a.PAY_START) XLASTD,
			a.ARREAR_P as txtXARRP,
			a.ARREAR_I as txtXARRI,
			a.ARREAR_OTH as txtXARRO,
			a.ARREAR_AS as txtXARRD,
			(select top 1 SYSDATE from DBO.CTRL) txtXTRAND,
			@XPINT as txtXPINT
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO				
	where	a.PN_NO = @v1	
end

else if @QueryType = 'LC-2'
begin
	select	@XBPRIN = SUM(case 
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ', 'TER', 'REP') THEN DR - CR
				else 0
				end),
			@XPint = SUM(case
				when ACCT_CODE = 'int' THEN CR - DR
				else 0
				end),
			@XLASTD = MAX(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ') THEN [DATE]
				else null
				end),
			@XLASPD = MAX(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE = 'PAY' THEN [DATE]
				else null
				end)
	from	dbo.LEDGER
	where	PN_NO = @v1
	
	select	@AXGOMO = case
				when FREQ = 'M' THEN -1
				when FREQ = 'S' THEN -6
				when FREQ = 'Q' THEN -3
				when FREQ = 'A' THEN -12
				end,
			@AXFREQ = case
				when FREQ = 'M' THEN 12
				when FREQ = 'S' THEN 6
				when FREQ = 'Q' THEN 4
				when FREQ = 'A' THEN 1
				end
	from	dbo.LOANS
	where	PN_NO = @v1	
	
	if @XLASPD is null 
		set @XLASPD = null
	else
		set @XLASPD = DATEADD(MM, ABS(@AXGOMO), @XLASPD)
	
	select	top 1 
			@SYSDATE = SYSDATE 
	from	DBO.CTRL
	
	select	top 1
			a.LOANS_ID, 
			a.KBCI_NO,
			a.APP_DATE as txtDateApplied,
			a.PN_NO,
			b.LNAME as txtLName,
			b.FNAME as txtFName,
			b.MI as txtMI,
			a.LOAN_TYPE as cboLoanType,
			a.LED_TYPE as cboLedgerType,
			a.TERM as txtTerm,
			a.FREQ as cboFrequency,
			a.RATE as txtRate,
			a.AMORT_AMT as txtAmortization,
			a.MOD_PAY as cboPaymentMode,
			a.CHKNO_DATE as txtReleaseDate,
			a.PD,
			a.DATE_DUE as txtDateDue,
			a.LOAN_STAT as cboLoanStatus,
			a.PAY_START as txtPayStart,
			a.PRINCIPAL as txtLoanAmount,
			a.PRINCIPAL + @XBPRIN as txtXBPrin,
			isnull(@XLASTD, a.PAY_START) XLASTD,
			a.ARREAR_P txtXARRP,
			a.ARREAR_I txtXARRI,
			a.ARREAR_OTH txtXARRO,
			a.ARREAR_AS txtXARRD,
			@SYSDATE txtXTRAND,
			@SYSDATE SYSDATE,
			@XPINT txtXPINT,
			@XLASPD XLASPD,
			@AXGOMO AXGOMO,
			@AXFREQ AXFREQ			
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	a.PN_NO = @v1	
end

else if @QueryType = 'LC-3'
	select	SUM(case
				when ACCT_CODE = 'INT' THEN CR - DR
				else 0
				end) XARRIP,
			SUM(case
				when ACCT_CODE = 'OTH' THEN CR - DR
				else 0
				end) XARROP
	from	dbo.LEDGER
	where	PN_NO = @v1 AND
			[DATE] = @dt1

else if @QueryType = 'LE-1'
begin
	select	top 1 
			@SYSDATE = SYSDATE,
			@PAY_DAY = PAY_DAY
	from	DBO.CTRL

	select	*,
			@SYSDATE SYSDATE,
			@PAY_DAY PAY_DAY
	from	dbo.LOANS
	where	KBCI_NO = @v1	
end

else if @QueryType = 'LE-2'
	select	'X'			
	from	dbo.LOANS
	where	KBCI_NO = @v1 and
			LOAN_TYPE = 'RSL' and
			LOAN_STAT = 'R'

else if @QueryType = 'LF-1'
begin
	select	@KBCI_NO = KBCI_NO
	from	dbo.LOANS	
	where	PN_NO = @v1
	
	select	top 1
			@FEBTC_SA = FEBTC_SA
	from	dbo.MEMBERS
	where	KBCI_NO = @KBCI_NO
		
	select	*
	from	dbo.LEDGER
	where	PN_NO = @v1
	order	
	by		[DATE]
	
	select	*
	from	dbo.LNHOLD
	where	ACCTNO = @FEBTC_SA	
	
	select	*
	from	dbo.LRIDUE
	where	PN_NO = @v1
	
	select	*
	from	dbo.RLRIDUE
	where	PN_NO = @v1
	
	select	*
	from	dbo.PAYHIST
	where	PN_NO = @v1
	
	select	*
	from	dbo.MEMBERS
	where	KBCI_NO = @KBCI_NO
	
	select	*
	from	dbo.LOANS
	where	PN_NO = @v1
	
	select	SYSDATE
	from	dbo.CTRL	
end

else if @QueryType = 'LF-2'
begin
	select	top 1
			@SYSDATE = SYSDATE
	from	dbo.CTRL
	
	select	distinct
			b.KBCI_NO,
			isnull(c.LNAME + ', ', '') + isnull(c.FNAME + ' ', '') + isnull(c.MI + '.', '') MEMBER,
			b.PN_NO,
			b.LOAN_TYPE,
			b.PRINCIPAL
	from	dbo.LEDGER a
				inner join
			dbo.LOANS b
				on a.PN_NO = b.PN_NO
				inner join
			dbo.MEMBERS c
				on b.KBCI_NO = c.KBCI_NO
	where	a.[DATE] = @SYSDATE and
			b.CHKNO_DATE != @SYSDATE
	order
	by		MEMBER
end

else if @QueryType = 'LH-1'
begin
	select	@XLASTD = MAX(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ') THEN [DATE]
				else null
				end),
			@XLASPD = MAX(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE = 'PAY' THEN [DATE]
				else null
				end)
	from	dbo.LEDGER
	where	PN_NO = @v1
	
	select	@AXGOMO = case
				when FREQ = 'M' THEN -1
				when FREQ = 'S' THEN -6
				when FREQ = 'Q' THEN -3
				when FREQ = 'A' THEN -12
				end,
			@AXFREQ = case
				when FREQ = 'M' THEN 12
				when FREQ = 'S' THEN 6
				when FREQ = 'Q' THEN 4
				when FREQ = 'A' THEN 1
				end
	from	dbo.LOANS
	where	PN_NO = @v1	
	
	if @XLASPD is null 
		set @XLASPD = null
	else
		set @XLASPD = DATEADD(MM, ABS(@AXGOMO), @XLASPD)
	
	select	top 1 
			@SYSDATE = SYSDATE 
	from	DBO.CTRL
	
	select	top 1
			a.LOANS_ID, 
			a.KBCI_NO,
			a.APP_DATE as txtDateApplied,
			a.PN_NO,
			b.LNAME as txtLName,
			b.FNAME as txtFName,
			b.MI as txtMI,
			a.LOAN_TYPE as cboLoanType,
			a.LED_TYPE as cboLedgerType,
			a.TERM as txtTerm,
			a.FREQ as cboFrequency,
			a.RATE as txtRate,
			a.AMORT_AMT as txtAmortization,
			a.MOD_PAY as cboPaymentMode,
			a.CHKNO_DATE as txtReleaseDate,
			a.PD,
			a.DATE_DUE as txtDateDue,
			a.LOAN_STAT as cboLoanStatus,
			a.PAY_START as txtPayStart,
			a.PRINCIPAL as txtLoanAmount,
			isnull(@XLASTD, a.PAY_START) XLASTD,
			a.P_BAL as txtXPAMT,
			a.I_BAL as txtXIAMT,
			a.O_BAL as txtXOAMT,
			a.ARREAR_P as txtXARRP,
			a.ARREAR_I as txtXARRI,
			a.ARREAR_OTH as txtXARRO,
			a.ARREAR_AS as txtXARRD,
			@SYSDATE SYSDATE,
			@XLASPD XLASPD,
			@AXGOMO AXGOMO,
			@AXFREQ AXFREQ
	from	DBO.SLOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO				
	where	a.PN_NO = @v1	
end

else if @QueryType = 'PD-1'
	select	distinct
			a.KBCI_NO,
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.', '') MEMBER
	from	dbo.ADVANCE a
				inner join
			dbo.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO	

else if @QueryType = 'PD-2'
	select	a.PN_NO,
			a.LOAN_TYPE,
			a.AMOUNT,
			a.ACCTNO,
			a.[STATUS],
			a.REMARKS,
			a.ADVANCE_ID
	from	dbo.ADVANCE a
				inner join
			dbo.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	b.KBCI_NO = @v1
	order
	by		PN_NO

else if @QueryType = 'PG-1'
	select	isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.', '') FULL_NAME,
			isnull(adv.AMOUNT, 0) AMOUNT
	from	dbo.MEMBERS mem
				left join
			dbo.ADVICE adv on
				adv.EMPNO = mem.CB_EMPNO
	where	mem.CB_EMPNO = @v1
	
else if @QueryType = 'OD-1'
	select
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.', '') FULL_NAME,
		dbo.func_Format241(lon.PN_NO) PN_NO,
		lt.LOAN_DESC,
		lon.PRINCIPAL
	from
		dbo.LOANS lon
			inner join
		dbo.LOAN_TYPE lt on
			lt.LOAN_TYPE = lon.LOAN_TYPE
			inner join
		dbo.MEMBERS mem on
			mem.KBCI_NO = lon.KBCI_NO
			inner join
		dbo.LRIDUE lri on
			lri.PN_NO = lon.PN_NO
	where
		lon.PN_NO like @v1 and
		lon.LOAN_STAT = 'R' and
		lri.LRI_DUE_P > 0
	order by
		FULL_NAME,
		lon.LOANS_ID

else if @QueryType = 'OD-2'
	select
		'[' + dbo.func_Format241(mem.KBCI_NO) + '] ' + isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.', '') MEMBER,
		dbo.func_Format241(lon.PN_NO) PN_NO,
		lt.LOAN_DESC,
		lon.PRINCIPAL,
		lon.TERM,
		lon.RATE,
		CASE lon.FREQ
			WHEN 'M' THEN 'Monthly'
			WHEN 'Q' THEN 'Quarterly'
			WHEN 'S' THEN 'Semi-Annual'
			WHEN 'A' THEN 'Annual'
			WHEN 'D' THEN 'Daily'
			END FREQ,
		lon.AMORT_AMT,
		CASE CONVERT(INT, lon.MOD_PAY)
			WHEN 1 THEN 'Payroll'
			WHEN 2 THEN 'PDC'
			WHEN 3 THEN 'DM'
			END MOD_PAY,
		CASE lon.PD
			WHEN 0 THEN 'Current'
			WHEN 1 THEN 'Past Due'
			end PD,
		lon.CHKNO_DATE,
		lon.PAY_START,
		lon.DATE_DUE,
		CASE lon.LOAN_STAT
			WHEN 'F' THEN 'Fully Paid'
			WHEN 'R' THEN 'Released'
			WHEN 'T' THEN 'Terminated'
			END LOAN_STAT,
		lon.NDUE
	from
		dbo.LOANS lon
			left join
		dbo.LOAN_TYPE lt on
			lt.LOAN_TYPE = lon.LOAN_TYPE
			left join
		dbo.LRIDUE lri on
			lri.PN_NO = lon.PN_NO
			left join
		dbo.MEMBERS mem on
			mem.KBCI_NO = lon.KBCI_NO
	where
		lon.PN_NO = @v1

else if @QueryType = 'OD-3'
	select
		lon.PN_NO,
		lon.KBCI_NO,
		mem.FEBTC_SA,
		lri.LRI_DUE_P,
		SUM (
			case
			when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE IN ('PAY', 'ADJ', 'AMT') then ISNULL(led.DR, 0) - ISNULL(led.CR, 0)
			else 0
			end
		) xbprin,
		MAX(
			case
			when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE IN ('PAY', 'ADJ', 'AMT') then led.[DATE]
			end
		) xlastd
	from
		dbo.LOANS lon
			inner join
		dbo.MEMBERS mem on
			mem.KBCI_NO = lon.KBCI_NO
			inner join
		dbo.LEDGER led on 
			led.PN_NO = lon.PN_NO
			left join
		dbo.LRIDUE lri on
			lri.PN_NO = lon.PN_NO
	where
		lon.PN_NO = @v1
	group by
		lon.PN_NO,
		lon.KBCI_NO,
		mem.FEBTC_SA,
		lri.LRI_DUE_P

else if @QueryType = 'OD-4'
	select
		PAYOR
	from
		PAYHIST pay
	where
		pay.PAYOR = @v1
		
else if @QueryType = 'OD-5'
begin
	select
		@SYSDATE = SYSDATE
	from
		dbo.CTRL
		
	select
		sdm.ACCTABAL as XABAL,
		ctl.MINBAL as XMBAL,
		sum(
			hld.HOLDAMT
		) XHBAL
	from
		SDMASTER sdm
			left join
		LNHOLD hld on
			sdm.ACCTNO = hld.ACCTNO and
			hld.HOLDCD = 'PAY' and
			hld.HOLDTYPE = 'DM' and
			hld.POSTSTAT != 'Y' and
			hld.HOLDDATE >= @SYSDATE
			cross join
		CTRL_S ctl
	where
		sdm.ACCTNO = @v1
	group by
		sdm.ACCTABAL,
		ctl.MINBAL
end

else if @QueryType = 'MA-1'
	select	top 1
			a.PN_NO,
			a.KBCI_NO as txtKBCI,
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.', '') MEMBER,
			a.MOD_PAY as cboPaymentMode,
			a.AMORT_AMT as txtNXAmortization,
			a.PAY_START as txtDXPayStart,
			a.LED_TYPE as cboLedgerType,
			a.LOAN_TYPE as cboLoanType,
			a.LOAN_STAT as cboLoanStatus,
			a.ACCU_PAYP as txtNXAccuPymts,
			a.YTD_I as txtNXYTDInt,
			a.PD as chkPD,
			a.ARREAR_P as txtNXARRPrin,
			a.ARREAR_I as txtNXARRInt,
			a.ARREAR_OTH as txtNXARROthers,
			a.ARREAR_AS as txtDXARRAsOf,
			a.P_BAL as txtNXPrincipal,
			a.I_BAL as txtNXInterest,
			a.O_BAL as txtNXOthers
	from	dbo.LOANS a
				inner join
			dbo.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	PN_NO = @v1

else if @QueryType = 'MA-2'
	select	a.PN_NO,
			a.KBCI_NO,
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.', '') MEMBER,
			a.MOD_PAY as cboPaymentMode,
			a.AMORT_AMT as txtNXAmortization,
			a.PAY_START as txtDXPayStart,
			a.LED_TYPE as cboLedgerType,
			a.LOAN_TYPE as cboLoanType,
			a.LOAN_STAT as cboLoanStatus			
	from	dbo.LOANS a
				inner join
			dbo.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	order by
			MEMBER

else if @QueryType = 'MA-3'
	select	[LOANS_ID],
			[PN_NO],
			[LOAN_TYPE],
			[KBCI_NO],
			[APP_DATE],
			[APP_NO],
			[DATE_GRANT],
			[BY_WHOM],
			[DATE_DUE],
			[CHKNO_BANK],
			[CHKNO],
			[CHKNO_AMT],
			[CHKNO_DATE],
			[CHKNO_RELS],
			[CHKNO_ACK],
			[MOD_PAY],
			[AMORT_AMT],
			[PAY_START],
			[RATE],
			[TERM],
			[FREQ],
			[PRINCIPAL],
			[LED_TYPE],
			[ADV_INTE],
			[AFT_INTE],
			[ACCU_PAYP],
			[YTD_I],
			[LOAN_STAT],
			[ARREAR_I],
			[ARREAR_P],
			[ARREAR_OTH],
			[ARREAR_AS],
			[COLLATERAL],
			[DED_BAL],
			[ADD_DATE],
			[CHG_DATE],
			[USER],
			[P_BAL],
			[I_BAL],
			[O_BAL],
			[REC_STAT],
			[RENEW],
			[ADVANCE],
			[LRI_IND],
			[NDUE],
			[L_EXT],
			[PD],
			[LRI_DUE]
	from	dbo.LOANS
	where	KBCI_NO = @v1 and
			LOAN_STAT = 'R'
	order
	by		LOANS_ID

else if @QueryType = 'MA-4'
	select	[LOANS_ID],
			[PN_NO],
			[LOAN_TYPE],
			[KBCI_NO],
			[APP_DATE],
			[APP_NO],
			[DATE_GRANT],
			[BY_WHOM],
			[DATE_DUE],
			[CHKNO_BANK],
			[CHKNO],
			[CHKNO_AMT],
			[CHKNO_DATE],
			[CHKNO_RELS],
			[CHKNO_ACK],
			[MOD_PAY],
			[AMORT_AMT],
			[PAY_START],
			[RATE],
			[TERM],
			[FREQ],
			[PRINCIPAL],
			[LED_TYPE],
			[ADV_INTE],
			[AFT_INTE],
			[ACCU_PAYP],
			[YTD_I],
			[LOAN_STAT],
			[ARREAR_I],
			[ARREAR_P],
			[ARREAR_OTH],
			[ARREAR_AS],
			[COLLATERAL],
			[DED_BAL],
			[ADD_DATE],
			[CHG_DATE],
			[USER],
			[P_BAL],
			[I_BAL],
			[O_BAL],
			[REC_STAT],
			[RENEW],
			[ADVANCE],
			[LRI_IND],
			[NDUE],
			[L_EXT],
			[PD],
			[LRI_DUE]
	from	dbo.LOANS
	where	KBCI_NO = @v1
	order
	by		LOANS_ID
			
else if @QueryType = 'MB-1'
	select	KBCI_NO,
			LNAME as txtLName,
			FNAME as txtFName,
			MI as txtMI,
			MEM_ADDR as txtAddress,
			MEM_CODE as txtMembershipCode,
			MEM_STAT as txtMembershipStat,
			CB_EMPNO as txtCBEmpNo,
			DEPT as txtDepartment,
			REGION as txtRegion,
			OFF_TEL as txtUXOfficeTel,
			DORI as chkDORI,
			REA_DORI as txtReasonDORI,
			CIV_STAT as txtCivilStatus,
			RES_TEL as txtUXResidenceTel,
			POSITION as txtPosition,
			SAL_BAS as txtNXBasicSalary,
			SAL_ALL as txtNXAllowance,
			OTH_INC as txtNXOtherIncome,
			FEBTC_SA as txtUXFEBTCSA,
			FEBTC_CA as txtUXFEBTCCA,
			YTD_DIVAMT as txtNXYTDDividend,
			YTD_LRI as txtNXYTDLRI,
			REM_VALUE as txtNXREMValue,
			NO_DEPEND as txtIXDependents,
			SP_NAME as txtSpouseName,
			SP_EMPLOY as txtSPEmployer,
			SP_OFFTEL as txtUXSpouseEmployer,
			SP_CBEMPNO as txtSpouseCBEmpNo,
			SP_KBCINO as txtSpouseKBCI,
			SP_SALARY as txtNXSpouseSal,
			SD_AMOUNT as txtNXSABalance,
			FD_AMOUNT as txtNXFDBalance
	from	dbo.MEMBERS
	where	KBCI_NO = @v1

else if @QueryType = 'MB-2'
	select	KBCI_NO,
			isnull(LNAME + ', ', '') + isnull(FNAME + ' ', '') + isnull(MI + '.', '') MEMBER,
			MEM_STAT,
			DORI,
			CB_EMPNO,			
			DEPT,
			POSITION
	from	dbo.MEMBERS
	order by
			MEMBER

else if @QueryType = 'MC-1'
	select	LOAN_TYPE,
			TERM txtIXTerm, 
			FREQ cboFrequency, 
			RATE txtNXRate, 
			[MIN] txtNXMinLoan, 
			[MAX] txtNXMaxLoan
	from	dbo.[PARAM]
	order
	by		LOAN_TYPE

else if @QueryType = 'MD-1'
	select	top 1
			MAPP_NO as txtMAppNo,
			LAPP_DATE as txtDXLAppDate,
			LAPP_NO as txtIXLAppNo,
			KBCI_NO as txtKBCINo,
			PN_NO as txtIXPNNo,
			PAY_DAY as txtDXPayDate,
			L_DM as txtIXDMNo,
			L_CM as txtIXCMNo,
			[CEILING] as txtNXCeiling
	from	dbo.CTRL

else if @QueryType = 'ME-1'
	select	[OR_ID] as OR_ID,
			[PN_NO] as txtUXPN_NO,
			[DATE] as txtDXDATE,
			[REF] as txtREF,
			[AMT] as txtNXAMT,
			[POSTED] as chkPOSTED,
			[RMK] as txtRMK,			
			[REC_STAT] as chkREC_STAT
	from	dbo.[OR]

else if @QueryType = 'MF-1'
	select	[LEDGER_ID] as LEDGER_ID,
			[PN_NO] as txtUXPN_NO,
			[DATE] as txtDXDATE,
			[DOX_TYPE] as txtDOX_TYPE,
			[REF] as txtREF,
			[ACCT_TYPE] as txtACCT_TYPE,
			[ACCT_CODE] as txtACCT_CODE,
			[BEGBAL] as txtNXBEGBAL,
			[DR] as txtNXDR,
			[CR] as txtNXCR,
			[ENDBAL] as txtNXENDBAL,
			[RMK] as txtRMK,
			[ADD_DATE] as txtDXADD_DATE,
			[USER] as txtUSER
	from	dbo.LEDGER
	where	[PN_NO] = @v1
	order	by
			[DATE]

else if @QueryType = 'ML-1'
	select	isnull(LNAME + ', ', '') + isnull(FNAME + ' ', '') + isnull(MI + '.', '') MEMBER,
			KBCI_NO
	from	dbo.MEMBERS
	order
	by		isnull(LNAME + ', ', '') + isnull(FNAME + ' ', '') + isnull(MI + '.', '')

else if @QueryType = 'ML-2'
	select	case lon.LOAN_STAT
				when 'P' then 'Past Due'
				when 'T' then 'Terminated'
				when 'F' then 'Fully Paid'
				when 'R' then 'Released'
				when 'A' then 'Approved'
				when 'D' then 'Disapproved'
				when ' ' then 'New Application'
				end [LOAN_STATUS],
			mem.KBCI_NO,
			lon.PN_NO,			
			lon.LOAN_TYPE,
			lon.DATE_GRANT,
			lon.DATE_DUE,
			mem.MEM_STAT,
			lon.RATE,
			lon.TERM,
			lon.FREQ,
			lon.PRINCIPAL PRINCIPAL
	from	dbo.MEMBERS mem
				inner join
			dbo.LOANS lon
				on mem.KBCI_NO = lon.KBCI_NO
	where	mem.KBCI_NO = @v1
	order
	by		lon.DATE_GRANT
	
else if @QueryType = 'ML-3'
	select	'X'
	from	dbo.S_ACCNT
	where	KBCI_NO = @v1

else if @QueryType = 'EOD-1'
begin
	select	top 1
			@SYSDATE = SYSDATE
	from	dbo.CTRL

	select	sum(case
				when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ') then ISNULL(led.DR, 0) - ISNULL(led.CR, 0)
				else 0
				end) XBPRIN,			
			sum(case
				when ACCT_CODE = 'INT' and ACCT_TYPE in ('PAY', 'ADJ') then ISNULL(led.CR, 0) - ISNULL(led.DR, 0)
				else 0
				end) XPINT,
			sum(case
				when ACCT_CODE = 'OTH' and ACCT_TYPE in ('PAY', 'ADJ') then ISNULL(led.CR, 0) - ISNULL(led.DR, 0)
				else 0
				end) XPOTH,
			sum(case
				when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') then ISNULL(led.DR, 0) - ISNULL(led.CR, 0)
				else 0
				end) XBPRIN_PATR,
			sum(case
				when ACCT_CODE = 'INT' then ISNULL(led.CR, 0) - ISNULL(led.DR, 0)
				else 0
				end) XPINT_ALL,
			case lon.FREQ
				when 'M' then DATEADD(D, 1, DATEADD(M, -1, lon.NDUE))
				when 'Q' then DATEADD(D, 1, DATEADD(M, -3, lon.NDUE))
				when 'S' then DATEADD(D, 1, DATEADD(M, -6, lon.NDUE))
				when 'A' then DATEADD(D, 1, DATEADD(M, -12, lon.NDUE))
				end LDUE,
			sum(case
				when ACCT_CODE = 'OTH' and ACCT_TYPE in ('PAY', 'ADJ', 'TER') and led.[DATE] > lon.ARREAR_AS then ISNULL(led.CR, 0) - ISNULL(led.DR, 0)
				else 0
				end) TARRP,
			sum(case
				when ACCT_CODE = 'INT' and ACCT_TYPE in ('PAY', 'ADJ', 'TER') and led.[DATE] > z.XLASTD then ISNULL(led.CR, 0) - ISNULL(led.DR, 0)
				else 0
				end) TINTP,
			sum(case
				when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ', 'TER') and led.[DATE] > lon.ARREAR_AS then ISNULL(led.CR, 0) - ISNULL(led.DR, 0)
				else 0
				end) TPPRI,
			sum(case
				when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and led.[DATE] = y.LRI_BALDA24 then ISNULL(led.DR, 0) - ISNULL(led.CR, 0)
				else 0
				end) XBPRIN_LRIDUE,
			dbo.func_J_AMORT(lon.PRINCIPAL, lon.AMORT_AMT, lon.FREQ, lon.RATE, lon.PAY_START, @sysdate) AMORT,
			z.PRI_PA_DATE,			
			lon.PN_NO
	from	dbo.LOANS lon
				left join
			dbo.LEDGER led
				on lon.PN_NO = led.PN_NO
				left join
			(
				select	max(case
							when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ') then led.[DATE]
							--else 0
							end) PRI_PA_DATE,
						isnull(
							max(case
								when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ') then led.[DATE]
								--else 0
								end),
							lon.PAY_START) XLASTD,
						lon.PN_NO
				from	dbo.LOANS lon
							left join
						dbo.LEDGER led
							on lon.PN_NO = led.PN_NO
				group
				by		lon.PN_NO, lon.PAY_START
			) as z
				on z.PN_NO = lon.PN_NO
				left join
			(
				select	lon.PN_NO,
						DATEADD(M, -24, lri.LRI_BALDA) as LRI_BALDA24
				from	dbo.LOANS lon
							left join
						dbo.LRIDUE lri
							on lri.PN_NO = lon.PN_NO							
			) as y
				on y.PN_NO = lon.PN_NO			
	group
	by		lon.PN_NO, lon.PRINCIPAL, lon.AMORT_AMT, lon.FREQ, lon.RATE, lon.PAY_START, lon.NDUE, z.PRI_PA_DATE
end

else if @QueryType = 'sp_params'
begin

	select
		sc.name
	from
		sys.sysobjects so
			inner join
		sys.syscolumns sc
			on so.id = sc.id
	where
		so.name = @v1
	order by
		sc.colorder

end
	
else if @QueryType = 'cs'

	select
		'public ' +
		case
			when st.name like 'bigint' then 'integer'
			when st.name like '%int' then 'Int32'
			when st.name like '%char' then 'string'
			when st.name like 'bit' then 'bool'
			when st.name like 'date%' then 'DateTime'
			when st.name like 'numeric' then 'decimal'
			end + ' ' +
		sc.name +
		' { get; set; }'
	from
		sys.sysobjects so
			inner join
		sys.syscolumns sc on
			so.id = sc.id
			inner join
		sys.systypes st on
			sc.xusertype = st.xusertype
	where
		so.name = @v1 and
		so.xtype = @v2
	order by
		sc.colorder

else if @QueryType = 'sp'
begin

	declare @params nvarchar(45)

	select
		@params = ISNULL(@params + ',NULL', 'NULL')
	from
		sys.sysobjects so
			inner join
		sys.syscolumns sc on
			so.id = sc.id
	where
		so.name = @v1

	exec
	(
		'
		select
			*
		into
			#tempsp
		from
			openrowset
			(
				''SQLNCLI10'', 
				--''Server=(local)\' + @@SERVICENAME + ';Database=KBCI;Uid=KBCIuser;Pwd=+kbci+;'',
				''Server=' + @@SERVERNAME + ';Database=KBCI;Uid=KBCIuser;Pwd=+kbci+;'',
				''SET NOCOUNT ON; SET FMTONLY OFF; EXEC ' + @v1 + ' ' + @params + '''
			);
			
		select
			''public '' +
			case
				when st.name like ''bigint'' then ''integer''
				when st.name like ''%int'' then ''Int32''
				when st.name like ''%char'' then ''string''
				when st.name like ''bit'' then ''bool''
				when st.name like ''date%'' then ''DateTime''
				when st.name like ''numeric'' then ''decimal''
				end + '' '' +
			sc.name +
			'' { get; set; }''
		from
			tempdb.sys.sysobjects so
				inner join
			tempdb.sys.syscolumns sc on
				so.id = sc.id
				inner join
			tempdb.sys.systypes st on
				sc.xusertype = st.xusertype
		where
			so.id = object_id(''tempdb..#tempsp'') and
			so.xtype = ''U''
		order by
			sc.colorder
		'
	)	
		
end



GO


