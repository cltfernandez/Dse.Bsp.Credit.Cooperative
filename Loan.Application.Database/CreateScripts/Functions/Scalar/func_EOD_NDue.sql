USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_EOD_NDue]    Script Date: 07/09/2009 12:07:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_EOD_NDue]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_EOD_NDue]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_EOD_NDue]    Script Date: 07/09/2009 12:07:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE    FUNCTION [dbo].[func_EOD_NDue]
(
@freq varchar(1),
@pay_start date,
@sysdate date
)
returns date
as
begin

declare @output as numeric (11, 2)

declare @xgomo int = 0
declare @do bit = 1
declare @ttl smallint = 255
declare @xdate date = @pay_start

set	@xgomo = case @freq
	when 'M' then 1
	when 'S' then 6
	when 'Q' then 3
	when 'A' then 12
	end

if @freq = 'D'
begin
	set @xdate = dateadd(D, 30, @xdate)	
	if DATENAME(DW, @xdate) = "Saturday"
		set @xdate = dateadd(D, 2, @xdate)
	else if DATENAME(DW, @xdate) = "Sunday"
		set @xdate = dateadd(D, 1, @xdate)
end
else
	while @do = 1 and @ttl > 0
	begin
		set @ttl -= 1
		
		if @xdate > @sysdate
			set @do = 0
		else
			set @xdate = dateadd(M, @xgomo, @xdate)
		
	end

return(@xdate)

END




GO