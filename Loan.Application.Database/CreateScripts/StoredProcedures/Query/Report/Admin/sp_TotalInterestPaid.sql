USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_TotalInterestPaid]    Script Date: 08/30/2014 14:40:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_TotalInterestPaid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_TotalInterestPaid]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_TotalInterestPaid]    Script Date: 08/30/2014 14:40:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*****************************************************************************
MODIFIED:
JS 06/18/2016		INCLUDE PTL
*****************************************************************************/

CREATE PROCEDURE [dbo].[Report_Admin_TotalInterestPaid]
@source varchar(3),
@year int
as

declare @sql nvarchar(max)	

/* Create temp tables */
	
select
	LED.PN_NO,
	LED.[DATE],
	LED.ACCT_TYPE,
	LED.ACCT_CODE,
	LED.DR,
	LED.CR
into
	#LEDGER
from
	LEDGER LED
where
	7 != 7;
	
select
	LON.PN_NO,
	LON.KBCI_NO,
	LON.LOAN_TYPE
into
	#LOANS
from
	LOANS LON
where
	7 != 7;

select
	M.KBCI_NO,
	M.LNAME,
	M.FNAME,
	M.MI,
	M.MEM_STAT
into
	#MEMBERS
from
	MEMBERS M
where
	7 != 7;

insert into #LEDGER
(
	PN_NO,
	[DATE],
	ACCT_TYPE,
	ACCT_CODE,
	DR,
	CR
)
SELECT
	LED.PN_NO,
	LED.DATE,
	LED.ACCT_TYPE,
	LED.ACCT_CODE,
	LED.DR,
	LED.CR
FROM
	LOANS LON
		INNER JOIN
	LEDGER LED ON
		LON.PN_NO = LED.PN_NO
WHERE
	DATEPART(YYYY, LED.DATE) = @year and
	--LON.LOAN_TYPE <> 'PTL' AND																				-- JS 06/18/2016
	LED.ACCT_TYPE <> 'INI' AND
	LED.ACCT_CODE = 'INT'

insert into #LOANS
(
	PN_NO,
	KBCI_NO,
	LOAN_TYPE
)
SELECT
	DISTINCT
	LON.PN_NO,
	LON.KBCI_NO,
	LON.LOAN_TYPE
FROM
	LOANS LON
		INNER JOIN
	LEDGER LED ON
		LON.PN_NO = LED.PN_NO
WHERE
	DATEPART(YYYY, LED.DATE) = @year and
	--LON.LOAN_TYPE <> 'PTL' AND																				-- JS 06/18/2016
	LED.ACCT_TYPE <> 'INI' AND
	LED.ACCT_CODE = 'INT'
	
insert into #MEMBERS
(
	KBCI_NO,
	LNAME,
	FNAME,
	MI,
	MEM_STAT
)
SELECT
	DISTINCT
	M.KBCI_NO,
	M.LNAME,
	M.FNAME,
	M.MI,
	M.MEM_STAT
FROM
	MEMBERS M
		INNER JOIN
	(
		SELECT
			DISTINCT
			LON.*
		FROM
			LOANS LON
				INNER JOIN
			LEDGER LED ON
				LON.PN_NO = LED.PN_NO
		WHERE
			DATEPART(YYYY, LED.DATE) = @year and
			--LON.LOAN_TYPE <> 'PTL' AND																		-- JS 06/18/2016
			LED.ACCT_TYPE <> 'INI' AND
			LED.ACCT_CODE = 'INT'
	) L ON
		M.KBCI_NO = L.KBCI_NO

/* Data cleansing */
	
update
	#LEDGER
set
	CR = isnull(CR, 0),
	DR = isnull(DR, 0)

select
	*
from
(	
	select
		dbo.func_Format241(mem.KBCI_NO) KBCI_NO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) [FULL_NAME],
		mem.MEM_STAT,
		'Total' as LOAN_TYPE,
		sum(convert(numeric(17,2), led.CR - led.DR)) INTEREST,
		@year [Year]
	from
		#MEMBERS mem
			cross join
		LOAN_TYPE lt
			left join
		#LOANS lon on
			lon.LOAN_TYPE = lt.LOAN_TYPE and
			lon.KBCI_NO = mem.KBCI_NO
			left join
		#LEDGER led ON
			lon.PN_NO = led.PN_NO
	group by
		dbo.func_Format241(mem.KBCI_NO),
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI),
		mem.MEM_STAT
	
	union all
	
	select
		dbo.func_Format241(mem.KBCI_NO) KBCI_NO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) [FULL_NAME],
		mem.MEM_STAT,
		lt.LOAN_TYPE,
		sum(convert(numeric(17,2), led.CR - led.DR)) INTEREST,
		@year [Year]
	from
		#MEMBERS mem
			cross join
		LOAN_TYPE lt
			left join
		#LOANS lon on
			lon.LOAN_TYPE = lt.LOAN_TYPE and
			lon.KBCI_NO = mem.KBCI_NO
			left join
		#LEDGER led ON
			lon.PN_NO = led.PN_NO
	--where																											-- JS 06/18/2016
		--lt.LOAN_TYPE != 'PTL'																						-- JS 06/18/2016
	group by
		dbo.func_Format241(mem.KBCI_NO),
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI),
		mem.MEM_STAT,
		lt.LOAN_TYPE
) final
order by
	FULL_NAME,
	LOAN_TYPE

drop table #MEMBERS
drop table #LOANS
drop table #LEDGER

GO

