USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_AmortizationSchedule]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_AmortizationSchedule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_AmortizationSchedule]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_AmortizationSchedule]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*****************************************************************************
MODIFIED:
JS 03/10/2014		Prevent infinite loop by adding @ttl
*****************************************************************************/

CREATE PROCEDURE [dbo].[Report_Loans_AmortizationSchedule]
@PN_NO VARCHAR(7)
AS

declare @amortization table 
(
	[DATE] date,
	[OUTS_BALANCE] numeric(12,4),
	[AMORT_PRIN] numeric(12,4),
	[AMORT_INT] numeric(12,4),
	[TOTAL_AMORT] numeric(12,4),
	[PRINCIPAL] numeric(12,4),
	[INTEREST] numeric(12,4),
	[TOTAL_PAYMENT] numeric(12,4)
)

declare @chkno_date as date
declare @rate as numeric(7,4) = 0
declare @term as numeric(3) = 0
declare @amort_amt as numeric(12,4) = 0
declare @first as bit = 1
declare @FULL_NAME varchar(100)
declare @LOAN_DESC varchar(100)

declare @xdate as date
declare @xbal as numeric(12,4) = 0
declare @xbaln as numeric(12,4) = 0
declare @ybaln as numeric(12,4) = 0
declare @tprin as numeric(12,4) = 0
declare @tint as numeric(12,4) = 0
declare @ttot as numeric(12,4) = 0
declare @xtprin as numeric(12,4) = 0
declare @xtint as numeric(12,4) = 0
declare @xint as numeric(12,4) = 0
declare @xpri as numeric(12,4) = 0
declare @jfreq as varchar(1)
declare @xfreq as numeric(2)
declare @xgomo as numeric(2)
declare @ttl as int = 120																							-- JS 03/10/2014

select	
	@xdate = lon.PAY_START,
	@xbaln = lon.PRINCIPAL,
	@jfreq = lon.FREQ,
	@chkno_date = lon.CHKNO_DATE,
	@rate = lon.RATE,
	@term = lon.TERM,
	@amort_amt = lon.AMORT_AMT,
	@xfreq = case lon.FREQ
		when 'M' then 12
		when 'S' then 6
		when 'Q' then 4
		when 'A' then 1
		end,
	@xgomo = case lon.FREQ
		when 'M' then 1
		when 'S' then 6
		when 'Q' then 3
		when 'A' then 12
		end,
	@FULL_NAME = dbo.func_Format241(mem.KBCI_NO) + ' ' + dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI),
	@LOAN_DESC = dbo.func_Format241(lon.PN_NO) + ' ' + isnull(p.LOAN_DESC, '')
from
	dbo.LOANS lon with(nolock)
		left join
	dbo.LOAN_TYPE p with(nolock) on
		p.LOAN_TYPE = lon.LOAN_TYPE
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO			
where
	PN_NO = @PN_NO;

insert into @amortization
(
	[DATE], 
	[OUTS_BALANCE]
)
values
(
	@chkno_date,
	@xbaln
)

while (@ttl >= 0 and (@first = 1 or @ybaln < @xbaln))																-- JS 03/10/2014
begin

	set @ttl = @ttl - 1																								-- JS 03/10/2014
	set @first = 0
	set @xbal = ROUND(@xbaln - @ybaln, 2)
	
	if @jfreq = 'D'
		set @xint = ROUND(@xbal * (@rate / 100) * (@term / 360), 2)
	else
		set @xint = ROUND(@xbal * (@rate / 100) * (1 / @xfreq), 2)

	set @xpri = ROUND(@amort_amt - @xint, 2)
	
	if @xpri > @xbal
	begin
		set @xpri = @xbal
		set @xint = 0
	end
	
	set @tprin = @tprin + @xpri
	set @tint = @tint + @xint
	set @ttot = @ttot + (@xpri - @xint)
	set @xtprin = @xtprin + @xpri
	set @xtint = @xtint + @xint
	set @ybaln = @ybaln + @xpri

	insert into @amortization 
	(
		[DATE],
		[OUTS_BALANCE],
		[AMORT_PRIN],
		[AMORT_INT],
		[TOTAL_AMORT],
		[PRINCIPAL],
		[INTEREST],
		[TOTAL_PAYMENT]
	)
	values
	(
		@xdate,
		@xbal-@xpri,
		@xpri,
		@xint,
		@xpri+@xint,
		@xtprin,
		@xtint,
		@xtprin+@xtint			
	)
	
	if @jfreq = 'D'
		set @xdate = DATEADD(dd, @term, @xdate)
	else
		set @xdate = DATEADD(mm, @xgomo, @xdate)

end

select
	[DATE] DATES,
	[OUTS_BALANCE],
	[AMORT_PRIN],
	[AMORT_INT],
	[TOTAL_AMORT],
	[PRINCIPAL],
	[INTEREST],
	[TOTAL_PAYMENT],
	@FULL_NAME FULL_NAME,
	UPPER(@LOAN_DESC) LOAN_DESC
from
	@amortization;




GO