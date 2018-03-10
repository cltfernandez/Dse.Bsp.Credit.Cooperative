USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_J_Preterm]    Script Date: 07/17/2009 17:38:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_J_Preterm]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_J_Preterm]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_J_Preterm]    Script Date: 07/17/2009 17:38:25 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*****************************************************************************
MODIFIED:
JS 03/02/2013		RETURNED TABLE INSTEAD OF VALUE
					MANUALLY TRIGGER PRORATE?
*****************************************************************************/


CREATE    FUNCTION [dbo].[func_J_Preterm]
(
@mode as varchar(3),
@nampien as varchar(7),
@lontayp as varchar(3),
@my_user as varchar(50)
)
RETURNS numeric(10, 2)
AS  
BEGIN 

declare @xbprin as numeric(10, 2) = 0
declare @xbarr as numeric(10, 2) = 0
declare @xbpen as numeric(10, 2) = 0
declare @xpayment as numeric(10, 2) = 0
declare @xadvint as numeric(10, 2) = 0
declare @xbint as numeric(10, 2) = 0
declare @xbtotal as numeric(10, 2) = 0
declare @xlri as numeric(10, 2) = 0
declare @xtouts as numeric(10, 2) = 0
declare @rate as numeric(7, 4) = 0
declare @arrear_p as numeric(10, 2) = 0
declare @arrear_i as numeric(10, 2) = 0
declare @arrear_oth as numeric(10, 2) = 0
declare @xpint as numeric(10, 2) = 0
declare @xbfull as numeric(10, 2) = 0
declare @xbpart as numeric(10, 2) = 0
declare @p_bal as numeric(10, 2) = 0
declare @i_bal as numeric(10, 2) = 0
declare @o_bal as numeric(10, 2) = 0

declare @xlpay as datetime = '01/01/1901'
declare @poandue as datetime = '01/01/1901'
declare @pay_start as datetime = '01/01/1901'
declare @chkno_date as datetime = '01/01/1901'
declare @sysdate as datetime

declare @poakey as varchar(7) = @nampien
declare @loan_type as varchar(3) = ''
declare @pn_no as varchar(7) = ''
declare @kbci_no as varchar(7) = ''
declare @xpdtag as bit = 0

select	top 1 @sysdate = SYSDATE
from	dbo.CTRL

select	@xbprin = SUM(case
			when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ', 'AMT') then DR - CR
			else 0
			end),
		@xadvint = SUM(case
			when ACCT_CODE = 'INT' and ACCT_TYPE = 'INT' then CR
			else 0
			end),
		@xlpay = MAX(case
			when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ') then [DATE]
			else NULL
			end)
from	dbo.LEDGER
where	PN_NO = @poakey

set @xpdtag = 0

select	@xbarr = ARREAR_P + ARREAR_I,
		@xbpen = ARREAR_OTH,
		@poandue = ndue,
		@xpdtag = pd,
		@pay_start = PAY_START,
		@chkno_date = CHKNO_DATE,
		@rate = rate,		
		@arrear_p = ARREAR_P,
		@arrear_i = ARREAR_I, 
		@arrear_oth = ARREAR_OTH,
		@p_bal = P_BAL,
		@o_bal = O_BAL,
		@i_bal = I_BAL,
		@pn_no = PN_NO,
		@kbci_no = KBCI_NO,
		@loan_type = LOAN_TYPE
from	dbo.LOANS
where	PN_NO = @poakey

if not ('DE' like '%' + @mode + '%')
	--set @xlri = [dbo].[func_J_Preterm_LRI](@poakey)						-- JS 03/02/2013
	select	@xlri =  LRI_DUE												--		|
	from	dbo.LOANS														--		|
	where	PN_NO = @poakey													-- JS 03/02/2013

if @poandue = @pay_start
begin
	if @xlpay is null or @xlpay = '01/01/1901'
		-- set @xbint = (@xbprin * (@rate/100/360) * DATEDIFF(dd, @chkno_date, @sysdate)) - @xadvint
		set @xbint = ((@xbprin * @rate * DATEDIFF(dd, @chkno_date, @sysdate)) / 36000) - @xadvint
	else
		-- set @xbint = @xbprin * (@rate/100/360) * DATEDIFF(dd, @xlpay, @sysdate)
		set @xbint = (@xbprin * @rate * DATEDIFF(dd, @xlpay, @sysdate)) / 36000
	if @xtouts > 0
		set @xbint = 0	
end
else
begin
	set @xtouts = @arrear_p + @arrear_i + @arrear_oth
	if @xtouts > 0
		if @xadvint > 0
			set @xbint = 0
		else		
			if @loan_type = 'STL'
				set @xbint = 0
			else
				-- set @xbint = @xbprin * (@rate/100/360) * DATEDIFF(dd, @chkno_date, @pay_start)
				set @xbint = (@xbprin * @rate * DATEDIFF(dd, @chkno_date, @pay_start)) / 36000
	else
		if @xlpay is null or rtrim(ltrim(@xlpay)) =	'' or @xlpay = '01/01/1901'
			-- set @xbint = (@xbprin * (@rate/100/360) * DATEDIFF(dd, @chkno_date, @sysdate)) - @xadvint
			set @xbint = ((@xbprin * @rate * DATEDIFF(dd, @chkno_date, @sysdate)) / 36000) - @xadvint
		else
			-- set @xbint = @xbprin * (@rate/100/360) * DATEDIFF(dd, @xlpay, @sysdate)
			set @xbint = (@xbprin * @rate * DATEDIFF(dd, @xlpay, @sysdate)) / 36000
end

set @xbint = ROUND(@xbint, 2)

if (@arrear_p + @arrear_i + @arrear_oth) > 0
	set @xbint = 0

set @xpint = @arrear_i
set @xbtotal = isnull(@xbprin, 0) + isnull(@xbint, 0) + isnull(@xbarr, 0) + isnull(@xbpen, 0)

if 'DE' like '%' + @mode + '%'
	set @xbfull = isnull(@xbprin, 0) + isnull(@arrear_i, 0) + isnull(@arrear_oth, 0) + isnull(@xlri, 0)
else
	set @xbfull = isnull(@xbprin, 0) + isnull(@xbint, 0) + isnull(@arrear_i, 0) + isnull(@arrear_oth, 0) + isnull(@xlri, 0)

set  @xbpart = isnull(@xbarr, 0) + isnull(@xbpen, 0) + isnull(@p_bal, 0) + isnull(@i_bal, 0) + isnull(@o_bal, 0)

if 'CD' like '%' + @mode + '%'
begin	
	return(@xbfull)
end

return(0)

END





GO

