USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Process_EndOfDay_Arrears]    Script Date: 07/15/2009 11:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Process_EndOfDay_Arrears]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Process_EndOfDay_Arrears]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Process_EndOfDay_Arrears]    Script Date: 07/15/2009 11:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Process_EndOfDay_Arrears]
AS

declare @sysdate datetime

select
	@sysdate = SYSDATE
from
	CTRL

update
	lar
set
	ARREAR_P = lon.ARREAR_P,
	ARREAR_I = lon.ARREAR_I,
	ARREAR_OTH = lon.ARREAR_OTH,
	SYSDATE = @sysdate
from
	dbo.LOAN_ARREARS lar
		inner join
	dbo.LOANS lon on
		lar.PN_NO = lon.PN_NO and
		lar.ARREAR_AS = lon.ARREAR_AS
where
	lon.LOAN_STAT = 'R' and
	lon.PD = 0 and
	isnull(lon.ARREAR_AS, '1900-01-01') != '1900-01-01' and
	month(lar.SYSDATE) = month(@sysdate) and
	year(lar.SYSDATE) = year(@sysdate) and
	(
		isnull(lon.[ARREAR_P], 0) > 0 and
		isnull(lon.[ARREAR_I], 0) > 0 and
		isnull(lon.[ARREAR_OTH], 0) > 0
	)

insert into dbo.LOAN_ARREARS
(
	PN_NO,
	ARREAR_AS,
	ARREAR_P,
	ARREAR_I,
	ARREAR_OTH,
	SYSDATE
)
select
	lon.PN_NO,
	lon.ARREAR_AS,
	isnull(lon.ARREAR_P, 0),
	isnull(lon.ARREAR_I, 0),
	isnull(lon.ARREAR_OTH, 0),
	@sysdate
from
	dbo.LOANS lon
		inner join
	dbo.LOAN_ARREARS lar on
		lon.PN_NO = lar.PN_NO and
		lon.ARREAR_AS = lar.ARREAR_AS
where
	lon.LOAN_STAT = 'R' and
	lon.PD = 0 and
	isnull(lon.ARREAR_AS, '1900-01-01') != '1900-01-01' and
	(
		isnull(lon.[ARREAR_P], 0) > 0 or
		isnull(lon.[ARREAR_I], 0) > 0 or
		isnull(lon.[ARREAR_OTH], 0) > 0
	) and
	not exists
	(
		select
			'exist'
		from
			dbo.LOAN_ARREARS
		where
			PN_NO = lon.PN_NO and
			ARREAR_AS = lon.ARREAR_AS and
			month(SYSDATE) = month(@sysdate) and
			year(SYSDATE) = year(@sysdate)
	)

GO