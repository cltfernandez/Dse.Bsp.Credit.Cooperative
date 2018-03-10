declare @FROM datetime = '2016-01-01'
declare @TO datetime = '2016-12-31'
declare @RATE numeric(12,4) = 0.2756

/* UPDATE ECD TO DM */

select * from SDTRAN where SDTRAN_ID = 471656
update SDTRAN set TRANCODE = 'DM' where SDTRAN_ID = 471656
select * from SDTRAN where SDTRAN_ID = 471656

/* UPDATE REFUND TO INCLUDE FAL, EML, BFL */

declare @int table
	(
	KBCI_NO varchar(7),
	REFUND numeric(12,4)
	)

insert into @int
	(
	KBCI_NO,
	REFUND
	)
select
	lon.KBCI_NO,
	sum(isnull(led.CR,0) - isnull(led.DR,0)) * @RATE REFUND
from
	LOANS lon
		inner join
	LEDGER led on
		lon.PN_NO = led.PN_NO
where
	led.ACCT_CODE = 'INT' and
	led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP', 'INT') and
	led.DATE between @FROM and @TO
group by
	lon.KBCI_NO

select
	div.KBCI_NO,
	div.LNAME,
	div.FNAME,
	div.REFUND,
	tmp.REFUND
from
	DIVREF div
		inner join
	@int tmp on
		div.KBCI_NO = tmp.KBCI_NO
order by
	div.LNAME,
	div.FNAME

update
	div
set
	REFUND = round(tmp.REFUND,0)
from
	DIVREF div
		inner join
	@int tmp on
		div.KBCI_NO = tmp.KBCI_NO

select
	div.KBCI_NO,
	div.LNAME,
	div.FNAME,
	div.REFUND,
	tmp.REFUND
from
	DIVREF div
		inner join
	@int tmp on
		div.KBCI_NO = tmp.KBCI_NO
order by
	div.LNAME,
	div.FNAME

/*
select
	lon.KBCI_NO,
	mem.LNAME,
	mem.FNAME,
	lon.LOAN_TYPE,
	led.*
from
	LOANS lon
		inner join
	LEDGER led on
		lon.PN_NO = led.PN_NO
		inner join
	MEMBERS mem on
		mem.KBCI_NO = lon.KBCI_NO
where
	led.ACCT_CODE = 'INT' and
	led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP', 'INT') and
	lon.KBCI_NO = '1400185' and
	led.DATE between '2016-01-01' and '2016-12-31'
order by
	lon.LOAN_TYPE

select
	lon.KBCI_NO,
	mem.LNAME,
	mem.FNAME,
	lon.LOAN_TYPE,
	sum(isnull(led.CR,0) - isnull(led.DR,0)) REFUND,
	sum(isnull(led.CR,0) - isnull(led.DR,0)) * @RATE REFUND
from
	LOANS lon
		inner join
	LEDGER led on
		lon.PN_NO = led.PN_NO
		inner join
	MEMBERS mem on
		mem.KBCI_NO = lon.KBCI_NO
where
	led.ACCT_CODE = 'INT' and
	led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP', 'INT') and
	led.DATE between '2016-01-01' and '2016-12-31'
group by
	lon.LOAN_TYPE,
	lon.KBCI_NO,
	mem.LNAME,
	mem.FNAME
order by
	mem.LNAME,
	mem.FNAME
*/
