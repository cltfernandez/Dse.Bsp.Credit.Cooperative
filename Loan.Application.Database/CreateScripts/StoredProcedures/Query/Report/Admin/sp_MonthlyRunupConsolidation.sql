USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_MonthlyRunupConsolidation]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_MonthlyRunupConsolidation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_MonthlyRunupConsolidation]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_MonthlyRunupConsolidation]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Admin_MonthlyRunupConsolidation]
@dateTo datetime
AS

--SAVINGS ACCOUNT NUMBER	KBCI NUMBER	EMPNO	NAME	WAGETYPE	AMOUNT

select	upper(datename(month, @dateTo) + ', ' + datename(year, @dateTo)) as MONTH_NAME,
		mem.FEBTC_SA as ACCTNO,
		mem.KBCI_NO,
		mem.CB_EMPNO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) NAME,
		'7662' CODE5,
		SUM(case
			when ISNULL(lon.AMORT_AMT,0) + ISNULL(lon.ARREAR_P,0) + ISNULL(lon.ARREAR_I,0) + ISNULL(lon.ARREAR_OTH,0) > bal.BALANCE then bal.BALANCE
			else ISNULL(lon.AMORT_AMT,0) + ISNULL(lon.ARREAR_P,0) + ISNULL(lon.ARREAR_I,0) + ISNULL(lon.ARREAR_OTH,0)
			end) AMORT_AMT
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
group
by		mem.FEBTC_SA,
		mem.KBCI_NO,
		mem.CB_EMPNO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
order
by		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)

	
go


