USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_AdminDate]    Script Date: 07/15/2009 11:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Admin_AdminDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Admin_AdminDate]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_AdminDate]    Script Date: 07/15/2009 11:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Do_Admin_AdminDate]
@admdate date
AS


update
	dbo.CTRL
set
	ADMDATE = @admdate


GO