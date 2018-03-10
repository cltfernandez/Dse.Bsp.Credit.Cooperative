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
declare @xbprin numeric(12,4)
declare @xadvint numeric(12,4)
declare @xlastd date
declare @intpay numeric(12,4)
declare @xpint numeric(12,4)
declare @wvadv bit
declare	@xparr numeric(12,4)
declare	@xiarr numeric(12,4)
declare	@xpen numeric(12,4)
declare @xiouts numeric(12,4)
declare @xpouts numeric(12,4)
declare @xoouts numeric(12,4)
declare @xtouts numeric(12,4)
declare @xtfull numeric(12,4)
declare @xtpart numeric(12,4)

declare	@pay_start date
declare @ndue date
declare @date_due date
declare @rate numeric(10, 2)
declare @chkno_date date
declare @lri_due numeric(10, 2)
declare @arrear_as date

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

set @xbprin = 0
set @xadvint = 0
set @intpay = 0
set @xpint = 0
set @wvadv = 0
set @xparr = 0
set @xiarr = 0
set @xpen = 0
set @xiouts = 0
set @xpouts = 0
set @xtouts = 0
set @xtfull = 0
set @xtpart = 0

select
	@xbprin = sum
	(
		case 
			when acct_type in ('PAY', 'ADJ', 'AMT') and acct_code = 'PRI' then dr-cr
			else 0
			end
	),
	@xadvint = sum
	(
		case 
			when acct_type = 'INT' and acct_code = 'INT' then cr
			else 0
			end
	),
	@xlastd = max
	(
		case 
			when acct_type in ('PAY', 'ADJ') and acct_code = 'PRI' then [date]
			else ''
			end
	)
from
	dbo.ledger with (nolock)
where
 	pn_no = @vpn_no
group by
	pn_no

set @xbprin = isnull(@xbprin, 0)
set @xadvint = isnull(@xadvint, 0)

select
	@xparr = ARREAR_P,
	@xiarr = ARREAR_I,
	@xpen = ARREAR_OTH,
	@xpouts = P_BAL,
	@xiouts = I_BAL,
	@xoouts = O_BAL,
	@pay_start = PAY_START,
	@ndue = NDUE,
	@date_due = DATE_DUE,
	@rate = RATE,
	@lri_due = LRI_DUE,
	@chkno_date = CHKNO_DATE,
	@arrear_as = ARREAR_AS
from
	dbo.LOANS with(nolock)
where
	PN_NO = @vpn_no

if @xlastd = '1900-01-01' set @xlastd = null

select
	@intpay = sum(CR-DR)
from
	dbo.LEDGER with(nolock)
where
 	PN_NO = @vpn_no and
	ACCT_CODE = 'INT' and
	ACCT_TYPE in ('PAY', 'ADJ') and
	(
		(
			@xlastd is null and
			[DATE] >= @chkno_date
		) or
		(
			@xlastd is not null and
			[DATE] > @xlastd
		)
	)

set @intpay = isnull(@intpay, 0)

if @ndue = @pay_start
begin
	if isnull(@xlastd, '1900-01-01') = '1900-01-01'
	begin
		if @date_due <> @pay_start or @ndue <> @sysdate
		begin
			set @xpint = (@xbprin * (@rate/100/360) * datediff(dd, @chkno_date, @sysdate)) - @xadvint			
		end
		if @sysdate < @pay_start
			set @wvadv = 1
	end
	else
	begin
		set @xpint = @xbprin * (@rate/100/360) * datediff(dd, @xlastd, @sysdate)
		set @xiouts = 0
		set @xpouts = 0
	end
end
else
begin
	set @xtouts = @xparr + @xiarr + @xpen
	if @xtouts > 0
	begin
		set @xpint = 0
		set @intpay = 0
	end
	else
	begin
		if isnull(@xlastd, '1900-01-01') = '1900-01-01'
		begin
			set @xpint = (@xbprin * (@rate/100/360) * datediff(dd, @chkno_date, @sysdate)) - @xadvint
			if @sysdate = @ndue
			begin
				set @xiouts = round(@xiouts, 2)
				set @xpint = 0
			end			
		end
		else
			set @xpint = (@xbprin * (@rate/100/360) * datediff(dd, @xlastd, @sysdate))
	end	
end

set @xpint = round(@xpint - @intpay, 2)

if @wvadv = 1
	set @xtfull = @xbprin + @xpint + @xiarr + @xpen + @lri_due
else
	begin
	set @xadvint = 0
	if @xiarr + @xpen > 0
		set @xtfull = @xbprin + @xiarr + @xpen + @lri_due
	else
		set @xtfull = @xbprin + @xpint + @xiouts + @xiarr + @xpen + @lri_due
	end

set @xtpart = @xparr + @xiarr + @xpen + @xpouts + @xiouts

select
	top 1
	dbo.func_FullName(b.LNAME, b.FNAME, b.MI) FULL_NAME,
	dbo.func_Format241(a.KBCI_NO) KBCI_NO,
	b.FEBTC_SA,
	a.COLLATERAL,
	a.LOAN_TYPE,
	a.LOAN_STAT,
	dbo.func_Format241(a.PN_NO) PN_NO,
	a.DATE_GRANT,
	a.DATE_DUE,
	a.RATE,
	@xlastd XLASTD
	--a.ARREAR_AS,
	--a.ARREAR_P,
	--a.ARREAR_I,
	--a.ARREAR_OTH,
	--a.O_BAL,
	--@sysdate SYSDATE,
	--@xbprin XBPRIN,			--BALONPRIN
	--@xadvint XADVINT,
	--@intpay INTPAY,
	--@xpint XPINT,			--PRETINTEREST
	--@wvadv WVADV,
	--@xparr XPARR,			--PRINCIPAL
	--@xiarr XIARR,			--INTEREST
	--@xpen XPEN,				--PENALTY
	--@xiouts XIOUTS,			--INT
	--@xpouts XPOUTS,			--OUTSPRIN
	--@xtouts XTOUTS,
	--@xtfull XTFULL,			--FULLAMOUNT
	--@xtpart XTPART,
	--@lri_due LRI_DUE,
	--c.LRI_DUE_C
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

