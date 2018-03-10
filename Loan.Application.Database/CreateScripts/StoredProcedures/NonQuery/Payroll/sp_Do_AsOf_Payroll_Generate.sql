USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Payroll_Generate]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_AsOf_Payroll_Generate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_AsOf_Payroll_Generate]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Payroll_Generate]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*****************************************************************************
MODIFIED:
JS 01/26/2013	REPLACED HARDCODED CODE5 WITH TABLE SOURCE
JS 07/27/2013	PREVENT STOP OF RENEWED LOANS WITHIN THE CUTOFF
*****************************************************************************/

CREATE PROCEDURE [dbo].[Do_AsOf_Payroll_Generate]
@dateTo date
as

declare @myproc_date as date
declare @sysdate as date

select top 1
	@myproc_date = [PROC],
	@sysdate = SYSDATE
from
	dbo.CTRL

truncate table dbo.ADVICE

insert	dbo.ADVICE (
		EMPNO,
		WAGE_TYPE,
		BEGDA,
		ENDDA,
		AMOUNT
		)
select	mem.CB_EMPNO,
		lt.CODE5,
		dbo.func_FormatCCYYMMDD(@sysdate),
		dbo.func_FormatCCYYMMDD(@sysdate),
		case
			when ISNULL(lon.AMORT_AMT,0) + ISNULL(lon.ARREAR_P,0) + ISNULL(lon.ARREAR_I,0) + ISNULL(lon.ARREAR_OTH,0) > bal.BALANCE then bal.BALANCE
			else ISNULL(lon.AMORT_AMT,0) + ISNULL(lon.ARREAR_P,0) + ISNULL(lon.ARREAR_I,0) + ISNULL(lon.ARREAR_OTH,0)
			end
from	dbo.LOANS as lon
			inner join
		dbo.LOAN_TYPE as lt on
			lt.LOAN_TYPE = lon.LOAN_TYPE
			inner join
		dbo.MEMBERS as mem on
			lon.KBCI_NO = mem.KBCI_NO
			outer apply
		dbo.func_Balance(lon.PN_NO, '1900-01-01', @dateTo)
			as bal
where	lon.MOD_PAY like '%1%' and
		mem.MEM_STAT != 'S' and
		lon.LOAN_STAT = 'R' and
		lon.CHKNO_DATE <= @dateTo and
		(
			lon.ARREAR_I = 0 or
			(
				lon.ARREAR_I > 0 and
				lon.ARREAR_P > 0
			)
		) and
		lon.PD = 0 -- no longer included


GO


