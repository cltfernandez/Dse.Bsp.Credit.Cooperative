USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_InterestDetails]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_InterestDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_InterestDetails]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_InterestDetails]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Admin_InterestDetails]
AS

declare @SYSDATE date

select
	top 1
	@SYSDATE = SYSDATE
from
	dbo.CTRL with(nolock)

select
	MOYEAR,
	mem.KBCI_NO,
	isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI, '') MEMBER,
	lon.LOAN_TYPE,
	led.ADD_DATE,
	led.BEGBAL,
	led.DR,
	led.CR,	
	led.ENDBAL,
	led.RMK,
	DATENAME(M, @SYSDATE) + ' ' + DATENAME(D, @SYSDATE) + ', ' + DATENAME(YYYY, @SYSDATE) SYSDATE
from
	dbo.XLEDGER led with(nolock)
		left join
	dbo.LOANS lon with(nolock) on
		lon.PN_NO = led.PN_NO
		left join			
	dbo.MEMBERS mem with(nolock) on
		led.KBCI_NO = mem.KBCI_NO
order by
	MOYEAR,
	MEMBER,
	led.PN_NO,
	led.XLEDGER_ID



GO