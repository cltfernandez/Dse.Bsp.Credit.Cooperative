USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_RestructuredLoans]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_RestructuredLoans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_RestructuredLoans]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_RestructuredLoans]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Report_Loans_RestructuredLoans]
@dt1 date,
@dt2 date
AS

select	
	dbo.func_Format241(PN_NO) PN_NO,
	dbo.func_Format241(KBCI_NO) KBCI_NO,
	LOAN_TYPE,
	PRINCIPAL,
	CHKNO_AMT,
	case MOD_PAY
		when 1 then 'Payroll'
		when 2 then 'PDC'
		when 3 then 'DM'
		end MOD_PAY,
	TERM,
	FREQ,
	RATE,
	PAY_START,
	DATE_DUE,
	CHKNO_DATE,
	@dt1 DT1,
	@dt2 DT2
from
	dbo.LOANS with(nolock)
where
	LOAN_TYPE = 'RSL' and
	CHKNO_DATE between @dt1 and @dt2 and
	LOAN_STAT = 'R'
order by
	CHKNO_DATE



GO