USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanRestructuring]    Script Date: 04/17/2009 17:27:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Loans_LoanRestructuring]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Loans_LoanRestructuring]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Loans_LoanRestructuring]    Script Date: 04/17/2009 17:27:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Loans_LoanRestructuring]
@PN_NO varchar(7),
@KBCI_NO varchar(7),
@APP_DATE date,
@APP_NO numeric(5,0),
@DATE_GRANT date,
@BY_WHOM varchar(15),
@DATE_DUE date,
@CHKNO_BANK varchar(15),
@CHKNO varchar(15),
@CHKNO_AMT numeric(12,4),
@CHKNO_DATE date,
@CHKNO_RELS varchar(6),
@CHKNO_ACK date,
@MOD_PAY varchar(3),
@AMORT_AMT numeric(12,4),
@PAY_START date,
@RATE numeric(7,4),
@TERM numeric(3,0),
@FREQ varchar(1),
@PRINCIPAL numeric(12,4),
@LED_TYPE numeric(1,0),
@ADV_INTE numeric(2,0),
@AFT_INTE numeric(12,4),
@ACCU_PAYP numeric(12,4),
@YTD_I numeric(11,4),
@LOAN_TYPE varchar(3),
@LOAN_STAT varchar(1),
@ARREAR_I numeric(12,4),
@ARREAR_P numeric(12,4),
@ARREAR_OTH numeric(12,4),
@ARREAR_AS date,
@COLLATERAL varchar(55),
@DED_BAL varchar(7),
@ADD_DATE date,
@CHG_DATE date,
@USER varchar(8),
@P_BAL numeric(12,4),
@I_BAL numeric(12,4),
@O_BAL numeric(12,4),
@REC_STAT bit,
@RENEW bit,
@ADVANCE numeric(12,4),
@LRI_IND bit,
@NDUE date,
@L_EXT bit,
@PD bit,
@LRI_DUE numeric(12,4)
as

insert	into dbo.LOANS (
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
		[NDUE],
		[L_EXT],
		[PD],
		[LRI_DUE]
		)
values (
		@PN_NO,
		@KBCI_NO,
		@APP_DATE,
		@APP_NO,
		@DATE_GRANT,
		@BY_WHOM,
		@DATE_DUE,
		@CHKNO_BANK,
		@CHKNO,
		@CHKNO_AMT,
		@CHKNO_DATE,
		@CHKNO_RELS,
		@CHKNO_ACK,
		@MOD_PAY,
		@AMORT_AMT,
		@PAY_START,
		@RATE,
		@TERM,
		@FREQ,
		@PRINCIPAL,
		@LED_TYPE,
		@ADV_INTE,
		@AFT_INTE,
		@ACCU_PAYP,
		@YTD_I,
		@LOAN_TYPE,
		@LOAN_STAT,
		@ARREAR_I,
		@ARREAR_P,
		@ARREAR_OTH,
		@ARREAR_AS,
		@COLLATERAL,
		@DED_BAL,
		@ADD_DATE,
		@CHG_DATE,
		@USER,
		@P_BAL,
		@I_BAL,
		@O_BAL,
		@REC_STAT,
		@RENEW,
		@ADVANCE,
		@LRI_IND,
		@NDUE,
		@L_EXT,
		@PD,
		@LRI_DUE
)




GO


