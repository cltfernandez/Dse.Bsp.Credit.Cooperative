USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Voucher_LoanApplication]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Voucher_LoanApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Voucher_LoanApplication]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Voucher_LoanApplication]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Voucher_LoanApplication]
@pn_no varchar(7),
@my_user VARCHAR(50)
AS

declare @sysdate datetime
declare @total numeric(12,4)

select
	@sysdate = SYSDATE
from
	dbo.CTRL

select
	@total = sum(led.DR - led.CR)
from
	dbo.LOANS lon
		inner join
	dbo.LEDGER led
		on lon.PN_NO = led.PN_NO
where
	lon.PN_NO = @pn_no
	and 
	(
		led.RMK like '%INIT%'
		or led.REF like '%INIT%'
	)

select
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) KBCI,
	dbo.func_Format241(lon.PN_NO) PN_NO,
	dbo.func_Format241(lon.KBCI_NO) KBCI_NO,
	lon.CHKNO_DATE,
	led.DR,
	led.CR,
	dbo.func_Format451(mem.FEBTC_SA) FEBTC_SA,
	case lon.L_EXT
		when 1 then 1
		when 0 then 0
		end L_EXT,
	lon.CHKNO,
	lon.CHKNO_BANK,
	@my_user MY_USER,
	@total TOTAL,
	dbo.func_NumberToWord(@total) TOTAL_WORDS,		
	replace(isnull(p.LOAN_DESC, ''), ' LOAN', '') as LOAN_TYPE,
	case
		when lon.LOAN_TYPE = 'SML' then 'Zero Percent'
		when lon.LED_TYPE = 0 then 'Diminishing Prin'
		when lon.LED_TYPE = 1 then 'Advance Interest'
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
	case lon.MOD_PAY
		when '1' then 'Payroll'
		when '2' then 'PDC'
		when '3' then 'DM'
		when '4' then 'Lump Sum'
		end TERM,
	lon.ADV_INTE,
	lon.RATE,
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
	dbo.func_FullName(com1.LNAME, com1.FNAME, com1.MI) as comaker1,
	dbo.func_FullName(com2.LNAME, com2.FNAME, com2.MI) as comaker2,
	lon.COLLATERAL,
	@sysdate SYSDATE,
	case
		when lon.LOAN_TYPE != 'STL' and lon.DATE_GRANT >= '2005-04-19' and lon.FREQ = 'M' and lon.TERM = 12 then 1
		else 0
		end Flag,
	case
		when led.ACCT_TYPE = 'AMT' then 'Loan Principal'
		when led.ACCT_TYPE = 'INT' then 'Additional Interest'
		when led.ACCT_TYPE = 'SC'  then 'Service Charge'
		when led.ACCT_TYPE = 'LRI' and led.RMK like '%INITIAL%' then 'Loan Redemption Insurance'
		when led.ACCT_TYPE = 'LRI' and led.RMK like '%INIT-%' then led.RMK
		when led.ACCT_TYPE = 'MSC' then 'Misc. Liabilities'
		when led.ACCT_TYPE = 'SD'  then 'Savings Deposit Requirement'
		when led.ACCT_TYPE = 'FD'  then 'Fixed Deposit Requirement'
		when led.ACCT_TYPE = 'INI' then REPLACE(led.RMK, 'INIT-', '')
		when led.REF like '%INITIAL%' OR led.RMK like '%INITIAL%' OR led.RMK like '%INIT-%' then REPLACE(led.RMK, 'INIT-', '')
		else led.ACCT_TYPE + ' ' + led.ACCT_CODE
		end [Description]
from
	dbo.LOANS lon
		left join
	dbo.LOAN_TYPE p on
		p.LOAN_TYPE = lon.LOAN_TYPE
		inner join
	dbo.MEMBERS mem on
		lon.KBCI_NO = mem.KBCI_NO
		inner join
	dbo.LEDGER led on
		lon.PN_NO = led.PN_NO
		left join
	dbo.COMAKER com on
		com.PN_NO = lon.PN_NO
		left join
	dbo.MEMBERS com1 on
		com.COMAKER1 = com1.KBCI_NO
		left join
	dbo.MEMBERS com2 on
		com.COMAKER2 = com2.KBCI_NO
where
	lon.PN_NO = @pn_no and
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
	lon.PN_NO,
	led.ADD_DATE



GO