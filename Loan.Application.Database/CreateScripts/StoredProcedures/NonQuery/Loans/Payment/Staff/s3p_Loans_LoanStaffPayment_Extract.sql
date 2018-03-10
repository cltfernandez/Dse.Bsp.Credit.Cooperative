USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanStaffPayment_Extract]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoanStaffPayment_Extract]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoanStaffPayment_Extract]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanStaffPayment_Extract]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Loans_LoanStaffPayment_Extract]
as

if exists (select top 1 'x' from dbo.SLOANS) delete from dbo.SLOANS

declare	@sysdate date
declare @xday smallint

select	@sysdate = SYSDATE
from	dbo.CTRL

insert	into dbo.SLOANS (
		[PN_NO],
		[KBCI_NO],
		[APP_DATE],
		[DATE_DUE],
		[CHKNO_DATE],
		[MOD_PAY],
		[AMORT_AMT],
		[PAY_START],
		[RATE],
		[TERM],
		[FREQ],
		[PRINCIPAL],
		[LED_TYPE],
		[ACCU_PAYP],
		[LOAN_TYPE],
		[LOAN_STAT],
		[ARREAR_I],
		[ARREAR_P],
		[ARREAR_OTH],
		[ARREAR_AS],
		[P_BAL],
		[I_BAL],
		[O_BAL],
		[ADVANCE],
		[PD],
		xbprin
		)
select
		lon.[PN_NO],
		lon.[KBCI_NO],
		lon.[APP_DATE],
		lon.[DATE_DUE],
		lon.[CHKNO_DATE],
		lon.[MOD_PAY],
		lon.[AMORT_AMT],
		lon.[PAY_START],
		lon.[RATE],
		lon.[TERM],
		lon.[FREQ],
		lon.[PRINCIPAL],
		lon.[LED_TYPE],
		lon.[ACCU_PAYP],
		lon.[LOAN_TYPE],
		lon.[LOAN_STAT],
		lon.[ARREAR_I],
		lon.[ARREAR_P],
		lon.[ARREAR_OTH],
		lon.[ARREAR_AS],
		lon.[P_BAL],
		lon.[I_BAL],
		lon.[O_BAL],
		lon.[ADVANCE],
		lon.[PD],		
		lon.[PRINCIPAL] + led.xbprin
from	dbo.LOANS
			as lon
		inner join dbo.MEMBERS
			as mem
			on mem.KBCI_NO = lon.KBCI_NO
		inner join (
			select	PN_NO,
					sum(case
						when ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ', 'TER') then DR - CR
						else 0
						end) as xbprin
			from	dbo.LEDGER
			group
			by		PN_NO
			)
			as led
			on led.PN_NO = lon.PN_NO
where	lon.LOAN_TYPE != 'STL' and
		not (
			lon.PAY_START > @sysdate and
			DATEPART(M, lon.PAY_START) != DATEPART(M, @sysdate)
		) and
		lon.ACCU_PAYP < lon.PRINCIPAL and
		UPPER(mem.MEM_STAT) = 'S' and
		UPPER(lon.LOAN_STAT) = 'R'

update	dbo.SLOANS
set		xfreq = case
			when FREQ = 'M' then 12
			when FREQ = 'S' then 6
			when FREQ = 'Q' then 4
			when FREQ = 'A' then 1
			else 1
			end

if DATEPART(d, @sysdate) < 8
begin
	update	dbo.SLOANS
	set		I_BAL = ROUND((xbprin * RATE) / (xfreq * 100), 2)
	
	update	dbo.SLOANS
	set		P_BAL = ROUND(AMORT_AMT - I_BAL, 2)
end

set @xday = 7 - DATEPART(d, @sysdate)

update	dbo.SLOANS
set		xint = (xbprin * rate) / (36000)
where	ARREAR_P != 0 or
		ARREAR_I != 0 or
		ARREAR_OTH != 0

update	dbo.SLOANS
set		ARREAR_I = ARREAR_I + (xint * @xday)
where	ARREAR_P != 0 or
		ARREAR_I != 0 or
		ARREAR_OTH != 0

update	dbo.SLOANS
set		ARREAR_AS = NULL,
		ARREAR_P = 0,
		ARREAR_I = 0,
		ARREAR_OTH = 0

update	dbo.SLOANS
set		amount = P_BAL + I_BAL + O_BAL + ARREAR_P + ARREAR_I + ARREAR_OTH
		
update	dbo.LOANS
set		P_BAL = src.P_BAL,
		I_BAL = src.I_BAL,
		ARREAR_P = src.ARREAR_P,
		ARREAR_I = src.ARREAR_I,
		ARREAR_OTH = src.ARREAR_OTH
from	dbo.LOANS
			as tgt
		inner join dbo.SLOANS
			as src
			on src.PN_NO = tgt.PN_NO

GO


