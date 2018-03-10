USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_Close]    Script Date: 07/15/2009 11:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Admin_Close]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Admin_Close]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_Close]    Script Date: 07/15/2009 11:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*****************************************************************************
MODIFIED:
JS 03/13/2013		DELETE TO TRUNCATE
JS 04/04/2013		REMOVED BEGBAL AND ENDBAL FROM FETCH
JS 06/15/2013		CORRECTED DAILYTRN NAME PREFIX
JS 08/08/2013		CORRECTED DAILYTRN LRI AMOUNT
JS 09/14/2013		CORRECTED DAILYTRN NAME
JS 10/04/2013		ADDED LRI ADJUSTMENT TO LRI AMOUNT
JS 04/05/2014		MODIFIED SCRIPT GETTING BEGBAL AND ENDBAL
*****************************************************************************/

CREATE PROCEDURE [dbo].[Do_Admin_Close]
@my_user varchar(8)
AS

declare @SYSDATE date
declare @dtint date

declare @LEDGER_ID as bigint
declare @ACCT_TYPE as varchar(3)
declare @ACCT_CODE as varchar(3)
declare @BEGBAL as numeric(13,4)
declare @DR as numeric(11,4)
declare @CR as numeric(11,4)
declare @ENDBAL as numeric(13,4)

declare @FD_ID bigint
declare @KBCI_NO varchar(7)
declare @AMOUNT numeric(11,4)
declare @DRCR varchar(2)

declare @xendbal as numeric(13,4)
declare @kbamt numeric(12,4)

declare @xterm numeric(3)

declare @xkbci_no varchar(7)

----------------- Declare temp tables

declare @DAILYTRN table
(
	PN_NO varchar(7)
)

create table #LOANS
(
	ACCU_PAYP numeric(12,4),
	ADD_DATE date,
	ADV_INTE numeric(2),
	ADVANCE numeric(12,4),
	AFT_INTE numeric(12,4),
	AMORT_AMT numeric(12,4),
	APP_DATE date,
	APP_NO numeric(5),
	ARREAR_AS date,
	ARREAR_I numeric(12,4),
	ARREAR_OTH numeric(12,4),
	ARREAR_P numeric(12,4),
	BY_WHOM varchar(15),
	CHG_DATE date,
	CHKNO varchar(15),
	CHKNO_ACK date,
	CHKNO_AMT numeric(12,4),
	CHKNO_BANK varchar(15),
	CHKNO_DATE date,
	CHKNO_RELS varchar(6),
	COLLATERAL varchar(55),
	DATE_DUE date,
	DATE_GRANT date,
	DED_BAL varchar(7),
	FREQ varchar(1),
	I_BAL numeric(12,4),
	KBCI_NO varchar(7),
	L_EXT bit,
	LED_TYPE numeric(1),
	LOAN_STAT varchar(1),
	LOAN_TYPE varchar(3),
	LOANS_ID bigint,
	LRI_DUE numeric(12,4),
	LRI_IND bit,
	MOD_PAY varchar(3),
	NDUE date,
	O_BAL numeric(12,4),
	P_BAL numeric(12,4),
	PAY_START date,
	PD bit,
	PN_NO varchar(7),
	PRINCIPAL numeric(12,4),
	RATE numeric(7,4),
	REC_STAT bit,
	RENEW bit,
	TERM numeric(3),
	[USER] varchar(8),
	YTD_I numeric(11,4),
	xterm numeric(3) default 0,
	dtdue date,
	xamort numeric(12,4) default 0,
	xax numeric(12,4) default 0,
	mylastd date,
	myprin numeric(12,4) default 0,
	myint numeric(12,4) default 0,
	amortint numeric(12,4) default 0,
	amortint1 numeric(12,4) default 0,
	xbprin numeric(12,4) default 0,
	xyint numeric(12,4) default 0,
	updated bit default 0
)

----------------- Get system date

select	@SYSDATE = DATEADD(D, 0, SYSDATE)
from	dbo.CTRL

----------------- Check for double entries in LOANS

insert	dbo.CBLOG (
		[DESC],
		[ACTIVITY],
		[DATE],
		KBCI_NO,
		PN_NO,
		[USER]
		)
select	'DOUBLE ENTRIES',
		'DOUBLE ENTRY IN LOAN',
		@SYSDATE,
		KBCI_NO,
		PN_NO,
		@MY_USER
from	dbo.LOANS
where	CHKNO_DATE = @SYSDATE and
		LOAN_STAT = 'F'

delete	dbo.LEDGER
where	PN_NO in (
		select	PN_NO
		from	dbo.LOANS
		where	CHKNO_DATE = @SYSDATE and
				LOAN_STAT = 'F'
		)

delete	dbo.LOANS 
where	CHKNO_DATE = @SYSDATE and
		LOAN_STAT = 'F'

-----------------  Populate work table

insert into #LOANS (
		ACCU_PAYP,
		ADD_DATE,
		ADV_INTE,
		ADVANCE,
		AFT_INTE,
		AMORT_AMT,
		APP_DATE,
		APP_NO,
		ARREAR_AS,
		ARREAR_I,
		ARREAR_OTH,
		ARREAR_P,
		BY_WHOM,
		CHG_DATE,
		CHKNO,
		CHKNO_ACK,
		CHKNO_AMT,
		CHKNO_BANK,
		CHKNO_DATE,
		CHKNO_RELS,
		COLLATERAL,
		DATE_DUE,
		DATE_GRANT,
		DED_BAL,
		FREQ,
		I_BAL,
		KBCI_NO,
		L_EXT,
		LED_TYPE,
		LOAN_STAT,
		LOAN_TYPE,
		LOANS_ID,
		LRI_DUE,
		LRI_IND,
		MOD_PAY,
		NDUE,
		O_BAL,
		P_BAL,
		PAY_START,
		PD,
		PN_NO,
		PRINCIPAL,
		RATE,
		REC_STAT,
		RENEW,
		TERM,
		[USER],
		YTD_I
		)
select	ACCU_PAYP,
		ADD_DATE,
		ADV_INTE,
		ADVANCE,
		AFT_INTE,
		AMORT_AMT,
		APP_DATE,
		APP_NO,
		ARREAR_AS,
		ARREAR_I,
		ARREAR_OTH,
		ARREAR_P,
		BY_WHOM,
		CHG_DATE,
		CHKNO,
		CHKNO_ACK,
		CHKNO_AMT,
		CHKNO_BANK,
		CHKNO_DATE,
		CHKNO_RELS,
		COLLATERAL,
		DATE_DUE,
		DATE_GRANT,
		DED_BAL,
		FREQ,
		I_BAL,
		KBCI_NO,
		L_EXT,
		LED_TYPE,
		LOAN_STAT,
		LOAN_TYPE,
		LOANS_ID,
		LRI_DUE,
		LRI_IND,
		MOD_PAY,
		NDUE,
		O_BAL,
		P_BAL,
		PAY_START,
		PD,
		PN_NO,
		PRINCIPAL,
		RATE,
		REC_STAT,
		RENEW,
		TERM,
		[USER],
		YTD_I
from	dbo.LOANS
where	LOAN_STAT not in ('F','T')

-----------------  Correcting due date

update	#LOANS
set		xterm = 30,
		dtdue = DATEADD(D, 30, CHKNO_DATE),
		updated = 1
where	MOD_PAY not like '%1%' and 
		FREQ = 'D'

update	#LOANS
set		dtdue = case
			when DATENAME(W, dtdue) = 'Sunday'   then DATEADD(D, 1, dtdue)
			when DATENAME(W, dtdue) = 'Saturday' then DATEADD(D, 2, dtdue)
			else dtdue
			end,
		xterm = case
			when DATENAME(W, dtdue) = 'Sunday'   then 31
			when DATENAME(W, dtdue) = 'Saturday' then 32
			else xterm
			end,
		updated = 1
where	MOD_PAY not like '%1%' and 
		FREQ = 'D'

update	#LOANS
set		xamort = PRINCIPAL + ((PRINCIPAL * RATE * xterm) / 36000),
		updated = 1
where	MOD_PAY not like '%1%' and 
		FREQ = 'D'

update	#LOANS
set		AMORT_AMT = xamort,
		updated = 1
where	MOD_PAY not like '%1%' and 
		FREQ = 'D' and
		AMORT_AMT != xamort

update	#LOANS
set		TERM = xterm,
		updated = 1
where	MOD_PAY not like '%1%' and 
		FREQ = 'D' and
		TERM != xterm

update	#LOANS
set		dtdue = dbo.func_GoDue(PAY_START, TERM, FREQ),
		updated = 1
where	MOD_PAY not like '%1%' and 
		FREQ != 'D'

update	#LOANS
set		DATE_DUE = dtdue,
		updated = 1
where	MOD_PAY not like '%1%' and 
		DATE_DUE != dtdue

update	#LOANS
set		ARREAR_AS = case
			when @SYSDATE > DATE_DUE then DATEADD(D, 1, DATE_DUE)
			when @SYSDATE = DATE_DUE then null
			else ARREAR_AS
			end,
		updated = 1
where	MOD_PAY not like '%1%' and 
		DATE_DUE != dtdue and
		ARREAR_AS is not null and
		LOAN_STAT = 'R' and
		TERM = 1 and
		FREQ = 'M'

update	#LOANS
set		NDUE = case 
			when NDUE < DATE_DUE then DATE_DUE
			when DATE_DUE > @SYSDATE and NDUE != DATE_DUE then DATE_DUE
			else NDUE
			end,
		PAY_START = case
			when PAY_START != DATE_DUE then DATE_DUE
			else PAY_START
			end,
		updated = 1
where	MOD_PAY not like '%1%' and 
		FREQ = 'D'

update	#LOANS
set		NDUE = case
			when NDUE < DATE_DUE then DATE_DUE
			else NDUE
			end,
		PAY_START = case
			when PAY_START != DATE_DUE then DATE_DUE
			else PAY_START
			end,
		xax = PRINCIPAL + ((PRINCIPAL * RATE * DATEDIFF(D, CHKNO_DATE, DATE_DUE)) / 36000),
		updated = 1
where	MOD_PAY not like '%1%' and
		LOAN_STAT = 'R' and
		TERM = 1 and
		FREQ = 'M'

update	#LOANS
set		AMORT_AMT = ROUND(xax, 2),
		updated = 1
where	MOD_PAY not like '%1%' and
		LOAN_STAT = 'R' and
		TERM = 1 and
		FREQ = 'M' and
		AMORT_AMT < xax

update	#LOANS
set		CHKNO_AMT = src.AMT,
		updated = 1
from	#LOANS tgt
		inner join (
			select	PN_NO,
					sum(DR - CR) as AMT
			from	dbo.LEDGER
			where	RMK like '%INIT%'
			group
			by		PN_NO
			)
			as src
			on src.PN_NO = tgt.PN_NO and src.AMT != tgt.CHKNO_AMT

-----------------  Recompute Arrear_i for 1 month term (current loan)

update	#LOANS
set		mylastd = CHKNO_DATE,
		myprin = PRINCIPAL,
		myint = 0,
		amortint = 0,
		updated = 1
where	LOAN_STAT = 'R' and
		TERM = 1 and
		FREQ = 'M'

update	#LOANS
set		myprin = myprin + src.AMT,
		mylastd = src.DT,
		updated = 1
from	#LOANS tgt
		inner join (
			select	PN_NO,
					sum(DR - CR) as AMT,
					MAX([DATE]) as DT
			from	dbo.LEDGER
			where	ACCT_CODE = 'PRI' and
					ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP')
			group
			by		PN_NO
			)
			as src
			on src.PN_NO = tgt.PN_NO
where	LOAN_STAT = 'R' and
		TERM = 1 and
		FREQ = 'M'

update	#LOANS
set		amortint = (PRINCIPAL * RATE * DATEDIFF(D, CHKNO_DATE, DATE_DUE)) / 36000,
		amortint1 = (myprin * RATE * DATEDIFF(D, mylastd, DATE_DUE)) / 36000,
		ARREAR_P = myprin,
		updated = 1
where	LOAN_STAT = 'R' and
		TERM = 1 and
		FREQ = 'M' and
		DATEADD(D, -1, ARREAR_AS) = PAY_START

update	#LOANS
set		ARREAR_I = case
			when ARREAR_I != amortint1 then amortint1
			else ARREAR_I
			end,
		AMORT_AMT = case
			when AMORT_AMT != (PRINCIPAL + amortint) then PRINCIPAL + amortint
			else AMORT_AMT
			end,
		updated = 1
where	LOAN_STAT = 'R' and
		TERM = 1 and
		FREQ = 'M' and
		DATEADD(D, -1, ARREAR_AS) = PAY_START

-----------------  Recompute Arrear_i for 30 days term (current loan)

update	#LOANS
set		mylastd = CHKNO_DATE,
		myprin = PRINCIPAL,
		myint = 0,
		amortint = 0,
		updated = 1
where	LOAN_STAT = 'R' and
		FREQ = 'D'

update	#LOANS
set		myprin = myprin + src.AMT,
		mylastd = src.DT,
		updated = 1
from	#LOANS tgt
		inner join (
			select	PN_NO,
					sum(DR - CR) as AMT,
					MAX([DATE]) as DT
			from	dbo.LEDGER
			where	ACCT_CODE = 'PRI' and
					ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP')
			group
			by		PN_NO
			)
			as src
			on src.PN_NO = tgt.PN_NO
where	LOAN_STAT = 'R' and
		FREQ = 'D'

update	#LOANS
set		amortint = (PRINCIPAL * RATE * DATEDIFF(D, CHKNO_DATE, DATE_DUE)) / 36000,
		amortint1 = (myprin * RATE * DATEDIFF(D, mylastd, DATE_DUE)) / 36000,
		ARREAR_P = myprin,
		updated = 1
where	LOAN_STAT = 'R' and
		FREQ = 'D' and
		DATEADD(D, -1, ARREAR_AS) = PAY_START

update	#LOANS
set		ARREAR_I = case
			when ARREAR_I != amortint1 then amortint1
			else ARREAR_I
			end,
		AMORT_AMT = case
			when AMORT_AMT != (PRINCIPAL + amortint) then PRINCIPAL + amortint
			else AMORT_AMT
			end,
		updated = 1
where	LOAN_STAT = 'R' and
		FREQ = 'D' and
		DATEADD(D, -1, ARREAR_AS) = PAY_START
		
-----------------  Update Control File

update	dbo.CTRL
set		[CLOSE] = 1,
		[REP1] = 0,
		[REP2] = 0,
		[REP3] = 0,
		[REP4] = 0,
		[REP5] = 0,
		[TD_REP] = 0

-----------------  Update Ledger File

declare LEDGER_CURSOR cursor for
select	led.LEDGER_ID,
		led.ACCT_TYPE,
		led.ACCT_CODE,
		led.DR,
		led.CR
from	dbo.LOANS lon
			inner join
		dbo.LEDGER led on
			led.PN_NO = lon.PN_NO
where	lon.LOAN_STAT not in ('F', 'T') and
		led.PN_NO is not null and
		led.[DATE] <= @SYSDATE
order
by		led.PN_NO, led.[DATE], led.LEDGER_ID

open LEDGER_CURSOR

fetch	LEDGER_CURSOR
into	@LEDGER_ID,
		@ACCT_TYPE,
		@ACCT_CODE,
		@DR,
		@CR
		
while @@FETCH_STATUS = 0
begin

	if @ACCT_CODE = 'PRI' and @ACCT_TYPE = 'AMT' begin
		set @BEGBAL = 0
		set @ENDBAL = @DR - @CR
	end
	else if @ACCT_CODE = 'PRI' and @ACCT_TYPE in ('PAY', 'ADJ', 'TER') begin
		set @BEGBAL = @xendbal
		set @ENDBAL = @BEGBAL + @DR - @CR
	end
	else begin
		set @BEGBAL = @xendbal
		set @ENDBAL = @xendbal
	end

	set @xendbal = @ENDBAL
	
	update	dbo.LEDGER
	set		BEGBAL = @BEGBAL,
			ENDBAL = @ENDBAL
	where	LEDGER_ID = @LEDGER_ID
	
	fetch	LEDGER_CURSOR
	into	@LEDGER_ID,
			@ACCT_TYPE,
			@ACCT_CODE,
			@DR,
			@CR

end

close LEDGER_CURSOR
deallocate LEDGER_CURSOR

-----------------  Recomputing Accumulated Payments

update	#LOANS
set		xbprin = src.xbprin,
		xyint = src.xyint,
		ACCU_PAYP = PRINCIPAL - src.xbprin,
		YTD_I = src.xyint,
		updated = 1
from	#LOANS tgt
		inner join (
			select	PN_NO,
					sum(case
						when ACCT_CODE = 'PRI' and ACCT_TYPE != 'INI' then DR - CR
						else 0
						end) as xbprin,
					sum(case
						when ACCT_CODE = 'INT' and ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'INT') then CR -DR
						else 0
						end) as xyint
			from	dbo.LEDGER
			group
			by		PN_NO
			)
			as src
			on src.PN_NO = tgt.PN_NO

-----------------  Recomputing Balances - Fixed Deposit

set @xkbci_no = REPLICATE(' ', 7)
set @kbamt = 0

declare FD_CURSOR cursor for
select	FD_ID,
		KBCI_NO,
		DRCR,
		AMOUNT
from	dbo.FD
where	KBCI_NO IN (
		select	KBCI_NO
		from	dbo.MEMBERS
		where	MEM_STAT != 'S'
		)
order
by		KBCI_NO, [DATE], FD_ID

open FD_CURSOR

fetch	FD_CURSOR
into	@FD_ID,
		@KBCI_NO,
		@DRCR,
		@AMOUNT

while @@FETCH_STATUS = 0
begin
	if @DRCR = 'CR' begin
		set @kbamt = @kbamt + @AMOUNT
	end
	else begin
		set @kbamt = @kbamt - @AMOUNT
	end

	update	dbo.FD
	set		BALANCE = @kbamt
	where	FD_ID = @FD_ID
	
	set @xkbci_no = @KBCI_NO
	
	fetch	FD_CURSOR
	into	@FD_ID,
			@KBCI_NO,
			@DRCR,
			@AMOUNT
		
	if @xkbci_no != @KBCI_NO begin
		update	dbo.MEMBERS
		set		FD_AMOUNT = @kbamt,
				MEM_STAT = case
					when @kbamt <= 0 then 'R'
					else MEM_STAT
					end
		where	KBCI_NO = @xkbci_no
		
		set @kbamt = 0
	end
end

close FD_CURSOR
deallocate FD_CURSOR

-----------------  Extracting transaction balances for the day

-- if acct_code='PRI' and acct_type='AMT'
--    toloy = .T.
-- endi

-- if xxbal<0
--    toloy=.F.
-- endi   

-- if loan_stat='F'
--    toloy=.F.
-- endi   

-- PD = 0 for RUNUP1
-- PD = 1 for RRUNUP1

set @dtint = convert(date, convert(varchar(4), YEAR(@SYSDATE) - 1) + '-12-31')

update	dbo.RUNUP1
set		FLN_AMNT = isnull(src.xxbal, 0),
		FINT_AMNT = isnull(src.xxbali, 0),
		[DATE] = @SYSDATE
from	dbo.RUNUP1
			as tgt
		left join (
			select	lon.LOAN_TYPE,
					sum(case
						when led.[DATE] <= @SYSDATE and ACCT_CODE = 'PRI' and ACCT_TYPE in ('AMT', 'PAY', 'ADJ', 'TER') then led.DR - led.CR
						else 0
						end) as xxbal,
					sum(case
						when led.[DATE] > @dtint and led.[DATE] <= @SYSDATE and ACCT_CODE = 'INT' and ACCT_TYPE != 'INI' then led.CR - led.DR
						else 0
						end) as xxbali
			from	dbo.LOANS
						as lon
					inner join dbo.LEDGER
						as led
						on led.PN_NO = lon.PN_NO
					inner join (
						select	distinct PN_NO
						from	dbo.LEDGER
						where	ACCT_CODE = 'PRI' and ACCT_TYPE = 'AMT'
						)
						as toloy
						on toloy.PN_NO = lon.PN_NO
			where	lon.LOAN_STAT = 'R' and
					lon.LOAN_STAT != 'F' and
					lon.PD = 0
			group
			by		lon.LOAN_TYPE
			)
			as src
			on src.LOAN_TYPE = tgt.LOAN_TYP and src.xxbal >= 0 -- if xxbal<0 (negate)

update	dbo.RRUNUP1
set		FLN_AMNT = isnull(src.xxbal, 0),
		FINT_AMNT = isnull(src.xxbali, 0),
		[DATE] = @SYSDATE
from	dbo.RRUNUP1
			as tgt
		left join (
			select	lon.LOAN_TYPE,
					sum(case
						when led.[DATE] <= @SYSDATE and ACCT_CODE = 'PRI' and ACCT_TYPE in ('AMT', 'PAY', 'ADJ', 'TER') then led.DR - led.CR
						else 0
						end) as xxbal,
					sum(case
						when led.[DATE] > @dtint and led.[DATE] <= @SYSDATE and ACCT_CODE = 'INT' and ACCT_TYPE != 'INI' then led.CR - led.DR
						else 0
						end) as xxbali
			from	dbo.LOANS
						as lon
					inner join dbo.LEDGER
						as led
						on led.PN_NO = lon.PN_NO
					inner join (
						select	distinct PN_NO
						from	dbo.LEDGER
						where	ACCT_CODE = 'PRI' and ACCT_TYPE = 'AMT'
						)
						as toloy
						on toloy.PN_NO = lon.PN_NO
			where	lon.LOAN_STAT = 'R' and
					lon.LOAN_STAT != 'F' and
					lon.PD = 1
			group
			by		lon.LOAN_TYPE
			)
			as src
			on src.LOAN_TYPE = tgt.LOAN_TYP and src.xxbal >= 0 -- if xxbal<0 (negate)

-----------------  Extracting transactions for the day

set @dtint = convert(date, convert(varchar(4), YEAR(@SYSDATE) - 1) + '-12-31')

--delete	dbo.DAILYTRN										-- JS 03/16/2013
truncate table dbo.DAILYTRN										-- JS 03/16/2013

insert	@DAILYTRN
(
	PN_NO
)
select
	distinct
	PN_NO
from
	dbo.LEDGER
where
	ADD_DATE = @SYSDATE

insert	dbo.DAILYTRN(
		PN_NO,
		KBCI_NO,
		LOAN_TYPE,
		NAME,
		BEGBAL,
		ENDBAL,
		CR,
		DR,
		[INT],
		INTCR,
		INTDR,
		SC,
		LRI,
		FD,
		SD,
		PEN,
		[USER]
		)
select	lon.PN_NO,
		mem.KBCI_NO,
		lon.LOAN_TYPE,
		case
			when ISNULL(lon.PD, 0) = 0 then '1'								-- JS 06/15/2013
			when ISNULL(lon.PD, 0) = 1 then '2'								-- JS 06/15/2013
			else '1'
			end + ISNULL(LTRIM(RTRIM(mem.LNAME)) + ', ', '') + ISNULL(LTRIM(RTRIM(mem.FNAME)), '') + ISNULL(' ' + LTRIM(RTRIM(mem.MI)), '') as name,			-- JS 09/14/2013
			--end + ISNULL(LTRIM(RTRIM(mem.LNAME)) + ', ', '') + ISNULL(LTRIM(RTRIM(mem.FNAME)) + ' ', '') + ISNULL(LTRIM(RTRIM(mem.MI)), '') as name,			-- JS 09/14/2013
		B1.BEGBAL,
		B1.ENDBAL,
		B1.CR,
		B1.DR,
		B1.[INT],
		B1.INTCR,
		B1.INTDR,
		B1.SC,
		B1.LRI,
		B1.FD,
		B1.SD,
		B1.PEN,
		B1.[USER]
from	dbo.LOANS
			as lon
		inner join dbo.MEMBERS
			as mem
			on mem.KBCI_NO = lon.KBCI_NO
		inner join (
			select	led.PN_NO,
					--SUM(case																																	-- JS 04/05/2014
					--	when led.LEDGER_ID = ISNULL(A1.LEDGER_ID_MIN, 0) then led.BEGBAL																		--		|
					--	else 0																																	--		|
					--	end) AS BEGBAL,																															--		|
					--SUM(case																																	--		|
					--	when led.LEDGER_ID = ISNULL(A1.LEDGER_ID_MAX, 0) then led.ENDBAL																		--		|
					--	else 0																																	--		|
					--	end) AS ENDBAL,																															-- JS 04/05/2014
					sum(case
						when led.[ADD_DATE] != @SYSDATE and led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') and led.ACCT_CODE = 'PRI' then led.DR - led.CR			-- JS 04/05/2014
						else 0
						end) as BEGBAL,
					sum(case
						when led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') and led.ACCT_CODE = 'PRI' then led.DR - led.CR										-- JS 04/05/2014
						else 0
						end) as ENDBAL,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') and led.ACCT_CODE = 'PRI' then led.CR						-- JS 04/05/2014
						else 0
						end) AS CR,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and led.ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') and led.ACCT_CODE = 'PRI' then led.DR						-- JS 04/05/2014
						else 0
						end) AS DR,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE != 'INI' and ACCT_CODE = 'INT' then led.CR - led.DR											-- JS 04/05/2014
						else 0
						end) AS [INT],
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE != 'INI' and ACCT_CODE = 'INT' then led.CR														-- JS 04/05/2014
						else 0
						end) AS INTCR,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE != 'INI' and ACCT_CODE = 'INT' then led.DR														-- JS 04/05/2014
						else 0
						end) AS INTDR,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE = 'SC' then led.CR - led.DR																	-- JS 04/05/2014
						else 0
						end) AS SC,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE = 'LRI' and (RMK like '%INIT-LRI-%' and SUBSTRING(RMK, 10, 7) != led.PN_NO) then 0				-- JS 04/05/2014
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE = 'LRI' then led.CR - led.DR																	-- JS 04/05/2014
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE IN ('INI', 'PAY', 'ADJ') and ACCT_CODE = 'LRI' then led.CR - led.DR							-- JS 08/08/2013	-- JS 10/05/2013	-- JS 04/05/2014
						else 0
						end) AS LRI,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE = 'FD' then led.CR - led.DR																	-- JS 04/05/2014
						else 0
						end) AS FD,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and ACCT_TYPE = 'SD' then led.CR - led.DR																	-- JS 04/05/2014
						else 0
						end) AS SD,
					SUM(case
						when led.[ADD_DATE] = @SYSDATE and led.ACCT_TYPE = 'PAY' and led.ACCT_CODE = 'OTH' then led.CR - led.DR										-- JS 04/05/2014
						else 0
						end) AS PEN,
					MAX(led.[USER]) AS [USER]													-- JS 03/16/2013
					--MAX(case																	--		|
					--	when led.LEDGER_ID = A1.LEDGER_ID_MAX then led.[USER]					--		|
					--	else null																--		|
					--	end) AS [USER]															-- JS 03/16/2013
			from	dbo.LEDGER
						as led
						inner join
					@DAILYTRN d
						on d.PN_NO = led.PN_NO
					----inner join (																-- JS 03/16/2013
					--left join (																	-- JS 03/16/2013
					--	select	PN_NO,
					--			MIN(case
					--				when BEGBAL > 0 then LEDGER_ID
					--				end) AS LEDGER_ID_MIN,
					--			MAX(LEDGER_ID) AS LEDGER_ID_MAX
					--	from	dbo.LEDGER
					--	where	ADD_DATE = @SYSDATE and
					--			ACCT_TYPE in ('PAY', 'ADJ', 'AMT', 'TER') and 
					--			ACCT_CODE = 'PRI'
					--	group
					--	by		PN_NO
					--	)
					--	as A1
					--	on A1.PN_NO = led.PN_NO
			group
			by		led.PN_NO
			)
			as B1
			on B1.PN_NO = lon.PN_NO
order
by		lon.LOAN_TYPE, name

-----------------  apply changes to loans

update	dbo.LOANS
set		ACCU_PAYP = src.ACCU_PAYP,
		ADD_DATE = src.ADD_DATE,
		ADV_INTE = src.ADV_INTE,
		ADVANCE = src.ADVANCE,
		AFT_INTE = src.AFT_INTE,
		AMORT_AMT = src.AMORT_AMT,
		APP_DATE = src.APP_DATE,
		APP_NO = src.APP_NO,
		ARREAR_AS = src.ARREAR_AS,
		ARREAR_I = src.ARREAR_I,
		ARREAR_OTH = src.ARREAR_OTH,
		ARREAR_P = src.ARREAR_P,
		BY_WHOM = src.BY_WHOM,
		CHG_DATE = src.CHG_DATE,
		CHKNO = src.CHKNO,
		CHKNO_ACK = src.CHKNO_ACK,
		CHKNO_AMT = src.CHKNO_AMT,
		CHKNO_BANK = src.CHKNO_BANK,
		CHKNO_DATE = src.CHKNO_DATE,
		CHKNO_RELS = src.CHKNO_RELS,
		COLLATERAL = src.COLLATERAL,
		DATE_DUE = src.DATE_DUE,
		DATE_GRANT = src.DATE_GRANT,
		DED_BAL = src.DED_BAL,
		FREQ = src.FREQ,
		I_BAL = src.I_BAL,
		KBCI_NO = src.KBCI_NO,
		L_EXT = src.L_EXT,
		LED_TYPE = src.LED_TYPE,
		LOAN_STAT = src.LOAN_STAT,
		LOAN_TYPE = src.LOAN_TYPE,
		LRI_DUE = src.LRI_DUE,
		LRI_IND = src.LRI_IND,
		MOD_PAY = src.MOD_PAY,
		NDUE = src.NDUE,
		O_BAL = src.O_BAL,
		P_BAL = src.P_BAL,
		PAY_START = src.PAY_START,
		PD = src.PD,
		PN_NO = src.PN_NO,
		PRINCIPAL = src.PRINCIPAL,
		RATE = src.RATE,
		REC_STAT = src.REC_STAT,
		RENEW = src.RENEW,
		TERM = src.TERM,
		[USER] = src.[USER],
		YTD_I = src.YTD_I
from	dbo.LOANS
			as tgt
		inner join #LOANS
			as src
			on src.LOANS_ID = tgt.LOANS_ID and src.updated = 1



GO