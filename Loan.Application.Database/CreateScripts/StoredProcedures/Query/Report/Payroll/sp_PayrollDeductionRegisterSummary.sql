USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_PayrollDeductionRegisterSummary]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Payroll_PayrollDeductionRegisterSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Payroll_PayrollDeductionRegisterSummary]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_PayrollDeductionRegisterSummary]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**************************************************************************************************
JS	02/07/2014	Set refund to zero
**************************************************************************************************/


CREATE PROCEDURE [dbo].[Report_Payroll_PayrollDeductionRegisterSummary]
@payrollDate date = NULL,
@loanType varchar(3) = '%',
@code5 integer = 0,
@offcycle bit = 0
AS

declare @sysdate as datetime
declare @paydate as nvarchar(20)
declare @title as nvarchar(20)
declare @from as nvarchar(20)
declare @sql as nvarchar(2000)
declare @where as nvarchar(100)

create table #summary
(
	PD bit,
	PAYDATE varchar(20),
	TITLE varchar(20),
	LOAN_TYPE varchar(30),
	MONTHLY_DEDN numeric(11, 2),
	PRINCIPAL numeric(11, 2),
	INTEREST numeric(11, 2),
	PENALTIES numeric(11, 2),
	REFUND numeric(11, 2),
	FD_SA numeric(11, 2),
	MA numeric(11, 2),
	AP numeric(11, 2)
)

select	
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

if @payrollDate = '01/01/1900' set @payrollDate = NULL
if @loanType = '' set @loanType = NULL

if @payrollDate is null
begin
	set @from = 'MO_DEDN'
	set @title = 'CURRENT LOANS'
	set @paydate = LEFT(DATENAME(M, @sysdate), 3) + '. 07, ' + DATENAME(YYYY, @sysdate)
end
else
begin
	if @offcycle = 0
	begin
		set @from = 'MO_DEDNH'
		set @title = 'HISTORY LOANS'
	end
	else
	begin
		set @from = 'MO_DEDNO'
		set @title = 'CURRENT LOANS'
	end
	
	set @paydate = CONVERT(VARCHAR, @payrollDate, 107)
end

set @where = 'where md.LOAN_TYPE like ''' + @loanType + ''''

if @payrollDate is not null
	set @where = @where + ' and md.[DATE] = ''' + CONVERT(varchar, @payrollDate, 20) + ''''

if @code5 > 0
	set @where = @where + ' and md.CODE5 = ' + convert(varchar, @code5)

set @sql = 
	'
	select
		isnull(md.PD, 0) PD,
		''' + @paydate + ''' PAYDATE,
		''' + @title + ''' TITLE,
		isnull(lt.LOAN_DESC, ''Others'') as LOAN_TYPE,
		isnull(sum(DEDUCTION), 0) MONTHLY_DEDN,
		isnull(sum(PRINCIPAL), 0) + isnull(sum(ARR_PRI), 0) PRINCIPAL,
		isnull(sum(INTEREST), 0) + isnull(sum(ARR_INT), 0) INTEREST,
		isnull(sum(ARREARS), 0) PENALTIES,
		sum(case 
			when md.LOAN_TYPE in (''AIU'', ''SAV'') then 0					-- JS 02/07/2014
			when isnull(lt.LOAN_DESC, '''') = '''' then 0
			else ADVANCE
			end) REFUND,
		sum(case md.LOAN_TYPE
			when ''FIX'' then DEDUCTION
			when ''SAV'' then DEDUCTION
			else 0
			end) FD_SA,
		sum(case md.LOAN_TYPE
			when '' MA'' then DEDUCTION
			else 0
			end) MA,
		sum(case md.LOAN_TYPE
			when ''PAM'' then DEDUCTION
			when ''AIU'' then DEDUCTION
			when ''COC'' then DEDUCTION
			else 0
			end) AP
	from
		dbo.' + @from + ' md with(nolock)
			left join
		(
			select
				LOAN_TYPE,
				LOAN_DESC
			from
				dbo.LOAN_TYPE with(nolock)
			union all
			select
				OTHER_TYPE,
				OTHER_DESC
			from
				dbo.OTHER_TYPE with(nolock)
		) lt on lt.LOAN_TYPE = md.LOAN_TYPE
	' + @where + '
	group by
		isnull(md.PD, 0),
		md.LOAN_TYPE,
		lt.LOAN_DESC
	order by
		isnull(md.PD, 0),
		LOAN_TYPE
	'

insert #summary 
(
	PD,
	PAYDATE,
	TITLE,
	LOAN_TYPE,
	MONTHLY_DEDN,
	PRINCIPAL,
	INTEREST,
	PENALTIES,
	REFUND,
	FD_SA,
	MA,
	AP
)
exec sp_executesql @statement = @sql

select
	PD,
	PAYDATE,
	TITLE,
	LOAN_TYPE,
	MONTHLY_DEDN,
	PRINCIPAL,
	INTEREST,
	PENALTIES,
	REFUND,
	FD_SA,
	MA,
	AP
from
	#summary
order by
	PD,
	LOAN_TYPE



GO