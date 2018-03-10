USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Others_LriMaintenance_Divref2_HoldLri]    Script Date: 05/04/2013 16:19:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Others_LriMaintenance_Divref2_HoldLri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Others_LriMaintenance_Divref2_HoldLri]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Others_LriMaintenance_Divref2_HoldLri]    Script Date: 05/04/2013 16:19:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[s3p_Others_LriMaintenance_Divref2_HoldLri]
@dt1 DATETIME,
@dt2 DATETIME

AS

CREATE TABLE #TXLRI
(
	[PN_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[KBCI_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[LRI_DUE] date,
	[LRI_BALDA] date,
	[LOAN_BAL] numeric(14, 2) DEFAULT 0,
	[LRI_DUE_C] numeric(14, 2) DEFAULT 0,
	[LRI_DUE_P] numeric(14, 2) DEFAULT 0,
	[LRI_DUE_Y] numeric(14, 2) DEFAULT 0,
	[DIV] numeric(14, 2) DEFAULT 0,
	[REF] numeric(14, 2) DEFAULT 0,
	[NAME] varchar(45) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[LN] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS
)

UPDATE
	DIVREF
SET
	DEDUCTIONS = 0,
	DETAILS = REPLICATE(' ', 10);

INSERT INTO	#TXLRI
(
	KBCI_NO,
	DIV,
	REF,
	NAME,
	LRI_DUE_P
)
SELECT
	div.KBCI_NO,
	div.DIVIDEND,
	div.REFUND,
	isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
	sum(lri.LRI_DUE_P) TLRIDUEC
FROM
	dbo.DIVREF div
		inner join
	dbo.MEMBERS mem on
		mem.KBCI_NO = div.KBCI_NO
		inner join
	dbo.LRIDUE lri on
		lri.KBCI_NO = div.KBCI_NO
WHERE
	ISNULL(ROUND(lri.LRI_DUE_P, 2), 0) > 0 AND
	lri.LRI_DUE BETWEEN @dt1 AND @dt2 AND
	mem.MEM_CODE != 'S'
GROUP BY
	div.KBCI_NO,
	div.DIVIDEND,
	div.REFUND,
	isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , '');

UPDATE
	tgt
SET
	DEDUCTIONS = CASE
		WHEN ISNULL(tgt.DIVIDEND, 0) + ISNULL(tgt.REFUND, 0) - ISNULL(src.LRI_DUE_P, 0) <= 0 THEN ISNULL(tgt.DIVIDEND, 0) + ISNULL(tgt.REFUND, 0)
		ELSE ISNULL(src.LRI_DUE_P, 0)
		END,
	DETAILS = 'HOLD LRI'
FROM
	DIVREF tgt
		inner join
	#TXLRI src on
		src.KBCI_NO = tgt.KBCI_NO;
		
TRUNCATE TABLE DIVREF2

INSERT INTO DIVREF2
(
	[KBCI_NO],
	[LNAME],
	[FNAME],
	[MI],
	[FEBTC_SA],
	[DIVIDEND],
	[REFUND],
	[DEDUCTIONS]
)
SELECT
	[KBCI_NO],
	[LNAME],
	[FNAME],
	[MI],
	[FEBTC_SA],
	[DIVIDEND],
	[REFUND],
	[DEDUCTIONS]
FROM
	dbo.DIVREF;

UPDATE
	dbo.DIVREF2
SET
	TOTAL = ISNULL(DIVIDEND, 0) + ISNULL(REFUND, 0),
	GTOTAL = ISNULL(DIVIDEND, 0) + ISNULL(REFUND, 0) - ISNULL(DEDUCTIONS, 0),
	FOR_LRI = ISNULL(DEDUCTIONS, 0)
WHERE
	KBCI_NO IN (
		SELECT
			KBCI_NO
		FROM
			MEMBERS
		WHERE
			MEM_STAT = 'A'
	);
GO