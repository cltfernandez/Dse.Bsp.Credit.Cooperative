DECLARE @LOAN_TYPE VARCHAR(3) = 'BFL'
DECLARE @LOAN_DESC VARCHAR(30) = 'Better & Fast Financing Loan'
DECLARE @CODE5 VARCHAR(4) = NULL
DECLARE @TERM INT = 6
DECLARE @FREQ VARCHAR(1) = 'M'
DECLARE @RATE NUMERIC(12, 4) = '0'
DECLARE @MAX NUMERIC(12, 2) = 100000
DECLARE @MIN NUMERIC(12, 2) = 1000

INSERT INTO LOAN_TYPE
(
	LOAN_TYPE,
	LOAN_DESC,
	CODE5,
	MBAT
)
VALUES
(
	@LOAN_TYPE,
	@LOAN_DESC,
	@CODE5,
	NULL
)


INSERT INTO RUNUP1
(
	LOAN_TYP,
	LN_AMNT,
	FLN_AMNT,
	INT_AMNT,
	FINT_AMNT,
	DATE
)
VALUES
(
	@LOAN_TYPE,
	0,
	0,
	0,
	0,
	getdate()
)

INSERT INTO RRUNUP1
(
	LOAN_TYP,
	LN_AMNT,
	FLN_AMNT,
	INT_AMNT,
	FINT_AMNT,
	DATE
)
VALUES
(
	@LOAN_TYPE,
	0,
	0,
	0,
	0,
	getdate()
)

INSERT INTO PARAM
(
	LOAN_TYPE,
	TERM,
	FREQ,
	RATE,
	[MAX],
	[MIN],
	ADD_DATE,
	CHG_DATE,
	[USER]
)
VALUES
(
	@LOAN_TYPE,
	@TERM,
	@FREQ,
	@RATE,
	@MAX,
	@MIN,
	getdate(),
	getdate(),
	'SYSTEM'
)

go

