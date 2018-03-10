USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_TransactionSchedule]    Script Date: 07/08/2009 01:10:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_TransactionSchedule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_TransactionSchedule]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_TransactionSchedule]    Script Date: 07/08/2009 01:10:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROCEDURE [dbo].[Report_Loans_TransactionSchedule]
AS

declare @sysdate datetime

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

select
	dbo.func_Format241(mem.KBCI_NO) KBCI_NO,
	dbo.func_Format241(lon.PN_NO) PN_NO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	lon.LOAN_TYPE,
	isnull(DR, 0) DR,
	isnull(CR, 0) CR,
	RMK,
	ACCT_CODE,
	ACCT_TYPE,
	case
		when ACCT_TYPE = 'AMT' then isnull(DR, 0)
		else 0
		end DAMT,
	case
		when ACCT_TYPE = 'SC' then isnull(DR, 0)
		else 0
		end DSC,
	case
		when ACCT_TYPE = 'LRI' then isnull(DR, 0)
		else 0
		end DLRI,
	case			
		when ACCT_TYPE = 'AMT' then 0
		when ACCT_TYPE = 'SC' then 0
		when ACCT_TYPE = 'LRI' then 0
		when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then 0
		when ACCT_TYPE = 'INI' and ACCT_CODE = 'PRI' then 0
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'PRI' then 0
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'INT' then 0
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'OTH' then 0
		when ACCT_TYPE = 'PAY' and ACCT_CODE = 'PRI' then 0
		when ACCT_TYPE = 'PAY' and ACCT_CODE = 'INT' then 0
		when ACCT_TYPE = 'SD' then isnull(DR, 0)
		when ACCT_TYPE = 'FD' then isnull(DR, 0)
		else isnull(DR, 0)
		end DOTH,
	case
		when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then isnull(DR, 0)
		else 0
		end DINT,
	case
		when ACCT_TYPE = 'INI' and ACCT_CODE = 'PRI' then isnull(DR, 0)
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'PRI' then isnull(DR, 0)
		else 0
		end DTERP,
	case
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'INT' then isnull(DR, 0)
		else 0
		end DTERI,
	case
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'OTH' then isnull(DR, 0)
		else 0
		end DTERO,
	case
		when ACCT_TYPE = 'PAY' and ACCT_CODE = 'PRI' then isnull(DR, 0)
		else 0
		end DPAYP,
	case
		when ACCT_TYPE = 'PAY' and ACCT_CODE = 'INT' then isnull(DR, 0)
		else 0
		end DPAYI,
	case
		when ACCT_TYPE = 'AMT' then isnull(CR, 0)
		else 0
		end CAMT,
	case
		when ACCT_TYPE = 'SC' then isnull(CR, 0)
		else 0
		end CSC,
	case
		when ACCT_TYPE = 'LRI' then isnull(CR, 0)
		else 0
		end CLRI,
	case			
		when ACCT_TYPE = 'AMT' then 0
		when ACCT_TYPE = 'SC' then 0
		when ACCT_TYPE = 'LRI' then 0
		when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then 0
		when ACCT_TYPE = 'INI' and ACCT_CODE = 'PRI' then 0
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'PRI' then 0
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'INT' then 0
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'OTH' then 0
		when ACCT_TYPE = 'PAY' and ACCT_CODE = 'PRI' then 0
		when ACCT_TYPE = 'PAY' and ACCT_CODE = 'INT' then 0
		when ACCT_TYPE = 'SD' then isnull(CR, 0)
		when ACCT_TYPE = 'FD' then isnull(CR, 0)
		else isnull(CR, 0)
		end COTH,
	case
		when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then isnull(CR, 0)
		else 0
		end CINTS,
	case
		when ACCT_TYPE = 'INI' and ACCT_CODE = 'PRI' then isnull(CR, 0)
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'PRI' then isnull(CR, 0)
		else 0
		end CTERP,
	case
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'INT' then isnull(CR, 0)
		else 0
		end CTERI,
	case
		when ACCT_TYPE = 'TER' and ACCT_CODE = 'OTH' then isnull(CR, 0)
		else 0
		end CTERO,
	case
		when ACCT_TYPE = 'PAY' and ACCT_CODE = 'PRI' then isnull(CR, 0)
		else 0
		end CPAYP,
	case
		when ACCT_TYPE = 'PAY' and ACCT_CODE = 'INT' then isnull(CR, 0)
		else 0
		end CPAYI,
	@sysdate SYSDATE
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO
		inner join
	dbo.LEDGER led with(nolock) on
		lon.PN_NO = led.PN_NO
where
	led.ADD_DATE = @sysdate and
	isnull(led.CR, 0) + isnull(led.DR, 0) > 0 and
	not led.RMK like '%PAYROLL%'
order by
	lon.LOAN_TYPE,
	FULL_NAME




GO