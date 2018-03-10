USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanStaffPayment_Post]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoanStaffPayment_Post]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoanStaffPayment_Post]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanStaffPayment_Post]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*************************************************
JS	05/01/2014	Recompute balance
*************************************************/


CREATE PROCEDURE [dbo].[s3p_Loans_LoanStaffPayment_Post]
@MY_USER VARCHAR(8)
as

declare @PN_NO varchar(7)
declare @PRINCIPAL numeric(12,4)
declare @ACCU_PAYP numeric(12,4)
declare @ARREAR_I numeric(12,4)
declare @ARREAR_P numeric(12,4)
declare @ARREAR_OTH numeric(12,4)
declare @P_BAL numeric(12,4)
declare @I_BAL numeric(12,4)
declare @O_BAL numeric(12,4)
declare @LOAN_STAT varchar(1)
declare @ADVANCE numeric(12,4)
declare @amount numeric(12,4)

declare @pbalans numeric(12,4)
declare @xbprin numeric(12,4)
declare @xppay numeric(12,4)
declare @xpen numeric(12,4)
declare @a2pay numeric(12,4)
declare @x numeric(12,4)
declare @xiarr numeric(12,4)
declare @xparr numeric(12,4)
declare @xiouts numeric(12,4)
declare @xpouts numeric(12,4)

declare @sysdate date

declare @LEDGER table (
	PN_NO varchar(7),
	PBALANS numeric(12,4)
)

insert	@LEDGER (PN_NO, PBALANS)
select	lon.PN_NO,
		SUM(case
			when ACCT_TYPE in ('PAY', 'ADJ', 'TER') and ACCT_CODE = 'PRI' then ISNULL(DR,0) - ISNULL(CR,0)
			else 0
			end)
from	dbo.SLOANS
			as lon
		inner join dbo.LEDGER
			as led
			on led.PN_NO = lon.PN_NO
group
by		lon.PN_NO

select	@sysdate = SYSDATE
from	dbo.CTRL

declare SLOANS_CURSOR cursor for
select	PN_NO,
		PRINCIPAL,
		ACCU_PAYP,
		ARREAR_I,
		ARREAR_P,
		ARREAR_OTH,
		P_BAL,
		I_BAL,
		O_BAL,
		LOAN_STAT,
		ADVANCE,
		amount
from	dbo.SLOANS

open	SLOANS_CURSOR

fetch	SLOANS_CURSOR
into	@PN_NO,
		@PRINCIPAL,
		@ACCU_PAYP,
		@ARREAR_I,
		@ARREAR_P,
		@ARREAR_OTH,
		@P_BAL,
		@I_BAL,
		@O_BAL,
		@LOAN_STAT,
		@ADVANCE,
		@amount

while @@FETCH_STATUS = 0
begin
	select	@pbalans = PBALANS + @PRINCIPAL
	from	@LEDGER
	where	PN_NO = @PN_NO
	
	if @pbalans > 0 begin
		set @xbprin = @PRINCIPAL - @ACCU_PAYP
		set @xppay = @amount
		set @xpen = @ARREAR_OTH
		
		if @xpen > 0 and @xppay > 0 begin
			set @xppay = @xppay - @xpen
			
			if @xppay < 0 begin
				set @a2pay = @xppay + @xpen
				set @xppay = 0
			end
			else begin
				set @a2pay = @xpen
			end
			
			if @a2pay > 0 begin
				exec dbo.s3p_J_U_Ledger @PN_NO, @sysdate, 'CM', @PN_NO, 'PAY', 'OTH', @a2pay, 'LOAN PENALTY', NULL, @MY_USER
				set @x = @ARREAR_OTH - @a2pay
				
				if @x > 0 begin
					set @ARREAR_OTH = @ARREAR_OTH - @a2pay
				end
				else begin
					set @ARREAR_OTH = 0
				end
			end
		end
		
		set @xiarr = @ARREAR_I
		
		if @xiarr > 0 and @xppay > 0 begin
			set @xppay = @xppay - @xiarr
			
			if @xppay < 0 begin
				set @a2pay = @xppay + @xiarr
				set @xppay = 0
			end
			else begin
				set @a2pay = @xiarr
			end
			
			if @a2pay > 0 begin
				exec dbo.s3p_J_U_Ledger @PN_NO, @sysdate, 'CM', @PN_NO, 'PAY', 'INT', @a2pay, 'LOAN ARREAR-INT', NULL, @MY_USER
				set @x = @ARREAR_I - @a2pay
				
				if @x > 0 begin
					set @ARREAR_I = @ARREAR_I - @a2pay
				end
				else begin
					set @ARREAR_I = 0
				end
			end
		end
		
		set @xparr = @ARREAR_P
		
		if @xparr > 0 and @xppay > 0 begin
			set @xppay = @xppay - @xparr
			
			if @xppay < 0 begin
				set @a2pay = @xppay + @xparr
				set @xppay = 0
			end
			else begin
				set @a2pay = @xparr
			end
			
			if @a2pay > 0 begin
				exec dbo.s3p_J_U_Ledger @PN_NO, @sysdate, 'CM', @PN_NO, 'PAY', 'PRI', @a2pay, 'LOAN ARREAR-PRI', NULL, @MY_USER
				set @x = @ARREAR_P - @a2pay
				
				if @x > 0 begin
					set @ARREAR_P = @ARREAR_P - @a2pay
				end
				else begin
					set @ARREAR_P = 0
				end
				
				set @xbprin = @xbprin - @a2pay
			end
		end
		
		set @xiouts = @I_BAL
		
		if @xiouts > 0 and @xppay > 0 begin
			set @xppay = @xppay - @xiouts
			
			if @xppay < 0 begin
				set @a2pay = @xppay + @xiouts
				set @xppay = 0
			end
			else begin
				set @a2pay = @xiouts
			end
			
			if @a2pay > 0 begin
				exec dbo.s3p_J_U_Ledger @PN_NO, @sysdate, 'CM', @PN_NO, 'PAY', 'INT', @a2pay, 'PAYROLL DEDUCTION AMORT-INT', NULL, @MY_USER
				set @x = @I_BAL - @a2pay
				
				if @x > 0 begin
					set @I_BAL = @I_BAL - @a2pay
				end
				else begin
					set @I_BAL = 0
				end
			end
		end
		
		set @xpouts = @P_BAL
		
		if @xpouts > 0 and @xppay > 0 begin
			set @xppay = @xppay - @xpouts
			
			if @xppay < 0 begin
				set @a2pay = @xppay + @xpouts
				set @xppay = 0
			end
			else begin
				set @a2pay = @xpouts
			end
			
			if @a2pay > 0 begin
				exec dbo.s3p_J_U_Ledger @PN_NO, @sysdate, 'CM', @PN_NO, 'PAY', 'PRI', @a2pay, 'PAYROLL DEDUCTION AMORT-PRI', NULL, @MY_USER
				set @x = @P_BAL - @a2pay
				
				if @x > 0 begin
					set @P_BAL = @P_BAL - @a2pay
				end
				else begin
					set @P_BAL = 0
				end
				
				set @xbprin = @xbprin - @a2pay
			end
		end
		
		set @ACCU_PAYP = @PRINCIPAL - @xbprin
		
		if @xbprin > 0 and @xppay > 0 begin
			set @xppay = @xppay - @xbprin
			
			if @xppay < 0 begin
				set @a2pay = @xppay + @xbprin
				set @xppay = 0
			end
			else begin
				set @a2pay = @xbprin
			end
			
			if @a2pay > 0 begin
				exec dbo.s3p_J_U_Ledger @PN_NO, @sysdate, 'CM', @PN_NO, 'PAY', 'PRI', @a2pay, 'LOAN PAY PRINCIPAL', NULL, @MY_USER
				set @ACCU_PAYP = @ACCU_PAYP + @a2pay
			end
		end
		
		if @ACCU_PAYP >= @PRINCIPAL begin
			set @LOAN_STAT = 'F'
			set @P_BAL = 0
			set @I_BAL = 0
			set @O_BAL = 0
			set @ARREAR_P = 0
			set @ARREAR_I = 0
			set @ARREAR_OTH = 0
			set @ADVANCE = @xppay
			
			update	dbo.LOANS
			set		LOAN_STAT = @LOAN_STAT,
					ADVANCE = @ADVANCE
			where	PN_NO = @PN_NO
		end
		
		update	dbo.LOANS
		set		ARREAR_OTH = @ARREAR_OTH,
				ARREAR_I = @ARREAR_I,
				ARREAR_P = @ARREAR_P,
				I_BAL = @I_BAL,
				P_BAL = @P_BAL,
				ACCU_PAYP = @ACCU_PAYP
		where	PN_NO = @PN_NO
	end

	fetch	SLOANS_CURSOR
	into	@PN_NO,
			@PRINCIPAL,
			@ACCU_PAYP,
			@ARREAR_I,
			@ARREAR_P,
			@ARREAR_OTH,
			@P_BAL,
			@I_BAL,
			@O_BAL,
			@LOAN_STAT,
			@ADVANCE,
			@amount

end

close SLOANS_CURSOR
deallocate SLOANS_CURSOR

declare SLOANS_COMPUTE_CURSOR cursor for									-- JS 05/01/2014
select	PN_NO																--		|
from	dbo.SLOANS															--		|
																			--		|
open	SLOANS_COMPUTE_CURSOR												--		|
																			--		|
fetch	SLOANS_COMPUTE_CURSOR												--		|
into	@PN_NO																--		|
																			--		|
while @@FETCH_STATUS = 0													--		|
begin																		--		|
	exec s3p_Loans_LoansNew_RecomputeBalance @PN_NO							--		|
																			--		|
	fetch	SLOANS_COMPUTE_CURSOR											--		|
	into	@PN_NO															--		|
end																			--		|
																			--		|
close SLOANS_COMPUTE_CURSOR													--		|
deallocate SLOANS_COMPUTE_CURSOR											-- JS 05/01/2014

if exists (select top 1 'x' from dbo.SLOANS) delete from dbo.SLOANS

GO


