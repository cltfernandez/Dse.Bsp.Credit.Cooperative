USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_ReversedTransactionRegisterDetails]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_ReversedTransactionRegisterDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_ReversedTransactionRegisterDetails]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_ReversedTransactionRegisterDetails]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Admin_ReversedTransactionRegisterDetails]
AS

declare @sysdate datetime

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

select
	isnull(dbo.func_Format241(dt.KBCI_NO), '') KBCI_NO,
	isnull(dt.NAME, '') NAME,
	isnull(dt.LOAN_TYPE, '') LOAN_TYPE,
	isnull(dt.BEGBAL, 0) BEG_BAL,
	isnull(dt.DR, 0) DR,
	isnull(dt.CR, 0) CR,
	isnull(dt.ENDBAL, 0) ENDBAL,
	isnull(dt.[INT], 0) INTE,
	isnull(dt.PEN, 0) PEN,
	isnull(dt.SC, 0) SC,
	isnull(dt.FD, 0) FD,
	isnull(dt.SD, 0) SD,
	isnull(dt.LRI, 0) LRI,			
	isnull(Z.BEGBAL, 0) BEGBAL_S,
	isnull(Z.DR, 0) DR_S,
	isnull(Z.CR, 0) CR_S,
	isnull(Z.ENDBAL, 0) ENDBAL_S,
	isnull(Z.INT_AMNT, 0) INT_AMNT_S,
	isnull(Z.INTCR, 0) INTCR_S,
	isnull(Z.INTDR, 0) INTDR_S,
	isnull(Z.FINT_AMNT, 0) FINT_AMNT_S,
	isnull(Z.PEN, 0) PEN_S,
	isnull(Z.SC, 0) SC_S,
	isnull(Z.FD, 0) FD_S,
	isnull(Z.SD, 0) SD_S,
	isnull(Z.LRI, 0) LRI_S,
	isnull(dbo.func_Format241(dt.PN_NO), 0) PN_NO,
	@sysdate SYSDATE
from
	dbo.DAILYREV dt with(nolock)
		cross join
	(
		select
			isnull(r1.BEGBAL, 0) BEGBAL, 
			isnull(dt.DR, 0) DR,
			isnull(dt.CR, 0) CR,
			isnull(r1.ENDBAL, 0) ENDBAL,
			isnull(r1.INT_AMNT, 0) INT_AMNT,
			isnull(dt.INTCR, 0) INTCR,
			isnull(dt.INTDR, 0) INTDR,
			isnull(r1.FINT_AMNT, 0) FINT_AMNT ,
			isnull(dt.PEN, 0) PEN,
			isnull(dt.SC, 0) SC,
			isnull(dt.FD, 0) FD,
			isnull(dt.SD, 0) SD,
			isnull(dt.LRI, 0) LRI
		from
			(
				select
					sum(isnull(dt.DR, 0)) DR, 
					sum(isnull(dt.CR, 0)) CR,
					sum(isnull(0, 0)) INTDR, 
					sum(isnull(0, 0)) INTCR,
					sum(isnull(dt.PEN, 0)) PEN,
					sum(isnull(dt.SC, 0)) SC,
					sum(isnull(dt.FD, 0)) FD,
					sum(isnull(dt.SD, 0)) SD,
					sum(isnull(dt.LRI, 0)) LRI
				from
					dbo.DAILYREV dt with(nolock)
			) dt
				cross join
			(
				select
					sum(isnull(r1.LN_AMNT, 0)) BEGBAL,
					sum(isnull(r1.FLN_AMNT, 0)) ENDBAL,
					sum(isnull(r1.INT_AMNT, 0)) INT_AMNT,
					sum(isnull(r1.FINT_AMNT, 0)) FINT_AMNT
				from
					dbo.RUNUP1 r1 with(nolock)
			) r1
	) Z
order by
	dt.LOAN_TYPE,
	dt.NAME




GO