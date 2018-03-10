USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_ReleasedLoans]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_ReleasedLoans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_ReleasedLoans]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_ReleasedLoans]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Admin_ReleasedLoans]
AS

declare @sysdate datetime

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)
	
select
	dbo.func_Format241(lon.PN_NO) PN_NO,
	dbo.func_Format241(lon.KBCI_NO) KBCI_NO,
	lon.LOAN_TYPE,
	lon.PRINCIPAL,
	lon.CHKNO_AMT,
	case lon.MOD_PAY
		when '1' then 'PAYROLL'
		when '2' then 'PDC'
		when '3' then 'DM'
		end MOD_PAY,
	lon.TERM,
	lon.FREQ,
	lon.RATE,
	lon.PAY_START,
	lon.DATE_DUE,
	lon.CHKNO_DATE,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) MEMBER,
	@sysdate SYSDATE
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO			
where
	lon.CHKNO_DATE = (select top 1 SYSDATE from dbo.CTRL)		
order by
	lon.LOAN_TYPE, MEMBER



GO