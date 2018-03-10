USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_NoDeductionRegister]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Payroll_NoDeductionRegister]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Payroll_NoDeductionRegister]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_NoDeductionRegister]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Payroll_NoDeductionRegister]
@payrollDate date = NULL
AS

declare @formattedDate varchar(10) 
declare @sysdate as date

if @payrollDate = '01/01/1900' set @payrollDate = NULL

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

select
	datename(month, isnull(@payrollDate, @sysdate)) + ', ' + datename(YYYY, isnull(@payrollDate, @sysdate)) PAYDATE,
	LOAN_TYPE,
	KBCI_NO,
	EMPNO,
	[NAME] EMPNAME,
	DEDUCTION,
	PN_NO,
	AMORT_PRI,
	AMORT_INT,
	ARR_PRI,
	ARR_INT,
	ARREARS,
	LOAN_STAT
from
	MO_DEDNP md with(nolock)
where
	[DATE] = @payrollDate or
	@payrollDate is null
order by
	LOAN_TYPE,
	[NAME]




GO