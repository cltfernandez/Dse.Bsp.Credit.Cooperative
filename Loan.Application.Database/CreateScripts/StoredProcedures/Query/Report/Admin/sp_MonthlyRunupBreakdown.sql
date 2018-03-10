USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_MonthlyRunupBreakdown]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_MonthlyRunupBreakdown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_MonthlyRunupBreakdown]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_MonthlyRunupBreakdown]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Admin_MonthlyRunupBreakdown]
@dateTo datetime
AS

--Format
--SAVINGS/ACCT	KBCI /ACCT	EMPNO	NAME	LOANS CODE	LOANS TYPE	AMORT	LOAN AMT.	LOAN PROCEED	BALANCE	ARREARS

select
	upper(datename(month, @dateTo) + ', ' + datename(year, @dateTo)) as MONTH_NAME,
	mem.FEBTC_SA as ACCTNO,
	mem.KBCI_NO,
	mem.CB_EMPNO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) NAME,
	lt.CODE5,
	case PD when 0 then '' else 'PD - ' end + lon.LOAN_TYPE as LOAN_TYPE,
	lon.AMORT_AMT,
	lon.PRINCIPAL,
	lon.CHKNO_AMT,
	bal.BALANCE,
	isnull(lon.ARREAR_P,0) + isnull(lon.ARREAR_I,0) + isnull(lon.ARREAR_OTH,0) ARREARS,
	lon.PN_NO
from
	dbo.LOANS lon
		inner join
	dbo.LOAN_TYPE lt on
		lt.LOAN_TYPE = lon.LOAN_TYPE 
		inner join
	dbo.MEMBERS mem on
		lon.KBCI_NO = mem.KBCI_NO
		cross apply
	dbo.func_Balance(lon.PN_NO, '1900-01-01', @dateTo) bal
where
	lon.MOD_PAY like '%1%' and
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
order by
	lon.PD,
	lt.LOAN_TYPE,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
	
go

