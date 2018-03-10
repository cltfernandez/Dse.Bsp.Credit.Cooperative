Imports System.Linq.Expressions
Imports System.Collections.Generic
Imports System.Reflection
Imports Loan.Application.Infrastructure.Business.Interfaces
Imports Loan.Application.Infrastructure.Forms.Popups
Imports Loan.Application.Infrastructure.Enumerations.Popups
Imports Loan.Application.Infrastructure.Enumerations.Sql
Imports Loan.Application.Infrastructure.Enumerations.DropDownItems

Public Class Common

#Region "Combo Box"

    Public Shared Function AddColumn(ByVal DataType As String, ByVal ColumnName As String) As DataColumn
        Dim dc As New DataColumn
        Try
            dc.DataType = System.Type.GetType(DataType)
            dc.ColumnName = ColumnName
            dc.Caption = ColumnName
            Return dc
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Sub LoadComboBox(ByVal DataSource As DataTable, ByVal ValueMember As String, ByVal DisplayMember As String)
        Dim dr As DataRow

        If DataSource.Columns.Count = 0 Then
            DataSource.Columns.Add(Common.AddColumn("System.String", "Code"))
            DataSource.Columns.Add(Common.AddColumn("System.String", "Desc"))
        End If

        dr = DataSource.NewRow
        dr.Item("Code") = ValueMember
        dr.Item("Desc") = DisplayMember
        DataSource.Rows.Add(dr)
    End Sub

    Public Shared Sub SetComboBoxAutoComplete(ByVal ComboBox As ComboBox)
        ComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        ComboBox.AutoCompleteSource = AutoCompleteSource.ListItems
        ComboBox.DropDownStyle = ComboBoxStyle.DropDownList
    End Sub

    Public Shared Sub LoadPayrollCode(ByVal cboPayrollCode As ComboBox)
        Dim dt As New DataTable
        LoadComboBox(dt, "", "[SELECT]")
        LoadComboBox(dt, "7600", "Insurance (PHILAM)")
        LoadComboBox(dt, "7605", "Appliance Loan")
        LoadComboBox(dt, "7610", "Mutual Aid Benefit")
        LoadComboBox(dt, "7615", "Educational Loan")
        LoadComboBox(dt, "7620", "Emergency Loan")
        LoadComboBox(dt, "7625", "Fixed Deposit")
        LoadComboBox(dt, "7630", "Regular Loan")
        LoadComboBox(dt, "7635", "Savings Deposit")
        LoadComboBox(dt, "7640", "Special Loan")
        LoadComboBox(dt, "7645", "2nd Yr. Int. Spl.")
        LoadComboBox(dt, "7650", "Insurance (PGA)")
        LoadComboBox(dt, "7655", "Restructured Loan")
        LoadComboBox(dt, "7660", "Accounts Receivable")

        cboPayrollCode.DisplayMember = "Desc"
        cboPayrollCode.ValueMember = "Code"
        cboPayrollCode.DataSource = dt
        cboPayrollCode.SelectedIndex = 0
        dt = Nothing
        SetComboBoxAutoComplete(cboPayrollCode)
    End Sub

    Public Shared Sub LoadLoanType(ByVal cboLoanType As ComboBox)
        Dim dt As New DataTable
        Dim dr As DataRow

        LoadComboBox(dt, "", "[SELECT]")

        For Each dr In Common.GetDetailsOld("LoanTypeDetails", "%").Rows
            LoadComboBox(dt, dr.Item("LOAN_TYPE"), dr.Item("LOAN_DESC"))
        Next

        cboLoanType.DisplayMember = "Desc"
        cboLoanType.ValueMember = "Code"
        cboLoanType.DataSource = dt
        cboLoanType.SelectedIndex = 0
        dt = Nothing
        SetComboBoxAutoComplete(cboLoanType)
    End Sub

    Public Shared Sub LoadFrequency(ByVal cboFrequency As ComboBox)
        Dim dt As New DataTable
        LoadComboBox(dt, "", "[SELECT]")
        LoadComboBox(dt, "D", "Daily")
        LoadComboBox(dt, "M", "Monthly")
        LoadComboBox(dt, "Q", "Quarterly")
        LoadComboBox(dt, "S", "Semi-Annual")
        LoadComboBox(dt, "A", "Annual")

        cboFrequency.DisplayMember = "Desc"
        cboFrequency.ValueMember = "Code"
        cboFrequency.DataSource = dt
        cboFrequency.SelectedIndex = 0
        dt = Nothing
        SetComboBoxAutoComplete(cboFrequency)
    End Sub

    Public Shared Sub LoadPaymentMode(ByVal cboPaymentMode As ComboBox)
        Dim dt As New DataTable
        LoadComboBox(dt, "", "[SELECT]")
        LoadComboBox(dt, "1", "Payroll")
        LoadComboBox(dt, "2", "PDC")
        LoadComboBox(dt, "3", "DM")

        cboPaymentMode.DisplayMember = "Desc"
        cboPaymentMode.ValueMember = "Code"
        cboPaymentMode.DataSource = dt
        cboPaymentMode.SelectedIndex = 0
        dt = Nothing
        SetComboBoxAutoComplete(cboPaymentMode)
    End Sub

    Public Shared Sub LoadLoanStatus(ByVal cboLoanStatus As ComboBox)
        Dim dt As New DataTable

        LoadComboBox(dt, String.Empty, "[SELECT]")
        LoadComboBox(dt, "P", "Past due")
        LoadComboBox(dt, "T", "Terminated")
        LoadComboBox(dt, "F", "Fully Paid")
        LoadComboBox(dt, "R", "Released")
        LoadComboBox(dt, "A", "Approved")
        LoadComboBox(dt, "D", "Disapproved")
        LoadComboBox(dt, " ", "New Application")

        cboLoanStatus.DisplayMember = "Desc"
        cboLoanStatus.ValueMember = "Code"
        cboLoanStatus.DataSource = dt
        cboLoanStatus.SelectedIndex = 0
        dt = Nothing
        SetComboBoxAutoComplete(cboLoanStatus)
    End Sub

    Public Shared Sub LoadLedgerType(ByVal cboLedgerType As ComboBox)
        Dim dt As New DataTable

        LoadComboBox(dt, String.Empty, "[SELECT]")
        LoadComboBox(dt, "0", "Diminishing Prin.")
        LoadComboBox(dt, "1", "One Time Interest")

        cboLedgerType.DisplayMember = "Desc"
        cboLedgerType.ValueMember = "Code"
        cboLedgerType.DataSource = dt
        cboLedgerType.SelectedIndex = 0
        dt = Nothing
        SetComboBoxAutoComplete(cboLedgerType)
    End Sub

    Public Shared Sub LoadTextBox(ByVal txtBox As TextBox, ByVal txtString As Object, Optional ByVal txtFormat As String = "")
        If txtFormat Like "*#*" Then
            txtBox.Text = Format(Common.IsDBNullNum(txtString), txtFormat)
        Else
            txtBox.Text = Format(Common.IsDBNull(txtString), txtFormat)
        End If
    End Sub

    Public Shared Sub LoadComboBox(ByVal cboBox As ComboBox, ByVal cboSearch As String)
        cboBox.SelectedIndex = cboBox.FindStringExact(cboSearch)
    End Sub

#End Region

#Region "Code Descriptor"

    Public Shared Function LoanStatusDesc(ByVal Code As Object) As String
        If Code Is DBNull.Value Then
            Return String.Empty
        End If

        Select Case CStr(Code)
            Case "P"
                Return "Past due"
            Case "T"
                Return "Terminated"
            Case "F"
                Return "Fully Paid"
            Case "R"
                Return "Released"
            Case "A"
                Return "Approved"
            Case "D"
                Return "Disapproved"
            Case " " Or ""
                Return "New Application"
            Case Else
                Return String.Empty
        End Select
    End Function

    Public Shared Function PayModeDesc(ByVal Code As Object) As String
        If Code Is DBNull.Value Then
            Return String.Empty
        End If

        Select Case CInt(Code)
            Case 1
                Return "Payroll"
            Case 2
                Return "PDC"
            Case 3
                Return "DM"
            Case Else
                Return String.Empty
        End Select
    End Function

    Public Shared Function FrequencyDesc(ByVal Code As Object) As String
        If Code Is DBNull.Value Then
            Return String.Empty
        End If

        Select Case CStr(Code)
            Case "D"
                Return "Daily"
            Case "M"
                Return "Monthly"
            Case "Q"
                Return "Quarterly"
            Case "S"
                Return "Semi-Annual"
            Case "A"
                Return "Annual"
            Case Else
                Return String.Empty
        End Select
    End Function

    Public Shared Function LoanTypeDesc(ByVal Code As Object) As String
        If Code Is DBNull.Value Then
            Return String.Empty
        End If

        Dim retval As String
        retval = Common.GetDetailsOld("LoanTypeDetails", Code.ToString()).Rows(0).Item("LOAN_DESC")

        Return retval
    End Function

    Public Shared Function LedgerDesc(ByVal Code As Object) As String
        If Code Is DBNull.Value Then
            Return String.Empty
        ElseIf CInt(Code) = 1 Then
            Return "One Time Interest"
        Else
            Return "Diminishing Prin."
        End If
    End Function

#End Region

#Region "Display Custom Inputs"

    Public Shared Function ShowCalendar() As Date
        Dim form As New PopupCalendar
        Dim dateSelected As Date
        form.ShowDialog()
        dateSelected = form.GetSelectedDate()
        form = Nothing
        Return dateSelected
    End Function

    Public Shared Function PopupOptions(ByVal ParamArray options() As String) As Integer
        Dim form As New PopupButtons(options)
        form.ShowDialog()
        Return form.OptionIndex
    End Function

    Public Shared Function InputTextBox(ByVal message As String, Optional ByVal unformatted As Boolean = True) As String
        Dim inputString As String = String.Empty
        inputString = InputBox(message).Replace("-", "")
        Return inputString
    End Function

    Public Shared Function InputAmountBox(ByVal message As String, Optional ByVal amount As Double = 0) As String
        Dim inputString As String = InputBox(message, "", amount)

        If inputString.Trim <> String.Empty AndAlso IsNumeric(inputString.Trim) Then
            Return CDbl(inputString)
        Else
            Return 0
        End If
    End Function

#End Region

#Region "Null Tester"

    Public Shared Function IsDBNullDate(ByVal Value As Object) As Date
        If Value Is DBNull.Value Then
            Return Date.MinValue
        Else
            Return CDate(Value)
        End If
    End Function

    Public Shared Function IsDBNullBool(ByVal Value As Object) As Boolean
        If Value Is DBNull.Value Then
            Return False
        Else
            Return CBool(Value)
        End If
    End Function

    Public Shared Function IsDBNull(ByVal Text As Object) As String
        If Text Is DBNull.Value Then
            Return String.Empty
        Else
            Return Text
        End If
    End Function

    Public Shared Function IsDBNullNum(ByVal Value As Object) As Double
        If Value Is DBNull.Value Then
            Return 0
        Else
            Return CDbl(Value)
        End If
    End Function

    Public Shared Function IsNum(ByVal Value As String) As Boolean
        If Value = String.Empty Then
            Return False
        ElseIf IsNumeric(Replace(Value, ",", "")) Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region

#Region "Text Formatter"

    Public Shared Function Format241(ByVal Number As String) As String
        If Number Is String.Empty Or Number Is DBNull.Value Then
            Return String.Empty
        End If
        Return Microsoft.VisualBasic.Strings.Left(Number, 2) & "-" & Mid(Number, 3, 4) & "-" & Microsoft.VisualBasic.Strings.Right(Number, 1)
    End Function

    Public Shared Function FormatCS(ByVal Number As Double) As String
        Return Format(Number, "#,##0.00")
    End Function

    Public Shared Sub FormatSeparator(ByVal form As Form)
        Dim ctrl1 As Control
        Dim ctrl2 As Control

        For Each ctrl1 In form.Controls
            If TypeOf ctrl1 Is GroupBox Then
                For Each ctrl2 In ctrl1.Controls
                    If TypeOf ctrl2 Is TextBox AndAlso IsNumeric(CType(ctrl2, TextBox).Text) Then
                        CType(ctrl2, TextBox).Text = Format(CDbl(CType(ctrl2, TextBox).Text), "#,##0.00")
                    End If
                Next
            ElseIf TypeOf ctrl1 Is TextBox AndAlso IsNumeric(CType(ctrl1, TextBox).Text) Then
                CType(ctrl1, TextBox).Text = Format(CDbl(CType(ctrl1, TextBox).Text), "#,##0.00")
            End If
        Next
    End Sub

    Public Shared Sub FormatSeparator(ByVal Textbox As TextBox)
        If IsNumeric(Textbox.Text) Then
            Textbox.Text = Format(CDbl(Textbox.Text), "#,##0.00")
        End If
    End Sub

#End Region

#Region "Controls Manipulation"

    Public Enum EnumSetControls As Integer
        Clear = 1
        Enable = 2
        Disable = 3
        Read = 4
        ReadWrite = 5
    End Enum

    Public Shared Sub SetControls(ByVal form As Form, ByVal Control As EnumSetControls)
        Dim Ctrl1 As Control
        Dim Ctrl2 As Control
        Dim intControl As Integer = Control

        For Each Ctrl1 In form.Controls
            If TypeOf Ctrl1 Is GroupBox Then
                For Each Ctrl2 In Ctrl1.Controls
                    ChangeControlProperty(Ctrl2, intControl)
                Next
            Else
                ChangeControlProperty(Ctrl1, intControl)
            End If
        Next
    End Sub

    Private Shared Sub ChangeControlProperty(ByVal Ctrl As Control, ByVal intcontrol As Integer)
        If TypeOf Ctrl Is TextBox Then
            Select Case intcontrol
                Case 1
                    CType(Ctrl, TextBox).Text = String.Empty
                Case 2
                    CType(Ctrl, TextBox).Enabled = True
                Case 3
                    CType(Ctrl, TextBox).Enabled = False
                Case 4
                    CType(Ctrl, TextBox).ReadOnly = True
                Case 5
                    CType(Ctrl, TextBox).ReadOnly = False
            End Select
        ElseIf TypeOf Ctrl Is ComboBox Then
            Select Case intcontrol
                Case 1
                    CType(Ctrl, ComboBox).SelectedIndex = 0
                Case 2, 5
                    CType(Ctrl, ComboBox).Enabled = True
                Case 3, 4
                    CType(Ctrl, ComboBox).Enabled = False
            End Select
        ElseIf TypeOf Ctrl Is CheckBox Then
            Select Case intcontrol
                Case 1
                    CType(Ctrl, CheckBox).Checked = False
                Case 2, 5
                    CType(Ctrl, CheckBox).Enabled = True
                Case 3, 4
                    CType(Ctrl, CheckBox).Enabled = False
            End Select
        End If

    End Sub

    Public Shared Sub LoadControlDetails(ByVal form As Form, ByVal dr As DataRow)
        Dim Ctrl1 As Control
        Dim Ctrl2 As Control

        For Each Ctrl1 In form.Controls
            If TypeOf Ctrl1 Is GroupBox Then
                For Each Ctrl2 In Ctrl1.Controls
                    LoadControlDetail(Ctrl2, dr)
                Next
            Else
                LoadControlDetail(Ctrl1, dr)
            End If
        Next
    End Sub

    Private Shared Sub LoadControlDetail(ByVal Ctrl As Control, ByVal dr As DataRow)
        Dim ControlName As String = Ctrl.Name
        Dim Prefix As String = Microsoft.VisualBasic.Strings.Left(ControlName, 5)

        Try
            If TypeOf Ctrl Is TextBox Then
                If Prefix = "txtNX" Then
                    CType(Ctrl, TextBox).Text = Format(CDbl(Common.IsDBNullNum(dr.Item(ControlName))), "#,##0.00")
                Else
                    CType(Ctrl, TextBox).Text = Common.IsDBNull(dr.Item(ControlName))
                End If
            ElseIf TypeOf Ctrl Is ComboBox Then
                CType(Ctrl, ComboBox).SelectedValue = dr.Item(ControlName)
            ElseIf TypeOf Ctrl Is CheckBox Then
                CType(Ctrl, CheckBox).Checked = IIf(dr.Item(ControlName) Is DBNull.Value, False, CBool(dr.Item(ControlName)))
            End If
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function ValidFields(ByVal form As Form, Optional ByVal IncludeList As String = "", Optional ByVal ExcludeList As String = "") As Boolean
        Dim Ctrl1 As Control
        Dim Ctrl2 As Control
        Dim bValid As Boolean

        For Each Ctrl1 In form.Controls
            If TypeOf Ctrl1 Is GroupBox Then
                For Each Ctrl2 In Ctrl1.Controls
                    Ctrl2.Focus()
                    bValid = ValidField(Ctrl2, IncludeList, ExcludeList)
                    If Not bValid Then Return bValid
                Next
            Else
                Ctrl1.Focus()
                bValid = ValidField(Ctrl1, IncludeList, ExcludeList)
                If Not bValid Then Return bValid
            End If
        Next

        Return True
    End Function

    Private Shared Function ValidField(ByVal Ctrl As Control, ByVal IncludeList As String, ByVal ExcludeList As String) As Boolean
        Dim ControlName As String = Ctrl.Name
        Dim ControlText As String
        Dim Prefix As String = Microsoft.VisualBasic.Strings.Left(ControlName, 5)

        If (ExcludeList = "" And IncludeList = "") Or (ExcludeList = "" And IncludeList Like "*" & ControlName & "*") Or (IncludeList = "" And Not (ExcludeList Like "*" & ControlName & "*")) Then
            If TypeOf Ctrl Is TextBox Then
                ControlText = CType(Ctrl, TextBox).Text.Trim
                If ControlText = String.Empty Then
                    'MsgBox("ERROR: Field is required.", MsgBoxStyle.Critical, "Error")
                    Return True
                ElseIf Prefix = "txtDX" AndAlso Not IsDate(ControlText) Then
                    MsgBox("ERROR: Field is an invalid date.", MsgBoxStyle.Critical, "Error")
                    Return False
                ElseIf (Prefix = "txtNX" Or Prefix = "txtUX" Or Prefix = "txtIX") AndAlso Not IsNumeric(Replace(ControlText, "-", "")) Then
                    MsgBox("ERROR: Field not numeric.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
            ElseIf TypeOf Ctrl Is ComboBox Then
                If CType(Ctrl, ComboBox).SelectedIndex = 0 Then
                    MsgBox("ERROR: Please select from the list.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
            End If
        End If

        Return True
    End Function

#End Region

#Region "Computation"

    Public Shared Function Payment(ByVal Principal As Double, ByVal Interest As Double, ByVal Months As Integer) As Double
        Dim i As Double = Interest
        Dim k As Double

        If Interest > 0 Then
            k = (1 + i) ^ Months
            Return Principal * ((k * i) / (k - 1))
        Else
            Return Principal / Months
        End If
    End Function

    Public Shared Function GoDue(ByVal Pay_Start As DateTime, ByVal Term As Integer, ByVal Freq As Frequency) As DateTime
        Dim minus As Integer
        Dim mddue As DateTime
        Select Case Freq
            Case Frequency.Monthly
                minus = 1
            Case Frequency.Quarterly
                minus = 3
            Case Frequency.SemiAnnual
                minus = 6
            Case Frequency.Annual
                minus = 12
            Case Frequency.Daily
                minus = 0
        End Select

        If Freq = Frequency.Daily Then
            mddue = Pay_Start
        Else
            mddue = DateAdd(DateInterval.Month, Term - minus, Pay_Start)
        End If

        If Term = 1 And Freq = Frequency.Monthly Then
            Select Case mddue.DayOfWeek
                Case 1
                    mddue = DateAdd(DateInterval.Day, 1, mddue)
                Case 7
                    mddue = DateAdd(DateInterval.Day, 2, mddue)
            End Select
        End If

        Return mddue
    End Function

    Public Shared Function GoDueOld(ByVal Pay_Start As DateTime, ByVal Term As Integer, ByVal Freq As String) As DateTime
        Dim minus As Integer
        Dim mddue As DateTime
        Select Case Freq
            Case "M"
                minus = 1
            Case "Q"
                minus = 3
            Case "S"
                minus = 6
            Case "A"
                minus = 12
            Case "D"
                minus = 0
        End Select

        If Freq = "D" Then
            mddue = Pay_Start
        Else
            mddue = DateAdd(DateInterval.Month, Term - minus, Pay_Start)
        End If

        If Term = 1 And Freq = "M" Then
            Select Case mddue.DayOfWeek
                Case 1
                    mddue = DateAdd(DateInterval.Day, 1, mddue)
                Case 7
                    mddue = DateAdd(DateInterval.Day, 2, mddue)
            End Select
        End If

        Return mddue
    End Function

#End Region

#Region "Common SQL Tasks"

    Private Shared Function GetCommandBuilderText(ByVal table As MaintainanceTable, Optional ByVal column As MaintenanceFilter = MaintenanceFilter.None, Optional ByVal filter As String = "") As String
        Dim commandText As String = String.Empty

        If column <> MaintenanceFilter.None Then
            commandText = String.Format(" where [{0}] = '{1}'", CType(column, MaintenanceFilter).ToString(), filter)
        End If

        commandText = String.Format("select * from [{0}]", CType(table, MaintainanceTable).ToString()) & commandText

        If table = MaintainanceTable.Mo_Dedn_Detl Then
            commandText &= " order by EMP_NO, AMT"
        End If

        Return commandText
    End Function

    Public Shared Sub ShowTableEditor(ByVal table As MaintainanceTable, Optional ByVal column As MaintenanceFilter = MaintenanceFilter.None, Optional ByVal filter As String = "")
        Dim caption As String = String.Empty
        Dim isOverriden As Boolean = False
        Dim loan As Business.Objects.Loan
        Dim member As Business.Objects.Member
        Dim dt As DataTable

        Const level As Infrastructure.Enumerations.System.AccessLevels = Infrastructure.Enumerations.System.AccessLevels.Level4

        If Common.IsAuthenticated(level) Then
            If filter = String.Empty Then
                If column <> MaintenanceFilter.None Then
                    filter = GetFilter(table, column)
                    If filter = String.Empty Then Exit Sub
                    Select Case table
                        Case MaintainanceTable.Loans, MaintainanceTable.Ledger, MaintainanceTable.Lridue, MaintainanceTable.Payhist
                            loan = GetObject(Of Business.Objects.Loan)(filter)
                            member = GetObject(Of Business.Objects.Member)(loan.KBCI_NO)
                            caption = String.Format(" - {0} - {1} - {2}", CType(column, MaintenanceFilter), filter, member.LNAME & ", " & member.FNAME)
                        Case MaintainanceTable.Members
                            member = GetObject(Of Business.Objects.Member)(filter)
                            caption = String.Format(" - {0} - {1} - {2}", CType(column, MaintenanceFilter), filter, member.LNAME & ", " & member.FNAME)
                        Case Else
                            caption = String.Format(" - {0} - {1}", CType(column, MaintenanceFilter), filter)
                    End Select
                End If
            Else
                caption = String.Format(" - {0}", filter)
            End If

            If table = MaintainanceTable.Mo_Dedn_Detl Then
                dt = Common.GetDetails("SELECT SUM(AMT) AS 'AMT', SUM(PAY_AMT) AS 'PAY_AMT' FROM MO_DEDN_DETL")
                caption = String.Format("Deduction - Paid {0} of {1}", Format(dt.Rows(0).Item("PAY_AMT"), "#,##0.00"), Format(dt.Rows(0).Item("AMT"), "#,##0.00"))
            Else
                caption = String.Format("Maintenance - {0}{1}", CType(table, MaintainanceTable), caption)
            End If

            Using form As New SqlTableEditor(caption, GetCommandBuilderText(table, column, filter))
                'If table = MaintainanceTable.Mo_Dedn_Detl Then
                If table = MaintainanceTable.Mo_Dedn_Detl AndAlso False Then
                    form.pgRow.Enabled = False
                    form.dgvTable.ReadOnly = False
                    form.mnuSave.Enabled = False
                End If
                form.ShowDialog()
            End Using
        End If
    End Sub

    Private Shared Function GetFilter(ByVal table As MaintainanceTable, ByRef column As MaintenanceFilter) As String
        Dim filter As Object = Nothing

        Select Case PopupOptions("Input", "List", "Cancel", "Find Method")
            Case 1
                filter = InputTextBox(String.Format("Enter {0}", CType(column, MaintenanceFilter)))
                'If filter.Trim() = String.Empty Then MsgBox("ERROR: No input parameter.", MsgBoxStyle.Critical, "ERROR")
            Case 2
                Select Case True
                    Case column = MaintenanceFilter.PN_NO AndAlso MaintainanceTable.Ledger
                        filter = GetLoan().PN_NO
                    Case column = MaintenanceFilter.KBCI_NO
                        filter = GetMemberList().KBCI_NO
                    Case Else
                        filter = GetMemberList().KBCI_NO
                        column = MaintenanceFilter.KBCI_NO
                End Select
        End Select

        If filter Is Nothing Then Return String.Empty Else Return filter.ToString()
    End Function

    Public Shared Function DGVSearch(ByVal listType As String, ByVal listSubType As String, ByVal columnName As String) As DataRow
        Dim form As New PopupSearchDr
        Dim dr As DataRow
        form.dgvList.DataSource = Common.GetDetailsOld(listType, "")

        If CType(form.dgvList.DataSource, DataTable).Rows.Count <= 0 Then
            MsgBox("No record found.", MsgBoxStyle.Information)
            dr = Nothing
        Else
            form.ShowDialog()
            If form.Record Is Nothing Then
                MsgBox("No record found.", MsgBoxStyle.Information)
                dr = Nothing
            Else
                dr = Common.GetDetailsOld(listSubType, form.Record.Item(columnName)).Rows(0)
                If dr Is Nothing Then MsgBox("No record found.", MsgBoxStyle.Information)
            End If
        End If

        form = Nothing
        Return dr
    End Function

    Public Shared Function DGVSearchDS(ByVal listType As String, ByVal listSubType As String, ByVal columnName As String) As DataSet
        Dim form As New PopupSearchDr
        Dim ds As New DataSet
        form.dgvList.DataSource = Common.GetDetailsOld(listType, "")

        If CType(form.dgvList.DataSource, DataTable).Rows.Count <= 0 Then
            MsgBox("No record found.", MsgBoxStyle.Information)
            ds = Nothing
        Else
            form.ShowDialog()
            If form.Record Is Nothing Then
                MsgBox("No record found.", MsgBoxStyle.Information)
                ds = Nothing
            Else
                ds = Common.GetDetailsAsDataSet(form.Record.Item(columnName), listSubType)
                If ds Is Nothing Or ds.Tables.Count = 0 Then MsgBox("No record found.", MsgBoxStyle.Information)
            End If
        End If

        form = Nothing
        Return ds
    End Function

    Public Shared Function FindActiveLoans(ByVal listType As String) As DataRow
        Dim dr As DataRow
        dr = FindMember("LB-7")

        If dr IsNot Nothing Then
            dr = Common.GetDetailsOld(listType, dr.Item("PN_No")).Rows(0)
        End If

        Return dr
    End Function

    Public Shared Function FindLoans(ByVal loanListType As String, ByVal memberListType As String) As DataRow
        Dim dr As DataRow
        dr = FindMember(memberListType)

        If dr IsNot Nothing Then
            dr = Common.GetDetailsOld(loanListType, dr.Item("PN_No")).Rows(0)
        End If

        Return dr
    End Function

    Public Shared Function FindLoan(ByVal loanListType As String, ByVal memberListType As String) As DataRow
        Dim menu As Integer
        Dim dr As DataRow = Nothing
        Dim kbciNo As String = String.Empty
        Dim pnNo As String = String.Empty

        menu = PopupOptions("Input KBCI_NO", "Input PN_NO", "List", "Search Method")

        If menu = 1 Then
            kbciNo = InputTextBox("Enter KBCI_NO")
            If kbciNo.Trim().Length > 0 Then dr = FindMember(memberListType, kbciNo)
        ElseIf menu = 3 Then
            dr = FindMember(memberListType, "%")
        End If

        If menu = 1 Then
            If dr IsNot Nothing Then pnNo = dr.Item("PN_No")
        ElseIf menu = 2 Then
            pnNo = InputTextBox("Enter PN_NO")
        End If

        If pnNo.Trim().Length > 0 Then dr = Common.GetDetailsOld(loanListType, pnNo).Rows(0)
        Return dr
    End Function

    Public Shared Function FindMember(ByVal listType As String, Optional ByVal kbciNo As String = "%") As DataRow
        Dim dt As DataTable = Common.GetDetailsOld(listType, kbciNo)
        If dt IsNot Nothing Then
            Using popup As New PopupSearchDr
                popup.dgvList.DataSource = dt
                popup.ShowDialog()
                FindMember = popup.Record
            End Using
        Else
            FindMember = Nothing
        End If
    End Function

    Public Shared Function FindByInput(ByVal listType As String, ByVal message As String) As DataRow
        Dim dt As DataTable
        Dim dr As DataRow
        Dim filter As String
        filter = InputTextBox(message)

        If filter.Trim = String.Empty Then
            MsgBox("Invalid input.")
            dr = Nothing
        Else
            dt = Common.GetDetailsOld(listType, filter)

            If dt.Rows.Count = 0 Then
                MsgBox("No record found")
                dr = Nothing
            Else
                dr = dt.Rows(0)
            End If

            dt.Dispose()
        End If

        Return dr
    End Function

    Public Shared Function FindByListOld(ByVal listType As String, Optional ByVal filter As String = "") As DataRow
        Dim form As New PopupDataGridOptions
        Dim dt As New DataTable
        Dim dr As DataRow

        dt = Common.GetDetailsOld(listType, filter)

        If dt.Rows.Count > 0 Then
            form.GetDataGrid().ReadOnly = True
            form.GetDataGrid().DataSource = dt
            form.ShowDialog()

            If form.IsCanceled Then
                dr = Nothing
            Else
                dr = dt.Rows(form.GetDataGrid().CurrentRow.Index)
            End If
        Else
            MsgBox("No record found.", MsgBoxStyle.Information, "Information")
            dr = Nothing
        End If

        form.Dispose()
        dt.Dispose()
        Return dr
    End Function

    Public Shared Function FindByList(Of T)(ByVal type As Query, ByVal ParamArray parameters() As Object) As T
        Dim list As List(Of T) = Common.GetObjectList(Of T)(type, parameters)

        If list.Count <= 0 Then
            Common.PopupError("No record found.")
            Return Nothing
        End If

        Using form As New PopupDataGridOptions()
            form.GetDataGrid().DataSource = list
            form.ShowDialog()
            If form.IsCanceled() Then Return Nothing
            Return list(form.GetDataGrid().CurrentCell.RowIndex)
        End Using
    End Function

    Public Shared Function FindByList(ByVal listType As String, ByVal listSubType As String, ByVal columName As String) As DataRow
        Dim form As New PopupDataGridOptions
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim filter As String

        dt = Common.GetDetailsOld(listType, "DUMMY")
        form.GetDataGrid().ReadOnly = True
        form.GetDataGrid().DataSource = dt
        form.ShowDialog()

        If form.IsCanceled Then
            dr = Nothing
        Else
            filter = dt.Rows(form.GetDataGrid().CurrentRow.Index).Item(columName)
            dr = Common.GetDetailsOld(listSubType, filter).Rows(0)
        End If

        form.Dispose()
        dt.Dispose()
        Return dr
    End Function

    Public Shared Function FindDataSetByInput(ByVal listType As String, ByVal message As String) As DataSet
        Dim ds As DataSet
        Dim filter As String
        filter = InputTextBox(message)

        If filter.Trim = String.Empty Then
            MsgBox("Invalid input.")
            ds = Nothing
        Else
            ds = Common.GetDetailsAsDataSet(filter, listType)
            If ds.Tables(6).Rows.Count = 0 Then
                MsgBox("No record found")
                ds = Nothing
            End If
        End If

        Return ds
    End Function

    Public Shared Function FindDataSetByList(ByVal listType As String, ByVal listSubType As String) As DataSet
        Dim form As New PopupDataGridOptions
        Dim dt As New DataTable
        Dim ds As New DataSet
        Dim filter As String

        dt = Common.GetDetailsOld(listType, "DUMMY")
        form.GetDataGrid().ReadOnly = True
        form.GetDataGrid().DataSource = dt
        form.ShowDialog()

        If form.IsCanceled Then
            ds = Nothing
        Else
            filter = dt.Rows(form.GetDataGrid().CurrentRow.Index).Item("PN_NO")
            ds = Common.GetDetailsAsDataSet(filter, listSubType)

            If ds.Tables(6).Rows.Count = 0 Then
                MsgBox("No record found")
                ds = Nothing
            End If
        End If

        form.Dispose()
        dt.Dispose()
        Return ds
    End Function

    Public Shared Function GetDetailsOld(ByVal listType As String, Optional ByVal filter As String = "", Optional ByVal dateFilter As String = "") As DataTable
        Dim db As New Data.Database()
        Dim ds As DataSet

        db.AddParameter("@QueryType", listType)
        If filter <> String.Empty Then db.AddParameter("@v1", filter)
        If dateFilter <> String.Empty Then db.AddParameter("@dt1", dateFilter)

        ds = db.ExecuteQuery("dbo.s3p_Common_GetDetails", CommandType.StoredProcedure)

        If ds.Tables.Count > 0 Then
            Return ds.Tables(0)
        Else
            Return New DataTable()
        End If
    End Function

    Public Shared Function GetSDEDBAL(ByVal KBCI_NO As String, ByVal LOAN_TYPE As String, ByVal XRENEWSTL As Boolean, ByVal ID As String, ByVal MY_USER As String) As DataSet
        Dim db As New Data.Database()
        db.AddParameter("@v1", KBCI_NO)
        db.AddParameter("@v2", LOAN_TYPE)
        db.AddParameter("@v3", ID)
        db.AddParameter("@v4", MY_USER)
        db.AddParameter("@xrenewSTL", XRENEWSTL)
        db.AddParameter("@QueryType", "SDEDBAL_INIT")
        db.AddParameter("@dt1", "")
        Return db.ExecuteQuery("dbo.s3p_Common_GetDetails", CommandType.StoredProcedure)
    End Function

    Public Shared Function GetAdditionalSDEDBAL(ByVal PN_NO As String, ByVal ID As String, ByVal MY_USER As String) As DataTable
        Dim db As New Data.Database()
        db.AddParameter("@v1", PN_NO)
        db.AddParameter("@v3", ID)
        db.AddParameter("@v4", MY_USER)
        db.AddParameter("@QueryType", "SDEDBAL_ADD")
        db.AddParameter("@dt1", "")
        Return db.ExecuteQuery("dbo.s3p_Common_GetDetails", CommandType.StoredProcedure).Tables(0)
    End Function

    Public Shared Function GetDetailsAsDataSet(ByVal Code As String, ByVal Type As String, Optional ByVal dtDate As String = "") As DataSet
        Dim db As New Data.Database()
        db.AddParameter("@v1", Code)
        db.AddParameter("@QueryType", Type)
        db.AddParameter("@dt1", dtDate)
        Return db.ExecuteQuery("dbo.s3p_Common_GetDetails", CommandType.StoredProcedure)
    End Function

    Public Shared Function GetEmployeeName(ByVal search As EmployeeSearch, ByVal number As String) As String
        Dim dt As New DataTable
        Dim name As String = String.Empty

        Select Case search
            Case EmployeeSearch.CB_EMPNO
                dt = GetDetailsOld(number, "PG-1")
        End Select

        If dt.Rows.Count > 0 Then
            name = dt.Rows(0).Item("FULL_NAME")
        End If

        Return name
    End Function

    Public Shared Function SaveControlDetails(ByVal form As Form, Optional ByVal IncludeList As String = "", Optional ByVal ExcludeList As String = "") As SqlClient.SqlCommand
        Dim db As New Data.Database()
        Dim Ctrl1 As Control
        Dim Ctrl2 As Control

        For Each Ctrl1 In form.Controls
            If TypeOf Ctrl1 Is GroupBox Then
                For Each Ctrl2 In Ctrl1.Controls
                    SaveControlAsParameters(Ctrl2, db, IncludeList, ExcludeList)
                Next
            Else
                SaveControlAsParameters(Ctrl1, db, IncludeList, ExcludeList)
            End If
        Next

        Return db.Command
    End Function

    Private Shared Sub SaveControlAsParameters(ByVal Ctrl As Control, ByVal db As Data.Database, ByVal IncludeList As String, ByVal ExcludeList As String)
        Dim ControlName As String = Ctrl.Name
        Dim SqlParamName As String = "@" & ControlName
        Dim strText As String
        Dim objText As Object

        If (ExcludeList = "" And IncludeList = "") Or (ExcludeList = "" And IncludeList Like "*" & ControlName & "*") Or (IncludeList = "" And Not (ExcludeList Like "*" & ControlName & "*")) Then
            If TypeOf Ctrl Is TextBox Then
                strText = CType(Ctrl, TextBox).Text.Trim
                Select Case Microsoft.VisualBasic.Strings.Left(ControlName, 5)
                    Case "txtNX", "txtIX"
                        strText = Replace(IIf(strText = String.Empty, 0, strText), ",", "")
                        db.AddParameter(SqlParamName, strText)
                        'Case "txtDX"
                        '    objText = IIf(strText = String.Empty, DBNull.Value, strText)
                        '    db.addInputParameter(SqlParamName, objText)
                    Case Else
                        objText = IIf(strText = String.Empty, DBNull.Value, strText)
                        db.AddParameter(SqlParamName, objText)
                End Select
            ElseIf TypeOf Ctrl Is ComboBox Then
                db.AddParameter(SqlParamName, CType(Ctrl, ComboBox).SelectedValue)
            ElseIf TypeOf Ctrl Is CheckBox Then
                db.AddParameter(SqlParamName, CType(Ctrl, CheckBox).Checked)
            End If
        End If
    End Sub

    Public Shared Sub ProcessPayrollRecomputeBalance()
        Dim db As New Data.Database
        For Each deduction As Business.Objects.MonthlyDeduction In Common.GetObjectList(Of Business.Objects.MonthlyDeduction)("%", sysdate)
            If deduction.PN_NO IsNot Nothing AndAlso deduction.PN_NO.Trim().Length > 0 Then
                db.AddParameter("@PN_NO", deduction.PN_NO)
                db.ExecuteNonQuery("s3p_Loans_LoansNew_RecomputeBalance", CommandType.StoredProcedure)
            End If
        Next
    End Sub

    Public Shared Sub ProcessOffCycleRecomputeBalance()
        Dim db As New Data.Database
        For Each deduction As Business.Objects.OffCycleDeduction In Common.GetObjectList(Of Business.Objects.OffCycleDeduction)("%", sysdate)
            If deduction.PN_NO IsNot Nothing AndAlso deduction.PN_NO.Trim().Length > 0 Then
                db.AddParameter("@PN_NO", deduction.PN_NO)
                db.ExecuteNonQuery("s3p_Loans_LoansNew_RecomputeBalance", CommandType.StoredProcedure)
            End If
        Next
    End Sub

#End Region

#Region "Linq Helpers"

    'Public Shared Function FilterList(Of T)(ByVal filter As LambdaFiltering, ByVal source As List(Of T), ByVal columnName As String, ByVal value As String) As List(Of T)
    '    Dim parameter As ParameterExpression = Expression.Parameter(GetType(T), "x")
    '    Dim propertyOrField As Expression = Expression.PropertyOrField(parameter, columnName)
    '    Dim constant As Expression = Expression.Constant(value.ToUpper(), GetType(String))
    '    Dim process As Expression
    '    Dim contains As MethodInfo

    '    Select Case filter
    '        Case LambdaFiltering.Contains
    '            contains = GetType(Common).GetMethod("Contains")
    '            process = Expression.Call(contains, propertyOrField, constant)
    '        Case LambdaFiltering.Equals
    '            process = Expression.Equal(propertyOrField, constant)
    '    End Select

    '    Dim predicate As Expression(Of Func(Of T, Boolean)) = Expression.Lambda(Of Func(Of T, Boolean))(process, parameter)
    '    Dim compiled As Func(Of T, Boolean) = predicate.Compile()
    '    Return source.Where(compiled).ToList()
    'End Function

    'Public Shared Function Contains(ByVal mainString As String, ByVal subString As String) As Boolean
    '    Return mainString.Contains(subString)
    'End Function

#End Region

    Public Shared Sub SelectNextProperty(ByVal propertyGrid As PropertyGrid)
        Dim index As Integer = 0
        For Each gridItem As GridItem In propertyGrid.SelectedGridItem.Parent.GridItems
            If gridItem.Equals(propertyGrid.SelectedGridItem) Then Exit For
            index += 1
        Next
        If index + 1 < propertyGrid.SelectedGridItem.Parent.GridItems.Count Then
            propertyGrid.SelectedGridItem.Parent.GridItems(index + 1).Select()
        End If
    End Sub

    Public Shared Sub SetDataGridView(ByVal dgv As DataGridView)
        Infrastructure.Helpers.Controls.SetDataGridView(dgv)
    End Sub

    Public Shared Sub GetLatestControl()
        loanControl = GetObject(Of Business.Objects.Control)()
    End Sub

    Public Shared Function GetLoanRate(ByVal loanType As String, ByVal term As Int32) As Double
        Dim dRate As Double = 0
        For Each dr As DataRow In loanRates.Rows
            If dr.Item("LOAN_TYPE") = loanType And dr.Item("TERM") = term Then
                Return CDbl(dr.Item("RATE"))
            End If
        Next
        Return dRate
    End Function

    Public Shared Function GetLoan(Optional ByVal type As Query = Query.MemberLoans) As Business.Objects.Loan
        Dim pn_no As String = String.Empty
        Dim member As Business.Objects.MemberList = GetMemberList()
        If member.KBCI_NO IsNot Nothing Then
            Return FindByList(Of Business.Objects.Loan)(type, member.KBCI_NO)
        End If
        Return Nothing
    End Function

    Public Shared Function GetLoanHoldList(ByVal accountNo As String) As List(Of Business.Objects.LoanHold)
        Return GetObjectList(Of Business.Objects.LoanHold)(accountNo)
    End Function

    Public Shared Function GetLoanReleaseInsurance(ByVal pnNo As String) As Business.Objects.LoanReleaseInsurance
        Return GetObject(Of Business.Objects.LoanReleaseInsurance)(pnNo)
    End Function

    Public Shared Function GetLoanTypeDetail(ByVal loanType As LoanType) As Business.Objects.LoanTypeDetail
        If loanTypeDetails Is Nothing OrElse loanTypeDetails.Count = 0 Then
            loanTypeDetails = GetObjectList(Of Business.Objects.LoanTypeDetail)(Query.LoanTypeDetails)
        End If

        Return loanTypeDetails.Where(Function(x) x.LOAN_TYPE = loanType).FirstOrDefault()
    End Function

    Public Shared Function GetMember(ByVal kbciNo As String) As Business.Objects.Member
        Return GetObject(Of Business.Objects.Member)(kbciNo)
    End Function

    Public Shared Function GetMemberList() As Business.Objects.MemberList
        Return GetMemberList(Nothing, Query.MemberList)
    End Function

    Public Shared Function GetMemberList(ByVal service As System.Windows.Forms.Design.IWindowsFormsEditorService, Optional ByVal type As Query = Query.MemberList, Optional ByVal kbciNo As String = "%") As Business.Objects.MemberList
        Dim list As List(Of Business.Objects.MemberList)

        If IsMemberNamesValid() Then
            list = memberList
        Else
            list = GetObjectList(Of Business.Objects.MemberList)(kbciNo)
            memberList = list
        End If

        If list.Count > 0 Then
            Using popup As New PopupSearch(Of Business.Objects.MemberList)
                popup.SetDataSource(list)
                If service Is Nothing Then
                    popup.ShowDialog()
                Else
                    service.ShowDialog(popup)
                End If

                If Not popup.IsCanceled Then Return popup.SelectedObject
            End Using
        End If

        GetMemberList = Nothing
    End Function

    Public Shared Function GetDetails(ByVal type As Query, ByVal ParamArray parameters() As Object) As DataTable
        Return Data.Database.GetDetails(type, parameters)
    End Function

    Public Shared Function GetDetails(ByVal commandText As String) As DataTable
        Dim db As New Data.Database
        Return db.ExecuteQuery(commandText, CommandType.Text).Tables(0)
    End Function

    Public Shared Function GetObject(Of T)(ByVal ParamArray parameters() As Object) As T
        Return Data.Database.GetObject(Of T)(parameters)
    End Function

    Public Shared Function GetObject(Of T)(ByVal type As Query, ByVal ParamArray parameters() As Object) As T
        Return Data.Database.GetObject(Of T)(type, parameters)
    End Function

    Public Shared Function GetObjectList(Of T)(ByVal ParamArray parameters() As Object) As List(Of T)
        Return Data.Database.GetObjectList(Of T)(parameters)
    End Function

    Public Shared Function GetObjectList(Of T)(ByVal type As Query, ByVal ParamArray parameters() As Object) As List(Of T)
        Return Data.Database.GetObjectList(Of T)(type, parameters)
    End Function

    Public Shared Function GetSavingsAccount(ByVal accountNo As String, ByVal payment As Double, Optional ByVal isCredit As Boolean = False) As Business.Objects.SavingsDepositMaster
        Dim accountBalance As Decimal
        Dim heldBalance As Decimal
        Dim minimumBalance As Decimal
        Dim remarks As String
        Dim isOtherSA As Boolean = False
        Dim pn_no As String = String.Empty

        Dim member As Business.Objects.MemberList
        Dim savingsControl As Business.Objects.SavingsControl
        Dim savingsDeposit As Business.Objects.SavingsDepositMaster = Nothing
        Dim loansHeld As List(Of Business.Objects.LoanHold)

        If accountNo = String.Empty Then
            remarks = "No savings account. "
            isOtherSA = True
        ElseIf Common.PopupQuestion(String.Format("Use borrower's savings account ({0})?", accountNo)) = Windows.Forms.DialogResult.No Then
            isOtherSA = True
        End If

        If isOtherSA Then
            member = GetMemberList()
            If member.KBCI_NO IsNot Nothing Then
                savingsDeposit = FindByList(Of Business.Objects.SavingsDepositMaster)(Query.SavingsDepositMaster, member.KBCI_NO)
            End If
        Else
            savingsDeposit = GetSavingsDeposit(SavingsDepositQuery.ACCTNO, accountNo)
        End If

        If savingsDeposit Is Nothing Then
            Common.PopupError(String.Format("Invalid Account No.: {0}", accountNo))
            Return Nothing
        Else
            savingsControl = GetSavingsControl()
            If Not isCredit Then
                loansHeld = Common.GetLoanHoldList(savingsDeposit.ACCTNO)

                accountBalance = savingsDeposit.ACCTABAL
                minimumBalance = savingsControl.MINBAL
                heldBalance = loansHeld.Where(Function(x) x.HOLDCD = "PAY" AndAlso x.HOLDTYPE = "DM" AndAlso x.POSTSTAT <> "Y" AndAlso x.HOLDDATE >= sysdate).Select(Function(x) x.HOLDAMT).Sum()

                If payment > accountBalance - heldBalance - minimumBalance Then
                    MessageBox.Show("SA balance cannot cover amount to be paid.", "Debit Memo", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return Nothing
                End If
            End If
        End If

        Return savingsDeposit
    End Function

    Public Shared Function GetSavingsControl() As Business.Objects.SavingsControl
        Return GetObject(Of Business.Objects.SavingsControl)()
    End Function

    Public Shared Function GetSavingsDeposit(ByVal query As SavingsDepositQuery, ByVal number As String) As Business.Objects.SavingsDepositMaster
        Return GetObject(Of Business.Objects.SavingsDepositMaster)(String.Format("{0}{1}", query.ToString(), number))
    End Function

    Public Shared Function IsAuthenticated(ByVal level As Infrastructure.Enumerations.System.AccessLevels) As Boolean
        If sysuserlevel < level Then
            Using frmOverride As New PopupAuthenticate(level)
                frmOverride.ShowDialog()
                Return frmOverride.IsOverriden
            End Using
        Else
            Return True
        End If
        Return False
    End Function

    Private Shared Function IsMemberNamesValid() As Boolean
        Dim count As Double = CDbl(Common.GetDetails(Query.MemberCount).Rows(0).Item("Value"))
        Return memberList IsNot Nothing AndAlso memberList.Count = count
    End Function

    Public Shared Function OpenPopup(ByVal popup As Object, Optional ByVal text As String = "") As PopupResponses
        Dim response As PopupResponses = PopupResponses.Nok

        Using params As New PopupParameters(popup)
            params.Text = text

            Do While response = PopupResponses.Nok
                params.ShowDialog()
                response = ValidatePopup(popup, params)
                If response = PopupResponses.Quit Then Return response
            Loop

            Return response
        End Using
    End Function

    Public Shared Sub OpenReport(Of T)(ByVal ParamArray parameters() As Object)
        Dim report As New Report.Viewer(Of T)(parameters)
        report.Show()
    End Sub

    Private Shared Function ValidatePopup(ByVal popup As Object, ByVal params As PopupParameters) As PopupResponses
        Dim message As String = String.Empty

        Select Case True
            Case params.Response = PopupResponses.Quit
                Return PopupResponses.Quit
            Case TypeOf popup Is IIntRange
                message = ValidateIntRange(popup)
            Case TypeOf popup Is IDateRange
                message = ValidatePopupDateRange(popup)
            Case TypeOf popup Is ITextRange
                message = ValidatePopupTextRange(popup)
            Case TypeOf popup Is Business.Popups.ControlDate
                message = ValidatePopupControlDate(popup)
            Case TypeOf popup Is Business.Popups.BankAndCheckNo
                message = ValidatePopupBankAndCheckNo(popup)
            Case TypeOf popup Is Business.Popups.InputAmount
                message = ValidatePopupUserAmount(popup)
        End Select

        If message = String.Empty Then
            Return PopupResponses.Ok
        Else
            Common.PopupError(message)
            Return PopupResponses.Nok
        End If

    End Function

    Private Shared Function ValidateIntRange(ByVal popup As IIntRange) As String
        Const errorMessage As String = "Starting value is required"
        With popup
            If .IntFrom > .IntTo Then
                Return errorMessage
            End If
        End With
        Return String.Empty
    End Function

    Private Shared Function ValidatePopupDateRange(ByVal popup As IDateRange) As String
        Const errorMessage As String = "Invalid date range."
        With popup
            If .DateFrom > .DateTo Then
                Return errorMessage
            End If
        End With
        Return String.Empty
    End Function

    Private Shared Function ValidatePopupTextRange(ByVal popup As ITextRange) As String
        Const errorMessage As String = "Starting value is required."
        With popup
            If .TextFrom Is Nothing Then Return errorMessage
            If .TextTo Is Nothing Then .TextTo = .TextFrom
        End With
        Return String.Empty
    End Function

    Private Shared Function ValidatePopupControlDate(ByVal popup As Business.Popups.ControlDate) As String
        Const errorMessage As String = "Admin date is not equal to system date."
        If popup.AdminDate.Equals(popup.SystemDate) Then Return String.Empty Else Return errorMessage
    End Function

    Private Shared Function ValidatePopupBankAndCheckNo(ByVal popup As Business.Popups.BankAndCheckNo) As String
        Const errorMessage As String = "Bank and check number is required."
        With popup
            If (.Bank IsNot Nothing AndAlso .Bank.Trim().Length <= 0) OrElse (.CheckNo IsNot Nothing AndAlso .CheckNo.Trim().Length <= 0) Then Return errorMessage Else Return String.Empty
        End With
    End Function

    Private Shared Function ValidatePopupUserAmount(ByVal popup As Business.Popups.InputAmount) As String
        Const errorMessage As String = "Amount must be greater than zero."
        If popup.Amount <= 0 Then Return errorMessage Else Return String.Empty
    End Function

    Public Shared Function PopupError(ByVal message As String) As DialogResult
        Const caption As String = "DSE BSP Credit Cooperative"
        Return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Function

    Public Shared Function PopupExclamation(ByVal message As String) As DialogResult
        Const caption As String = "DSE BSP Credit Cooperative"
        Return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
    End Function

    Public Shared Function PopupInformation(ByVal message As String) As DialogResult
        Const caption As String = "DSE BSP Credit Cooperative"
        Return MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Function

    Public Shared Function PopupQuestion(ByVal message As String) As DialogResult
        Const caption As String = "DSE BSP Credit Cooperative"
        Return MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    End Function

    Public Shared Sub RunAsyncProcess(ByVal form As Windows.Forms.Form, ByVal process As Infrastructure.Enumerations.System.AsyncProcess)
        Dim bgp As New BackgroundProcess(form, process)
        bgp.RunWorkerAsync()
    End Sub

End Class
