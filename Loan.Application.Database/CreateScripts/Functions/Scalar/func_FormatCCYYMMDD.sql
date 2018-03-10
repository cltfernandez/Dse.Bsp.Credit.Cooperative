USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_FormatCCYYMMDD]    Script Date: 04/17/2009 17:25:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_FormatCCYYMMDD]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_FormatCCYYMMDD]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_FormatCCYYMMDD]    Script Date: 04/17/2009 17:25:30 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




CREATE    FUNCTION [dbo].[func_FormatCCYYMMDD]
(
@INPUT date
)
RETURNS VARCHAR(8)
AS  
BEGIN 

DECLARE @OUTPUT VARCHAR(8)

set @OUTPUT = 
	right('0000' + datename(YYYY, @INPUT), 4) + 
	right('00' + convert(varchar(2), datepart(MM, @INPUT)), 2) + 
	right('00' + datename(DD, @INPUT), 2)

	
RETURN(@OUTPUT)

END




GO

