USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Voucher_Release]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Voucher_Release]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Voucher_Release]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Voucher_Release]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Report_Voucher_Release]
@pn_no varchar(7),
@my_user varchar(50),
@pn_no_end varchar(7) = null
AS

declare @sysdate datetime;
declare @total numeric(12,4)

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock);

select
	@total = sum(led.DR - led.CR)
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.LEDGER led with(nolock) on
		lon.PN_NO = led.PN_NO
where
	lon.PN_NO = @pn_no
	and 
	(
		led.RMK like '%INIT%'
		or led.REF like '%INIT%'
	)

select
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	dbo.func_Format241(lon.PN_NO) PN_NO,
	dbo.func_Format241(lon.KBCI_NO) KBCI_NO,
	lon.CHKNO_DATE,
	dbo.func_Format451(mem.FEBTC_SA) FEBTC_SA,
	case lon.L_EXT
		when 1 then 1
		when 0 then 0
		end L_EXT,
	lon.CHKNO,
	lon.CHKNO_BANK,
	@my_user MY_USER,
	@total TOTAL,
	lower(dbo.func_NumberToWord(@total)) TOTAL_WORDS,
	replace(isnull(p.LOAN_DESC, ''), ' Loan', '') as LOAN_TYPE,
	case lon.LED_TYPE
		when 0 then 'Diminishing Principal'
		when 1 then 'Advance Interest'
		else ''
		end as LED_TYPE,
	convert(varchar(3), lon.TERM ) + 
	case 
		when lon.FREQ = 'D' then ' days '
		when lon.FREQ = 'A' and lon.TERM > 1 then ' years '
		when lon.FREQ = 'A' and lon.TERM = 1 then ' year '
		when lon.FREQ = 'M' and lon.TERM > 1 then ' months '
		when lon.FREQ = 'M' and lon.TERM = 1 then ' month '
		else ' other '
		end +
	case lon.LOAN_TYPE
		when 'BFL' then 'Offcycle'
		else
			case lon.MOD_PAY
				when '1' then 'Payroll'
				when '2' then 'PDC'
				when '3' then 'DM'
				when '4' then 'Lump Sum'
				end
		end TERM,
	lon.ADV_INTE,
	case lon.LOAN_TYPE
		when 'BFL' then 1
		else lon.RATE
		end as RATE,
	lon.AFT_INTE,
	lon.AMORT_AMT,
	(
		datename(DW, lon.DATE_DUE) + '; ' +
		datename(MM, lon.DATE_DUE) + ' ' +
		datename(D, lon.DATE_DUE) + ', ' +
		datename(YYYY, lon.DATE_DUE)
	) DATE_DUE,
	case lon.freq
		when 'D' then 'Daily'
		when 'A' then 'Annualy'
		when 'M' then 'Monthly'
		else 'Others'
		end FREQ,
	dbo.func_Format241(com.comaker1) + ' ' + isnull(com1.LNAME + ', ', '') + isnull(com1.FNAME + ' ', '') + isnull(com1.MI + '.', '') as comaker1,
	dbo.func_Format241(com.comaker2) + ' ' + isnull(com2.LNAME + ', ', '') + isnull(com2.FNAME + ' ', '') + isnull(com2.MI + '.', '') as comaker2,
	lon.COLLATERAL,
	@sysdate SYSDATE,
	case
		when lon.LOAN_TYPE != 'STL' and lon.DATE_GRANT >= '2005-04-19' and lon.FREQ = 'M' and lon.TERM = 12 then 1
		else 0
		end FLAG,
	led.DR,
	case
		when led.ACCT_TYPE = 'AMT' then 'Loan Principal'
		when led.ACCT_TYPE = 'INT' then 'Additional Interest'
		when led.ACCT_TYPE = 'SC'  then 'Service Charge'
		when led.ACCT_TYPE = 'LRI' and led.RMK like '%INITIAL%' then 'Loan Redemption Insurance'
		when led.ACCT_TYPE = 'LRI' and led.RMK like '%INIT-%' then led.RMK
		when led.ACCT_TYPE = 'MSC' then 'Misc. Liabilities'
		when led.ACCT_TYPE = 'SD'  then 'Savings Deposit Requirement'
		--when led.ACCT_TYPE = 'FD'  then 'Fixed Deposit Requirement'
		when led.ACCT_TYPE = 'FD'  then 'Share Capital Requirement'
		when led.ACCT_TYPE = 'INI' then REPLACE(led.RMK, 'INIT-', '')
		when led.REF like '%INITIAL%' or led.RMK like '%INITIAL%' or led.RMK like '%INIT-%' then replace(led.RMK, 'INIT-', '')
		else led.ACCT_TYPE + ' ' + led.ACCT_CODE
		end LED_DESC,
	led.CR,
	@total TOTAL,
	case lon.L_EXT
		when 1 then 1
		when 0 then 0
		end L_EXT,
	(
		lon.CHKNO +
		case
			when @total = 0 and lon.LOAN_TYPE != 'STL' then ' (no cash out)'
			else ''
			end
	) AS CHKNO,
	lon.CHKNO_BANK
from
	dbo.LOANS lon with(nolock)
		left join
	dbo.LOAN_TYPE p with(nolock)
		on p.LOAN_TYPE = lon.LOAN_TYPE
		inner join
	dbo.MEMBERS mem with(nolock)
		on lon.KBCI_NO = mem.KBCI_NO
		left join
	dbo.COMAKER com with(nolock)
		on com.PN_NO = lon.PN_NO
		left join
	dbo.MEMBERS com1 with(nolock)
		on com.COMAKER1 = com1.KBCI_NO
		left join
	dbo.MEMBERS com2 with(nolock)
		on com.COMAKER2 = com2.KBCI_NO
		inner join
	dbo.LEDGER led with(nolock)
		on led.PN_NO = lon.PN_NO
where
	lon.PN_NO between @pn_no and isnull(@pn_no_end, @pn_no) and
	(
		isnull(led.CR, 0) > 0 or
		isnull(led.DR, 0) > 0
	)
	and 
	(
		led.RMK like '%INIT%'
		or led.REF like '%INIT%'
	)
order by
	lon.PN_NO



GO