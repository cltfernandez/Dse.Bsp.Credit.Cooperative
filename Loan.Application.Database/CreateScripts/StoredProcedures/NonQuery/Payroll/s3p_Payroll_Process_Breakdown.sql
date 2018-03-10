USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Process_Breakdown]    Script Date: 04/20/2009 11:31:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[s3p_Payroll_Process_Breakdown]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[s3p_Payroll_Process_Breakdown]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[s3p_Payroll_Process_Breakdown]    Script Date: 04/20/2009 11:31:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[s3p_Payroll_Process_Breakdown]
as

declare @MO_DEDN_DETL_ID int
declare @MO_DEDN_DETL_ID_PREV int
declare @EMP_NO varchar(6)
declare @EMP_NO_PREV varchar(6) = ''
declare @CODE5 varchar(4)
declare @AMT numeric(12, 4)
declare @PAY_AMT numeric(12, 4) = 0
declare @PAY_AMT_BAL numeric(12, 4) = 0
declare @SYSDATE date

select 
	@SYSDATE = SYSDATE
from
	CTRL

/* Initialize tables */

delete
	MO_DEDN_HIST
where
	PAY_DATE = @SYSDATE

delete
	MO_DEDN_EXCESS
where
	PAY_DATE = @SYSDATE

/* Apply payments */

declare paymentCursor cursor fast_forward forward_only for
select
	d.MO_DEDN_DETL_ID,
	d.EMP_NO,
	d.CODE5,
	d.AMT,
	p.PAY_AMT
from
	MO_DEDN_DETL d
		left join
	MO_DEDN_PAID p on
		d.EMP_NO = p.EMP_NO
		left join
	OTHER_TYPE ot on
		d.CODE5 = right(ot.CODE5, 4)
order by
	EMP_NO,
	ISNULL(DEDN_SORT, 999),
	AMT

open paymentCursor

fetch paymentCursor into
	@MO_DEDN_DETL_ID,
	@EMP_NO,
	@CODE5,
	@AMT,
	@PAY_AMT

while @@FETCH_STATUS = 0
begin
	if @EMP_NO != @EMP_NO_PREV
	begin
		if @PAY_AMT_BAL > 0
		begin
			if exists (select * from MO_DEDN_DETL where EMP_NO = @EMP_NO_PREV and CODE5 = '7635')
			begin
				/* Add excess to SD */
				update
					MO_DEDN_DETL
				set
					PAY_AMT = PAY_AMT + @PAY_AMT_BAL
				where
					EMP_NO = @EMP_NO_PREV and
					CODE5 = '7635'
			end
			else
			begin
				/* Insert SD record for the excess */
				insert into MO_DEDN_DETL
				(
					EMP_NO,
					CODE5,
					AMT,
					PAY_AMT,
					PAY_DATE
				)
				select
					EMP_NO,
					'7635',
					@PAY_AMT_BAL,
					@PAY_AMT_BAL,
					PAY_DATE
				from
					MO_DEDN_DETL
				where
					MO_DEDN_DETL_ID = @MO_DEDN_DETL_ID_PREV
			end
				
			insert into MO_DEDN_EXCESS
			(
				EMP_NO,	
				PAY_AMT, 
				PAY_DATE
			)
			select
				@EMP_NO, 
				@PAY_AMT_BAL, 
				@SYSDATE
			
		end
	
		set @EMP_NO_PREV = @EMP_NO
		set @PAY_AMT_BAL = @PAY_AMT
	end
	
	if @PAY_AMT_BAL >= 0
	begin
		update
			MO_DEDN_DETL
		set
			PAY_AMT = case when @PAY_AMT_BAL >= @AMT then @AMT else @PAY_AMT_BAL end
		where
			MO_DEDN_DETL_ID = @MO_DEDN_DETL_ID
			
		set @PAY_AMT_BAL = case when @PAY_AMT_BAL >= @AMT then @PAY_AMT_BAL - @AMT else 0 end
	end
	
	set @MO_DEDN_DETL_ID_PREV = @MO_DEDN_DETL_ID

	fetch paymentCursor into
		@MO_DEDN_DETL_ID,
		@EMP_NO,
		@CODE5,
		@AMT,
		@PAY_AMT
end

close paymentCursor
deallocate paymentCursor

update
	MO_DEDN_DETL
set
	PAY_DATE = @SYSDATE

insert into MO_DEDN_HIST
(
	EMP_NO,
	CODE5,
	AMT,
	PAY_AMT,
	PAY_DATE,
	OFF_CYCLE
)
select
	EMP_NO,
	CODE5,
	AMT,
	PAY_AMT,
	PAY_DATE,
	case when exists (select top 1 'exists' from MO_DEDN_HIST where year(PAY_DATE) = year(@SYSDATE) and month(PAY_DATE) = month(@SYSDATE)) then 1 else 0 end
from
	MO_DEDN_DETL



GO