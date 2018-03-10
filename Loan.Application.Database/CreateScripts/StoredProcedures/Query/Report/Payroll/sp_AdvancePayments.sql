USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_AdvancePayments]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Payroll_AdvancePayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Payroll_AdvancePayments]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_AdvancePayments]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Payroll_AdvancePayments]
@dateFrom date,
@dateTo date,
@x1 integer
/*
	1 - ADVANCE
	2 - EXTRACT 
*/
as

if @x1 is null
	set @x1 = 1

declare @sysdate date
declare @sql nvarchar(1000)

declare @advance table
(
	PD bit,
	PN_NO varchar(10),
	KBCI_NO varchar(10),
	LOAN_TYPE varchar(3),
	ADD_DATE datetime,
	CHG_DATE datetime,
	[USER] varchar(8),
	AMOUNT numeric(11, 2),
	REMARKS varchar(60),
	SAVINGS bit,
	ACCTNO varchar(12),
	[STATUS] varchar(20),
	MEMBER varchar(50)
)

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

set @sql =
	'
	select
		case
			when left(ltrim(a.REMARKS), 3) = ''PD-'' then 1
			else 0
			end PD,
		dbo.func_Format241(a.KBCI_NO) KBCI_NO,
		dbo.func_Format241(a.PN_NO) PN_NO,
		a.LOAN_TYPE,				
		a.ADD_DATE,
		a.CHG_DATE,
		a.[USER],
		a.AMOUNT,
		a.REMARKS,
		0,
		a.ACCTNO,
		a.[STATUS],
		dbo.func_FullName(b.LNAME, b.FNAME, b.MI) MEMBER
	from
		dbo.ADVANCE a with(nolock)
			inner join
		dbo.MEMBERS b with(nolock)
			on a.KBCI_NO = b.KBCI_NO
			inner join
		dbo.LOAN_TYPE lt with(nolock)
			on a.LOAN_TYPE = lt.LOAN_TYPE
	where
		a.ADD_DATE between ''' + CONVERT(varchar, @dateFrom, 101) + ''' and ''' + CONVERT(varchar, @dateTo, 101) + '''
	order by
		a.ADD_DATE,
		a.LOAN_TYPE,
		MEMBER
	'

insert @advance
(
	PD,
	PN_NO,
	KBCI_NO,
	LOAN_TYPE,
	ADD_DATE,
	CHG_DATE,
	[USER],
	AMOUNT,
	REMARKS,
	SAVINGS,
	ACCTNO,
	[STATUS],
	MEMBER
)
exec sp_executesql @statement = @sql

IF @x1 = 2
begin
	truncate table EXTRACT

	insert into EXTRACT
	(
		[PN_NO],
		[KBCI_NO],
		[LOAN_TYPE],
		[ADD_DATE],
		[CHG_DATE],
		[USER],
		[AMOUNT],
		[REMARKS],
		[SAVINGS],
		[ACCTNO],
		[STATUS]
	)
	select
		adv.[PN_NO],
		adv.[KBCI_NO],
		adv.[LOAN_TYPE],
		adv.[ADD_DATE],
		adv.[CHG_DATE],
		adv.[USER],
		adv.[AMOUNT],
		adv.[REMARKS],
		adv.[SAVINGS],
		adv.[ACCTNO],
		'1'
	from
		@advance adv
			inner join
		dbo.LOAN_TYPE typ with(nolock) on
			typ.LOAN_TYPE = adv.LOAN_TYPE
	where
		isnull(adv.[STATUS], '1') != '2'
	
	update
		ADVANCE
	set
		[STATUS] = '1'
	where
		ADD_DATE between @dateFrom and @dateTo and
		LOAN_TYPE in (select LOAN_TYPE from dbo.LOAN_TYPE with(nolock))
		
	update
		@advance
	set
		[STATUS] = '1'
	
end

select
	PD,
	@dateFrom DATE_FROM,
	@dateTo DATE_TO,
	@sysdate SYSDATE,
	KBCI_NO,
	PN_NO,
	LOAN_TYPE,
	MEMBER,
	AMOUNT,
	ADD_DATE,
	REMARKS,
	ACCTNO,
	case [STATUS]
		when '1' then 'EXTRACTED'
		when '2' then 'POSTED'
		end [STATUS]
from
	@advance
order by
	PD,
	ADD_DATE,
	LOAN_TYPE,
	MEMBER



GO


