USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_NumberToWord]    Script Date: 07/09/2009 12:07:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_NumberToWord]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_NumberToWord]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_NumberToWord]    Script Date: 07/09/2009 12:07:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE    FUNCTION [dbo].[func_NumberToWord]
(
@nInput numeric(18,2)
)
RETURNS VARCHAR(300)
AS  
BEGIN 

declare @cOnes varchar(200)
declare @cTens varchar(100)
declare @cPValue varchar(100)

declare @cOutput varchar(300)
declare @cTemp varchar(100)
declare @nTemp numeric(7,2)
declare @iX int
declare @i3 int
declare @i2 int
declare @i1 int

set @cOnes = 'One      Two      Three    Four     Five     Six      Seven    Eight    Nine     Ten      Eleven   Twelve   Thirteen Fourteen Fifteen  Sixteen  SeventeenEighteen Nineteen '
set @cTens = 'Twenty Thirty Fourty Fifty  Sixty  SeventyEighty Ninety '
set @cPValue = 'Thousand Million  Billion  Trillion '

set @cTemp = right('000000000000000' + convert(varchar(100), @nInput), 18)
set @iX = 5
set @cOutput = ''

while @iX > 0
	begin
		set @nTemp = convert(numeric(5,2), substring(@cTemp, ((5-@iX)*3)+1, 3))
		set @i3 = convert(int, substring(convert(varchar(100), @cTemp), ((5-@iX)*3)+1, 1))
		set @i2 = convert(int, substring(convert(varchar(100), @cTemp), ((5-@iX)*3)+2, 1))
		set @i1 = convert(int, substring(convert(varchar(100), @cTemp), ((5-@iX)*3)+3, 1))

		if @i3 > 0
			set @cOutput = @cOutput + rtrim(substring(@cOnes, ((@i3-1)*9)+1, 9)) + ' Hundred '			

		if @i2 = 1
			begin
			set @i2 = (@i2 * 10) + @i1
			set @cOutput = @cOutput + rtrim(substring(@cOnes, ((@i2-1)*9)+1, 9)) + ' '
			set @i2 = (@i2 - @i1) / 10
			end
		else 
			if @i2 > 1
				set @cOutput = @cOutput + rtrim(substring(@cTens, ((@i2-2)*7)+1, 7)) + ' '
			if @i1 > 0 and @i2 <> 1
				set @cOutput = @cOutput + rtrim(substring(@cOnes, ((@i1-1)*9)+1, 9)) + ' '
	
		if @nTemp > 0 and @iX > 1
			set @cOutput = @cOutput + rtrim(substring(@cPValue, ((@iX-2)*9)+1, 9)) + ' '
		else if @nInput >= 0 and @nInput < 1
		begin
			set @cOutput = 'zero '
			set @iX = 0
		end

		set @iX = @iX - 1
	end

set @cOutput = @cOutput + 'and ' + right(@cTemp, 2) + '/100'
	
RETURN(@cOutput)

END





GO