USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_DailyTransactionRegisterSummary]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_DailyTransactionRegisterSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_DailyTransactionRegisterSummary]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_DailyTransactionRegisterSummary]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Admin_DailyTransactionRegisterSummary]
AS

declare @SYSDATE datetime

select
	@SYSDATE = SYSDATE
from
	dbo.CTRL with(nolock)

select
	@SYSDATE SYSDATE,
	isnull(lon.PD, 0) PD,
	lt.LOAN_TYPE,
	sum(isnull(dt.DR, 0)) DR,
	sum(isnull(dt.CR, 0)) CR,
	sum(isnull(dt.INTDR, 0)) INTDR,
	sum(isnull(dt.INTCR, 0)) INTCR,
	sum(isnull(dt.PEN, 0)) PEN,
	sum(isnull(dt.SC, 0)) SC,
	sum(isnull(dt.FD, 0)) FD,
	sum(isnull(dt.SD, 0)) SD,
	sum(isnull(dt.LRI, 0)) LRI
from
	dbo.LOAN_TYPE lt with(nolock)
		left join
	dbo.DAILYTRN dt with(nolock) on
		dt.LOAN_TYPE = lt.LOAN_TYPE
		left join
	dbo.LOANS lon with(nolock) on
		lon.PN_NO = dt.PN_NO
group by
	isnull(lon.PD, 0),
	lt.LOAN_TYPE



GO