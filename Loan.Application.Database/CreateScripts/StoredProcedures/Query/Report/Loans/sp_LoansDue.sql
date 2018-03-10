USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_LoansDue]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_LoansDue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_LoansDue]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_LoansDue]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Loans_LoansDue]
@fromDate date,
@toDate date,
@sysdate date,
@my_user varchar(8)
AS

select
	@fromDate fromDate,
	@toDate toDate,		
	@sysdate sysDate,
	@my_user myUser,
	lon.PN_NO,
	lon.KBCI_NO,
	lon.LOAN_TYPE,
	lon.PRINCIPAL,		
	lon.AMORT_AMT,
	case lon.MOD_PAY
		when '1' then 'Payroll'
		when '2' then 'PDC'
		when '3' then 'DM'
		end as MOD_PAY,
	lon.TERM,
	lon.FREQ,
	lon.RATE,
	lon.PAY_START,
	lon.DATE_DUE,
	lon.CHKNO_DATE,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) as FULL_NAME
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO
where
	lon.LOAN_STAT = 'R' and
	lon.DATE_DUE between @fromDate and @toDate
order by
	lon.LOAN_TYPE, FULL_NAME




GO