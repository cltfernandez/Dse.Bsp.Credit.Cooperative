USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_KbciDeductionRegister]    Script Date: 07/11/2009 20:52:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_KbciDeductionRegister]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_KbciDeductionRegister]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_KbciDeductionRegister]    Script Date: 07/11/2009 20:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Loans_KbciDeductionRegister]
AS

declare @SYSDATE date
declare @DATE date
declare @loans table
(
	ID int identity(1,1),
	PN_NO varchar(7),
	XBPRIN numeric(13,4),
	XFREQ int,
	PAYDATE date
)

select
	@SYSDATE = SYSDATE
from
	dbo.CTRL with(nolock)

set @DATE = convert(date, convert(varchar(2), datepart(MM, @SYSDATE)) + '-07-' + convert(varchar(4), datepart(YYYY, @SYSDATE)))

insert into @loans
(
	PN_NO, 
	XBPRIN, 
	XFREQ
)
select	
	drv.PN_NO,
	lon.PRINCIPAL + drv.XBPRIN,
	case FREQ
		when 'M' then 12
		when 'S' then 6
		when 'Q' then 4
		when 'A' then 1
		end
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO
		inner join 
	(
		select
			lon.PN_NO,
			sum(case
				when led.ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ') then led.DR - led.CR
				else 0
				end) XBPRIN
		from
			dbo.LOANS lon with(nolock)
				inner join
			dbo.MEMBERS mem with(nolock) on
				lon.KBCI_NO = mem.KBCI_NO
				inner join
			dbo.LEDGER led with(nolock) on
				lon.PN_NO = led.PN_NO
		where
			lon.LOAN_TYPE != 'STL' and
			lon.LOAN_STAT = 'R' and
			mem.MEM_STAT = 'S' and
			(
				lon.PAY_START <= @SYSDATE or
				(
					lon.PAY_START > @SYSDATE and
					DATEPART(m, lon.PAY_START) = DATEPART(m, @SYSDATE)
				)
			)		
		group by
			lon.PN_NO,
			lon.PRINCIPAL
	) drv on lon.PN_NO = drv.PN_NO
order by
	isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.', ''),
	lon.PN_NO

update
	@loans
set
	PAYDATE = B.PAYDATE
from
	@loans A,
	(
		select	
			PN_NO,
			MAX(PAYDATE) PAYDATE
		from
			PAYHIST
		where
			PN_NO in 
			(
				select
					PN_NO
				from
					@loans
			)
		group by
			PN_NO
	) B
where
	A.PN_NO = B.PN_NO

select
	tmp.ID,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	lon.LOAN_TYPE,
	@DATE [DATES],
	case
		when datepart(D, @SYSDATE) = 7 then lon.I_BAL
		else round(lon.AMORT_AMT - round((tmp.XBPRIN * lon.RATE) / (100 * XFREQ), 2), 2)
		end XPRI,
	case
		when datepart(D, @SYSDATE) = 7 then lon.P_BAL
		else round((tmp.XBPRIN * lon.RATE) / (100 * XFREQ), 2)
		end XINT,
	case
		when datepart(D, @SYSDATE) = 7 then lon.I_BAL + lon.P_BAL
		else round(lon.AMORT_AMT - round((tmp.XBPRIN * lon.RATE) / (100 * XFREQ), 2), 2) + round((tmp.XBPRIN * lon.RATE) / (100 * XFREQ), 2)
		end TOTAL,
	dbo.func_Format241(lon.PN_NO) PN_NO,
	tmp.PAYDATE,
	upper(datename(M, @DATE)) MONTH_NAME
from
	dbo.LOANS lon with(nolock)
		inner join
	@loans tmp on
		lon.PN_NO = tmp.PN_NO
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO			
order by
	tmp.ID





GO