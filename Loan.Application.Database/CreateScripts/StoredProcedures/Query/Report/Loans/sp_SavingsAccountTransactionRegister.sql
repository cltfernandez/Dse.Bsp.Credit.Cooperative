USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_SavingsAccountTransactionRegister]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_SavingsAccountTransactionRegister]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_SavingsAccountTransactionRegister]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_SavingsAccountTransactionRegister]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Report_Loans_SavingsAccountTransactionRegister]
@dt1 date,
@dt2 date
AS

declare @sysdate datetime

select
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

select	@sysdate SYSDATE,
		dbo.func_Format451(sdm.ACCTNO) ACCTNO,
		sdm.ACCTNAME,
		lnh.HOLDCD,
		lnh.HOLDTYPE,		
		lnh.HOLDAMT,
		case lnh.HOLDTYPE
			when 'DM' then lnh.HOLDAMT
			else 0
			end DEBIT,
		case lnh.HOLDTYPE
			when 'DM' then 0
			else lnh.HOLDAMT
			end CREDIT,
		lnh.HOLDDATE,
		lnh.HOLDUSER,
		lnh.HOLDRMKS,
		lnh.POSTSTAT,
		lnh.POSTDATE,
		lnh.POSTUSER,
		@dt1 DT1,
		@dt2 DT2
from	dbo.LNHOLD lnh with(nolock)
			inner join
		dbo.SDMASTER sdm with(nolock)
			on lnh.ACCTNO = sdm.ACCTNO
where	lnh.HOLDDATE between @dt1 and @dt2
		
order
by		lnh.HOLDDATE, sdm.ACCTNO




GO