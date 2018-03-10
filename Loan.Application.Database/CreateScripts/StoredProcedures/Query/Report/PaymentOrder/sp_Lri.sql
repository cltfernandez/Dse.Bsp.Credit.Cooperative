USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_PaymentOrder_Lri]    Script Date: 05/04/2013 16:19:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_PaymentOrder_Lri]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_PaymentOrder_Lri]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_PaymentOrder_Lri]    Script Date: 05/04/2013 16:19:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_PaymentOrder_Lri]
@PN_NO varchar(7)
AS

select
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	dbo.func_Format241(mem.KBCI_NO) as KBCI_NO,
	dbo.func_Format451(mem.FEBTC_SA) as FEBTC_SA,
	lon.COLLATERAL,
	lon.PD,
	lon.LOAN_TYPE,
	dbo.func_Format241(lon.PN_NO) as PN_NO,
	lon.PRINCIPAL,
	lon.DATE_GRANT,
	lon.DATE_DUE,
	lon.RATE,
	lri.LRI_DUE,
	LRI_DUE_P,
	sum
	(
		case 
			when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT') then ISNULL(led.DR, 0) - ISNULL(led.CR, 0)
			else 0
			end
	) XBPRIN
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.LRIDUE lri with(nolock) on
		lri.PN_NO = lon.PN_NO
		inner join
	dbo.MEMBERS mem with(nolock) on
		mem.KBCI_NO = lon.KBCI_NO
		inner join
	dbo.LEDGER led with(nolock) on
		led.PN_NO = lon.PN_NO
where
	lon.PN_NO = @PN_NO
group by
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI),
	dbo.func_Format241(mem.KBCI_NO),
	dbo.func_Format451(mem.FEBTC_SA),
	lon.COLLATERAL,
	lon.PD,
	lon.LOAN_TYPE,
	dbo.func_Format241(lon.PN_NO),
	lon.PRINCIPAL,
	lon.DATE_GRANT,
	lon.DATE_DUE,
	lon.RATE,
	lon.PD,
	lri.LRI_DUE,
	lri.LRI_DUE_P

GO