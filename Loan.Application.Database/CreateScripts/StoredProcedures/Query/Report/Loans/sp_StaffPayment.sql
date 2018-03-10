USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_StaffPayment]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_StaffPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_StaffPayment]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_StaffPayment]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Loans_StaffPayment]
as

declare @sysdate date

select
	@sysdate = convert(date, datename(YYYY, SYSDATE) + '-' + convert(varchar(2), datepart(MM, SYSDATE)) + '-07')
from
	dbo.CTRL with(nolock)

select	
	dbo.func_format241(lon.KBCI_NO) as KBCI_NO,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) as NAME,
	lon.P_BAL,
	lon.I_BAL,
	dbo.func_format241(lon.PN_NO) as PN_NO,
	lon.LOAN_TYPE,
	@sysdate sysdate
from
	dbo.SLOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		mem.KBCI_NO = lon.KBCI_NO
order by
	lon.KBCI_NO,
	lon.PN_NO



GO


