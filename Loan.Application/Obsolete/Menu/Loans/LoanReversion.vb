Public Class LoanReversion
    Inherits Loan.Application.Infrastructure.Forms.Windows.BaseForm

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtPNNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtKBCI As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtOutPrincipal As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPrincipal As System.Windows.Forms.TextBox
    Friend WithEvents txtInterest As System.Windows.Forms.TextBox
    Friend WithEvents txtOthers As System.Windows.Forms.TextBox
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuNew As System.Windows.Forms.MenuItem
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPenalty As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents dgDetails As System.Windows.Forms.DataGrid
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.txtPNNo = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cboLoanType = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtKBCI = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtOutPrincipal = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtPrincipal = New System.Windows.Forms.TextBox
        Me.txtInterest = New System.Windows.Forms.TextBox
        Me.txtOthers = New System.Windows.Forms.TextBox
        Me.dgDetails = New System.Windows.Forms.DataGrid
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuNew = New System.Windows.Forms.MenuItem
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtPenalty = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.lblTitle = New System.Windows.Forms.Label
        CType(Me.dgDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtPNNo
        '
        Me.txtPNNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPNNo.Location = New System.Drawing.Point(86, 17)
        Me.txtPNNo.Name = "txtPNNo"
        Me.txtPNNo.Size = New System.Drawing.Size(127, 21)
        Me.txtPNNo.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 20)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "PN No."
        '
        'cboLoanType
        '
        Me.cboLoanType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLoanType.Location = New System.Drawing.Point(86, 71)
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.Size = New System.Drawing.Size(127, 21)
        Me.cboLoanType.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 74)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "Loan Type"
        '
        'txtKBCI
        '
        Me.txtKBCI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKBCI.Location = New System.Drawing.Point(86, 44)
        Me.txtKBCI.MaxLength = 9
        Me.txtKBCI.Name = "txtKBCI"
        Me.txtKBCI.Size = New System.Drawing.Size(127, 21)
        Me.txtKBCI.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "KBCI No."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 13)
        Me.Label4.TabIndex = 55
        Me.Label4.Text = "Outs Prin"
        '
        'txtOutPrincipal
        '
        Me.txtOutPrincipal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOutPrincipal.Location = New System.Drawing.Point(86, 98)
        Me.txtOutPrincipal.MaxLength = 9
        Me.txtOutPrincipal.Name = "txtOutPrincipal"
        Me.txtOutPrincipal.Size = New System.Drawing.Size(127, 21)
        Me.txtOutPrincipal.TabIndex = 56
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 57
        Me.Label5.Text = "Principal"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 13)
        Me.Label6.TabIndex = 58
        Me.Label6.Text = "Interest"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(6, 209)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 13)
        Me.Label8.TabIndex = 59
        Me.Label8.Text = "Others"
        '
        'txtPrincipal
        '
        Me.txtPrincipal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrincipal.Location = New System.Drawing.Point(86, 152)
        Me.txtPrincipal.MaxLength = 9
        Me.txtPrincipal.Name = "txtPrincipal"
        Me.txtPrincipal.Size = New System.Drawing.Size(127, 21)
        Me.txtPrincipal.TabIndex = 60
        '
        'txtInterest
        '
        Me.txtInterest.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtInterest.Location = New System.Drawing.Point(86, 179)
        Me.txtInterest.MaxLength = 9
        Me.txtInterest.Name = "txtInterest"
        Me.txtInterest.Size = New System.Drawing.Size(127, 21)
        Me.txtInterest.TabIndex = 61
        '
        'txtOthers
        '
        Me.txtOthers.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOthers.Location = New System.Drawing.Point(86, 206)
        Me.txtOthers.MaxLength = 9
        Me.txtOthers.Name = "txtOthers"
        Me.txtOthers.Size = New System.Drawing.Size(127, 21)
        Me.txtOthers.TabIndex = 62
        '
        'dgDetails
        '
        Me.dgDetails.DataMember = ""
        Me.dgDetails.HeaderForeColor = System.Drawing.SystemColors.ControlText
        Me.dgDetails.Location = New System.Drawing.Point(6, 17)
        Me.dgDetails.Name = "dgDetails"
        Me.dgDetails.Size = New System.Drawing.Size(522, 210)
        Me.dgDetails.TabIndex = 63
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuNew})
        '
        'mnuNew
        '
        Me.mnuNew.Index = 0
        Me.mnuNew.Text = "&New"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtPenalty)
        Me.GroupBox1.Controls.Add(Me.txtOthers)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtInterest)
        Me.GroupBox1.Controls.Add(Me.txtPNNo)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPrincipal)
        Me.GroupBox1.Controls.Add(Me.txtKBCI)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cboLoanType)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtOutPrincipal)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(228, 240)
        Me.GroupBox1.TabIndex = 66
        Me.GroupBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 64
        Me.Label2.Text = "Penalty"
        '
        'txtPenalty
        '
        Me.txtPenalty.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPenalty.Location = New System.Drawing.Point(86, 125)
        Me.txtPenalty.MaxLength = 9
        Me.txtPenalty.Name = "txtPenalty"
        Me.txtPenalty.Size = New System.Drawing.Size(127, 21)
        Me.txtPenalty.TabIndex = 63
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dgDetails)
        Me.GroupBox2.Location = New System.Drawing.Point(246, 53)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(534, 240)
        Me.GroupBox2.TabIndex = 67
        Me.GroupBox2.TabStop = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(267, 37)
        Me.lblTitle.TabIndex = 97
        Me.lblTitle.Text = "Loan Reversion"
        '
        'LoanReversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(792, 305)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Menu = Me.MainMenu1
        Me.Name = "LoanReversion"
        Me.Text = "Loan Reversion"
        CType(Me.dgDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub LoanReversion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Common.LoadLoanType(cboLoanType)
        Common.SetControls(Me, Common.EnumSetControls.Clear)
        Common.SetControls(Me, Common.EnumSetControls.Disable)
    End Sub

    Private Sub mnuNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNew.Click
        Dim dr As DataRow
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dr2 As DataRow

        Dim dtCTRL As New DataTable
        Dim dtLoans As New DataTable
        Dim dtLedger As New DataTable
        Dim dtLNHold As New DataTable
        Dim dtLRIDue As New DataTable
        Dim dtRLRIDue As New DataTable
        Dim dtPayhist As New DataTable
        Dim dtMembers As New DataTable

        Dim xsa As String = String.Empty
        Dim xpilt As String = "PAY"
        Dim xpn_no As String = String.Empty
        Dim npn_no As String = String.Empty

        Dim xtdate As DateTime
        Dim chkno_date As DateTime
        Dim date_godue As DateTime

        Dim opri As Double = 0
        Dim rpri As Double = 0
        Dim oint As Double = 0
        Dim rint As Double = 0
        Dim roth As Double = 0
        Dim rpen As Double = 0
        Dim xpayl As Double = 0

        Dim lanow As Boolean = False
        Dim x2 As Boolean

        Dim intIndex As Integer = 0

        Common.SetControls(Me, Common.EnumSetControls.Clear)
        Common.SetControls(Me, Common.EnumSetControls.Read)

        Select Case Common.PopupOptions("Find by Input", "Find by List", "Cancel", "")
            Case 1
                ds = Common.FindDataSetByInput("LF-1", "Enter PN No.")
            Case 2
                'ds = FindDataSetByList("LB-4", "LF-1")
                ds = Common.DGVSearchDS("LF-2", "LF-1", "PN_NO")
                If ds Is Nothing Then Exit Sub
            Case 3
                Exit Sub
        End Select

        If ds Is Nothing Then
            Exit Sub
        Else
            dtLedger = ds.Tables(0)
            dtLNHold = ds.Tables(1)
            dtLRIDue = ds.Tables(2)
            dtRLRIDue = ds.Tables(3)
            dtPayhist = ds.Tables(4)
            dtMembers = ds.Tables(5)
            dtLoans = ds.Tables(6)
            dtCTRL = ds.Tables(7)

            xpn_no = dtLoans.Rows(0).Item("PN_NO")

            txtKBCI.Text = Common.Format241(dtLoans.Rows(0).Item("KBCI_NO"))
            txtPNNo.Text = Common.Format241(dtLoans.Rows(0).Item("PN_NO"))
            cboLoanType.SelectedValue = dtLoans.Rows(0).Item("LOAN_TYPE")
            txtOutPrincipal.Text = Common.IsDBNullNum(dtLoans.Rows(0).Item("PRINCIPAL")) - Common.IsDBNullNum(dtLoans.Rows(0).Item("ACCU_PAYP"))

            txtPrincipal.Text = Common.IsDBNullNum(dtLoans.Rows(0).Item("ARREAR_P")) + Common.IsDBNullNum(dtLoans.Rows(0).Item("P_BAL"))
            txtInterest.Text = Common.IsDBNullNum(dtLoans.Rows(0).Item("ARREAR_I")) + Common.IsDBNullNum(dtLoans.Rows(0).Item("I_BAL"))
            txtOthers.Text = Common.IsDBNullNum(dtLoans.Rows(0).Item("ARREAR_OTH")) + Common.IsDBNullNum(dtLoans.Rows(0).Item("O_BAL"))

            Common.FormatSeparator(Me)
        End If

        chkno_date = CDate(dtLoans.Rows(0).Item("CHKNO_DATE"))

        If sysdate = chkno_date Then
            lanow = True
        End If

        If lanow Then
            MsgBox("Loans released for today cannot be reversed.", MsgBoxStyle.Information)
            Reset()
            Exit Sub
        End If

        If dtLedger.Rows.Count = 0 Then
            MsgBox("No entries in ledger file.", MsgBoxStyle.Information)
            Reset()
            Exit Sub
        End If

        xpilt = "PAY"
        xpayl = 0

        For intIndex = 0 To dtLedger.Rows.Count - 1
            dr = dtLedger.Rows(intIndex)
            If Not CStr(dr.Item("RMK")).Trim Like "*PAYROLL DEDUCTION*" Then
                If dr.Item("ACCT_TYPE").ToString.Trim <> "AMT" And CDate(dr.Item("DATE")) = sysdate Then xpayl = intIndex
                If dr.Item("ACCT_TYPE").ToString.Trim = "TER" And CDate(dr.Item("DATE")) = sysdate Then xpilt = "TER"
            End If
        Next

        If xpayl <> 0 Then
            dt.Columns.Add(Common.AddColumn("System.String", "DATE"))
            dt.Columns.Add(Common.AddColumn("System.String", "DOX_TYPE"))
            dt.Columns.Add(Common.AddColumn("System.String", "ACCT_TYPE"))
            dt.Columns.Add(Common.AddColumn("System.String", "ACCT_CODE"))
            dt.Columns.Add(Common.AddColumn("System.String", "DR"))
            dt.Columns.Add(Common.AddColumn("System.String", "CR"))
            dt.Columns.Add(Common.AddColumn("System.String", "RMK"))

            dr = dtLedger.Rows(xpayl)
            xtdate = CDate(dr.Item("DATE"))

            For Each dr In dtLedger.Rows
                If CDate(dr.Item("DATE")) = xtdate AndAlso Not CStr(dr.Item("RMK")).Trim Like "*PAYROLL DEDUCTION*" Then
                    If "PAY.ADJ" Like "*" & CStr(dr.Item("ACCT_TYPE")) & "*" Then
                        Select Case CStr(dr.Item("ACCT_CODE"))
                            Case "PRI"
                                If CStr(dr.Item("RMK")) Like "*LOAN AMORT*" Then opri += CDbl(dr.Item("CR"))
                                If CStr(dr.Item("RMK")) Like "*LOAN ARREAR*" Then rpri += CDbl(dr.Item("CR"))
                            Case "INT"
                                If CStr(dr.Item("RMK")) Like "*LOAN AMORT*" Then oint += CDbl(dr.Item("CR"))
                                If CStr(dr.Item("RMK")) Like "*LOAN ARREAR*" Then rint += CDbl(dr.Item("CR"))
                            Case "OTH"
                                If CStr(dr.Item("RMK")) Like "*LOAN PENALTY*" Then roth += CDbl(dr.Item("CR"))
                        End Select
                    End If

                    If "TER.PAY" Like "*" & CStr(dr.Item("ACCT_TYPE")) & "*" Then
                        npn_no = dr.Item("REF").ToString.Trim
                        Select Case CStr(dr.Item("ACCT_CODE"))
                            Case "PRI"
                                'code below is disabled in foxpro
                                'rpri += CDbl(dr.Item("CR"))
                            Case "INT"
                                If CStr(dr.Item("RMK")) Like "*PRETERM INT*" Then rint += CDbl(dr.Item("CR"))
                            Case "PEN"
                                roth += CDbl(dr.Item("CR"))
                        End Select
                    End If

                    dr2 = dt.NewRow()
                    dr2.Item("DATE") = Common.IsDBNull(dr.Item("DATE"))
                    dr2.Item("DOX_TYPE") = Common.IsDBNull(dr.Item("DOX_TYPE"))
                    dr2.Item("ACCT_TYPE") = Common.IsDBNull(dr.Item("ACCT_TYPE"))
                    dr2.Item("ACCT_CODE") = Common.IsDBNull(dr.Item("ACCT_CODE"))
                    dr2.Item("DR") = Format(CDbl(Common.IsDBNullNum(dr.Item("DR"))), "##,###,##0.00")
                    dr2.Item("CR") = Format(CDbl(Common.IsDBNullNum(dr.Item("CR"))), "##,###,##0.00")
                    dr2.Item("RMK") = Common.IsDBNull(dr.Item("RMK"))
                    dt.Rows.Add(dr2)

                End If
            Next
        Else
            MsgBox("There are no entries today in ledger file.", MsgBoxStyle.Exclamation)
            Reset()
            Exit Sub
        End If

        rpen = roth
        txtPenalty.Text = Format(rpen, "#,##0.00")

        dgDetails.DataSource = dt
        dgDetails.Refresh()

        If MsgBox("Reverse entries?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes AndAlso MsgBox("Are you sure?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
            x2 = True
            If xpilt = "TER" Then
                dt = Common.GetDetailsOld("Loan", npn_no)
                If dt.Rows.Count > 0 Then
                    For Each dr In dt.Rows
                        If dr.Item("LOAN_STAT").ToString.Trim = "R" Then
                            MsgBox("ERROR: Please payoff PN No: " + npn_no + " before reversal.", MsgBoxStyle.Critical, "Error")
                            Reset()
                            x2 = False
                        End If
                    Next
                End If
            End If
        Else
            x2 = False
        End If

        If x2 = True Then
            date_godue = Common.GoDueOld(CDate(dtLoans.Rows(0).Item("PAY_START")), CInt(dtLoans.Rows(0).Item("TERM")), dtLoans.Rows(0).Item("FREQ").ToString.Trim)
            If Business.LoanReversion.IsReversed(xtdate, xpn_no, roth, rint, rpri, opri, oint, date_godue) Then
                MsgBox("Reversal successful", MsgBoxStyle.Information)
            End If
        End If

        ds = Nothing
        dt = Nothing
        dtCTRL = Nothing
        dtLoans = Nothing
        dtLedger = Nothing
        dtLNHold = Nothing
        dtLRIDue = Nothing
        dtRLRIDue = Nothing
        dtPayhist = Nothing
        dtMembers = Nothing

        dgDetails.DataSource = Nothing
        dgDetails.Refresh()

        Common.SetControls(Me, Common.EnumSetControls.Clear)
        Common.SetControls(Me, Common.EnumSetControls.Disable)
    End Sub

    Private Sub Reset()
        Common.SetControls(Me, Common.EnumSetControls.Clear)
        dgDetails.DataSource = Nothing
    End Sub

End Class
