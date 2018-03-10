/* UPDATE LRIDUE */

	update
		dbo.LOANS
	set
		LRI_DUE = 0;
		
	update
		dbo.LRIDUE
	set
		LRI_DUE_C = 0,
		LRI_DUE_P = 0,
		LRI_DUE_Y = 0;

	exec Process_EndOfDay_LriDue

	go

/* RECOMPUTE ARREARS */

	declare @SYSDATE datetime
	select @SYSDATE = SYSDATE from ctrl

	update
		lon
	set
		ARREAR_I = isnull(((xbprin * RATE) / 36000) * DATEDIFF(D, xlastd, @SYSDATE), ARREAR_I) - drv110.INTEREST_PAID,
		ARREAR_OTH = isnull(((AMORT_AMT * 12.0000) / 36000) * DATEDIFF(D, xlastd, @SYSDATE), ARREAR_OTH) - drv110.PENALTIES
	from
		dbo.LOANS lon
			inner join
		(
			select
				drv10.PN_NO,
				drv10.xbprin,
				drv10.xlastd,
				sum
				(	case
						when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then CR - DR
						when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE = 'INT' then CR - DR
						else 0
						end
				) INTEREST_PAID,
				sum
				(
					case
						when ACCT_CODE = 'OTH' and ACCT_TYPE != 'LRI' and RMK like '%PEN%' and not RMK like '%INIT%' then CR - DR
						when ACCT_TYPE in ('ADJ', 'PAY', 'TER') and ACCT_CODE in ('OTH', 'PEN') then CR - DR
						else 0
						end
				) PENALTIES
			from
			(
				select
					lon.PN_NO,
					lon.PRINCIPAL + 
					sum
					(
						case
							when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') then led.DR - led.CR
							else 0
							end
					) as xbprin,
					max
					(
						case
							when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ') then led.[DATE]
							end
					) as xlastd
				from
					dbo.LOANS lon
						inner join
					dbo.LEDGER led on
						led.PN_NO = lon.PN_NO
				where
					lon.LOAN_TYPE != 'STL' and
					lon.LOAN_STAT = 'R' and
					lon.ARREAR_AS is not null
				group by
					lon.PN_NO, lon.PRINCIPAL
			) drv10
					left join
				dbo.LEDGER led on
					led.PN_NO = drv10.PN_NO and
					led.[DATE] > drv10.xlastd
			group by
				drv10.PN_NO,
				drv10.xbprin,
				drv10.xlastd
		) drv110 on
			drv110.PN_NO = lon.PN_NO
	where
		ARREAR_P > 0 and
		xbprin > 1
			
	go

/* DELETE LOANS WITHOUT LEDGER */

	delete
		lon
	from
		dbo.LOANS lon
	where
		not exists
		(
			select
				0
			from
				dbo.LEDGER
			where
				PN_NO = lon.PN_NO
		)
	
	go

/* ARCHIVE ERRONEOUS LOANS */

	-- ARCHIVE ERRONEOUS PAYMENT --

	if not exists (select 'x' from sys.tables where name = 'ERRONEOUS_PAYMENT_LOANS')
	begin

		declare @LOANS table
		(
			PN_NO varchar(7)
		)

		insert into @LOANS
		(
			PN_NO
		)
		select
			lon.PN_NO
			--,SUM(led.CR - led.DR) LEDGER
		from
			dbo.LOANS lon
				inner join
			dbo.LEDGER led
				on
			lon.PN_NO = led.PN_NO
		where
			lon.LOAN_STAT = 'F' and
			led.ACCT_TYPE in ('AMT','PAY','ADJ','TER','REP') and
			led.ACCT_CODE = 'PRI'
		group by
			lon.PN_NO
		having
			SUM(led.CR - led.DR) != 0

		select
			lon.*
		into
			dbo.ERRONEOUS_PAYMENT_LOANS
		from
			dbo.LOANS lon
				inner join
			@LOANS gigo on
				gigo.PN_NO = lon.PN_NO
		order by
			lon.LOANS_ID

		select
			led.*
		into
			dbo.ERRONEOUS_PAYMENT_LEDGER
		from
			dbo.LEDGER led
				inner join
			@LOANS gigo on
				gigo.PN_NO = led.PN_NO
		order by
			led.LEDGER_ID
		
		delete
			lon
		from 
			LOANS lon
				inner join
			@LOANS gigo on
				gigo.PN_NO = lon.PN_NO

		delete
			led
		from 
			LEDGER led
				inner join
			@LOANS gigo on
				gigo.PN_NO = led.PN_NO

	end
	
	-- ARCHIVE ERRONEOUS PRINCIPAL --

	if not exists (select 'x' from sys.tables where name = 'ERRONEOUS_PRINCIPAL_LOANS')
	begin

		delete @LOANS

		insert into @LOANS
		(
			PN_NO
		)
		select
			lon.PN_NO
			--,lon.PRINCIPAL
			--,SUM(led.DR - led.CR) LEDGER
		from
			dbo.LOANS lon
				inner join
			dbo.LEDGER led
				on
			lon.PN_NO = led.PN_NO
		where
			lon.LOAN_STAT = 'F' and
			led.ACCT_TYPE = 'AMT' and
			led.ACCT_CODE = 'PRI'
		group by
			lon.PN_NO,
			lon.PRINCIPAL
		having
			SUM(led.DR - led.CR) != lon.PRINCIPAL

		select
			lon.*
		into
			dbo.ERRONEOUS_PRINCIPAL_LOANS
		from
			dbo.LOANS lon
				inner join
			@LOANS gigo on
				gigo.PN_NO = lon.PN_NO
		order by
			lon.LOANS_ID

		select
			led.*
		into
			dbo.ERRONEOUS_PRINCIPAL_LEDGER
		from
			dbo.LEDGER led
				inner join
			@LOANS gigo on
				gigo.PN_NO = led.PN_NO
		order by
			led.LEDGER_ID

		delete
			lon
		from 
			LOANS lon
				inner join
			@LOANS gigo on
				gigo.PN_NO = lon.PN_NO

		delete
			led
		from 
			LEDGER led
				inner join
			@LOANS gigo on
				gigo.PN_NO = led.PN_NO

		end
	
	go

/* DELETE DUPLICATE USERS */

	delete u
	from
		[USER] u
	inner join
	(
		select 
			[USERNAME], 
			MAX([USER_ID]) as [USER_ID]
		from 
			[USER] 
		group by
			[USERNAME]
		having 
			COUNT([USERNAME]) > 1
	) x on
		u.[USERNAME] = x.[USERNAME] and
		u.[USER_ID] < x.[USER_ID]
	
	go

/* SET GENERIC USER PASSWORD */

	update
		dbo.[USER]
	set
		USERPASS = 'uiNqZl4ScumAGSF5GkqtuA=='
	
	go
		
/* SET ACCESS LEVEL */

	update
		[USER]
	set
		[LEVEL] = '1'
	where
		USERNAME = 'MIRLO'
	
	go

/* UPDATE RYAN'S MEM_CODE */

	update
		dbo.MEMBERS
	set
		MEM_CODE = 'S'
	where
		KBCI_NO IN ('9099713', '9099660')

	go
	
/* UPDATE FD */

	update
		dbo.FD
	set
		LPOSTED = 0
	where
		LPOSTED IS NULL
	
	go