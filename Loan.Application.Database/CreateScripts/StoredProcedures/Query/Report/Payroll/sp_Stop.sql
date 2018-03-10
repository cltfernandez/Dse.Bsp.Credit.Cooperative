USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_Stop]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Payroll_Stop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Payroll_Stop]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Payroll_Stop]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Payroll_Stop]
as

select
	EMPNO,
	WAGE_TYPE,
	ENDDA		
from
	dbo.[STOP] with(nolock)




GO