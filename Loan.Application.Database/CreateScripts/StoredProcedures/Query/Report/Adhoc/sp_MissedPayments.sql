USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_MissedPayments]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Adhoc_MissedPayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Adhoc_MissedPayments]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_MissedPayments]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Report_Adhoc_MissedPayments]
@dateFrom datetime,
@dateTo datetime,
@dateAsOf datetime,
@sortByName bit
AS

declare @outstanding table
(
	SORT_BY_NAME bit,
	SORT varchar(100),
	DETAIL varchar(100),
	KBCI_NO varchar(7),
	PN_NO varchar(7),
	DATE_GRANT datetime,
	NDUE datetime,
	TERM int,
	LOAN_AMOUNT numeric(12,4),
	NET_PROCEEDS numeric(12,4),
	LOAN_BALANCE numeric(12,4),
	STAT varchar(1),
	RATE numeric(7,4)
)
	
declare @missed table
(
	PN_NO varchar(7)
)

declare @amortization table 
(
	[DATE] date,
	[OUTS_BALANCE] numeric(14,4),
	[AMORT_PRIN] numeric(14,4),
	[AMORT_INT] numeric(14,4),
	[TOTAL_AMORT] numeric(14,4),
	[PRINCIPAL] numeric(14,4),
	[INTEREST] numeric(14,4),
	[TOTAL_PAYMENT] numeric(14,4),
	[FULL_NAME] varchar(100),
	[LOAN_DESC] varchar(100)
)

declare @PN_NO as varchar(7)
declare @missedCount int

insert into @outstanding
exec Report_Adhoc_Runup @dateFrom, @dateTo, @dateAsOf, @sortByName

/* Exclude others */
delete from
	@outstanding
where
	not 
	(
		DATE_GRANT between @dateFrom and @dateTo and
		DETAIL != 'STL'
	)

declare LOANS_CURSOR cursor for
select
	PN_NO
from
	@outstanding

open
	LOANS_CURSOR
	
fetch
	LOANS_CURSOR
into
	@PN_NO

while @@FETCH_STATUS = 0
begin

	delete from @amortization

	insert into @amortization
	exec Report_Loans_AmortizationSchedule @PN_NO
	
	select
		@missedCount = SUM
		(
			case
				when (b.PAID_PRI + b.PAID_INT) > (a.AMORT_PRIN + a.AMORT_INT - 1) then 0
				else 1
				end
		)
	from
	(	
		select
			*
		from
			@amortization
		where
			[DATE] between @dateFrom and @dateTo
	) a
		inner join
	(
		select
			[DATE],
			sum
			(
				case
					when ACCT_TYPE = 'PAY' and ACCT_CODE = 'PRI' then CR
					else 0
					end
			) PAID_PRI,
			sum
			(
				case
					when ACCT_TYPE = 'PAY' and ACCT_CODE = 'INT' then CR
					else 0
					end
			) PAID_INT
		from
			dbo.LOANS lon with(nolock)
				inner join
			dbo.LEDGER led with(nolock) on
				led.PN_NO = lon.PN_NO
		where
			lon.PN_NO = @PN_NO and
			led.[DATE] != lon.CHKNO_DATE
		group by
			[DATE]
	) b on
		a.[DATE] = b.[DATE]
		
	if @missedCount > 0
	begin
		insert into	@missed
		(
			PN_NO
		)
		values
		(
			@PN_NO
		)
	end
		
	fetch
		LOANS_CURSOR
	into
		@PN_NO

end

close LOANS_CURSOR
deallocate LOANS_CURSOR

select
	SORT_BY_NAME,
	SORT,
	DETAIL,
	KBCI_NO,
	o.PN_NO,
	DATE_GRANT,
	NDUE,
	TERM,
	LOAN_AMOUNT,
	NET_PROCEEDS,
	o.LOAN_BALANCE,
	STAT,
	RATE
from
	@outstanding o
		inner join
	@missed m on
		m.PN_NO = o.PN_NO





GO