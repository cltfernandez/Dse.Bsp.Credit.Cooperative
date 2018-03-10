USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_Open]    Script Date: 07/15/2009 11:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Admin_Open]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Admin_Open]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_Open]    Script Date: 07/15/2009 11:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Do_Admin_Open]
AS


update	dbo.CTRL
set		[CLOSE] = 0




GO