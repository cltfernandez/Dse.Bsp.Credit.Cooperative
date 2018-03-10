USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_SubsidiaryLoanLedger]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_SubsidiaryLoanLedger]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_SubsidiaryLoanLedger]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_SubsidiaryLoanLedger]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_Loans_SubsidiaryLoanLedger]
@pn_no varchar(7),
@dt1 date,
@dt2 date
AS

declare @PRINCIPAL numeric(12,4)

declare @SL table (
	[UID]				int identity(1,1),
	[DATES]				date,
	[REF]				varchar(10),
	[RMK]				varchar(35),
	[LEDGER_ID]			bigint,
	[LEDGER_ID_PRI]		bigint,
	[AMOUNT_LOANED]		numeric(12,4),
	[PRINCIPAL_PAID]	numeric(12,4),
	[BALANCE]			numeric(12,4),
	[INTEREST_PAID]		numeric(12,4),
	[PENALTIES]			numeric(12,4),
	[LRI]				numeric(12,4),
	[OTHERS]			numeric(12,4)
)

/* get amounts */
insert into @SL (
	[DATES],
	[REF],
	[RMK],
	[LEDGER_ID],
	[LEDGER_ID_PRI],
	[AMOUNT_LOANED],
	[PRINCIPAL_PAID],
	[BALANCE],
	[INTEREST_PAID],
	[PENALTIES],
	[LRI],
	[OTHERS]
)
select	
	[DATE] DATES,
	[REF],		
	case
		when LEFT([RMK], 17) = 'PAYROLL DEDUCTION' THEN LEFT([RMK], 17)
		when [RMK] in ('INITIAL - ADD. INTEREST', 'INITIAL - PRINCIPAL') THEN 'INITIAL'
		else [RMK]
		end RMK,
	min([LEDGER_ID]) LEDGER_ID,
	max
	(
		case
			when RMK like '%INITIAL - PRINCIPAL%' then [LEDGER_ID]
			when ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and ACCT_CODE = 'PRI' then [LEDGER_ID]
			else 0
			end
	) LEDGER_ID_PRI,
	sum
	(	case
			when ACCT_TYPE = 'AMT' and ACCT_CODE = 'PRI' then DR
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE = 'PRI' and DOX_TYPE = 'DM' then DR
			else 0
			end
	) AMOUNT_LOANED,
	sum
	(
		case
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE = 'PRI' and DOX_TYPE != 'DM' then CR
			else 0
			end
	) PRINCIPAL_PAID,
	0 BALANCE,
	sum
	(	case
			when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then CR - DR
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE = 'INT' then CR - DR
			else 0
			end
	) INTEREST_PAID,
	sum
	(
		case
			when ACCT_CODE = 'OTH' and ACCT_TYPE != 'LRI' and RMK like '%PEN%' and not RMK like '%INIT%' then CR - DR
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE in ('OTH', 'PEN') then CR - DR
			else 0
			end
	) PENALTIES,
	sum
	(
		case
			when ACCT_CODE = 'OTH' and ACCT_TYPE = 'LRI' and (RMK like '%INITIAL%' OR RMK like '%LOAN ADJUSTMENT%' OR RMK like '%PAYMENT%') then CR - DR
			when ACCT_CODE = 'OTH' and ACCT_TYPE = 'LRI' and SUBSTRING(RMK, 10, 7) = @pn_no then CR - DR
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE like '%LRI%' then CR - DR
			else 0
			end
	) LRI,
	sum
	(
		case
			when ACCT_TYPE = 'AMT' and ACCT_CODE = 'PRI' then 0
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE = 'PRI' and DOX_TYPE = 'DM' then 0
			when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then 0
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE = 'INT' then 0
			when ACCT_CODE = 'OTH' and ACCT_TYPE != 'LRI' and RMK like '%PEN%' and not RMK like '%INIT%' then 0
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE in ('OTH', 'PEN') then 0
			when ACCT_CODE = 'OTH' and ACCT_TYPE = 'LRI' and (RMK like '%INITIAL%' OR RMK like '%LOAN ADJUSTMENT%' OR RMK like '%PAYMENT%') then 0
			when ACCT_CODE = 'OTH' and ACCT_TYPE = 'LRI' and SUBSTRING(RMK, 10, 7) = @pn_no then 0
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE like '%LRI%' then 0
			when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE = 'PRI' and DOX_TYPE != 'DM' then 0
			else CR - DR
			end
	) OTHERS
from
	dbo.LEDGER with(nolock)
where
	PN_NO = @pn_no
group by
	[DATE],
	[REF],
	case
		when LEFT([RMK], 17) = 'PAYROLL DEDUCTION' THEN LEFT([RMK], 17)
		when [RMK] in ('INITIAL - ADD. INTEREST', 'INITIAL - PRINCIPAL') THEN 'INITIAL'
		else [RMK]
		end
order by
	[DATE],
	min([LEDGER_ID])

/* get principal */
select
	@PRINCIPAL = [PRINCIPAL]
from
	dbo.LOANS with(nolock)
where
	[PN_NO] = @pn_no

/* set principal */
update
	@SL
set
	[AMOUNT_LOANED] = @PRINCIPAL,
	[BALANCE] = @PRINCIPAL,
	[RMK] = 'BEGINNING BALANCE'
where
	[UID] = 1

/* get and set ending balance when principal gets paid */
update
	tmp
set
	[BALANCE] = led.[ENDBAL]
from
	@SL tmp
		inner join
	dbo.[LEDGER] led on
		tmp.[LEDGER_ID_PRI] = led.[LEDGER_ID]
where
	tmp.[LEDGER_ID_PRI] > 0

/* borrower details */
select
	case
		when lon.PD = 1 then 'PAST DUE '
		when LOAN_STAT = 'F' then 'FULLY PAID'
		else 'CURRENT'
		end + ' - ' +
		upper(isnull(p.LOAN_DESC, '')) as SUBTITLE,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	mem.DEPT,
	mem.OFF_TEL,
	dbo.func_Format241(lon.KBCI_NO) KBCI_NO,
	mem.FEBTC_SA,
	lon.CHKNO_DATE,
	dbo.func_Format241(lon.PN_NO) PN_NO,
	lon.PRINCIPAL,
	lon.DATE_DUE,
	lon.TERM,
	lon.AMORT_AMT,
	lon.RATE,
	case lon.MOD_PAY
		when 1 then 'PAYROLL'
		when 2 then 'PDC'
		when 3 then 'DM'
		end MOD_PAY,
	case
		when p.LOAN_TYPE = 'STL' then lon.COLLATERAL
		else
			case
				when com1.KBCI_NO is not null then isnull(com1.FNAME + ' ', '') + isnull(com1.MI + '. ', '') + isnull(com1.LNAME, '')
				else ''
				end + 
			case
				when com1.KBCI_NO is not null and com2.KBCI_NO is not null then ', '
				else ''
				end +
			case
				when com2.KBCI_NO is not null then isnull(com2.FNAME + ' ', '') + isnull(com2.MI + '. ', '') + isnull(com2.LNAME, '')
				else ''
				end
		end REMARKS,
	@dt1 DT1,
	@dt2 DT2
from
	dbo.LOANS lon with(nolock)
		left join
	dbo.LOAN_TYPE p with(nolock) on 
		p.LOAN_TYPE = lon.LOAN_TYPE
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO
		left join
	dbo.COMAKER com with(nolock) on
		com.PN_NO = lon.PN_NO
		left join
	dbo.MEMBERS com1 with(nolock) on
		com1.KBCI_NO = com.COMAKER1
		left join
	dbo.MEMBERS com2 with(nolock) on
		com2.KBCI_NO = com.COMAKER2
where
	lon.PN_NO = @pn_no

/* loan history */
select
	[DATES],
	[REF],
	[RMK],
	[LEDGER_ID],
	[AMOUNT_LOANED],
	[PRINCIPAL_PAID],
	[BALANCE],
	[INTEREST_PAID],
	[PENALTIES],
	[LRI],
	[OTHERS]
from
	@SL
order by
	[DATES],
	[LEDGER_ID]

GO