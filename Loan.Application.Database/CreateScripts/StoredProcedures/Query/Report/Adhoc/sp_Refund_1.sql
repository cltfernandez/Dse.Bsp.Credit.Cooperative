USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Refund]    Script Date: 11/16/2013 18:27:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Adhoc_Refund]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Adhoc_Refund]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Refund]    Script Date: 11/16/2013 18:27:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[Report_Adhoc_Refund]
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

select
	ROW_NUMBER() OVER (ORDER BY case when datepart(YYYY, lon.CHKNO_DATE ) <= 2010 then 0 else 1 end, lon.LOAN_TYPE, mem.LNAME, mem.FNAME) ID,
	case
		when
			datepart(YYYY, lon.CHKNO_DATE ) <= 2010
		then
			0
		else
			1
		end DECEMBER_ANNIV,
	convert(varchar, @year) - datepart(YYYY, lon.CHKNO_DATE) [YEAR],
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
	prm.AMOUNT_PAID,
	lon.PRINCIPAL - prm.AMOUNT_PAID ANNIVERSARY_BALANCE,
	round((lon.PRINCIPAL - prm.AMOUNT_PAID) * prm.MULTIPLIER * 0.00046, 0) LRI_REFUND,
	prm.ANNIV_DATE,
	lon.CHG_DATE,
	prm.MULTIPLIER
from
(
	select
		lon.PN_NO,
		lon.ANNIV_DATE,
		12 - datediff(M, lon.ANNIV_DATE, lon.CHG_DATE) MULTIPLIER,
		sum
		(
			case
				when 
					led.ACCT_CODE = 'PRI' and 
					led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
					led.[DATE] <= lon.[ANNIV_DATE]
				then
					led.CR - led.DR
				else
					0
				end
		) AMOUNT_PAID
	from
		(
			select
				PN_NO,
				KBCI_NO,
				CHG_DATE,
				CHKNO_DATE,
				case
					when
						datepart(YYYY, CHKNO_DATE) > 2010
					then
						case					
							when
								convert(datetime, left(convert(varchar, CHKNO_DATE, 101), 6) + @yearC) <= CHG_DATE and
								datepart(M, CHKNO_DATE) < datepart(M, CHG_DATE)
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
				PD = 0 and
				LOAN_TYPE like '' + @loan_type + '' and
				LOAN_TYPE != 'STL' and
				LOAN_STAT = 'F' and
				datepart(MM,   CHG_DATE) = @month and
				datepart(YYYY, CHG_DATE) = @year
		) lon
			inner join
		dbo.MEMBERS mem with(nolock)
			on lon.KBCI_NO = mem.KBCI_NO
			inner join
		dbo.LEDGER led with(nolock)
			on lon.PN_NO = led.PN_NO
	group by
		lon.PN_NO,
		lon.ANNIV_DATE,
		12 - datediff(M, lon.ANNIV_DATE, lon.CHG_DATE)
) prm
		inner join
	dbo.LOANS lon with(nolock) on
		lon.PN_NO = prm.PN_NO
		inner join
	dbo.MEMBERS mem with(nolock) on
		mem.KBCI_NO = lon.KBCI_NO
where
	round((lon.PRINCIPAL - prm.AMOUNT_PAID) * prm.MULTIPLIER * 0.00046, 0) > 0
order by
	DECEMBER_ANNIV,
	lon.LOAN_TYPE,
	mem.LNAME, 
	mem.FNAME
GO

