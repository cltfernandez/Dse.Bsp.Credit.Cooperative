USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_GetDetails]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_GetDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_GetDetails]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_GetDetails]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Loans_GetDetails]
@vCode varchar(20),
@vType varchar(20),
@dtDate date
as

declare @XBPRIN numeric(10, 2)
declare @XPINT numeric(10, 2)
declare @XOPRI numeric(10, 2)
declare @XLASTD date
declare @XLASPD date
declare @SYSDATE date
declare @AXGOMO int
declare @AXFREQ int

if @vType = 'LOANS'
	select	*
	from	dbo.LOANS
	where	PN_NO = @vCode

else if @vType = 'MEMBERS'
	select	*
	from	dbo.MEMBERS
	where	KBCI_NO = @vCode

else if @vType = 'LB-1'
	select	LOANS_ID, KBCI_NO, PN_NO, LOAN_TYPE, LOAN_STAT as cboLoanStatus, DATE_DUE, PRINCIPAL
	from	DBO.LOANS
	where	KBCI_No = @vCode AND
			LOAN_STAT = 'R'	
			
else if @vType = 'LB-2'
	select	top 1 
			a.LOANS_ID, 
			a.KBCI_NO, 
			a.PN_NO, 
			a.LOAN_TYPE as cboLoanType, 
			a.LOAN_STAT as cboLoanStatus, 
			a.DATE_DUE, 
			a.PRINCIPAL, 
			a.TERM as txtTerm, 
			a.RATE as txtRate, 
			a.FREQ as cboFrequency, 
			a.AMORT_AMT as txtAmortization, 
			a.MOD_PAY as cboPaymentMode, 
			a.PD, 
			a.CHKNO_DATE as txtReleaseDate, 
			a.PAY_START as txtPayStart, 
			a.DATE_DUE as txtDateDue, 			
			a.NDUE as txtNewDueDate,
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.', '') MEMBER
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	a.PN_No = @vCode

else if @vType = 'LB-3'
	select	top 1
			a.LOANS_ID, 
			isnull(b.FNAME + ' ', '') + isnull(b.MI + '. ', '') + isnull(b.LNAME, '') MEMBERNAME,
			a.KBCI_NO,
			b.FEBTC_SA,
			a.COLLATERAL,
			a.LOAN_TYPE cboLoanType,
			a.LOAN_STAT cboLoanStatus,
			a.PN_NO,
			a.DATE_GRANT,
			a.DATE_DUE,
			a.RATE,
			a.ARREAR_AS,
			a.ARREAR_P,
			a.ARREAR_I,
			a.ARREAR_OTH,
			a.O_BAL,
			(select top 1 SYSDATE from DBO.CTRL) SYSDATE
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	a.PN_NO = @vCode

else if @vType = 'LB-4'
	select	a.LOANS_ID, 
			a.KBCI_NO, 
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.', '') MEMBER,
			a.PN_NO, 
			a.LOAN_TYPE, 
			a.LOAN_STAT, 
			a.DATE_DUE, 
			a.PRINCIPAL
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	ORDER	BY MEMBER, PN_NO

else if @vType = 'LC-1'
begin	
	select	@XBPRIN = SUM(case 
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ', 'TER', 'REP') THEN DR
				else 0
				end) -
			SUM(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ', 'TER', 'REP') THEN CR
				else 0
				end),
			@XPint = SUM(case
				when ACCT_CODE = 'int' THEN CR
				else 0
				end) -
			SUM(case
				when ACCT_CODE = 'int' THEN DR
				else 0
				end),
			@XLASTD = MAX(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ') THEN [DATE]
				else null
				end)
	from	dbo.LEDGER
	where	PN_NO = @vCode
	
	select	top 1
			a.LOANS_ID, 
			a.KBCI_NO,
			a.APP_DATE as txtDateApplied,
			a.PN_NO,
			b.LNAME as txtLName,
			b.FNAME as txtFName,
			b.MI as txtMI,
			a.LOAN_TYPE as cboLoanType,
			a.LED_TYPE as cboLedgerType,
			a.TERM as txtTerm,
			a.FREQ as cboFrequency,
			a.RATE as txtRate,
			a.AMORT_AMT as txtAmortization,
			a.MOD_PAY as cboPaymentMode,
			a.CHKNO_DATE as txtReleaseDate,
			a.PD,
			a.DATE_DUE as txtDateDue,
			a.LOAN_STAT cboLoanStatus,
			a.PRINCIPAL as txtLoanAmount,
			a.PRINCIPAL + @XBPRIN as txtXBPrin,
			a.PAY_START as txtPayStart,
			isnull(@XLASTD, a.PAY_START) XLASTD,
			a.ARREAR_P as txtXARRP,
			a.ARREAR_I as txtXARRI,
			a.ARREAR_OTH as txtXARRO,
			a.ARREAR_AS as txtXARRD,
			(select top 1 SYSDATE from DBO.CTRL) txtXTRAND,
			@XPINT as txtXPINT
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO				
	where	a.PN_NO = @vCode	
end

else if @vType = 'LC-2'
begin
	select	@XBPRIN = SUM(case 
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ', 'TER', 'REP') THEN DR - CR
				else 0
				end),
			@XPint = SUM(case
				when ACCT_CODE = 'int' THEN CR - DR
				else 0
				end),
			@XLASTD = MAX(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE IN ('PAY', 'ADJ') THEN [DATE]
				else null
				end),
			@XLASPD = MAX(case
				when ACCT_CODE = 'PRI' AND ACCT_TYPE = 'PAY' THEN [DATE]
				else null
				end)
	from	dbo.LEDGER
	where	PN_NO = @vCode
	
	select	@AXGOMO = case
				when FREQ = 'M' THEN -1
				when FREQ = 'S' THEN -6
				when FREQ = 'Q' THEN -3
				when FREQ = 'A' THEN -12
				end,
			@AXFREQ = case
				when FREQ = 'M' THEN 12
				when FREQ = 'S' THEN 6
				when FREQ = 'Q' THEN 4
				when FREQ = 'A' THEN 1
				end
	from	dbo.LOANS
	where	PN_NO = @vCode	
	
	if @XLASPD is null 
		set @XLASPD = null
	else
		set @XLASPD = DATEADD(MM, ABS(@AXGOMO), @XLASPD)
	
	select	top 1 
			@SYSDATE = SYSDATE 
	from	DBO.CTRL
	
	select	top 1
			a.LOANS_ID, 
			a.KBCI_NO,
			a.APP_DATE as txtDateApplied,
			a.PN_NO,
			b.LNAME as txtLName,
			b.FNAME as txtFName,
			b.MI as txtMI,
			a.LOAN_TYPE as cboLoanType,
			a.LED_TYPE as cboLedgerType,
			a.TERM as txtTerm,
			a.FREQ as cboFrequency,
			a.RATE as txtRate,
			a.AMORT_AMT as txtAmortization,
			a.MOD_PAY as cboPaymentMode,
			a.CHKNO_DATE as txtReleaseDate,
			a.PD,
			a.DATE_DUE as txtDateDue,
			a.LOAN_STAT as cboLoanStatus,
			a.PAY_START as txtPayStart,
			a.PRINCIPAL as txtLoanAmount,
			a.PRINCIPAL + @XBPRIN as txtXBPrin,
			isnull(@XLASTD, a.PAY_START) XLASTD,
			a.ARREAR_P txtXARRP,
			a.ARREAR_I txtXARRI,
			a.ARREAR_OTH txtXARRO,
			a.ARREAR_AS txtXARRD,
			@SYSDATE txtXTRAND,
			@SYSDATE SYSDATE,
			@XPINT txtXPINT,
			@XLASPD XLASPD,
			@AXGOMO AXGOMO,
			@AXFREQ AXFREQ			
	from	DBO.LOANS a
				inner join
			DBO.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	a.PN_NO = @vCode	
end

else if @vType = 'LC-3'
	select	SUM(case
				when ACCT_CODE = 'int' THEN CR - DR
				else 0
				end) XARRIP,
			SUM(case
				when ACCT_CODE = 'OTH' THEN CR - DR
				else 0
				end) XARROP
	from	dbo.LEDGER
	where	PN_NO = @vCode AND
			[DATE] = @dtDate

else if @vType = 'LE-1'
	select	' ' as rtag,
			'PDUE' as rlstat,
			PN_NO as rpn_no,
			LOAN_TYPE as rltyp,
			PRINCIPAL as rlamt
			
	from	dbo.LOANS
	where	KBCI_NO = @vCode
	

else if @vType = 'MA-1'
	select	top 1
			a.PN_NO,
			a.KBCI_NO as txtKBCI,
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.', '') MEMBER,
			a.MOD_PAY as cboPaymentMode,
			a.AMORT_AMT as txtNXAmortization,
			a.PAY_START as txtDXPayStart,
			a.LED_TYPE as cboLedgerType,
			a.LOAN_TYPE as cboLoanType,
			a.LOAN_STAT as cboLoanStatus,
			a.ACCU_PAYP as txtNXAccuPymts,
			a.YTD_I as txtNXYTDInt,
			a.PD as chkPD,
			a.ARREAR_P as txtNXARRPrin,
			a.ARREAR_I as txtNXARRInt,
			a.ARREAR_OTH as txtNXARROthers,
			a.ARREAR_AS as txtDXARRAsOf,
			a.P_BAL as txtNXPrincipal,
			a.I_BAL as txtNXInterest,
			a.O_BAL as txtNXOthers
	from	dbo.LOANS a
				inner join
			dbo.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	where	PN_NO = @vCode

else if @vType = 'MA-2'
	select	a.PN_NO,
			a.KBCI_NO,
			isnull(b.LNAME + ', ', '') + isnull(b.FNAME + ' ', '') + isnull(b.MI + '.', '') MEMBER,
			a.MOD_PAY as cboPaymentMode,
			a.AMORT_AMT as txtNXAmortization,
			a.PAY_START as txtDXPayStart,
			a.LED_TYPE as cboLedgerType,
			a.LOAN_TYPE as cboLoanType,
			a.LOAN_STAT as cboLoanStatus			
	from	dbo.LOANS a
				inner join
			dbo.MEMBERS b
				on a.KBCI_NO = b.KBCI_NO
	order by
			MEMBER

else if @vType = 'MB-1'
	select	KBCI_NO,
			LNAME as txtLName,
			FNAME as txtFName,
			MI as txtMI,
			MEM_ADDR as txtAddress,
			MEM_CODE as txtMembershipCode,
			MEM_STAT as txtMembershipStat,
			CB_EMPNO as txtCBEmpNo,
			DEPT as txtDepartment,
			REGION as txtRegion,
			OFF_TEL as txtUXOfficeTel,
			DORI as chkDORI,
			REA_DORI as txtReasonDORI,
			CIV_STAT as txtCivilStatus,
			RES_TEL as txtUXResidenceTel,
			POSITION as txtPosition,
			SAL_BAS as txtNXBasicSalary,
			SAL_ALL as txtNXAllowance,
			OTH_INC as txtNXOtherIncome,
			FEBTC_SA as txtUXFEBTCSA,
			FEBTC_CA as txtUXFEBTCCA,
			YTD_DIVAMT as txtNXYTDDividend,
			YTD_LRI as txtNXYTDLRI,
			REM_VALUE as txtNXREMValue,
			NO_DEPEND as txtIXDependents,
			SP_NAME as txtSpouseName,
			SP_EMPLOY as txtSPEmployer,
			SP_OFFTEL as txtUXSpouseEmployer,
			SP_CBEMPNO as txtSpouseCBEmpNo,
			SP_KBCINO as txtSpouseKBCI,
			SP_SALARY as txtNXSpouseSal,
			SD_AMOUNT as txtNXSABalance,
			FD_AMOUNT as txtNXFDBalance
	from	dbo.MEMBERS
	where	KBCI_NO = @vCode

else if @vType = 'MB-2'
	select	KBCI_NO,
			isnull(LNAME + ', ', '') + isnull(FNAME + ' ', '') + isnull(MI, '') MEMBER,
			MEM_STAT,
			DORI,
			CB_EMPNO,			
			DEPT,
			POSITION
	from	dbo.MEMBERS
	order by
			MEMBER

else if @vType = 'MC-1'
	select	LOAN_TYPE,
			TERM txtIXTerm, 
			FREQ txtFrequency, 
			RATE txtNXRate, 
			[MIN] txtNXMinLoan, 
			[MAX] txtNXMaxLoan
	from	dbo.[PARAM]

else if @vType = 'MD-1'
	select	top 1
			MAPP_NO as txtMAppNo,
			LAPP_DATE as txtDXLAppDate,
			LAPP_NO as txtIXLAppNo,
			KBCI_NO as txtKBCINo,
			PN_NO as txtIXPNNo,
			PAY_DAY as txtDXPayDate,
			L_DM as txtIXDMNo,
			L_CM as txtIXCMNo,
			[CEILING] as txtNXCeiling
	from	dbo.CTRL

else if @vType = 'ME-1'
	select	[OR_ID] as OR_ID,
			[PN_NO] as txtUXPN_NO,
			[DATE] as txtDXDATE,
			[REF] as txtREF,
			[AMT] as txtNXAMT,
			[POSTED] as chkPOSTED,
			[RMK] as txtRMK,			
			[REC_STAT] as chkREC_STAT
	from	dbo.[OR]

else if @vType = 'MF-1'
	select	[LEDGER_ID] as LEDGER_ID,
			[PN_NO] as txtUXPN_NO,
			[DATE] as txtDXDATE,
			[DOX_TYPE] as txtDOX_TYPE,
			[REF] as txtREF,
			[ACCT_TYPE] as txtACCT_TYPE,
			[ACCT_CODE] as txtACCT_CODE,
			[BEGBAL] as txtNXBEGBAL,
			[DR] as txtNXDR,
			[CR] as txtNXCR,
			[ENDBAL] as txtNXENDBAL,
			[RMK] as txtRMK,
			[ADD_DATE] as txtDXADD_DATE,
			[USER] as txtUSER
	from	dbo.LEDGER
	where	[PN_NO] = @vCode
	order	by
			[DATE]




GO


