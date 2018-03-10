USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_CashDividend_Register]    Script Date: 07/11/2009 20:52:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_CashDividend_Register]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_CashDividend_Register]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_CashDividend_Register]    Script Date: 07/11/2009 20:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Report_CashDividend_Register]
@order integer,
@region varchar(20)
AS

declare @orderBy nvarchar(20)
declare @percent numeric(6,4)
declare @sql nvarchar(1000)

select
	top 1
	@percent = [PERCENT]
from
	dbo.CASHDIV with(nolock)

set @orderBy =
(
	case @order
		when 1 then 'mem.KBCI_NO'
		when 2 then 'mem.REGION, FULL_NAME'
		when 3 then 'mem.FULL_NAME'
		end
)

set @sql =
'
	select
		mem.KBCI_NO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
		DIV.[DATE] DATES,
		div.FD_AMT FD_AMOUNT,
		div.FD_AMT * (' + convert(varchar, @percent) + '/100) DIVIDEND,
		div.DEDNS DEDUCTIONS,
		div.DIV_AMT NET_DIVIDEND,
		''' + convert(nvarchar, @order) + ''' [ORDER],
		''' + isnull(@region, '') + ''' REGIONS
	from
		dbo.DIV div with(nolock)
			inner join
		dbo.MEMBERS mem with(nolock)
			on div.KBCI_NO = mem.KBCI_NO
	order by
		' + @orderBy + '
'

exec sp_executesql @sql



GO