USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_J_AMORT]    Script Date: 07/09/2009 12:07:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_J_AMORT]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_J_AMORT]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_J_AMORT]    Script Date: 07/09/2009 12:07:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE    FUNCTION [dbo].[func_J_AMORT]
(
@principal numeric(13,4),
@amort_amt numeric(13,4),
@freq varchar(1),
@rate numeric(7,4),
@pay_start date,
@sysdate date
)
returns varchar(100)
as
begin

declare @output as varchar(100)

declare @xbal numeric(11, 2) = 0
declare @xint numeric(11, 2) = 0
declare @xpri numeric(11, 2) = 0
declare @ybaln numeric(11, 2) = 0
declare @xdate date = @pay_start
declare @xfreq int = 1
declare @xgomo int = 0
declare @do bit = 1
declare @ttl smallint = 255

set	@xgomo = case @FREQ
	when 'M' then 1
	when 'S' then 6
	when 'Q' then 3
	when 'A' then 12
	end

set	@xfreq = case @FREQ
	when 'M' then 12
	when 'S' then 6
	when 'Q' then 4
	when 'A' then 1
	end

while @do = 1 and @ttl > 0
begin
	set @ttl -= 1
	set @xbal = @principal - @ybaln
	set @xint = (@xbal * @rate) / (100 * @xfreq)
	set @xpri = @amort_amt - @xint
	
	if @xpri > @xbal
	begin
		set @xpri = @xbal
		set @xint = 0
	end
	
	if @xdate > @sysdate
		set @do = 0
	else
	begin
		set @ybaln += @xpri
		set @xdate = dateadd(M, @xgomo, @xdate)
	end
	
	if @ybaln >= @principal set @do = 0
end

set @xbal = isnull(@xbal, 0)

-- return(@xbal)
set @output = 'xbal:' + convert(varchar(20), @xbal) + ';xpri:' + convert(varchar(20), @xpri) + ';xint:' + convert(varchar(20), @xint) + ';'
return(@output)

END




GO