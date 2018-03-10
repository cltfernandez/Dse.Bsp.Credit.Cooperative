USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_TotalExposure]    Script Date: 08/30/2014 14:40:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Adhoc_TotalExposure]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Adhoc_TotalExposure]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_TotalExposure]    Script Date: 08/30/2014 14:40:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_Adhoc_TotalExposure]
@dateStart datetime,
@dateEnd datetime
as

SELECT
	lon.PN_NO,
	lon.KBCI_NO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	lon.PRINCIPAL,
	lon.CHKNO_AMT,
	bal.BALANCE,
	lon.LOAN_TYPE,
	lon.PD,
	lon.LOAN_STAT,
	lon.DATE_GRANT,
	lon.DATE_DUE,
	@dateStart 'START_DATE',
	@dateEnd 'END_DATE'
FROM
	dbo.LOANS lon
		INNER JOIN
	dbo.MEMBERS mem on
		lon.KBCI_NO = mem.KBCI_NO
		CROSS APPLY
	dbo.func_Balance(lon.PN_NO, @dateStart, @dateEnd) bal
WHERE
	bal.BALANCE > 0
ORDER BY
	FULL_NAME,
	LOAN_TYPE
GO

