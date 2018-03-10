USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_MT_Member_Insert]    Script Date: 04/17/2009 17:28:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_MT_Member_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_MT_Member_Insert]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_MT_Member_Insert]    Script Date: 04/17/2009 17:28:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[s3p_MT_Member_Insert]
@KBCI_NO varchar(7),
@MY_USER varchar(8),
@txtLName varchar(20),
@txtFName varchar(20),
@txtMI varchar(1),
@txtAddress varchar(60),
@txtMembershipCode varchar(1),
@txtMembershipStat varchar(1),
@txtCBEmpNo varchar(6),
@txtDepartment varchar(15),
@txtRegion varchar(15),
@txtUXOfficeTel varchar(11),
@chkDORI bit,
@txtReasonDORI varchar(15),
@txtCivilStatus varchar(1),
@txtUXResidenceTel varchar(7),
@txtPosition varchar(15),
@txtNXBasicSalary numeric(11,4),
@txtNXAllowance numeric(11,4),
@txtNXOtherIncome numeric(12,4),
@txtUXFEBTCSA varchar(10),
@txtUXFEBTCCA varchar(10),
@txtNXYTDDividend numeric(11,4),
@txtNXYTDLRI numeric(11,4),
@txtNXREMValue numeric(12,4),
@txtIXDependents numeric(2),
@txtSpouseName varchar(30),
@txtSPEmployer varchar(40),
@txtUXSpouseEmployer varchar(11),
@txtSpouseCBEmpNo varchar(6),
@txtSpouseKBCI varchar(7),
@txtNXSpouseSal numeric(11,4),
@txtNXSABalance numeric(13,4),
@txtNXFDBalance numeric(13,4)
AS

insert	dbo.MEMBERS (
		[KBCI_NO],
		[LNAME],
		[FNAME],
		[MI],
		[MEM_ADDR],
		[MEM_CODE],
		[MEM_STAT],
		[CB_EMPNO],
		[DEPT],
		[REGION],
		[OFF_TEL],
		[DORI],
		[REA_DORI],
		[CIV_STAT],
		[RES_TEL],
		[POSITION],
		[SAL_BAS],
		[SAL_ALL],
		[OTH_INC],
		[FEBTC_SA],
		[FEBTC_CA],
		[YTD_DIVAMT],
		[YTD_LRI],
		[REM_VALUE],
		[NO_DEPEND],
		[SP_NAME],
		[SP_EMPLOY],
		[SP_OFFTEL],
		[SP_CBEMPNO],
		[SP_KBCINO],
		[SP_SALARY],
		[SD_AMOUNT],
		[FD_AMOUNT],
		[USER],
		[CHG_DATE]
		)
values	(
		@KBCI_NO,
		@txtLName,
		@txtFName,
		@txtMI,
		@txtAddress,
		@txtMembershipCode,
		@txtMembershipStat,
		@txtCBEmpNo,
		@txtDepartment,
		@txtRegion,
		@txtUXOfficeTel,
		@chkDORI,
		@txtReasonDORI,
		@txtCivilStatus,
		@txtUXResidenceTel,
		@txtPosition,
		@txtNXBasicSalary,
		@txtNXAllowance,
		@txtNXOtherIncome,
		@txtUXFEBTCSA,
		@txtUXFEBTCCA,
		@txtNXYTDDividend,
		@txtNXYTDLRI,
		@txtNXREMValue,
		@txtIXDependents,
		@txtSpouseName,
		@txtSPEmployer,
		@txtUXSpouseEmployer,
		@txtSpouseCBEmpNo,
		@txtSpouseKBCI,
		@txtNXSpouseSal,
		@txtNXSABalance,
		@txtNXFDBalance,
		@MY_USER,
		GETDATE()
		)




GO


