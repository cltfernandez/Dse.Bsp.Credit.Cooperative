USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Members_List]    Script Date: 07/11/2009 20:52:38 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Members_List]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Members_List]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Members_List]    Script Date: 07/11/2009 20:52:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO








CREATE PROCEDURE [dbo].[Report_Members_List]
AS

declare @SYSDATE date

select
	top 1
	@SYSDATE = SYSDATE
from
	dbo.CTRL with(nolock)

select
	KBCI_NO,
	dbo.func_FullName(LNAME, FNAME, MI) FULL_NAME,
	MEM_CODE,
	MEM_STAT,
	DORI,
	REA_DORI,
	dbo.func_Format451(FEBTC_SA) FEBTC_SA,
	CB_EMPNO,
	REGION REGIONS,
	OFF_TEL,
	SAL_BAS,
	SEX,
	CIV_STAT,
	NO_DEPEND,
	B_DATE,
	SP_NAME,
	MEM_ADDR,
	CB_HIRE,
	DEPT,
	RES_TEL,
	SAL_ALL,
	SP_EMPLOY,
	POSITION,
	OTH_INC,
	SP_CBEMPNO,
	SP_OFFTEL,
	SP_SALARY,
	@SYSDATE SYSDATE
from
	dbo.MEMBERS with(nolock)
order by
	FULL_NAME




GO