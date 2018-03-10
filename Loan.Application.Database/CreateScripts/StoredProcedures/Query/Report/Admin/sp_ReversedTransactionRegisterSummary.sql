USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_ReversedTransactionRegisterSummary]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_ReversedTransactionRegisterSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_ReversedTransactionRegisterSummary]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_ReversedTransactionRegisterSummary]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Admin_ReversedTransactionRegisterSummary]
AS

declare @sysdate datetime

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

select
	Z.LOAN_TYPE,
	sum(isnull(dt.DR, 0)) DR,
	sum(isnull(dt.CR, 0)) CR,
	sum(isnull(dt.[INT]*(-1), 0)) [INT],
	sum(isnull(dt.PEN, 0)) PEN,
	sum(isnull(dt.SC, 0)) SC,
	sum(isnull(dt.FD, 0)) FD,
	sum(isnull(dt.SD, 0)) SD,
	sum(isnull(dt.LRI, 0)) LRI,
	@sysdate SYSDATE
from
	dbo.DAILYREV dt with(nolock)
		right join
	(
		select
			RANK() over (order by LOAN_TYPE) as RANKING,
			LOAN_TYPE
		from
			dbo.LOAN_TYPE with(nolock)
	) Z on 
		dt.LOAN_TYPE = Z.LOAN_TYPE
group by
	Z.LOAN_TYPE,
	Z.RANKING
order by
	Z.RANKING




GO