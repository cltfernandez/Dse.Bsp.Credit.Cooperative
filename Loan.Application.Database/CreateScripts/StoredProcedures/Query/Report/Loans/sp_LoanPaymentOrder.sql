USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_LoanPaymentOrder]    Script Date: 07/03/2009 14:33:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_LoanPaymentOrder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_LoanPaymentOrder]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_LoanPaymentOrder]    Script Date: 07/03/2009 14:33:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Loans_LoanPaymentOrder]
@PN_NO VARCHAR(7)
as


declare @SYSDATE date
declare @XLASTD date
declare @WVADV bit = 0
declare @XBPRIN numeric(12,4) = 0
declare @XADVINT numeric(12,4) = 0
declare @INTPAY numeric(12,4) = 0
declare @XPINT numeric(12,4) = 0
declare @XPARR numeric(12,4) = 0
declare @XIARR numeric(12,4) = 0
declare @XPEN numeric(12,4) = 0
declare @XIOUTS numeric(12,4) = 0
declare @XPOUTS numeric(12,4) = 0
declare @XTOUTS numeric(12,4) = 0
declare @XTFULL numeric(12,4) = 0
declare @XTPART numeric(12,4) = 0

declare @PAY_START date
declare @NDUE date
declare @DATE_DUE date
declare @CHKNO_DATE date
declare @ARREAR_AS date
declare @RATE numeric(12,4) = 0
declare @LRI_DUE numeric(12,4) = 0

select
	@SYSDATE = SYSDATE
from
	dbo.CTRL with(nolock)

select	
	@XBPRIN = SUM
	(
		case 
			when ACCT_TYPE in ('PAY', 'ADJ', 'AMT') and ACCT_CODE = 'PRI' then DR-CR
			else 0
			end
	),
	@XADVINT = SUM
	(
		case 
			when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then CR
			else 0
			end
	),
	@XLASTD = MAX
	(
		case 
			when ACCT_TYPE IN ('PAY', 'ADJ') and ACCT_CODE = 'PRI' then [DATE]
			end
	)
from
	dbo.LEDGER with(nolock)
where
	PN_NO = @PN_NO
group by
	PN_NO

set @XBPRIN = isnull(@XBPRIN, 0)
set @XADVINT = isnull(@XADVINT, 0)

select
	@XPARR = ARREAR_P,
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
from
	dbo.LOANS with(nolock)
where
	PN_NO = @PN_NO

if @XLASTD = '01-01-1900' set @XLASTD = NULL

select
	@INTPAY = sum(CR-DR)
from
	dbo.LEDGER with(nolock)
where
	PN_NO = @PN_NO and
	ACCT_CODE = 'INT' and
	ACCT_TYPE in ('PAY', 'ADJ') and
	(
		(
			@XLASTD is null and
			[DATE] >= @CHKNO_DATE
		) or
		(
			@XLASTD is not null and
			[DATE] > @XLASTD
		)
	)

SET @INTPAY = ISNULL(@INTPAY, 0)

IF @NDUE = @PAY_START
BEGIN
	IF @XLASTD IS NULL OR @XLASTD = '' OR @XLASTD = '1900-01-01'
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
		IF ISNULL(@XLASTD, '1900-01-01') = '1900-01-01'
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
BEGIN
	SET @XTFULL = @XBPRIN + @XPINT + @XIARR + @XPEN + @LRI_DUE
END
ELSE
BEGIN
	SET @XADVINT = 0
	IF @XIARR + @XPEN > 0
		SET @XTFULL = @XBPRIN + @XIARR + @XPEN + @LRI_DUE
	ELSE
		SET @XTFULL = @XBPRIN + @XPINT + @XIOUTS + @XIARR + @XPEN + @LRI_DUE
END

SET @XTPART = @XPARR + @XIARR + @XPEN + @XPOUTS + @XIOUTS

SELECT
	TOP 1
	a.LOANS_ID, 
	dbo.func_FullName(b.LNAME, b.FNAME, b.MI) FULL_NAME,
	a.KBCI_NO,
	b.FEBTC_SA,
	a.COLLATERAL,
	a.LOAN_TYPE,
	a.LOAN_STAT,
	a.PN_NO,
	a.DATE_GRANT,
	a.DATE_DUE,
	a.RATE,
	a.ARREAR_AS,
	a.ARREAR_P,
	a.ARREAR_I,
	a.ARREAR_OTH,
	a.O_BAL,
	@SYSDATE SYSDATE,
	@XBPRIN as BALONPRIN,
	@XADVINT XADVINT,
	@XLASTD XLASTD,
	@INTPAY INTPAY,
	@XPINT as PRETINTEREST,
	@WVADV WVADV,
	@XPARR as PRINCIPAL,
	@XIARR as INTEREST,
	@XPEN as PENALTY,
	@XIOUTS as INT,
	@XPOUTS as OUTSPRIN,
	@XTOUTS XTOUTS,
	@XTFULL as FULL_AMOUNT,
	@XTPART XTPART,
	@LRI_DUE LRI_DUE,
	@ARREAR_AS ARREAR_AS,
	c.LRI_DUE_C
FROM
	dbo.LOANS a with(nolock)
		inner join
	dbo.MEMBERS b with(nolock) on 
		a.KBCI_NO = b.KBCI_NO
		left join
	dbo.LRIDUE c with(nolock) on
		c.PN_NO = a.PN_NO
WHERE
	a.PN_NO = @PN_NO




GO

