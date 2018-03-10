USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_TagReport]    Script Date: 07/15/2009 11:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Admin_TagReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Admin_TagReport]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_TagReport]    Script Date: 07/15/2009 11:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Do_Admin_TagReport]
@REP int
AS


if @REP = 1
	update dbo.CTRL set REP1 = 1
else if @REP = 2
	update dbo.CTRL set REP2 = 1
else if @REP = 3
	update dbo.CTRL set REP3 = 1
else if @REP = 4
	update dbo.CTRL set REP4 = 1
else if @REP = 5
	update dbo.CTRL set REP5 = 1
else if @REP = 7
	update dbo.CTRL set TD_REP = 1


GO