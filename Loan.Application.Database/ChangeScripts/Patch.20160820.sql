if not exists (select 'exists' from sys.tables st inner join sys.columns sc on st.object_id = sc.object_id where st.name = 'CTRL' and sc.name = 'BFL_DATE_DUE')
begin
	alter table CTRL add BFL_DATE_DUE date
end

go

update CTRL set BFL_DATE_DUE = '09/30/2016'
update LOAN_TYPE set LOAN_DESC = 'Better & Fast Financing Loan' where LOAN_TYPE = 'BFL'
update [PARAM] set [MAX] = 500000 where LOAN_TYPE = 'BFL'

go

DECLARE @BFL_DATE_DUE DATE
SELECT @BFL_DATE_DUE = BFL_DATE_DUE FROM CTRL

UPDATE
	LOANS
SET
	DATE_DUE = @BFL_DATE_DUE,
	PAY_START = @BFL_DATE_DUE
WHERE
	LOAN_TYPE = 'BFL'
	
go

UPDATE
	led
SET
	RMK = 'INITIAL - LRI'
FROM
	LOANS lon
		INNER JOIN
	LEDGER led on
		lon.PN_NO = led.PN_NO
WHERE
	lon.LOAN_TYPE ='BFL' and
	led.DOX_TYPE = 'DM' and
	led.ACCT_TYPE = 'LRI' and
	led.ACCT_CODE = 'OTH' and
	led.RMK = 'INIT-LRI'

UPDATE
	led
SET
	ACCT_TYPE = 'INT',
	ACCT_CODE = 'INT',
	RMK = 'INITIAL - ADD. INTEREST'
FROM
	LOANS lon
		INNER JOIN
	LEDGER led on
		lon.PN_NO = led.PN_NO
WHERE
	lon.LOAN_TYPE ='BFL' and
	led.DOX_TYPE = 'DM' and
	led.ACCT_TYPE = 'OTH' and
	led.ACCT_CODE = 'OTH' and
	led.RMK = 'INIT-OTHERS'