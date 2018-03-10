
/******* DROP OBJECTS *******/

	declare @sql nvarchar(1000)
	declare @count smallint = 2
	declare @object varchar(100)
	declare @type char(2)

	while (@count > 0)
	begin
		set @object = 
		(
			case @count
				when 2 then 'procedure'
				when 1 then 'function'
				end
		)
		
		set @type = 
		(
			case @count
				when 2 then 'P'
				when 1 then 'FN'
				end
		)

		declare sp cursor for
		select
			'drop ' + @object + ' ' + name	
		from
			sys.sysobjects
		where
			type = @type
		order by
			name

		open sp

		fetch
			sp
		into
			@sql
			
		while @@fetch_status = 0
		begin
			exec sp_executesql @sql
			
			fetch
				sp
			into
				@sql
		end

		close sp
		deallocate sp
		
		set @count = @count - 1
	end

	go

/******* INCREASE PRECISION *******/

	alter table RUNUP1 alter column LN_AMNT numeric(16, 4)
	alter table RUNUP1 alter column FLN_AMNT numeric(16, 4)
	alter table RUNUP1 alter column INT_AMNT numeric(16, 4)
	alter table RUNUP1 alter column FINT_AMNT numeric(16, 4)
	alter table RRUNUP1 alter column LN_AMNT numeric(16, 4)
	alter table RRUNUP1 alter column FLN_AMNT numeric(16, 4)
	alter table RRUNUP1 alter column INT_AMNT numeric(16, 4)
	alter table RRUNUP1 alter column FINT_AMNT numeric(16, 4)
	alter table DAILYTRN alter column BEGBAL numeric(13, 4)
	alter table DAILYTRN alter column DR numeric(11, 4)
	alter table DAILYTRN alter column CR numeric(11, 4)
	alter table DAILYTRN alter column ENDBAL numeric(13, 4)
	alter table DAILYTRN alter column INT numeric(11, 4)
	alter table DAILYTRN alter column INTCR numeric(11, 4)
	alter table DAILYTRN alter column INTDR numeric(11, 4)
	alter table DAILYTRN alter column PEN numeric(11, 4)
	alter table DAILYTRN alter column SC numeric(13, 4)
	alter table DAILYTRN alter column LRI numeric(13, 4)
	alter table DAILYTRN alter column FD numeric(13, 4)
	alter table DAILYTRN alter column SD numeric(13, 4)
	alter table LEDGEREV alter column BEGBAL numeric(13, 4)
	alter table LEDGEREV alter column DR numeric(11, 4)
	alter table LEDGEREV alter column CR numeric(11, 4)
	alter table LEDGEREV alter column ENDBAL numeric(13, 4)
	alter table DAILYREV alter column BEGBAL numeric(13, 4)
	alter table DAILYREV alter column DR numeric(11, 4)
	alter table DAILYREV alter column CR numeric(11, 4)
	alter table DAILYREV alter column ENDBAL numeric(13, 4)
	alter table DAILYREV alter column INT numeric(11, 4)
	alter table DAILYREV alter column PEN numeric(11, 4)
	alter table DAILYREV alter column SC numeric(13, 4)
	alter table DAILYREV alter column LRI numeric(13, 4)
	alter table DAILYREV alter column FD numeric(13, 4)
	alter table DAILYREV alter column SD numeric(13, 4)
	alter table PAYHIST alter column PAYAMT numeric(16, 4)
	alter table EXTKBCO alter column AMT7C numeric(16, 4)
	alter table XLEDGER alter column BEGBAL numeric(13, 4)
	alter table XLEDGER alter column DR numeric(11, 4)
	alter table XLEDGER alter column CR numeric(11, 4)
	alter table XLEDGER alter column ENDBAL numeric(13, 4)
	alter table XPMINT alter column INTAMT numeric(16, 4)
	alter table ADVICE alter column AMOUNT numeric(16, 4)
	alter table MO_DEDN alter column AMORT_PRI numeric(13, 4)
	alter table MO_DEDN alter column AMORT_INT numeric(13, 4)
	alter table MO_DEDN alter column DEDUCTION numeric(13, 4)
	alter table MO_DEDN alter column PRINCIPAL numeric(13, 4)
	alter table MO_DEDN alter column INTEREST numeric(13, 4)
	alter table MO_DEDN alter column ARREARS numeric(13, 4)
	alter table MO_DEDN alter column ADVANCE numeric(13, 4)
	alter table MO_DEDN alter column ARR_PRI numeric(13, 4)
	alter table MO_DEDN alter column ARR_INT numeric(13, 4)
	alter table MO_DEDNO alter column AMORT_PRI numeric(13, 4)
	alter table MO_DEDNO alter column AMORT_INT numeric(13, 4)
	alter table MO_DEDNO alter column DEDUCTION numeric(13, 4)
	alter table MO_DEDNO alter column PRINCIPAL numeric(13, 4)
	alter table MO_DEDNO alter column INTEREST numeric(13, 4)
	alter table MO_DEDNO alter column ARREARS numeric(13, 4)
	alter table MO_DEDNO alter column ADVANCE numeric(13, 4)
	alter table MO_DEDNO alter column ARR_PRI numeric(13, 4)
	alter table MO_DEDNO alter column ARR_INT numeric(13, 4)
	alter table LRIDUE alter column LOAN_BAL numeric(16, 4)
	alter table LRIDUE alter column LRI_DUE_C numeric(16, 4)
	alter table LRIDUE alter column LRI_DUE_P numeric(16, 4)
	alter table LRIDUE alter column LRI_DUE_Y numeric(16, 4)
	alter table MO_DEDNH alter column AMORT_PRI numeric(13, 4)
	alter table MO_DEDNH alter column AMORT_INT numeric(13, 4)
	alter table MO_DEDNH alter column DEDUCTION numeric(13, 4)
	alter table MO_DEDNH alter column PRINCIPAL numeric(13, 4)
	alter table MO_DEDNH alter column INTEREST numeric(13, 4)
	alter table MO_DEDNH alter column ARREARS numeric(13, 4)
	alter table MO_DEDNH alter column ADVANCE numeric(13, 4)
	alter table MO_DEDNH alter column ARR_PRI numeric(13, 4)
	alter table MO_DEDNH alter column ARR_INT numeric(13, 4)
	alter table EXTKBC alter column AMT7C numeric(16, 4)
	alter table UPLOAD alter column AMOUNT numeric(12, 4)
	alter table UPLOAD alter column MO_DEDN numeric(12, 4)
	alter table ADVANCE alter column AMOUNT numeric(13, 4)
	alter table MO_DEDNP alter column AMORT_PRI numeric(13, 4)
	alter table MO_DEDNP alter column AMORT_INT numeric(13, 4)
	alter table MO_DEDNP alter column DEDUCTION numeric(13, 4)
	alter table MO_DEDNP alter column PRINCIPAL numeric(13, 4)
	alter table MO_DEDNP alter column INTEREST numeric(13, 4)
	alter table MO_DEDNP alter column ARREARS numeric(13, 4)
	alter table MO_DEDNP alter column ADVANCE numeric(13, 4)
	alter table MO_DEDNP alter column ARR_PRI numeric(13, 4)
	alter table MO_DEDNP alter column ARR_INT numeric(13, 4)
	alter table S_ACCNT alter column INT numeric(17, 4)
	alter table S_ACCNT alter column PEN numeric(17, 4)
	alter table S_ACCNT alter column SAV numeric(17, 4)
	alter table S_ACCNT alter column FIX numeric(17, 4)
	alter table S_ACCNT alter column LRI numeric(17, 4)
	alter table INTER alter column ADB numeric(17, 4)
	alter table INTER alter column MONTH1 numeric(12, 4)
	alter table INTER alter column MONTH2 numeric(12, 4)
	alter table INTER alter column MONTH3 numeric(12, 4)
	alter table INTER alter column QTD numeric(17, 4)
	alter table EXTRACT alter column AMOUNT numeric(11, 4)
	alter table EXTRACT alter column EXBBAL numeric(16, 4)
	alter table EXTRACT alter column EXEBAL numeric(16, 4)
	alter table UPLOADO alter column AMOUNT numeric(12, 4)
	alter table UPLOADO alter column MO_DEDN numeric(12, 4)
	alter table LOANS alter column CHKNO_AMT numeric(12, 4)
	alter table LOANS alter column AMORT_AMT numeric(12, 4)
	alter table LOANS alter column PRINCIPAL numeric(12, 4)
	alter table LOANS alter column AFT_INTE numeric(12, 4)
	alter table LOANS alter column ACCU_PAYP numeric(12, 4)
	alter table LOANS alter column YTD_I numeric(11, 4)
	alter table LOANS alter column ADVANCE numeric(12, 4)
	alter table LOANS alter column LRI_DUE numeric(12, 4)
	alter table [PARAM] alter column [MAX] numeric(11, 4)
	alter table [PARAM] alter column [MIN] numeric(11, 4)
	alter table SLOANS alter column AMORT_AMT numeric(12, 4)
	alter table SLOANS alter column PRINCIPAL numeric(12, 4)
	alter table SLOANS alter column ACCU_PAYP numeric(12, 4)
	alter table SLOANS alter column ARREAR_I numeric(12, 4)
	alter table SLOANS alter column ARREAR_P numeric(12, 4)
	alter table SLOANS alter column ARREAR_OTH numeric(12, 4)
	alter table SLOANS alter column P_BAL numeric(12, 4)
	alter table SLOANS alter column I_BAL numeric(12, 4)
	alter table SLOANS alter column O_BAL numeric(12, 4)
	alter table SLOANS alter column ADVANCE numeric(12, 4)
	alter table SLOANS alter column amount numeric(12, 4)
	alter table SLOANS alter column xbprin numeric(12, 4)
	alter table SLOANS alter column xint numeric(12, 4)
	alter table PRUNUP1 alter column LN_AMNT numeric(16, 4)
	alter table PRUNUP1 alter column FLN_AMNT numeric(16, 4)
	alter table PRUNUP1 alter column INT_AMNT numeric(16, 4)
	alter table PRUNUP1 alter column FINT_AMNT numeric(16, 4)
	alter table MEMBERS alter column SAL_BAS numeric(11, 4)
	alter table MEMBERS alter column SAL_ALL numeric(11, 4)
	alter table MEMBERS alter column OTH_INC numeric(12, 4)
	alter table MEMBERS alter column AP_AMOUNT numeric(12, 4)
	alter table MEMBERS alter column AR_AMOUNT numeric(12, 4)
	alter table MEMBERS alter column FD_AMOUNT numeric(13, 4)
	alter table MEMBERS alter column SD_AMOUNT numeric(13, 4)
	alter table MEMBERS alter column TD_AMOUNT numeric(13, 4)
	alter table MEMBERS alter column OTH_AMOUNT numeric(13, 4)
	alter table MEMBERS alter column YTD_DIVAMT numeric(11, 4)
	alter table MEMBERS alter column YTD_LRI numeric(12, 4)
	alter table MEMBERS alter column REM_VALUE numeric(12, 4)
	alter table MEMBERS alter column SP_SALARY numeric(11, 4)
	alter table MEMBERS alter column APRUN_AMT numeric(12, 4)
	alter table MEMBERS alter column ARRUN_AMT numeric(12, 4)
	alter table MEMBERS alter column RUN_AMT numeric(14, 4)
	alter table ODEDBAL alter column ODR numeric(11, 4)
	alter table ODEDBAL alter column OCR numeric(11, 4)
	alter table LEDGER alter column BEGBAL numeric(13, 4)
	alter table LEDGER alter column ENDBAL numeric(13, 4)

	go

/******* RESIZING *******/

	if 30 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'SDTRAN' and sc.[name] = 'CHKNUM'), 30)
		alter table dbo.SDTRAN
		alter column CHKNUM varchar(30);

	if 10 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'EXTRACT' and sc.[name] = 'PN_NO'), 10)
		alter table dbo.EXTRACT
		alter column PN_NO varchar(10);
		
	if 10 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'EXTRACT' and sc.[name] = 'KBCI_NO'), 10)
		alter table dbo.EXTRACT
		alter column KBCI_NO varchar(10);

	if 40 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'FD' and sc.[name] = 'RMK'), 40)
		alter table dbo.FD
		alter column RMK varchar(40);

	if 40 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'SD' and sc.[name] = 'RMK'), 40)
		alter table dbo.SD
		alter column RMK varchar(40);

	if 50 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'DAILYREV' and sc.[name] = 'NAME'), 50)
		alter table dbo.DAILYREV
		alter column NAME varchar(50);
		
	if 50 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'USER' and sc.[name] = 'USERPASS'), 50)
		alter table dbo.[USER]
		alter column USERPASS varchar(50);

	if 50 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'DAILYTRN' and sc.[name] = 'NAME'), 50)
		alter table dbo.DAILYTRN
		alter column NAME varchar(50);

	if 3 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LEDGER' and sc.[name] = 'DOX_TYPE'), 3)
		alter table dbo.LEDGER
		alter column DOX_TYPE varchar(3);

	if 12 != isnull((select top 1 sc.prec from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LEDGER' and sc.[name] = 'DR'), 12)
		alter table dbo.LEDGER
		alter column DR numeric(12, 4);
		
	if 12 != isnull((select top 1 sc.prec from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LEDGER' and sc.[name] = 'CR'), 12)
		alter table dbo.LEDGER
		alter column CR numeric(12, 4);	

	if 3 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'CTRL' and sc.[name] = 'VOUCHER'), 3)
		alter table dbo.CTRL
		alter column VOUCHER numeric(3);

	if 12 != isnull((select top 1 sc.prec from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LOANS' and sc.[name] = 'ARREAR_P'), 12)
		alter table LOANS
		alter column ARREAR_P numeric(12, 4)

	if 12 != isnull((select top 1 sc.prec from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LOANS' and sc.[name] = 'ARREAR_I'), 12)
		alter table LOANS
		alter column ARREAR_I numeric(12, 4)

	if 12 != isnull((select top 1 sc.prec from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LOANS' and sc.[name] = 'ARREAR_OTH'), 12)
		alter table LOANS
		alter column ARREAR_OTH numeric(12, 4)

	if 12 != isnull((select top 1 sc.prec from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LOANS' and sc.[name] = 'P_BAL'), 12)
		alter table LOANS
		alter column P_BAL numeric(12, 4)

	if 12 != isnull((select top 1 sc.prec from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LOANS' and sc.[name] = 'I_BAL'), 12)
		alter table LOANS
		alter column I_BAL numeric(12, 4)

	if 12 != isnull((select top 1 sc.prec from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LOANS' and sc.[name] = 'O_BAL'), 12)
		alter table LOANS
		alter column O_BAL numeric(12, 4)

	if 'bit' = (select top 1 st.name from sys.sysobjects so inner join sys.syscolumns sc on sc.id = so.id inner join sys.systypes st on st.xtype = sc.xtype where so.name = 'CORRLRI' and sc.name = 'CHKNO_DATE')
		alter table dbo.CORRLRI
		alter column CHKNO_DATE datetime;

	if 9 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'MO_DEDNH' and sc.[name] = 'PN_NO'), 9)
	begin
		IF EXISTS (SELECT 'X' FROM SYSINDEXES WHERE NAME = 'IX_MO_DEDNH') DROP INDEX MO_DEDNH.IX_MO_DEDNH;
		
		alter table dbo.MO_DEDNH
		alter column PN_NO varchar(9);
		
		if not exists (select 'X' from sysindexes where name = 'IX_MO_DEDNH')
			create index IX_MO_DEDNH
			on DBO.[MO_DEDNH] (EMPNO, KBCI_NO, PN_NO);
	end;

	if 9 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'MO_DEDNH' and sc.[name] = 'PN_NO'), 9)
	begin
		IF EXISTS (SELECT 'X' FROM SYSINDEXES WHERE NAME = 'IX_MO_DEDNH') DROP INDEX MO_DEDNH.IX_MO_DEDNH;
		
		alter table dbo.MO_DEDN
		alter column PN_NO varchar(9);
		
		if not exists (select 'X' from sysindexes where name = 'IX_MO_DEDNH')
			create index IX_MO_DEDNH
			on DBO.[MO_DEDNH] (EMPNO, KBCI_NO, PN_NO);
	end;

	if 40 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LNHOLD' and sc.[name] = 'HOLDRMKS'), 40)
	begin
		alter table dbo.LNHOLD
		alter column HOLDRMKS varchar(40);
	end;

	if 11 != isnull((select top 1 sc.[xprec] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'ADVANCE' and sc.[name] = 'AMOUNT'), 11)
	begin
		alter table dbo.ADVANCE
		alter column AMOUNT numeric(11, 2)
	end;

	if 45 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'MO_DEDN' and sc.[name] = 'NAME'), 45)
	begin
		alter table dbo.MO_DEDN
		alter column NAME varchar(45);
	end;

	if 45 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'MO_DEDNH' and sc.[name] = 'NAME'), 45)
	begin
		alter table dbo.MO_DEDNH
		alter column NAME varchar(45);
	end;

	if 3 != isnull((select top 1 sc.[length] from sys.sysobjects so inner join sys.syscolumns sc on sc.[id] = so.[id] where so.[name] = 'LEDGEREV' and sc.[name] = 'DOX_TYPE'), 3)
	begin
		alter table dbo.LEDGEREV
		alter column DOX_TYPE varchar(3);
	end;

	go

/******* DIVREF2 *******/

	if not exists (select 'X' from sysobjects where name = 'DIVREF2' and type = 'U')
		create table DBO.[DIVREF2] (
			[KBCI_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[LNAME] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[FNAME] varchar(20) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[MI] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[FEBTC_SA] varchar(10) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[DIVIDEND] numeric(11, 2),
			[REFUND] numeric(11, 2),
			[TOTAL] numeric(11, 2),
			[DEDUCTIONS] numeric(11, 2),
			[GTOTAL] numeric(11, 2),
			[FOR_LRI] numeric(11, 2),
		)

	go

/******* EXTKBCO *******/

	if not exists (select 'X' from sys.sysobjects where name = 'EXTKBCO')
	begin
		create table DBO.[EXTKBCO]
		(
			[EMPNO1] numeric(7),
			[ACTYPE] numeric(2),
			[ACTCD1] numeric(3),
			[ACTCD2] numeric(4),
			[DATE7] date,
			[AMT7C] numeric(14, 2),
			[CODE5] numeric(2)
		);
	end;

	go

/******* UPLOADO *******/

	if exists (select 'X' from sys.sysobjects where name = 'UPLOADO')
	begin
		drop table dbo.[UPLOADO];
	end

	if not exists (select 'X' from sys.sysobjects where name = 'UPLOADO')
	begin
		create table dbo.[UPLOADO]
		(
			[UPLOADO_ID] bigint identity(1,1) PRIMARY KEY NOT NULL,
			[BATCH] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[EMP_NUM] varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[ACCT_CODE] varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[AMOUNT] numeric(10, 2),
			[DATE_GRANT] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[DATE_DUE] varchar(8) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[MO_DEDN] numeric(10, 2),
			[TRANS_CODE] varchar(1) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[CODE5] numeric(2)
		);
	end;

	go

/******* LOAN_TYPE *******/
	
	if not exists (select 'X' from sys.sysobjects where name = 'LOAN_TYPE')
	begin
		create table dbo.[LOAN_TYPE]
		(
			[LOAN_TYPE_ID] bigint identity(1,1) PRIMARY KEY NOT NULL,
			[LOAN_TYPE] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS UNIQUE,
			[LOAN_DESC] varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
			[CODE5] varchar(4) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[MBAT] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS
		);
	end;

	go

	if not exists (select 'x' from dbo.[LOAN_TYPE])
	begin
		insert	dbo.LOAN_TYPE 
		(
			LOAN_TYPE, 
			CODE5, 
			MBAT, 
			LOAN_DESC
		)
		values
			('APL',	'7605',	'001',	'Appliance Loan'),
			('CML',	'7601',	'003',	'Calamity Loan'),
			('EDL',	'7615',	'002',	'Educational Loan'),
			('EML',	'7620',	'003',	'Emergency Loan'),
			('MPL',	'7606',	NULL,	'Multi-Purpose Loan'),
			('RGL',	'7630',	'004',	'Regular Loan'),
			('SPL',	'7640',	'005',	'Special Loan'),
			('RSL',	'7655',	'006',	'Restructured Loan'),
			('PTL',	NULL,	NULL,	'Provident Loan'),
			('STL',	NULL,	NULL,	'Short Term Loan')
		
	end

	if (select top 1 CODE5 from dbo.LOAN_TYPE where LOAN_TYPE = 'MPL') is null
	begin
		update	dbo.LOAN_TYPE
		set		CODE5 = '7606'
		where	LOAN_TYPE = 'MPL'
	end
	
	go

/******* OTHER_TYPE *******/

	if exists (select 'X' from sys.tables where name = 'OTHER_TYPE')
	begin
		drop table dbo.[OTHER_TYPE]
		
		create table dbo.[OTHER_TYPE]
		(
			[OTHER_TYPE_ID] bigint identity(1,1) PRIMARY KEY NOT NULL,
			[OTHER_TYPE] varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS UNIQUE,
			[OTHER_DESC] varchar(30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
			[CODE5] varchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS,
			[DEDN_SORT] int
		)
	end
	
	go
	
	insert dbo.OTHER_TYPE 
	(
		CODE5,
		OTHER_TYPE,
		OTHER_DESC,
		DEDN_SORT
	)
	values	
		('007625', 'FIX', 'Fixed Deposit', 20),
		('007635', 'SAV', 'Savings Deposit', 30),
		('221150', 'IRG', 'RGL Interest', null),
		('221185', 'IED', 'EDL Interest', null),
		('221140', 'IAP', 'APL Interest', null),
		('221070', 'IEM', 'EML Interest', null),
		('221080', 'PEM', 'EML Penalty', null),
		('007660', 'COC', 'Cocolife', null),
		('221120', 'SER', 'Service Chg.', null),
		('007600', 'PAM', 'Insurance (Philam)', 50),
		('221090', 'PER', 'RGL Penalty', null),
		('007650', 'AIU', 'Insurance (PGA)', 40),
		('007610', ' MA', 'Mutual Aid', 10)
	
	go

/******* PARAM *******/

	if exists (select 'x' from sys.sysobjects where name = 'UQ_PARAM_LOANS_TYPE')
	begin
		alter table dbo.[PARAM]
		drop constraint UQ_PARAM_LOANS_TYPE;
	end

	if exists (select top 1 'x' from sys.sysobjects so inner join sys.syscolumns sc on sc.id = so.id where so.name = 'PARAM' and sc.name = 'LOAN_DESC')
	begin
		alter table dbo.[PARAM]
		drop [LOAN_DESC]
	end
	
	go
	
/******* MPL *******/

	if not exists (select top 1 'X' from dbo.[PARAM] where [LOAN_TYPE] = 'MPL')
	begin		
		insert	dbo.[PARAM]
				(
				[LOAN_TYPE],
				[TERM],
				[FREQ],
				[RATE],
				[MAX],
				[MIN],
				[ADD_DATE],
				[CHG_DATE],
				[USER]
				)
		select	'MPL' as 'LOAN_TYPE',
				[TERM],
				[FREQ],
				[RATE],
				[MAX],
				[MIN],
				[ADD_DATE],
				[CHG_DATE],
				[USER]
		from	dbo.[PARAM]
		where	[LOAN_TYPE] = 'APL';
	end;
	
	if not exists (select * from dbo.[RUNUP1] where [LOAN_TYP] = 'MPL')
	begin
		insert	dbo.[RUNUP1]
		(
				[BNAME],
				[KBCI_NO],
				[PN_NO],
				[LOAN_TYP],
				[LN_AMNT],
				[FLN_AMNT],
				[INT_AMNT],
				[FINT_AMNT],
				[DATE]
		)
		values
		(
				NULL,
				NULL,
				NULL,
				'MPL',
				0,
				0,
				0,
				0,
				(select SYSDATE from dbo.[CTRL])
		)
	end

	if not exists (select * from dbo.[RRUNUP1] where [LOAN_TYP] = 'MPL')
	begin
		insert	dbo.[RRUNUP1]
		(
				[BNAME],
				[KBCI_NO],
				[PN_NO],
				[LOAN_TYP],
				[LN_AMNT],
				[FLN_AMNT],
				[INT_AMNT],
				[FINT_AMNT],
				[DATE]
		)
		values
		(
				NULL,
				NULL,
				NULL,
				'MPL',
				0,
				0,
				0,
				0,
				(select SYSDATE from dbo.[CTRL])
		)
	end
	
	go

/******* FAL *******/

	if not exists (select * from dbo.[LOAN_TYPE] where [LOAN_TYPE] = 'FAL')
	begin
		insert	dbo.LOAN_TYPE 
		(
				LOAN_TYPE,
				CODE5,
				LOAN_DESC
		)
		values
		(
				'FAL',
				'7607',
				'Financial Assistance Loan'
		)
	end

	if not exists (select * from dbo.[RUNUP1] where [LOAN_TYP] = 'FAL')
	begin
		insert	dbo.[RUNUP1]
		(
				[BNAME],
				[KBCI_NO],
				[PN_NO],
				[LOAN_TYP],
				[LN_AMNT],
				[FLN_AMNT],
				[INT_AMNT],
				[FINT_AMNT],
				[DATE]
		)
		values
		(
				NULL,
				NULL,
				NULL,
				'FAL',
				0,
				0,
				0,
				0,
				(select SYSDATE from dbo.[CTRL])
		)
	end

	if not exists (select * from dbo.[RRUNUP1] where [LOAN_TYP] = 'FAL')
	begin
		insert	dbo.[RRUNUP1]
		(
				[BNAME],
				[KBCI_NO],
				[PN_NO],
				[LOAN_TYP],
				[LN_AMNT],
				[FLN_AMNT],
				[INT_AMNT],
				[FINT_AMNT],
				[DATE]
		)
		values
		(
				NULL,
				NULL,
				NULL,
				'FAL',
				0,
				0,
				0,
				0,
				(select SYSDATE from dbo.[CTRL])
		)
	end
	
	go

/******* SML *******/

	if not exists (select * from dbo.[LOAN_TYPE] where [LOAN_TYPE] = 'SML')
	begin
		insert	dbo.LOAN_TYPE 
		(
				LOAN_TYPE,
				CODE5,
				LOAN_DESC
		)
		values
		(
				'SML',
				'7604',
				'SM Appliance Loan'
		)
	end

	if not exists (select * from dbo.[RUNUP1] where [LOAN_TYP] = 'SML')
	begin
		insert	dbo.[RUNUP1]
		(
				[BNAME],
				[KBCI_NO],
				[PN_NO],
				[LOAN_TYP],
				[LN_AMNT],
				[FLN_AMNT],
				[INT_AMNT],
				[FINT_AMNT],
				[DATE]
		)
		values
		(
				NULL,
				NULL,
				NULL,
				'SML',
				0,
				0,
				0,
				0,
				(select SYSDATE from dbo.[CTRL])
		)
	end

	if not exists (select * from dbo.[RRUNUP1] where [LOAN_TYP] = 'SML')
	begin
		insert	dbo.[RRUNUP1]
		(
				[BNAME],
				[KBCI_NO],
				[PN_NO],
				[LOAN_TYP],
				[LN_AMNT],
				[FLN_AMNT],
				[INT_AMNT],
				[FINT_AMNT],
				[DATE]
		)
		values
		(
				NULL,
				NULL,
				NULL,
				'SML',
				0,
				0,
				0,
				0,
				(select SYSDATE from dbo.[CTRL])
		)
	end
	
	go

/******* S_ACCNT *******/

	if
		exists (select 'x' from sys.sysobjects where name = 'S_ACCNT' and xtype = 'U') and
		not exists (select 'x' from sys.sysobjects where name = 'S_ACCNT_BAK' and xtype = 'U') and
		not exists (select 'x' from sys.sysobjects where name = 'S_ACCNT_LOANS' and xtype = 'U')
	begin
		exec
		('
			select
				*
			into
				S_ACCNT_BAK
			from
				S_ACCNT
		')

		create table S_ACCNT_LOANS
		(
			KBCI_NO varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS,
			LOAN_TYPE varchar(3) COLLATE SQL_Latin1_General_CP1_CI_AS,
			PD bit,
			BALANCE numeric(14, 4)
		)
		
		exec
		('
			create index IX_S_ACCNT_LOANS
			on dbo.S_ACCNT_LOANS (KBCI_NO, LOAN_TYPE)
		')
		
		exec
		('
			insert into S_ACCNT_LOANS
			(
				KBCI_NO,
				LOAN_TYPE,
				BALANCE,
				PD
			)
			select
				t.KBCI_NO,
				t.LOAN_TYPE,
				t.BALANCE,
				t.PD
			from
				S_ACCNT_BAK
			cross apply
			(
				values
				(KBCI_NO, ''APL'', APL, PDAPL),
				(KBCI_NO, ''CML'', CML, PDCML),
				(KBCI_NO, ''EDL'', EDL, PDEDL),
				(KBCI_NO, ''EML'', EML, PDEML),
				(KBCI_NO, ''FAL'', FAL, PDFAL),
				(KBCI_NO, ''MPL'', MPL, PDMPL),
				(KBCI_NO, ''RGL'', RGL, PDRGL),
				(KBCI_NO, ''RSL'', RSL, PDRSL),
				(KBCI_NO, ''SML'', SML, PDSML),
				(KBCI_NO, ''SPL'', SPL, PDSPL),
				(KBCI_NO, ''STL'', STL, PDSTL),
				(KBCI_NO, ''PTL'', PTL, PDPTL)
			) t
				(KBCI_NO, LOAN_TYPE, BALANCE, PD)
			where
				isnull(t.BALANCE, 0) > 0
		')

		exec
		('
			ALTER TABLE S_ACCNT DROP COLUMN APL
			ALTER TABLE S_ACCNT DROP COLUMN CML
			ALTER TABLE S_ACCNT DROP COLUMN EDL
			ALTER TABLE S_ACCNT DROP COLUMN EML
			ALTER TABLE S_ACCNT DROP COLUMN FAL
			ALTER TABLE S_ACCNT DROP COLUMN MPL
			ALTER TABLE S_ACCNT DROP COLUMN RGL
			ALTER TABLE S_ACCNT DROP COLUMN RSL
			ALTER TABLE S_ACCNT DROP COLUMN SPL
			ALTER TABLE S_ACCNT DROP COLUMN STL
			ALTER TABLE S_ACCNT DROP COLUMN PTL
			ALTER TABLE S_ACCNT DROP COLUMN PDAPL
			ALTER TABLE S_ACCNT DROP COLUMN PDCML
			ALTER TABLE S_ACCNT DROP COLUMN PDEDL
			ALTER TABLE S_ACCNT DROP COLUMN PDEML
			ALTER TABLE S_ACCNT DROP COLUMN PDFAL
			ALTER TABLE S_ACCNT DROP COLUMN PDMPL
			ALTER TABLE S_ACCNT DROP COLUMN PDRGL
			ALTER TABLE S_ACCNT DROP COLUMN PDRSL
			ALTER TABLE S_ACCNT DROP COLUMN PDSPL
			ALTER TABLE S_ACCNT DROP COLUMN PDSTL
			ALTER TABLE S_ACCNT DROP COLUMN PDPTL
		')
	end
	
	go

/******* CTRL_S *******/

	if not exists(select * from sys.columns where Name = N'DormancyCharge' and Object_ID = Object_ID(N'CTRL_S'))
	BEGIN
		ALTER TABLE CTRL_S
		ADD DormancyCharge money null
	END

/******* LOAN_ARREARS *******/

	if not exists (select 'X' from sys.sysobjects where name = 'LOAN_ARREARS')
	begin
		create table dbo.LOAN_ARREARS
		(
			[LOAN_ARREARS_ID] bigint identity(1,1) PRIMARY KEY NOT NULL,
			[PN_NO] varchar(7) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
			[ARREAR_AS] date,
			[ARREAR_P] numeric(12, 4) DEFAULT 0,
			[ARREAR_I] numeric(12, 4) DEFAULT 0,
			[ARREAR_OTH] numeric(12, 4) DEFAULT 0,
			[SYSDATE] date
		)
		
		create index
			IX_LOAN_ARREARS
		on
			LOAN_ARREARS (PN_NO, ARREAR_AS)
		include
			(SYSDATE)
	end

/******* MO_DEDN_SUM *******/

	if not exists (select 'X' from sys.tables where name = 'MO_DEDN_DETL')
	begin
		create table dbo.MO_DEDN_DETL
		(
			[MO_DEDN_DETL_ID] int identity(1,1) PRIMARY KEY,
			[EMP_NO] varchar(6),
			[CODE5] varchar(4),
			[AMT] numeric(12, 4) DEFAULT 0,
			[PAY_AMT] numeric(12, 4) DEFAULT 0,
			[PAY_DATE] date
		)	
	end
	
	if not exists (select 'X' from sys.tables where name = 'MO_DEDN_PAID')
	begin
		create table dbo.MO_DEDN_PAID
		(
			[MO_DEDN_PAID_ID] int identity(1,1) PRIMARY KEY,
			[EMP_NO] varchar(6),
			[PAY_AMT] numeric(12, 4) DEFAULT 0
		)	
	end
	
	if not exists (select 'X' from sys.tables where name = 'MO_DEDN_HIST')
	begin
		create table dbo.MO_DEDN_HIST
		(
			[MO_DEDN_HIST_ID] int identity(1,1) PRIMARY KEY,
			[EMP_NO] varchar(6),
			[CODE5] varchar(4),
			[AMT] numeric(12, 4) DEFAULT 0,
			[PAY_AMT] numeric(12, 4) DEFAULT 0,
			[PAY_DATE] date,
			[OFF_CYCLE] bit
		)	
	end
	
	if not exists (select 'X' from sys.tables where name = 'MO_DEDN_EXCESS')
	begin
		create table dbo.MO_DEDN_EXCESS
		(
			[MO_DEDN_EXCESS_ID] int identity(1,1) PRIMARY KEY,
			[EMP_NO] varchar(6),
			[PAY_AMT] numeric(12, 4) DEFAULT 0,
			[PAY_DATE] date
		)	
	end