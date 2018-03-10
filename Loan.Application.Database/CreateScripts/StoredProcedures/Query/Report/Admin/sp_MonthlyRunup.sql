USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_MonthlyRunup]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_MonthlyRunup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_MonthlyRunup]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_MonthlyRunup]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Admin_MonthlyRunup]
@asOfDate as date,
@pd bit,
@showSubTotal bit
AS

select
	@showSubTotal AS SHOW_SUB_TOTAL,
	@asOfDate AS SYSDATE,
	lon.PD,
	lt.LOAN_DESC LOAN_TYPE,
	lon.KBCI_NO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) as BORROWER,
	mem.B_DATE,
	dbo.func_Age(mem.B_DATE, NULL) AGE,
	lon.PN_NO,
	lon.DATE_GRANT,
	lon.DATE_DUE,
	lon.TERM,
	lon.RATE,
	lon.AMORT_AMT,
	lon.PRINCIPAL,
	(
		lon.PRINCIPAL + 
		sum
		(
			case
				when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') then isnull(led.DR, 0) - isnull(led.CR, 0)
				else 0
				end
		)
	) as BALANCE,
	max
	(
		case
			when led.ACCT_TYPE in ('AMT','PAY','ADJ','TER') then led.[DATE]
			end
	) as LAST_TRAN_DT,
	isnull(lon.ARREAR_P, 0) + isnull(lon.ARREAR_I, 0) + isnull(lon.ARREAR_OTH, 0) ARREARS,
	case
		when isnull(lon.ARREAR_AS, '') = '' then ''
		when datediff(M, lon.ARREAR_AS, @asOfDate) < 1 then ''
		else datediff(M, lon.ARREAR_AS, @asOfDate)
		end as MONTHS
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.LOAN_TYPE lt with(nolock) on
		lt.LOAN_TYPE = lon.LOAN_TYPE
		inner join
	dbo.MEMBERS mem with(nolock) on
		mem.KBCI_NO = lon.KBCI_NO
		inner join
	dbo.LEDGER led with(nolock) on
		led.PN_NO = lon.PN_NO
where
	lon.LOAN_STAT = 'R' and
	lon.PD = @pd and
	led.[DATE] <= @asOfDate
group by
	lon.PD,
	lt.LOAN_DESC,
	lon.KBCI_NO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI),
	mem.B_DATE,
	lon.PN_NO,
	lon.DATE_GRANT,
	lon.DATE_DUE,
	lon.TERM,
	lon.RATE,
	lon.AMORT_AMT,
	lon.PRINCIPAL,
	isnull(lon.ARREAR_P, 0) + isnull(lon.ARREAR_I, 0) + isnull(lon.ARREAR_OTH, 0),
	case
		when isnull(lon.ARREAR_AS, '') = '' then ''
		when datediff(M, lon.ARREAR_AS, @asOfDate) < 1 then ''
		else datediff(M, lon.ARREAR_AS, @asOfDate)
		end
order by
	lt.LOAN_DESC,
	BORROWER



GO