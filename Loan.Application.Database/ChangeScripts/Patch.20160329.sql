use KBCI

-- recompute balance

select
	'Before Patch' as 'LABEL',
	SDTRAN_ID,
	TRANBBAL,
	TRANDEB,
	TRANCRE,
	TRANEBAL
from
	SDTRAN
where
	ACCTNUM = '0101039557'
order by
	TRANDATE, SDTRAN_ID

;with data
as
(
	select
		ROW_NUMBER() over (order by TRANDATE, SDTRAN_ID) ID,
		*
	from
		SDTRAN
	where
		ACCTNUM = '0101039557'
),
bal as
(
	select
		ID,
		SDTRAN_ID,
		TRANBBAL,
		TRANDEB,
		TRANCRE,
		cast(TRANBBAL - TRANDEB + TRANCRE as numeric(11,2)) AS 'TRANEBAL'
	from
		data
	where
		ID = 1
	union all
	select
		d.ID,
		d.SDTRAN_ID,
		b.TRANEBAL AS 'TRANBBAL',
		d.TRANDEB,
		d.TRANCRE,
		cast(b.TRANEBAL - d.TRANDEB + d.TRANCRE as numeric(11,2)) AS 'TRANEBAL'
	from
		data d
			inner join
		bal b on
			b.ID + 1 = d.ID
	
)
update
	tgt
set
	TRANEBAL = src.TRANEBAL
from
	bal src
		inner join
	SDTRAN tgt on
		src.SDTRAN_ID = tgt.SDTRAN_ID
option
	(MAXRECURSION 10000);

select
	'After Patch' as 'LABEL',
	SDTRAN_ID,
	TRANBBAL,
	TRANDEB,
	TRANCRE,
	TRANEBAL
from
	SDTRAN
where
	ACCTNUM = '0101039557'
order by
	TRANDATE, SDTRAN_ID

-- patch account number

select
	'Before Patch' as 'LABEL',
	KBCI_NO,
	LNAME,
	FNAME,
	FEBTC_SA
from
	MEMBERS
where
	KBCI_NO in ('1501119','1501127')

update
	MEMBERS
set
	FEBTC_SA = '0101073283'
where
	KBCI_NO = '1501119'

update
	MEMBERS
set
	FEBTC_SA = '0101073291'
where
	KBCI_NO = '1501127'

select
	'After Patch' as 'LABEL',
	KBCI_NO,
	LNAME,
	FNAME,
	FEBTC_SA
from
	MEMBERS
where
	KBCI_NO in ('1501119','1501127')
