USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Generate_Feed]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Payroll_Generate_Feed]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Payroll_Generate_Feed]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Generate_Feed]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Payroll_Generate_Feed]
@EMPNO varchar(6),
@WAGE_TYPE varchar(4),
@BEGDA varchar(8),
@ENDDA varchar(8),
@AMOUNT numeric(16,4)
as

delete	dbo.[STOP]
where	EMPNO + WAGE_TYPE = @EMPNO + @WAGE_TYPE

if exists (select 'X' from dbo.ADVICE where EMPNO + WAGE_TYPE = @EMPNO + @WAGE_TYPE)
	update	dbo.ADVICE
	set		[BEGDA] = @BEGDA,
			[ENDDA] = @ENDDA,
			[AMOUNT] = @AMOUNT
	where	EMPNO + WAGE_TYPE = @EMPNO + @WAGE_TYPE
else
	insert	dbo.ADVICE (
			[EMPNO],
			[WAGE_TYPE],
			[BEGDA],
			[ENDDA],
			[AMOUNT]
	)
	values (
			@EMPNO,
			@WAGE_TYPE,
			@BEGDA,
			@ENDDA,
			@AMOUNT
	)



GO