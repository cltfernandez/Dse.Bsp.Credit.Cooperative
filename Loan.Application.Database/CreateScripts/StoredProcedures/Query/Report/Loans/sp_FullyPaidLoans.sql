USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_FullyPaidLoans]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_FullyPaidLoans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_FullyPaidLoans]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_FullyPaidLoans]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Loans_FullyPaidLoans]
@dt1 date,
@dt2 date,
@all bit 
AS

declare @loans table
(
	LOANS_ID bigint
)

if @all = 1
		insert into @loans
		(
			LOANS_ID
		)
		select
			LOANS_ID
		from
			dbo.LOANS lon with(nolock)
				inner join
			dbo.MEMBERS mem with(nolock) on
				lon.KBCI_NO = mem.KBCI_NO
		where
			lon.CHG_DATE between @dt1 and @dt2 and
			lon.LOAN_STAT = 'F'
		order by
			lon.LOAN_TYPE, lon.KBCI_NO
else
		insert into @loans
		(
			LOANS_ID
		)
		select
			LOANS_ID
		from
			dbo.LOANS lon with(nolock)
				inner join
			dbo.MEMBERS mem with(nolock) on
				lon.KBCI_NO = mem.KBCI_NO
		where
			lon.CHG_DATE between @dt1 and @dt2 and
			lon.LOAN_STAT = 'F' and			
			lon.LOAN_TYPE != 'STL' and
			not exists (select 'X' from dbo.LOANS with(nolock) where KBCI_NO = lon.KBCI_NO and LOAN_TYPE = lon.LOAN_TYPE and LOAN_STAT = 'R') and
			substring(mem.KBCI_NO, 1, 2) <> '90'
		order by
			lon.LOAN_TYPE, lon.KBCI_NO

select
	dbo.func_Format241(lon.PN_NO) PN_NO,
	dbo.func_Format241(lon.KBCI_NO) KBCI_NO,
	lon.LOAN_TYPE,
	lon.PRINCIPAL,
	isnull((select top 1 CR-DR from dbo.LEDGER with(nolock) where PN_NO = lon.PN_NO and ACCT_TYPE = 'PAY' and ACCT_CODE = 'PRI' order by [DATE], LEDGER_ID desc), 0) BALANCE_PAID,
	case lon.MOD_PAY
		when 1 then 'Payroll'
		when 2 then 'PDC'
		when 3 then 'DM'
		end MOD_PAY,
	lon.TERM,
	lon.FREQ,
	lon.RATE,
	lon.PAY_START,
	lon.DATE_DUE,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	mem.CB_EMPNO,
	@dt1 DT1,
	@dt2 DT2
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO
where
	lon.LOANS_ID in (select LOANS_ID from @loans)
order by
	lon.LOAN_TYPE,
	mem.LNAME,
	mem.FNAME




GO