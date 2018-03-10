USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_Advice]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Payroll_Advice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Payroll_Advice]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_Advice]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Payroll_Advice]
@offcycle bit = 0
as

declare @SYSDATE datetime

select
	@SYSDATE = SYSDATE
from
	CTRL

if @offcycle = 0
begin
	select
		EMPNO,
		WAGE_TYPE,
		BEGDA,
		ENDDA,
		round(AMOUNT, 2) AMOUNT
	from
		dbo.ADVICE with(nolock)
end
else
begin
	delete
		MO_DEDN_DETL
	where
		AMT = PAY_AMT
	
	update
		MO_DEDN_DETL
	set
		AMT = AMT - PAY_AMT,
		PAY_AMT = 0,
		PAY_DATE = NULL
		
	select
		EMP_NO AS 'EMPNO',
		CODE5 AS 'WAGE_TYPE',
		convert(varchar(10), @SYSDATE, 112) AS 'BEGDA',
		convert(varchar(10), @SYSDATE, 112) AS 'ENDDA',
		round(AMT, 2) AS 'AMOUNT'
	from
		MO_DEDN_DETL
end



GO