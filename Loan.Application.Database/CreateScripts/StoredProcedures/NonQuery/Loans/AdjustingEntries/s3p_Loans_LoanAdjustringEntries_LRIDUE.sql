USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanAdjustingEntries_LRIDUE]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoanAdjustingEntries_LRIDUE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoanAdjustingEntries_LRIDUE]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanAdjustingEntries_LRIDUE]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Loans_LoanAdjustingEntries_LRIDUE]
@PN_NO VARCHAR(7),
@XLAMT NUMERIC(10, 2),
@XLDC AS CHAR(1)
as

IF EXISTS (SELECT 'X' FROM dbo.LEDGER WHERE PN_NO = @PN_NO)
BEGIN
	IF @XLDC = 'C'
		UPDATE	dbo.LRIDUE
		SET		LRI_DUE_C = LRI_DUE_C - @XLAMT,
				LRI_DUE_P = LRI_DUE_P - @XLAMT
		WHERE	PN_NO = @PN_NO
	ELSE IF @XLDC = 'D'
		UPDATE	dbo.LRIDUE
		SET		LRI_DUE_C = LRI_DUE_C + @XLAMT,
				LRI_DUE_P = LRI_DUE_P + @XLAMT
		WHERE	PN_NO = @PN_NO
END




GO


