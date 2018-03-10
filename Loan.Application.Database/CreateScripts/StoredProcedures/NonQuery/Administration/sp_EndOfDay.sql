USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_EndOfDay]    Script Date: 01/16/2016 16:31:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Do_Admin_EndOfDay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Do_Admin_EndOfDay]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Do_Admin_EndOfDay]    Script Date: 01/16/2016 16:31:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*****************************************************************************
MODIFIED:
JS 08/04/2012		DATE TO NUMBER CONVERSION ERROR
					ARREAR_P CORRECTION
JS 11/30/2012		PENALTY RECOMPUTATION
JS 04/24/2013		RECOUNT XBPRIN
JS 08/31/3013		NEW LRI ALGORITHM
JS 11/09/2013		NEW PENALTY FORMULA
JS 11/15/2014		EXCLUDE LOANS WITH OUTSTANDING <= 1
JS 03/14/2015		ADDED Process_EndOfDay_Arrears S PROC
JS 01/16/2016		ARREAR new condition
*****************************************************************************/

CREATE PROCEDURE [dbo].[Do_Admin_EndOfDay]
@my_user varchar(8)
AS

/* DECLARE VARIABLES */

declare @SYSDATE date
declare @pmonth date
declare @sdate date

declare @svyear as varchar(4)

declare @proc_date as date
declare @pdate date
declare @lridate date

/* DECLARE TEMP TABLES */

create table #LOANS
(
	[LOANS_ID] bigint PRIMARY KEY NOT NULL,
	[PN_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS UNIQUE NOT NULL,
	[KBCI_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[APP_DATE] date,
	[APP_NO] numeric(5),
	[DATE_GRANT] date,
	[BY_WHOM] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[DATE_DUE] date,
	[CHKNO_BANK] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[CHKNO] varchar(15) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[CHKNO_AMT] numeric(12, 4) DEFAULT 0,
	[CHKNO_DATE] date,
	[CHKNO_RELS] varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[CHKNO_ACK] date,
	[MOD_PAY] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[AMORT_AMT] numeric(12, 4) DEFAULT 0,
	[PAY_START] date,
	[RATE] numeric(7, 4) DEFAULT 0,
	[TERM] numeric(3) DEFAULT 0,
	[FREQ] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[PRINCIPAL] numeric(12, 4) DEFAULT 0,
	[LED_TYPE] numeric(1),
	[ADV_INTE] numeric(2) DEFAULT 0,
	[AFT_INTE] numeric(10, 2) DEFAULT 0,
	[ACCU_PAYP] numeric(12, 4) DEFAULT 0,
	[YTD_I] numeric(9, 2) DEFAULT 0,
	[LOAN_TYPE] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[LOAN_STAT] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[ARREAR_I] numeric(12, 4) DEFAULT 0,
	[ARREAR_P] numeric(12, 4) DEFAULT 0,
	[ARREAR_OTH] numeric(12, 4) DEFAULT 0,
	[ARREAR_AS] date,
	[COLLATERAL] varchar(55) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[DED_BAL] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[ADD_DATE] date,
	[CHG_DATE] date,
	[USER] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[P_BAL] numeric(12, 4) DEFAULT 0,
	[I_BAL] numeric(12, 4) DEFAULT 0,
	[O_BAL] numeric(12, 4) DEFAULT 0,
	[REC_STAT] bit,
	[RENEW] bit,
	[ADVANCE] numeric(12, 4) DEFAULT 0,
	[LRI_IND] bit,
	[NDUE] date,
	[L_EXT] bit,
	[PD] bit,
	[LRI_DUE] numeric(12, 4) DEFAULT 0,
	updated bit default 0,
	xdate date,
	xfreq int default 0,
	xgomo int default 0,
	xbprin numeric(12, 4) default 0,
	xbprin2 numeric(12, 4) default 0,										-- JS 09/27/2014
	xpdate date,
	xpint numeric(12, 4) default 0,
	xpoth numeric(12, 4) default 0,
	asked bit default 0,
	mayb bit default 0,
	xpri numeric(12, 4) default 0,
	xint numeric(12, 4) default 0,
	xarro numeric(12, 4) default 0,
	xbalo numeric(12, 4) default 0,
	myarsp bit default 0,
	xybal numeric(12, 4) default 0,
	xydue date,
	xydate date,
	xyarsp numeric(12, 4) default 0,
	xlastd date,
	axfreq int default 0,
	axgomo int default 0,
	trace varchar(127) default '',
	amort varchar(100)
)

/* POPULATE LOANS TABLE */

insert into #LOANS
(
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
	YTD_I,
	updated
)
select
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
	YTD_I,
	0
from
	dbo.LOANS
where
	LOAN_STAT not in ('F','T')

/* Exclude Loans With Oustanding Balance <= 1  */

update	#LOANS																-- JS 11/15/2014
set		xbprin2 = tgt.PRINCIPAL + src.xbprin								--		|
from	#LOANS																--		|
			as tgt															--		|
		inner join (														--		|
			select	lon.PN_NO,												--		|
					sum														--		|
					(														--		|
						case												--		|
						when												--		|
							led.ACCT_CODE = 'PRI' and						--		|
							led.ACCT_TYPE in ('PAY', 'ADJ')					--		|
						then												--		|
							led.DR - led.CR									--		|
						else												--		|
							0												--		|
						end													--		|
					) as xbprin												--		|
			from	#LOANS													--		|
						as lon												--		|
					inner join dbo.LEDGER									--		|
						as led												--		|
						on led.PN_NO = lon.PN_NO							--		|
			group															--		|
			by		lon.PN_NO												--		|
			)																--		|
			as src															--		|
			on src.PN_NO = tgt.PN_NO										--		|
																			--		|
delete #LOANS																--		|
where xbprin2 <= 1															-- JS 11/15/2014

/* READ CTRL */

select
	top 1
	@SYSDATE = SYSDATE,
	@pdate = SYSDATE,		
	@proc_date = [PROC],
	@svyear = DATENAME(YYYY, SYSDATE)
from
	dbo.CTRL

/* UPDATE CONTROL FILE */

update
	dbo.CTRL
set	
	CHG_DATE = @SYSDATE,
	SYSDATE = DATEADD(D, 1, @SYSDATE)

update
	dbo.CTRL
set	
	[USER] = @my_user,
	LAPP_DATE = SYSDATE,
	REP1 = 0,
	REP2 = 0,
	REP3 = 0,
	REP4 = 0,
	REP5 = 0,
	[CLOSE] =0,
	TD_REP = 0,
	FD_REP = 0,
	L_DM = 
	(
		convert(varchar(2), DATEPART(MM, SYSDATE)) + 
		right('0' + convert(varchar(2), DATEPART(DD, SYSDATE)), 2) + 
		right('0' + convert(varchar(4), DATEPART(YY, SYSDATE)), 2) + 
		'0000'
	),
	L_CM = 
	(
		convert(varchar(2), DATEPART(MM, SYSDATE)) + 
		right('0' + convert(varchar(2), DATEPART(DD, SYSDATE)), 2) + 
		right('0' + convert(varchar(4), DATEPART(YY, SYSDATE)), 2) + 
		'0000'
	),
	MAPP_NO = 
	(
		right('0' + convert(varchar(2), DATEPART(MM, SYSDATE)), 2) + 
		right('0' + convert(varchar(2), DATEPART(DD, SYSDATE)), 2) + 
		'000'
	),
	KBCI_NO = 
	(
		case
			when DATENAME(YYYY, SYSDATE) > @svyear then right('0' + convert(varchar(4), DATEPART(YY, SYSDATE)), 2) + '00000'
			else right('0' + convert(varchar(4), DATEPART(YY, SYSDATE)), 2) + RIGHT(KBCI_NO, 5)
			end
	),
	LAPP_NO = 
	(
		case 
			when LAPP_NO = 99999 then 0
			when DATEPART(YYYY, SYSDATE) > @svyear then 0
			else LAPP_NO 
			end
	)

select
	top 1
	@SYSDATE = SYSDATE
from
	dbo.CTRL

/* CHECK CONTENTS OF PAST DUE LOANS DBF */

set	@pmonth = DATEADD(M, -1, @SYSDATE)

--update	#LOANS
--set		ARREAR_P = 0,
--		ARREAR_I = 0,
--		ARREAR_OTH = 0,
--		P_BAL = 0,
--		I_BAL = 0,
--		O_BAL = 0,
--		ARREAR_AS = null,
--		updated = 1, trace = trace + ',' + 'A'
--where	ARREAR_AS = @pmonth

/* FIX NDUE FIELD IN LOANS */

set	@sdate = @SYSDATE

update
	#LOANS
set
	NDUE = PAY_START,
	updated = 1, 
	trace = trace + ',' + 'B'
where
	LOAN_STAT not in ('F', 'T') and
	PAY_START = @sdate and
	isnull(NDUE, '1900-1-1') != PAY_START

update
	#LOANS
set
	xdate = PAY_START,
	xfreq = 
	(
		case
			when FREQ = 'M' then 12
			when FREQ = 'S' then 6
			when FREQ = 'Q' then 4
			when FREQ = 'A' then 1
			else 1
			end
	),
	xgomo = 
	(
		case
			when FREQ = 'M' then 1
			when FREQ = 'S' then 6
			when FREQ = 'Q' then 3
			when FREQ = 'A' then 12
			else 0
			end
	),
	updated = 1, 
	trace = trace + ',' + 'C'
where
	LOAN_STAT not in ('F', 'T') and
	PAY_START < @sdate

--IF
	update	#LOANS
	set		xdate = DATEADD(D, 
				ceiling(convert(numeric(12,4), DATEDIFF(D, xdate, @sdate)) / 30) * 30, 
				xdate),
			updated = 1, trace = trace + ',' + 'D'
	where	LOAN_STAT not in ('F', 'T') and
			PAY_START < @sdate and
			FREQ = 'D'
--ELSE
	update	#LOANS
	set		xdate = DATEADD(M, 
				ceiling(convert(numeric(12,4), DATEDIFF(M, xdate, @sdate)) / xgomo) * xgomo, 
				xdate),
			updated = 1, trace = trace + ',' + 'E'
	where	LOAN_STAT not in ('F', 'T') and
			PAY_START < @sdate and
			FREQ != 'D' and
			RIGHT(trace, 2) not in (',D')

	update	#LOANS
	set		xdate = DATEADD(M, xgomo, xdate),
			updated = 1, trace = trace + ',' + 'F'
	where	LOAN_STAT not in ('F', 'T') and
			PAY_START < @sdate and
			FREQ != 'D' and
			xdate < @sdate and
			RIGHT(trace, 2) not in (',D')
--ENDI
			
update	#LOANS
set		xdate = case
			when DATENAME(W, xdate) = 'Sunday'   then DATEADD(D, 1, xdate)
			when DATENAME(W, xdate) = 'Saturday' then DATEADD(D, 2, xdate)
			else xdate
			end,
		updated = 1, trace = trace + ',' + 'G'
where	LOAN_STAT not in ('F', 'T') and
		PAY_START < @sdate and
		FREQ = 'D'
		
update	#LOANS
set		NDUE = xdate,
		updated = 1, trace = trace + ',' + 'H'
where	LOAN_STAT not in ('F', 'T') and
		PAY_START < @sdate and
		NDUE != xdate

-----------------  fix NDUE field in LOANS end
-----------------  Apply Amortization Due

update	#LOANS
set		ARREAR_AS = null,
		ARREAR_P = 0,
		ARREAR_I = 0,
		ARREAR_OTH = 0,
		updated = 1, trace = trace + ',' + 'I'
where	LOAN_STAT in ('F', 'T') and
		(
			ARREAR_P > 0 or
			ARREAR_I > 0 or
			ARREAR_OTH > 0 or
			ARREAR_AS is not null
		)

update	#LOANS
set		ARREAR_AS = null,
		updated = 1, trace = trace + ',' + 'J'
where	LOAN_STAT not in ('F', 'T') and
		ARREAR_P <= 0 and
		ARREAR_I <= 0 and
		ARREAR_OTH <= 0

-----------------  asked
/*
where	LOAN_STAT not in ('F', 'T') and
		NDUE = @SYSDATE and
		NDUE <= DATE_DUE
*/

--IF
	update	#LOANS
	set		asked = 1,
			updated = 1, trace = trace + ',' + 'K'
	where	LOAN_STAT not in ('F', 'T') and
			NDUE = @SYSDATE and
			NDUE <= DATE_DUE

	update	#LOANS
	set		xbprin = tgt.PRINCIPAL + src.xbprin,
			xpdate = case
				when src.xpdate is null then tgt.PAY_START
				else src.xpdate
				end,
			xpint = tgt.xpint + src.xpint,
			xpoth = tgt.xpoth + src.xpoth,
			updated = 1, trace = trace + ',' + 'L'
	from	#LOANS
				as tgt
			inner join (
				select	lon.PN_NO,
						sum(case
							when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ') then led.DR - led.CR
							else 0
							end) as xbprin,
						max(case
							when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ') then [DATE]
							-- else 0																				-- JS 08/04/2012
							end) as xpdate,
						sum(case
							when led.ACCT_CODE = 'INT' and led.ACCT_TYPE in ('PAY', 'ADJ') then led.CR - led.DR
							else 0
							end) as xpint,
						sum(case
							when led.ACCT_CODE = 'OTH' and led.ACCT_TYPE in ('PAY', 'ADJ') then led.CR - led.DR
							else 0
							end) as xpoth
				from	#LOANS
							as lon
						inner join dbo.LEDGER
							as led
							on led.PN_NO = lon.PN_NO
				where	lon.asked = 1
				group
				by		lon.PN_NO
				)
				as src
				on src.PN_NO = tgt.PN_NO

	update	#LOANS
	set		xfreq = 1,
			updated = 1, trace = trace + ',' + 'M'
	where	asked = 1

	update	#LOANS
	set		xfreq = case
				when FREQ = 'M' then 12
				when FREQ = 'S' then 6
				when FREQ = 'Q' then 4
				when FREQ = 'A' then 1
				else 1
				end,
			xgomo = case
				when FREQ = 'M' then 1
				when FREQ = 'S' then 6
				when FREQ = 'Q' then 3
				when FREQ = 'A' then 12
				else 0
				end,
			mayb = 0,
			updated = 1, trace = trace + ',' + 'N'
	where	asked = 1

	update	#LOANS
	set		mayb = 1,
			updated = 1, trace = trace + ',' + 'O'
	where	asked  =1 and
			xpdate is not null and
			xpdate >= DATEADD(D, 1, DATEADD(M, 0 - xgomo, NDUE)) and
			xpdate < NDUE

	--IF
		update	#LOANS
		set		xpri = xbprin,
				xint = case
					when mayb = 1 then (xbprin * RATE * DATEDIFF(D, xpdate, @SYSDATE)) / 36000
					when mayb = 0 then AMORT_AMT - PRINCIPAL - xpint
					else 0
					end,
				updated = 1, trace = trace + ',' + 'P'
		where	asked = 1 and
				AMORT_AMT > PRINCIPAL
	--ELSE
		update	#LOANS
		set		xint = case
					when mayb = 1 then (xbprin * RATE * DATEDIFF(D, xpdate, @SYSDATE)) / 36000
					when mayb = 0 then ROUND((xbprin * RATE) / (100 * xfreq), 2)
					else 0
					end,
				updated = 1, trace = trace + ',' + 'Q'
		where	asked = 1 and
				AMORT_AMT <= PRINCIPAL and
			RIGHT(trace, 2) not in (',P')
		
		--IF
			update	#LOANS
			set		xpri = xbprin,
					updated = 1, trace = trace + ',' + 'R'
			where	asked = 1 and
					AMORT_AMT <= PRINCIPAL and
					xbprin <= AMORT_AMT and
				RIGHT(trace, 2) not in (',P')
		--ELSE
			update	#LOANS
			set		xpri = 0,
					updated = 1, trace = trace + ',' + 'S'
			where	asked = 1 and
					AMORT_AMT <= PRINCIPAL and
					xbprin > AMORT_AMT and
					RIGHT(trace, 2) not in (',P', ',R')

			--IF
				update	#LOANS
				set		amort = dbo.func_J_AMORT(PRINCIPAL, AMORT_AMT, FREQ, RATE, PAY_START, @SYSDATE)
				where	asked = 1 and
						AMORT_AMT <= PRINCIPAL and
						xbprin > AMORT_AMT and
						ARREAR_AS is not null and
						RIGHT(trace, 2) not in (',P', ',R')
						
				update	#LOANS
				set		xpri = xbprin - CONVERT(numeric(13,4), SUBSTRING(
							amort, 
							CHARINDEX('xbal:', amort, 1) + 5, 
							CHARINDEX(';', amort, CHARINDEX('xbal:', amort, 1) + 5) - CHARINDEX('xbal:', amort, 1) - 5
							)),
						xint = CONVERT(numeric(13,4), SUBSTRING(
							amort, 
							CHARINDEX('xint:', amort, 1) + 5, 
							CHARINDEX(';', amort, CHARINDEX('xint:', amort, 1) + 5) - CHARINDEX('xint:', amort, 1) - 5
							)),
						updated = 1, trace = trace + ',' + 'T'
				where	asked = 1 and
						AMORT_AMT <= PRINCIPAL and
						xbprin > AMORT_AMT and
						ARREAR_AS is not null and
						RIGHT(trace, 2) not in (',P', ',R')

				--IF
					update	#LOANS
					set		ARREAR_AS = null,
							ARREAR_P = 0,
							ARREAR_I = 0,
							ARREAR_OTH = 0,
							xpri = ROUND(AMORT_AMT - xint, 2),
							updated = 1, trace = trace + ',' + 'U'
					where	asked = 1 and
							AMORT_AMT <= PRINCIPAL and
							xbprin > AMORT_AMT and
							ARREAR_AS is not null and
							xpri <= 0 and
							RIGHT(trace, 2) not in (',P', ',R')
				--ELSE
					update	#LOANS
					set		ARREAR_P = xpri,
							updated = 1, trace = trace + ',' + 'V'
					where	asked = 1 and
							AMORT_AMT <= PRINCIPAL and
							xbprin > AMORT_AMT and
							ARREAR_AS is not null and
							xpri > 0 and
							RIGHT(trace, 2) not in (',P', ',R', ',U')

					update	#LOANS
					set		xpri = 0,
							updated = 1, trace = trace + ',' + 'W'
					where	asked = 1 and
							AMORT_AMT <= PRINCIPAL and
							xbprin > AMORT_AMT and
							ARREAR_AS is not null and
							xpri > 0 and
							RIGHT(trace, 2) not in (',P', ',R', ',U')
			--ELSE
				update	#LOANS
				set		xpri = ROUND(AMORT_AMT - xint, 2),
						updated = 1, trace = trace + ',' + 'X'
				where	asked = 1 and
						AMORT_AMT <= PRINCIPAL and
						xbprin > AMORT_AMT and
						ARREAR_AS is null and
						RIGHT(trace, 2) not in (',P', ',R', ',T', ',U', ',V', ',W')

	update	#LOANS
	set		xpri = 0,
			xint = 0,
			updated = 1, trace = trace + ',' + 'Y'
	where	asked = 1 and
			NDUE > DATE_DUE		

	update	#LOANS
	set		xpri = 0,
			updated = 1, trace = trace + ',' + 'Z'
	where	asked = 1 and
			xpri < 0

	--IF
		update	#LOANS
		set		xdate = case
					when DATENAME(W, DATEADD(D, 30, NDUE)) = 'Sunday' then DATEADD(D, 31, NDUE)
					when DATENAME(W, DATEADD(D, 30, NDUE)) = 'Saturday' then DATEADD(D, 32, NDUE)
					else DATEADD(D, 30, NDUE)
					end,
				updated = 1, trace = trace + ',' + 'AA'
		where	asked = 1 and
				FREQ = 'D'
	--ELSE
		update	#LOANS
		set		xdate = DATEADD(M, xgomo, NDUE),
				updated = 1, trace = trace + ',' + 'AB'
		where	asked = 1 and
				FREQ != 'D' and
				RIGHT(trace, 2) not in ('AA')

	update	#LOANS
	set		P_BAL = P_BAL + xpri,
			I_BAL = I_BAL + xint,
			NDUE = case
				when xdate <= DATE_DUE then xdate
				else NDUE
				end,
			updated = 1, trace = trace + ',' + 'AC'
	where	asked = 1

-----------------  asked end

--ELSE
	--IF
		update	#LOANS
		set		xarro = ARREAR_P + ARREAR_I + ARREAR_OTH,
				xbalo = P_BAL + I_BAL + O_BAL,
				updated = 1, trace = trace + ',' + 'AD'
		where	LOAN_STAT not in ('F', 'T') and
				NDUE != @SYSDATE and
				ARREAR_AS is null and
				asked = 0
		--IF
		
			update	#LOANS
			set		ARREAR_AS = @SYSDATE,
					ARREAR_P = ARREAR_P + P_BAL,
					ARREAR_I = ARREAR_I + I_BAL,
					ARREAR_OTH = ARREAR_OTH + O_BAL,
					P_BAL = 0,
					I_BAL = 0,
					O_BAL = 0,
					updated = 1, trace = trace + ',' + 'AE'
			where	LOAN_STAT not in ('F', 'T') and
					NDUE != @SYSDATE and
					ARREAR_AS is null and
					xbalo > 0 and
					asked = 0
	--ELSE

		-----------------  myarsp
		/*
		where	LOAN_STAT not in ('F', 'T') and
				NDUE != @SYSDATE and
				ARREAR_AS is not null and
				LOAN_TYPE != 'STL' and
				ARREAR_AS < @proc_date
		*/

		update	#LOANS
		set		myarsp = 1,
				updated = 1, trace = trace + ',' + 'AG'
		where	LOAN_STAT not in ('F', 'T') and
				NDUE != @SYSDATE and
				ARREAR_AS is not null and
				LOAN_TYPE != 'STL' and
				ARREAR_AS < @proc_date and
				asked = 0 and
				RIGHT(trace, 2) not in ('AD', 'AE')

		update	#LOANS
		set		xydate = PAY_START,
				xybal = PRINCIPAL,
				updated = 1, trace = trace + ',' + 'AH'
		where	myarsp = 1

		update	#LOANS
		set		xybal = tgt.xybal + src.xybal,
				xydate = case
					when src.xydate is not null then src.xydate
					else tgt.xydate
					end,
				updated = 1, trace = trace + ',' + 'AI'
		from	#LOANS
					as tgt
				inner join (
					select	lon.PN_NO,
							sum(case
								when led.ACCT_CODE = 'PRI' and ACCT_TYPE in ('PAY', 'ADJ') then led.DR - led.CR
								else 0
								end) as xybal,
							max(led.[DATE]) as xydate
					from	#LOANS
								as lon
							inner join dbo.LEDGER
								as led
								on led.PN_NO = lon.PN_NO
					where	lon.myarsp = 1
					group
					by		lon.PN_NO
					)
					as src
					on src.PN_NO = tgt.PN_NO
					
		update	#LOANS
		set		amort = dbo.func_J_AMORT(PRINCIPAL, AMORT_AMT, FREQ, RATE, PAY_START, @SYSDATE),
				updated = 1
		where	myarsp = 1

		update	#LOANS
		set		xyarsp = xybal - CONVERT(numeric(13,4), SUBSTRING(
					amort, 
					CHARINDEX('xbal:', amort, 1) + 5, 
					CHARINDEX(';', amort, CHARINDEX('xbal:', amort, 1) + 5) - CHARINDEX('xbal:', amort, 1) - 5
					)),
				xint = CONVERT(numeric(13,4), SUBSTRING(
					amort, 
					CHARINDEX('xint:', amort, 1) + 5, 
					CHARINDEX(';', amort, CHARINDEX('xint:', amort, 1) + 5) - CHARINDEX('xint:', amort, 1) - 5
					)),
				updated = 1, trace = trace + ',' + 'AJ'
		where	myarsp = 1

		update	#LOANS
		set		ARREAR_P = case
					when DATE_DUE > @SYSDATE then xyarsp
					else xybal
					end,
				updated = 1, trace = trace + ',' + 'AK'
		where	myarsp = 1

		-----------------  myarsp end

		update	#LOANS
		set		ARREAR_AS = null,
				ARREAR_P = 0,
				ARREAR_I = 0,
				ARREAR_OTH = 0,
				updated = 1, trace = trace + ',' + 'AL'
		where	LOAN_STAT not in ('F', 'T') and
				NDUE != @SYSDATE and
				ARREAR_AS is not null and
				ARREAR_P < 0 and
				asked = 0 and
				RIGHT(trace, 2) not in ('AD', 'AE')

-----------------  Apply Amortization Due End
-----------------  Compute for Penalties Incurred

update	#LOANS
set		xbprin = PRINCIPAL,
		xpint = 0,
		xlastd = PAY_START,
		updated = 1, trace = trace + ',' + 'AM'
where	LOAN_STAT not in ('F', 'T')

update	#LOANS
set		xbprin = tgt.xbprin + src.xbprin,
		xpint = src.xpint,
		xlastd = case
			when src.xlastd is not null then src.xlastd
			else tgt.xlastd
			end,
		updated = 1, trace = trace + ',' + 'AN'
from	#LOANS
			as tgt
		inner join (
			select	lon.PN_NO,
					sum(case
						when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') then led.DR - led.CR
						else 0
						end) as xbprin,
					max(case
						when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ') then led.[DATE]
						end) as xlastd,
					sum(case
						when led.ACCT_CODE = 'INT' then led.CR - led.DR
						else 0
						end) as xpint
			from	#LOANS
						as lon
					inner join dbo.LEDGER
						as led
						on led.PN_NO = lon.PN_NO
			where	lon.LOAN_STAT not in ('F', 'T')
			group
			by		lon.PN_NO
			)
			as src
			on src.PN_NO = tgt.PN_NO

update	#LOANS
set		ARREAR_OTH = ((ARREAR_P + ARREAR_I) * RATE * (1 + DATEDIFF(D, ARREAR_AS, @SYSDATE))) / 36000,
		updated = 1, trace = trace + ',' + 'AO'
where	LOAN_STAT not in ('F', 'T') and
		ARREAR_AS is not null and
		ARREAR_AS != PAY_START and
		LOAN_TYPE = 'STL'

update	#LOANS
set		axfreq = case
			when FREQ = 'M' then 12
			when FREQ = 'S' then 6
			when FREQ = 'Q' then 4
			when FREQ = 'A' then 1
			else 1
			end,
		axgomo = case
			when FREQ = 'M' then -1
			when FREQ = 'S' then -6
			when FREQ = 'Q' then -3
			when FREQ = 'A' then -12
			else 1
			end,
		ARREAR_P = case										-- JS 08/04/2012
			when @SYSDATE >= DATE_DUE then xbprin			--		|
			else ARREAR_P									--		|
			end,											-- JS 08/04/2012
		ARREAR_I = case																							-- JS 11/09/2013
			--when ARREAR_AS = @SYSDATE and ISNULL(ARREAR_I, 0) = 0												-- JS 10/11/2014
			when ARREAR_AS = @SYSDATE																			-- JS 01/16/2016
			then ((xbprin * RATE) / 36000) * DATEDIFF(D, xlastd, @SYSDATE)										-- JS 11/09/2013
			else ARREAR_I + ((xbprin * RATE) / 36000)
			end,																								-- JS 11/09/2013
		I_BAL = 0,
		updated = 1, trace = trace + ',' + 'AP'
where	LOAN_STAT not in ('F', 'T') and
		ARREAR_AS is not null and
		ARREAR_AS != PAY_START and
		LOAN_TYPE != 'STL' and
		ARREAR_AS > PAY_START and
		RIGHT(trace, 2) not in ('AO')

-- 11/30/2012

	update	#LOANS
	set		xpdate = case
				when src.xpdate is null then tgt.PAY_START
				else src.xpdate
				end
	from	#LOANS
				as tgt
			inner join (
				select	lon.PN_NO,
						max(case
							when led.ACCT_CODE = 'PRI' and led.ACCT_TYPE in ('PAY', 'ADJ') then [DATE]
							end) as xpdate
				from	#LOANS
							as lon
						inner join dbo.LEDGER
							as led
							on led.PN_NO = lon.PN_NO
				where	LOAN_STAT not in ('F', 'T') and
						ARREAR_AS is not null and
						ARREAR_AS != PAY_START and
						LOAN_TYPE != 'STL' and
						NDUE = DATEADD(D, -1, @SYSDATE)
				group
				by		lon.PN_NO
				)
				as src
				on src.PN_NO = tgt.PN_NO


update	#LOANS
set		ARREAR_OTH = ARREAR_OTH + ((DATEDIFF(D, xpdate, @SYSDATE) * AMORT_AMT * 12.0000) / 36000)
where	LOAN_STAT not in ('F', 'T') and
		ARREAR_AS is not null and
		ARREAR_AS != PAY_START and
		LOAN_TYPE != 'STL' and
		NDUE = DATEADD(D, -1, @SYSDATE)

-- 11/30/2012

update	#LOANS
set		ARREAR_OTH = case
			when @SYSDATE > DATE_DUE OR PD = 1
			then ARREAR_OTH + ((xbprin * 12.0000) / 36000)
			--when ARREAR_AS = @SYSDATE and ISNULL(ARREAR_OTH, 0) = 0					-- JS 10/11/2014
			when ARREAR_AS = @SYSDATE													-- JS 01/16/2016
			then ((AMORT_AMT * 12.0000) / 36000) * DATEDIFF(D, xlastd, @SYSDATE)		-- JS 11/09/2013
			else ARREAR_OTH + ((AMORT_AMT * 12.0000) / 36000)
			end,
		updated = 1, trace = trace + ',' + 'AQ'
where	LOAN_STAT not in ('F', 'T') and
		ARREAR_AS is not null and
		ARREAR_AS != PAY_START and
		LOAN_TYPE != 'STL'

update	#LOANS
set		ARREAR_OTH = case
			when ARREAR_OTH < 0 then 0
			else ARREAR_OTH
			end,
		ARREAR_I = case
			when ARREAR_I < 0 then 0
			else ARREAR_I
			end,
		updated = 1, trace = trace + ',' + 'AR'
where	LOAN_STAT not in ('F', 'T') and
		ARREAR_AS is not null and
		ARREAR_AS != PAY_START

update	#LOANS
set		ARREAR_I = ARREAR_I + ((xbprin * RATE) / 36000),
		ARREAR_OTH = ARREAR_OTH + ((xbprin * 12) / 36000),
		ARREAR_P = case
			when @SYSDATE > DATE_DUE then xbprin
			else ARREAR_P
			end,
		updated = 1, trace = trace + ',' + 'AS'
where	LOAN_STAT not in ('F', 'T') and
		not (
			ARREAR_AS is not null and
			ARREAR_AS != PAY_START
		) and
		(
			@SYSDATE > DATE_DUE  or
			PD = 1
		) and
		RIGHT(trace, 2) not in ('AO', 'AP', 'AQ', 'AR')

update	#LOANS
set		ARREAR_P = 0,
		ARREAR_I = 0,
		ARREAR_OTH = 0,
		ARREAR_AS = null,
		updated = 1, trace = trace + ',' + 'AT'
where	LOAN_STAT not in ('F', 'T') and
		not (
			ARREAR_AS is not null and
			ARREAR_AS != PAY_START
		) and
		not (
			@SYSDATE > DATE_DUE  or
			PD = 1
		) and
		RIGHT(trace, 2) not in ('AO', 'AP', 'AQ', 'AR', 'AS')

update	#LOANS
set		ACCU_PAYP = PRINCIPAL - xbprin,
		YTD_I = xpint,
		updated = 1, trace = trace + ',' + 'AU'
where	LOAN_STAT not in ('F', 'T')

update	#LOANS
set		ARREAR_P = 0,
		ARREAR_I = 0,
		ARREAR_OTH = 0,
		P_BAL = 0,
		I_BAL = 0,
		O_BAL = 0,
		LOAN_STAT = 'F',
		CHG_DATE = @pdate,
		updated = 1, trace = trace + ',' + 'AV'
where	LOAN_STAT not in ('F', 'T') and
		ACCU_PAYP >= PRINCIPAL and
		LOAN_STAT = 'R'

-----------------  Compute for Penalties Incurred end
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
		
--delete	#LOANS

-----------------  apply changes to loans end
-----------------  apply LRI

update	LRIDUE
set		LOAN_BAL = 0,
		LRI_DUE_C = 0,
		LRI_DUE_P = 0,
		LRI_DUE_Y = 0
where	PN_NO in (
		select	lon.PN_NO
		from	dbo.LOANS
					as lon
				inner join dbo.MEMBERS
					as mem
					on mem.KBCI_NO = lon.KBCI_NO
		where	lon.LOAN_STAT != 'R' and
				mem.MEM_STAT != 'R'
		)

exec Process_EndOfDay_LriDue @sysdate																				-- JS 08/31/3013

exec Process_EndOfDay_Arrears																						-- JS 03/14/2015

/* DELETE TEMP TABLE */

--delete	#LRIDUE

-----------------  apply LRI end

update	LOANS
set		LOAN_STAT = 'F',
		CHG_DATE = @pdate
where	LOAN_STAT != 'F' and
		ACCU_PAYP >= PRINCIPAL

delete	LEDGEREV

update	RUNUP1
set		LN_AMNT = FLN_AMNT,
		INT_AMNT = FINT_AMNT

update	RUNUP1
set		FLN_AMNT = 0,
		FINT_AMNT = 0

update	RRUNUP1
set		LN_AMNT = FLN_AMNT,
		INT_AMNT = FINT_AMNT

update	RRUNUP1
set		FLN_AMNT = 0,
		FINT_AMNT = 0




GO

