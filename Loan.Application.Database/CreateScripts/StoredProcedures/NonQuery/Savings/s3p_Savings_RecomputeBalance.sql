USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Savings_RecomputeBalance]    Script Date: 04/17/2009 17:28:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Savings_RecomputeBalance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Savings_RecomputeBalance]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Savings_RecomputeBalance]    Script Date: 04/17/2009 17:28:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Savings_RecomputeBalance]
@ACCTNUM VARCHAR(10)
AS

;with data
as
(
	select
		ROW_NUMBER() over (order by TRANDATE, SDTRAN_ID) ID,
		*
	from
		SDTRAN
	where
		ACCTNUM = @ACCTNUM
),
bal as
(
	select
		ID,
		SDTRAN_ID,
		TRANBBAL,
		TRANDEB,
		TRANCRE,
		cast(TRANBBAL - TRANDEB + TRANCRE as numeric(11,2)) AS 'TRANEBAL'
	from
		data
	where
		ID = 1
	union all
	select
		d.ID,
		d.SDTRAN_ID,
		b.TRANEBAL AS 'TRANBBAL',
		d.TRANDEB,
		d.TRANCRE,
		cast(b.TRANEBAL - d.TRANDEB + d.TRANCRE as numeric(11,2)) AS 'TRANEBAL'
	from
		data d
			inner join
		bal b on
			b.ID + 1 = d.ID
	
)
update
	tgt
set
	TRANEBAL = src.TRANEBAL
from
	bal src
		inner join
	SDTRAN tgt on
		src.SDTRAN_ID = tgt.SDTRAN_ID
option
	(MAXRECURSION 10000);



GO


