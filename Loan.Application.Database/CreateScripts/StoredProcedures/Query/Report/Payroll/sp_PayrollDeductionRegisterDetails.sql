USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_PayrollDeductionRegisterDetails]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Payroll_PayrollDeductionRegisterDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Payroll_PayrollDeductionRegisterDetails]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_PayrollDeductionRegisterDetails]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*****************************************************************************
MODIFIED:
JS 06/09/2012		FORMATTED PN_NO
*****************************************************************************/

CREATE PROCEDURE [dbo].[Report_Payroll_PayrollDeductionRegisterDetails]
@payrollDate date = null,
@loanType varchar(3) = '%',
@code5 integer = 0,
@offcycle as bit = 0
AS

declare @sysdate as date
declare @paydate nvarchar(30)
declare @title nvarchar(20)
declare @sql nvarchar(2000)
declare @from nvarchar(20)
declare @where nvarchar(1000)

create table #details
(
	PD bit,
	PAYDATE varchar(20),
	TITLE varchar(20),
	LOAN_TYPE varchar(30),
	LNAME char(1),
	KBCI_NO varchar(10),
	EMPNAME varchar(45),
	DEDUCTION numeric(11, 2),
	PN_NO varchar(10),
	PRINCIPAL numeric(11, 2),
	INTEREST numeric(11, 2),
	ARR_PRI numeric(11, 2),
	ARR_INT numeric(11, 2),
	ARREARS numeric(11, 2),
	ADVANCE numeric(11, 2),
	EMPNO varchar(6),
	RANKING int
)

select	@sysdate = SYSDATE
from	dbo.CTRL

if @payrollDate = '01/01/1900' set @payrollDate = NULL
if @loanType = '' set @loanType = NULL

if @payrollDate is null
begin
	set @from  = 'dbo.MO_DEDN'
	set @title = 'CURRENT LOANS'
	set @paydate = left(datename(M, @sysdate), 3) + '.07, ' + datename(YYYY, @sysdate)
end
else
begin
	if @offcycle = 0
	begin
		set @from  = 'dbo.MO_DEDNH'
		set @title = 'HISTORY LOANS'
	end
	else
	begin
		set @from  = 'dbo.MO_DEDNO'
		set @title = 'CURRENT LOANS'
	end
	
	set @paydate = convert(varchar, @payrollDate, 107)
end

set @where = 'where dedn.LOAN_TYPE like ''' + @loanType + ''''

if @payrollDate is not NULL
	set @where = @where + ' and dedn.[DATE] = ''' + CONVERT(varchar, @payrollDate, 20) + ''''

if @code5 > 0
	set @where = @where + ' and dedn.CODE5 = ' + convert(varchar, @code5)

set	@sql = 
	'
	select	
		isnull(PD, 0) PD,
		''' + @paydate + ''' PAYDATE,
		''' + @title + ''' TITLE,
		dedn.LOAN_TYPE,
		left(ltrim(dedn.NAME), 1) LNAME,
		dbo.func_Format241(dedn.KBCI_NO) KBCI_NO,
		dedn.[NAME] EMPNAME,
		dedn.DEDUCTION,
		dbo.func_Format241(dedn.PN_NO) PN_NO,			-- JS 06/09/2012
		dedn.PRINCIPAL,
		dedn.INTEREST,
		dedn.ARR_PRI,
		dedn.ARR_INT,
		dedn.ARREARS,
		dedn.ADVANCE,
		dedn.EMPNO,
		case
			when isnull(lt.loan_type, ''XXX'') = ''XXX'' then
				1
			else
				2
			end ranking
	from
		' + @from + ' dedn with(nolock)
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
		) lt on
				lt.LOAN_TYPE = dedn.LOAN_TYPE
	' + @where + '
	order by
		ranking,
		dedn.LOAN_TYPE,
		dedn.PD,
		dedn.[NAME]
	'

insert	#details
(
	PD,
	PAYDATE,
	TITLE,
	LOAN_TYPE,
	LNAME,
	KBCI_NO,
	EMPNAME,
	DEDUCTION,
	PN_NO,
	PRINCIPAL,
	INTEREST,
	ARR_PRI,
	ARR_INT,
	ARREARS,
	ADVANCE,
	EMPNO,
	RANKING
)
exec sp_executesql @statement = @sql

select	
	PD,
	PAYDATE,
	TITLE,
	LOAN_TYPE,
	LNAME,
	KBCI_NO,
	EMPNAME,
	DEDUCTION,
	PN_NO,
	PRINCIPAL,
	INTEREST,
	ARR_PRI,
	ARR_INT,
	ARREARS,
	ADVANCE,
	EMPNO,
	RANKING
from
	#details
order by
	RANKING,
	LOAN_TYPE,
	PD,
	EMPNAME



GO