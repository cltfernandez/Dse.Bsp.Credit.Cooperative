USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_GetLockingUser]    Script Date: 04/17/2009 17:25:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_GetLockingUser]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_GetLockingUser]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_GetLockingUser]    Script Date: 04/17/2009 17:25:30 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




CREATE    FUNCTION [dbo].[func_GetLockingUser]
(
@ENTRY VARCHAR(20),
@MY_USER VARCHAR(50)
)
RETURNS VARCHAR(20)
AS  
BEGIN 

DECLARE @vLocker VARCHAR(20)

IF EXISTS (SELECT 'X' FROM LOCKS WHERE [ENTRY] = @ENTRY AND MY_USER <> @MY_USER)
	SELECT	@vLocker = MY_USER
	FROM	LOCKS
	WHERE	[ENTRY] = @ENTRY
ELSE
	SET @vLocker = 'N/A'
	
RETURN(@vLocker)

END




GO

