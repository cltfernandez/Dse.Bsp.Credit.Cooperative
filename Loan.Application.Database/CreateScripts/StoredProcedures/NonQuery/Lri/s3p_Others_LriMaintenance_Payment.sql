USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Others_LriMaintenance_Payment]    Script Date: 05/04/2013 16:19:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Others_LriMaintenance_Payment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Others_LriMaintenance_Payment]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Others_LriMaintenance_Payment]    Script Date: 05/04/2013 16:19:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[s3p_Others_LriMaintenance_Payment]
@xp int,
@pn_no varchar(7),
@kbci_no varchar(7),
@xpayment numeric(14, 2),
@my_user varchar(8),
@xfulpay bit,
@xor_no varchar(20) = '',
@xsa_no varchar(20) = '',
@xpdc_no varchar(20) = '',
@xpdc_bnk varchar(20) = ''
AS

declare @SYSDATE datetime;
declare @payrem varchar(20);
--declare @gen_lapp table (
--	value varchar(20)
--)
		
select
	@SYSDATE = SYSDATE
from
	dbo.CTRL;

if @xp = 2
	set @payrem = 'SA :' + @xsa_no;
	
if @xp = 3
	set @payrem = 'PDC: ' + ltrim(rtrim(@xpdc_bnk)) + '-' + ltrim(rtrim(@xpdc_no));
	
--if @xp != 1
--begin
--	insert into @gen_lapp(value)
--	exec dbo.s3p_J_GEN_LAPP 'V'
	
--	select
--		@xor_no = value
--	from
--		@gen_lapp
--end

insert into dbo.PAYHIST
(
	KBCI_NO,
	PN_NO,
	PAYTYPE,
	PAYAMT,
	PAYDATE,
	ADDATE,
	LUPDATE,
	UPDUSER,
	PAYOR,
	PAYREM
)
values
(
	@kbci_no,
	@pn_no,
	CONVERT(varchar, @xp),
	@xpayment,
	@SYSDATE,
	@SYSDATE,
	@SYSDATE,
	@my_user,
	@xor_no,
	@payrem
);

if @xp = 2
	insert into dbo.LNHOLD
	(
		ACCTNO,
		HOLDCD,
		HOLDTYPE,
		HOLDAMT,
		HOLDDATE,
		HOLDUSER
	)
	values
	(
		@xsa_no,
		'PAY',
		'DM',
		@xpayment,
		@SYSDATE,
		@my_user
	);

update
	dbo.LOANS
set
	CHG_DATE = @SYSDATE,
	LRI_DUE = LRI_DUE - @xpayment
where
	PN_NO = @pn_no;

if exists (select 'x' from RLRIDUE where PN_NO = @pn_no)
begin
	update tgt
	set
		LRI_DUE = src.LRI_DUE,
		LRI_BALDA = src.LRI_BALDA,
		LOAN_BAL = src.LOAN_BAL,
		LRI_DUE_C = src.LRI_DUE_C,
		LRI_DUE_P = src.LRI_DUE_P,
		LRI_DUE_Y = src.LRI_DUE_Y
	from
		RLRIDUE tgt
			inner join
		LRIDUE src
			on src.PN_NO = tgt.PN_NO
	where
		src.PN_NO = @pn_no;
end
else
begin
	insert into RLRIDUE
	(
		PN_NO,
		KBCI_NO,
		LRI_DUE,
		LRI_BALDA,
		LOAN_BAL,
		LRI_DUE_C,
		LRI_DUE_P,
		LRI_DUE_Y
	)
	select
		PN_NO,
		KBCI_NO,
		LRI_DUE,
		LRI_BALDA,
		LOAN_BAL,
		LRI_DUE_C,
		LRI_DUE_P,
		0
	from
		LRIDUE
	where
		PN_NO = @pn_no;
end

if @xfulpay = 1
	update
		LRIDUE
	set
		LRI_DUE_C = 0,
		LRI_DUE_P = 0,
		LRI_DUE = DATEADD(M, 12, LRI_DUE),
		LRI_BALDA = DATEADD(M, 12, LRI_BALDA)
	where
		PN_NO = @pn_no;
else
	update
		LRIDUE
	set
		LRI_DUE_C = LRI_DUE_C - @xpayment,
		LRI_DUE_P = LRI_DUE_P - @xpayment
	where
		PN_NO = @pn_no;

exec dbo.s3p_J_U_Ledger @pn_no, @SYSDATE,' CM', @xor_no, 'PAY', 'LRI', @xpayment, 'LRI PAYMENT'

GO