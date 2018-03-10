USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Process_Insert]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Payroll_Process_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Payroll_Process_Insert]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Process_Insert]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Payroll_Process_Insert]
@EMPNO1	numeric(7,0),
@ACTYPE	numeric(2,0),
@ACTCD1	numeric(3,0),
@ACTCD2	numeric(4,0),
@DATE7	date,
@AMT7C	numeric(16,4),
@CODE5	numeric(2,0)
as

insert	dbo.EXTKBC (
		EMPNO1,
		ACTYPE,
		ACTCD1,
		ACTCD2,
		DATE7,
		AMT7C,
		CODE5
)
values (
		@EMPNO1,
		@ACTYPE,
		@ACTCD1,
		@ACTCD2,
		@DATE7,
		@AMT7C,
		@CODE5
)




GO