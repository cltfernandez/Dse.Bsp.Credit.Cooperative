USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_PaymentOrder]    Script Date: 08/19/2014 21:17:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[func_PaymentOrder]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[func_PaymentOrder]
GO

USE [KBCI]
GO

/****** Object:  UserDefinedFunction [dbo].[func_PaymentOrder]    Script Date: 08/19/2014 21:17:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[func_PaymentOrder]
(
	@type varchar(7),			-- LOANS OR MEMBERS
	@no varchar(7),
	@fullLri bit
)
returns @loans table (
	pn_no		varchar(7),
	loan_type	varchar(3),
	pay_start	date,
	ndue		date,
	date_due	date,
	chkno_date	date,
	arrear_as	date,
	rate		numeric(10,2) default 0,
	lri_due		numeric(10,2) default 0,
	wvadv		bit			  default 0,
	xlastd		date,
	xbprin		numeric(12,4) default 0,
	xadvint		numeric(12,4) default 0,
	intpay		numeric(12,4) default 0,
	xpint		numeric(12,4) default 0,
	xparr		numeric(12,4) default 0,
	xiarr		numeric(12,4) default 0,
	xpen		numeric(12,4) default 0,
	xiouts		numeric(12,4) default 0,
	xpouts		numeric(12,4) default 0,
	xoouts		numeric(12,4) default 0,
	xtfull		numeric(12,4) default 0,
	xtpart		numeric(12,4) default 0
)
as
begin

declare @sysdate date;

select top 1
	@sysdate = sysdate
from
	dbo.CTRL with (nolock)
;

insert into @loans (
	pn_no,
	loan_type,
	xparr,
	xiarr,
	xpen,
	xpouts,
	xiouts,
	xoouts,
	pay_start,
	ndue,
	date_due,
	rate,
	lri_due,
	chkno_date,
	arrear_as,
	xbprin,
	xadvint,
	xlastd
)
select
	lon.PN_NO,
	lon.LOAN_TYPE,
	lon.ARREAR_P,
	lon.ARREAR_I,
	lon.ARREAR_OTH,
	lon.P_BAL,
	lon.I_BAL,
	lon.O_BAL,
	lon.PAY_START,
	lon.NDUE,
	lon.DATE_DUE,
	lon.RATE,
	case @fullLri
		when 0 then isnull(lri.LRI_DUE_C, 0)
		else isnull(lri.LRI_DUE_P, 0)
		end,
	lon.CHKNO_DATE,
	lon.ARREAR_AS,
	sum
	(
		case 
			when ACCT_TYPE in ('PAY', 'ADJ', 'AMT') and ACCT_CODE = 'PRI' then isnull(DR, 0) - isnull(CR, 0)
			else 0
			end
	),
	sum
	(
		case 
			when ACCT_TYPE = 'INT' and ACCT_CODE = 'INT' then isnull(CR, 0)
			else 0
			end
	),
	max
	(
		case 
			when ACCT_TYPE in ('PAY', 'ADJ') and ACCT_CODE = 'PRI' then [DATE]
			else ''
			end
	)
from
	dbo.LOANS lon with (nolock)
		left join
	dbo.LRIDUE lri with (nolock) on
		lon.PN_NO = lri.PN_NO
		inner join
	dbo.LEDGER led with (nolock) on
		lon.PN_NO = led.PN_NO
where
	(
		(
			@type = 'MEMBERS' and
 			lon.KBCI_NO = @no
 		) or
 		(
 			@type = 'LOANS' and
 			lon.PN_NO = @no
 		)
 	) and
 	lon.LOAN_STAT = 'R'
group by
	lon.PN_NO,
	lon.LOAN_TYPE,
	lon.ARREAR_P,
	lon.ARREAR_I,
	lon.ARREAR_OTH,
	lon.P_BAL,
	lon.I_BAL,
	lon.O_BAL,
	lon.PAY_START,
	lon.NDUE,
	lon.DATE_DUE,
	lon.RATE,
	case @fullLri
		when 0 then isnull(lri.LRI_DUE_C, 0)
		else isnull(lri.LRI_DUE_P, 0)
		end,
	lon.CHKNO_DATE,
	lon.ARREAR_AS
;
	
update
	@loans
set
	xbprin = isnull(xbprin, 0),
	xadvint = isnull(xadvint, 0)
;

update
	@loans
set
	xlastd = 
	(
		case xlastd
			when '1900-01-01' then null
			else xlastd
			end
	)
;

update
	@loans
set
	intpay = src.intpay
from
	@loans tgt
		inner join
	(
		select
			lon.pn_no,
			sum
			(
				isnull(CR, 0) - isnull(DR, 0)
			) intpay
		from
			@loans lon
				inner join
			dbo.LEDGER led on
				led.PN_NO = lon.pn_no and
				led.ACCT_CODE = 'INT' and
				led.ACCT_TYPE in ('PAY', 'ADJ') and
				(
					(
						lon.xlastd is null and
						led.[DATE] >= lon.chkno_date
					) or
					(
						lon.xlastd is not null and
						led.[DATE] > lon.xlastd
					)
				)
		group by
			lon.PN_NO
	) src on
		src.PN_NO = tgt.pn_no
;

-- calculate using formula and apply to all for now. will be set to zero after
update
	@loans
set
	xpint =
	(
		case
		when isnull(xlastd, '1900-01-01') = '1900-01-01'
		then (xbprin * (rate/100/360) * datediff(dd, chkno_date, @sysdate)) - xadvint
		else (xbprin * (rate/100/360) * datediff(dd, xlastd, @sysdate))
		end
	)
;

update
	@loans
set
	xpint =
	(
		case
		when not (date_due <> pay_start or ndue <> @sysdate)
		then 0
		else xpint
		end
	),
	wvadv =
	(
		case
		when @sysdate < pay_start
		then 1
		else wvadv
		end
	)
where
	ndue = pay_start and
	isnull(xlastd, '1900-01-01') = '1900-01-01'
;

update
	@loans
set
	xiouts = 0,
	xpouts = 0
where
	ndue = pay_start and
	isnull(xlastd, '1900-01-01') != '1900-01-01'
;

update
	@loans
set
	xpint = 0,
	intpay = 0
where
	ndue != pay_start and
	xparr + xiarr + xpen > 0
;

update
	@loans
set
	xpint = 0,
	xiouts = round(xiouts, 2)
where
	ndue != pay_start and
	xparr + xiarr + xpen <= 0 and
	isnull(xlastd, '1900-01-01') = '1900-01-01' and
	@sysdate = ndue
;

update
	@loans
set
	xbprin = round(isnull(xbprin, 0), 2),
	xpouts = round(isnull(xpouts, 0), 2),
	xiouts = round(isnull(xiouts, 0), 2),
	xpint = round(isnull(xpint, 0), 2),
	xparr = round(isnull(xparr, 0), 2),
	xiarr = round(isnull(xiarr, 0), 2),
	xpen = round(isnull(xpen, 0), 2),
	lri_due = round(isnull(lri_due, 0), 2)
;
	
update
	@loans
set
	xpint = round(xpint - intpay, 2),
	xtfull =
	(
		case 
		when wvadv = 1
		then xbprin + xpint + xiarr + xpen + lri_due
		when wvadv != 1 and xiarr + xpen > 0
		then xbprin + xiarr + xpen + lri_due
		when wvadv != 1 and xiarr + xpen <= 0
		then xbprin + xpint + xiouts + xiarr + xpen + lri_due
		else xtfull
		end
	),
	xadvint =
	(
		case wvadv
		when 1
		then 0
		else xadvint
		end
	),
	xtpart = xparr + xiarr + xpen + xpouts + xiouts
;

return;

end
GO


