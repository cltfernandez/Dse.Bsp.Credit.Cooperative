USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_MiscellaneousLiability]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Adhoc_MiscellaneousLiability]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Adhoc_MiscellaneousLiability]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_MiscellaneousLiability]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






create procedure [dbo].[Report_Adhoc_MiscellaneousLiability]
@dateFrom datetime,
@dateTo datetime
as

select
	dbo.func_Format241(lon.KBCI_NO) as KBCI_NO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) as NAME,
	dbo.func_Format241(lon.PN_NO) as PN_NO,
	lon.LOAN_TYPE as [TYPE],
	lon.CHKNO_DATE,
	led.CR
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		mem.KBCI_NO = lon.KBCI_NO
		inner join
	dbo.LEDGER led with(nolock) on
		led.PN_NO = lon.PN_NO and
		led.ACCT_TYPE = 'MSC' and
		led.ACCT_CODE = 'OTH'
where
	lon.CHKNO_DATE between @dateFrom and @dateTo
order by
	NAME, [TYPE]




GO