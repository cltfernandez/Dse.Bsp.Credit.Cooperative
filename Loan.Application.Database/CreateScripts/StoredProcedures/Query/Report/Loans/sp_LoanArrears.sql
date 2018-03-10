USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_LoanArrears]    Script Date: 07/11/2009 20:52:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_LoanArrears]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_LoanArrears]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_LoanArrears]    Script Date: 07/11/2009 20:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Report_Loans_LoanArrears]
@dateFrom as date,
@dateTo as date = null
AS

declare @SYSDATE date

select
	@SYSDATE = SYSDATE
from
	dbo.CTRL with(nolock)

if @dateTo is not null
begin
	select
		lon.LOAN_TYPE,
		dbo.func_Format241(mem.KBCI_NO) KBCI_NO,
		mem.CB_EMPNO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
		lon.AMORT_AMT,
		dbo.func_Format241(lon.PN_NO) PN_NO,
		lon.ARREAR_P,
		lon.ARREAR_I,
		lon.ARREAR_OTH,
		lon.ARREAR_P + lon.ARREAR_I + lon.ARREAR_OTH as ARREAR_TOTAL,
		DATEDIFF(M, lon.ARREAR_AS, @SYSDATE) MONTHS,
		case lon.MOD_PAY
			when 1 then 'Payroll'
			when 2 then 'PDC'
			when 3 then 'DM'
			end MOD_PAY,
		sum(led.DR - led.CR) OUTSBAL,
		@SYSDATE SYSDATE
	from
		dbo.LOANS lon with(nolock)
			inner join
		dbo.MEMBERS mem with(nolock) on
			lon.KBCI_NO = mem.KBCI_NO
			inner join
		dbo.LEDGER led with(nolock) on
			lon.PN_NO = led.PN_NO and 
			ACCT_CODE = 'PRI' and
			ACCT_TYPE != 'INI'
	where
		isnull(ARREAR_AS, '1/1/1900') between @dateFrom and @dateTo and
		--dateadd(M, 1, lon.ARREAR_AS) < @SYSDATE and
		lon.PD = 0 and		
		lon.LOAN_STAT = 'R' and		
		--mem.MEM_STAT = 'A' and
		isnull(lon.ARREAR_P, 0) + isnull(lon.ARREAR_I, 0) + isnull(lon.ARREAR_OTH, 0) > 0
	group by
		lon.PN_NO,
		lon.LOAN_TYPE,
		mem.KBCI_NO,
		mem.CB_EMPNO,
		mem.LNAME,
		mem.FNAME,
		mem.MI,
		lon.AMORT_AMT,
		lon.ARREAR_P,
		lon.ARREAR_I,
		lon.ARREAR_OTH,
		lon.ARREAR_AS,
		lon.MOD_PAY	
	order by
		lon.LOAN_TYPE,
		FULL_NAME
end
begin
	select
		lon.LOAN_TYPE,
		dbo.func_Format241(mem.KBCI_NO) KBCI_NO,
		mem.CB_EMPNO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
		lon.AMORT_AMT,
		dbo.func_Format241(lon.PN_NO) PN_NO,
		lar.ARREAR_P,
		lar.ARREAR_I,
		lar.ARREAR_OTH,
		lar.ARREAR_P + lar.ARREAR_I + lar.ARREAR_OTH as ARREAR_TOTAL,
		DATEDIFF(M, lar.ARREAR_AS, lar.SYSDATE) MONTHS,
		case lon.MOD_PAY
			when 1 then 'Payroll'
			when 2 then 'PDC'
			when 3 then 'DM'
			end MOD_PAY,
		sum(led.DR - led.CR) OUTSBAL,
		@SYSDATE SYSDATE
	from
		dbo.LOANS lon with(nolock)
			inner join
		dbo.LOAN_ARREARS lar with(nolock) on
			lon.PN_NO = lar.PN_NO
			inner join
		dbo.MEMBERS mem with(nolock) on
			lon.KBCI_NO = mem.KBCI_NO
			inner join
		dbo.LEDGER led with(nolock) on
			lon.PN_NO = led.PN_NO and 
			ACCT_CODE = 'PRI' and
			ACCT_TYPE != 'INI'
	where
		--isnull(ARREAR_AS, '1/1/1900') between @dateFrom and @dateTo and
		month(lar.SYSDATE) = month(@dateFrom) and
		year(lar.SYSDATE) = year(@dateFrom) and
		--dateadd(M, 1, lon.ARREAR_AS) < @SYSDATE and
		lon.PD = 0 and		
		lon.LOAN_STAT = 'R' and		
		--mem.MEM_STAT = 'A' and
		isnull(lar.ARREAR_P, 0) + isnull(lar.ARREAR_I, 0) + isnull(lar.ARREAR_OTH, 0) > 0
	group by
		lon.PN_NO,
		lon.LOAN_TYPE,
		mem.KBCI_NO,
		mem.CB_EMPNO,
		mem.LNAME,
		mem.FNAME,
		mem.MI,
		lon.AMORT_AMT,
		lar.ARREAR_P,
		lar.ARREAR_I,
		lar.ARREAR_OTH,
		lar.ARREAR_AS,
		lar.SYSDATE,
		lon.MOD_PAY	
	order by
		lon.LOAN_TYPE,
		FULL_NAME
end



GO