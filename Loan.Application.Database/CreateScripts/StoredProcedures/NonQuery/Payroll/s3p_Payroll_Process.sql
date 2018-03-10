USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Process]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Payroll_Process]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Payroll_Process]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Process]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*****************************************************************************
MODIFIED:
JS 06/02/2012		FIXED MO_DEDN(H) INCORRECT VALUES
JS 06/30/2012		TRUNCATE INSTEAD OF DELETE
JS 08/04/2012		DATE TO NUMBER CONVERSION ERROR
JS 11/17/2012		UPDATED CTRL.PROC
JS 01/26/2013		INSERTED MISSING ACCTNO
JS 01/18/2014		RECOMPUTE BALANCE
JS 03/05/2016		CORRECTION ON DEDUCTION REGISTER
*****************************************************************************/

CREATE PROCEDURE [dbo].[s3p_Payroll_Process]
@MY_USER varchar(8),
@SOD_TXT varchar(35),
@SOD_TOT numeric(13,4)
as

declare @SYSDATE date

declare @UPLOAD table (
	UPLOAD_ID bigint,
	TRACE int
)

declare @LOANS table (
	LOANS_ID bigint
)

declare @LOAN_TYPE_CODES table (
	ACCT_CODE varchar(6),
	TAYP varchar(3)
)

declare @VOUCHER table (
	XPN_NO VARCHAR(9)
)

declare @MO_DEDN table (
	[UPLOAD_ID] bigint,
    [EMPNO] varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[KBCI_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[NAME] varchar(45) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[PN_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[LOAN_TYPE] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[AMORT_PRI] numeric(11, 2),
	[AMORT_INT] numeric(11, 2),
	[DEDUCTION] numeric(11, 2),
	[PRINCIPAL] numeric(11, 2),
	[INTEREST] numeric(11, 2),
	[ARREARS] numeric(11, 2),
	[ADVANCE] numeric(11, 2),
	[DATE] date,
	[USER] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[ARR_PRI] numeric(11, 2),
	[ARR_INT] numeric(11, 2),
	[CODE5] numeric(2),
	[PD] bit
)

create table #MO_DEDN (
	[UPLOAD_ID] bigint,
    [EMPNO] varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[KBCI_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[NAME] varchar(45) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[PN_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[LOAN_TYPE] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[AMORT_PRI] numeric(11, 2),
	[AMORT_INT] numeric(11, 2),
	[DEDUCTION] numeric(11, 2),
	[PRINCIPAL] numeric(11, 2),
	[INTEREST] numeric(11, 2),
	[ARREARS] numeric(11, 2),
	[ADVANCE] numeric(11, 2),
	[DATE] date,
	[USER] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[ARR_PRI] numeric(11, 2),
	[ARR_INT] numeric(11, 2),
	[CODE5] numeric(2),
	[PD] bit
)

select	top 1
		@SYSDATE = SYSDATE
from	dbo.CTRL

truncate table dbo.MO_DEDN			-- JS 06/30/2012
truncate table dbo.MO_DEDNP			-- JS 03/05/2016
truncate table dbo.UPLOAD			-- JS 06/30/2012

insert	dbo.UPLOAD (
		EMP_NUM,
		ACCT_CODE,
		AMOUNT,
		CODE5
		)
select	right('000000' + CONVERT(VARCHAR(6),ISNULL(EMPNO1,0)), 6), --TODO: upload table in foxpro not padded? :O
		right('0' + ISNULL(ACTYPE,0), 1) + right('0' + ISNULL(ACTCD1,0), 1) + right('0000' + ISNULL(ACTCD2,0), 4),
		ISNULL(AMT7C,0),
		CODE5
from	dbo.EXTKBC

--truncate table dbo.EXTKBC			-- JS 06/30/2012

update	dbo.UPLOAD
set		ACCT_CODE = '007601'
where	ACCT_CODE = '007665'

insert	@LOAN_TYPE_CODES (ACCT_CODE, TAYP) 
select	'00' + CODE5,														-- JS 01/26/2013
		LOAN_TYPE															--		|
from	dbo.LOAN_TYPE														--		|
where	CODE5 is not null													--		|
union																		--		|
select	CODE5,																--		|
		OTHER_TYPE															--		|
from	dbo.OTHER_TYPE														--		|
where	CODE5 is not null													--		|
union																		-- JS 01/26/2013
select '007645', 'SPL'

-- ***************************************************************
-- BEGIN - IF UPLOAD.emp_num does not exist in MEMBERS.DBF
-- ***************************************************************

update	dbo.UPLOAD
set		TRANS_CODE = 'X'
where	EMP_NUM not in (
		select	EMP_NUM
		from	dbo.UPLOAD up
				inner join dbo.MEMBERS mem on mem.CB_EMPNO = up.EMP_NUM
		)

insert	@MO_DEDN (
		UPLOAD_ID,
		EMPNO,
		DEDUCTION,
		[DATE],
		[USER],
		LOAN_TYPE,
		CODE5
		)
select	up.UPLOAD_ID,
		up.EMP_NUM,
		up.AMOUNT,
		@SYSDATE,
		@MY_USER,
		isnull(tmp.TAYP, 'OTH'),
		up.CODE5
from	dbo.UPLOAD up
			left join
		@LOAN_TYPE_CODES tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	up.EMP_NUM not in (
		select	EMP_NUM
		from	dbo.UPLOAD up
				inner join dbo.MEMBERS mem on mem.CB_EMPNO = up.EMP_NUM
		)

--update	dbo.UPLOAD
--set		EMP_NUM = right('000000' + ISNULL(EMP_NUM,0), 6)

--update	@MO_DEDN
--set		EMPNO = right('000000' + ISNULL(EMPNO,0), 6)

-- BEGIN - HPOST

insert	@UPLOAD (UPLOAD_ID, TRACE)
select	UPLOAD_ID, 1
from	@MO_DEDN

update	#MO_DEDN
set		[EMPNO] = src.[EMPNO], [KBCI_NO] = src.[KBCI_NO], [NAME] = src.[NAME], [PN_NO] = src.[PN_NO],
		[LOAN_TYPE] = src.[LOAN_TYPE], [AMORT_PRI] = src.[AMORT_PRI], [AMORT_INT] = src.[AMORT_INT],
		[DEDUCTION] = src.[DEDUCTION], [PRINCIPAL] = src.[PRINCIPAL], [INTEREST] = src.[INTEREST],
		[ARREARS] = src.[ARREARS], [ADVANCE] = src.[ADVANCE], [DATE] = src.[DATE], [USER] = src.[USER],
		[ARR_PRI] = src.[ARR_PRI], [ARR_INT] = src.[ARR_INT], [CODE5] = src.[CODE5], [PD] = src.[PD]
from	#MO_DEDN tgt
		inner join @MO_DEDN src on src.UPLOAD_ID = tgt.UPLOAD_ID

insert	#MO_DEDN (
		[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
		)
select	[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
from	@MO_DEDN
where	UPLOAD_ID not in (select UPLOAD_ID from #MO_DEDN)

delete	@MO_DEDN

-- END - HPOST
-- END - IF UPLOAD.emp_num does not exist in MEMBERS.DBF

-- ***************************************************************
-- BEGIN - IF tayp='FIX'
-- ***************************************************************

insert	dbo.FD (
		KBCI_NO,
		TRAN_CODE, 
		[DATE], 
		REF, 
		AMOUNT, 
		BALANCE, 
		DRCR, 
		TPOSTED, 
		RMK, 
		ADD_DATE, 
		[USER]
		)
select	mem.KBCI_NO,
		'8',
		@SYSDATE,
		'PAYROLL',
		up.AMOUNT,
		mem.FD_AMOUNT + up.AMOUNT,
		'CR',
		1,
		'PAYROLL DEDUCTION',
		@SYSDATE,
		@MY_USER		
from	dbo.UPLOAD up
			left join
		@LOAN_TYPE_CODES tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.FD fd
			on fd.KBCI_NO = mem.KBCI_NO
where	tmp.TAYP = 'FIX' and 
		up.UPLOAD_ID not in (select UPLOAD_ID from @UPLOAD)

update	dbo.MEMBERS
set		FD_AMOUNT = b.AMOUNT,
		FD_DATE = @SYSDATE
from	dbo.MEMBERS
			as a 
		inner join (
			select	mem.KBCI_ID,
					fd.AMOUNT AMOUNT
			from	dbo.FD fd
						inner join
					dbo.MEMBERS mem
						on fd.KBCI_NO = mem.KBCI_NO			
						inner join
					dbo.UPLOAD up
						on mem.CB_EMPNO = up.EMP_NUM
						left join
					@LOAN_TYPE_CODES tmp
						on tmp.ACCT_CODE = up.ACCT_CODE
			where	tmp.TAYP = 'FIX' and 
					up.UPLOAD_ID not in (select UPLOAD_ID from @UPLOAD)
			)
			as b on a.KBCI_ID = b.KBCI_ID

insert	@MO_DEDN (
		UPLOAD_ID,
		EMPNO,
		KBCI_NO,
		DEDUCTION,
		[DATE],
		[USER],
		LOAN_TYPE,
		[NAME],
		CODE5
		)
select	up.UPLOAD_ID,
		mem.CB_EMPNO,
		mem.KBCI_NO,
		up.AMOUNT,
		@SYSDATE,
		@MY_USER,
		isnull(tmp.TAYP, 'OTH'),
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		up.CODE5
from	dbo.MEMBERS mem
			inner join
		dbo.UPLOAD up
			on up.EMP_NUM = mem.CB_EMPNO
			left join
		@LOAN_TYPE_CODES tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	tmp.TAYP = 'FIX' and 
		up.UPLOAD_ID not in (select UPLOAD_ID from @UPLOAD)

-- BEGIN - HPOST

insert	@UPLOAD (UPLOAD_ID, TRACE)
select	UPLOAD_ID, 2
from	@MO_DEDN

update	#MO_DEDN
set		[EMPNO] = src.[EMPNO], [KBCI_NO] = src.[KBCI_NO], [NAME] = src.[NAME], [PN_NO] = src.[PN_NO],
		[LOAN_TYPE] = src.[LOAN_TYPE], [AMORT_PRI] = src.[AMORT_PRI], [AMORT_INT] = src.[AMORT_INT],
		[DEDUCTION] = src.[DEDUCTION], [PRINCIPAL] = src.[PRINCIPAL], [INTEREST] = src.[INTEREST],
		[ARREARS] = src.[ARREARS], [ADVANCE] = src.[ADVANCE], [DATE] = src.[DATE], [USER] = src.[USER],
		[ARR_PRI] = src.[ARR_PRI], [ARR_INT] = src.[ARR_INT], [CODE5] = src.[CODE5], [PD] = src.[PD]
from	#MO_DEDN tgt
		inner join @MO_DEDN src on src.UPLOAD_ID = tgt.UPLOAD_ID

insert	#MO_DEDN (
		[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
		)
select	[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
from	@MO_DEDN
where	UPLOAD_ID not in (select UPLOAD_ID from #MO_DEDN)

delete	@MO_DEDN

-- END - HPOST
-- END - IF tayp='FIX'

-- ***************************************************************
-- BEGIN - IF mhi = .T.
-- ***************************************************************

insert	@LOANS (LOANS_ID)
select	lon.LOANS_ID
from	dbo.UPLOAD up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			left join
		@LOAN_TYPE_CODES tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	lon.LOAN_TYPE = tmp.TAYP and
		lon.LOAN_STAT = 'R' and
		up.UPLOAD_ID not in (select UPLOAD_ID from @UPLOAD)

insert	@MO_DEDN (
		UPLOAD_ID,
		EMPNO,
		CODE5,
		KBCI_NO,
		PN_NO,
		PD,
		DEDUCTION,
		LOAN_TYPE,
		[NAME],
		[DATE],
		[USER]
		)
select	up.UPLOAD_ID,
		mem.CB_EMPNO,
		up.CODE5,
		mem.KBCI_NO,
		lon.PN_NO,
		lon.PD,
		up.AMOUNT,
		isnull(tmp.TAYP, 'OTH'),
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		@SYSDATE,		
		@MY_USER
from	dbo.UPLOAD up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			left join
		@LOAN_TYPE_CODES tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	lon.LOAN_TYPE = tmp.TAYP and
		lon.LOAN_STAT = 'R' and
		CHARINDEX('1', lon.MOD_PAY) > 0 and
		up.UPLOAD_ID not in (select UPLOAD_ID from @UPLOAD)

insert	@UPLOAD (UPLOAD_ID, TRACE)
select	UPLOAD_ID, 3
from	@MO_DEDN

update	#MO_DEDN
set		[EMPNO] = src.[EMPNO], [KBCI_NO] = src.[KBCI_NO], [NAME] = src.[NAME], [PN_NO] = src.[PN_NO],
		[LOAN_TYPE] = src.[LOAN_TYPE], [AMORT_PRI] = src.[AMORT_PRI], [AMORT_INT] = src.[AMORT_INT],
		[DEDUCTION] = src.[DEDUCTION], [PRINCIPAL] = src.[PRINCIPAL], [INTEREST] = src.[INTEREST],
		[ARREARS] = src.[ARREARS], [ADVANCE] = src.[ADVANCE], [DATE] = src.[DATE], [USER] = src.[USER],
		[ARR_PRI] = src.[ARR_PRI], [ARR_INT] = src.[ARR_INT], [CODE5] = src.[CODE5], [PD] = src.[PD]
from	#MO_DEDN tgt
		inner join @MO_DEDN src on src.UPLOAD_ID = tgt.UPLOAD_ID

insert	#MO_DEDN (
		[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
		)
select	[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
from	@MO_DEDN
where	UPLOAD_ID not in (select UPLOAD_ID from #MO_DEDN)

insert	dbo.PAYHIST (
		PN_NO,
		KBCI_NO,
		PAYTYPE,
		PAYAMT,
		PAYDATE,
		PAYREM,
		ADDATE,
		LUPDATE,
		UPDUSER
		)
select	PN_NO,
		KBCI_NO,
		'4',
		DEDUCTION,
		@SYSDATE,
		'PAYROLL DEDUCTION',
		@SYSDATE,
		@SYSDATE,
		@MY_USER
from	@MO_DEDN

delete	@MO_DEDN

-- END - IF mhi = .T.

-- ***************************************************************
-- BEGIN - IF mhi = .F.
-- ***************************************************************

update	dbo.UPLOAD
set		TRANS_CODE = 'N'
where	UPLOAD_ID not in (select UPLOAD_ID from @UPLOAD) and
		UPLOAD_ID not in (
		select	distinct
				up.UPLOAD_ID
		from	dbo.UPLOAD up
					inner join
				dbo.MEMBERS mem
					on mem.CB_EMPNO = up.EMP_NUM
					left join
				dbo.LOANS lon
					on lon.KBCI_NO = mem.KBCI_NO
					left join
				@LOAN_TYPE_CODES tmp
					on tmp.ACCT_CODE = up.ACCT_CODE
		where	lon.LOAN_TYPE = tmp.TAYP and
				lon.LOAN_STAT = 'R' and
				CHARINDEX('1', lon.MOD_PAY) = 0
		)

insert	@MO_DEDN (
		UPLOAD_ID,
		EMPNO,
		KBCI_NO,
		DEDUCTION,
		[DATE],
		[USER],
		LOAN_TYPE,
		[NAME],
		CODE5,
		ADVANCE
		)
select	distinct
		up.UPLOAD_ID,
		mem.CB_EMPNO,
		mem.KBCI_NO,
		up.AMOUNT,
		@SYSDATE,
		@MY_USER,
		isnull(tmp.TAYP, 'OTH'),
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		up.CODE5,
		up.AMOUNT
from	dbo.UPLOAD up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			left join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			left join
		@LOAN_TYPE_CODES tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	up.TRANS_CODE = 'N'

-- BEGIN - HPOST

insert	@UPLOAD (UPLOAD_ID, TRACE)
select	UPLOAD_ID, 4
from	@MO_DEDN

update	#MO_DEDN
set		[EMPNO] = src.[EMPNO], [KBCI_NO] = src.[KBCI_NO], [NAME] = src.[NAME], [PN_NO] = src.[PN_NO],
		[LOAN_TYPE] = src.[LOAN_TYPE], [AMORT_PRI] = src.[AMORT_PRI], [AMORT_INT] = src.[AMORT_INT],
		[DEDUCTION] = src.[DEDUCTION], [PRINCIPAL] = src.[PRINCIPAL], [INTEREST] = src.[INTEREST],
		[ARREARS] = src.[ARREARS], [ADVANCE] = src.[ADVANCE], [DATE] = src.[DATE], [USER] = src.[USER],
		[ARR_PRI] = src.[ARR_PRI], [ARR_INT] = src.[ARR_INT], [CODE5] = src.[CODE5], [PD] = src.[PD]
from	#MO_DEDN tgt
		inner join @MO_DEDN src on src.UPLOAD_ID = tgt.UPLOAD_ID

insert	#MO_DEDN (
		[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
		)
select	[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
from	@MO_DEDN
where	UPLOAD_ID not in (select UPLOAD_ID from #MO_DEDN)

insert	dbo.ADVANCE(
		PN_NO,
		KBCI_NO,
		LOAN_TYPE,
		ADD_DATE,
		[USER],
		AMOUNT,
		ACCTNO,
		REMARKS
		)
select	REPLICATE(' ',7),
		mem.KBCI_NO,
		LOAN_TYPE,
		[DATE],
		@MY_USER,
		DEDUCTION,
		mem.FEBTC_SA,
		'REFUND: ' + 
			RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  MM, @SYSDATE)), 2) + '/' + 
			RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  DD, @SYSDATE)), 2) + '/' + 
			RIGHT('0000' + CONVERT(VARCHAR(4), DATEPART(YYYY, @SYSDATE)), 4) +
			' FULLY PAID LOAN  '
from	@MO_DEDN a
		inner join dbo.MEMBERS mem on mem.KBCI_NO = a.KBCI_NO

delete	@MO_DEDN

-- END - HPOST
-- END - IF mhi = .F.

-- ***************************************************************
-- BEGIN - IF mhi = .T. and .not. '1' $ mpay
-- ***************************************************************

insert	@MO_DEDN (
		UPLOAD_ID,
		EMPNO,
		KBCI_NO,
		DEDUCTION,
		[DATE],
		[USER],
		LOAN_TYPE,
		[NAME],
		CODE5,
		ADVANCE
		)
select	distinct
		up.UPLOAD_ID,
		mem.CB_EMPNO,
		mem.KBCI_NO,
		up.AMOUNT,
		@SYSDATE,
		@MY_USER,
		isnull(tmp.TAYP, 'OTH'),
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		up.CODE5,
		up.AMOUNT
from	dbo.UPLOAD up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			left join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			left join
		@LOAN_TYPE_CODES tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	lon.LOAN_TYPE = tmp.TAYP and
		lon.LOAN_STAT = 'R' and
		CHARINDEX('1', lon.MOD_PAY) = 0
				
-- BEGIN - HPOST

insert	@UPLOAD (UPLOAD_ID, TRACE)
select	UPLOAD_ID, 5
from	@MO_DEDN

update	#MO_DEDN
set		[EMPNO] = src.[EMPNO], [KBCI_NO] = src.[KBCI_NO], [NAME] = src.[NAME], [PN_NO] = src.[PN_NO],
		[LOAN_TYPE] = src.[LOAN_TYPE], [AMORT_PRI] = src.[AMORT_PRI], [AMORT_INT] = src.[AMORT_INT],
		[DEDUCTION] = src.[DEDUCTION], [PRINCIPAL] = src.[PRINCIPAL], [INTEREST] = src.[INTEREST],
		[ARREARS] = src.[ARREARS], [ADVANCE] = src.[ADVANCE], [DATE] = src.[DATE], [USER] = src.[USER],
		[ARR_PRI] = src.[ARR_PRI], [ARR_INT] = src.[ARR_INT], [CODE5] = src.[CODE5], [PD] = src.[PD]
from	#MO_DEDN tgt
		inner join @MO_DEDN src on src.UPLOAD_ID = tgt.UPLOAD_ID

insert	#MO_DEDN (
		[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
		)
select	[UPLOAD_ID], [EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
from	@MO_DEDN
where	UPLOAD_ID not in (select UPLOAD_ID from #MO_DEDN)

insert	dbo.ADVANCE(
		PN_NO,
		KBCI_NO,
		LOAN_TYPE,
		ADD_DATE,
		[USER],
		AMOUNT,
		ACCTNO,
		REMARKS
		)
select	REPLICATE(' ',7),
		mem.KBCI_NO,
		LOAN_TYPE,
		[DATE],
		@MY_USER,
		DEDUCTION,
		mem.FEBTC_SA,
		'REFUND: ' + 
			RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  MM, @SYSDATE)), 2) + '/' + 
			RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  DD, @SYSDATE)), 2) + '/' + 
			RIGHT('0000' + CONVERT(VARCHAR(4), DATEPART(YYYY, @SYSDATE)), 4) +
			' FULLY PAID LOAN  '
from	@MO_DEDN a
		inner join dbo.MEMBERS mem on mem.KBCI_NO = a.KBCI_NO

delete	@MO_DEDN

-- END - HPOST
-- END - IF mhi = .F.

-- ***************************************************************
-- AFTER ALL THE IFs
-- ***************************************************************

declare @XBPRIN numeric(11, 2)
declare @YBPRIN numeric(11, 2)
declare @YBADV numeric(11, 2)
declare @XOPRI numeric(11, 2) = 0 
declare @XOINT numeric(11, 2) = 0 
declare @XAPEN numeric(11, 2) = 0 
declare @XAINT numeric(11, 2) = 0 
declare @XAPRI numeric(11, 2) = 0 
declare @XADV numeric(11, 2) = 0 
declare @XAMORTP numeric(11, 2) = 0 
declare @XAMORTI numeric(11, 2) = 0 
declare @A2PAY numeric(11, 2) = 0 
declare @XPPAY numeric(11, 2) = 0

declare @LOANS_ID bigint
declare @PN_NO varchar(7)
declare @KBCI_NO varchar(7)
declare @PRINCIPAL numeric(10, 2)
declare @OR_AMOUNT numeric(10, 2)
declare @PAY_START date
declare @P_BAL numeric(10, 2)
declare @I_BAL numeric(10, 2)
declare @ARREAR_AS date
declare @ARREAR_P numeric(10, 2)
declare @ARREAR_I numeric(10, 2)
declare @ARREAR_OTH numeric(10, 2)
declare @PD bit
declare @ACCU_PAYP numeric(10, 2)
declare @YTD_I numeric(9, 2)
declare @CHG_DATE date
declare @LOAN_STAT varchar(1)
declare @LOAN_TYPE varchar(3)
declare @REC_STAT varchar(1)
declare @FEBTC_SA varchar(10)																	-- JS 01/26/2013

declare UPLOAD_CURSOR cursor for
select	lon.LOANS_ID,
		lon.PN_NO,
		lon.KBCI_NO,
		ISNULL(lon.PRINCIPAL, 0),
		sum(case
			when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') then isnull(led.DR, 0) - isnull(led.CR, 0)
			else 0
			end
			),
		ISNULL(up.AMOUNT, 0),
		PAY_START, --ISNULL(lon.PAY_START, 0),		-- JS 08/04/2012
		ISNULL(lon.P_BAL, 0),
		ISNULL(lon.I_BAL, 0),
		lon.ARREAR_AS,
		ISNULL(lon.ARREAR_P, 0),
		ISNULL(lon.ARREAR_I, 0),
		ISNULL(lon.ARREAR_OTH, 0),
		lon.PD,
		ISNULL(lon.ACCU_PAYP, 0),
		ISNULL(lon.YTD_I, 0),
		lon.CHG_DATE,
		lon.LOAN_STAT,
		lon.LOAN_TYPE,
		mem.FEBTC_SA																			-- JS 01/26/2013
from	dbo.UPLOAD up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			inner join 
		dbo.LEDGER led on led.PN_NO = lon.PN_NO
			left join
		@LOAN_TYPE_CODES tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	lon.LOAN_TYPE = tmp.TAYP and
		lon.LOAN_STAT = 'R' and
		up.UPLOAD_ID in (select UPLOAD_ID from @UPLOAD where TRACE = 3) and
		lon.LOANS_ID in	(select LOANS_ID from @LOANS)
group
by		lon.LOANS_ID, 
		lon.PN_NO, 
		lon.KBCI_NO, 
		lon.PRINCIPAL, 
		up.AMOUNT, 
		lon.PAY_START, 
		lon.P_BAL, 
		lon.I_BAL, 
		lon.ARREAR_AS, 
		lon.ARREAR_P, 
		lon.ARREAR_I, 
		lon.ARREAR_OTH, 
		lon.PD, 
		lon.ACCU_PAYP, 
		lon.YTD_I, 
		lon.CHG_DATE, 
		lon.LOAN_STAT, 
		lon.LOAN_TYPE,
		mem.FEBTC_SA																			-- JS 01/26/2013

open UPLOAD_CURSOR

fetch	UPLOAD_CURSOR
into	@LOANS_ID,
		@PN_NO,
		@KBCI_NO,
		@PRINCIPAL,
		@XBPRIN,
		@OR_AMOUNT,
		@PAY_START,
		@P_BAL,
		@I_BAL,
		@ARREAR_AS,
		@ARREAR_P,
		@ARREAR_I,
		@ARREAR_OTH,
		@PD,
		@ACCU_PAYP,
		@YTD_I,
		@CHG_DATE,
		@LOAN_STAT,
		@LOAN_TYPE,
		@FEBTC_SA																				-- JS 01/26/2013

while @@FETCH_STATUS = 0
begin

	set	@YBPRIN = 0
	set @YBADV = 0
	set @XBPRIN = @XBPRIN + @PRINCIPAL

	if @XBPRIN < 0
		set @XBPRIN = 0
	
	set @YBPRIN = @XBPRIN
	
	if @PAY_START > @SYSDATE 
		set @XBPRIN = 0
	
	set @XOPRI = 0 
	set @XOINT = 0 
	set @XAPEN = 0 
	set @XAINT = 0 
	set @XAPRI = 0 
	set @XADV = 0 
	set @XAMORTP = 0 
	set @XAMORTI = 0 
	
	set @XAMORTP = @P_BAL
	set @XAMORTI = @I_BAL
	
-- ***************************************************************
-- BEGIN POST
-- ***************************************************************

	set @XPPAY = @OR_AMOUNT
	
	if @ARREAR_OTH > 0 and @XPPAY > 0 begin
		set @XPPAY = @XPPAY - @ARREAR_OTH
		
		if @XPPAY < 0 begin
			set @A2PAY = @XPPAY + @ARREAR_OTH
			set @XPPAY = 0
		end	else begin
			set @A2PAY = @ARREAR_OTH
		end
		
		if @A2PAY > 0 begin
			exec s3p_J_U_Ledger @PN_NO, @SYSDATE, 'DED', @PN_NO, 'PAY', 'OTH', @A2PAY, 'PAYROLL DEDUCTION-PEN', NULL, @MY_USER
			set @XAPEN = @A2PAY
			if (@ARREAR_OTH - @A2PAY) > 0 begin
				set @ARREAR_OTH = @ARREAR_OTH - @A2PAY --!
			end else begin
				set @ARREAR_OTH = 0 --!
			end
		end
	end
	
	if @ARREAR_I > 0 and @XPPAY > 0 begin
		set @XPPAY = @XPPAY - @ARREAR_I
		
		if @XPPAY < 0 begin
			set @A2PAY = @XPPAY + @ARREAR_I
			set @XPPAY = 0
		end	else begin
			set @A2PAY = @ARREAR_I
		end
		
		if @A2PAY > 0 begin
			set @XAINT = @A2PAY
			exec s3p_J_U_Ledger @PN_NO, @SYSDATE, 'DED', @PN_NO, 'PAY', 'INT', @A2PAY, 'PAYROLL DEDUCTION ARR-INT', NULL, @MY_USER
			if (@ARREAR_I - @A2PAY) > 0 begin
				set @ARREAR_I = @ARREAR_I - @A2PAY --!
			end else begin
				set @ARREAR_I = 0 --!
			end
		end
	end
	
	if @ARREAR_P > 0 and @XPPAY > 0 begin
		set @XPPAY = @XPPAY - @ARREAR_P
		
		if @XPPAY < 0 begin
			set @A2PAY = @XPPAY + @ARREAR_P
			set @XPPAY = 0
		end	else begin
			set @A2PAY = @ARREAR_P
		end
		
		if @YBPRIN < @A2PAY begin
			set	@YBADV = @A2PAY - @YBPRIN
			set @A2PAY = @YBPRIN
			set @YBPRIN = 0
		end else begin
			set @YBPRIN = @YBPRIN - @A2PAY
		end
		
		if @A2PAY > 0 begin
			set @XAPRI = @A2PAY
			exec s3p_J_U_Ledger @PN_NO, @SYSDATE, 'DED', @PN_NO, 'PAY', 'PRI', @A2PAY, 'PAYROLL DEDUCTION ARR-PRI', NULL, @MY_USER
			if (@ARREAR_P - @A2PAY) > 0 begin
				set @ARREAR_P = @ARREAR_P - @A2PAY --!
				set @ARREAR_AS = @SYSDATE
			end else begin
				set @ARREAR_P = 0 --!
			end
		end
	end
	
	if @I_BAL > 0 and @XPPAY > 0 begin
		set @XPPAY = @XPPAY - @I_BAL
		
		if @XPPAY < 0 begin
			set @A2PAY = @XPPAY + @I_BAL
			set @XPPAY = 0
		end	else begin
			set @A2PAY = @I_BAL
		end
		
		if @A2PAY > 0 begin
			set @XOINT = @A2PAY
			exec s3p_J_U_Ledger @PN_NO, @SYSDATE, 'DED', @PN_NO, 'PAY', 'INT', @A2PAY, 'PAYROLL DEDUCTION AMORT-INT', NULL, @MY_USER
			if (@I_BAL - @A2PAY) > 0 begin
				set @I_BAL = @I_BAL - @A2PAY --!
			end else begin
				set @I_BAL = 0 --!
			end
		end
	end
	
	if @P_BAL > 0 and @XPPAY > 0 begin
		set @XPPAY = @XPPAY - @P_BAL
		
		if @XPPAY < 0 begin
			set @A2PAY = @XPPAY + @P_BAL
			set @XPPAY = 0
		end	else begin
			set @A2PAY = @P_BAL
		end
		
		if @YBPRIN < @A2PAY begin
			set	@YBADV = @YBADV + (@A2PAY - @YBPRIN)
			set @A2PAY = @YBPRIN
			set @YBPRIN = 0
		end else begin
			set @YBPRIN = @YBPRIN - @A2PAY
		end
		
		if @A2PAY > 0 begin
			set @XOPRI = @A2PAY
			exec s3p_J_U_Ledger @PN_NO, @SYSDATE, 'DED', @PN_NO, 'PAY', 'PRI', @A2PAY, 'PAYROLL DEDUCTION AMORT-PRI', NULL, @MY_USER
			if (@P_BAL - @A2PAY) > 0 begin
				set @P_BAL = @P_BAL - @A2PAY --!
			end else begin
				set @P_BAL = 0 --!
			end
		end
	end
	
	IF @PD = 1 begin
		if @XBPRIN > 0 and @XPPAY > 0 begin
			set @XPPAY = @XPPAY - @XBPRIN
			
			if @XPPAY < 0 begin
				set @A2PAY = @XPPAY + @XBPRIN
				set @XPPAY = 0
			end	else begin
				set @A2PAY = @XBPRIN
			end
			
			if @YBPRIN < @A2PAY begin
				set	@YBADV = @YBADV + (@A2PAY - @YBPRIN)
				set @A2PAY = @YBPRIN
				set @YBPRIN = 0
			end else begin
				set @YBPRIN = @YBPRIN - @A2PAY
			end
			
			if @A2PAY > 0 begin
				set @XOPRI = @XOPRI + @A2PAY
				exec s3p_J_U_Ledger @PN_NO, @SYSDATE, 'DED', @PN_NO, 'PAY', 'PRI', @A2PAY, 'PAYROLL DEDUCTION OUTS-BAL', NULL, @MY_USER
				if (@XBPRIN - @A2PAY) > 0 begin
					set @XBPRIN = @XBPRIN - @A2PAY
				end else begin
					set @XBPRIN = 0
				end
			end
		end
	end
	
	set @XADV = @XPPAY
	set @OR_AMOUNT = @XPPAY

-- ***************************************************************
-- END POST
-- ***************************************************************

	set	@ACCU_PAYP = @ACCU_PAYP + @XOPRI + @XAPRI
	set @YTD_I = @YTD_I + @XOINT + @XAINT
	set @CHG_DATE = @SYSDATE
	
	if @ACCU_PAYP >= @PRINCIPAL begin
		set @LOAN_STAT = 'F'
	end
	
	set @REC_STAT = 0
	set @OR_AMOUNT = ISNULL(@OR_AMOUNT,0) + ISNULL(@YBADV,0)
	
	update	#MO_DEDN
	set		[EMPNO] = src.[EMPNO],
			[KBCI_NO] = src.[KBCI_NO],
			[NAME] = src.[NAME], 
			[PN_NO] = src.[PN_NO],
			[LOAN_TYPE] = src.[LOAN_TYPE], 
			[AMORT_PRI] = src.[AMORT_PRI], 
			[AMORT_INT] = src.[AMORT_INT],
			[DEDUCTION] = src.[DEDUCTION], 
			[PRINCIPAL] = src.[PRINCIPAL], 
			[INTEREST] = src.[INTEREST],
			[ARREARS] = src.[ARREARS], 
			[ADVANCE] = src.[ADVANCE], 
			[DATE] = src.[DATE], 
			[USER] = src.[USER],
			[ARR_PRI] = src.[ARR_PRI], 
			[ARR_INT] = src.[ARR_INT], 
			[CODE5] = src.[CODE5], 
			[PD] = src.[PD]
	from	#MO_DEDN tgt
			inner join (
				select	up.UPLOAD_ID,
						mem.CB_EMPNO EMPNO,
						up.CODE5,
						lon.KBCI_NO,
						lon.PN_NO,
						lon.PD,
						up.AMOUNT DEDUCTION,
						isnull(tmp.TAYP, 'OTH') LOAN_TYPE,
						isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , '') NAME,
						@SYSDATE [DATE],
						@MY_USER [USER],
						isnull(@XAMORTP, 0) AMORT_PRI,
						isnull(@XAMORTI, 0) AMORT_INT,
						isnull(@XOPRI, 0) PRINCIPAL,
						isnull(@XOINT, 0) INTEREST,
						isnull(@XAPEN, 0) ARREARS,
						isnull(@XAPRI, 0) ARR_PRI,
						isnull(@XAINT, 0) ARR_INT,
						case
							when @OR_AMOUNT > 0 THEN @OR_AMOUNT
							else ADVANCE
							end ADVANCE
				from	dbo.LOANS lon
							inner join
						dbo.MEMBERS mem
							on lon.KBCI_NO = mem.KBCI_NO
							inner join
						@LOAN_TYPE_CODES tmp
							on tmp.TAYP = lon.LOAN_TYPE
							inner join
						dbo.UPLOAD up on
							up.EMP_NUM = mem.CB_EMPNO and		-- JS 06/02/2012
							up.ACCT_CODE = tmp.ACCT_CODE		-- JS 06/02/2012
				where	lon.LOANS_ID = @LOANS_ID
			) src on src.UPLOAD_ID = tgt.UPLOAD_ID
	
	insert	#MO_DEDN (
			EMPNO,
			CODE5,
			KBCI_NO,
			PN_NO,
			PD,
			DEDUCTION,
			LOAN_TYPE,
			[NAME],
			[DATE],
			[USER],
			AMORT_PRI,
			AMORT_INT,
			PRINCIPAL,
			INTEREST,
			ARREARS,
			ARR_PRI,
			ARR_INT,
			ADVANCE
			)
	select	mem.CB_EMPNO,
			up.CODE5,
			lon.KBCI_NO,
			lon.PN_NO,
			lon.PD,
			up.AMOUNT,
			isnull(tmp.TAYP, 'OTH'),
			isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
			@SYSDATE,
			@MY_USER,
			isnull(@XAMORTP, 0),
			isnull(@XAMORTI, 0),
			isnull(@XOPRI, 0),
			isnull(@XOINT, 0),
			isnull(@XAPEN, 0),
			isnull(@XAPRI, 0),
			isnull(@XAINT, 0),
			case
				when @OR_AMOUNT > 0 THEN @OR_AMOUNT
				else ADVANCE
				end
	from	dbo.LOANS lon
				inner join
			dbo.MEMBERS mem
				on lon.KBCI_NO = mem.KBCI_NO
				inner join
			dbo.UPLOAD up
				on up.EMP_NUM = mem.CB_EMPNO
				left join
			@LOAN_TYPE_CODES tmp
				on tmp.ACCT_CODE = up.ACCT_CODE
	where	lon.LOANS_ID = @LOANS_ID and
			up.UPLOAD_ID not in (select UPLOAD_ID from #MO_DEDN)
	
	-- set @OR_AMOUNT = @OR_AMOUNT + @YBADV
	if @OR_AMOUNT > 0 begin
		insert	dbo.ADVANCE (
				PN_NO,
				KBCI_NO,
				LOAN_TYPE,
				REMARKS,
				ADD_DATE,
				[USER],
				AMOUNT,
				ACCTNO																			-- JS 01/26/2013
				)
		select	@PN_NO,
				@KBCI_NO,
				@LOAN_TYPE,
				case
					when @PD = 1 and @LOAN_STAT =  'F' then 'PD-REFUND: FULLY PAID LOAN ' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  MM, @SYSDATE)), 2) + '/' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  DD, @SYSDATE)), 2) + '/' + RIGHT('0000' + CONVERT(VARCHAR(4), DATEPART(YYYY, @SYSDATE)), 4)
					when @PD = 1 and @LOAN_STAT != 'F' then 'PD-PAYROLL POSTING ADVANCE ' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  MM, @SYSDATE)), 2) + '/' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  DD, @SYSDATE)), 2) + '/' + RIGHT('0000' + CONVERT(VARCHAR(4), DATEPART(YYYY, @SYSDATE)), 4)
					when @PD = 0 then 'PAYROLL POSTING ADVANCE ' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  MM, @SYSDATE)), 2) + '/' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  DD, @SYSDATE)), 2) + '/' + RIGHT('0000' + CONVERT(VARCHAR(4), DATEPART(YYYY, @SYSDATE)), 4)
					end,
				@SYSDATE,
				@MY_USER,
				@OR_AMOUNT,
				@FEBTC_SA																		-- JS 01/26/2013
	end
	
	--insert	dbo.MO_DEDN (
	--		EMPNO,CODE5,KBCI_NO,PN_NO,PD,DEDUCTION,LOAN_TYPE,[NAME],[DATE],[USER],AMORT_PRI,AMORT_INT,PRINCIPAL,INTEREST,ARREARS,ARR_PRI,ARR_INT,ADVANCE)
	--select	EMPNO,CODE5,KBCI_NO,PN_NO,PD,DEDUCTION,LOAN_TYPE,[NAME],[DATE],[USER],AMORT_PRI,AMORT_INT,PRINCIPAL,INTEREST,ARREARS,ARR_PRI,ARR_INT,ADVANCE
	--from	#MO_DEDN
			
	---- HPOST
	
	--insert	dbo.MO_DEDNH (
	--		EMPNO,CODE5,KBCI_NO,PN_NO,PD,DEDUCTION,LOAN_TYPE,[NAME],[DATE],[USER],AMORT_PRI,AMORT_INT,PRINCIPAL,INTEREST,ARREARS,ARR_PRI,ARR_INT,ADVANCE)
	--select	EMPNO,CODE5,KBCI_NO,PN_NO,PD,DEDUCTION,LOAN_TYPE,[NAME],[DATE],[USER],AMORT_PRI,AMORT_INT,PRINCIPAL,INTEREST,ARREARS,ARR_PRI,ARR_INT,ADVANCE
	--from	#MO_DEDN
	
	--delete	table #MO_DEDN

	--insert	MO_DEDNH ([DATE], EMPNO, LOAN_TYPE, KBCI_NO, NAME, PN_NO, AMORT_PRI, AMORT_INT, DEDUCTION, PRINCIPAL, INTEREST,	ARREARS, ADVANCE, [USER], ARR_PRI, ARR_INT, CODE5, PD)
	--select	md.[DATE], md.EMPNO, md.LOAN_TYPE, md.KBCI_NO, md.NAME, md.PN_NO, 0, 0, md.DEDUCTION, 0, 0, 0, md.ADVANCE, md.[USER], 0, 0, md.CODE5, md.PD	
	--from	dbo.LOANS lon
	--			inner join
	--		dbo.MEMBERS mem
	--			on lon.KBCI_NO = mem.KBCI_NO
	--			inner join
	--		dbo.MO_DEDN md
	--			on md.EMPNO = mem.CB_EMPNO
	--			inner join
	--		dbo.UPLOAD up
	--			on up.EMP_NUM = mem.CB_EMPNO
	--where	lon.LOANS_ID = @LOANS_ID
	
	-- HPOST WITH COMPUTED POST COMPUTED VALUES
	
	--insert	MO_DEDNH ([DATE], EMPNO, LOAN_TYPE, KBCI_NO, NAME, PN_NO, AMORT_PRI, AMORT_INT, DEDUCTION, PRINCIPAL, INTEREST,	ARREARS, ADVANCE, [USER], ARR_PRI, ARR_INT, CODE5, PD)
	--select	md.[DATE], md.EMPNO, md.LOAN_TYPE, md.KBCI_NO, md.NAME, md.PN_NO, md.AMORT_PRI, md.AMORT_INT, md.DEDUCTION, md.PRINCIPAL, md.INTEREST, md.ARREARS, md.ADVANCE, md.[USER], md.ARR_PRI, md.ARR_INT, md.CODE5, md.PD	
	--from	dbo.LOANS lon
	--			inner join
	--		dbo.MEMBERS mem
	--			on lon.KBCI_NO = mem.KBCI_NO
	--			inner join
	--		dbo.MO_DEDN md
	--			on md.EMPNO = mem.CB_EMPNO
	--			inner join
	--		dbo.UPLOAD up
	--			on up.EMP_NUM = mem.CB_EMPNO
	--where	lon.LOANS_ID = @LOANS_ID
	
	update	dbo.LOANS
	set		PRINCIPAL = @PRINCIPAL,
			PAY_START =	@PAY_START,
			P_BAL = @P_BAL,
			I_BAL = @I_BAL,
			ARREAR_AS = @ARREAR_AS,
			ARREAR_P = @ARREAR_P,
			ARREAR_I = @ARREAR_I,
			ARREAR_OTH = @ARREAR_OTH,
			PD = @PD,
			ACCU_PAYP = @ACCU_PAYP,
			YTD_I = @YTD_I,
			CHG_DATE = @CHG_DATE,
			LOAN_STAT = @LOAN_STAT,
			REC_STAT = @REC_STAT
	where	LOANS_ID = @LOANS_ID
	
	fetch	UPLOAD_CURSOR
	into	@LOANS_ID,
			@PN_NO,
			@KBCI_NO,
			@PRINCIPAL,
			@XBPRIN,
			@OR_AMOUNT,
			@PAY_START,
			@P_BAL,
			@I_BAL,
			@ARREAR_AS,
			@ARREAR_P,
			@ARREAR_I,
			@ARREAR_OTH,
			@PD,
			@ACCU_PAYP,
			@YTD_I,
			@CHG_DATE,
			@LOAN_STAT,
			@LOAN_TYPE,
			@FEBTC_SA																			-- JS 01/26/2013
			
--	exec s3p_Loans_LoansNew_RecomputeBalance @PN_NO												-- JS 01/18/2014
end

close UPLOAD_CURSOR
deallocate UPLOAD_CURSOR

insert	dbo.MO_DEDN (
		[EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
		)
select	[EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
from	#MO_DEDN

insert	dbo.MO_DEDNH (
		[EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
		)
select	[EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD]
from	#MO_DEDN

insert	dbo.MO_DEDNP (
		PN_NO,
		KBCI_NO,
		DEDUCTION,
		LOAN_TYPE,
		TERM,
		FREQ,
		LOAN_STAT,
		NAME,
		EMPNO,
		TEL,
		ARREARS,
		ARR_INT,
		INTEREST,
		PRINCIPAL,
		[DATE]
		)
select	lon.PN_NO,
		lon.KBCI_NO,
		lon.AMORT_AMT,
		lon.LOAN_TYPE,
		TERM,
		FREQ,
		LOAN_STAT,
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		mem.CB_EMPNO,
		ltrim(rtrim(mem.OFF_TEL)) + '/' + ltrim(rtrim(mem.RES_TEL)),
		lon.ARREAR_OTH,
		lon.ARREAR_I,
		I_BAL,
		P_BAL,
		getdate()
from	dbo.LOANS lon
			inner join
		dbo.MEMBERS mem
			on lon.KBCI_NO = mem.KBCI_NO			
where	CHARINDEX('1', lon.MOD_PAY) > 0 and
		lon.NDUE = @SYSDATE and (
			lon.LOAN_STAT = 'R' or
			lon.LOAN_STAT = 'P'
		) and 
		PN_NO not in (select PN_NO from MO_DEDN) and
		lon.PD = 0 AND																				-- JS 03/05/2016
		mem.MEM_STAT != 'S'

update	dbo.CTRL																					-- JS 11/17/2012
set		[PROC] = @SYSDATE																			-- JS 11/17/2012

insert	@VOUCHER
exec	S3P_J_GEN_LAPP 'V'

insert	MO_DEDNH (
		[DATE],
		EMPNO,
		NAME,
		LOAN_TYPE,
		PN_NO,
		DEDUCTION
		)
values	(
		@SYSDATE,
		'999999',
		@SOD_TXT,
		'XXX',
		(select top 1 XPN_NO from @VOUCHER),
		@SOD_TOT
		)



GO