USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Process_EndOfDay_LriDue]    Script Date: 07/15/2009 11:17:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Process_EndOfDay_LriDue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Process_EndOfDay_LriDue]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Process_EndOfDay_LriDue]    Script Date: 07/15/2009 11:17:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*****************************************************************************
JS 05/01/2014	Ignore LRI refund - not to be included in computation
JS 08/23/2014	Ignore all 'INIT-LRI' (different from 'INITIAL-LRI')
JS 08/30/2014	Exclude INITIAL-LRI from due computations
*****************************************************************************/

CREATE PROCEDURE [dbo].[Process_EndOfDay_LriDue]
@sysdate datetime = null
AS

if @sysdate is null
	select
		@sysdate = SYSDATE
	from
		dbo.CTRL

create table #LRIDUE
(
	[PN_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[KBCI_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
	[PRINCIPAL] numeric(14, 4) DEFAULT 0,
	[LRI_DUE] date,
	[LOAN_BAL] numeric(14, 4) DEFAULT 0,
	[LRI_DUE_C] numeric(14, 4) DEFAULT 0,
	[LRI_DUE_P] numeric(14, 4) DEFAULT 0,
	[LRI_PAID] numeric(14, 4) DEFAULT 0,
	[Y01] numeric(18, 6) DEFAULT 0,
	[Y02] numeric(18, 6) DEFAULT 0,
	[Y03] numeric(18, 6) DEFAULT 0,
	[Y04] numeric(18, 6) DEFAULT 0,
	[Y05] numeric(18, 6) DEFAULT 0,
	[Y06] numeric(18, 6) DEFAULT 0,
	[Y07] numeric(18, 6) DEFAULT 0,
	[Y08] numeric(18, 6) DEFAULT 0,
	[Y09] numeric(18, 6) DEFAULT 0,
	[Y10] numeric(18, 6) DEFAULT 0,
	[P01] numeric(7, 6) DEFAULT 0,
	[P02] numeric(7, 6) DEFAULT 0,
	[P03] numeric(7, 6) DEFAULT 0,
	[P04] numeric(7, 6) DEFAULT 0,
	[P05] numeric(7, 6) DEFAULT 0,
	[P06] numeric(7, 6) DEFAULT 0,
	[P07] numeric(7, 6) DEFAULT 0,
	[P08] numeric(7, 6) DEFAULT 0,
	[P09] numeric(7, 6) DEFAULT 0,
	[P10] numeric(7, 6) DEFAULT 0,
	[F01] bit DEFAULT 0,
	[F02] bit DEFAULT 0,
	[F03] bit DEFAULT 0,
	[F04] bit DEFAULT 0,
	[F05] bit DEFAULT 0,
	[F06] bit DEFAULT 0,
	[F07] bit DEFAULT 0,
	[F08] bit DEFAULT 0,
	[F09] bit DEFAULT 0,
	[F10] bit DEFAULT 0
)

/* GET PRINCIPAL PAYMENTS PER LOAN ANNIVERSARY AND PRORATES */

insert	#LRIDUE 
(
	PN_NO,
	KBCI_NO,
	PRINCIPAL,
	LRI_PAID,
	LRI_DUE,
	Y01,
	Y02,
	Y03,
	Y04,
	Y05,
	Y06,
	Y07,
	Y08,
	Y09,
	Y10,
	P01,
	P02,
	P03,
	P04,
	P05,
	P06,
	P07,
	P08,
	P09,
	P10,
	F01,
	F02,
	F03,
	F04,
	F05,
	F06,
	F07,
	F08,
	F09,
	F10
)
select
	lon.PN_NO,
	lon.KBCI_NO,
	lon.PRINCIPAL,
	
	sum
	(
		case
		when																										-- JS 08/23/2014
			left(led.RMK, 8) = 'INIT-LRI' or																		--		|
			ltrim(rtrim(led.RMK)) = 'INITIAL - LRI'																						-- JS 08/30/2014
		then																										--		|
			0																										-- JS 08/23/2014
		when
			(
				led.ACCT_TYPE = 'PAY' and 
				led.ACCT_CODE = 'LRI'
			) or
			(
				led.ACCT_TYPE = 'LRI' and 
				led.ACCT_CODE = 'OTH'
			)
			then led.CR - led.DR
		else 0
		end
	)
		as LRI_PAID,
	
	case
		when @SYSDATE between DATEADD(YYYY, 1, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 2, lon.DATE_GRANT)) then DATEADD(YYYY, 1, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 2, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 3, lon.DATE_GRANT)) then DATEADD(YYYY, 2, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 3, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 4, lon.DATE_GRANT)) then DATEADD(YYYY, 3, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 4, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 5, lon.DATE_GRANT)) then DATEADD(YYYY, 4, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 5, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 6, lon.DATE_GRANT)) then DATEADD(YYYY, 5, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 6, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 7, lon.DATE_GRANT)) then DATEADD(YYYY, 6, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 7, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 8, lon.DATE_GRANT)) then DATEADD(YYYY, 7, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 8, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 9, lon.DATE_GRANT)) then DATEADD(YYYY, 8, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 9, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 10, lon.DATE_GRANT)) then DATEADD(YYYY, 9, lon.DATE_GRANT)
		when @SYSDATE between DATEADD(YYYY, 10, lon.DATE_GRANT) and DATEADD(D, -1 ,DATEADD(YYYY, 11, lon.DATE_GRANT)) then DATEADD(YYYY, 10, lon.DATE_GRANT)
		else lon.DATE_GRANT
		end LRI_DUE,
	
	--Y01 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(YYYY, 0, lon.DATE_GRANT) and DATEADD(YYYY, 1, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 1, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y01,
	
	--Y02 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 1, lon.DATE_GRANT)) and DATEADD(YYYY, 2, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 2, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y02,
	
	--Y03 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 2, lon.DATE_GRANT)) and DATEADD(YYYY, 3, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 3, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y03,
	
	--Y04 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 3, lon.DATE_GRANT)) and DATEADD(YYYY, 4, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 4, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y04,
	
	--Y05 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 4, lon.DATE_GRANT)) and DATEADD(YYYY, 5, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 5, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y05,
	
	--Y06 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 5, lon.DATE_GRANT)) and DATEADD(YYYY, 6, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 6, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y06,
	
	--Y07 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 6, lon.DATE_GRANT)) and DATEADD(YYYY, 7, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 7, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y07,
	
	--Y08 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 7, lon.DATE_GRANT)) and DATEADD(YYYY, 8, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 8, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y08,
	
	--Y09 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 8, lon.DATE_GRANT)) and DATEADD(YYYY, 9, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 9, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y09,
	
	--Y10 PRINCIPAL PAYMENTS
	sum(case
	when 
		led.ACCT_CODE = 'PRI' and 
		led.ACCT_TYPE in ('PAY', 'ADJ', 'TER', 'REP') and
		led.[DATE] between DATEADD(DD, 1, DATEADD(YYYY, 9, lon.DATE_GRANT)) and DATEADD(YYYY, 10, lon.DATE_GRANT) AND  
		@SYSDATE >= DATEADD(YYYY, 10, lon.DATE_GRANT)
		then led.CR - led.DR
	else 0
	end) Y10,
	
	case
		when @SYSDATE > DATEADD(YYYY, 2, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 1, lon.DATE_GRANT) and DATEADD(YYYY, 2, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 1, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P01,
	
	case
		when @SYSDATE > DATEADD(YYYY, 3, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 2, lon.DATE_GRANT) and DATEADD(YYYY, 3, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 2, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P02,
	
	case
		when @SYSDATE > DATEADD(YYYY, 4, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 3, lon.DATE_GRANT) and DATEADD(YYYY, 4, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 3, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P03,
		
	case
		when @SYSDATE > DATEADD(YYYY, 5, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 4, lon.DATE_GRANT) and DATEADD(YYYY, 5, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 4, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P04,
	
	case
		when @SYSDATE > DATEADD(YYYY, 6, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 5, lon.DATE_GRANT) and DATEADD(YYYY, 6, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 5, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P05,
	
	case
		when @SYSDATE > DATEADD(YYYY, 7, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 6, lon.DATE_GRANT) and DATEADD(YYYY, 7, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 6, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P06,
	
	case
		when @SYSDATE > DATEADD(YYYY, 8, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 7, lon.DATE_GRANT) and DATEADD(YYYY, 8, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 7, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P07,
		
	case
		when @SYSDATE > DATEADD(YYYY, 9, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 8, lon.DATE_GRANT) and DATEADD(YYYY, 9, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 8, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P08,
		
	case
		when @SYSDATE > DATEADD(YYYY,10, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 9, lon.DATE_GRANT) and DATEADD(YYYY,10, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY, 9, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P09,
	
	case
		when @SYSDATE > DATEADD(YYYY,11, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY,10, lon.DATE_GRANT) and DATEADD(YYYY,11, lon.DATE_GRANT) then CONVERT(numeric(11, 2), DATEDIFF(DAY, DATEADD(YYYY,10, lon.DATE_GRANT), @SYSDATE)) / 360
		else 0
		end P10,
	
	case
		when @SYSDATE > DATEADD(YYYY, 2, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 1, lon.DATE_GRANT) and DATEADD(YYYY, 2, lon.DATE_GRANT) then 1
		else 0
		end F01,
	
	case
		when @SYSDATE > DATEADD(YYYY, 3, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 2, lon.DATE_GRANT) and DATEADD(YYYY, 3, lon.DATE_GRANT) then 1
		else 0
		end F02,
	
	case
		when @SYSDATE > DATEADD(YYYY, 4, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 3, lon.DATE_GRANT) and DATEADD(YYYY, 4, lon.DATE_GRANT) then 1
		else 0
		end F03,
		
	case
		when @SYSDATE > DATEADD(YYYY, 5, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 4, lon.DATE_GRANT) and DATEADD(YYYY, 5, lon.DATE_GRANT) then 1
		else 0
		end F04,
	
	case
		when @SYSDATE > DATEADD(YYYY, 6, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 5, lon.DATE_GRANT) and DATEADD(YYYY, 6, lon.DATE_GRANT) then 1
		else 0
		end F05,
	
	case
		when @SYSDATE > DATEADD(YYYY, 7, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 6, lon.DATE_GRANT) and DATEADD(YYYY, 7, lon.DATE_GRANT) then 1
		else 0
		end F06,
	
	case
		when @SYSDATE > DATEADD(YYYY, 8, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 7, lon.DATE_GRANT) and DATEADD(YYYY, 8, lon.DATE_GRANT) then 1
		else 0
		end F07,
		
	case
		when @SYSDATE > DATEADD(YYYY, 9, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 8, lon.DATE_GRANT) and DATEADD(YYYY, 9, lon.DATE_GRANT) then 1
		else 0
		end F08,
		
	case
		when @SYSDATE > DATEADD(YYYY,10, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY, 9, lon.DATE_GRANT) and DATEADD(YYYY,10, lon.DATE_GRANT) then 1
		else 0
		end F09,
	
	case
		when @SYSDATE > DATEADD(YYYY,11, lon.DATE_GRANT) then 1
		when @SYSDATE between DATEADD(YYYY,10, lon.DATE_GRANT) and DATEADD(YYYY,11, lon.DATE_GRANT) then 1
		else 0
		end F10
from	
	dbo.LOANS
		as lon
		inner join 
	dbo.MEMBERS
		as mem
		on mem.KBCI_NO = lon.KBCI_NO
		inner join 
	dbo.LEDGER
		as led
		on led.PN_NO = lon.PN_NO
where
	mem.MEM_STAT != 'R' and
	lon.LOAN_STAT = 'R' and
	lon.LOAN_TYPE != 'STL'
	--not																	-- JS 05/01/2014
	--(																		--		|
	--	left(led.RMK, 8) = 'INIT-LRI' and									--		|
	--	led.CR = 0 and														--		|
	--	led.DR > 0															--		|
	--)																		-- JS 05/01/2014
group by
	lon.PN_NO,
	lon.KBCI_NO,
	lon.PRINCIPAL,
	lon.DATE_GRANT

/* SATURATE PRORATES */
	
update
	#LRIDUE
set
	LOAN_BAL = PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07 - Y08 - Y09 - Y10,
	P01 = case when P01 > 1 then 1 else P01 end,
	P02 = case when P02 > 1 then 1 else P02 end,
	P03 = case when P03 > 1 then 1 else P03 end,
	P04 = case when P04 > 1 then 1 else P04 end,
	P05 = case when P05 > 1 then 1 else P05 end,
	P06 = case when P06 > 1 then 1 else P06 end,
	P07 = case when P07 > 1 then 1 else P07 end,
	P08 = case when P08 > 1 then 1 else P08 end,
	P09 = case when P09 > 1 then 1 else P09 end,
	P10 = case when P10 > 1 then 1 else P10 end

/* COMPUTE PRETERM AND YEARLY FEES */

update
	#LRIDUE
set
	LRI_DUE_C =
	(
		ROUND
		(
			(
				--0.01 * PRINCIPAL +																				-- JS 08/30/2014
				CASE
					WHEN F01 > 0 THEN 0.01 * P01 * (PRINCIPAL - Y01)
					ELSE 0
					END +
				CASE
					WHEN F02 > 0 THEN 0.01 * P02 * (PRINCIPAL - Y01 - Y02)
					ELSE 0
					END +
				CASE
					WHEN F03 > 0 THEN 0.01 * P03 * (PRINCIPAL - Y01 - Y02 - Y03)
					ELSE 0
					END +
				CASE
					WHEN F04 > 0 THEN 0.01 * P04 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04)
					ELSE 0
					END +
				CASE
					WHEN F05 > 0 THEN 0.01 * P05 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05)
					ELSE 0
					END +
				CASE
					WHEN F06 > 0 THEN 0.01 * P06 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06)
					ELSE 0
					END +
				CASE
					WHEN F07 > 0 THEN 0.01 * P07 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07)
					ELSE 0
					END +
				CASE
					WHEN F08 > 0 THEN 0.01 * P08 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07 - Y08)
					ELSE 0
					END +
				CASE
					WHEN F09 > 0 THEN 0.01 * P09 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07 - Y08 - Y09)
					ELSE 0
					END +
				CASE
					WHEN F10 > 0 THEN 0.01 * P10 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07 - Y08 - Y09 -Y10)
					ELSE 0
					END - 
				LRI_PAID
			),
			4
		)
	),
	LRI_DUE_P =
	(
		ROUND
		(
			(
				--0.01 * PRINCIPAL +																				-- JS 08/30/2014
				CASE
					WHEN F01 > 0 THEN 0.01 * (PRINCIPAL - Y01)
					ELSE 0
					END +
				CASE
					WHEN F02 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02)
					ELSE 0
					END +
				CASE
					WHEN F03 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02 - Y03)
					ELSE 0
					END +
				CASE
					WHEN F04 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04)
					ELSE 0
					END +
				CASE
					WHEN F05 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05)
					ELSE 0
					END +
				CASE
					WHEN F06 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06)
					ELSE 0
					END +
				CASE
					WHEN F07 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07)
					ELSE 0
					END +
				CASE
					WHEN F08 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07 - Y08)
					ELSE 0
					END +
				CASE
					WHEN F09 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07 - Y08 - Y09)
					ELSE 0
					END +
				CASE
					WHEN F10 > 0 THEN 0.01 * (PRINCIPAL - Y01 - Y02 - Y03 - Y04 - Y05 - Y06 - Y07 - Y08 - Y09 -Y10)
					ELSE 0
					END - 
				LRI_PAID
			),
			4
		)
	)

/* SATURATE FEES */

update
	#LRIDUE
set
	LRI_DUE_C = case when LRI_DUE_C < 0 then 0 else LRI_DUE_C end,
	LRI_DUE_P = case when LRI_DUE_P < 0 then 0 else LRI_DUE_P end

/* INSERT LOANS NOT IN LRIDUE */

insert into dbo.LRIDUE
(
	PN_NO
)
select
	lri.PN_NO
from
	#LRIDUE lri
where not exists
(
	select
		*
	from
		dbo.LRIDUE
	where
		PN_NO = lri.PN_NO
)

/* UPDATE LRIDUE */

update
	dbo.LRIDUE
set	
	[PN_NO] = src.PN_NO,
	[KBCI_NO] = src.KBCI_NO,
	[LRI_DUE] = src.LRI_DUE,
	[LRI_BALDA] = NULL,
	[LOAN_BAL] = src.LOAN_BAL,
	[LRI_DUE_C] = src.LRI_DUE_C,
	[LRI_DUE_P] = src.LRI_DUE_P,
	[LRI_DUE_Y] = 0
from
	dbo.LRIDUE
		as tgt
		inner join 
	#LRIDUE
		as src
		on src.PN_NO = tgt.PN_NO

/* UPDATE LOANS */

update
	dbo.LOANS
set
	LRI_DUE = src.LRI_DUE_C
from
	dbo.LOANS
		as tgt
		inner join 
	dbo.LRIDUE
		as src
		on src.PN_NO = tgt.PN_NO

GO