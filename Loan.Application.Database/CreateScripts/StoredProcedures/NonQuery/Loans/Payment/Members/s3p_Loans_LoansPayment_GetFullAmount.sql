USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoansPayment_GetFullAmount]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoansPayment_GetFullAmount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoansPayment_GetFullAmount]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoansPayment_GetFullAmount]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Loans_LoansPayment_GetFullAmount]
@vPN_NO VARCHAR(7)
as


DECLARE @SYSDATE date
DECLARE @XBPRIN NUMERIC(12,4)
DECLARE @XADVINT NUMERIC(12,4)
DECLARE @XLASTD date
DECLARE @INTPAY NUMERIC(12,4)
DECLARE @XPINT NUMERIC(12,4)
DECLARE @WVADV BIT
DECLARE	@XPARR NUMERIC(12,4)
DECLARE	@XIARR NUMERIC(12,4)
DECLARE	@XPEN NUMERIC(12,4)
DECLARE @XIOUTS NUMERIC(12,4)
DECLARE @XPOUTS NUMERIC(12,4)
DECLARE @XTOUTS NUMERIC(12,4)
DECLARE @XTFULL NUMERIC(12,4)
DECLARE @XTPART NUMERIC(12,4)

DECLARE	@PAY_START date
DECLARE @NDUE date
DECLARE @DATE_DUE date
DECLARE @RATE NUMERIC(10, 2)
DECLARE @CHKNO_DATE date
DECLARE @LRI_DUE NUMERIC(10, 2)
DECLARE @ARREAR_AS date

SELECT	TOP 1 @SYSDATE = SYSDATE
FROM	DBO.CTRL

SET @XBPRIN = 0
SET @XADVINT = 0
-- SET @XLASTD = 0
SET @INTPAY = 0
SET @XPINT = 0
SET @WVADV = 0
SET @XPARR = 0
SET @XIARR = 0
SET @XPEN = 0
SET @XIOUTS = 0
SET @XPOUTS = 0
SET @XTOUTS = 0
SET @XTFULL = 0
SET @XTPART = 0

SELECT	@XBPRIN = SUM(
			CASE 
				WHEN ACCT_TYPE IN ('PAY', 'ADJ', 'AMT') AND ACCT_CODE = 'PRI' THEN DR-CR
				ELSE 0
				END
		),
		@XADVINT = SUM(
			CASE 
				WHEN ACCT_TYPE = 'INT' AND ACCT_CODE = 'INT' THEN CR
				ELSE 0
				END
		),
		@XLASTD = MAX(
			CASE 
				WHEN ACCT_TYPE IN ('PAY', 'ADJ') AND ACCT_CODE = 'PRI' THEN [DATE]
				ELSE ''
				END
		)
FROM	DBO.LEDGER 
WHERE 	PN_NO = @vPN_NO
GROUP	BY PN_NO

SET @XBPRIN = ISNULL(@XBPRIN, 0)
SET @XADVINT = ISNULL(@XADVINT, 0)

SELECT	@XPARR = ARREAR_P,
		@XIARR = ARREAR_I,
		@XPEN = ARREAR_OTH,
		@XPOUTS = P_BAL,
		@XIOUTS = I_BAL,
		@PAY_START = PAY_START,
		@NDUE = NDUE,
		@DATE_DUE = DATE_DUE,
		@RATE = RATE,
		@LRI_DUE = LRI_DUE,
		@CHKNO_DATE = CHKNO_DATE,
		@ARREAR_AS = ARREAR_AS
FROM	DBO.LOANS
WHERE	PN_NO = @vPN_NO

IF @@ROWCOUNT = 0 RAISERROR('PN_NO NOT FOUND!', 16, 1)

IF @XLASTD = '01-01-1900' SET @XLASTD = NULL

SELECT	@INTPAY = SUM(CR-DR)
FROM	DBO.LEDGER 
WHERE 	PN_NO = @vPN_NO AND
		ACCT_CODE = 'INT' AND
		ACCT_TYPE IN ('PAY', 'ADJ') AND
		(
			(
				@XLASTD IS NULL AND
				[DATE] >= @CHKNO_DATE
			) OR
			(
				@XLASTD IS NOT NULL AND
				[DATE] > @XLASTD
			)
		)

SET @INTPAY = ISNULL(@INTPAY, 0)

IF @NDUE = @PAY_START
BEGIN
	IF @XLASTD IS NULL OR @XLASTD = ''
	BEGIN
		IF @DATE_DUE <> @PAY_START OR @NDUE <> @SYSDATE
		BEGIN
			SET @XPINT = (@XBPRIN * (@RATE/100/360) * DATEDIFF(DD, @CHKNO_DATE, @SYSDATE)) - @XADVINT			
		END
		IF @SYSDATE < @PAY_START
			SET @WVADV = 1
	END
	ELSE
	BEGIN
		SET @XPINT = @XBPRIN * (@RATE/100/360) * DATEDIFF(DD, @XLASTD, @SYSDATE)
		SET @XIOUTS = 0
		SET @XPOUTS = 0
	END
END
ELSE
BEGIN
	SET @XTOUTS = @XPARR + @XIARR + @XPEN
	IF @XTOUTS > 0
	BEGIN
		SET @XPINT = 0
		SET @INTPAY = 0
	END
	ELSE
	BEGIN
		IF @XLASTD IS NULL OR @XLASTD = ''
		BEGIN
			SET @XPINT = (@XBPRIN * (@RATE/100/360) * DATEDIFF(DD, @CHKNO_DATE, @SYSDATE)) - @XADVINT
			IF @SYSDATE = @NDUE
			BEGIN
				SET @XIOUTS = ROUND(@XIOUTS, 2)
				SET @XPINT = 0
			END			
		END
		ELSE
			SET @XPINT = (@XBPRIN * (@RATE/100/360) * DATEDIFF(DD, @XLASTD, @SYSDATE))
	END	
END

SET @XPINT = ROUND(@XPINT - @INTPAY, 2)

IF @WVADV = 1
	SET @XTFULL = @XBPRIN + @XPINT + @XIARR + @XPEN + @LRI_DUE
ELSE
	BEGIN
	SET @XADVINT = 0
	IF @XIARR + @XPEN > 0
		SET @XTFULL = @XBPRIN + @XIARR + @XPEN + @LRI_DUE
	ELSE
		SET @XTFULL = @XBPRIN + @XPINT + @XIOUTS + @XIARR + @XPEN + @LRI_DUE
	END

SET @XTPART = @XPARR + @XIARR + @XPEN + @XPOUTS + @XIOUTS

SELECT  round(@XBPRIN, 2) as txtBalOnPrin,
		round(@XADVINT, 2) XADVINT,
		@XLASTD XLASTD,
		round(@INTPAY, 2) INTPAY,
		round(@XPINT, 2) as txtPretInterest,
		@WVADV WVADV,
		round(@XPARR, 2) as txtPrincipal,
		round(@XIARR, 2) as txtInterest,
		round(@XPEN, 2) as txtPenalty,
		round(@XIOUTS, 2) as txtInt,
		round(@XPOUTS, 2) as txtOutsPrin,
		round(@XTOUTS, 2) XTOUTS,
		round(@XTFULL, 2) as txtFullAmount,
		round(@XTPART, 2) XTPART,
		round(@LRI_DUE, 2) LRI_DUE,
		@ARREAR_AS ARREAR_AS




GO


