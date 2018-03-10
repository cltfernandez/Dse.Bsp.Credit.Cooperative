USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Runup]    Script Date: 11/16/2013 18:28:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Adhoc_Runup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Adhoc_Runup]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Adhoc_Runup]    Script Date: 11/16/2013 18:28:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[Report_Adhoc_Runup]
@dateFrom datetime,
@dateTo datetime,
@dateAsOf datetime,
@sortByName bit,
@mode varchar(20) = 'Outstanding'
as

declare @sql nvarchar(max)
declare @select nvarchar(100)
declare @whereInner nvarchar(1000)
declare @whereOuter nvarchar(100)

if @mode = 'Outstanding'
begin
	select
		convert(bit, @sortByName) SORT_BY_NAME,
		case @sortByName
			when 1 then dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
			when 0 then lon.LOAN_TYPE
			end SORT,
		case @sortByName
			when 1 then lon.LOAN_TYPE
			when 0 then dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
			end DETAIL,
		lon.KBCI_NO,
		lon.PN_NO,
		lon.DATE_GRANT,
		CASE lon.[FREQ]
			WHEN 'A' THEN dateadd(M, (lon.[TERM] * 12) - 1, lon.[PAY_START])
			WHEN 'S' THEN dateadd(M, (lon.[TERM] * 6) - 1, lon.[PAY_START])
			WHEN 'Q' THEN dateadd(M, (lon.[TERM] * 3) - 1, lon.[PAY_START])
			WHEN 'M' THEN dateadd(M, (lon.[TERM]) - 1, lon.[PAY_START])
			WHEN 'D' THEN 
				CASE datename(dw, dateadd(D, lon.[TERM], lon.[CHKNO_DATE]))
					WHEN 'Saturday'
					THEN dateadd(D, lon.[TERM] + 2, lon.[CHKNO_DATE])
					WHEN 'Sunday'
					THEN dateadd(D, lon.[TERM] + 1, lon.[CHKNO_DATE])
					ELSE dateadd(D, lon.[TERM], lon.[CHKNO_DATE])
				END
			END NDUE,
		convert(int, lon.TERM) TERM,
		lon.PRINCIPAL LOAN_AMOUNT,
		lon.CHKNO_AMT NET_PROCEEDS,
		bal.BALANCE LOAN_BALANCE,
		lon.LOAN_STAT STAT,
		lon.RATE
	from
		dbo.LOANS lon
			cross apply
		dbo.func_BalanceAsOf(lon.PN_NO, @dateFrom, @dateTo, @dateAsOf) bal
			inner join
		dbo.MEMBERS mem on
			mem.KBCI_NO = lon.KBCI_NO
			inner join
		dbo.LOAN_TYPE lt on
			lt.LOAN_TYPE = lon.LOAN_TYPE
	where
		bal.BALANCE != 0
	order by
		lon.PD, SORT, DETAIL
end
else if @mode = 'Released'
begin
	select
		convert(bit, @sortByName) SORT_BY_NAME,
		case @sortByName
			when 1 then dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
			when 0 then lon.LOAN_TYPE
			end SORT,
		case @sortByName
			when 1 then lon.LOAN_TYPE
			when 0 then dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
			end DETAIL,
		lon.KBCI_NO,
		lon.PN_NO,
		lon.DATE_GRANT,
		CASE lon.[FREQ]
			WHEN 'A' THEN dateadd(M, (lon.[TERM] * 12) - 1, lon.[PAY_START])
			WHEN 'S' THEN dateadd(M, (lon.[TERM] * 6) - 1, lon.[PAY_START])
			WHEN 'Q' THEN dateadd(M, (lon.[TERM] * 3) - 1, lon.[PAY_START])
			WHEN 'M' THEN dateadd(M, (lon.[TERM]) - 1, lon.[PAY_START])
			WHEN 'D' THEN 
				CASE datename(dw, dateadd(D, lon.[TERM], lon.[CHKNO_DATE]))
					WHEN 'Saturday'
					THEN dateadd(D, lon.[TERM] + 2, lon.[CHKNO_DATE])
					WHEN 'Sunday'
					THEN dateadd(D, lon.[TERM] + 1, lon.[CHKNO_DATE])
					ELSE dateadd(D, lon.[TERM], lon.[CHKNO_DATE])
				END
			END NDUE,
		convert(int, lon.TERM) TERM,
		lon.PRINCIPAL LOAN_AMOUNT,
		lon.CHKNO_AMT NET_PROCEEDS,
		bal.BALANCE LOAN_BALANCE,
		lon.LOAN_STAT STAT,
		lon.RATE
	from
		dbo.LOANS lon
			cross apply
		dbo.func_BalanceAsOf(lon.PN_NO, @dateFrom, @dateTo, @dateAsOf) bal
			inner join
		dbo.MEMBERS mem on
			mem.KBCI_NO = lon.KBCI_NO
			inner join
		dbo.LOAN_TYPE lt on
			lt.LOAN_TYPE = lon.LOAN_TYPE
	where
		lon.CHKNO_DATE between @dateFrom and @dateTo
	order by
		lon.PD, SORT, DETAIL
end
else if @mode = 'Maturing'
begin
	select
		convert(bit, @sortByName) SORT_BY_NAME,
		case @sortByName
			when 1 then dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
			when 0 then lon.LOAN_TYPE
			end SORT,
		case @sortByName
			when 1 then lon.LOAN_TYPE
			when 0 then dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
			end DETAIL,
		lon.KBCI_NO,
		lon.PN_NO,
		lon.DATE_GRANT,
		CASE lon.[FREQ]
			WHEN 'A' THEN dateadd(M, (lon.[TERM] * 12) - 1, lon.[PAY_START])
			WHEN 'S' THEN dateadd(M, (lon.[TERM] * 6) - 1, lon.[PAY_START])
			WHEN 'Q' THEN dateadd(M, (lon.[TERM] * 3) - 1, lon.[PAY_START])
			WHEN 'M' THEN dateadd(M, (lon.[TERM]) - 1, lon.[PAY_START])
			WHEN 'D' THEN 
				CASE datename(dw, dateadd(D, lon.[TERM], lon.[CHKNO_DATE]))
					WHEN 'Saturday'
					THEN dateadd(D, lon.[TERM] + 2, lon.[CHKNO_DATE])
					WHEN 'Sunday'
					THEN dateadd(D, lon.[TERM] + 1, lon.[CHKNO_DATE])
					ELSE dateadd(D, lon.[TERM], lon.[CHKNO_DATE])
				END
			END NDUE,
		convert(int, lon.TERM) TERM,
		lon.PRINCIPAL LOAN_AMOUNT,
		lon.CHKNO_AMT NET_PROCEEDS,
		bal.BALANCE LOAN_BALANCE,
		lon.LOAN_STAT STAT,
		lon.RATE
	from
		dbo.LOANS lon
			cross apply
		dbo.func_Balance(lon.PN_NO, '1900-01-01', @dateAsOf) bal
			inner join
		dbo.MEMBERS mem on
			mem.KBCI_NO = lon.KBCI_NO
			inner join
		dbo.LOAN_TYPE lt on
			lt.LOAN_TYPE = lon.LOAN_TYPE
			inner join
		(
			SELECT
				PN_NO,
				CASE 
					WHEN MOD_PAY = '1' AND DATEPART(D, CHKNO_DATE) > 15 THEN CONVERT(DATETIME, DATENAME(YYYY, DATEADD(M, 2, CHKNO_DATE)) + '-' + CONVERT(VARCHAR, DATEPART(M, DATEADD(M, 2, CHKNO_DATE))) + '-07')
					ELSE CONVERT(DATETIME, DATENAME(YYYY, DATEADD(M, 1, CHKNO_DATE)) + '-' + CONVERT(VARCHAR, DATEPART(M, DATEADD(M, 1, CHKNO_DATE))) + '-07')
				END NDUE1
			FROM
				dbo.LOANS
		) due on
			lon.PN_NO = due.PN_NO
			
	where
		(lon.[FREQ] = 'A' and dateadd(M, (lon.[TERM] * 12) - 1, lon.[PAY_START]) between @dateFrom and @dateTo) or
		(lon.[FREQ] = 'S' and dateadd(M, (lon.[TERM] * 6) - 1, lon.[PAY_START]) between @dateFrom and @dateTo) or
		(lon.[FREQ] = 'Q' and dateadd(M, (lon.[TERM] * 3) - 1, lon.[PAY_START]) between @dateFrom and @dateTo) or
		(lon.[FREQ] = 'M' and dateadd(M, lon.[TERM], lon.[PAY_START]) between @dateFrom and @dateTo) or
		(
			lon.[FREQ] = 'D' and
			CASE datename(dw, dateadd(D, lon.[TERM], lon.[CHKNO_DATE]))
				WHEN 'Saturday'
				THEN dateadd(D, lon.[TERM] + 2, lon.[CHKNO_DATE])
				WHEN 'Sunday'
				THEN dateadd(D, lon.[TERM] + 1, lon.[CHKNO_DATE])
				ELSE dateadd(D, lon.[TERM], lon.[CHKNO_DATE])
			END between @dateFrom and @dateTo
		)
	order by
		lon.PD, SORT, DETAIL
end