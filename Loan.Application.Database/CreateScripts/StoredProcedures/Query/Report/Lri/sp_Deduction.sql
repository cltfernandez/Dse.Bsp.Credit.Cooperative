USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Lri_Deduction]    Script Date: 07/11/2009 20:52:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Lri_Deduction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Lri_Deduction]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Lri_Deduction]    Script Date: 07/11/2009 20:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Lri_Deduction]
AS

select
	dbo.func_Format241(KBCI_NO) KBCI_NO,
	LNAME,
	FNAME,
	dbo.func_Format451(FEBTC_SA) FEBTC_SA,
	DIVIDEND,
	REFUND,
	TOTAL,
	DEDUCTIONS,
	GTOTAL,
	FOR_LRI
from
	dbo.DIVREF2 with(nolock)
where
	FOR_LRI > 0	
order by
	LNAME,
	FNAME;
	


GO