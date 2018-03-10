USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_Age]    Script Date: 07/09/2009 12:07:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Age]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_Age]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_Age]    Script Date: 07/09/2009 12:07:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE    FUNCTION [dbo].[func_Age]
(
@date date,
@referenceDate date = NULL
)
RETURNS INT
AS  
BEGIN 

if @referenceDate is null select @referenceDate = SYSDATE from dbo.CTRL
RETURN DATEDIFF(yyyy, @date, @referenceDate) - 1 + case when DATEPART(DAYOFYEAR, @referenceDate) >= DATEPART(DAYOFYEAR, @date) then 1 else 0 end 

END





GO