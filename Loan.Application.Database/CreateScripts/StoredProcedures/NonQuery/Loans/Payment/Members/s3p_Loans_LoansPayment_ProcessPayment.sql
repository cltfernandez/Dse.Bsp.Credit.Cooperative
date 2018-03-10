USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoansPayment_ProcessPayment]    Script Date: 04/17/2009 17:28:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoansPayment_ProcessPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoansPayment_ProcessPayment]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoansPayment_ProcessPayment]    Script Date: 04/17/2009 17:28:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*****************************************************************************
MODIFIED:
JS 04/14/2012		CHANGED FROM - TO + - FIXED DEPOSIT DOUBLING
JS 07/14/2012		CHANGED DATE TO DATETIME AND USED GETDATE() FOR TIME
JS 09/14/2012		TABLE-DRIVEN LOAN TYPE
JS 10/06/2012		USED GEN_LAPP
JS 06/14/2013		TRANSFERRED SA BALANCE VALIDATION TO VB
*****************************************************************************/

CREATE PROCEDURE [dbo].[s3p_Loans_LoansPayment_ProcessPayment]
@MY_USER VARCHAR(50),
@KBCI_NO VARCHAR(7),
@PN_NO VARCHAR(7),
@DATE_DUE date,
@LOAN_TYPE VARCHAR(3),
@LRI_DUE NUMERIC(10, 2),
@XBPRIN NUMERIC(12,4),
@XADVINT NUMERIC(12,4),
@XLASTD date,
@XPINT NUMERIC(12,4),
@XPARR NUMERIC(12,4),
@XIARR NUMERIC(12,4),
@XPEN NUMERIC(12,4),
@XIOUTS NUMERIC(12,4),
@XPOUTS NUMERIC(12,4),
@XP INTEGER,
@XPAYMENT NUMERIC(16,4),
@XFULPAY BIT,
@XOR_NO VARCHAR(10),
@XPDC_BNK VARCHAR(10),
@XPDC_NO VARCHAR(15),
@XSA_NO_OTH VARCHAR(10)
AS

DECLARE @MSG TABLE (
	MSG VARCHAR(200)
)

DECLARE @SYSDATE DATETIME				-- JS 07/14/2012
DECLARE @XSA_NO VARCHAR(10)
--DECLARE @XMBAL NUMERIC(16,4)
--DECLARE @XHBAL NUMERIC(16,4)
--DECLARE @XABAL NUMERIC(16,4)
DECLARE @XPPAY NUMERIC(16,4)
DECLARE @A2PAY NUMERIC(16,4)
DECLARE @XFDAM NUMERIC(16,4)

DECLARE	@iErr INT
DECLARE @iVoucher NUMERIC(3,0)

SET @iErr = 0

BEGIN TRANSACTION PROCESSING

SELECT	TOP 1 @SYSDATE = SYSDATE
FROM	DBO.CTRL

IF @XP = 1
BEGIN
	IF EXISTS (SELECT TOP 1 'X' FROM DBO.PAYHIST WHERE PAYOR = @XOR_NO)
	BEGIN
		INSERT	INTO @MSG (MSG)
		SELECT	'Error: OR number already used.' MSG
		SET @iErr = @iErr + 1
	END
END

ELSE IF @XP = 2
BEGIN

	IF ISNULL(@XSA_NO_OTH, '') != '' BEGIN
		SET		@XSA_NO = @XSA_NO_OTH
	END
	ELSE BEGIN
		SELECT	@XSA_NO = FEBTC_SA
		FROM	DBO.MEMBERS
		WHERE	KBCI_NO = @KBCI_NO
	END
		
--	SELECT	TOP 1 @XMBAL = MINBAL													-- JS 06/14/2013
--	FROM	DBO.CTRL																--		|
																					--		|
--	SELECT 	@XABAL = ACCTABAL														--		|
--	FROM 	DBO.SDMASTER															--		|
--	WHERE 	ACCTNO = @XSA_NO														--		|
																					--		|
--	SET @XHBAL = 0																	--		|
--	IF @XMBAL IS NULL SET @XMBAL = 0												--		|
																					--		|
--	IF @XABAL IS NULL																--		|
--	BEGIN																			--		|
--		INSERT	INTO @MSG (MSG)														--		|
--		SELECT	'Error: Invalid Account No.' MSG									--		|
--		SET @iErr = @iErr + 1														--		|
--	END																				--		|
--	ELSE																			--		|
--	BEGIN																			--		|
																					--		|
--		SELECT	@XHBAL = @XHBAL + HOLDAMT											--		|
--		FROM	DBO.LNHOLD															--		|
--		WHERE	ACCTNO = @XSA_NO AND												--		|
--			HOLDCD = 'PAY' AND														--		|
--			HOLDTYPE = 'DM' AND														--		|
--			POSTSTAT <> 'Y' AND														--		|
--			HOLDDATE = @SYSDATE														--		|
																					--		|
--		IF @XPAYMENT > (@XABAL - @XHBAL - @XMBAL)									--		|
--		BEGIN																		--		|
--			INSERT	INTO @MSG (MSG)													--		|
--			SELECT	'Error: SA balance cannot cover amount to paid.' MSG			--		|
--			SET @iErr = @iErr + 1													--		|
--		END																			--		|
																					--		|
--	END																				-- JS 06/14/2013

END

IF @iErr = 0
BEGIN

	IF @XP = 2 OR @XP = 3 
	BEGIN
		
		declare @gen_lapp table														-- JS 10/06/2012
		(																			--		|
			XOR_NO varchar(20)														--		|
		)																			--		|
																					--		|
		insert into @gen_lapp (XOR_NO)												--		|
		exec s3p_J_Gen_Lapp 'V'														--		|
																					--		|
		select	top 1																--		|
				@XOR_NO = XOR_NO													--		|
		from	@gen_lapp															--		|
																					--		|
		--SELECT 	@iVoucher = VOUCHER												--		|
		--FROM 	DBO.CTRL 															--		|
		--WITH 	(TABLOCKX)															--		|
																					--		|
		--IF @iVoucher = 999														--		|
		--BEGIN																		--		|
		--	UPDATE DBO.CTRL WITH (TABLOCKX) SET VOUCHER = 0							--		|
		--	SET @iVoucher = 0														--		|
		--END																		--		|
		--ELSE																		--		|
		--BEGIN																		--		|
		--	UPDATE DBO.CTRL WITH (TABLOCKX) SET VOUCHER = VOUCHER + 1				--		|
		--	SET @iVoucher = @iVoucher + 1											--		|
		--END																		--		|
																					--		|
		--SET @XOR_NO = 															--		|
		--	SUBSTRING(CONVERT(VARCHAR, DATEPART(yyyy, @SYSDATE)), 3, 2) +			--		|
		--	RIGHT('00' + CONVERT(VARCHAR, DATEPART(mm, @SYSDATE)), 2) +				--		|
		--	RIGHT('000' + CONVERT(VARCHAR, @iVoucher), 3)							-- JS 10/06/2012

	END

	INSERT	INTO DBO.PAYHIST (
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
	SELECT
		@KBCI_NO,
		@PN_NO,
		@XP,
		@XPAYMENT,
		@SYSDATE,
		@SYSDATE,
		@SYSDATE,
		@MY_USER,
		@XOR_NO,
		CASE @XP
			WHEN 2 
				THEN 'SA :' + @XSA_NO
			WHEN 3 
				THEN 'PDC :' + @XPDC_BNK + '-' + @XPDC_NO
			ELSE
				NULL
			END

	IF @XP = 2
		INSERT	INTO DBO.LNHOLD (
			ACCTNO,
			HOLDCD,
			HOLDTYPE,
			HOLDAMT,
			HOLDDATE,
			HOLDUSER,
			HOLDRMKS			
		)
		SELECT	@XSA_NO,
			'PAY',
			'DM',
			@XPAYMENT,
			@SYSDATE,
			@MY_USER,
			'PAYMENT: ' + @LOAN_TYPE + '-' + @PN_NO

	SELECT	*
	INTO	#LOANS
	FROM	DBO.LOANS	
	WHERE	PN_NO = @PN_NO
	
	UPDATE	#LOANS
	SET		CHG_DATE = @SYSDATE
	WHERE	PN_NO = @PN_NO

	SET @XPPAY = @XPAYMENT

------------------------------------------------- PENALTY PAYMENT -------------------------------------------------

	IF @XPEN > 0 AND @XPPAY > 0
	BEGIN

		SET @XPPAY = @XPPAY - @XPEN

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @XPEN
			SET @XPPAY = 0
		END
		ELSE
			SET @A2PAY = @XPEN

		IF @A2PAY > 0
		BEGIN
			-- EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'PRI', @A2PAY, 'LOAN PAY PRINCIPAL', NULL, @MY_USER
			EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'OTH', @A2PAY, 'LOAN PENALTY', NULL, @MY_USER

			UPDATE	#LOANS
			SET		ARREAR_OTH = CASE
						WHEN ARREAR_OTH - @A2PAY > 0 THEN ARREAR_OTH - @A2PAY
						ELSE 0
						END
			WHERE	PN_NO = @PN_NO

		END

	END

------------------------------------------------- ARREARS PAYMENT (INT) -------------------------------------------------

	IF @XIARR > 0 AND @XPPAY > 0
	BEGIN
		SET @XPPAY = @XPPAY - @XIARR

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @XIARR
			SET @XPPAY = 0
		END
		ELSE
			SET @A2PAY = @XIARR

		IF @A2PAY > 0
		BEGIN
			EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'INT', @A2PAY, 'LOAN ARREAR-INT', NULL, @MY_USER

			UPDATE	#LOANS
			SET		ARREAR_I = CASE	
						WHEN ARREAR_I - @A2PAY > 0 THEN ARREAR_I - @A2PAY
						ELSE 0
						END
			WHERE 	PN_NO = @PN_NO
					
		END

	END

------------------------------------------------- ARREARS PAYMENT (PRI) -------------------------------------------------

	IF (@XPARR > 0 AND @XPPAY > 0) AND @XFULPAY = 0
	BEGIN

		SET @XPPAY = @XPPAY - @XPARR

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @XPARR
			SET @XPPAY = 0
		END
		ELSE
			SET @A2PAY = @XPARR

		IF @A2PAY > 0
		BEGIN
			EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'PRI', @A2PAY, 'LOAN ARREAR-PRI', NULL, @MY_USER

			UPDATE	#LOANS
			SET 	ARREAR_P = CASE	
						WHEN ARREAR_P - @A2PAY > 0 THEN ARREAR_P - @A2PAY
						ELSE 0
						END,
					ARREAR_AS = CASE
						WHEN ARREAR_P = 0 THEN NULL
						WHEN ARREAR_P - @A2PAY > 0 THEN @SYSDATE
						ELSE NULL
						END,
					ACCU_PAYP = ACCU_PAYP + @A2PAY
			WHERE	PN_NO = @PN_NO
		END

	END

------------------------------------------------- OUTSTANDING BALANCE PAYMENT (INT) -------------------------------------------------

	IF (@XIOUTS > 0 AND @XPPAY > 0)
	BEGIN

		SET @XPPAY = @XPPAY - @XIOUTS

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @XIOUTS
			SET @XPPAY = 0
		END
		ELSE
			SET @A2PAY = @XIOUTS

		IF @A2PAY > 0
		BEGIN
			EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'INT', @A2PAY, 'LOAN AMORT-INT', NULL, @MY_USER

			UPDATE	#LOANS
			SET		I_BAL = CASE
						WHEN I_BAL - @A2PAY > 0 THEN I_BAL - @A2PAY
						ELSE 0
						END
			WHERE	PN_NO = @PN_NO

		END

	END

------------------------------------------------- OUTSTANDING BALANCE PAYMENT (PRI) -------------------------------------------------

	IF (@XPOUTS > 0 AND @XPPAY > 0) AND @XFULPAY = 0
	BEGIN

		SET @XPPAY = @XPPAY - @XPOUTS

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @XPOUTS
			SET @XPPAY = 0
		END
		ELSE
			SET @A2PAY = @XPOUTS

		IF @A2PAY > 0
		BEGIN
			EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'PRI', @A2PAY, 'LOAN AMORT-PRI', NULL, @MY_USER

			UPDATE	#LOANS
			SET		P_BAL = CASE
						WHEN P_BAL - @A2PAY > 0 THEN P_BAL - @A2PAY
						ELSE 0
						END,
					ACCU_PAYP = ACCU_PAYP + @A2PAY
			WHERE	PN_NO = @PN_NO

		END

	END

------------------------------------------------- LRI PAYMENT IF FULL PAYMENT (NEW) -------------------------------------------------

	IF (@LRI_DUE > 0 AND @XPPAY > 0) AND @XFULPAY = 1
	BEGIN
		SET @XPPAY = @XPPAY - @LRI_DUE

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @LRI_DUE
			SET @XPPAY = 0
		END 
		ELSE
			SET @A2PAY = @LRI_DUE

		IF @A2PAY > 0
		BEGIN
			EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'LRI', @A2PAY, 'LRI PAYMENT', NULL, @MY_USER

			UPDATE	#LOANS
			SET		LRI_DUE = CASE
						WHEN LRI_DUE - @A2PAY > 0 THEN LRI_DUE - @A2PAY
						ELSE 0
						END
			WHERE	PN_NO = @PN_NO

			IF EXISTS (SELECT TOP 1 'X' FROM DBO.LRIDUE WHERE PN_NO = @PN_NO)
			BEGIN
	
				IF NOT EXISTS (SELECT TOP 1 'X' FROM DBO.RLRIDUE WHERE PN_NO = @PN_NO) 
					INSERT INTO DBO.RLRIDUE (
							PN_NO, 
							KBCI_NO, 
							LRI_DUE, 
							LRI_BALDA, 
							LOAN_BAL, 
							LRI_DUE_C, 
							LRI_DUE_P, 
							LRI_DUE_Y
					) 
					SELECT 	PN_NO, 
							KBCI_NO, 
							LRI_DUE, 
							LRI_BALDA, 
							0, 
							0, 
							0, 
							0
					FROM	DBO.LRIDUE
					WHERE	PN_NO = @PN_NO
				ELSE
					UPDATE	DBO.RLRIDUE
					SET		LRI_DUE_C = 0,
							LRI_DUE_P = 0,
							LRI_DUE_Y = 0,
							LOAN_BAL = 0
					WHERE	PN_NO = @PN_NO

				UPDATE	#LOANS
				SET		LRI_DUE = 0
				WHERE	PN_NO = @PN_NO
					
			END
		END
	END

	IF (@XPINT > 0 AND @XPPAY > 0) AND (@XFULPAY = 1 AND (@XIARR + @XPEN) <= 0)
	BEGIN
		SET @XPPAY = @XPPAY - @XPINT

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @XPINT
			SET @XPPAY = 0
		END
		ELSE
			SET @A2PAY = @XPINT

		IF @A2PAY > 0 EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'INT', @A2PAY, 'PRETERMINATION INT', NULL, @MY_USER

	END

	IF (@XPINT > 0 AND @XPPAY > 0) AND (@XFULPAY = 0 AND (@XIARR + @XPEN) <= 0) AND @SYSDATE < @DATE_DUE
	BEGIN
		SET @XPPAY = @XPPAY - @XPINT

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @XPINT
			SET @XPPAY = 0
		END
		ELSE
			SET @A2PAY = @XPINT

		IF @A2PAY > 0 EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'INT', @A2PAY, 'PRETERMINATION INT', NULL, @MY_USER

	END

------------------------------------------------- PRINCIPAL PAYMENT -------------------------------------------------

	IF @XBPRIN > 0 AND @XPPAY > 0
	BEGIN
		SET @XPPAY = @XPPAY - @XBPRIN

		IF @XPPAY < 0
		BEGIN
			SET @A2PAY = @XPPAY + @XBPRIN
			SET @XPPAY = 0
		END
		ELSE
			SET @A2PAY = @XBPRIN

		IF @A2PAY > 0
		BEGIN
			EXEC s3p_J_U_Ledger @PN_NO, @SYSDATE, 'CM', @XOR_NO, 'PAY', 'PRI', @A2PAY, 'LOAN PAY PRINCIPAL', NULL, @MY_USER

			UPDATE	#LOANS
			SET		ACCU_PAYP = ACCU_PAYP + @A2PAY
			WHERE	PN_NO = @PN_NO
		END

		IF (SELECT ACCU_PAYP FROM #LOANS WHERE PN_NO = @PN_NO) >= (SELECT PRINCIPAL FROM #LOANS WHERE PN_NO = @PN_NO)
		BEGIN
			UPDATE	#LOANS
			SET		LOAN_STAT = 'F',
					CHG_DATE = @SYSDATE,
					P_BAL = 0,
					I_BAL = 0,
					O_BAL = 0,
					ARREAR_P = 0,
					ARREAR_I = 0,
					ARREAR_OTH = 0,
					ADVANCE = @XPPAY
			WHERE	PN_NO = @PN_NO

			INSERT	INTO @MSG (MSG)
			SELECT	'Loan is fully paid.' MSG

		END		
	END

------------------------------------------------- END PAYMENTS -------------------------------------------------

	DELETE
	FROM 	DBO.CTDHLON 
	WHERE 	PN_NO = @PN_NO 

	IF ((SELECT CHKNO_DATE FROM #LOANS WHERE PN_NO = @PN_NO) = @SYSDATE) AND @XFULPAY = 1 
		DELETE 
		FROM 	DBO.LNHOLD 
		WHERE 	ACCTNO = @XSA_NO AND 
				HOLDDATE = @SYSDATE AND 
				HOLDRMKS LIKE '%' + CONVERT(VARCHAR(7), @PN_NO)

	IF EXISTS (SELECT TOP 1 'X' FROM DBO.FD WHERE REF = CONVERT(VARCHAR(7), @PN_NO)) AND ((SELECT CHKNO_DATE FROM #LOANS WHERE PN_NO = @PN_NO) = @SYSDATE) AND @XFULPAY = 1
	BEGIN
		SELECT 	@XFDAM = SUM(CASE TRAN_CODE WHEN 1 THEN AMOUNT ELSE AMOUNT * -1	END)
		FROM 	DBO.FD
		WHERE	REF = @PN_NO

		DELETE
		FROM	DBO.FD
		WHERE	REF = @PN_NO

		UPDATE	DBO.MEMBERS
		SET		FD_AMOUNT = FD_AMOUNT + @XFDAM		--JS 04/14/2012
		WHERE	KBCI_NO = @KBCI_NO
	END
	
END

------------------------------------------------- EVALUATE TRANSACTION -------------------------------------------------

-- -- TEMP
-- 
-- 	ROLLBACK TRANSACTION PROCESSING
-- 
-- 	INSERT	INTO @MSG (MSG)
-- 	SELECT	'LOAN PAYMENT PROCESSING ABORTED !!!' MSG
-- 
-- 	INSERT	INTO @MSG (MSG)
-- 	SELECT	ISNULL(@XOR_NO, 'WALA') MSG
-- 
-- 	SELECT	MSG 
-- 	FROM	@MSG
-- 
-- 	RETURN
-- 
-- -- TEMP

IF @iErr > 0
BEGIN
	ROLLBACK TRANSACTION PROCESSING
	INSERT	INTO @MSG (MSG)
	SELECT	'Loan payment processing aborted.' MSG
END
ELSE
	BEGIN
	UPDATE	DBO.LOANS
	SET		[KBCI_NO] = B.[KBCI_NO],
			[APP_DATE] = B.[APP_DATE],
			[APP_NO] = B.[APP_NO],
			[DATE_GRANT] = B.[DATE_GRANT],
			[BY_WHOM] = B.[BY_WHOM],
			[DATE_DUE] = B.[DATE_DUE],
			[CHKNO_BANK] = B.[CHKNO_BANK],
			[CHKNO] = B.[CHKNO],
			[CHKNO_AMT] = B.[CHKNO_AMT],
			[CHKNO_DATE] = B.[CHKNO_DATE],
			[CHKNO_RELS] = B.[CHKNO_RELS],
			[CHKNO_ACK] = B.[CHKNO_ACK],
			[MOD_PAY] = B.[MOD_PAY],
			[AMORT_AMT] = B.[AMORT_AMT],
			[PAY_START] = B.[PAY_START],
			[RATE] = B.[RATE],
			[TERM] = B.[TERM],
			[FREQ] = B.[FREQ],
			[PRINCIPAL] = B.[PRINCIPAL],
			[LED_TYPE] = B.[LED_TYPE],
			[ADV_INTE] = B.[ADV_INTE],
			[AFT_INTE] = B.[AFT_INTE],
			[ACCU_PAYP] = B.[ACCU_PAYP],
			[YTD_I] = B.[YTD_I],
			[LOAN_TYPE] = B.[LOAN_TYPE],
			[LOAN_STAT] = B.[LOAN_STAT],
			[ARREAR_I] = B.[ARREAR_I],
			[ARREAR_P] = B.[ARREAR_P],
			[ARREAR_OTH] = B.[ARREAR_OTH],
			[ARREAR_AS] = B.[ARREAR_AS],
			[COLLATERAL] = B.[COLLATERAL],
			[DED_BAL] = B.[DED_BAL],
			[ADD_DATE] = B.[ADD_DATE],
			[CHG_DATE] = B.[CHG_DATE],
			[USER] = B.[USER],
			[P_BAL] = B.[P_BAL],
			[I_BAL] = B.[I_BAL],
			[O_BAL] = B.[O_BAL],
			[REC_STAT] = B.[REC_STAT],
			[RENEW] = B.[RENEW],
			[ADVANCE] = B.[ADVANCE],
			[LRI_IND] = B.[LRI_IND],
			[NDUE] = B.[NDUE],
			[L_EXT] = B.[L_EXT],
			[PD] = B.[PD],
			[LRI_DUE] = B.[LRI_DUE]
	FROM	DBO.LOANS A, #LOANS B
	WHERE	A.PN_NO = B.PN_NO AND
			B.PN_NO = @PN_NO

	INSERT	INTO @MSG (MSG)
	SELECT	'Payment successfully processed.' MSG
	COMMIT TRANSACTION PROCESSING
	END

SELECT	MSG 
FROM	@MSG

IF @iErr = 0 AND @XP <> 1
BEGIN

	--DECLARE @XCRSUM NUMERIC(16,4)
	--DECLARE @XDRSUM NUMERIC(16,4)

	DECLARE @PAYMENT TABLE (
		ADD_DATE date,
		DEBIT NUMERIC(16,4),
		REMARKS VARCHAR(50),
		CREDIT NUMERIC(16,4)
	)

	INSERT INTO @PAYMENT (ADD_DATE, DEBIT, REMARKS, CREDIT)
	SELECT	B.ADD_DATE,		
			CASE B.DOX_TYPE
				WHEN 'DM' THEN ISNULL(B.DR, 0)
				ELSE 0
				END DEBIT,
			CASE
				WHEN B.ACCT_CODE = 'LRI' THEN 'L.R.I. DUE'
				WHEN B.ACCT_CODE = 'OTH' THEN 'PENALTY'
				WHEN B.ACCT_CODE = 'INT' THEN 'INTEREST ON LOAN'
				WHEN B.ACCT_CODE = 'PRI' AND A.PD = 0 THEN 'LOAN PN: ' + A.PN_NO
				WHEN B.ACCT_CODE = 'PRI' AND A.PD = 1 THEN 'PAST DUE - ' + A.LOAN_TYPE + ' (PN: ' + A.PN_NO + ')'
				END XRMK,
			CASE B.DOX_TYPE
				WHEN 'DM' THEN 0
				ELSE ISNULL(B.CR, 0)
				END CREDIT
	FROM	DBO.LOANS A
				INNER JOIN
			DBO.LEDGER B ON
				A.PN_NO = B.PN_NO
	WHERE	A.PN_NO = B.PN_NO AND
			A.PN_NO = @PN_NO AND
			B.REF = @XOR_NO
	UNION
	SELECT	ADDATE,
			ISNULL(PAYAMT, 0) PAYAMT,
			CASE
				WHEN LEFT(PAYREM, 2) = 'SA' THEN REPLACE(PAYREM, 'SA :', 'SAVING(') + ')'
				WHEN LEFT(PAYREM, 3) = 'PDC' THEN REPLACE(PAYREM, 'PDC :', 'CASH ON HAND(') + ')'
				WHEN PAYREM IS NULL OR RTRIM(LTRIM(PAYREM)) = ''  THEN ''
				ELSE PAYREM
				END,
			0
	FROM	DBO.PAYHIST
	WHERE	PAYOR = @XOR_NO

	--SELECT @XDRSUM = SUM(DEBIT), @XCRSUM = SUM(CREDIT) FROM @PAYMENT

	SELECT	B.FNAME + ' ' + B.MI + ' ' + B.LNAME MEMBERNAME,
			@XOR_NO XOR_NO,
			@SYSDATE SYSDATE,
			CONVERT(VARCHAR, GETDATE(), 108) [TIME], -- JS 07/14/2012
			@MY_USER MY_USER,
			C.PAYREM,
			C.PAYDATE,
			ISNULL(D.LOAN_DESC, '') AS 'LOAN_TYPE',										-- JS 09/14/2012
			A.PN_NO
	FROM	dbo.LOANS A
				INNER JOIN
			dbo.MEMBERS B ON 
				A.KBCI_NO = B.KBCI_NO
				LEFT JOIN
			dbo.PAYHIST C ON 
				C.PN_NO = A.PN_NO AND 
				C.PAYOR = @XOR_NO
				LEFT JOIN
			dbo.LOAN_TYPE D ON															-- JS 09/14/2012
				D.LOAN_TYPE = A.LOAN_TYPE												-- JS 09/14/2012
	WHERE	A.PN_NO = @PN_NO

	SELECT	DEBIT, REMARKS, CREDIT
	FROM 	@PAYMENT
	ORDER	BY 
			ADD_DATE

END



GO


