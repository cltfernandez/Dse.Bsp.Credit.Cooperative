USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_Archive]    Script Date: 07/15/2009 11:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Admin_Archive]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Admin_Archive]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_Archive]    Script Date: 07/15/2009 11:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Do_Admin_Archive]
@cur_off_date date = null
AS


if @cur_off_date is null
	select	top 1 @cur_off_date = dateadd(D, -360, SYSDATE)
	from	dbo.CTRL

insert	ALOANS 
(
	[PN_NO],
	[KBCI_NO],
	[APP_DATE],
	[APP_NO],
	[DATE_GRANT],
	[BY_WHOM],
	[DATE_DUE],
	[CHKNO_BANK],
	[CHKNO],
	[CHKNO_AMT],
	[CHKNO_DATE],
	[CHKNO_RELS],
	[CHKNO_ACK],
	[MOD_PAY],
	[AMORT_AMT],
	[PAY_START],
	[RATE],
	[TERM],
	[FREQ],
	[PRINCIPAL],
	[LED_TYPE],
	[ADV_INTE],
	[AFT_INTE],
	[ACCU_PAYP],
	[YTD_I],
	[LOAN_TYPE],
	[LOAN_STAT],
	[ARREAR_I],
	[ARREAR_P],
	[ARREAR_OTH],
	[ARREAR_AS],
	[COLLATERAL],
	[DED_BAL],
	[ADD_DATE],
	[CHG_DATE],
	[USER],
	[P_BAL],
	[I_BAL],
	[O_BAL],
	[REC_STAT],
	[RENEW],
	[ADVANCE],
	[LRI_IND],
	[NDUE]
)
select
	[PN_NO],
	[KBCI_NO],
	[APP_DATE],
	[APP_NO],
	[DATE_GRANT],
	[BY_WHOM],
	[DATE_DUE],
	[CHKNO_BANK],
	[CHKNO],
	[CHKNO_AMT],
	[CHKNO_DATE],
	[CHKNO_RELS],
	[CHKNO_ACK],
	[MOD_PAY],
	[AMORT_AMT],
	[PAY_START],
	[RATE],
	[TERM],
	[FREQ],
	[PRINCIPAL],
	[LED_TYPE],
	[ADV_INTE],
	[AFT_INTE],
	[ACCU_PAYP],
	[YTD_I],
	[LOAN_TYPE],
	[LOAN_STAT],
	[ARREAR_I],
	[ARREAR_P],
	[ARREAR_OTH],
	[ARREAR_AS],
	[COLLATERAL],
	[DED_BAL],
	[ADD_DATE],
	[CHG_DATE],
	[USER],
	[P_BAL],
	[I_BAL],
	[O_BAL],
	[REC_STAT],
	[RENEW],
	[ADVANCE],
	[LRI_IND],
	[NDUE]
from
	dbo.LOANS
where
	LOAN_STAT != 'R'

insert	ARCHIVE
(
	[PN_NO],
	[KBCI_NO]
)
select
	[PN_NO],
	[KBCI_NO]
from
	dbo.LOANS
where
	LOAN_STAT != 'R'

insert	ALEDGER 
(
	[PN_NO],
	[DATE],
	[DOX_TYPE],
	[REF],
	[ACCT_TYPE],
	[ACCT_CODE],
	[BEGBAL],
	[DR],
	[CR],
	[ENDBAL],
	[RMK],
	[ADD_DATE],
	[USER]
)
select
	led.[PN_NO],
	led.[DATE],
	led.[DOX_TYPE],
	led.[REF],
	led.[ACCT_TYPE],
	led.[ACCT_CODE],
	led.[BEGBAL],
	led.[DR],
	led.[CR],
	led.[ENDBAL],
	led.[RMK],
	led.[ADD_DATE],
	led.[USER]
from
	dbo.LEDGER led
		inner join
	dbo.ARCHIVE arc
		on led.PN_NO = arc.PN_NO

delete
	dbo.LOANS
where
	LOAN_STAT != 'R'

delete
	led
from
	dbo.LEDGER led
where
	exists
	(
		select
			'X'
		from
			dbo.ARCHIVE
		where
			PN_NO = led.PN_NO
	)




GO