USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_Format241]    Script Date: 04/17/2009 17:25:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Format241]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_Format241]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_Format241]    Script Date: 04/17/2009 17:25:30 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*****************************************************************************
MODIFIED:
JS 06/09/2012		HANDLED NULL INPUTSS
*****************************************************************************/

CREATE    FUNCTION [dbo].[func_Format241]
(
@INPUT VARCHAR(7)
)
RETURNS VARCHAR(9)
AS  
BEGIN 

DECLARE @OUTPUT VARCHAR(9)

IF @INPUT IS NULL
	SET @OUTPUT = NULL			-- JS 06/06/2012
ELSE
	SET @OUTPUT = SUBSTRING(@INPUT, 1, 2) + '-' + SUBSTRING(@INPUT, 3, 4) + '-' + SUBSTRING(@INPUT, 7, 1)
	
RETURN(@OUTPUT)

END




GO

