USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_MT_LRIDUE_Update]    Script Date: 04/17/2009 17:28:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_MT_LRIDUE_Update]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_MT_LRIDUE_Update]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_MT_LRIDUE_Update]    Script Date: 04/17/2009 17:28:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[s3p_MT_LRIDUE_Update]
@PN_NO varchar(7),
@LRIDUE numeric(13,4),
@MY_USER varchar(8)
AS

begin transaction process;

begin try

	update	dbo.LRIDUE
	set		[LRI_DUE_C] = @LRIDUE
	where	[PN_NO] = @PN_NO;

	update	dbo.LOANS
	set		[LRI_DUE] = @LRIDUE,
			[USER] = @MY_USER
	where	[PN_NO] = @PN_NO;

	commit transaction process;
	
end try
begin catch
	
	declare @ErrorMessage nvarchar(4000) = error_message();
    declare @ErrorSeverity int = error_severity();
    declare @ErrorState int = error_state();
	
	rollback transaction process;
	raiserror(@ErrorMessage, @ErrorSeverity, @ErrorState);

end catch;


GO


