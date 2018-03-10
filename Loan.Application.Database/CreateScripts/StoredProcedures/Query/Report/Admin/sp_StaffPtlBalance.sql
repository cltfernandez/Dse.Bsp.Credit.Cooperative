USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_StaffPtlBalance]    Script Date: 08/30/2014 14:40:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_StaffPtlBalance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_StaffPtlBalance]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_StaffPtlBalance]    Script Date: 08/30/2014 14:40:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_Admin_StaffPtlBalance]
@year int
as

declare @sysdate datetime
declare @day1 datetime = cast(cast(@year as varchar) + '-01-01' as datetime)

select
	@sysdate = SYSDATE
from
	CTRL
	
select
	@year REPORT_YEAR,
	FULL_NAME,
	'' AS PN_NO,
	SUM(M01 + M02 + M03) AS Q01,
	SUM(M04 + M05 + M06) AS Q02,
	SUM(M07 + M08 + M09) AS Q03,
	SUM(M10 + M11 + M12) AS Q04,
	SUM(M01) AS M01,
	SUM(M02) AS M02,
	SUM(M03) AS M03,
	SUM(M04) AS M04,
	SUM(M05) AS M05,
	SUM(M06) AS M06,
	SUM(M07) AS M07,
	SUM(M08) AS M08,
	SUM(M09) AS M09,
	SUM(M10) AS M10,
	SUM(M11) AS M11,
	SUM(M12) AS M12
from
	(
	select
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) FULL_NAME,
		lon.PN_NO,
		case when month(@sysdate) >=  1 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  1, @day1)) then isnull(bal01.BALANCE, 0) else 0 end M01,
		case when month(@sysdate) >=  2 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  2, @day1)) then isnull(bal02.BALANCE, 0) else 0 end M02,
		case when month(@sysdate) >=  3 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  3, @day1)) then isnull(bal03.BALANCE, 0) else 0 end M03,
		case when month(@sysdate) >=  4 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  4, @day1)) then isnull(bal04.BALANCE, 0) else 0 end M04,
		case when month(@sysdate) >=  5 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  5, @day1)) then isnull(bal05.BALANCE, 0) else 0 end M05,
		case when month(@sysdate) >=  6 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  6, @day1)) then isnull(bal06.BALANCE, 0) else 0 end M06,
		case when month(@sysdate) >=  7 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  7, @day1)) then isnull(bal07.BALANCE, 0) else 0 end M07,
		case when month(@sysdate) >=  8 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  8, @day1)) then isnull(bal08.BALANCE, 0) else 0 end M08,
		case when month(@sysdate) >=  9 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month,  9, @day1)) then isnull(bal09.BALANCE, 0) else 0 end M09,
		case when month(@sysdate) >= 10 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month, 10, @day1)) then isnull(bal10.BALANCE, 0) else 0 end M10,
		case when month(@sysdate) >= 11 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month, 11, @day1)) then isnull(bal11.BALANCE, 0) else 0 end M11,
		case when month(@sysdate) >= 12 and lon.CHKNO_DATE <= dateadd(day, -1, dateadd(month, 12, @day1)) then isnull(bal12.BALANCE, 0) else 0 end M12
	from
		dbo.MEMBERS mem
			inner join
		dbo.LOANS lon on
			mem.KBCI_NO = lon.KBCI_NO
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  1, @day1))) bal01
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  2, @day1))) bal02
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  3, @day1))) bal03
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  4, @day1))) bal04
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  5, @day1))) bal05
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  6, @day1))) bal06
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  7, @day1))) bal07
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  8, @day1))) bal08
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month,  9, @day1))) bal09
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month, 10, @day1))) bal10
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month, 11, @day1))) bal11
			outer apply
		dbo.func_Balance(lon.PN_NO, '2000-01-01', dateadd(day, -1, dateadd(month, 12, @day1))) bal12
	where
		mem.MEM_STAT = 'S' and
		lon.LOAN_TYPE = 'PTL' and
		(
			bal01.BALANCE > 0 or
			bal02.BALANCE > 0 or
			bal03.BALANCE > 0 or
			bal04.BALANCE > 0 or
			bal05.BALANCE > 0 or
			bal06.BALANCE > 0 or
			bal07.BALANCE > 0 or
			bal08.BALANCE > 0 or
			bal09.BALANCE > 0 or
			bal10.BALANCE > 0 or
			bal11.BALANCE > 0 or
			bal12.BALANCE > 0
		)
	) x
GROUP BY
	FULL_NAME
ORDER BY
	FULL_NAME


GO

