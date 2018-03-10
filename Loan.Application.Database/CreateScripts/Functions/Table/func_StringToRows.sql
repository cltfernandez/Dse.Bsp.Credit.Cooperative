USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_StringToRows]    Script Date: 08/19/2014 21:30:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_StringToRows]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_StringToRows]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_StringToRows]    Script Date: 08/19/2014 21:30:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create function [dbo].[func_StringToRows]
(
	@sInput varchar(1000),
	@sDelimiter char(1)
)
returns @tOutput table (
	VALUE varchar(100)
)
as
begin

declare @iStart int = 0
declare @iEnd int = 0

while charindex(@sDelimiter, @sInput, @iStart) > 0
begin
	set @iEnd = charindex(@sDelimiter, @sInput, @iStart + 1)
	insert into @tOutput ([VALUE]) values (substring(@sInput, @iStart, @iEnd - @iStart))
	set @iStart = @iEnd + 1
end

insert into @tOutput ([VALUE]) values (substring(@sInput, @iStart, len(@sInput) - @iStart + 1))

update @tOutput set [VALUE] = ltrim(rtrim([VALUE]))

return

end

GO


