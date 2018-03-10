Imports System.Collections.Generic
Imports System.Configuration
Imports System.IO
Imports System.Text
Imports Excel = Microsoft.Office.Interop.Excel
Imports Loan.Application.Infrastructure.Enumerations.Sql
Imports Loan.Application.Infrastructure.Enumerations.Popups

Namespace Business.Rules
    Public Class Payroll
        Structure Deduction
            Dim sEmpNo As String
            Dim sAcctCode As String
            Dim dAmount As Decimal

            Public Sub New(ByVal empNo As String, ByVal acctCode As String, ByVal amount As Decimal)
                sEmpNo = empNo
                sAcctCode = acctCode
                dAmount = amount
            End Sub
        End Structure

        Public Shared Function IsPayrollGEnerated() As Boolean
            Dim popup As New Business.Popups.DateRange
            Dim temp As Date

            temp = DateAdd(DateInterval.Month, -1, sysdate)
            popup.DateFrom = New Date(temp.Year, temp.Month, 16)
            temp = sysdate
            popup.DateTo = New Date(temp.Year, temp.Month, 15)

            If Common.OpenPopup(popup) <> PopupResponses.Ok Then Exit Function

            Dim db As New Data.Database()
            Try
                db.BeginTransaction()
                db.ExecuteNonQuerySp(NonQuery.Do_Payroll_Generate, popup.DateFrom, popup.DateTo)
                db.CommitTransaction()
            Catch ex As Exception
                db.RollbackTransaction()
                Common.PopupError(ex.Message)
                Return False
            End Try
            Common.PopupInformation("Done. Please check Advice report.")
            Return True
        End Function

        Public Shared Function IsAsOfPayrollGenerated() As Boolean
            Dim popup As New Business.Popups.InputDate
            popup.Date = New Date(sysdate.Year, sysdate.Month, 15)

            If Common.OpenPopup(popup) <> PopupResponses.Ok Then Exit Function

            Dim db As New Data.Database()
            Try
                db.BeginTransaction()
                db.ExecuteNonQuerySp(NonQuery.Do_AsOf_Payroll_Generate, popup.Date)
                db.CommitTransaction()
            Catch ex As Exception
                db.RollbackTransaction()
                Common.PopupError(ex.Message)
                Return False
            End Try
            Common.PopupInformation("Done. Please check Advice report.")
            Return True
        End Function

        Public Shared Function IsAdviceGenerated(ByVal fbd As Windows.Forms.FolderBrowserDialog) As Boolean
            Dim deductions As New List(Of Deduction)
            Dim sAdvice As String = fbd.SelectedPath & "\ADVICE.xls"
            Dim sAdditional As String = fbd.SelectedPath & "\ADDITIONAL.xls"
            Dim sAdditionalMa As String = fbd.SelectedPath & "\ADDITIONAL.MA.xls"
            Dim bAdditional As Boolean = True
            Dim bAdditionalMa As Boolean = True

            If Not File.Exists(sAdvice) Then
                Common.PopupExclamation("Advice file does not exist.")
                Return False
            ElseIf Not File.Exists(sAdditional) Then
                If Common.PopupQuestion(String.Format("{0} not found. Proceed?", sAdditional)) = DialogResult.No Then
                    Return False
                Else
                    bAdditional = False
                End If
            End If

            bAdditionalMa = Common.PopupQuestion("Include mutual aid?") = DialogResult.Yes
            If bAdditionalMa AndAlso Not File.Exists(sAdditionalMa) Then
                If Common.PopupQuestion(String.Format("{0} not found. Proceed?", sAdditionalMa)) = DialogResult.No Then
                    Return False
                Else
                    bAdditional = False
                End If
            End If

            Common.PopupInformation("Initializing process. Press any key to continue.")
            GetDeductions(deductions, sAdvice, 1, 2, 5)
            If bAdditional Then GetDeductions(deductions, sAdditional, 1, 3, 4)
            If bAdditionalMa Then GetDeductions(deductions, sAdditionalMa, 1, 3, 4)
            InsertDeductions(deductions)
            CreateDeductionFile(fbd)
            CreateBreakdownFile(fbd)
            Return True
        End Function

        Public Shared Function IsExtkbcGenerated(ByVal fbd As Windows.Forms.FolderBrowserDialog, ByVal opd As Windows.Forms.FileDialog, Optional ByVal bConsolidateExtkbc As Boolean = False) As Boolean
            Dim collected As New List(Of Deduction)
            Dim deductions As New List(Of Deduction)
            Dim sBreakdown As String
            Dim sCollected As String

            If Not bConsolidateExtkbc Then
                opd.FileName = "ADVICE.BREAKDOWN.xls"
                opd.Filter = "Excel Workbook (*.xls)|*.xls"
                opd.Title = "Select breakdown file."

                If opd.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Return False
                If Not File.Exists(opd.FileName) Then
                    MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                Else
                    sBreakdown = opd.FileName
                End If

                opd.FileName = String.Empty
                opd.Filter = "Excel Workbook (*.xlsx,*.xls)|*.xlsx;*.xls"
                opd.Title = "Select remittance/deduction file."

                If opd.ShowDialog() = Windows.Forms.DialogResult.Cancel Then Return False
                If Not File.Exists(opd.FileName) Then
                    MessageBox.Show("File not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Return False
                Else
                    sCollected = opd.FileName
                End If

                Common.PopupInformation("Initializing process. Press any key to continue.")
                GetDeductions(deductions, sBreakdown, 1, 3, 5)
                If deductions.Count > 0 Then
                    InsertDeductions(deductions)
                    GetCollected(collected, sCollected, 1, 5)
                    If collected.Count > 0 Then
                        InsertCollected(collected)
                        CreateExtkbcFile(fbd, collected.Select(Function(x) x.dAmount).Sum())
                    Else
                        Return False
                    End If
                Else
                    Return False
                End If
            Else
                CreateConsolidatedExtkbcFile(fbd)
            End If
            
            Return True
        End Function

        Private Shared Sub GetCollected(ByVal deductions As List(Of Deduction), ByVal sXlsFile As String, ByVal iEmpnoIndex As Integer, ByVal iAmountIndex As Integer)
            GetDeductions(deductions, sXlsFile, iEmpnoIndex, -1, iAmountIndex)
        End Sub

        Private Shared Sub GetDeductions(ByVal deductions As List(Of Deduction), ByVal sXlsFile As String, ByVal iEmpnoIndex As Integer, ByVal iAcctCodeIndex As Integer, ByVal iAmountIndex As Integer)
            Dim xlsApp As New Excel.Application()
            Dim xlsBook As Excel.Workbook = xlsApp.Workbooks.Open(sXlsFile)
            Dim xlsSheet As Excel.Worksheet
            Dim sEmpNo As String
            Dim sAcctCode As String = ""
            Dim dAmount As Decimal

            If iAcctCodeIndex = -1 And xlsBook.Sheets.Count > 1 Then
                Common.PopupExclamation("There are multiple sheets in the remittance file. Please check.")
                Return
            End If

            For iIndex1 As Integer = 1 To xlsBook.Sheets.Count
                xlsSheet = xlsBook.Sheets(iIndex1)

                For iIndex2 As Integer = 1 To xlsSheet.Rows.Count
                    sEmpNo = GetCellValue(xlsSheet.Cells(iIndex2 + 1, iEmpnoIndex))
                    sEmpNo = Microsoft.VisualBasic.Right("000000" & IIf(String.IsNullOrEmpty(sEmpNo), "", sEmpNo), 6)
                    If sEmpNo = "000000" Then Exit For
                    If (iAcctCodeIndex > 0) Then sAcctCode = GetCellValue(xlsSheet.Cells(iIndex2 + 1, iAcctCodeIndex))
                    dAmount = GetCellValue(xlsSheet.Cells(iIndex2 + 1, iAmountIndex))
                    deductions.Add(New Deduction(sEmpNo, sAcctCode, dAmount))
                Next
            Next

            xlsBook.Close()
            xlsApp.Quit()
        End Sub

        Private Shared Sub InsertDeductions(ByVal deductions As List(Of Deduction))
            Dim db As New Data.Database()
            Dim sb As New StringBuilder()
            Dim bFirst As Boolean = True
            Dim iCount As Integer = 0

            db.ExecuteNonQuery("truncate table dbo.MO_DEDN_DETL", CommandType.Text)

            For Each dedn As Deduction In deductions
                If iCount = 0 Then
                    bFirst = True
                    sb.Length = 0
                    sb.AppendLine("insert into dbo.MO_DEDN_DETL (EMP_NO, CODE5, AMT) values")
                End If

                sb.Append(String.Format("{0}('{1}','{2}',{3})", IIf(bFirst, "", ","), dedn.sEmpNo, dedn.sAcctCode, dedn.dAmount))
                iCount += 1
                bFirst = False

                If iCount = 1000 Then
                    iCount = 0
                    db.ExecuteNonQuery(sb.ToString(), CommandType.Text)
                End If
            Next

            If iCount > 0 AndAlso iCount <> 1000 Then db.ExecuteNonQuery(sb.ToString(), CommandType.Text)
        End Sub

        Private Shared Sub InsertCollected(ByVal deductions As List(Of Deduction))
            Dim db As New Data.Database()
            Dim sb As New StringBuilder()
            Dim bFirst As Boolean = True
            Dim iCount As Integer = 0

            db.ExecuteNonQuery("truncate table dbo.MO_DEDN_PAID", CommandType.Text)

            For Each dedn As Deduction In deductions
                If iCount = 0 Then
                    bFirst = True
                    sb.Length = 0
                    sb.AppendLine("insert into dbo.MO_DEDN_PAID (EMP_NO, PAY_AMT) values")
                End If

                sb.Append(String.Format("{0}('{1}',{2})", IIf(bFirst, "", ","), dedn.sEmpNo, dedn.dAmount))
                iCount += 1
                bFirst = False

                If iCount = 1000 Then
                    iCount = 0
                    db.ExecuteNonQuery(sb.ToString(), CommandType.Text)
                End If
            Next

            If iCount > 0 AndAlso iCount <> 1000 Then db.ExecuteNonQuery(sb.ToString(), CommandType.Text)
        End Sub

        Private Shared Sub CreateDeductionFile(ByVal fbd As Windows.Forms.FolderBrowserDialog)
            Dim db As New Data.Database()
            Dim dt As New DataTable()
            Dim sWageType As String = ConfigurationManager.AppSettings.Get("Payroll.WageType")
            Dim sXlsFile As String = fbd.SelectedPath & "\ADVICE.CONSOLIDATED.xls"
            Dim xlsApp As New Excel.Application()
            Dim xlsBook As Excel.Workbook = xlsApp.Workbooks.Add()
            Dim xlsSheet As Excel.Worksheet = xlsBook.Sheets(1)
            Dim index As Integer = 0
            Dim sb As New StringBuilder()

            sb.AppendLine("SELECT d.EMP_NO, ISNULL(dbo.func_FullName(m.LNAME, m.FNAME, m.MI), 'Unknown') NAME, SUM(d.AMT) as AMT")
            sb.AppendLine("FROM MO_DEDN_DETL d left join MEMBERS m on d.EMP_NO = m.CB_EMPNO")
            sb.AppendLine("GROUP BY d.EMP_NO, dbo.func_FullName(m.LNAME, m.FNAME, m.MI)")
            sb.AppendLine("ORDER BY NAME")
            dt = db.ExecuteQuery(sb.ToString(), CommandType.Text).Tables(0)

            xlsSheet.Range("B1").EntireColumn.NumberFormat = "@"
            SetCellValue(xlsSheet.Cells(1, 1), "DATE")
            SetCellValue(xlsSheet.Cells(1, 2), "EMPNO")
            SetCellValue(xlsSheet.Cells(1, 3), "NAME")
            SetCellValue(xlsSheet.Cells(1, 4), "WAGETYPE")
            SetCellValue(xlsSheet.Cells(1, 5), "AMOUNT")

            For index = 0 To dt.Rows.Count - 1
                xlsSheet.Cells(index + 2, 1) = Date.Now.ToString("yyyyMM") & "07"
                xlsSheet.Cells(index + 2, 2) = dt.Rows(index).Item(0).ToString()
                xlsSheet.Cells(index + 2, 3) = dt.Rows(index).Item(1).ToString()
                xlsSheet.Cells(index + 2, 4) = sWageType
                xlsSheet.Cells(index + 2, 5) = dt.Rows(index).Item(2)
            Next

            xlsBook.SaveAs(sXlsFile, Excel.XlFileFormat.xlWorkbookNormal)
            xlsBook.Close()
            xlsApp.Quit()
        End Sub

        Private Shared Sub CreateBreakdownFile(ByVal fbd As Windows.Forms.FolderBrowserDialog)
            Dim db As New Data.Database()
            Dim dt As New DataTable()
            Dim sXlsFile As String = fbd.SelectedPath & "\ADVICE.BREAKDOWN.xls"
            Dim xlsApp As New Excel.Application()
            Dim xlsBook As Excel.Workbook = xlsApp.Workbooks.Add()
            Dim xlsSheet As Excel.Worksheet = xlsBook.Sheets(1)
            Dim index As Integer = 0
            Dim sb As New StringBuilder()

            sb.AppendLine("SELECT d.EMP_NO, ISNULL(dbo.func_FullName(m.LNAME, m.FNAME, m.MI), 'Unknown') NAME, d.CODE5, COALESCE('KBCI ' + lt.LOAN_DESC, 'KBCI ' + ot.OTHER_DESC, '') AS DISPLAY, SUM(d.AMT) AS AMT")
            'sb.AppendLine("FROM MO_DEDN_DETL d left join MEMBERS m on d.EMP_NO = m.CB_EMPNO left join LOAN_TYPE lt on lt.CODE5 = d.CODE5 left join OTHER_TYPE ot on right(ot.CODE5, 4) = d.CODE5 left join LOANS l on l.KBCI_NO = m.KBCI_NO and l.LOAN_TYPE = lt.LOAN_TYPE and l.LOAN_STAT = 'R'")
            sb.AppendLine("FROM MO_DEDN_DETL d left join MEMBERS m on d.EMP_NO = m.CB_EMPNO left join LOAN_TYPE lt on lt.CODE5 = d.CODE5 left join OTHER_TYPE ot on right(ot.CODE5, 4) = d.CODE5")
            sb.AppendLine("GROUP BY d.EMP_NO, dbo.func_FullName(m.LNAME, m.FNAME, m.MI), d.CODE5, COALESCE('KBCI ' + lt.LOAN_DESC, 'KBCI ' + ot.OTHER_DESC, '')")
            sb.AppendLine("ORDER BY NAME, DISPLAY")
            dt = db.ExecuteQuery(sb.ToString(), CommandType.Text).Tables(0)

            xlsSheet.Range("A1").EntireColumn.NumberFormat = "@"
            xlsSheet.Range("D1").EntireColumn.NumberFormat = "@"

            SetCellValue(xlsSheet.Cells(1, 1), "PERNR")
            SetCellValue(xlsSheet.Cells(1, 2), "EMPLOYEE NAME")
            SetCellValue(xlsSheet.Cells(1, 3), "SUBTY")
            SetCellValue(xlsSheet.Cells(1, 4), "DESCRIPTION")
            SetCellValue(xlsSheet.Cells(1, 5), "BETRG")

            For index = 0 To dt.Rows.Count - 1
                xlsSheet.Cells(index + 2, 1) = dt.Rows(index).Item(0).ToString()
                xlsSheet.Cells(index + 2, 2) = dt.Rows(index).Item(1).ToString()
                xlsSheet.Cells(index + 2, 3) = dt.Rows(index).Item(2).ToString()
                xlsSheet.Cells(index + 2, 4) = dt.Rows(index).Item(3).ToString()
                xlsSheet.Cells(index + 2, 5) = dt.Rows(index).Item(4)
            Next

            xlsBook.SaveAs(sXlsFile, Excel.XlFileFormat.xlWorkbookNormal)
            xlsBook.Close()
            xlsApp.Quit()
        End Sub

        Private Shared Sub CreateExtkbcFile(ByVal fbd As Windows.Forms.FolderBrowserDialog, ByVal dCollectedTotal As Decimal)
            Dim db As New Data.Database()
            Dim dt As New DataTable()
            Dim sXlsFile As String = String.Format("{0}\EXTKBC.xls", fbd.SelectedPath)
            Dim xlsApp As New Excel.Application()
            Dim xlsBook As Excel.Workbook = xlsApp.Workbooks.Add()
            Dim xlsSheet As Excel.Worksheet = xlsBook.Sheets(1)
            Dim sb As New StringBuilder()
            Dim bMissing As Boolean = False
            Dim index As Integer = 1
            Dim dAmountTotal As Decimal = 0
            Dim sName As String
            Dim sMessage As String = String.Empty
            
            'Find available extkc file
            Do While File.Exists(sXlsFile)
                index += 1
                sXlsFile = String.Format("{0}\EXTKBC{1}.xls", fbd.SelectedPath, index.ToString())
            Loop

            'Apply payments
            db.ExecuteNonQuery("s3p_Payroll_Process_Breakdown", CommandType.StoredProcedure)

            'Build script
            sb.AppendLine("select")
            sb.AppendLine("   d.EMP_NO AS 'EMPNO1', ")
            sb.AppendLine("   ISNULL(dbo.func_FullName(m.LNAME, m.FNAME, m.MI), '') AS 'NAME',")
            sb.AppendLine("   CAST(0.00 AS numeric(6,2)) AS 'ACTYPE',")
            sb.AppendLine("   CAST(0.00 AS numeric(6,2)) AS 'ACTCD1',")
            sb.AppendLine("   CONVERT(numeric(9,2), d.CODE5) as 'ACTCD2',")
            sb.AppendLine("   CONVERT(numeric(13,2), CONVERT(VARCHAR(10), d.PAY_DATE, 112)) AS 'DATE7',")
            sb.AppendLine("   COALESCE('KBCI ' + lt.LOAN_DESC, 'KBCI ' + ot.OTHER_DESC, '') AS 'DESCRIPT', ")
            sb.AppendLine("   ROUND(d.PAY_AMT,2) AS 'AMT7C',")
            sb.AppendLine("   CAST(1.00 AS numeric(6,2)) AS 'CODE5'")
            sb.AppendLine("from")
            sb.AppendLine("   MO_DEDN_DETL d")
            sb.AppendLine("      left join")
            sb.AppendLine("   MEMBERS m on ")
            sb.AppendLine("      d.EMP_NO = m.CB_EMPNO ")
            sb.AppendLine("      left join ")
            sb.AppendLine("   LOAN_TYPE lt on ")
            sb.AppendLine("      lt.CODE5 = d.CODE5 ")
            sb.AppendLine("      left join ")
            sb.AppendLine("   OTHER_TYPE ot on ")
            sb.AppendLine("      right(ot.CODE5, 4) = d.CODE5 ")
            'sb.AppendLine("      left join ")
            'sb.AppendLine("   LOANS l on ")
            'sb.AppendLine("      l.KBCI_NO = m.KBCI_NO and ")
            'sb.AppendLine("      l.LOAN_TYPE = lt.LOAN_TYPE and ")
            'sb.AppendLine("      l.LOAN_STAT = 'R'")
            sb.AppendLine("where")
            sb.AppendLine("      d.PAY_AMT > 0")
            sb.AppendLine("order by")
            sb.AppendLine("   ISNULL(dbo.func_FullName(m.LNAME, m.FNAME, m.MI), ''),")
            sb.AppendLine("   isnull(ot.DEDN_SORT, 999),")
            sb.AppendLine("   AMT")
            dt = db.ExecuteQuery(sb.ToString(), CommandType.Text).Tables(0)

            'Format excel file
            xlsSheet.Range("A1").EntireColumn.NumberFormat = "@"
            xlsSheet.Range("B1").EntireColumn.NumberFormat = "@"
            xlsSheet.Range("G1").EntireColumn.NumberFormat = "@"
            xlsSheet.Range("H1").EntireColumn.NumberFormat = "#,##0.00"
            SetCellValue(xlsSheet.Cells(1, 1), "Empno1")
            SetCellValue(xlsSheet.Cells(1, 2), "Name")
            SetCellValue(xlsSheet.Cells(1, 3), "Actype")
            SetCellValue(xlsSheet.Cells(1, 4), "Actcd1")
            SetCellValue(xlsSheet.Cells(1, 5), "Actcd2")
            SetCellValue(xlsSheet.Cells(1, 6), "Date7")
            SetCellValue(xlsSheet.Cells(1, 7), "Descript")
            SetCellValue(xlsSheet.Cells(1, 8), "Amt7c")
            SetCellValue(xlsSheet.Cells(1, 9), "Code5")

            'Populate excel file
            For index = 0 To dt.Rows.Count - 1
                sName = dt.Rows(index).Item(1).ToString()
                xlsSheet.Cells(index + 2, 1) = dt.Rows(index).Item(0).ToString()
                xlsSheet.Cells(index + 2, 2) = sName
                xlsSheet.Cells(index + 2, 3) = dt.Rows(index).Item(2)
                xlsSheet.Cells(index + 2, 4) = dt.Rows(index).Item(3)
                xlsSheet.Cells(index + 2, 5) = dt.Rows(index).Item(4)
                xlsSheet.Cells(index + 2, 6) = dt.Rows(index).Item(5)
                xlsSheet.Cells(index + 2, 7) = dt.Rows(index).Item(6).ToString()
                xlsSheet.Cells(index + 2, 8) = dt.Rows(index).Item(7)
                xlsSheet.Cells(index + 2, 9) = dt.Rows(index).Item(8)

                dAmountTotal += dt.Rows(index).Item(7)
                If Not bMissing Then
                    bMissing = sName.Trim() = String.Empty
                End If
            Next

            If dAmountTotal <> dCollectedTotal Then sMessage = "Amount collected is not equal to EXTKBC total."
            If bMissing Then sMessage = IIf(sMessage = String.Empty, "", vbCrLf) & "Employee name not found. Please check employee number."
            If sMessage <> String.Empty Then Common.PopupInformation(sMessage)
            xlsBook.SaveAs(sXlsFile, Excel.XlFileFormat.xlWorkbookNormal)
            xlsBook.Close()
            xlsApp.Quit()
        End Sub

        Private Shared Sub CreateConsolidatedExtkbcFile(ByVal fbd As Windows.Forms.FolderBrowserDialog)
            Dim db As New Data.Database()
            Dim dt As New DataTable()
            Dim sXlsFile As String = String.Format("{0}\EXTKBC.xls", fbd.SelectedPath)
            Dim xlsApp As New Excel.Application()
            Dim xlsBook As Excel.Workbook = xlsApp.Workbooks.Add()
            Dim xlsSheet As Excel.Worksheet = xlsBook.Sheets(1)
            Dim index As Integer = 1
            Dim sb As New StringBuilder()
            Dim dAmountTotal As Decimal = 0

            'Find available extkc file
            Do While File.Exists(sXlsFile)
                index += 1
                sXlsFile = String.Format("{0}\EXTKBC{1}.xls", fbd.SelectedPath, index.ToString())
            Loop

            'Build script
            sb.AppendLine("select")
            sb.AppendLine("   d.EMP_NO AS 'EMPNO1', ")
            sb.AppendLine("   ISNULL(dbo.func_FullName(m.LNAME, m.FNAME, m.MI), '') AS 'NAME',")
            sb.AppendLine("   CAST(0.00 AS numeric(6,2)) AS 'ACTYPE',")
            sb.AppendLine("   CAST(0.00 AS numeric(6,2)) AS 'ACTCD1',")
            sb.AppendLine("   CONVERT(numeric(9,2), d.CODE5) as 'ACTCD2',")
            sb.AppendLine("   CONVERT(numeric(13,2), CONVERT(VARCHAR(10), MAX(d.PAY_DATE), 112)) AS 'DATE7',")
            sb.AppendLine("   COALESCE('KBCI ' + lt.LOAN_DESC, 'KBCI ' + ot.OTHER_DESC, '') AS 'DESCRIPT', ")
            sb.AppendLine("   ROUND(SUM(d.PAY_AMT),2) AS 'AMT7C',")
            sb.AppendLine("   CAST(1.00 AS numeric(6,2)) AS 'CODE5'")
            sb.AppendLine("from")
            sb.AppendLine("   MO_DEDN_HIST d")
            sb.AppendLine("      left join")
            sb.AppendLine("   MEMBERS m on ")
            sb.AppendLine("      d.EMP_NO = m.CB_EMPNO ")
            sb.AppendLine("      left join ")
            sb.AppendLine("   LOAN_TYPE lt on ")
            sb.AppendLine("      lt.CODE5 = d.CODE5 ")
            sb.AppendLine("      left join ")
            sb.AppendLine("   OTHER_TYPE ot on ")
            sb.AppendLine("      right(ot.CODE5, 4) = d.CODE5 ")
            sb.AppendLine("      left join ")
            sb.AppendLine("   LOANS l on ")
            sb.AppendLine("      l.KBCI_NO = m.KBCI_NO and ")
            sb.AppendLine("      l.LOAN_TYPE = lt.LOAN_TYPE and ")
            sb.AppendLine("      l.LOAN_STAT = 'R'")
            sb.AppendLine("where")
            sb.AppendLine("      d.PAY_AMT > 0 and")
            sb.AppendLine("      d.PAY_DATE > (select [PROC] from CTRL) and")
            sb.AppendLine("      d.OFF_CYCLE = 1 ")
            sb.AppendLine("group by")
            sb.AppendLine("   d.EMP_NO, ")
            sb.AppendLine("   ISNULL(dbo.func_FullName(m.LNAME, m.FNAME, m.MI), ''),")
            sb.AppendLine("   CONVERT(numeric(9,2), d.CODE5),")
            sb.AppendLine("   COALESCE('KBCI ' + lt.LOAN_DESC, 'KBCI ' + ot.OTHER_DESC, '') ")
            sb.AppendLine("order by")
            sb.AppendLine("   NAME,")
            sb.AppendLine("   AMT7C")
            dt = db.ExecuteQuery(sb.ToString(), CommandType.Text).Tables(0)

            'Format excel file
            xlsSheet.Range("A1").EntireColumn.NumberFormat = "@"
            xlsSheet.Range("B1").EntireColumn.NumberFormat = "@"
            xlsSheet.Range("G1").EntireColumn.NumberFormat = "@"
            xlsSheet.Range("H1").EntireColumn.NumberFormat = "#,##0.00"
            SetCellValue(xlsSheet.Cells(1, 1), "Empno1")
            SetCellValue(xlsSheet.Cells(1, 2), "Name")
            SetCellValue(xlsSheet.Cells(1, 3), "Actype")
            SetCellValue(xlsSheet.Cells(1, 4), "Actcd1")
            SetCellValue(xlsSheet.Cells(1, 5), "Actcd2")
            SetCellValue(xlsSheet.Cells(1, 6), "Date7")
            SetCellValue(xlsSheet.Cells(1, 7), "Descript")
            SetCellValue(xlsSheet.Cells(1, 8), "Amt7c")
            SetCellValue(xlsSheet.Cells(1, 9), "Code5")

            'Populate excel file
            For index = 0 To dt.Rows.Count - 1
                xlsSheet.Cells(index + 2, 1) = dt.Rows(index).Item(0)
                xlsSheet.Cells(index + 2, 2) = dt.Rows(index).Item(1).ToString()
                xlsSheet.Cells(index + 2, 3) = dt.Rows(index).Item(2)
                xlsSheet.Cells(index + 2, 4) = dt.Rows(index).Item(3)
                xlsSheet.Cells(index + 2, 5) = dt.Rows(index).Item(4)
                xlsSheet.Cells(index + 2, 6) = dt.Rows(index).Item(5)
                xlsSheet.Cells(index + 2, 7) = dt.Rows(index).Item(6).ToString()
                xlsSheet.Cells(index + 2, 8) = dt.Rows(index).Item(7)
                xlsSheet.Cells(index + 2, 9) = dt.Rows(index).Item(8)
                dAmountTotal += dt.Rows(index).Item(7)
            Next

            xlsBook.SaveAs(sXlsFile, Excel.XlFileFormat.xlWorkbookNormal)
            xlsBook.Close()
            xlsApp.Quit()
        End Sub

        Private Shared Function GetCellValue(ByVal xlsRange As Excel.Range) As Object
            Return xlsRange.Value
        End Function

        Private Shared Sub SetCellValue(ByVal xlsRange As Excel.Range, ByVal obj As Object)
            xlsRange.Value = obj
        End Sub

    End Class
End Namespace