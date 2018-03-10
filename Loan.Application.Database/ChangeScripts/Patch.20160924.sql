/* Set floats to zero */

select
	ACCTNO,
	ACCTNAME,
	ACCTFLOATS
from
	SDMASTER x
where
	x.ACCTNO in ('0101003552','0101008910','0101007221','0101054165')

update
	SDMASTER
set
	ACCTFLOATS = 0
where
	ACCTNO in ('0101003552','0101008910','0101007221','0101054165')

select
	ACCTNO,
	ACCTNAME,
	ACCTFLOATS
from
	SDMASTER x
where
	x.ACCTNO in ('0101003552','0101008910','0101007221','0101054165')
	
go

/* Update KBCI_NO */

select
	x.KBCI_NO,
	s.*
from
	SDMASTER s
		inner join
	(
		select
		*
	from
		(values
			('0500291','0101055226'),
			('1600265','0101073909'),
			('1500953','0101073127'),
			('0601330','0101058241'),
			('0601365','0101058268')
		) as x(KBCI_NO,ACCT_NO)
	) x on x.ACCT_NO = s.ACCTNO

update
	s
set
	s.KBCI_NO = x.KBCI_NO
from
	SDMASTER s
		inner join
	(
		select
		*
	from
		(values
			('0500291','0101055226'),
			('1600265','0101073909'),
			('1500953','0101073127'),
			('0601330','0101058241'),
			('0601365','0101058268')
		) as x(KBCI_NO,ACCT_NO)
	) x on x.ACCT_NO = s.ACCTNO

select
	x.KBCI_NO,
	s.*
from
	SDMASTER s
		inner join
	(
		select
		*
	from
		(values
			('0500291','0101055226'),
			('1600265','0101073909'),
			('1500953','0101073127'),
			('0601330','0101058241'),
			('0601365','0101058268')
		) as x(KBCI_NO,ACCT_NO)
	) x on x.ACCT_NO = s.ACCTNO

go

/* Update FEBTC_SA */

select
	s.KBCI_NO,
	s.ACCTNO
into
	#temp
from
	SDMASTER s
		inner join
	MEMBERS m on
		s.KBCI_NO = m.KBCI_NO
where
	s.ACCTNO <>	m.FEBTC_SA

select
	m.KBCI_NO,
	s.ACCTNO,
	m.FEBTC_SA,
	m.LNAME,
	m.FNAME
from
	#temp s
		inner join
	MEMBERS m on
		s.KBCI_NO = m.KBCI_NO
order by
	case when isnull(m.FEBTC_SA,'')='' then 0 else 1 end,
	m.LNAME,
	m.FNAME

update
	m
set
	FEBTC_SA = s.ACCTNO
from
	#temp s
		inner join
	MEMBERS m on
		s.KBCI_NO = m.KBCI_NO

select
	m.KBCI_NO,
	s.ACCTNO,
	m.FEBTC_SA,
	m.LNAME,
	m.FNAME
from
	#temp s
		inner join
	MEMBERS m on
		s.KBCI_NO = m.KBCI_NO
order by
	case when isnull(m.FEBTC_SA,'')='' then 0 else 1 end,
	m.LNAME,
	m.FNAME

drop table #temp
	
go

/* Create new user */

insert into [USER]
	(
	USERNAME,
	USERPASS,
	[LEVEL],
	ADD_DATE,
	CHG_DATE,
	NAME,
	POSITION,
	[USER],
	LOGGED
	)
values
	(
	'MADEL',
	'uiNqZl4ScumAGSF5GkqtuA==',
	'1',
	getdate(),
	getdate(),
	'MARIDEL ASPILLA',
	'ADMIN ASSISTANT',
	'SYSTEM',
	'0'
	)

