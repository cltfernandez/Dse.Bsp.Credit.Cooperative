USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanStaffPayment_Edit]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoanStaffPayment_Edit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoanStaffPayment_Edit]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanStaffPayment_Edit]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Loans_LoanStaffPayment_Edit]
@PN_NO VARCHAR(7),
@ARREAR_P NUMERIC(11, 2),
@ARREAR_I NUMERIC(11, 2),
@ARREAR_OTH NUMERIC(11, 2),
@ARREAR_AS date,
@P_BAL NUMERIC(11, 2),
@I_BAL NUMERIC(11, 2),
@O_BAL NUMERIC(11, 2)
as

update	dbo.SLOANS
set		ARREAR_P = @ARREAR_P,
		ARREAR_I = @ARREAR_I,
		ARREAR_OTH = @ARREAR_OTH,
		ARREAR_AS = @ARREAR_AS,
		P_BAL = @P_BAL,
		I_BAL = @I_BAL,
		O_BAL = @O_BAL,
		amount = @P_BAL + @I_BAL + @O_BAL + @ARREAR_P + @ARREAR_I + @ARREAR_OTH
where	PN_NO = @PN_NO




GO


