USE [KBCI]
GO

/****** Object:  StoredProcedure sa.[dbo].sa.[Report_Loans_LoansStatement]    Script Date: 07/04/2009 22:50:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_LoansStatement]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_LoansStatement]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure sa.[dbo].sa.[Report_Loans_LoansStatement]    Script Date: 07/04/2009 22:50:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Report_Loans_LoansStatement]
@kbci_no varchar(7),
@my_user varchar(8)
AS

declare @sysdate date
declare @total numeric(14, 4)
declare @totalStl numeric(14, 4)

select
	top 1
	@sysdate = SYSDATE
from
	dbo.CTRL with(nolock)

select
	@total = sum
	(
		case
			when LOAN_TYPE != 'STL' then BALANCE
			else 0
			end
	),
	@totalStl = sum
	(
		case
			when LOAN_TYPE = 'STL' then BALANCE
			else 0
			end
	)
from
	S_ACCNT_LOANS with(nolock)
where
	KBCI_NO = @kbci_no

select	
	dbo.func_format241( sa.[KBCI_NO]) KBCI_NO,
	@total TOTAL,
	@totalStl TOTAL_STL,
	sa.[INT] [INT],
	sa.[PEN] PEN,
	sa.[SAV] SAV,
	sa.[FIX] FIX,
	sa.[LRI] LRI,
	sa.[USER],
	sa.[SEP_DATE],
	sa.[PRE_DATE],
	substring(sa.MARK,1,1) + substring(lower(sa.MARK), 2, len(sa.MARK) - 1) MARK,
	sa.[NO_UPDATE],
	sa.[L_UPDATE],
	sa.[NOTE],
	sa.[ADV],
	isnull(mem.LNAME + ', ', '') + isnull(mem.FNAME + ' ', '') + isnull(mem.MI + '.', '') FULL_NAME,
	dbo.func_Age(mem.B_DATE, NULL) AGE,
	datename(M, @sysdate) + ' ' + datename(D, @sysdate) + ', ' + datename(YYYY, @sysdate) SYSDATE,
	usr.NAME MY_USER,
	mem.DEPT,
	dbo.func_Format451(mem.FEBTC_SA) FEBTC_SA,
	case
		when mem.MEM_CODE = 'M' then 'Regular Member'
		else 'Associate'
		end LABEL,
	c.CTD_COLLATERAL_0,
	c.CTD_COLLATERAL_1,
	mem.B_DATE
from
	dbo.S_ACCNT sa with(nolock)
		inner join 
	dbo.MEMBERS mem with(nolock) on 
		mem.KBCI_NO = sa.KBCI_NO
		inner join 
	dbo.[USER] usr with(nolock) on 
		usr.USERNAME = @my_user
		left join
	(
		select
			c.KBCI_NO,
			sum(case c.COLLATERAL
				when 0 then c.PRINCIPAL
				else 0
				end) CTD_COLLATERAL_0,
			sum(case c.COLLATERAL
				when 1 then c.PRINCIPAL
				else 0
				end) CTD_COLLATERAL_1
		from
			dbo.CTD c with(nolock)
		where
			c.KBCI_NO = @kbci_no and
			c.[STATUS] = 'NEW' and
			c.CTD_NO is not null and
			ltrim(rtrim(c.CTD_NO)) <> ''
		group by
			c.KBCI_NO
	) c on
		c.KBCI_NO = sa.KBCI_NO
where
	sa.KBCI_NO = @kbci_no

select
	lt.LOAN_DESC LOAN_TYPE,
	sal.BALANCE,
	sal.PD
from
	S_ACCNT_LOANS sal with(nolock)
		inner join
	LOAN_TYPE lt with(nolock) on
		lt.LOAN_TYPE = sal.LOAN_TYPE
where
	sal.KBCI_NO = @kbci_no
order by
	sal.LOAN_TYPE



GO