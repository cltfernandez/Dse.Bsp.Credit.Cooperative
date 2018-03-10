USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanReversion]    Script Date: 07/02/2009 20:11:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoanReversion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoanReversion]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanReversion]    Script Date: 07/02/2009 20:11:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[s3p_Loans_LoanReversion]
@xtdate date,
@xpn_no varchar(7),
@roth numeric(12,4),
@rint numeric(12,4),
@rpri numeric(12,4),
@opri numeric(12,4),
@oint numeric(12,4),
@date_godue date
as



declare @err int
declare @xsa varchar(10)
declare @xpprin numeric(12,4)
declare @KBCI_NO varchar(7)
declare @LRI_DUE numeric(12,4)
declare @SYSDATE date
declare @DATE_DUE date
declare @LOAN_STAT varchar(1)
declare @PRINCIPAL numeric(12,4)
declare @FD_AMOUNT numeric(13,4)
declare @SD_AMOUNT numeric(13,4)

declare @newPnNo varchar(7)

set @err = 0
set	@xpprin = 0
set @LRI_DUE = 0

begin transaction REVERSE
begin try

	select	top 1 
			@SYSDATE = SYSDATE
	from	dbo.CTRL
	
	select	@KBCI_NO = KBCI_NO
	from	dbo.LOANS
	where	PN_NO = @xpn_no
	
	if exists (select REF from dbo.LEDGER where PN_NO = @xpn_no and ACCT_TYPE = 'TER' and ACCT_CODE = 'PRI')
	begin
		select	@newPnNo = max([REF])
		from	dbo.LEDGER
		where	PN_NO = @xpn_no and 
				ACCT_TYPE = 'TER' and 
				ACCT_CODE = 'PRI'

		delete	dbo.LNHOLD
		where	HOLDDATE = @SYSDATE AND 
				right(HOLDRMKS, 7) = @newPnNo
		
		/* get deducted amounts */
		
		select	@SD_AMOUNT = isnull(SUM([AMOUNT]), 0)
		from	SD
		where	[DATE] = @SYSDATE and
				[REF] = @newPnNo
		
		delete	SD
		where	[DATE] = @SYSDATE and
				[REF] = @newPnNo
		
		--select	@FD_AMOUNT = isnull(SUM(isnull([AMOUNT], 0) * case [DRCR] WHEN 'CR' THEN -1 ELSE 1 END), 0)
		--from	FD
		--where	[DATE] = @SYSDATE and
		--		[REF] = @newPnNo
		
		--delete	FD
		--where	[DATE] = @SYSDATE and
		--		[REF] = @newPnNo

		/* update member amounts */
		update	dbo.MEMBERS
		set		[FD_AMOUNT] = [FD_AMOUNT] + @FD_AMOUNT,
				[SD_AMOUNT] = [SD_AMOUNT] - @SD_AMOUNT
		where	[KBCI_NO] = @KBCI_NO
		
	end
	
	insert into dbo.LEDGEREV (
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
	select	[PN_NO],
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
	from	dbo.LEDGER
	where	PN_NO = @xpn_no and
			[DATE] = @xtdate and
			RMK not like '%PAYROLL DEDUCTION%'

	delete	dbo.LEDGER
	where	PN_NO = @xpn_no and
			[DATE] = @xtdate and
			RMK not like '%PAYROLL DEDUCTION%'

	if exists (select 'x' from dbo.LRIDUE where PN_NO = @xpn_no)
	begin	
			if exists (select 'x' from dbo.RLRIDUE where PN_NO = @xpn_no)	
			begin
					
					delete	dbo.LRIDUE
					where	PN_NO = @xpn_no
					
					insert	into dbo.LRIDUE (
							[PN_NO],
							[KBCI_NO],
							[LRI_DUE],
							[LRI_BALDA],
							[LOAN_BAL],
							[LRI_DUE_C],
							[LRI_DUE_P],
							[LRI_DUE_Y]
					)
					select	top 1
							[PN_NO],
							[KBCI_NO],
							[LRI_DUE],
							[LRI_BALDA],
							[LOAN_BAL],
							[LRI_DUE_C],
							[LRI_DUE_P],
							[LRI_DUE_Y]
					from	dbo.RLRIDUE
					where	PN_NO = @xpn_no
			end
			
			select	@LRI_DUE = LRI_DUE_C
			from	dbo.LRIDUE	
			where	PN_NO = @xpn_no	
	end

	select	@xpprin = SUM(CR - DR)
	from	dbo.LEDGER
	where	PN_NO = @xpn_no and
			ACCT_CODE = 'PRI' and
			ACCT_TYPE in ('ADJ', 'PAY', 'TER')

	select	@PRINCIPAL = PRINCIPAL,
			@LOAN_STAT = LOAN_STAT,
			@DATE_DUE = DATE_DUE
	from	dbo.LOANS
	where	PN_NO = @xpn_no

	if @xpprin >= @PRINCIPAL
	begin
			select	@LOAN_STAT =
					case
					when @LOAN_STAT in ('R', 'P') then 'F'
					else @LOAN_STAT
					end
	end
	else
	begin
			if @LOAN_STAT in ('F', 'T')
			begin
					set @DATE_DUE = @date_godue
					set @LOAN_STAT = 'R'
			end			
	end

	update	dbo.LOANS
	set		ARREAR_OTH = ARREAR_OTH + @roth,
			ARREAR_I = ARREAR_I + @rint,
			ARREAR_P = ARREAR_P + @rpri,
			P_BAL = P_BAL + @opri,
			I_BAL = I_BAL + @oint,
			LRI_DUE = ISNULL(@LRI_DUE, 0),
			ACCU_PAYP = @xpprin,
			PRINCIPAL = @PRINCIPAL,
			LOAN_STAT = @LOAN_STAT,
			CHG_DATE = @SYSDATE,
			DATE_DUE = @DATE_DUE
	where	PN_NO = @xpn_no

	select	@xsa = FEBTC_SA
	from	dbo.MEMBERS
	where	KBCI_NO = @KBCI_NO

	delete	dbo.LNHOLD
	where	ACCTNO = @xsa and
			HOLDRMKS like '%' + @xpn_no + '%' and
			HOLDDATE = @SYSDATE

	delete	dbo.PAYHIST
	where	PN_NO = @xpn_no and 
			PAYDATE = @SYSDATE
			
	commit transaction REVERSE

end try
begin catch

	rollback transaction REVERSE
	raiserror('Reversal cancelled', 16, 1)

end catch



GO

