PROCEEDURE WHEN TRANSACTIONS EFFECT ON SDMASTER BUT NOT IN SDTRAN

declare @ACCTNUM varchar(50) = '0101057466'
declare @TRANEBAL numeric(20,4)

select
	TRANBBAL,
	TRANCRE,
	TRANDEB,
	TRANBBAL + TRANCRE - TRANDEB COMPUTED_TRANEBAL,
	TRANEBAL,
	CASE WHEN TRANBBAL + TRANCRE - TRANDEB = TRANEBAL THEN 'N' ELSE 'Y' END AS BALANCE_ERROR
from
	SDTRAN
where 
	ACCTNUM = @ACCTNUM
order by
	SDTRAN_ID

if exists (select SDTRAN_ID from SDTRAN where ACCTNUM = @ACCTNUM and TRANBBAL + TRANCRE - TRANDEB != TRANEBAL)
begin
	select 'Balance error.'
end
else
begin
	select
		@TRANEBAL = TRANEBAL
	from
		SDTRAN
	where
		SDTRAN_ID = (select max(SDTRAN_ID) from SDTRAN where ACCTNUM = @ACCTNUM)
	
	select
		'Before Patch' as NOTES,
		ACCTNO,
		ACCTLBAL,
		ACCTOBAL,
		ACCTABAL
	from
		SDMASTER
	where
		ACCTNO  = @ACCTNUM
	
	update
		SDMASTER
	set
		ACCTLBAL = @TRANEBAL,
		ACCTOBAL = @TRANEBAL,
		ACCTABAL = @TRANEBAL
	where
		ACCTNO  = @ACCTNUM
	
	select
		'After Patch' as NOTES,
		ACCTNO,
		ACCTLBAL,
		ACCTOBAL,
		ACCTABAL
	from
		SDMASTER
	where
		ACCTNO  = @ACCTNUM
end
