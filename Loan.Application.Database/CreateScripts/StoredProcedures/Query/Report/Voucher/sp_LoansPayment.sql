USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Voucher_LoansPayment]    Script Date: 07/03/2009 14:33:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Voucher_LoansPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Voucher_LoansPayment]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Voucher_LoansPayment]    Script Date: 07/03/2009 14:33:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*****************************************************************************
MODIFIED:
JS 07/14/2012		CHANGED DATE TO DATETIME AND USED GETDATE() FOR TIME
JS 08/11/2012		IF DM, SHOULD BE JOURNAL VOUCHER. ELSE CASH DISBURSEMENT ORDER
JS 04/06/2013		ADDED SORTING FIELD
*****************************************************************************/

CREATE PROCEDURE [dbo].[Report_Voucher_LoansPayment]
@pn_no varchar(7),
@xor_no varchar(10),
@my_user varchar(50),
@xp integer
AS

declare @sysdate datetime

select
	top 1 @sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

declare @payment table (
	ADD_DATE date,
	DEBIT numeric(16,4),
	REMARKS VARCHAR(50),
	CREDIT NUMERIC(16,4),
	SORT TINYINT
)

insert into @payment
(
	ADD_DATE,
	DEBIT,
	REMARKS,
	CREDIT,
	SORT
)
select
	B.ADD_DATE,		
	case B.DOX_TYPE
		when 'DM' then isnull(B.DR, 0)
		else 0
		end DEBIT,
	case
		when B.ACCT_CODE = 'LRI' then 'LRI DUE'
		when B.ACCT_CODE = 'OTH' then 'PENALTY'
		when B.ACCT_CODE = 'INT' then 'INTEREST ON LOAN'
		when B.ACCT_CODE = 'PRI' and A.PD = 0 then upper(C.LOAN_DESC) + ' (PN: ' + A.PN_NO + ')'
		when B.ACCT_CODE = 'PRI' and A.PD = 1 then 'PAST DUE - ' + A.LOAN_TYPE + ' (PN: ' + A.PN_NO + ')'
		end XRMK,
	case B.DOX_TYPE
		when 'DM' then 0
		else isnull(B.CR, 0)
		end CREDIT,
	case B.DOX_TYPE
		when 'DM' then 70
		else 20
		end +
	case
		when B.ACCT_CODE = 'LRI' then 4
		when B.ACCT_CODE = 'OTH' then 1
		when B.ACCT_CODE = 'INT' then 2
		when B.ACCT_CODE = 'PRI' then 3
		else 5
		end
from
	dbo.LOANS A with(nolock)
		inner join
	dbo.LEDGER B with(nolock) on
		A.PN_NO = B.PN_NO
		inner join
	dbo.LOAN_TYPE C with(nolock) on
		C.LOAN_TYPE = A.LOAN_TYPE
where
	A.PN_NO = @pn_no and
	B.REF = @xor_no

union all

select
	ADDATE,
	isnull(PAYAMT, 0) PAYAMT,
	case
		when
			left(PAYREM, 2) = 'SA'
		then
			replace(PAYREM, 'SA: ', 'SAVING(') + ')'
		when
			left(PAYREM, 3) = 'PDC' and charindex(PAYREM, 'CASH ON HAND') > 0
		then
			replace(PAYREM, 'PDC: ', 'CASH ON HAND(') + ')'
		when
			rtrim(ltrim(isnull(PAYREM, ''))) = '' then ''
		else PAYREM
		end,
	0,
	70
from
	dbo.PAYHIST with(nolock)
where
	PAYOR = @xor_no

select
	top 1
	dbo.func_FullName(B.LNAME, B.FNAME, B.MI) MEMBERNAME,
	@xor_no XOR_NO,
	@sysdate SYSDATE,
	convert(varchar, getdate(), 108) [TIME],			-- JS 07/14/2012
	@my_user MY_USER,
	C.PAYREM,
	C.PAYDATE,
	isnull(P.LOAN_DESC, '') LOAN_TYPE,
	A.PN_NO,
	case @xp
		when 2 then 'JOURNAL VOUCHER'
		else 'CASH DISBURSEMENT ORDER'
		end TITLE
from
	dbo.LOANS A with(nolock)
		left join
	dbo.LOAN_TYPE P with(nolock) on
		P.LOAN_TYPE = A.LOAN_TYPE
		inner join
	dbo.MEMBERS B with(nolock) on
		A.KBCI_NO = B.KBCI_NO
		left join
	dbo.PAYHIST C with(nolock) on
		C.PN_NO = A.PN_NO AND C.PAYOR = @XOR_NO
where
	A.PN_NO = @pn_no

select
	DEBIT,
	REMARKS,
	CREDIT	
from
 	@payment
order by
	SORT



GO