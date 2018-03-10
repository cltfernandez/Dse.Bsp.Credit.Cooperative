USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_PreterminatedLoans]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_PreterminatedLoans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_PreterminatedLoans]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_PreterminatedLoans]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Report_Loans_PreterminatedLoans]
@dt1 date,
@dt2 date
AS

select
	dbo.func_Format241(lon.PN_NO) PN_NO,
	dbo.func_Format241(lon.KBCI_NO) KBCI_NO,
	lon.LOAN_TYPE,
	lon.PRINCIPAL,
	lon.CHKNO_AMT,
	case lon.MOD_PAY
		when 1 then 'Payroll'
		when 2 then 'PDC'
		when 3 then 'DM'
		end MOD_PAY,
	lon.TERM,
	lon.FREQ,
	lon.RATE,
	lon.PAY_START,
	lon.DATE_DUE,
	lon.CHKNO_DATE,
	@dt1 DT1,
	@dt2 DT2
from
	dbo.LOANS lon with(nolock)
		left join
	(
		select
			distinct
			lon.PN_NO
		from
			dbo.LOANS lon with(nolock)
				inner join
			dbo.LEDGER led with(nolock) on
				led.PN_NO = lon.PN_NO and
				led.ACCT_TYPE = 'TER' and
				led.ACCT_CODE = 'PRI'
	) ter
		on ter.PN_NO = lon.PN_NO
where
	lon.CHKNO_DATE between @dt1 and @dt2 and
	(
		lon.LOAN_STAT = 'T' or
		ter.PN_NO is not null
	)
order by
	lon.LOAN_TYPE,
	lon.KBCI_NO



GO