USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Lri_Due]    Script Date: 07/11/2009 20:52:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Lri_Due]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Lri_Due]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Lri_Due]    Script Date: 07/11/2009 20:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Lri_Due]
@dt1 datetime,
@dt2 datetime
AS



select
	@dt1 DATE_FROM,
	@dt2 DATE_TO,
	dbo.func_Format241(lon.PN_NO) PN_NO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	lon.LOAN_TYPE,
	lon.DATE_GRANT,
	lon.DATE_DUE,
	lri.LRI_DUE,
	round(lri.LRI_DUE_P, 2) as LRI_DUE_P,
	dbo.func_Format451(mem.FEBTC_SA) SAVINGS
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		mem.KBCI_NO = lon.KBCI_NO
		inner join
	dbo.LRIDUE lri with(nolock) on
		lri.PN_NO = lon.PN_NO
where
	lon.LOAN_STAT not in ('F', 'T') and
	round(lri.LRI_DUE_P, 2) > 0 and
	lri.LRI_DUE between @dt1 and @dt2
order by
	lon.LOAN_TYPE,
	FULL_NAME
	


GO