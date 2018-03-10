USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Util_Compare]    Script Date: 07/15/2009 11:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Util_Compare]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Util_Compare]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Util_Compare]    Script Date: 07/15/2009 11:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[s3p_Util_Compare]
@strTable varchar(17),
@strWhere varchar(17),
@strOrder varchar(17)
AS

declare @strField varchar(17)
--declare @strFields varchar(500) = ''
declare @strSql varchar(8000) = ''

declare cursCompare cursor for
select	sc.name
from	sys.sysobjects so 
		inner join sys.syscolumns sc on sc.id = so.id
where	so.name = @strTable		

open	cursCompare
fetch	cursCompare
into	@strField

while @@FETCH_STATUS = 0
begin
	if right(@strField, 3) != '_ID' and @strField not in ('xbprin', 'xfreq', 'xint', 'trace')
		set	@strSql = @strSql + 'SELECT A.' + @strOrder + ',A.[' + @strField + '],B.[' + @strField + '] FROM ' + @strTable + ' A, #' + @strTable + ' B WHERE A.' + @strWhere + '=B.' + @strWhere + ' AND A.[' + @strField + ']<>B.[' + @strField + '] ORDER BY A.' + @strOrder + '; '
	
	--if @strField not in (@strTable + '_ID')
	--	set	@strFields = @strFields + ' [' + @strField + ']'

	fetch	cursCompare
	into	@strField
end

close		cursCompare
deallocate	cursCompare

-- set @strFields = REPLACE(LTRIM(@strFields), ' ', ',')

--set	@strSql = 'INSERT INTO #' + @strTable + '(' + @strFields + ') SELECT ' + @strFields + ' FROM openrowset(''MSDASQL'',''Driver={Microsoft dBASE Driver (*.dbf)};DefaultDir=D:\Jo\Programs\Foxpro\JUNTAGS\loans\data;SourceType=DBF'',''SELECT * FROM ' + @strTable + ''') ORDER BY ; ' + @strSql
--set	@strSql = 'ALTER TABLE #' + @strTable + ' ADD ' + @strTable + '_ID BIGINT IDENTITY(1,1); ' + @strSql
--set	@strSql = 'SELECT * INTO #' + @strTable + ' FROM openrowset(''MSDASQL'',''Driver={Microsoft dBASE Driver (*.dbf)};DefaultDir=D:\Jo\Programs\Foxpro\JUNTAGS\loans\data;SourceType=DBF'',''SELECT * FROM ' + @strTable + ''') WHERE 1<>1; ' + @strSql

set	@strSql = 'SELECT * INTO #' + @strTable + ' FROM openrowset(''MSDASQL'',''Driver={Microsoft dBASE Driver (*.dbf)};DefaultDir=D:\Jo\Programs\Foxpro\JUNTAGS\loans\data;SourceType=DBF'',''SELECT * FROM ' + @strTable + ''') ' + @strSql

exec(@strSql)



GO