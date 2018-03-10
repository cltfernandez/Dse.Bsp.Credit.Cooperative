USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_Format451]    Script Date: 04/17/2009 17:25:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Format451]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_Format451]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_Format451]    Script Date: 04/17/2009 17:25:30 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




CREATE    FUNCTION [dbo].[func_Format451]
(
@INPUT VARCHAR(10)
)
RETURNS VARCHAR(12)
AS  
BEGIN 

DECLARE @OUTPUT VARCHAR(12)

SET @OUTPUT = SUBSTRING(@INPUT, 1, 4) + '-' + SUBSTRING(@INPUT, 5, 5) + '-' + SUBSTRING(@INPUT, 10, 1)
	
RETURN(@OUTPUT)

END




GO

