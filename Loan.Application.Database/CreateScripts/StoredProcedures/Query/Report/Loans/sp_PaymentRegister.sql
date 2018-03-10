USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_PaymentRegister]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_PaymentRegister]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_PaymentRegister]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_PaymentRegister]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[Report_Loans_PaymentRegister]
@pn_no VARCHAR(7)
AS

declare @sysdate date

select
	top 1
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

select
	pay.PAYDATE,
	case pay.paytype
		when 1 then 'OTC'
		when 2 then 'DM'
		when 3 then 'PDC'
		when 4 then 'DEDN'
		else ''
		end as [PAYTYPE],
	pay.PAYAMT,
	pay.ADDATE,
	pay.UPDUSER,
	pay.PAYOR,
	pay.PAYREM,
	dbo.func_Format241(lon.KBCI_NO) + ' ' + dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
	dbo.func_Format241(lon.PN_NO) + ' ' + upper(isnull(p.LOAN_DESC, '')) as LOAN_DESC,
	@sysdate as SYSDATE
from
	dbo.LOANS lon with(nolock)
		left join
	dbo.LOAN_TYPE p with(nolock)
		on p.LOAN_TYPE = lon.LOAN_TYPE
		inner join		
	dbo.PAYHIST pay with(nolock)
		on lon.PN_NO = pay.PN_NO
		inner join
	dbo.MEMBERS mem with(nolock)
		on lon.KBCI_NO = mem.KBCI_NO
where
	lon.PN_NO = @pn_no
order by
	ADDATE




GO