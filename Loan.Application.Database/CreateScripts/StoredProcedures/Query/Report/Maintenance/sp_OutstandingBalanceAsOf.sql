USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Maintenance_OutstandingBalanceAsOf]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Maintenance_OutstandingBalanceAsOf]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Maintenance_OutstandingBalanceAsOf]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Maintenance_OutstandingBalanceAsOf]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Maintenance_OutstandingBalanceAsOf]
@kbci_no as varchar(7),
@my_user as varchar(8),
@asOfDate as date = null
as

exec Report_Maintenance_LoansMonitoring @kbci_no, @my_user, @asOfDate, 0