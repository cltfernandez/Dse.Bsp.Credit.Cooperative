USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_DailyReversion]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Admin_DailyReversion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Admin_DailyReversion]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_DailyReversion]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Do_Admin_DailyReversion]
AS

declare @sysdate datetime

select
	@sysdate = SYSDATE
from
	dbo.CTRL

truncate table dbo.DAILYREV

insert dbo.DAILYREV 
(
	PN_NO,
	KBCI_NO,
	LOAN_TYPE,
	[NAME],
	BEGBAL,
	ENDBAL,
	CR,
	DR,
	[INT],
	SC,
	LRI,
	PEN
)
select
	lon.PN_NO,
	lon.KBCI_NO,
	lon.LOAN_TYPE,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) MEMBER,
	sum
	(
		case
		when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') then led.BEGBAL
		else 0
		end
	) BEGBAL,
	sum
	(
		case
		when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') then led.ENDBAL
		else 0
		end
	) ENDBAL,
	sum
	(
		case
		when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') then isnull(led.DR, 0)
		else 0
		end
	) CR,
	sum
	(
		case
		when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') then isnull(led.CR, 0)
		else 0
		end
	) DR,
	sum
	(
		case
		when led.ACCT_CODE = 'INT' then isnull(led.DR, 0) - isnull(led.CR, 0)
		else 0
		end
	) [INT],
	sum
	(
		case
		when led.ACCT_CODE = 'SC' then isnull(led.DR, 0) - isnull(led.CR, 0)
		else 0
		end
	) [SC],
	sum
	(
		case
		when led.ACCT_CODE = 'LRI' then isnull(led.DR, 0) - isnull(led.CR, 0)
		else 0
		end
	) [LRI],
	sum
	(
		case
		when led.ACCT_CODE = 'OTH' and led.ACCT_TYPE = 'PAY' then isnull(led.DR, 0) - isnull(led.CR, 0)
		when led.ACCT_CODE = 'PEN' and led.ACCT_TYPE = 'TER' then isnull(led.DR, 0) - isnull(led.CR, 0)
		else 0
		end
	) [PEN]
from
	dbo.LOANS lon
		inner join
	dbo.MEMBERS mem
		on lon.KBCI_NO = mem.KBCI_NO
		inner join
	dbo.LEDGEREV led
		on lon.PN_NO = led.PN_NO
where
	led.[DATE] = @sysdate
group by
	lon.PN_NO,
	lon.KBCI_NO,
	lon.LOAN_TYPE,
	mem.LNAME,
	mem.FNAME,
	mem.MI



GO