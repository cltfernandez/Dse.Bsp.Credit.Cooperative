USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_GoDue]    Script Date: 04/17/2009 17:25:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_GoDue]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_GoDue]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_GoDue]    Script Date: 04/17/2009 17:25:30 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




CREATE    FUNCTION [dbo].[func_GoDue]
(
@PAY_START date,
@TERM NUMERIC(3,0),
@FREQ VARCHAR(1)
)
RETURNS date
AS  
BEGIN 

DECLARE @MDDUE AS date
DECLARE @NEXT  AS NUMERIC(2, 0)

SET @NEXT = CASE @FREQ
	WHEN 'M' THEN 1
	WHEN 'Q' THEN 3
	WHEN 'S' THEN 6
	WHEN 'A' THEN 12
	WHEN 'D' THEN 0
	ELSE 0
	END

IF @FREQ = 'D'
	SET @MDDUE = @PAY_START
ELSE
	SET	@MDDUE = DATEADD(M, @TERM - @NEXT, @PAY_START)

IF @TERM = 1 AND @FREQ = 'M'
	IF DATENAME(DW, @MDDUE) = 'Sunday'
		SET @MDDUE = DATEADD(D, 1, @MDDUE)
	ELSE IF DATENAME(DW, @MDDUE) = 'Saturday'
		SET @MDDUE = DATEADD(D, 2, @MDDUE)
				
RETURN(@MDDUE)

END




GO

