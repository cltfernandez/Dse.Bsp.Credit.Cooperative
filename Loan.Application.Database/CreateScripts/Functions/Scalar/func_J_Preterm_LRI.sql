USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_J_Preterm_LRI]    Script Date: 07/09/2009 12:07:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_J_Preterm_LRI]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_J_Preterm_LRI]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_J_Preterm_LRI]    Script Date: 07/09/2009 12:07:08 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE    FUNCTION [dbo].[func_J_Preterm_LRI]
(
@PN_NO varchar(7)
)
returns numeric(10, 2)
as
begin

declare @OUTPUT as numeric(10, 2);
declare @SYSDATE datetime;
declare @CURRENT_LRI_AMT numeric(10, 2);
declare @CURRENT_LRI_DUE datetime;

if exists(select top 1 PN_NO from dbo.[CORRLRI] where PN_NO = @PN_NO)
begin
	select	@SYSDATE = [SYSDATE]
	from	dbo.[CTRL];
	
	declare @LRI table
	(
		[LRI_YEAR] int identity(1,1),
		[LRI_DUE] datetime,
		[LOAN_BAL] numeric(10, 2),
		[LRI_AMT] numeric(10, 2),
		[LRI_PAY] numeric(10, 2)
	)
	
	insert @LRI
	(
			[LRI_DUE],  [LOAN_BAL],  [LRI_AMT],  [LRI_PAY]
	)	
	select	[LRI_DUE1], [LOAN_BAL1], [LRI_AMT1], [LRI_PAY1] from dbo.[CORRLRI] where [PN_NO] = @PN_NO union all
	select	[LRI_DUE2], [LOAN_BAL2], [LRI_AMT2], [LRI_PAY2] from dbo.[CORRLRI] where [PN_NO] = @PN_NO union all
	select	[LRI_DUE3], [LOAN_BAL3], [LRI_AMT3], [LRI_PAY3] from dbo.[CORRLRI] where [PN_NO] = @PN_NO union all
	select	[LRI_DUE4], [LOAN_BAL4], [LRI_AMT4], [LRI_PAY4] from dbo.[CORRLRI] where [PN_NO] = @PN_NO union all
	select	[LRI_DUE5], [LOAN_BAL5], [LRI_AMT5], [LRI_PAY5] from dbo.[CORRLRI] where [PN_NO] = @PN_NO union all
	select	[LRI_DUE6], [LOAN_BAL6], [LRI_AMT6], [LRI_PAY6] from dbo.[CORRLRI] where [PN_NO] = @PN_NO union all
	select	[LRI_DUE7], [LOAN_BAL7], [LRI_AMT7], [LRI_PAY7] from dbo.[CORRLRI] where [PN_NO] = @PN_NO;
	
	--select	@OUTPUT = SUM(ISNULL([LRI_AMT], 0) - ISNULL([LRI_PAY], 0))						-- JS 03/16/2013
	--from	@LRI																				--		|
	--where	[LRI_DUE] <= @SYSDATE;																--		|
	
	select	@CURRENT_LRI_AMT = [LRI_AMT],																			-- JS 04/06/2013
			@CURRENT_LRI_DUE = [LRI_DUE]																			--		|
	from	@LRI																									--		|
	where	[LRI_DUE] =																								--		|
			(																										--		|
				select	MAX([LRI_DUE])																				--		|
				from	@LRI																						--		|
				where	[LRI_DUE] <= @SYSDATE																		--		|
			);																										-- JS 04/06/2013
																								
	select	@OUTPUT =																			--		|
			(																					--		|
				select	SUM(ISNULL([LRI_AMT], 0))												--		|
				from	@LRI																	--		|
				where	[LRI_DUE] <= @SYSDATE													--		|
			) -										 											--		|
			(																					--		|
				select	SUM(ISNULL([LRI_PAY], 0))												--		|
				from	@LRI																	--		|
			)																					--		|
																								-- JS 03/16/2013

	if @OUTPUT > 0 AND @OUTPUT >= @CURRENT_LRI_AMT
	begin
		select	@OUTPUT = ROUND(@OUTPUT - @CURRENT_LRI_AMT + ((@CURRENT_LRI_AMT * DATEDIFF(D, @CURRENT_LRI_DUE, @SYSDATE)) / 360), 2)		-- JS 04/06/2013
	end
	
	if @OUTPUT < 0
	begin
		set		@OUTPUT = 0;
	end
end
else
begin
	set @OUTPUT = 0;
end


return(@OUTPUT);

end


GO