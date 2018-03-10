USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_DailyTransactionRegister]    Script Date: 10/08/2011 12:27:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_DailyTransactionRegister]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_DailyTransactionRegister]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_DailyTransactionRegister]    Script Date: 10/08/2011 12:27:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*************************************************
MODIFIED:
JS 12/15/2012	PD RANKING = 17
*************************************************/

CREATE PROCEDURE [dbo].[Report_Admin_DailyTransactionRegister]
AS

declare @sysdate datetime
declare @RANKING table
(
	RANKING smallint identity(20,10),
	LOAN_TYPE varchar(3)
)

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

insert	@RANKING 
(
	LOAN_TYPE
)
select
	LOAN_TYPE
from
	dbo.LOAN_TYPE with(nolock)
order by
	LOAN_TYPE

declare @DAILYTRN table
(
	[KBCI_NO] varchar(7),
	[NAME] varchar(50),
	[PN_NO] varchar(7),
	[LOAN_TYPE] varchar(3),
	[BEGBAL] numeric(11, 2),
	[DR] numeric(9, 2),
	[CR] numeric(9, 2),
	[ENDBAL] numeric(11, 2),
	[INT] numeric(9, 2),
	[INTCR] numeric(9, 2),
	[INTDR] numeric(9, 2),
	[PEN] numeric(9, 2),
	[SC] numeric(11, 2),
	[LRI] numeric(11, 2),
	[FD] numeric(11, 2),
	[SD] numeric(11, 2),
	[USER] varchar(8),
	[RANKING] smallint
)

/* insert PD = 0 records */
insert	@DAILYTRN
(
	[KBCI_NO],
	[NAME],
	[PN_NO],
	[LOAN_TYPE],
	[BEGBAL],
	[DR],
	[CR],
	[ENDBAL],
	[INT],
	[INTCR],
	[INTDR],
	[PEN],
	[SC],
	[LRI],
	[FD],
	[SD],
	[USER],
	[RANKING]
)
select
	[KBCI_NO],
	[NAME],
	[PN_NO],
	[LOAN_TYPE],
	[BEGBAL],
	[DR],
	[CR],
	[ENDBAL],
	[INT],
	[INTCR],
	[INTDR],
	[PEN],
	[SC],
	[LRI],
	[FD],
	[SD],
	[USER],
	10
from
	dbo.DAILYTRN with(nolock)
where
	LEFT(NAME,1) = '1'

/* insert dummy if there are no PD = 0 records */
if not exists (select top 1 'X' from @DAILYTRN where LEFT(NAME,1) = '1')
insert @DAILYTRN (RANKING) values (10)

/* insert PD = 1 records */
insert @DAILYTRN
(
	[KBCI_NO],
	[NAME],
	[PN_NO],
	[LOAN_TYPE],
	[BEGBAL],
	[DR],
	[CR],
	[ENDBAL],
	[INT],
	[INTCR],
	[INTDR],
	[PEN],
	[SC],
	[LRI],
	[FD],
	[SD],
	[USER],
	[RANKING]
)
select
	[KBCI_NO],
	[NAME],
	[PN_NO],
	[LOAN_TYPE],
	[BEGBAL],
	[DR],
	[CR],
	[ENDBAL],
	[INT],
	[INTCR],
	[INTDR],
	[PEN],
	[SC],
	[LRI],
	[FD],
	[SD],
	[USER],
	17						-- JS 12/15/2012
from
	dbo.DAILYTRN with(nolock)
where
	LEFT(NAME,1) = '2'

/* insert dummy if there are no PD = 1 records */
if not exists (select top 1 'X' from @DAILYTRN where LEFT(NAME,1) = '2') 
insert @DAILYTRN (RANKING) values (17)

select
	10 RANKING,
	isnull(dbo.func_Format241(dt.KBCI_NO), '') KBCI_NO,
	isnull(right(dt.NAME, len(dt.NAME) - 1), '') NAME,
	isnull(dt.LOAN_TYPE, '') LOAN_TYPE,
	isnull(dt.BEGBAL, 0) BEGBAL,
	isnull(dt.DR, 0) DR,
	isnull(dt.CR, 0) CR,
	isnull(dt.ENDBAL, 0) ENDBAL,
	isnull(dt.[INT], 0) INTE,
	isnull(dt.PEN, 0) PEN,
	isnull(dt.SC, 0) SC,
	isnull(dt.FD, 0) FD,
	isnull(dt.SD, 0) SD,
	isnull(dt.LRI, 0) LRI,
	isnull(sr1.BEGBAL, 0) BEGBAL_S,
	isnull(sdt.DR, 0) DR_S,
	isnull(sdt.CR, 0) CR_S,
	isnull(sr1.ENDBAL, 0) ENDBAL_S,
	isnull(sr1.INT_AMNT, 0) INT_AMNT_S,
	isnull(sdt.INTCR, 0) INTCR_S,
	isnull(sdt.INTDR, 0) INTDR_S,
	isnull(sr1.FINT_AMNT, 0) FINT_AMNT_S,
	isnull(sdt.PEN, 0) PEN_S,
	isnull(sdt.SC, 0) SC_S,
	isnull(sdt.FD, 0) FD_S,
	isnull(sdt.SD, 0) SD_S,
	isnull(sdt.LRI, 0) LRI_S,
	isnull(dbo.func_Format241(dt.PN_NO), 0) PN_NO,
	@sysdate SYSDATE
from
	@DAILYTRN dt
	cross join 
	(
		select
			sum(isnull(LN_AMNT, 0)) BEGBAL,
			sum(isnull(FLN_AMNT, 0)) ENDBAL,
			sum(isnull(INT_AMNT, 0)) INT_AMNT,
			sum(isnull(FINT_AMNT, 0)) FINT_AMNT
		from
			dbo.RUNUP1 with(nolock)
	) sr1 
	cross join 
	(
		select
			sum(isnull(DR, 0)) DR,
			sum(isnull(CR, 0)) CR,
			sum(isnull(INTDR, 0)) INTDR,
			sum(isnull(INTCR, 0)) INTCR,
			sum(isnull(PEN, 0)) PEN,
			sum(isnull(SC, 0)) SC,
			sum(isnull(FD, 0)) FD,
			sum(isnull(SD, 0)) SD,
			sum(isnull(LRI, 0)) LRI
		from
			@DAILYTRN
		where
			left(NAME,1) = '1'
	) sdt
where
	dt.RANKING = 10

union all

select
	rnk.RANKING,
	isnull(dbo.func_Format241(dt.KBCI_NO), '') KBCI_NO,
	isnull(right(dt.NAME, len(dt.NAME) - 1), '') NAME,
	isnull(r1.LOAN_TYP, '') LOAN_TYPE,
	isnull(dt.BEGBAL, 0) BEGBAL,
	isnull(dt.DR, 0) DR,
	isnull(dt.CR, 0) CR,
	isnull(dt.ENDBAL, 0) ENDBAL,
	isnull(dt.[INT], 0) INTE,
	isnull(dt.PEN, 0) PEN,
	isnull(dt.SC, 0) SC,
	isnull(dt.FD, 0) FD,
	isnull(dt.SD, 0) SD,
	isnull(dt.LRI, 0) LRI,
	isnull(sr1.BEGBAL, 0) BEGBAL_S,
	isnull(sdt.DR, 0) DR_S,
	isnull(sdt.CR, 0) CR_S,
	isnull(sr1.ENDBAL, 0) ENDBAL_S,
	isnull(sr1.INT_AMNT, 0) INT_AMNT_S,
	isnull(sdt.INTCR, 0) INTCR_S,
	isnull(sdt.INTDR, 0) INTDR_S,
	isnull(sr1.FINT_AMNT, 0) FINT_AMNT_S,
	isnull(sdt.PEN, 0) PEN_S,
	isnull(sdt.SC, 0) SC_S,
	isnull(sdt.FD, 0) FD_S,
	isnull(sdt.SD, 0) SD_S,
	isnull(sdt.LRI, 0) LRI_S,
	isnull(dbo.func_Format241(dt.PN_NO), 0) PN_NO,
	@sysdate SYSDATE
from
	dbo.RUNUP1 r1 with(nolock)
		left outer join 
	(
		select
			*
		from
			dbo.DAILYTRN with(nolock)
		where
			left(NAME,1) = '1'
	) dt on 
		dt.LOAN_TYPE = r1.LOAN_TYP
		left outer join 
	(
		select
			LOAN_TYP,
			sum(isnull(LN_AMNT, 0)) BEGBAL,
			sum(isnull(FLN_AMNT, 0)) ENDBAL,
			sum(isnull(INT_AMNT, 0)) INT_AMNT,
			sum(isnull(FINT_AMNT, 0)) FINT_AMNT
		from
			dbo.RUNUP1 with(nolock)
		group by
			LOAN_TYP
	) sr1 on 
		sr1.LOAN_TYP = r1.LOAN_TYP 
		left outer join 
	(
		select
			LOAN_TYPE,
			sum(isnull(DR, 0)) DR,
			sum(isnull(CR, 0)) CR,
			sum(isnull(INTDR, 0)) INTDR,
			sum(isnull(INTCR, 0)) INTCR,
			sum(isnull(PEN, 0)) PEN,
			sum(isnull(SC, 0)) SC,
			sum(isnull(FD, 0)) FD,
			sum(isnull(SD, 0)) SD,
			sum(isnull(LRI, 0)) LRI
		from
			dbo.DAILYTRN with(nolock)
		where
			left(NAME,1) = '1'
		group by
			LOAN_TYPE
	) sdt on 
		sdt.LOAN_TYPE = r1.LOAN_TYP
		inner join
	@RANKING rnk 
		on rnk.LOAN_TYPE = r1.LOAN_TYP

union all

select
	17 RANKING,
	isnull(dbo.func_Format241(dt.KBCI_NO), '') KBCI_NO,
	isnull(right(dt.NAME, len(dt.NAME) - 1), '') NAME,
	isnull(dt.LOAN_TYPE, '') LOAN_TYPE,
	isnull(dt.BEGBAL, 0) BEGBAL,
	isnull(dt.DR, 0) DR,
	isnull(dt.CR, 0) CR,
	isnull(dt.ENDBAL, 0) ENDBAL,
	isnull(dt.[INT], 0) INTE,
	isnull(dt.PEN, 0) PEN,
	isnull(dt.SC, 0) SC,
	isnull(dt.FD, 0) FD,
	isnull(dt.SD, 0) SD,
	isnull(dt.LRI, 0) LRI,
	isnull(sr1.BEGBAL, 0) BEGBAL_S,
	isnull(sdt.DR, 0) DR_S,
	isnull(sdt.CR, 0) CR_S,
	isnull(sr1.ENDBAL, 0) ENDBAL_S,
	isnull(sr1.INT_AMNT, 0) INT_AMNT_S,
	isnull(sdt.INTCR, 0) INTCR_S,
	isnull(sdt.INTDR, 0) INTDR_S,
	isnull(sr1.FINT_AMNT, 0) FINT_AMNT_S,
	isnull(sdt.PEN, 0) PEN_S,
	isnull(sdt.SC, 0) SC_S,
	isnull(sdt.FD, 0) FD_S,
	isnull(sdt.SD, 0) SD_S,
	isnull(sdt.LRI, 0) LRI_S,
	isnull(dbo.func_Format241(dt.PN_NO), 0) PN_NO,
	@sysdate SYSDATE
from
	@DAILYTRN dt
		cross join 
	(
		select
			sum(isnull(LN_AMNT, 0)) BEGBAL,
			sum(isnull(FLN_AMNT, 0)) ENDBAL,
			sum(isnull(INT_AMNT, 0)) INT_AMNT,
			sum(isnull(FINT_AMNT, 0)) FINT_AMNT
		from
			dbo.RRUNUP1 with(nolock)
	) sr1 
		cross join 
	(
		select
			sum(isnull(DR, 0)) DR,
			sum(isnull(CR, 0)) CR,
			sum(isnull(INTDR, 0)) INTDR,
			sum(isnull(INTCR, 0)) INTCR,
			sum(isnull(PEN, 0)) PEN,
			sum(isnull(SC, 0)) SC,
			sum(isnull(FD, 0)) FD,
			sum(isnull(SD, 0)) SD,
			sum(isnull(LRI, 0)) LRI
		from
			@DAILYTRN
		where
			left(NAME,1) = '2'
	) sdt
where
	dt.RANKING = 17

union all

select
	rnk.RANKING + 7 RANKING,
	isnull(dbo.func_Format241(dt.KBCI_NO), '') KBCI_NO,
	isnull(right(dt.NAME, len(dt.NAME) - 1), '') NAME,
	isnull(r1.LOAN_TYP, '') LOAN_TYPE,
	isnull(dt.BEGBAL, 0) BEGBAL,
	isnull(dt.DR, 0) DR,
	isnull(dt.CR, 0) CR,
	isnull(dt.ENDBAL, 0) ENDBAL,
	isnull(dt.[INT], 0) INTE,
	isnull(dt.PEN, 0) PEN,
	isnull(dt.SC, 0) SC,
	isnull(dt.FD, 0) FD,
	isnull(dt.SD, 0) SD,
	isnull(dt.LRI, 0) LRI,
	isnull(sr1.BEGBAL, 0) BEGBAL_S,
	isnull(sdt.DR, 0) DR_S,
	isnull(sdt.CR, 0) CR_S,
	isnull(sr1.ENDBAL, 0) ENDBAL_S,
	isnull(sr1.INT_AMNT, 0) INT_AMNT_S,
	isnull(sdt.INTCR, 0) INTCR_S,
	isnull(sdt.INTDR, 0) INTDR_S,
	isnull(sr1.FINT_AMNT, 0) FINT_AMNT_S,
	isnull(sdt.PEN, 0) PEN_S,
	isnull(sdt.SC, 0) SC_S,
	isnull(sdt.FD, 0) FD_S,
	isnull(sdt.SD, 0) SD_S,
	isnull(sdt.LRI, 0) LRI_S,
	isnull(dbo.func_Format241(dt.PN_NO), 0) PN_NO,
	@sysdate SYSDATE
from
	dbo.RRUNUP1 r1 with(nolock)
		left outer join 
	(
		select
			*
		from
			dbo.DAILYTRN with(nolock)
		where
			left(NAME,1) = '2'
	) dt on 
		dt.LOAN_TYPE = r1.LOAN_TYP
		left outer join 
	(
		select
			LOAN_TYP,
			sum(isnull(LN_AMNT, 0)) BEGBAL,
			sum(isnull(FLN_AMNT, 0)) ENDBAL,
			sum(isnull(INT_AMNT, 0)) INT_AMNT,
			sum(isnull(FINT_AMNT, 0)) FINT_AMNT
		from
			dbo.RRUNUP1 with(nolock)
		group by
			LOAN_TYP
	) sr1 on 
		sr1.LOAN_TYP = r1.LOAN_TYP 
		left outer join 
	(
		select	
			LOAN_TYPE,
			sum(isnull(DR, 0)) DR,
			sum(isnull(CR, 0)) CR,
			sum(isnull(INTDR, 0)) INTDR,
			sum(isnull(INTCR, 0)) INTCR,
			sum(isnull(PEN, 0)) PEN,
			sum(isnull(SC, 0)) SC,
			sum(isnull(FD, 0)) FD,
			sum(isnull(SD, 0)) SD,
			sum(isnull(LRI, 0)) LRI
		from
			dbo.DAILYTRN with(nolock)
		where
			left(NAME,1) = '2'
		group by
			LOAN_TYPE
	) sdt on 
		sdt.LOAN_TYPE = r1.LOAN_TYP
		inner join
	@RANKING rnk
		on rnk.LOAN_TYPE = r1.LOAN_TYP
order by
	RANKING,
	NAME,
	LOAN_TYPE



GO

