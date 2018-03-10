USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_InterestSummary]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_InterestSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_InterestSummary]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_InterestSummary]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Admin_InterestSummary]
AS

declare @SYSDATE date

select
	top 1
	@SYSDATE = SYSDATE
from
	dbo.CTRL with(nolock)

select
	left(MOYEAR,2) + '-' + right(MOYEAR,4) MOYEAR,
	SUM(isnull(CR, 0) - isnull(DR, 0)) INTE,
	DATENAME(M, @SYSDATE) + ' ' + DATENAME(D, @SYSDATE) + ', ' + DATENAME(YYYY, @SYSDATE) SYSDATE
from
	dbo.XLEDGER with(nolock)
group by
	MOYEAR
order by
	right(MOYEAR,4) + '-' + left(MOYEAR,2)




GO