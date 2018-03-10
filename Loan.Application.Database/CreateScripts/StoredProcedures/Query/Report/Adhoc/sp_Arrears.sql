USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Arrears]    Script Date: 11/16/2013 18:28:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Adhoc_Arrears]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Adhoc_Arrears]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Arrears]    Script Date: 11/16/2013 18:28:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[Report_Adhoc_Arrears]
@dateFrom datetime,
@dateTo datetime
as

declare @ARREARS table
(
	TOTAL		bit,
	NAME		varchar(100),
	PN_NO		varchar(7),
	LOAN_TYPE	varchar(3),
	END_DATE	date,
	PAID		char(1),
	LABEL		varchar(20),
	AMOUNT		numeric(20,4)
)

declare @labels table
(
	REPORT_DATE date,
	LABEL varchar(20)
)

declare @preterminated table
(
	PN_NO varchar(7),
	[DATE] datetime
)

;with cte (ID, REPORT_DATE) as (
	select
		1, dateadd(month, 0, @dateFrom)
	union all
	select
		ID + 1, dateadd(month, ID, @dateFrom)
	from
		cte
	where
		dateadd(month, ID, @dateFrom) <= @dateTo
)
	insert into @labels
	(
		REPORT_DATE,
		LABEL
	)
	select
		REPORT_DATE,
		upper(left(datename(MM, REPORT_DATE), 3)) + '-' + datename(YYYY, REPORT_DATE) + ' PAY'
	from cte
	union
	select
		REPORT_DATE,
		upper(left(datename(MM, REPORT_DATE), 3)) + '-' + datename(YYYY, REPORT_DATE) + ' ARR'
	from cte
	
insert into @preterminated
(
	PN_NO,
	[DATE]
)
select
	p.PN_NO,
	p.[DATE]
from
(
	select
		led.PN_NO,
		led.[DATE]
	from
		dbo.LEDGER led
			inner join
		dbo.LOAN_ARREARS lar on
			led.PN_NO = lar.PN_NO
	where
		lar.SYSDATE between @dateFrom and @dateTo and
		led.ENDBAL = 0
) p
	inner join
(
	select
		led.PN_NO,
		led.[DATE]
	from
		dbo.LEDGER led
			inner join
		dbo.LOAN_ARREARS lar on
			led.PN_NO = lar.PN_NO
	where
		lar.SYSDATE between @dateFrom and @dateTo and
		led.RMK = 'PRETERMINATION INT'
) i on
	p.PN_NO = i.PN_NO and
	p.[DATE] = i.[DATE]

insert into @ARREARS
(
	TOTAL,
	NAME,
	PN_NO,
	LOAN_TYPE,
	END_DATE,
	PAID,
	LABEL,
	AMOUNT
)
select
	0,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) as NAME,
	lar.PN_NO,
	lon.LOAN_TYPE,
	lar.SYSDATE END_DATE,
	'N' PAID,
	upper(left(datename(MM, lar.SYSDATE), 3)) + '-' + datename(YYYY, lar.SYSDATE) + ' ARR' as [LABEL],
	lar.ARREAR_P + lar.ARREAR_I + lar.ARREAR_OTH as [AMOUNT]
from
	dbo.LOAN_ARREARS lar
		inner join
	dbo.LOANS lon on
		lar.PN_NO = lon.PN_NO
		inner join
	dbo.MEMBERS mem on
		lon.KBCI_NO = mem.KBCI_NO
where
	lar.SYSDATE between @dateFrom and @dateTo and
	lon.PD = 0 and
	mem.MEM_STAT = 'A'
	
insert into @ARREARS
(
	TOTAL,
	NAME,
	PN_NO,
	LOAN_TYPE,
	END_DATE,
	PAID,
	LABEL,
	AMOUNT
)
select
	0,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) as NAME,
	lon.PN_NO,
	lon.LOAN_TYPE,
	lar.SYSDATE END_DATE,
	'Y' PAID,
	upper(left(datename(MM, lar.SYSDATE), 3)) + '-' + datename(YYYY, lar.SYSDATE) + ' PAY' as [LABEL],
	sum(case when led.ACCT_CODE = 'PRI' then isnull(led.CR, 0) - isnull(led.DR, 0) else 0 end) +
	sum(case when led.ACCT_CODE = 'INT' then isnull(led.CR, 0) - isnull(led.DR, 0) else 0 end) +
	sum(case when led.ACCT_CODE = 'OTH' then isnull(led.CR, 0) - isnull(led.DR, 0) else 0 end) as AMOUNT
from
	dbo.LOAN_ARREARS lar
		inner join
	dbo.LOANS lon on
		lar.PN_NO = lon.PN_NO
		inner join
	dbo.MEMBERS mem on
		lon.KBCI_NO = mem.KBCI_NO
		left join
	dbo.LEDGER led on
		led.PN_NO = lon.PN_NO and
		led.[DATE] between @dateFrom and @dateTo
		left join
	@preterminated pre on
		pre.PN_NO = led.PN_NO
where
	lar.SYSDATE between @dateFrom and @dateTo and
	lon.PD = 0 and
	mem.MEM_STAT = 'A' and
	(
		--led.RMK like '%ARREAR%' or
		led.RMK in
		(
			'LOAN PENALTY',
			'LOAN ARREAR-INT',
			'LOAN ARREAR-PRI',
			'PAYROLL DEDUCTION-PEN',
			'PAYROLL DEDUCTION ARR-INT',
			'PAYROLL DEDUCTION ARR-PRI',
			'OTHERPAY DEDUCTION-PEN',
			'OTHERPAY DEDUCTION ARR-INT',
			'OTHERPAY DEDUCTION ARR-PRI'
		) or
		(
			led.RMK = 'PRETERMINATION INT' and
			led.[DATE] != isnull(pre.[DATE], '1900-01-01')
		)
	)
group by
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI),
	lon.PN_NO,
	lon.LOAN_TYPE,
	lar.SYSDATE,
	upper(left(datename(MM, lar.SYSDATE), 3)) + '-' + datename(YYYY, lar.SYSDATE)
	
insert into @ARREARS
(
	TOTAL,
	NAME,
	PN_NO,
	LOAN_TYPE,
	END_DATE,
	PAID,
	LABEL,
	AMOUNT
)
select
	1,
	'*** TOTAL ***',
	'',
	'',
	lab.REPORT_DATE,
	case right(lab.LABEL, 3) when 'PAY' then 'Y' else 'N' end,
	lab.LABEL,
	isnull(sum(arr.AMOUNT), 0)
from
	@labels lab
		left join
	@ARREARS arr on
		arr.LABEL = lab.LABEL
group by
	lab.REPORT_DATE,
	case right(lab.LABEL, 3) when 'PAY' then 'Y' else 'N' end,
	lab.LABEL

select
	@dateFrom DATE_FROM,
	@dateTo DATE_TO,
	NAME,
	PN_NO,
	LOAN_TYPE,
	END_DATE,
	PAID,
	LABEL,
	AMOUNT
from
	@ARREARS
order by
	TOTAL,
	NAME,
	PN_NO,
	LOAN_TYPE,
	END_DATE,
	PAID desc

GO
