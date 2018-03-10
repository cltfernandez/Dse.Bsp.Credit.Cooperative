USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Lri_Collection]    Script Date: 07/11/2009 20:52:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Lri_Collection]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Lri_Collection]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Lri_Collection]    Script Date: 07/11/2009 20:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Lri_Collection]
@dt1 datetime,
@dt2 datetime,
@x_menu int
AS



select
	@dt1 DATE_FROM,
	@dt2 DATE_TO,
	dbo.func_Format241(lon.KBCI_NO) as KBCI_NO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	SUM(ISNULL(led.CR, 0) - ISNULL(led.DR, 0)) as XLRI
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		mem.KBCI_NO = lon.KBCI_NO
		inner join
	dbo.LEDGER led with(nolock) on
		led.PN_NO = lon.PN_NO
where
	led.[DATE] between @dt1 and @dt2 and
	(
		(
			led.ACCT_CODE = 'OTH' and 
			led.ACCT_TYPE = 'LRI' and
			(
				(
					@x_menu in (1, 3) and 
					CHARINDEX('INITIAL', RMK) > 0
				) or
				(
					@x_menu in (2, 3) and 
					(
						CHARINDEX('LOAN ADJUSTMENT', RMK) > 0 or
						CHARINDEX('PAYMENT', RMK) > 0
					)
				)
			)
		) or
		(
			led.ACCT_CODE = 'LRI' and
			led.ACCT_TYPE in ('ADJ', 'PAY', 'TER') and
			@x_menu in (2, 3)
		)
	)
group by
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI),
	lon.KBCI_NO
order by
	FULL_NAME;	


GO