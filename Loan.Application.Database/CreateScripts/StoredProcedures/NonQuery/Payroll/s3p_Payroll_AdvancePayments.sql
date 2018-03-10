USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_AdvancePayments]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Payroll_AdvancePayments]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Payroll_AdvancePayments]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_AdvancePayments]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Payroll_AdvancePayments]
@COMMAND varchar(20),
@ADVANCE_ID bigint = null,
@AMOUNT numeric(9, 2) = null,
@REMARKS varchar(60) = null,
@DATE1 date = null,
@DATE2 date = null,
@MY_USER varchar(8)
as

if @COMMAND = 'DELETE' begin 
	delete	dbo.ADVANCE
	where	ADVANCE_ID = @ADVANCE_ID
	
end else if @COMMAND = 'EDIT' begin
	update	dbo.ADVANCE
	set		AMOUNT = @AMOUNT,
			REMARKS = @REMARKS,
			CHG_DATE = GETDATE(),
			[USER] = @MY_USER
	where	ADVANCE_ID = @ADVANCE_ID
	
end else if @COMMAND = 'UPDATE' begin
	update	dbo.ADVANCE
	set		ACCTNO = b.FEBTC_SA,
			CHG_DATE = GETDATE(),
			[USER] = @MY_USER
	from	ADVANCE a, MEMBERS b
	where	a.KBCI_NO = b.KBCI_NO and
			a.ADVANCE_ID = @ADVANCE_ID

end else if @COMMAND = 'EXTRACT-1' begin
	update	dbo.ADVANCE
	set		ACCTNO = b.FEBTC_SA
	from	ADVANCE a, MEMBERS b
	where	a.KBCI_NO = b.KBCI_NO and
			a.[STATUS] != '2' and
			a.ADD_DATE between @DATE1 and @DATE2 and
			a.LOAN_TYPE in (SELECT LOAN_TYPE FROM dbo.LOAN_TYPE)

	select	convert(varchar,cast((sum(a.AMOUNT)) as money), 1) as tsum,
			count(a.AMOUNT) as tcnt
	from	dbo.ADVANCE a
				inner join
			dbo.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	a.[STATUS] != '2' and
			a.ADD_DATE between @DATE1 and @DATE2 and
			a.LOAN_TYPE in (SELECT LOAN_TYPE FROM dbo.LOAN_TYPE)

end else if @COMMAND = 'EXTRACT-2' begin
	delete	dbo.EXTRACT
	
	insert	dbo.EXTRACT (
			PN_NO,
			KBCI_NO,
			LOAN_TYPE,
			ADD_DATE,
			CHG_DATE,
			[USER],
			AMOUNT,
			REMARKS,
			SAVINGS,
			ACCTNO,
			[STATUS]
	)
	select	a.PN_NO,
			a.KBCI_NO,
			a.LOAN_TYPE,
			a.ADD_DATE,
			a.CHG_DATE,
			a.[USER],
			a.AMOUNT,
			a.REMARKS,
			a.SAVINGS,
			a.ACCTNO,
			a.[STATUS]
	from	dbo.ADVANCE a
				inner join
			dbo.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	a.[STATUS] != '2' and
			a.ADD_DATE between @DATE1 and @DATE2 and
			a.LOAN_TYPE in (SELECT LOAN_TYPE FROM dbo.LOAN_TYPE)
	
	select	convert(varchar,cast((sum(AMOUNT)) as money), 1) as tsum,
			count(AMOUNT) as tcnt
	from	dbo.EXTRACT

end

GO


