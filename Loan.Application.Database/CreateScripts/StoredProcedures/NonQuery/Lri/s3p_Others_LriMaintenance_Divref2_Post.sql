USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Others_LriMaintenance_Divref2_Post]    Script Date: 05/04/2013 16:19:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Others_LriMaintenance_Divref2_Post]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Others_LriMaintenance_Divref2_Post]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Others_LriMaintenance_Divref2_Post]    Script Date: 05/04/2013 16:19:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[s3p_Others_LriMaintenance_Divref2_Post]
@my_user varchar(8)
AS

DECLARE @KBCI_NO VARCHAR(7)
DECLARE @LNAME VARCHAR(20)
DECLARE @FNAME VARCHAR(20)
DECLARE @MI VARCHAR(1)
DECLARE @FEBTC_SA VARCHAR(10)
DECLARE @DIVIDEND NUMERIC(11, 2)
DECLARE @REFUND NUMERIC(11, 2)
DECLARE @TOTAL NUMERIC(11, 2)
DECLARE @DEDUCTIONS NUMERIC(11, 2)
DECLARE @GTOTAL NUMERIC(11, 2)
DECLARE @FOR_LRI NUMERIC(11, 2)
DECLARE @PN_NO VARCHAR(7)
DECLARE @LRI_DUE_P NUMERIC(14, 2)

DECLARE @xlri NUMERIC(11, 2) = 0
DECLARE @xplri NUMERIC(11, 2) = 0
DECLARE @xclri NUMERIC(11, 2) = 0
DECLARE @xkbci VARCHAR(7) = REPLICATE(' ', 7)
DECLARE @xor_no VARCHAR(10) = REPLICATE(' ', 10)

DECLARE @SYSDATE DATETIME

SELECT
	@SYSDATE = SYSDATE
FROM
	dbo.CTRL

DECLARE @VOUCHER TABLE (
	XOR_NO VARCHAR(9)
)

DECLARE LRI_CURSOR CURSOR FOR
SELECT
	div.[KBCI_NO],
	div.[LNAME],
	div.[FNAME],
	div.[MI],
	div.[FEBTC_SA],
	div.[DIVIDEND],
	div.[REFUND],
	div.[TOTAL],
	div.[DEDUCTIONS],
	div.[GTOTAL],
	div.[FOR_LRI],
	lri.[PN_NO],
	lri.[LRI_DUE_P]
FROM
	DIVREF2 div
		INNER JOIN
	LRIDUE lri on
		lri.KBCI_NO = div.KBCI_NO
WHERE
	div.FOR_LRI > 0 and
	lri.LRI_DUE_P > 0
ORDER BY
	div.KBCI_NO,
	lri.LRI_DUE_P

OPEN LRI_CURSOR

FETCH LRI_CURSOR INTO
	@KBCI_NO,
	@LNAME,
	@FNAME,
	@MI,
	@FEBTC_SA,
	@DIVIDEND,
	@REFUND,
	@TOTAL,
	@DEDUCTIONS,
	@GTOTAL,
	@FOR_LRI,
	@PN_NO,
	@LRI_DUE_P

WHILE @@FETCH_STATUS = 0
BEGIN
	SET @xplri = 0
	SET @xclri = 0
	
	IF @xkbci != @KBCI_NO
	BEGIN
		SET @xkbci = @KBCI_NO
		SET @xlri = @FOR_LRI
	END
	
	IF @LRI_DUE_P > 0
	BEGIN
		SET @xplri = 0
		
		IF @xlri - @LRI_DUE_P < 0
		BEGIN
			SET @xplri = @xlri
			SET @xclri = @LRI_DUE_P - @xlri
			SET @xlri = 0
		END
		ELSE
		BEGIN
			SET @xplri = @LRI_DUE_P
			SET @xclri = 0
			SET @xlri = @xlri - @xplri
		END
		
		INSERT
			@VOUCHER
		EXEC
			S3P_J_GEN_LAPP 'V'
		
		SELECT
			@xor_no = XOR_NO
		FROM
			@VOUCHER
			
		DELETE
			@VOUCHER
			
		INSERT INTO PAYHIST
		(
			KBCI_NO,
			PN_NO,
			PAYTYPE,
			PAYAMT,
			PAYDATE,
			ADDATE,
			LUPDATE,
			UPDUSER,
			PAYOR,
			PAYREM
		)
		VALUES
		(
			@KBCI_NO,
			@PN_NO,
			'4',
			@xplri,
			@SYSDATE,
			@SYSDATE,
			@SYSDATE,
			@my_user,
			@xor_no,
			'PAY:' + @xor_no
		)
		
		UPDATE
			dbo.LOANS
		SET
			CHG_DATE = @SYSDATE,
			LRI_DUE = CASE
				WHEN LRI_DUE - @xplri < 0  THEN 0
				ELSE LRI_DUE - @xplri
				END
		WHERE
			PN_NO = @PN_NO
		
		IF @xclri = 0
		BEGIN
			UPDATE
				dbo.LRIDUE
			SET
				LRI_DUE_C = 0,
				LRI_DUE_P = 0,
				LRI_DUE = DATEADD(M, 12, LRI_DUE),
				LRI_BALDA = DATEADD(M, 12, LRI_BALDA)
			WHERE
				PN_NO = @PN_NO
		END
		BEGIN
			UPDATE
				dbo.LRIDUE
			SET
				LRI_DUE_C = CASE
					WHEN LRI_DUE_C - @xplri < 0 THEN 0
					ELSE LRI_DUE_C - @xplri
					END,
				LRI_DUE_P = LRI_DUE_P - @xplri
			WHERE
				PN_NO = @PN_NO
		END
		
		EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @xor_no, 'PAY', 'LRI', @xplri, 'LRI HOLD-DIVREF'
	END

	FETCH LRI_CURSOR INTO
		@KBCI_NO,
		@LNAME,
		@FNAME,
		@MI,
		@FEBTC_SA,
		@DIVIDEND,
		@REFUND,
		@TOTAL,
		@DEDUCTIONS,
		@GTOTAL,
		@FOR_LRI,
		@PN_NO,
		@LRI_DUE_P

END

CLOSE LRI_CURSOR
DEALLOCATE LRI_CURSOR



GO