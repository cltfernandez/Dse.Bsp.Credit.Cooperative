/****** Object:  UserDefinedFunction [dbo].[func_Balance]    Script Date: 09/27/2011 13:53:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_Balance]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_Balance]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_Balance]    Script Date: 09/27/2011 13:53:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create	function [dbo].[func_Balance]
(
	@PN_NO as varchar(7),
	@DATE1 as date = null,
	@DATE2 as date = null
)
returns @loans table
(
	PN_NO varchar(7),
	BALANCE numeric(13,4)
)
as
begin

	insert @loans
	(
		PN_NO,
		BALANCE
	)
	select
		lon.PN_NO,
		lon.PRINCIPAL +
		sum
		(
			case
			when
				(@DATE1 is null or @DATE2 is null or led.[DATE] between @DATE1 and @DATE2) and
				led.ACCT_CODE = 'PRI' and 
				led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP')
			then
				led.DR - led.CR
			else
				0
			end
		) BALANCE
	from
		dbo.LOANS lon with(nolock)
			inner join
		dbo.LEDGER led with(nolock) on
			lon.PN_NO = led.PN_NO
	where
		lon.PN_NO = @PN_NO
	group by
		lon.PN_NO,
		lon.PRINCIPAL

return

end
GO

