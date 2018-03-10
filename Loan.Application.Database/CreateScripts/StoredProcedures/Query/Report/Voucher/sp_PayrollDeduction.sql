USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Voucher_PayrollDeduction]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Voucher_PayrollDeduction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Voucher_PayrollDeduction]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Voucher_PayrollDeduction]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Voucher_PayrollDeduction]
@payrollDate date,
@my_user varchar(8),
@sysdate date,
@offcycle bit = 0
AS

declare @vno as varchar(7)
declare @vno_txt as varchar(35)
declare @vno_tot as numeric(11, 2)
declare @payrollChar as nvarchar(10)
declare @sql as nvarchar(max)
declare @from as nvarchar(8)

if @payrollDate is not null
	set @payrollChar = convert(varchar, @payrollDate, 101)

if @offcycle = 0
begin
	set @from = 'MO_DEDNH'
	
	select
		top 1
		@vno = PN_NO,
		@vno_txt = [NAME],
		@vno_tot = DEDUCTION
	from
		MO_DEDNH with(nolock)
	where
		[DATE] = @payrollDate and
		ltrim(rtrim(LOAN_TYPE)) = 'XXX'

end
else
begin
	set @from = 'MO_DEDNO'
	
	select
		top 1
		@vno = HEDRCVNO,
		@vno_txt = HEDRS1,
		@vno_tot = HEDRA1
	from
		OTHHEDR with(nolock)
	where
		HEDRDATE = @payrollDate
end
		
declare @voucher table
(
	ROWID int identity(1,1),
	DETAIL varchar(100),
	VALUE numeric(18,4)
)

/******* PRINCIPALS *******/

set @sql =
'
select
	upper(lt.LOAN_DESC),
	sum(isnull(PRINCIPAL, 0) + isnull(ARR_PRI, 0))
from
	dbo.' + @from + ' dedn with(nolock)
		inner join
	dbo.LOAN_TYPE lt with(nolock) on
		lt.LOAN_TYPE = dedn.LOAN_TYPE
where
	[DATE] = ''' + @payrollChar + ''' and
	PD = 0
group by
	upper(lt.LOAN_DESC)
order by
	upper(lt.LOAN_DESC)
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

/******* PRINCIPALS - PAST DUE *******/

set @sql =
'
select
	''PAST DUE - '' + upper(lt.LOAN_TYPE),
	sum(isnull(PRINCIPAL, 0) + isnull(ARR_PRI, 0))
from
	dbo.' + @from + ' dedn with(nolock)
		inner join
	dbo.LOAN_TYPE lt with(nolock) on
		lt.LOAN_TYPE = dedn.LOAN_TYPE
where
	[DATE] = ''' + @payrollChar + ''' and
	PD = 1
group by
	''PAST DUE - '' + upper(lt.LOAN_TYPE)
order by
	''PAST DUE - '' + upper(lt.LOAN_TYPE)
'

insert	@voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

/******* INTEREST *******/

set @sql =
'
select
	''INTEREST ON LOANS'',
	sum
	(
		case
			when isnull(lt.LOAN_TYPE, ''XXX'') != ''XXX'' 
				then isnull(INTEREST, 0) + isnull(ARR_INT, 0)
			when ltrim(rtrim(dedn.LOAN_TYPE)) in (''IRG'', ''IED'', ''IAP'', ''ISP'', ''IEM'')
				then isnull(DEDUCTION, 0)
			else 
				0
			end
	)
from
	dbo.' + @from + ' dedn with(nolock)
		left join
	dbo.LOAN_TYPE lt with(nolock) on
		lt.LOAN_TYPE = dedn.LOAN_TYPE
where
	[DATE] = ''' + @payrollChar + ''' and
	(
		isnull(lt.LOAN_TYPE, ''XXX'') != ''XXX'' or
		ltrim(rtrim(dedn.LOAN_TYPE)) in (''IRG'', ''IED'', ''IAP'', ''ISP'', ''IEM'')
	)
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

/******* PENALTY *******/

set @sql = 
'
select
	''PENALTY ON LOANS'',
	sum
	(
		case
			when isnull(lt.LOAN_TYPE, ''XXX'') != ''XXX'' 
				then isnull(ARREARS, 0)
			when ltrim(rtrim(dedn.LOAN_TYPE)) in (''PEM'', ''PER'')
				then isnull(DEDUCTION, 0)
			else
				0
			end
	)
from
	dbo.' + @from + ' dedn with(nolock)
		left join
	dbo.LOAN_TYPE lt with(nolock) on
		lt.LOAN_TYPE = dedn.LOAN_TYPE
where
	[DATE] = ''' + @payrollChar + ''' and
	(
		isnull(lt.LOAN_TYPE, ''XXX'') != ''XXX'' or
		ltrim(rtrim(dedn.LOAN_TYPE)) in (''PEM'', ''PER'')
	)
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

/******* FIXED DEPOSIT *******/

set @sql =
'
select
	''FIXED DEPOSIT'',
	sum(isnull(DEDUCTION, 0))
from
	dbo.' + @from + ' with(nolock)
where
	[DATE] = ''' + @payrollChar + ''' and
	ltrim(rtrim(LOAN_TYPE)) = ''FIX''
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

/******* SAVINGS DEPOSIT *******/

set @sql = 
'
select
	''SAVINGS DEPOSIT'',
	sum
	(
		case
			when isnull(lt.LOAN_TYPE, ''XXX'') != ''XXX'' 
				then isnull(ADVANCE, 0)
			when ltrim(rtrim(dedn.LOAN_TYPE)) = ''SAV''
				then isnull(DEDUCTION, 0)
			else
				0
			end
	)
from
	dbo.' + @from + ' dedn with(nolock)
		left join
	dbo.LOAN_TYPE lt with(nolock) on
		lt.LOAN_TYPE = dedn.LOAN_TYPE
where
	[DATE] = ''' + @payrollChar + ''' and
	(
		isnull(lt.LOAN_TYPE, ''XXX'') != ''XXX'' or
		ltrim(rtrim(dedn.LOAN_TYPE)) = ''SAV''
	)
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql
		
/******* MUTUAL AID *******/

set @sql =
'
select
	''MUTUAL AID'',
	sum(isnull(DEDUCTION, 0))
from
	dbo.' + @from + ' with(nolock)
where
	[DATE] = ''' + @payrollChar + ''' and
	ltrim(rtrim(LOAN_TYPE)) = ''MA''
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

/******* SERVICE CHARGE *******/

set @sql =
'
select
	''SERVICE CHARGE'',
	sum(isnull(DEDUCTION, 0))
from
	dbo.' + @from + ' with(nolock)
where
	[DATE] = ''' + @payrollChar + ''' and
	ltrim(rtrim(LOAN_TYPE)) = ''SER''
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

/******* ACCOUNTS PAYABLE *******/

set @sql =
'
select
	''ACCOUNTS PAYABLE'',
	sum(isnull(DEDUCTION, 0))
from
	dbo.' + @from + ' with(nolock)
where
	[DATE] = ''' + @payrollChar + ''' and
	ltrim(rtrim(LOAN_TYPE)) in (''PAM'', ''AIU'', ''COC'')
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

/******* OTHERS *******/

set @sql =
'
select
	''OTHERS'',
	sum
	(
		case
			when isnull(lt.LOAN_TYPE, ''XXX'') = ''XXX'' and ltrim(rtrim(dedn.LOAN_TYPE)) not in (''FIX'', ''SAV'', ''IRG'', ''IED'', ''IAP'', ''ISP'', ''IEM'', ''PEM'', ''PER'', ''MA'', ''SER'', ''PAM'', ''AIU'', ''COC'', ''XXX'')
				then isnull(DEDUCTION, 0)
			else 
				0
			end
	)
from
	dbo.' + @from + ' dedn with(nolock)
		left join
	dbo.LOAN_TYPE lt with(nolock) on
		lt.LOAN_TYPE = dedn.LOAN_TYPE
where
	[DATE] = ''' + @payrollChar + '''
'

insert @voucher
(
	DETAIL,
	VALUE
)
exec sp_executesql @statement = @sql

select
	@my_user MY_USER,
	@sysdate SYSDATE,
	@vno VNO,
	@vno_txt VNO_TXT,
	@vno_tot VNO_TOT,
	@payrollDate PAYROLLDATE,
	DETAIL,
	VALUE
from
	@voucher
where
	isnull(VALUE, 0) > 0
order by
	rowid



GO