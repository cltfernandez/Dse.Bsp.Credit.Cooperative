USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_CashDisbursementOrder]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_CashDisbursementOrder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_CashDisbursementOrder]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_CashDisbursementOrder]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*****************************************************************************
MODIFIED:
JS 07/14/2012		CHANGED DATE TO DATETIME AND USED GETDATE() FOR TIME
JS 04/06/2013		ADDED SORTING FIELD
*****************************************************************************/

CREATE PROCEDURE [dbo].[Report_Loans_CashDisbursementOrder]
@pn_no varchar(7),
@my_user varchar(50)
AS

declare @sysdate date

declare @payhist table 
(
	PN_NO varchar(7),
	PAYOR varchar(10)
)

declare @payment table
(
	PN_NO varchar(7),
	PAYOR varchar(10),
	ADD_DATE date,
	DEBIT numeric(16,4),
	REMARKS varchar(50),
	CREDIT numeric(16,4),
	SORT tinyint
)

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

insert into @payhist
(
	PN_NO,
	PAYOR
)
select	distinct
	PN_NO,
	PAYOR
from
	dbo.PAYHIST with(nolock)
where
	PN_NO = @PN_NO and
	(
		left(PAYREM, 2) = 'SA'  or
		left(PAYREM, 3) = 'PDC'
	)

insert into @payment
(
	PN_NO,
	PAYOR,
	ADD_DATE,
	DEBIT,
	REMARKS,
	CREDIT,
	SORT
)
select
	lon.PN_NO,
	led.REF,
	led.ADD_DATE,		
	case led.DOX_TYPE
		when 'DM' then ISNULL(led.DR, 0)
		else 0
		end DEBIT,
	case
		when led.ACCT_CODE = 'LRI' then 'L.R.I. DUE'
		when led.ACCT_CODE = 'OTH' then 'PENALTY'
		when led.ACCT_CODE = 'INT' then 'INTEREST ON LOAN'
		when led.ACCT_CODE = 'PRI' AND lon.PD = 0 then 'LOAN PN: ' + lon.PN_NO
		when led.ACCT_CODE = 'PRI' AND lon.PD = 1 then 'PAST DUE - ' + lon.LOAN_TYPE + ' (PN: ' + lon.PN_NO + ')'
		end XRMK,
	case led.DOX_TYPE
		when 'DM' then 0
		else ISNULL(led.CR, 0)
		end CREDIT,
	case led.DOX_TYPE
		when 'DM' then 20
		else 10
		end +
	case
		when led.ACCT_CODE = 'LRI' then 4
		when led.ACCT_CODE = 'OTH' then 1
		when led.ACCT_CODE = 'INT' then 2
		when led.ACCT_CODE = 'PRI' then 3
		else 5
		end
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.LEDGER led with(nolock) on
		lon.PN_NO = led.PN_NO
		inner join
	@payhist pay on
		pay.PN_NO = led.PN_NO and
		pay.PAYOR = led.REF
union
select
	hist.PN_NO,
	hist.PAYOR,
	hist.ADDATE,
	isnull(hist.PAYAMT, 0) PAYAMT,
	case
		when left(hist.PAYREM, 2) = 'SA'  then replace(hist.PAYREM, 'SA :', 'SAVINGS (') + ')'
		when left(hist.PAYREM, 3) = 'PDC' then replace(hist.PAYREM, 'PDC :', 'CASH ON HAND (') + ')'
		when hist.PAYREM is null or rtrim(ltrim(hist.PAYREM)) = ''  then ''
		else hist.PAYREM
		end,
	0,
	20
from
	dbo.PAYHIST hist with(nolock)
		inner join
	@payhist pay on
		pay.PN_NO = hist.PN_NO and
		pay.PAYOR = hist.PAYOR

select
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	pay.PAYOR XOR_NO,
	@sysdate SYSDATE,
	@my_user MY_USER,
	hist.PAYREM,
	hist.PAYDATE,
	isnull(typ.LOAN_DESC, '') AS LOAN_TYPE,
	lon.PN_NO,
	pay.DEBIT,
	pay.REMARKS,
	pay.CREDIT
from
	dbo.LOANS lon with(nolock)
		left join
	dbo.LOAN_TYPE typ with(nolock) on
		typ.LOAN_TYPE = lon.LOAN_TYPE
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO
		inner join
	@payment pay on
		pay.PN_NO = lon.PN_NO
		inner join
	dbo.PAYHIST hist with(nolock) on
		hist.PN_NO = lon.PN_NO and hist.PAYOR = pay.PAYOR
order by
	pay.SORT




GO