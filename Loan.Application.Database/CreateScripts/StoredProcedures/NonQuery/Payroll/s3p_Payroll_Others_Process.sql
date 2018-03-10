USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Others_Process]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Payroll_Others_Process]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Payroll_Others_Process]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Others_Process]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*****************************************************************************
MODIFIED:
JS 01/05/2013	DISABLED PAYMENT TO OTHER LOANS
JS 01/26/2013	INSERTED MISSING ACCTNO
JS 11/09/2013	FIXED PROCESSING DATE
*****************************************************************************/

CREATE PROCEDURE [dbo].[s3p_Payroll_Others_Process]
@MY_USER varchar(8),
@SOD_TXT varchar(35),
@SOD_TOT numeric(13,4)
as

-- ***************************************************************
-- OTHERS DECLARATIONS
-- ***************************************************************

declare @XMINBAL numeric(14, 2)
declare @PROC_DATE datetime

-- ***************************************************************
-- PAYROLL DECLARATIONS
-- ***************************************************************

declare @SYSDATE date
declare @ORMKS1 varchar(10) = 'OTHERPAY'
declare @ORMKS2 varchar(20) = 'OTHERPAY REVERSAL'

declare @UPLOADO table (
	UPLOADO_ID bigint,
	TRACE int
)

declare @LOANS table (
	LOANS_ID bigint
)

declare @JOEL table (
	ACCT_CODE varchar(6),
	TAYP varchar(3)
)

declare @VOUCHER table (
	XPN_NO VARCHAR(9)
)

create table #MO_DEDNO (
	[UPLOADO_ID] bigint,
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
	[PD] bit,
	[PAYCODE] varchar(8)
)

select	@PROC_DATE = MAX([PROC]),
		@SYSDATE = MAX([SYSDATE]),
		@XMINBAL = MAX(ISNULL(MINBAL, 0))
from	dbo.CTRL

--truncate table dbo.MO_DEDNO		-- JS 06/30/2012
truncate table dbo.UPLOADO			-- JS 06/30/2012

insert	dbo.UPLOADO (
		EMP_NUM,
		ACCT_CODE,
		AMOUNT,
		CODE5
		)
select	right('000000' + CONVERT(VARCHAR(6),ISNULL(EMPNO1,0)), 6), --TODO: UPLOADO table in foxpro not padded? :O
		right('0' + ISNULL(ACTYPE,0), 1) + right('0' + ISNULL(ACTCD1,0), 1) + right('0000' + ISNULL(ACTCD2,0), 4),
		ISNULL(AMT7C,0),
		CODE5
from	dbo.EXTKBCO

--truncate table dbo.EXTKBCO			-- JS 06/30/2012

update	dbo.UPLOADO
set		ACCT_CODE = '007601'
where	ACCT_CODE = '007665'

insert	@JOEL 
(
		ACCT_CODE, 
		TAYP
) 
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
-- BEGIN - IF UPLOADO.emp_num does not exist in MEMBERS.DBF
-- ***************************************************************

update	dbo.UPLOADO
set		TRANS_CODE = 'X'
where	EMP_NUM not in (
		select	EMP_NUM
		from	dbo.UPLOADO up
				inner join dbo.MEMBERS mem on mem.CB_EMPNO = up.EMP_NUM
		)

insert	#MO_DEDNO (
		UPLOADO_ID,
		EMPNO,
		DEDUCTION,
		[DATE],
		[USER],
		LOAN_TYPE,
		CODE5,
		PAYCODE
		)
select	up.UPLOADO_ID,
		up.EMP_NUM,
		up.AMOUNT,
		@SYSDATE,
		@MY_USER,
		isnull(tmp.TAYP, 'OTH'),
		up.CODE5,
		@ORMKS1
from	dbo.UPLOADO up
			left join
		@JOEL tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	up.EMP_NUM not in (
		select	EMP_NUM
		from	dbo.UPLOADO up
				inner join dbo.MEMBERS mem on mem.CB_EMPNO = up.EMP_NUM
		)

insert	@UPLOADO (UPLOADO_ID, TRACE)
select	UPLOADO_ID, 10
from	#MO_DEDNO

-- END - HPOST
-- END - IF UPLOADO.emp_num does not exist in MEMBERS.DBF

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
		@ORMKS1,
		up.AMOUNT,
		mem.FD_AMOUNT + up.AMOUNT,
		'CR',
		1,
		@ORMKS1 + ' DEDUCTION',
		@SYSDATE,
		@MY_USER		
from	dbo.UPLOADO up
			left join
		@JOEL tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.FD fd
			on fd.KBCI_NO = mem.KBCI_NO
where	tmp.TAYP = 'FIX' and 
		up.UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

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
					dbo.UPLOADO up
						on mem.CB_EMPNO = up.EMP_NUM
						left join
					@JOEL tmp
						on tmp.ACCT_CODE = up.ACCT_CODE
			where	tmp.TAYP = 'FIX' and 
					up.UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)
			)
			as b on a.KBCI_ID = b.KBCI_ID

insert	#MO_DEDNO (
		UPLOADO_ID,
		EMPNO,
		KBCI_NO,
		DEDUCTION,
		[DATE],
		[USER],
		LOAN_TYPE,
		[NAME],
		CODE5,
		PAYCODE
		)
select	up.UPLOADO_ID,
		mem.CB_EMPNO,
		mem.KBCI_NO,
		up.AMOUNT,
		@SYSDATE,
		@MY_USER,
		isnull(tmp.TAYP, 'OTH'),
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		up.CODE5,
		@ORMKS1
from	dbo.MEMBERS mem
			inner join
		dbo.UPLOADO up
			on up.EMP_NUM = mem.CB_EMPNO
			left join
		@JOEL tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	tmp.TAYP = 'FIX' and 
		up.UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

-- BEGIN - HPOST

insert	@UPLOADO (UPLOADO_ID, TRACE)
select	UPLOADO_ID, 20
from	#MO_DEDNO
where	UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

-- END - HPOST
-- END - IF tayp='FIX'

---- ***************************************************************
---- BEGIN - OFFCYCLE - APPLY TO SA
---- ***************************************************************

declare @sav table
(
	UPLOADO_ID bigint,
	PREV_FLG bit default 0,
	PUPL_AMT numeric(10, 2) default 0,
	PLON_AMT numeric(10, 2) default 0,
	PSAV_AMT numeric(10, 2) default 0,
	ACCTABAL numeric(14, 2),
	HOLDAMT  numeric(14, 2),
	HOLDTYPE varchar(2),
	POSTSTAT varchar(1),
	XABAL    numeric(14, 2),
	SDMASTER BIT,
	LNHOLD   BIT
)

insert	@sav
(
		UPLOADO_ID,
		PUPL_AMT,
		PLON_AMT,
		ACCTABAL,
		HOLDAMT,
		HOLDTYPE,
		POSTSTAT,
		SDMASTER,
		LNHOLD
)
select	up.UPLOADO_ID,
		up.AMOUNT,
		0 - up.AMOUNT,
		ISNULL(sdm.ACCTABAL, 0),
		ISNULL(lnh.HOLDAMT, 0),
		ISNULL(lnh.HOLDTYPE, ''),
		ISNULL(lnh.POSTSTAT, ''),
		case
			when isnull(sdm.ACCTNO, '') != '' then 1
			else 0
			end,
		case
			when isnull(lnh.ACCTNO, '') != '' then 1
			else 0
			end
from	dbo.UPLOADO up
			inner join
		dbo.LOAN_TYPE lt
			on lt.LOAN_TYPE = up.ACCT_CODE and lt.LOAN_TYPE != 'STL'
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.SDMASTER sdm
			on sdm.ACCTNO = mem.FEBTC_SA
			left join
		dbo.LNHOLD lnh
			on lnh.ACCTNO = mem.FEBTC_SA
where	up.AMOUNT < 0 and
		up.UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO);

update	@sav
set		XABAL = case
			when SDMASTER = 1 and ACCTABAL > @XMINBAL then ACCTABAL - @XMINBAL
			else XABAL
			end;

update	@sav
set		XABAL = case
			when SDMASTER = 1 and ACCTABAL > @XMINBAL and LNHOLD = 1 and HOLDTYPE = 'DM' then XABAL - HOLDAMT
			when SDMASTER = 1 and ACCTABAL > @XMINBAL and LNHOLD = 1 then XABAL + HOLDAMT
			else XABAL
			end;

update	@sav
set		PSAV_AMT = case
			when (XABAL > 0) and ((XABAL - (0 - PUPL_AMT)) > 0) then 0 - PUPL_AMT
			when (XABAL > 0) and ((XABAL - (0 - PUPL_AMT)) < 0) then XABAL
			else PSAV_AMT
			end,
		PLON_AMT = case
			when (XABAL > 0) and ((XABAL - (0 - PUPL_AMT)) > 0) then 0
			when (XABAL > 0) and ((XABAL - (0 - PUPL_AMT)) < 0) then (0 - PUPL_AMT) - XABAL
			else PLON_AMT
			end;

insert	#MO_DEDNO
(
		EMPNO,
		KBCI_NO,
		DEDUCTION,
		LOAN_TYPE,
		NAME,
		[DATE],
		[USER],
		CODE5,
		PAYCODE,
		ADVANCE
)
select	mem.CB_EMPNO,
		mem.KBCI_NO,
		0 - sav.PSAV_AMT,
		'SAV',
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		@SYSDATE,
		@MY_USER,
		up.CODE5,
		@ORMKS1,
		0 - sav.PSAV_AMT
from	dbo.UPLOADO up
			inner join
		dbo.LOAN_TYPE lt
			on lt.LOAN_TYPE = up.ACCT_CODE and lt.LOAN_TYPE != 'STL'
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		@sav sav
			on sav.UPLOADO_ID = up.UPLOADO_ID
where	sav.PSAV_AMT > 0;

insert	dbo.LNHOLD
(
		ACCTNO,
		HOLDCD,
		HOLDTYPE,
		HOLDAMT,
		HOLDDATE,
		HOLDUSER,
		HOLDRMKS
)
select	mem.FEBTC_SA,
		'REV',
		'DM',
		sav.PSAV_AMT,
		@SYSDATE,
		@MY_USER,
		'REVERSAL: ' + CONVERT(VARCHAR, @SYSDATE, 101) + ' APPLIED TO SA '
from	dbo.UPLOADO up
			inner join
		dbo.LOAN_TYPE lt
			on lt.LOAN_TYPE = up.ACCT_CODE and lt.LOAN_TYPE != 'STL'
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		@sav sav
			on sav.UPLOADO_ID = up.UPLOADO_ID
where	sav.PSAV_AMT > 0;

update	up
set		up.AMOUNT = 0 - sav.PLON_AMT
from	UPLOADO up
			inner join
		@sav sav on
			sav.UPLOADO_ID = up.UPLOADO_ID
where	sav.PSAV_AMT > 0;

update	@sav
set		PREV_FLG = 1
where	PLON_AMT > 0;

insert	@UPLOADO (UPLOADO_ID, TRACE)
select	UPLOADO_ID, 27
from	@sav
where	PLON_AMT = 0;

---- ***************************************************************
---- BEGIN - OFFCYCLE - PAY TO OTHER LOANS
---- DISABLED
---- ***************************************************************

--declare @XLNSTR table
--(
--	LOAN_TYPE varchar(3),
--	RANKING int
--)

--declare @XLFIND table
--(
--	UPLOADO_ID bigint,
--	KBCI_NO varchar(7),
--	OLD_ACCT_CODE varchar(6),
--	NEW_ACCT_CODE varchar(6)
--)

--/* get hardcoded ranking */

--insert	@XLNSTR
--		(LOAN_TYPE, RANKING)
--values	('EML', 1), 
--		('APL', 2),
--		('CML', 3),
--		('EDL', 4),
--		('RGL', 5),
--		('SPL', 6),
--		('RSL', 7)

--/* get others and rank */

--insert	@XLNSTR
--		(
--		LOAN_TYPE,
--		RANKING
--		)
--select	LOAN_TYPE,
--		RANK() over (order by CODE5) * 10
--from	LOAN_TYPE
--where	CODE5 is not NULL and
--		LOAN_TYPE not in (select LOAN_TYPE from @XLNSTR)

--/* get members with other outstanding loans */

--insert	@XLFIND
--		(
--		UPLOADO_ID,
--		KBCI_NO,
--		OLD_ACCT_CODE
--		)
--select	up.UPLOADO_ID,
--		lon.KBCI_NO,
--		up.ACCT_CODE
--from	dbo.UPLOADO up
--			inner join
--		dbo.MEMBERS mem
--			on mem.CB_EMPNO = up.EMP_NUM
--			inner join
--		dbo.LOANS lon
--			on lon.KBCI_NO = mem.KBCI_NO
--			left join
--		@JOEL tmp
--			on tmp.ACCT_CODE = up.ACCT_CODE
--			left join
--		@sav sav
--			on sav.UPLOADO_ID = up.UPLOADO_ID
--where	lon.LOAN_TYPE = tmp.TAYP and
--		lon.LOAN_STAT != 'R' and
--		isnull(sav.PREV_FLG, 0) = 1 and
--		up.UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

--/* get other outstanding loans */

--update	@XLFIND
--set		NEW_ACCT_CODE = one.CODE5
--from	@XLFIND xlfind
--			inner join
--		(
--			select	other.UPLOADO_ID,
--					other.CODE5
--			from	(
--						select	mem.UPLOADO_ID,
--								lt.CODE5,
--								RANK() over (partition by lon.PN_NO order by lon.PN_NO, temp.RANKING) as NEW_RANKING
--						from	@XLFIND mem
--									inner join
--								dbo.LOANS lon on
--									lon.KBCI_NO = mem.KBCI_NO
--									inner join
--								dbo.LOAN_TYPE lt on
--									lt.LOAN_TYPE = lon.LOAN_TYPE
--									inner join
--								@XLNSTR temp on
--									temp.LOAN_TYPE = lon.LOAN_TYPE
--						where	lon.LOAN_STAT = 'R'
--					) other
--			where	other.NEW_RANKING = 1
--		) one on
--			one.UPLOADO_ID = xlfind.UPLOADO_ID
			
--update	up
--set		ACCT_CODE = xlfind.NEW_ACCT_CODE
--from	dbo.UPLOADO up
--			inner join
--		@XLFIND xlfind on
--			xlfind.UPLOADO_ID = up.UPLOADO_ID

-- ***************************************************************
-- BEGIN - IF mhi = .T.
-- ***************************************************************

insert	@LOANS (LOANS_ID)
select	lon.LOANS_ID
from	dbo.UPLOADO up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			left join
		@JOEL tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	lon.LOAN_TYPE = tmp.TAYP and
		lon.LOAN_STAT = 'R' and
		up.UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

insert	#MO_DEDNO (
		UPLOADO_ID,
		EMPNO,
		CODE5,
		PAYCODE,
		KBCI_NO,
		PN_NO,
		PD,
		DEDUCTION,
		LOAN_TYPE,
		[NAME],
		[DATE],
		[USER]
		)
select	up.UPLOADO_ID,
		mem.CB_EMPNO,
		up.CODE5,
		@ORMKS1,
		mem.KBCI_NO,
		lon.PN_NO,
		lon.PD,
		up.AMOUNT,
		isnull(tmp.TAYP, 'OTH'),
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		@SYSDATE,		
		@MY_USER
from	dbo.UPLOADO up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			left join
		@JOEL tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
where	lon.LOAN_TYPE = tmp.TAYP and
		lon.LOAN_STAT = 'R' and
		CHARINDEX('1', lon.MOD_PAY) > 0 and
		up.UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

insert	@UPLOADO (UPLOADO_ID, TRACE)
select	UPLOADO_ID, 30
from	#MO_DEDNO
where	UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

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
from	#MO_DEDNO
where	UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO where TRACE = 30)

-- END - IF mhi = .T.

-- ***************************************************************
-- BEGIN - IF mhi = .F.
-- ***************************************************************

update	dbo.UPLOADO
set		TRANS_CODE = 'N'
where	UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO) and
		UPLOADO_ID not in (
		select	distinct
				up.UPLOADO_ID
		from	dbo.UPLOADO up
					inner join
				dbo.MEMBERS mem
					on mem.CB_EMPNO = up.EMP_NUM
					left join
				dbo.LOANS lon
					on lon.KBCI_NO = mem.KBCI_NO
					left join
				@JOEL tmp
					on tmp.ACCT_CODE = up.ACCT_CODE
		where	lon.LOAN_TYPE = tmp.TAYP and
				lon.LOAN_STAT = 'R' and
				CHARINDEX('1', lon.MOD_PAY) = 0
		)

insert	#MO_DEDNO (
		UPLOADO_ID,
		EMPNO,
		KBCI_NO,
		DEDUCTION,
		[DATE],
		[USER],
		LOAN_TYPE,
		[NAME],
		CODE5,
		PAYCODE,
		ADVANCE
		)
select	distinct
		up.UPLOADO_ID,
		mem.CB_EMPNO,
		mem.KBCI_NO,
		up.AMOUNT,
		@SYSDATE,
		@MY_USER,
		case ISNULL(sav.PREV_FLG, 0)
			when 1 then 'AR '
			when 0 then tmp.TAYP
			end,
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		up.CODE5,
		@ORMKS1,
		up.AMOUNT
from	dbo.UPLOADO up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			left join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			left join
		@JOEL tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
			left join
		@sav sav
			on sav.UPLOADO_ID = up.UPLOADO_ID
where	up.TRANS_CODE = 'N'

-- BEGIN - HPOST

insert	@UPLOADO (UPLOADO_ID, TRACE)
select	UPLOADO_ID, 40
from	#MO_DEDNO
where	UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

-- END - HPOST

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
from	#MO_DEDNO dedno
			inner join 
		dbo.MEMBERS mem on 
			mem.KBCI_NO = dedno.KBCI_NO
			inner join
		@UPLOADO up on
			up.UPLOADO_ID = dedno.UPLOADO_ID
			left join
		@sav sav
			on sav.UPLOADO_ID = dedno.UPLOADO_ID
where	isnull(sav.PREV_FLG, 0) = 0 and
		up.TRACE = 40

-- END - IF mhi = .F.

-- ***************************************************************
-- BEGIN - IF mhi = .T. and .not. '1' $ mpay and .not. prev_flg
-- ***************************************************************

insert	#MO_DEDNO (
		UPLOADO_ID,
		EMPNO,
		KBCI_NO,
		DEDUCTION,
		[DATE],
		[USER],
		LOAN_TYPE,
		[NAME],
		CODE5,
		PAYCODE,
		ADVANCE
		)
select	distinct
		up.UPLOADO_ID,
		mem.CB_EMPNO,
		mem.KBCI_NO,
		up.AMOUNT,
		@SYSDATE,
		@MY_USER,
		isnull(tmp.TAYP, 'OTH'),
		isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.' , ''),
		up.CODE5,
		@ORMKS1,
		up.AMOUNT
from	dbo.UPLOADO up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			left join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			left join
		@JOEL tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
			left join
		@sav sav
			on sav.UPLOADO_ID = up.UPLOADO_ID
where	lon.LOAN_TYPE = tmp.TAYP and
		lon.LOAN_STAT = 'R' and
		CHARINDEX('1', lon.MOD_PAY) = 0 and
		ISNULL(sav.PREV_FLG, 0) = 0
				
-- BEGIN - HPOST

insert	@UPLOADO (UPLOADO_ID, TRACE)
select	UPLOADO_ID, 50
from	#MO_DEDNO
where	UPLOADO_ID not in (select UPLOADO_ID from @UPLOADO)

-- END - HPOST

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
from	#MO_DEDNO dedno
			inner join
		dbo.MEMBERS mem on 
			mem.KBCI_NO = dedno.KBCI_NO
			inner join
		@UPLOADO up on
			up.UPLOADO_ID = dedno.UPLOADO_ID
where	up.TRACE = 50

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

declare @I_PAID numeric(11, 2) = 0
declare @P_PAID numeric(11, 2) = 0
declare @PLON_AMT numeric(10, 2) = 0
declare @MY_ARREARS_OTH numeric(10, 2) = 0
declare @MY_ARREARS_INT numeric(10, 2) = 0
declare @AMORT_AMT numeric(10, 2) = 0
declare @ORMKS varchar(30)
declare @XLASTD datetime
declare @PPDATE datetime
declare @PREV_FLG bit

declare UPLOADO_CURSOR cursor for
select	lon.LOANS_ID,
		lon.PN_NO,
		lon.KBCI_NO,
		ISNULL(lon.PRINCIPAL, 0) AS 'PRINCIPAL',
		sum(case
			when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') then isnull(led.DR, 0) - isnull(led.CR, 0)
			else 0
			end
			) AS 'XBPRIN',
		max(case
			when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') then led.[DATE]
			end
			) AS 'XLASTD',
		sum(case
			when led.ACCT_CODE = 'INT' and led.ACCT_TYPE in ('PAY', 'ADJ', 'TER') and led.[DATE] >= @PROC_DATE then isnull(led.DR, 0) - isnull(led.CR, 0)
			else 0
			end
			) AS 'I_PAID',
		sum(case
			when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'TER') and led.[DATE] >= @PROC_DATE then isnull(led.DR, 0) - isnull(led.CR, 0)
			else 0
			end
			) AS 'P_PAID',
		ISNULL(up.AMOUNT, 0) AS 'OR_AMOUNT',
		PAY_START, --ISNULL(lon.PAY_START, 0),		-- JS 08/04/2012
		ISNULL(lon.P_BAL, 0) AS 'P_BAL',
		ISNULL(lon.I_BAL, 0) AS 'I_BAL',
		lon.ARREAR_AS,
		ISNULL(lon.ARREAR_P, 0) AS 'ARREAR_P',
		ISNULL(lon.ARREAR_I, 0) AS 'ARREAR_I',
		ISNULL(lon.ARREAR_OTH, 0) AS 'ARREAR_OTH',
		lon.PD,
		ISNULL(lon.ACCU_PAYP, 0) AS 'ACCU_PAYP',
		ISNULL(lon.YTD_I, 0) AS 'YTD_I',
		lon.CHG_DATE,
		lon.LOAN_STAT,
		lon.LOAN_TYPE,
		ISNULL(sav.PREV_FLG, 0) AS 'PREV_FLG',
		sav.PLON_AMT,
		lon.AMORT_AMT,
		mem.FEBTC_SA																			-- JS 01/26/2013
from	dbo.UPLOADO up
			inner join
		dbo.MEMBERS mem
			on mem.CB_EMPNO = up.EMP_NUM
			inner join
		dbo.LOANS lon
			on lon.KBCI_NO = mem.KBCI_NO
			inner join 
		dbo.LEDGER led on led.PN_NO = lon.PN_NO
			left join
		@JOEL tmp
			on tmp.ACCT_CODE = up.ACCT_CODE
			left join
		@sav sav
			on sav.UPLOADO_ID = up.UPLOADO_ID
where	lon.LOAN_TYPE = tmp.TAYP and
		lon.LOAN_STAT = 'R' and
		up.UPLOADO_ID in (select UPLOADO_ID from @UPLOADO where TRACE = 30) and
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
		sav.PREV_FLG, 
		sav.PLON_AMT, 
		lon.AMORT_AMT,
		mem.FEBTC_SA																			-- JS 01/26/2013

open UPLOADO_CURSOR

fetch	UPLOADO_CURSOR
into	@LOANS_ID,
		@PN_NO,
		@KBCI_NO,
		@PRINCIPAL,
		@XBPRIN,
		@XLASTD,
		@I_PAID,
		@P_PAID,
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
		@PREV_FLG,
		@PLON_AMT,
		@AMORT_AMT,
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
	
	if @PREV_FLG = 1
	begin
		exec s3p_J_U_Ledger @PN_NO, @SYSDATE, 'DM', @PN_NO, 'PAY', 'PRI', @PLON_AMT, @ORMKS2, NULL, @MY_USER
		set @XOPRI = 0 - @PLON_AMT
	end
	else
	begin
	-- ***************************************************************
	-- BEGIN POST
	-- ***************************************************************

		-- BEGIN OFFCYCLE
		
		set @PPDATE = @SYSDATE
		set @MY_ARREARS_OTH = @ARREAR_OTH
		set @MY_ARREARS_INT = @ARREAR_I
		
		if @ARREAR_AS > @PROC_DATE and @SYSDATE < DATEADD(M, 1, @PROC_DATE)
		begin
			--set @ARREAR_OTH = 0																						-- JS 01/16/2016.START
			
			--if @P_PAID > 0
			--begin
			--	set @ARREAR_I = 0
			--end
			--else
			--begin
			--	set @ARREAR_I = @AMORT_AMT - (@ARREAR_P - @P_PAID) + @I_PAID
				
			--	if @ARREAR_I < 0
			--	begin
			--		set @ARREAR_I = 0
			--	end
				
			--	if @OR_AMOUNT < @ARREAR_I
			--	begin
			--		set @ARREAR_OTH = @MY_ARREARS_OTH
			--		set @ARREAR_I = @MY_ARREARS_INT
			--	end
			--end
			
			--IF @OR_AMOUNT >= @ARREAR_P + @ARREAR_I + @ARREAR_OTH + @P_BAL + @I_BAL				-- JS 11/09/2013
			--BEGIN																					-- JS 11/09/2013
			--	set @PPDATE = @PROC_DATE
			--END																					-- JS 11/09/2013
			
			set @ARREAR_OTH = 0
			if @P_PAID > 0
			begin
				set @ARREAR_I = 0
			end
			else
			begin
				set @ARREAR_I = @AMORT_AMT - (@ARREAR_P - @P_PAID) + @I_PAID
				
				if @ARREAR_I < 0
				begin
					set @ARREAR_I = 0
				end
			end
			
			if @OR_AMOUNT >= @ARREAR_P + @ARREAR_I + @ARREAR_OTH
			begin
				set @PPDATE = @PROC_DATE
			end
			else
			begin
				set @ARREAR_OTH = @MY_ARREARS_OTH
				set @ARREAR_I = @MY_ARREARS_INT				
			end																											-- JS 01/16/2016.STOP
		end
		
		-- END OFFCYCLE
		
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
				set @ORMKS = @ORMKS1 + ' DEDUCTION-PEN'
				exec s3p_J_U_Ledger @PN_NO, @PPDATE, 'DED', @PN_NO, 'PAY', 'OTH', @A2PAY, @ORMKS, @SYSDATE, @MY_USER
				set @XAPEN = @A2PAY
				if (@ARREAR_OTH - @A2PAY) > 0 begin
					set @ARREAR_OTH = @ARREAR_OTH - @A2PAY --!
				end else begin
					set @ARREAR_OTH = 0 --!
				end
			end
		end
		
		--if @OR_AMOUNT < @ARREAR_I																					-- JS 01/16/2016
		--begin																										--		|
		--	set @ARREAR_OTH = @MY_ARREARS_OTH																		--		|
		--end																										-- JS 01/16/2016
		
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
				set @ORMKS = @ORMKS1 + ' DEDUCTION ARR-INT'
				exec s3p_J_U_Ledger @PN_NO, @PPDATE, 'DED', @PN_NO, 'PAY', 'INT', @A2PAY, @ORMKS, @SYSDATE, @MY_USER
				if (@ARREAR_I - @A2PAY) > 0 begin
					set @ARREAR_I = @MY_ARREARS_INT - @A2PAY --!
				end else begin
					set @ARREAR_I = 0 --!
					set @ARREAR_AS = @SYSDATE
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
				set @ORMKS = @ORMKS1 + ' DEDUCTION ARR-PRI'
				exec s3p_J_U_Ledger @PN_NO, @PPDATE, 'DED', @PN_NO, 'PAY', 'PRI', @A2PAY, @ORMKS, @SYSDATE, @MY_USER
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
				set @ORMKS = @ORMKS1 + ' DEDUCTION AMORT-INT'
				exec s3p_J_U_Ledger @PN_NO, @PPDATE, 'DED', @PN_NO, 'PAY', 'INT', @A2PAY, @ORMKS, @SYSDATE, @MY_USER
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
				set @ORMKS = @ORMKS1 + ' DEDUCTION AMORT-PRI'
				exec s3p_J_U_Ledger @PN_NO, @PPDATE, 'DED', @PN_NO, 'PAY', 'PRI', @A2PAY, @ORMKS, @SYSDATE, @MY_USER
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
					set @ORMKS = @ORMKS1 + ' DEDUCTION OUTS-BAL'
					exec s3p_J_U_Ledger @PN_NO, @PPDATE, 'DED', @PN_NO, 'PAY', 'PRI', @A2PAY, @ORMKS, @SYSDATE, @MY_USER
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
	end
	
	set	@ACCU_PAYP = @ACCU_PAYP + @XOPRI + @XAPRI
	set @YTD_I = @YTD_I + @XOINT + @XAINT
	set @CHG_DATE = @SYSDATE
	
	if @ACCU_PAYP >= @PRINCIPAL begin
		set @LOAN_STAT = 'F'
	end
	
	set @REC_STAT = 0
	set @OR_AMOUNT = ISNULL(@OR_AMOUNT,0) + ISNULL(@YBADV,0)
	
	update	#MO_DEDNO
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
			[PAYCODE] = @ORMKS1,
			[CODE5] = src.[CODE5], 
			[PD] = src.[PD]
	from	#MO_DEDNO tgt
			inner join (
				select	up.UPLOADO_ID,
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
						@JOEL tmp
							on tmp.TAYP = lon.LOAN_TYPE
							inner join
						dbo.UPLOADO up on
							up.EMP_NUM = mem.CB_EMPNO and		-- JS 06/02/2012
							up.ACCT_CODE = tmp.ACCT_CODE		-- JS 06/02/2012
				where	lon.LOANS_ID = @LOANS_ID
			) src on src.UPLOADO_ID = tgt.UPLOADO_ID
	
	insert	#MO_DEDNO (
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
			PAYCODE,
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
			@ORMKS1,
			case
				when @OR_AMOUNT > 0 THEN @OR_AMOUNT
				else ADVANCE
				end
	from	dbo.LOANS lon
				inner join
			dbo.MEMBERS mem
				on lon.KBCI_NO = mem.KBCI_NO
				inner join
			dbo.UPLOADO up
				on up.EMP_NUM = mem.CB_EMPNO
				left join
			@JOEL tmp
				on tmp.ACCT_CODE = up.ACCT_CODE
	where	lon.LOANS_ID = @LOANS_ID and
			up.UPLOADO_ID not in (select UPLOADO_ID from #MO_DEDNO)
	
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
					when @PD = 1 and @LOAN_STAT =  'F' then 'PD-' + @ORMKS1 + ' REFUND: FULLY PAID LOAN ' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  MM, @SYSDATE)), 2) + '/' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  DD, @SYSDATE)), 2) + '/' + RIGHT('0000' + CONVERT(VARCHAR(4), DATEPART(YYYY, @SYSDATE)), 4)
					when @PD = 1 and @LOAN_STAT != 'F' then 'PD-' + @ORMKS1 + ' PAYROLL POSTING ADVANCE ' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  MM, @SYSDATE)), 2) + '/' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  DD, @SYSDATE)), 2) + '/' + RIGHT('0000' + CONVERT(VARCHAR(4), DATEPART(YYYY, @SYSDATE)), 4)
					when @PD = 0 then @ORMKS1 + ' POSTING ADVANCE ' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  MM, @SYSDATE)), 2) + '/' + RIGHT(  '00' + CONVERT(VARCHAR(2), DATEPART(  DD, @SYSDATE)), 2) + '/' + RIGHT('0000' + CONVERT(VARCHAR(4), DATEPART(YYYY, @SYSDATE)), 4)
					end,
				@SYSDATE,
				@MY_USER,
				@OR_AMOUNT,
				@FEBTC_SA																		-- JS 01/26/2013
	end
	
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
	
	fetch	UPLOADO_CURSOR
	into	@LOANS_ID,
			@PN_NO,
			@KBCI_NO,
			@PRINCIPAL,
			@XBPRIN,
			@XLASTD,
			@I_PAID,
			@P_PAID,
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
			@PREV_FLG,
			@PLON_AMT,
			@AMORT_AMT,
			@FEBTC_SA																			-- JS 01/26/2013
			
	--exec s3p_Loans_LoansNew_RecomputeBalance @PN_NO												-- JS 01/18/2014
			
end

close UPLOADO_CURSOR
deallocate UPLOADO_CURSOR

insert	dbo.MO_DEDNO (
		[EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD],[PAYCODE]
		)
select	[EMPNO],[KBCI_NO],[NAME],[PN_NO],[LOAN_TYPE],[AMORT_PRI],[AMORT_INT],[DEDUCTION],
		[PRINCIPAL],[INTEREST],[ARREARS],[ADVANCE],[DATE],[USER],[ARR_PRI],[ARR_INT],[CODE5],[PD],[PAYCODE]
from	#MO_DEDNO


insert	@VOUCHER
exec	S3P_J_GEN_LAPP 'V'

insert	OTHHEDR (
		HEDRCODE,
		HEDRDATE,
		HEDRPROC,
		ADD_DATE,
		HEDRCVNO,
		HEDRS1,
		HEDRA1
		)
values	(
		@ORMKS1,
		@SYSDATE,
		@MY_USER,
		CONVERT(DATE, GETDATE()),
		(select top 1 XPN_NO from @VOUCHER),
		@SOD_TXT,
		@SOD_TOT
		)

GO