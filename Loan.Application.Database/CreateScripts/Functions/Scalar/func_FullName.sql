USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_FullName]    Script Date: 07/09/2009 12:07:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_FullName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_FullName]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_FullName]    Script Date: 07/09/2009 12:07:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE    FUNCTION [dbo].[func_FullName]
(
@lName varchar(20),
@fName varchar(20),
@mInitial varchar(1)
)
RETURNS varchar(50)
AS  
BEGIN 

RETURN(isnull(@lName + ', ', '') + isnull(@fName + ' ', '') + isnull(@mInitial + '.', ''))

END





GO