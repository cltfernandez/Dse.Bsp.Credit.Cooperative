USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_ExtractInterest]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Admin_ExtractInterest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Admin_ExtractInterest]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_ExtractInterest]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Do_Admin_ExtractInterest]
@dtDate1 date,
@dtDate2 date
AS

declare	@LEDGER_ID bigint
declare @PN_NO varchar(7) = ''
declare @PN_NO_PREV varchar(7) = ''
declare @DR numeric(9, 2)
declare @CR numeric(9, 2)
declare @BEGBAL numeric(11, 2) = 0
declare @ENDBAL numeric(11, 2) = 0
declare @ENDBAL_PREV numeric(11, 2) = 0
declare @MOYEAR varchar(6)
declare @MOYEAR_PREV varchar(6)

truncate table dbo.XLEDGER

insert	dbo.XLEDGER (
		[KBCI_NO],
		[LOAN_TYPE],
		[PN_NO],
		[DATE],
		[DOX_TYPE],
		[REF],
		[ACCT_TYPE],
		[ACCT_CODE],
		[BEGBAL],
		[DR],
		[CR],
		[ENDBAL],
		[RMK],
		[ADD_DATE],
		[MOYEAR]
		)
select	lon.[KBCI_NO],
		lon.[LOAN_TYPE],
		led.[PN_NO],
		led.[DATE],
		led.[DOX_TYPE],
		led.[REF],
		led.[ACCT_TYPE],
		led.[ACCT_CODE],
		led.[BEGBAL],
		led.[DR],
		led.[CR],
		led.[ENDBAL],
		led.[RMK],
		led.[ADD_DATE],
		right('00' + convert(varchar(2), DATEPART(MM, led.ADD_DATE)), 2) + 
		right('0000' + convert(varchar(4), DATEPART(YYYY, led.ADD_DATE)), 4)
from	dbo.LOANS lon
			inner join
		dbo.LEDGER led
			on lon.PN_NO = led.PN_NO
where	led.[ADD_DATE] between @dtDate1 and @dtDate2 and
		led.ACCT_CODE = 'INT' and
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'INT')
order
by		led.LEDGER_ID

insert	dbo.XPMINT (
		MOYEAR,
		INTAMT
		)
select	MOYEAR,
		sum(ISNULL(DR, 0) - ISNULL(CR, 0))
from	dbo.XLEDGER
where	MOYEAR not in (
		select	MOYEAR
		from	dbo.XPMINT
		)
group
by		MOYEAR

-- 

declare LEDGER_CURSOR cursor for
select	XLEDGER_ID,
		PN_NO,
		DR,
		CR,
		MOYEAR
from	dbo.XLEDGER
where	PN_NO is not NULL
order
by		MOYEAR, PN_NO, XLEDGER_ID

open LEDGER_CURSOR

fetch	LEDGER_CURSOR
into	@LEDGER_ID,
		@PN_NO,
		@DR,
		@CR,
		@MOYEAR
		
set @PN_NO_PREV  = ''
set @ENDBAL_PREV = 0
set @MOYEAR_PREV = ''
		
while @@FETCH_STATUS = 0
begin

	if @PN_NO = @PN_NO_PREV AND @MOYEAR = @MOYEAR_PREV
	begin
		set @BEGBAL = @ENDBAL_PREV
		set @ENDBAL = @BEGBAL + ISNULL(@CR, 0) - ISNULL(@DR, 0)
	end
	else
	begin
		set @BEGBAL = 0		
		set @ENDBAL = ISNULL(@CR, 0) - ISNULL(@DR, 0)		
	end
	
	update	dbo.XLEDGER
	set		BEGBAL = @BEGBAL,
			ENDBAL = @ENDBAL
	where	XLEDGER_ID = @LEDGER_ID
	
	set @PN_NO_PREV  = @PN_NO
	set @ENDBAL_PREV = @ENDBAL
	set @MOYEAR_PREV = @MOYEAR
	
	fetch	LEDGER_CURSOR
	into	@LEDGER_ID,
			@PN_NO,
			@DR,
			@CR,
			@MOYEAR
end

close LEDGER_CURSOR
deallocate LEDGER_CURSOR




GO