USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_PaymentOrder_Loans]    Script Date: 07/03/2009 14:33:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_PaymentOrder_Loans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_PaymentOrder_Loans]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_PaymentOrder_Loans]    Script Date: 07/03/2009 14:33:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_PaymentOrder_Loans]
@vPN_NO VARCHAR(7)
as

declare @sysdate date
declare @arrear_as date
declare @lri_due numeric(12,4)

declare @xbprin numeric(12,4)
declare @xadvint numeric(12,4)
declare @xlastd date
declare @xpint numeric(12,4)
declare	@xparr numeric(12,4)
declare	@xiarr numeric(12,4)
declare	@xpen numeric(12,4)
declare @xiouts numeric(12,4)
declare @xpouts numeric(12,4)
declare @xoouts numeric(12,4)
declare @xtfull numeric(12,4)
declare @xtpart numeric(12,4)

select
	@arrear_as = arrear_as,
	@lri_due = lri_due,
	@xbprin = xbprin,
	@xadvint = xadvint,
	@xlastd = xlastd,
	@xpint = xpint,
	@xparr = xparr,
	@xiarr = xiarr,
	@xpen = xpen,
	@xiouts = xiouts,
	@xpouts = xpouts,
	@xoouts = xoouts,
	@xtfull = xtfull,
	@xtpart = xtpart
from
	dbo.func_PaymentOrder('LOANS', @vPN_NO, 0)

declare @body table
(
	ID int identity(1, 1),
	DETAIL varchar(50),
	AMOUNT numeric(12, 4),
	CODE char(1)
	/*
		H Sub-Header (no bottom border)
		D Detail
		S Space
	*/
)

select
	top 1
	@sysdate = sysdate
from
	dbo.CTRL with (nolock)

select
	top 1
	dbo.func_FullName(b.LNAME, b.FNAME, b.MI) FULL_NAME,
	dbo.func_Format241(a.KBCI_NO) KBCI_NO,
	b.FEBTC_SA,
	a.COLLATERAL,
	case
		when a.PD = 1 then 'Past Due - '
		else ''
		end + a.LOAN_TYPE LOAN_TYPE,
	a.LOAN_STAT,
	dbo.func_Format241(a.PN_NO) PN_NO,
	a.DATE_GRANT,
	a.DATE_DUE,
	a.RATE,
	isnull(@xlastd, '1900-01-01') XLASTD
from
	dbo.LOANS a with(nolock)
		inner join
	dbo.MEMBERS b with(nolock) on 
		a.KBCI_NO = b.KBCI_NO
		left join
	dbo.LRIDUE c with(nolock) on
		c.PN_NO = a.PN_NO
where
	a.PN_NO = @vPN_NO

insert into @body
	(CODE, DETAIL, AMOUNT)
values
	('H', 'OUTSTANDING', 0),
	('D', 'Balance on Principal', @xbprin),
	('D', 'Preterm Interest', @xpint),
	('D', 'Advance Interest', @xadvint),
	('S', '', 0),
	('H', 'MONTHLY AMORTIZATION', 0),
	('D', 'Principal', @xpouts),
	('D', 'Interest', @xiouts),
	('D', 'Penalties', @xoouts),
	('S', '', 0),
	('H', 'ARREARS AS OF ' + isnull(convert(varchar, @arrear_as, 101), '--/--/----'), NULL),
	('D', 'Principal', @xparr),
	('D', 'Interest', @xiarr),
	('D', 'Penalties', @xpen),
	('S', '', 0),
	('H', 'TOTALS AS OF ' + convert(varchar, @sysdate, 101), NULL),
	('D', 'Full Payment', @xtfull),
	('D', 'Partial Payment', @xtpart),
	('D', 'LRI Preterm', @lri_due)

select
	CODE,
	DETAIL,
	AMOUNT
from
	@body
order by
	ID

GO

