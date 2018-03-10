USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Remit]    Script Date: 11/16/2013 18:28:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Adhoc_Remit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Adhoc_Remit]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Remit]    Script Date: 11/16/2013 18:28:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*****************************************************************************
MODIFIED:
JS 11/30/2013		NOT THE SAME MONTH FOR REFUND ONLY
*****************************************************************************/

create procedure [dbo].[Report_Adhoc_Remit]
@loan_type varchar(3),
@month int,
@year int
as

declare @yearC varchar(4)
declare @yearPrev int
declare @yearPrevC varchar(4)

set @yearC = convert(varchar, @year)
set @yearPrev = @year - 1 
set @yearPrevC = convert(varchar, @yearPrev)

declare @SYSDATE datetime

select
	@SYSDATE = max(SYSDATE)
from
	CTRL with(nolock)

declare @temp table
(
	[FLAG] bit,
	[YEAR] int,
	[LNAME] varchar(50),
	[FNAME] varchar(50),
	[MI] varchar(5),
	[B_DATE] datetime,
	[AGE] int,
	[CHKNO_DATE] datetime,
	[DATE_DUE] datetime,
	[PN_NO] varchar(10),
	[LOAN_TYPE] varchar(3),
	[PRINCIPAL] numeric(11, 2),
	[PROCEEDS] numeric(11, 2),
	[BALANCE] numeric(11, 2),
	[PREMIUM] numeric(11, 2)
)

if @month = 12
begin
		
	insert @temp
	(
		[FLAG],
		[YEAR],
		[LNAME],
		[FNAME],
		[MI],
		[B_DATE],
		[AGE],
		[CHKNO_DATE],
		[DATE_DUE],
		[PN_NO],
		[LOAN_TYPE],
		[PRINCIPAL],
		[PROCEEDS],
		[BALANCE],
		[PREMIUM]
	)
	select
		0,
		@year - datepart(YYYY, lon.CHKNO_DATE) [YEAR],
		mem.LNAME,
		mem.FNAME,
		mem.MI,
		mem.B_DATE,
		DATEDIFF(yyyy, mem.B_DATE, getdate()) - 1 + case when DATEPART(DAYOFYEAR, getdate()) >= DATEPART(DAYOFYEAR, mem.B_DATE) then 1 else 0 end AGE,
		lon.CHKNO_DATE,
		lon.DATE_DUE,
		prm.PN_NO,
		lon.LOAN_TYPE,
		lon.PRINCIPAL,
		lon.CHKNO_AMT,
		prm.BALANCE,
		convert(int, ROUND(prm.BALANCE * 12 * 0.46 * 0.001, 0)) LRI_PAYMENT
	from
	(
		select
			lon.PN_NO,
			lon.PRINCIPAL +
			sum
			(
				case
				when 
					led.ACCT_CODE = 'PRI' and 
					led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
					led.[DATE] <= convert(datetime, '12/31/' + @yearC)
				then
					led.DR - led.CR
				else
					0
				end
			) BALANCE
		from
			dbo.LOANS lon with(nolock)
				inner join
			dbo.MEMBERS mem with(nolock) on
				lon.KBCI_NO = mem.KBCI_NO
				inner join
			dbo.LEDGER led with(nolock) on
				lon.PN_NO = led.PN_NO
		where
			lon.LOAN_TYPE like @loan_type and
			lon.LOAN_TYPE != 'STL' and
			lon.LOAN_STAT = 'R' and
			mem.MEM_STAT != 'R' and
			DATEPART(YYYY, lon.CHKNO_DATE) <= 2010
		group by
			lon.PN_NO,
			lon.PRINCIPAL
	) prm
			inner join
		dbo.LOANS lon on
			lon.PN_NO = prm.PN_NO
			inner join
		dbo.MEMBERS mem on
			mem.KBCI_NO = lon.KBCI_NO
	
end

if @year > 2010
begin

	insert @temp
	(
		[FLAG],
		[YEAR],
		[LNAME],
		[FNAME],
		[MI],
		[B_DATE],
		[AGE],
		[CHKNO_DATE],
		[DATE_DUE],
		[PN_NO],
		[LOAN_TYPE],
		[PRINCIPAL],
		[PROCEEDS],
		[BALANCE],
		[PREMIUM]
	)
	select
		1,
		@year - datepart(YYYY, lon.CHKNO_DATE) [YEAR],
		mem.LNAME,
		mem.FNAME,
		mem.MI,
		mem.B_DATE,
		DATEDIFF(yyyy, mem.B_DATE, getdate()) - 1 + case when DATEPART(DAYOFYEAR, getdate()) >= DATEPART(DAYOFYEAR, mem.B_DATE) then 1 else 0 end AGE,
		lon.CHKNO_DATE,
		lon.DATE_DUE,
		prm.PN_NO,
		lon.LOAN_TYPE,
		lon.PRINCIPAL,
		lon.CHKNO_AMT,
		prm.BALANCE,
		convert(int, ROUND(prm.BALANCE * 12 * 0.46 * 0.001, 0)) LRI_PAYMENT
	from
	(
		select
			lon.PN_NO,
			lon.PRINCIPAL +
			sum
			(
				case
				when 
					led.ACCT_CODE = 'PRI' and 
					led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
					led.[DATE] <= lon.ANNIV_DATE
				then
					led.DR - led.CR
				else
					0
				end
			) BALANCE
		from
			(
				select
					PN_NO,
					PRINCIPAL,
					KBCI_NO,
					case
						when
							datepart(YYYY, CHKNO_DATE) > 2010
						then
							case					
								when
									convert(datetime, left(convert(varchar, CHKNO_DATE, 101), 6) + @yearC) <= @SYSDATE --and				-- JS 11/30/2013
									--datepart(M, CHKNO_DATE) < datepart(M, @SYSDATE)														-- JS 11/30/2013
								then
									convert(datetime, left(convert(varchar, CHKNO_DATE, 101), 6) + @yearC)
								else
									convert(datetime, left(convert(varchar, CHKNO_DATE, 101), 6) + @yearPrevC)
								end 
						else
							convert(datetime, '12/31/' + @yearPrevC)
						end ANNIV_DATE
				from
					dbo.LOANS with(nolock)
				where
					LOAN_TYPE like @loan_type and
					LOAN_TYPE != 'STL' and
					LOAN_STAT = 'R' and
					DATEPART(M, CHKNO_DATE) = @month and
					DATEPART(YYYY, CHKNO_DATE) > 2010
			) lon
				inner join
			dbo.MEMBERS mem
				on lon.KBCI_NO = mem.KBCI_NO
				inner join
			dbo.LEDGER led
				on lon.PN_NO = led.PN_NO
		group by
			lon.PN_NO,
			lon.PRINCIPAL
	) prm
			inner join
		dbo.LOANS lon with(nolock) on
			lon.PN_NO = prm.PN_NO
			inner join
		dbo.MEMBERS mem with(nolock) on
			mem.KBCI_NO = lon.KBCI_NO

end

select
	ROW_NUMBER() OVER(order by FLAG, LOAN_TYPE, LNAME, FNAME) ID,
	*
from
	@temp
order by
	FLAG,
	LOAN_TYPE,
	LNAME,
	FNAME
GO

