USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_ReleasedLoans]    Script Date: 07/05/2009 07:57:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Report_Loans_ReleasedLoans]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Report_Loans_ReleasedLoans]
GO

USE [KBCI]
GO

/****** Object:  StoredProcedure [dbo].[Report_Loans_ReleasedLoans]    Script Date: 07/05/2009 07:57:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[Report_Loans_ReleasedLoans]
@dt1 date,
@dt2 date
AS

select
	@dt1 DT1,
	@dt2 DT2,
	lon.LOAN_TYPE,
	RANK() over (partition by lon.LOAN_TYPE order by mem.LNAME, mem.FNAME) as SEQ,
	dbo.func_FullName(mem.LNAME, mem.FNAME, mem.MI) as FULL_NAME,
	CONVERT(varchar, mem.B_DATE, 101) as BDATE,
	dbo.func_Age(mem.B_DATE, default) as AGE,
	CONVERT(varchar, lon.CHKNO_DATE, 101) as RELDATE,
	--CONVERT(varchar, lon.NDUE, 101) as DUEDATE,
	CONVERT
	(
		varchar,
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
				END,
		101
	) DUEDATE,
	dbo.func_Format241(lon.PN_NO) PN_NO,
	lon.CHKNO,
	REPLACE(lon.CHKNO_BANK, ' ', '') BANK,
	lon.PRINCIPAL as PRINCIPAL,
	lon.CHKNO_AMT as PROCEEDS,
	bal.BALANCE as BALANCE
from
	dbo.LOANS lon with(nolock)
		inner join
	dbo.MEMBERS mem with(nolock) on
		lon.KBCI_NO = mem.KBCI_NO
		cross apply
	dbo.func_Balance(lon.PN_NO, default, default) bal
where
	lon.CHKNO_DATE between @dt1 and @dt2
order by
	lon.LOAN_TYPE, mem.LNAME, mem.FNAME



GO