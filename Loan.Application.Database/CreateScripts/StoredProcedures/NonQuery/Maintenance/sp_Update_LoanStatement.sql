USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Update_LoanStatement]    Script Date: 04/17/2009 17:28:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Update_LoanStatement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Update_LoanStatement]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Update_LoanStatement]    Script Date: 04/17/2009 17:28:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*****************************************************************************
MODIFIED:
JS 11/15/2014		USED CORRECT INTEREST
*****************************************************************************/

CREATE PROCEDURE [dbo].[Update_LoanStatement]
@my_user varchar(8),
@kbci_no varchar(7),
@s_rema varchar(10),
@s_date date,
@myldate date,
@updateme bit,
@reprintme bit,
@xgo bit,
@c_upd bit
AS

declare @pd bit
declare @loan_type varchar(3)
declare @mysavings numeric(16,4) = 0
declare @totalInterest numeric(16,4) = 0
declare @totalPenalty numeric(16,4) = 0
declare @totalLri numeric(16,4) = 0
declare @fd_amount numeric(13,4)
declare @sep_date date
declare @sysdate date

declare @user_loans table
(
	PN_NO varchar(7),
	LOAN_TYPE varchar(3),
	ARREAR numeric(14, 2),
	XBPRIN numeric(14, 2),
	XADVINT numeric(14, 2),
	XLASTD datetime,
	PD bit,
	PRINCIPAL numeric(14, 2),
	INTEREST numeric(14, 2),
	PENALTY numeric(14, 2),
	LRI numeric(14, 2)
)

select top 1
	@mysavings = ISNULL(sdm.ACCTOBAL,0),
	@fd_amount = ISNULL(mem.FD_AMOUNT,0)
from
	dbo.MEMBERS as mem
		inner join 
	dbo.SDMASTER as sdm 
		on sdm.ACCTNO = mem.FEBTC_SA
where
	mem.KBCI_NO = @kbci_no;

select top 1
	@sysdate = SYSDATE
from
	dbo.CTRL;

if @xgo = 0
begin
	select
		@s_rema = MARK,
		@sep_date = SEP_DATE
	from
		dbo.S_ACCNT
	where
		KBCI_NO = @kbci_no;
end

if exists (select 'x' from dbo.LOANS where KBCI_NO = @kbci_no and LOAN_STAT = 'R') begin
	if not exists (select 'x' from dbo.S_ACCNT where KBCI_NO = @kbci_no) begin
		insert	dbo.S_ACCNT 
		(
			KBCI_NO,
			[USER],
			PRE_DATE,
			L_UPDATE,
			NO_UPDATE,
			[INT],
			PEN,
			LRI,
			FIX,
			SAV,
			MARK,
			SEP_DATE
		)
		values
		(
			@kbci_no,
			@my_user,
			@sysdate,
			@sysdate,
			1,
			0,
			0,
			0,
			@fd_amount,
			@mysavings,
			@s_rema,
			@s_date
		)
		
		set @xgo = 1
	end

	if @updateme = 1 or @xgo = 1
	begin
	
		update
			dbo.S_ACCNT
		set
			[INT] = 0,
			PEN = 0,
			LRI = 0,
			FIX = @fd_amount,
			SAV = @mysavings,
			[USER] = @my_user,
			L_UPDATE = @sysdate,
			MARK = @s_rema,																							-- JS 11/23/2013
			SEP_DATE = @s_date																						-- JS 11/23/2013
		where
			KBCI_NO = @kbci_no;
			
		delete
			S_ACCNT_LOANS
		where
			KBCI_NO = @kbci_no
		
	end
	
	insert @user_loans
	(
		PN_NO,
		LOAN_TYPE,
		ARREAR,
		XBPRIN,
		XADVINT,
		XLASTD,
		
		PRINCIPAL,
		INTEREST,
		PENALTY,
		LRI
	)
	
	select
		pn_no,
		loan_type,
		xparr + xiarr + xpen,
		xbprin,
		xadvint,
		xlastd,
		
		xbprin,
		xpint + xiarr,														-- JS 11/15/2014
		xpen,
		lri_due
	from
		dbo.func_PaymentOrder('MEMBERS', @kbci_no, 1)
	
	/*
		
	insert @user_loans
	(
		PN_NO,
		LOAN_TYPE,
		ARREAR,
		XBPRIN,
		XADVINT,
		XLASTD
	)
	select
		lon.PN_NO,
		lon.LOAN_TYPE,
		case lon.LOAN_TYPE
			when 'STL' then
				isnull(lon.ARREAR_I, 0) + isnull(lon.ARREAR_OTH, 0)
			else
				isnull(lon.ARREAR_P, 0) + isnull(lon.ARREAR_I, 0) + isnull(lon.ARREAR_OTH, 0)
			end ARREAR,
		sum
		(
			case 
				when ACCT_TYPE in ('PAY', 'ADJ', 'AMT') and ACCT_CODE = 'PRI' then DR-CR
				else 0
				end
		) XBPRIN,
		sum
		(
			case 
				when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then CR
				else 0
				end
		) XADVINT,
		max
		(
			case 
				when ACCT_TYPE in ('PAY', 'ADJ') and ACCT_CODE = 'PRI' then [DATE]
				else ''
				end
		) XLASTD
	from
		dbo.LOANS lon
			inner join
		dbo.LEDGER led
			on led.PN_NO = lon.PN_NO
	where
		lon.KBCI_NO = @kbci_no and
		lon.LOAN_STAT = 'R'
	group by
		lon.PN_NO,
		lon.LOAN_TYPE,
		case lon.LOAN_TYPE
			when 'STL' then
				isnull(lon.ARREAR_I, 0) + isnull(lon.ARREAR_OTH, 0)
			else
				isnull(lon.ARREAR_P, 0) + isnull(lon.ARREAR_I, 0) + isnull(lon.ARREAR_OTH, 0)
			end;

	update
		tgt
	set
		tgt.PRINCIPAL = src.PRINCIPAL,
		tgt.INTEREST = src.INTEREST,
		tgt.PENALTY = src.PENALTY,
		tgt.LRI = src.LRI
	from
		@user_loans tgt
			inner join
		(
			select
				lon.PN_NO,
				case lon.LOAN_TYPE
					when 'STL' then 0
					else lon.PD
					end PD,
				ulons.XBPRIN PRINCIPAL,
				case
					when lon.NDUE = lon.PAY_START then
						case
							when isnull(ulons.XLASTD, '1900-01-01') = '1900-01-01' then
								case
									when lon.DATE_DUE <> lon.PAY_START OR lon.NDUE <> @SYSDATE then
										ulons.XBPRIN * (RATE/36000) * DATEDIFF(DD, lon.CHKNO_DATE, @SYSDATE) - ulons.XADVINT
									else
										0
									end
							else
								(ulons.XBPRIN * RATE * DATEDIFF(DD, ulons.XLASTD, @SYSDATE)) / 36000
							end
					else
						case
							when ulons.ARREAR > 0 then
								0
							else
								case
									when isnull(ulons.XLASTD, '1900-01-01') = '1900-01-01' then
										ulons.XBPRIN * (RATE/36000) * DATEDIFF(DD, lon.CHKNO_DATE, @SYSDATE) - ulons.XADVINT
									else
										ulons.XBPRIN * (RATE/36000) * DATEDIFF(DD, ulons.XLASTD, @SYSDATE)
									end
							end
				end INTEREST,
				ulons.ARREAR PENALTY,
				isnull(lri.LRI_DUE_C, 0) LRI
			from
				dbo.LOANS lon
					left join
				dbo.LRIDUE lri
					on lri.PN_NO = lon.PN_NO
					inner join	
				@user_loans ulons on
					lon.PN_NO = ulons.PN_NO
		) src on
			src.PN_NO = tgt.PN_NO
	
	*/
	
	insert into S_ACCNT_LOANS
	(
		KBCI_NO,
		LOAN_TYPE,
		BALANCE,
		PD
	)
	select
		@kbci_no,
		LOAN_TYPE,
		sum(PRINCIPAL),
		isnull(PD, 0)
	from
		@user_loans
	group by
		LOAN_TYPE,
		isnull(PD, 0)
	
	select
		@totalInterest = sum(INTEREST),
		@totalPenalty = sum(PENALTY),
		@totalLri = sum(LRI)
	from
		@user_loans
	where
		LOAN_TYPE != 'STL'
	
	update
		dbo.S_ACCNT
	set	
		[INT] = isnull([INT], 0) + isnull(@totalInterest,0),
		PEN = isnull(PEN, 0) + isnull(@totalPenalty,0),
		LRI = isnull(LRI, 0) + isnull(@totalLri,0),
		[USER] = @my_user,
		L_UPDATE = @sysdate,
		NO_UPDATE = NO_UPDATE + 1
	where
		KBCI_NO = @kbci_no;
	
end 
else
begin
	if not exists (select 'x' from dbo.S_ACCNT where KBCI_NO = @kbci_no) begin
		insert	dbo.S_ACCNT (
				KBCI_NO,
				[USER],
				PRE_DATE,
				L_UPDATE,
				NO_UPDATE,
				FIX,
				SAV,
				MARK,
				SEP_DATE)				
		values	(
				@kbci_no,
				@my_user,
				@sysdate,
				@sysdate,
				1,
				@fd_amount,
				@mysavings,
				@s_rema,
				@s_date)
		set @xgo = 0
	end else begin
		if @updateme = 1 begin
			update	dbo.S_ACCNT
			set		FIX = @fd_amount,
					SAV = @mysavings,
					L_UPDATE = @sysdate,
					NO_UPDATE = NO_UPDATE + 1,
					MARK = @s_rema,																					-- JS 11/23/2013
					SEP_DATE = @s_date																				-- JS 11/23/2013
			where	KBCI_NO = @kbci_no
			
			set	@updateme = 0
			set @c_upd = 1
		end
	end
end




GO


