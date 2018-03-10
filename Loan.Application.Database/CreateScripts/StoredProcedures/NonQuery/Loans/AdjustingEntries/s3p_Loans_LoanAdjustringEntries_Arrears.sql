USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanAdjustingEntries_Arrears]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoanAdjustingEntries_Arrears]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoanAdjustingEntries_Arrears]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanAdjustingEntries_Arrears]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Loans_LoanAdjustingEntries_Arrears]
@PN_NO VARCHAR(7),
@ARREAR_P NUMERIC(11, 2),
@ARREAR_I NUMERIC(11, 2),
@ARREAR_OTH NUMERIC(11, 2),
@ARREAR_AS date,
@USER VARCHAR(8),
@CHG_DATE date
as

update	dbo.LOANS
set		ARREAR_P = @ARREAR_P,
		ARREAR_I = @ARREAR_I,
		ARREAR_OTH = @ARREAR_OTH,
		ARREAR_AS = @ARREAR_AS,
		[USER] = @USER,
		CHG_DATE = @CHG_DATE
where	PN_NO = @PN_NO




GO


