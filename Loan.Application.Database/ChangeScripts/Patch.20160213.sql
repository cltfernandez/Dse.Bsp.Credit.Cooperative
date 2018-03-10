/* Sync FEBTC_SA acct no from SDMASTER */
UPDATE
	tgt
SET
	FEBTC_SA = src.ACCTNO
FROM
	MEMBERS tgt
		INNER JOIN
	(
		SELECT DISTINCT
			KBCI_NO, ACCTNO
		FROM
			SDMASTER
		WHERE
			ACCTSTAT != 'C' AND
			(
				KBCI_NO LIKE '16%' OR
				KBCI_NO = '1501003'
			)
	) src ON
		tgt.KBCI_NO = src.KBCI_NO
	
/* Set blank FD_DATE for new members instead of 1900-01-01 */
UPDATE
	MEMBERS
SET
	FD_DATE = NULL
WHERE
	year(ADD_DATE) = 2016 AND
	FD_DATE = '1900-01-01'