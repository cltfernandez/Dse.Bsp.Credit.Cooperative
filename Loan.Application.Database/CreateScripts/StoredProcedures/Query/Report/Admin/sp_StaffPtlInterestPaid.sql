USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_StaffPtlInterestPaid]    Script Date: 08/30/2014 14:40:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Admin_StaffPtlInterestPaid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Admin_StaffPtlInterestPaid]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Admin_StaffPtlInterestPaid]    Script Date: 08/30/2014 14:40:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Report_Admin_StaffPtlInterestPaid]
@source varchar(3),
@year int
as

declare @yr varchar(4) = convert(varchar, @year)

exec
('
if ''' + @source + ''' = ''FOX''
begin
	select
		*
	into
		#MEMBERS
	from
		openrowset(
			''MSDASQL'',
			''Driver={Microsoft dBASE Driver (*.dbf)};DefaultDir=C:\Documents and Settings\admin\Desktop\DBF;SourceType=DBF'',
			''
				SELECT
					M.*
				FROM 
					MEMBERS M
				WHERE
					M.MEM_CODE = ''''S''''
			''
		);

	select
		*
	into
		#LOANS
	from
		openrowset(
			''MSDASQL'',
			''Driver={Microsoft dBASE Driver (*.dbf)};DefaultDir=C:\Documents and Settings\admin\Desktop\DBF;SourceType=DBF'',
			''
				SELECT
					L.*
				FROM 
					MEMBERS M 
						INNER JOIN 
					LOANS L ON
						M.KBCI_NO = L.KBCI_NO
				WHERE
					M.MEM_STAT = ''''S'''' AND
					L.LOAN_TYPE = ''''PTL''''
			''
	);

	select
		*
	into
		#LEDGER
	from
		openrowset
		(
			''MSDASQL'',
			''Driver={Microsoft dBASE Driver (*.dbf)};DefaultDir=C:\Documents and Settings\admin\Desktop\DBF;SourceType=DBF'',
			''
				SELECT
					LED.*
				FROM
					LEDGER LED
						INNER JOIN
					(
						SELECT
							L.*
						FROM 
							MEMBERS M 
								INNER JOIN 
							LOANS L ON
								M.KBCI_NO = L.KBCI_NO
						WHERE
							M.MEM_STAT = ''''S'''' AND
							L.LOAN_TYPE = ''''PTL''''
					) LON ON
						LON.PN_NO = LED.PN_NO
				WHERE
					LED.ACCT_CODE = ''''INT'''' AND
					LED.ACCT_TYPE <> ''''INI''''
			''
		);
		
	update
		#LEDGER
	set
		CR = isnull(CR, 0),
		DR = isnull(DR, 0)
	
	select
		dbo.func_Format241(lon.KBCI_NO) KBCI_NO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) [FULL_NAME],
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) between  1 and  3 then led.CR - led.DR else 0 end)) Q01,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) between  4 and  6 then led.CR - led.DR else 0 end)) Q02,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) between  7 and  9 then led.CR - led.DR else 0 end)) Q03,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) between 10 and 12 then led.CR - led.DR else 0 end)) Q04,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  1 then led.CR - led.DR else 0 end)) M01,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  2 then led.CR - led.DR else 0 end)) M02,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  3 then led.CR - led.DR else 0 end)) M03,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  4 then led.CR - led.DR else 0 end)) M04,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  5 then led.CR - led.DR else 0 end)) M05,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  6 then led.CR - led.DR else 0 end)) M06,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  7 then led.CR - led.DR else 0 end)) M07,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  8 then led.CR - led.DR else 0 end)) M08,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  9 then led.CR - led.DR else 0 end)) M09,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) = 10 then led.CR - led.DR else 0 end)) M10,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) = 11 then led.CR - led.DR else 0 end)) M11,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) = 12 then led.CR - led.DR else 0 end)) M12,
		' + @yr + ' [Year]
	from
		#LOANS lon
			inner join
		#MEMBERS mem ON
			lon.KBCI_NO = mem.KBCI_NO
			inner join
		#LEDGER led ON
			lon.PN_NO = led.PN_NO
	where
		datepart(YYYY, led.[DATE]) = ''' + @yr + '''
	group by
		lon.KBCI_NO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)

	drop table #MEMBERS
	drop table #LOANS
	drop table #LEDGER
end
else
begin
	
	select
		dbo.func_Format241(lon.KBCI_NO) KBCI_NO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) [FULL_NAME],
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) between  1 and  3 then led.CR - led.DR else 0 end)) Q01,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) between  4 and  6 then led.CR - led.DR else 0 end)) Q02,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) between  7 and  9 then led.CR - led.DR else 0 end)) Q03,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) between 10 and 12 then led.CR - led.DR else 0 end)) Q04,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  1 then led.CR - led.DR else 0 end)) M01,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  2 then led.CR - led.DR else 0 end)) M02,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  3 then led.CR - led.DR else 0 end)) M03,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  4 then led.CR - led.DR else 0 end)) M04,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  5 then led.CR - led.DR else 0 end)) M05,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  6 then led.CR - led.DR else 0 end)) M06,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  7 then led.CR - led.DR else 0 end)) M07,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  8 then led.CR - led.DR else 0 end)) M08,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) =  9 then led.CR - led.DR else 0 end)) M09,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) = 10 then led.CR - led.DR else 0 end)) M10,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) = 11 then led.CR - led.DR else 0 end)) M11,
		convert(numeric(17,2), sum(case when datepart(month, [DATE]) = 12 then led.CR - led.DR else 0 end)) M12,
		' + @yr + ' [Year]
	from
		LOANS lon
			inner join
		MEMBERS mem ON
			lon.KBCI_NO = mem.KBCI_NO
			inner join
		LEDGER led ON
			lon.PN_NO = led.PN_NO
	where
		lon.LOAN_TYPE = ''PTL'' and
		mem.MEM_CODE = ''S'' and
		datepart(YYYY, led.[DATE]) = ''' + @yr + ''' and
		led.ACCT_CODE = ''INT'' and
		led.ACCT_TYPE != ''INI''
	group by
		lon.KBCI_NO,
		dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI)
		
end
')


GO

