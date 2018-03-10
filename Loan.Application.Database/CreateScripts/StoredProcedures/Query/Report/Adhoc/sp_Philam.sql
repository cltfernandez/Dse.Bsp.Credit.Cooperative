USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Philam]    Script Date: 11/16/2013 18:28:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Adhoc_Philam]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Adhoc_Philam]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Philam]    Script Date: 11/16/2013 18:28:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*****************************************************************************
MODIFIED:
*****************************************************************************/

create procedure [dbo].[Report_Adhoc_Philam]
@loan_type varchar(3),
@month int,
@year int
as

exec Report_Adhoc_Remit @loan_type, @month, @year

exec Report_Adhoc_Refund @loan_type, @month, @year

GO

