USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_J_Parterm]    Script Date: 07/17/2009 17:38:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_J_Parterm]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_J_Parterm]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_J_Parterm]    Script Date: 07/17/2009 17:38:25 ******/
SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





CREATE    FUNCTION [dbo].[func_J_Parterm]
(
@my_user varchar(8),
@para as integer,
@mode as varchar(1),
@nampien as varchar(7),
@xno_pn as varchar(7),
@lontayp as varchar(3),
@ppayamt as numeric(11, 2) = null,
@pay_tag as varchar(1) = null
)
RETURNS numeric(11, 2)
AS  
BEGIN 

declare @poakey as varchar(7)

declare @xbprin as numeric(11, 2) = 0
declare @xbarr as numeric(11, 2) = 0
declare @xbpen as numeric(11, 2) = 0
declare @xpayment as numeric(11, 2) = 0
declare @xadvint as numeric(11, 2) = 0
declare @xbint as numeric(11, 2) = 0
declare @xbtotal as numeric(11, 2) = 0
declare @xpayamt as numeric(11, 2) = 0
declare @xlri as numeric(11, 2) = 0
declare @xtouts as numeric(11, 2) = 0
declare @xbfull as numeric(11, 2) = 0
declare @xpint as numeric(11, 2) = 0
declare @xbpart as numeric(11, 2) = 0
declare @xppen as numeric(11, 2) = 0
declare @xws as numeric(11, 2) = 0
declare @a2pay as numeric(11, 2) = 0
declare @x as numeric(11, 2) = 0

declare @totpen as numeric(11, 2) = 0
declare @totint as numeric(11, 2) = 0
declare @totpri as numeric(11, 2) = 0
declare @tlri as numeric(11, 2) = 0

-- declare @xno_pn as varchar(7) = ''
declare @kbci_no as varchar(7) = ''

declare @xndue as date
declare @xlastd as date
declare @xlpay as date
declare @poandue as date
declare @pay_start as date
declare @sysdate as date
declare @chkno_date as date
declare @arrear_as as date

declare @xpdtag as bit = 0

declare @loan_type as varchar(3)
declare @rmk as varchar(35) = ''

declare @rate as numeric(7,4) = 0
declare @arrear_p as numeric(11, 2) = 0
declare @arrear_i as numeric(11, 2) = 0
declare @arrear_oth as numeric(11, 2) = 0
declare @lri_due as numeric(11, 2) = 0
declare @p_bal as numeric(11, 2) = 0
declare @i_bal as numeric(11, 2) = 0
declare @o_bal as numeric(11, 2) = 0
declare @accu_payp as numeric(11, 2) = 0

declare @nlri_due as numeric(11, 2) = 0
declare @narrear_oth as numeric(11, 2) = 0
declare @narrear_i as numeric(11, 2) = 0
declare @narrear_p as numeric(11, 2) = 0
declare @naccu_payp as numeric(11, 2) = 0
declare @ni_bal as numeric(11, 2) = 0
declare @np_bal as numeric(11, 2) = 0

declare @narrear_as date

set @poakey = @xno_pn

select	top 1
		@sysdate = SYSDATE
from	dbo.CTRL

select	@xbprin = sum(case
			when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ', 'AMT') then DR - CR
			else 0
			end),
		@xadvint = sum(case
			when ACCT_CODE = 'INT' and ACCT_TYPE = 'INT' then CR
			else 0
			end),
		@xlpay = max(case
			when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ') then [DATE]
			end)
from	dbo.LEDGER
where	PN_NO = @poakey

select	@xbarr = isnull(ARREAR_P, 0) + isnull(ARREAR_I, 0),
		@xbpen = isnull(ARREAR_OTH, 0),
		@xlri = isnull(LRI_DUE, 0),
		@poandue = NDUE,
		@xpdtag = PD,
		@pay_start = PAY_START,
		@chkno_date = CHKNO_DATE,
		@rate = RATE,
		@loan_type = LOAN_TYPE,
		@arrear_p = isnull(ARREAR_P, 0),
		@arrear_i = isnull(ARREAR_I, 0),
		@arrear_oth = isnull(ARREAR_OTH, 0),
		@p_bal = isnull(P_BAL, 0),
		@i_bal = isnull(I_BAL, 0),
		@o_bal = isnull(O_BAL, 0),
		@accu_payp = isnull(ACCU_PAYP, 0),
		@kbci_no = KBCI_NO
from	dbo.LOANS
where	PN_NO = @poakey

if @poandue = @pay_start begin
	if @xlpay is null
		set @xbint = (@xbprin * (@rate/100/360) * DATEDIFF(D, @chkno_date, @sysdate)) - @xadvint
	else
		set @xbint = @xbprin * (@rate/100/360) * DATEDIFF(D, @xlpay, @sysdate)
	
	set @xtouts = @arrear_p + @arrear_i + @arrear_oth
	
	if @xtouts > 0
		set @xbint = 0
		
end else begin
	set @xtouts = @arrear_p + @arrear_i + @arrear_oth
	
	if @xtouts > 0 begin
		if @xadvint > 0 begin
			set @xbint = 0
		end else if @loan_type = 'STL' begin			
			set @xbint = 0
		end
		
	end else begin
		if @xlpay is null begin
			set @xbint = (@xbprin * (@rate/100/360) * DATEDIFF(D, @chkno_date, @sysdate)) - @xadvint
		end else begin
			set @xbint = @xbprin * (@rate/100/360) * DATEDIFF(D, @xlpay, @sysdate)
		end
	end	
end

set @xbint = ROUND(@xbint, 2)
set @xbtotal = @xbprin + @xbint + @xbarr + @xbpen

if @mode in ('D', 'E') begin
	set @xbfull = @xbprin + @arrear_i + @arrear_oth
	set @xpint = @arrear_i
end else begin
	set @xbfull = @xbprin + @xbint + @arrear_i + @arrear_oth + @lri_due
	set @xpint = @xbint + @arrear_i
end

set @xbpart = @xbarr + @xbpen + @p_bal + @i_bal + @o_bal

if @mode in ('C', 'D')
begin
	return(@xbfull)
end

return(0)

END





GO

