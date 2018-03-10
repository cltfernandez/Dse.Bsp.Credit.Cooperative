USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Generate_Stop]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Payroll_Generate_Stop]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Payroll_Generate_Stop]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Generate_Stop]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Payroll_Generate_Stop]
@EMPNO varchar(6),
@WAGE_TYPE varchar(4),
@ENDDA varchar(8)
as

delete	dbo.[ADVICE]
where	EMPNO + WAGE_TYPE = @EMPNO + @WAGE_TYPE

if exists (select 'X' from dbo.[STOP] where EMPNO + WAGE_TYPE = @EMPNO + @WAGE_TYPE)
	update	dbo.[STOP]
	set		[ENDDA] = @ENDDA
	where	EMPNO + WAGE_TYPE = @EMPNO + @WAGE_TYPE
else
	insert	dbo.[STOP] (
			[EMPNO],
			[WAGE_TYPE],
			[ENDDA]
			
	)
	values (
			@EMPNO,
			@WAGE_TYPE,
			@ENDDA			
	)



GO