Public Class LoanPaymentOld
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
    Friend WithEvents txtKBCI As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPNNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtAmortization As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtDateDue As System.Windows.Forms.TextBox
    Friend WithEvents txtReleaseDate As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtLoanCondition As System.Windows.Forms.TextBox
    Friend WithEvents txtPayStart As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtNewDueDate As System.Windows.Forms.TextBox
    Friend WithEvents txtInt As System.Windows.Forms.TextBox
    Friend WithEvents txtOutsPrin As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblArrears As System.Windows.Forms.Label
    Friend WithEvents txtBalOnPrin As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents lblMember As System.Windows.Forms.Label
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuPayment As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPO As System.Windows.Forms.MenuItem
    Friend WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Friend WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents cboLoanStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents txtPaymentAmount As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtPretInterest As System.Windows.Forms.TextBox
    Friend WithEvents txtPenalty As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtInterest As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPrincipal As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFullAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtArrearsAs As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents mnuList As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.txtKBCI = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPNNo = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtAmortization = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtDateDue = New System.Windows.Forms.TextBox
        Me.txtReleaseDate = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtLoanCondition = New System.Windows.Forms.TextBox
        Me.txtPayStart = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtNewDueDate = New System.Windows.Forms.TextBox
        Me.txtInt = New System.Windows.Forms.TextBox
        Me.txtOutsPrin = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.lblArrears = New System.Windows.Forms.Label
        Me.txtBalOnPrin = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.lblMember = New System.Windows.Forms.Label
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuPayment = New System.Windows.Forms.MenuItem
        Me.mnuPO = New System.Windows.Forms.MenuItem
        Me.mnuList = New System.Windows.Forms.MenuItem
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox
        Me.cboLoanType = New System.Windows.Forms.ComboBox
        Me.cboLoanStatus = New System.Windows.Forms.ComboBox
        Me.cboFrequency = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtArrearsAs = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtPretInterest = New System.Windows.Forms.TextBox
        Me.txtPenalty = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.txtInterest = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtPrincipal = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtFullAmount = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPaymentAmount = New System.Windows.Forms.TextBox
        Me.btnSubmit = New System.Windows.Forms.Button
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.lblTitle = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtKBCI
        '
        Me.txtKBCI.Enabled = False
        Me.txtKBCI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtKBCI.Location = New System.Drawing.Point(142, 16)
        Me.txtKBCI.MaxLength = 9
        Me.txtKBCI.Name = "txtKBCI"
        Me.txtKBCI.ReadOnly = True
        Me.txtKBCI.Size = New System.Drawing.Size(152, 21)
        Me.txtKBCI.TabIndex = 1
        Me.txtKBCI.TabStop = False
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "KBCI No."
        '
        'txtPNNo
        '
        Me.txtPNNo.Enabled = False
        Me.txtPNNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPNNo.Location = New System.Drawing.Point(142, 13)
        Me.txtPNNo.Name = "txtPNNo"
        Me.txtPNNo.ReadOnly = True
        Me.txtPNNo.Size = New System.Drawing.Size(152, 21)
        Me.txtPNNo.TabIndex = 2
        Me.txtPNNo.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(6, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "PN No."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Loan Type"
        '
        'txtTerm
        '
        Me.txtTerm.Enabled = False
        Me.txtTerm.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTerm.Location = New System.Drawing.Point(456, 13)
        Me.txtTerm.MaxLength = 10
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.ReadOnly = True
        Me.txtTerm.Size = New System.Drawing.Size(152, 21)
        Me.txtTerm.TabIndex = 4
        Me.txtTerm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(320, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 33
        Me.Label14.Text = "Term"
        '
        'txtRate
        '
        Me.txtRate.Enabled = False
        Me.txtRate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtRate.Location = New System.Drawing.Point(456, 40)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReadOnly = True
        Me.txtRate.Size = New System.Drawing.Size(152, 21)
        Me.txtRate.TabIndex = 5
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(320, 43)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(36, 13)
        Me.Label15.TabIndex = 35
        Me.Label15.Text = "Rate"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(320, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 13)
        Me.Label10.TabIndex = 39
        Me.Label10.Text = "Freq of Payment"
        '
        'txtAmortization
        '
        Me.txtAmortization.Enabled = False
        Me.txtAmortization.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtAmortization.Location = New System.Drawing.Point(456, 67)
        Me.txtAmortization.Name = "txtAmortization"
        Me.txtAmortization.ReadOnly = True
        Me.txtAmortization.Size = New System.Drawing.Size(152, 21)
        Me.txtAmortization.TabIndex = 7
        Me.txtAmortization.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(320, 74)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(131, 13)
        Me.Label11.TabIndex = 40
        Me.Label11.Text = "Amortization Value"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(6, 43)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(102, 13)
        Me.Label12.TabIndex = 41
        Me.Label12.Text = "Payment Mode"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label17.Location = New System.Drawing.Point(6, 70)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 13)
        Me.Label17.TabIndex = 50
        Me.Label17.Text = "Pay Start"
        '
        'txtDateDue
        '
        Me.txtDateDue.Enabled = False
        Me.txtDateDue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtDateDue.Location = New System.Drawing.Point(142, 94)
        Me.txtDateDue.Name = "txtDateDue"
        Me.txtDateDue.ReadOnly = True
        Me.txtDateDue.Size = New System.Drawing.Size(152, 21)
        Me.txtDateDue.TabIndex = 12
        '
        'txtReleaseDate
        '
        Me.txtReleaseDate.Enabled = False
        Me.txtReleaseDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtReleaseDate.Location = New System.Drawing.Point(142, 40)
        Me.txtReleaseDate.Name = "txtReleaseDate"
        Me.txtReleaseDate.ReadOnly = True
        Me.txtReleaseDate.Size = New System.Drawing.Size(152, 21)
        Me.txtReleaseDate.TabIndex = 10
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(6, 97)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 13)
        Me.Label20.TabIndex = 53
        Me.Label20.Text = "Date Due"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(6, 43)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(92, 13)
        Me.Label18.TabIndex = 51
        Me.Label18.Text = "Release Date"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label19.Location = New System.Drawing.Point(320, 43)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(83, 13)
        Me.Label19.TabIndex = 52
        Me.Label19.Text = "Loan Status"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(320, 101)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(103, 13)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = "Loan Condition"
        '
        'txtLoanCondition
        '
        Me.txtLoanCondition.Enabled = False
        Me.txtLoanCondition.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtLoanCondition.Location = New System.Drawing.Point(456, 94)
        Me.txtLoanCondition.Name = "txtLoanCondition"
        Me.txtLoanCondition.ReadOnly = True
        Me.txtLoanCondition.Size = New System.Drawing.Size(152, 21)
        Me.txtLoanCondition.TabIndex = 9
        '
        'txtPayStart
        '
        Me.txtPayStart.Enabled = False
        Me.txtPayStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPayStart.Location = New System.Drawing.Point(142, 67)
        Me.txtPayStart.Name = "txtPayStart"
        Me.txtPayStart.ReadOnly = True
        Me.txtPayStart.Size = New System.Drawing.Size(152, 21)
        Me.txtPayStart.TabIndex = 11
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(6, 124)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 13)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "Next Due Date"
        '
        'txtNewDueDate
        '
        Me.txtNewDueDate.Enabled = False
        Me.txtNewDueDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtNewDueDate.Location = New System.Drawing.Point(142, 121)
        Me.txtNewDueDate.Name = "txtNewDueDate"
        Me.txtNewDueDate.ReadOnly = True
        Me.txtNewDueDate.Size = New System.Drawing.Size(152, 21)
        Me.txtNewDueDate.TabIndex = 14
        '
        'txtInt
        '
        Me.txtInt.Enabled = False
        Me.txtInt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtInt.Location = New System.Drawing.Point(456, 40)
        Me.txtInt.Name = "txtInt"
        Me.txtInt.ReadOnly = True
        Me.txtInt.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtInt.Size = New System.Drawing.Size(152, 21)
        Me.txtInt.TabIndex = 78
        Me.txtInt.TabStop = False
        '
        'txtOutsPrin
        '
        Me.txtOutsPrin.Enabled = False
        Me.txtOutsPrin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtOutsPrin.Location = New System.Drawing.Point(142, 40)
        Me.txtOutsPrin.Name = "txtOutsPrin"
        Me.txtOutsPrin.ReadOnly = True
        Me.txtOutsPrin.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtOutsPrin.Size = New System.Drawing.Size(152, 21)
        Me.txtOutsPrin.TabIndex = 64
        Me.txtOutsPrin.TabStop = False
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label8.Location = New System.Drawing.Point(6, 43)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(120, 23)
        Me.Label8.TabIndex = 72
        Me.Label8.Text = "Outs. Prin"
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(320, 43)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 23)
        Me.Label9.TabIndex = 73
        Me.Label9.Text = "Outs. Int"
        '
        'lblArrears
        '
        Me.lblArrears.AutoSize = True
        Me.lblArrears.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblArrears.Location = New System.Drawing.Point(320, 16)
        Me.lblArrears.Name = "lblArrears"
        Me.lblArrears.Size = New System.Drawing.Size(93, 13)
        Me.lblArrears.TabIndex = 68
        Me.lblArrears.Text = "Arrears as of"
        '
        'txtBalOnPrin
        '
        Me.txtBalOnPrin.Enabled = False
        Me.txtBalOnPrin.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtBalOnPrin.Location = New System.Drawing.Point(142, 13)
        Me.txtBalOnPrin.MaxLength = 9
        Me.txtBalOnPrin.Name = "txtBalOnPrin"
        Me.txtBalOnPrin.ReadOnly = True
        Me.txtBalOnPrin.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtBalOnPrin.Size = New System.Drawing.Size(152, 21)
        Me.txtBalOnPrin.TabIndex = 59
        Me.txtBalOnPrin.TabStop = False
        '
        'Label24
        '
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label24.Location = New System.Drawing.Point(6, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(120, 23)
        Me.Label24.TabIndex = 61
        Me.Label24.Text = "Bal on Prin"
        '
        'lblMember
        '
        Me.lblMember.AutoSize = True
        Me.lblMember.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMember.Location = New System.Drawing.Point(320, 19)
        Me.lblMember.Name = "lblMember"
        Me.lblMember.Size = New System.Drawing.Size(0, 13)
        Me.lblMember.TabIndex = 79
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuPayment, Me.mnuPO, Me.mnuList})
        '
        'mnuPayment
        '
        Me.mnuPayment.Index = 0
        Me.mnuPayment.Shortcut = System.Windows.Forms.Shortcut.CtrlP
        Me.mnuPayment.Text = "&Payment"
        '
        'mnuPO
        '
        Me.mnuPO.Index = 1
        Me.mnuPO.Shortcut = System.Windows.Forms.Shortcut.CtrlO
        Me.mnuPO.Text = "Payment &Order"
        '
        'mnuList
        '
        Me.mnuList.Index = 2
        Me.mnuList.Shortcut = System.Windows.Forms.Shortcut.CtrlL
        Me.mnuList.Text = "&List"
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboPaymentMode.FormattingEnabled = True
        Me.cboPaymentMode.Location = New System.Drawing.Point(142, 40)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.Size = New System.Drawing.Size(152, 21)
        Me.cboPaymentMode.TabIndex = 81
        Me.cboPaymentMode.TabStop = False
        '
        'cboLoanType
        '
        Me.cboLoanType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboLoanType.FormattingEnabled = True
        Me.cboLoanType.Location = New System.Drawing.Point(142, 13)
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.Size = New System.Drawing.Size(152, 21)
        Me.cboLoanType.TabIndex = 82
        Me.cboLoanType.TabStop = False
        '
        'cboLoanStatus
        '
        Me.cboLoanStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboLoanStatus.FormattingEnabled = True
        Me.cboLoanStatus.Location = New System.Drawing.Point(456, 40)
        Me.cboLoanStatus.Name = "cboLoanStatus"
        Me.cboLoanStatus.Size = New System.Drawing.Size(152, 21)
        Me.cboLoanStatus.TabIndex = 84
        Me.cboLoanStatus.TabStop = False
        '
        'cboFrequency
        '
        Me.cboFrequency.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboFrequency.FormattingEnabled = True
        Me.cboFrequency.Location = New System.Drawing.Point(456, 13)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(152, 21)
        Me.cboFrequency.TabIndex = 85
        Me.cboFrequency.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtKBCI)
        Me.GroupBox1.Controls.Add(Me.lblMember)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 58)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(622, 45)
        Me.GroupBox1.TabIndex = 86
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtPNNo)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtRate)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtTerm)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtAmortization)
        Me.GroupBox2.Controls.Add(Me.txtPayStart)
        Me.GroupBox2.Controls.Add(Me.txtLoanCondition)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.txtReleaseDate)
        Me.GroupBox2.Controls.Add(Me.txtDateDue)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.txtNewDueDate)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 109)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(622, 149)
        Me.GroupBox2.TabIndex = 80
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtArrearsAs)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.txtPretInterest)
        Me.GroupBox3.Controls.Add(Me.txtPenalty)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.txtInterest)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.txtPrincipal)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.txtBalOnPrin)
        Me.GroupBox3.Controls.Add(Me.txtInt)
        Me.GroupBox3.Controls.Add(Me.lblArrears)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtOutsPrin)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 340)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(622, 124)
        Me.GroupBox3.TabIndex = 87
        Me.GroupBox3.TabStop = False
        '
        'txtArrearsAs
        '
        Me.txtArrearsAs.Location = New System.Drawing.Point(456, 13)
        Me.txtArrearsAs.Name = "txtArrearsAs"
        Me.txtArrearsAs.ReadOnly = True
        Me.txtArrearsAs.Size = New System.Drawing.Size(152, 20)
        Me.txtArrearsAs.TabIndex = 94
        Me.txtArrearsAs.TabStop = False
        '
        'Label23
        '
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label23.Location = New System.Drawing.Point(6, 97)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(120, 23)
        Me.Label23.TabIndex = 89
        Me.Label23.Text = "Pret Interest"
        '
        'txtPretInterest
        '
        Me.txtPretInterest.Enabled = False
        Me.txtPretInterest.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPretInterest.Location = New System.Drawing.Point(142, 94)
        Me.txtPretInterest.Name = "txtPretInterest"
        Me.txtPretInterest.ReadOnly = True
        Me.txtPretInterest.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPretInterest.Size = New System.Drawing.Size(152, 21)
        Me.txtPretInterest.TabIndex = 86
        Me.txtPretInterest.TabStop = False
        '
        'txtPenalty
        '
        Me.txtPenalty.Enabled = False
        Me.txtPenalty.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPenalty.Location = New System.Drawing.Point(456, 94)
        Me.txtPenalty.Name = "txtPenalty"
        Me.txtPenalty.ReadOnly = True
        Me.txtPenalty.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPenalty.Size = New System.Drawing.Size(152, 21)
        Me.txtPenalty.TabIndex = 93
        Me.txtPenalty.TabStop = False
        '
        'Label16
        '
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label16.Location = New System.Drawing.Point(320, 69)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(120, 23)
        Me.Label16.TabIndex = 91
        Me.Label16.Text = "Interest"
        '
        'txtInterest
        '
        Me.txtInterest.Enabled = False
        Me.txtInterest.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtInterest.Location = New System.Drawing.Point(456, 67)
        Me.txtInterest.Name = "txtInterest"
        Me.txtInterest.ReadOnly = True
        Me.txtInterest.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtInterest.Size = New System.Drawing.Size(152, 21)
        Me.txtInterest.TabIndex = 88
        Me.txtInterest.TabStop = False
        '
        'Label13
        '
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(6, 70)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 23)
        Me.Label13.TabIndex = 90
        Me.Label13.Text = "Principal"
        '
        'txtPrincipal
        '
        Me.txtPrincipal.Enabled = False
        Me.txtPrincipal.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPrincipal.Location = New System.Drawing.Point(142, 67)
        Me.txtPrincipal.MaxLength = 10
        Me.txtPrincipal.Name = "txtPrincipal"
        Me.txtPrincipal.ReadOnly = True
        Me.txtPrincipal.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPrincipal.Size = New System.Drawing.Size(152, 21)
        Me.txtPrincipal.TabIndex = 87
        Me.txtPrincipal.TabStop = False
        '
        'Label6
        '
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(320, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(120, 23)
        Me.Label6.TabIndex = 92
        Me.Label6.Text = "Penalty"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.txtFullAmount)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtPaymentAmount)
        Me.GroupBox4.Controls.Add(Me.btnSubmit)
        Me.GroupBox4.Location = New System.Drawing.Point(11, 470)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(622, 42)
        Me.GroupBox4.TabIndex = 88
        Me.GroupBox4.TabStop = False
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(6, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 23)
        Me.Label5.TabIndex = 85
        Me.Label5.Text = "Full Amount"
        '
        'txtFullAmount
        '
        Me.txtFullAmount.Enabled = False
        Me.txtFullAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtFullAmount.Location = New System.Drawing.Point(142, 13)
        Me.txtFullAmount.Name = "txtFullAmount"
        Me.txtFullAmount.ReadOnly = True
        Me.txtFullAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtFullAmount.Size = New System.Drawing.Size(152, 21)
        Me.txtFullAmount.TabIndex = 84
        Me.txtFullAmount.TabStop = False
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(320, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 23)
        Me.Label4.TabIndex = 82
        Me.Label4.Text = "Payment Amount"
        '
        'txtPaymentAmount
        '
        Me.txtPaymentAmount.Enabled = False
        Me.txtPaymentAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPaymentAmount.Location = New System.Drawing.Point(456, 13)
        Me.txtPaymentAmount.Name = "txtPaymentAmount"
        Me.txtPaymentAmount.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPaymentAmount.Size = New System.Drawing.Size(123, 21)
        Me.txtPaymentAmount.TabIndex = 81
        '
        'btnSubmit
        '
        Me.btnSubmit.Enabled = False
        Me.btnSubmit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.btnSubmit.Image = Global.Loan.Application.My.Resources.Resources.submit
        Me.btnSubmit.Location = New System.Drawing.Point(585, 11)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(23, 23)
        Me.btnSubmit.TabIndex = 83
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.cboFrequency)
        Me.GroupBox5.Controls.Add(Me.cboLoanStatus)
        Me.GroupBox5.Controls.Add(Me.cboLoanType)
        Me.GroupBox5.Controls.Add(Me.Label10)
        Me.GroupBox5.Controls.Add(Me.cboPaymentMode)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.Label19)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 264)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(622, 70)
        Me.GroupBox5.TabIndex = 84
        Me.GroupBox5.TabStop = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(265, 37)
        Me.lblTitle.TabIndex = 89
        Me.lblTitle.Text = "Loans Payment"
        '
        'LoanPaymentOld
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(645, 524)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Menu = Me.MainMenu1
        Me.Name = "LoanPaymentOld"
        Me.Text = "Loans Payment"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Dim drDetails As DataRow
    Dim drComputed As DataRow

#Region "Subs"

    Private Sub LoadDetails()
        Common.LoadControlDetails(Me, drDetails)
        Me.lblMember.Text = Common.IsDBNull(drDetails.Item("MEMBER"))
        Me.txtKBCI.Text = Common.Format241(Common.IsDBNull(drDetails.Item("KBCI_NO")))
        Me.txtPNNo.Text = Common.Format241(Common.IsDBNull(drDetails.Item("PN_No")))
        Me.txtLoanCondition.Text = IIf(drDetails.Item("PD"), "Past Due", "Current")
        drComputed = Business.LoanPayment.GetFullAmount(Common.IsDBNull(drDetails.Item("PN_No")))
        Common.LoadControlDetails(Me, drComputed)
        Me.txtPaymentAmount.Text = Me.txtFullAmount.Text
        Me.txtArrearsAs.Text = Common.IsDBNull(drComputed.Item("ARREAR_AS"))
    End Sub

    Private Sub ClearFields()
        Common.SetControls(Me, Common.EnumSetControls.Clear)
        Me.lblMember.Text = String.Empty
        Me.lblArrears.Text = String.Empty
    End Sub

#End Region

#Region "Controls"

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If txtPaymentAmount.Text.Trim = String.Empty OrElse Not IsNumeric(Replace(txtPaymentAmount.Text, ",", "")) Then
            MsgBox("ERROR: Payment not numeric", MsgBoxStyle.Critical, "ERROR")
            Exit Sub
        End If

        Dim remarks As String = String.Empty
        Dim message As New System.Text.StringBuilder
        Dim xtfull As Double = drComputed.Item("txtFullAmount")
        Dim xtpart As Double = drComputed.Item("XTPART")
        Dim xfulpay As Boolean = False
        Dim xpayment As Double
        Dim xp As Integer
        Dim xor_no As String = String.Empty
        Dim xsa_no As String = String.Empty
        Dim xpdc_no As String = String.Empty
        Dim xpdc_bnk As String = String.Empty
        Dim sdm As Business.Objects.SavingsDepositMaster

        Dim ds As New DataSet
        Dim dr As DataRow

        xpayment = CDbl(Replace(txtPaymentAmount.Text, ",", ""))

        If xpayment > xtfull Then
            MsgBox("ERROR: No overpayment allowed", MsgBoxStyle.Critical, "ERROR")
            Exit Sub
        ElseIf xpayment = xtfull Then
            MsgBox("Loan will be paid in full", MsgBoxStyle.Information)
            xfulpay = True
        ElseIf xpayment < xtpart Then
            MsgBox("Warning: payment is less than the minimum amount", MsgBoxStyle.Exclamation)
        Else
            If xtpart = 0 And xpayment > 0 Then
                MsgBox("Payment will be applied to the principal", MsgBoxStyle.Information)
            ElseIf xpayment > xtpart Then
                MsgBox("Overpayment will be applied to the principal", MsgBoxStyle.Information)
            End If
        End If

        xp = Common.PopupOptions("OTC", "DM", "PDC", "Cancel", "")
        Select Case xp
            Case 1
                xor_no = String.Empty
                xor_no = InputBox("OR Number")
                If xor_no.Trim() = String.Empty Then Exit Sub
            Case 2
                xsa_no = Common.IsDBNull(drDetails.Item("FEBTC_SA").ToString())
                If xsa_no = String.Empty Then
                    Exit Sub
                Else
                    sdm = Common.GetSavingsAccount(xsa_no, xpayment)
                    If sdm Is Nothing Then
                        Exit Sub
                        xsa_no = String.Empty
                    End If
                End If
                xsa_no = sdm.ACCTNO
            Case 3
                xpdc_no = InputBox("Check Number")
                xpdc_bnk = InputBox("Bank Name")

                If xpdc_no = String.Empty Then MsgBox("ERROR: Check number required", MsgBoxStyle.Critical, "ERROR") : Exit Sub
                If xpdc_bnk = String.Empty Then MsgBox("ERROR: Bank name required", MsgBoxStyle.Critical, "ERROR") : Exit Sub
                If Not Common.IsNum(xpdc_no) Then MsgBox("ERROR: Invalid Check Number", MsgBoxStyle.Critical, "ERROR") : Exit Sub
            Case 4
                Exit Sub
        End Select

        Try
            ds = Business.LoanPayment.ProcessPayment(sysuser, _
                Common.IsDBNull(drDetails.Item("KBCI_NO")), _
                Common.IsDBNull(drDetails.Item("PN_NO")), _
                Common.IsDBNull(drDetails.Item("DATE_DUE")), _
                Common.IsDBNull(drDetails.Item("cboLoanType")), _
                Common.IsDBNullNum(drComputed.Item("LRI_DUE")), _
                Common.IsDBNullNum(drComputed.Item("txtBalOnPrin")), _
                Common.IsDBNullNum(drComputed.Item("XADVINT")), _
                Common.IsDBNull(drComputed.Item("XLASTD")), _
                Common.IsDBNullNum(drComputed.Item("txtPretInterest")), _
                Common.IsDBNullNum(drComputed.Item("txtPrincipal")), _
                Common.IsDBNullNum(drComputed.Item("txtInterest")), _
                Common.IsDBNullNum(drComputed.Item("txtPenalty")), _
                Common.IsDBNullNum(drComputed.Item("txtInt")), _
                Common.IsDBNullNum(drComputed.Item("txtOutsPrin")), _
                xp, _
                CDbl(txtPaymentAmount.Text), _
                xfulpay, _
                xor_no, _
                xpdc_bnk, _
                xpdc_no, _
                xsa_no)

            For Each dr In ds.Tables(0).Rows
                message.AppendLine((dr.Item("MSG")))
            Next

            MessageBox.Show(message.ToString(), "Loans Payment", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)

            If ds.Tables.Count > 1 AndAlso xp <> 1 Then
                Common.OpenReport(Of Report.Voucher.LoansPayment)(ds.Tables(1).Rows(0).Item("PN_NO"), ds.Tables(1).Rows(0).Item("XOR_NO"), sysuser, xp)
            End If

            ClearFields()
            Me.AcceptButton = Nothing
            txtPaymentAmount.Enabled = False
            btnSubmit.Enabled = False
        Catch ex As Exception
            MsgBox("Error: " & ex.Message, MsgBoxStyle.Critical, "ERROR")
        End Try

        ds.Dispose()
    End Sub

    Private Sub mnuPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPayment.Click
        Dim dr As DataRow
        dr = Common.FindActiveLoans("LB-2")

        If dr IsNot Nothing Then
            drDetails = dr
            LoadDetails()
            Common.FormatSeparator(Me)

            AcceptButton = btnSubmit
            btnSubmit.Enabled = True
            txtPaymentAmount.Enabled = True
            txtPaymentAmount.ReadOnly = False
            txtPaymentAmount.Focus()
        End If
    End Sub

    Private Sub mnuPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPO.Click
        drDetails = Common.FindActiveLoans("LB-3")

        If drDetails IsNot Nothing Then
            drComputed = Business.LoanPayment.GetFullAmount(drDetails.Item("PN_NO"))
            'Common.OpenReportOld("s3p_RPT_PaymentOrder_LoansPayment", drDetails.Item("PN_NO").ToString.Trim)
            Common.OpenReport(Of Report.PaymentOrder.Loans)(drDetails.Item("PN_NO"))
            btnSubmit.Enabled = False
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            Common.SetControls(Me, Common.EnumSetControls.Disable)
        End If
    End Sub

    Private Sub mnuList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuList.Click
        'drDetails = Common.FindByList("LB-4", "LB-2", "PN_NO")
        Dim dr As DataRow
        dr = Common.DGVSearch("LB-4", "LB-2", "PN_NO")

        If Not dr Is Nothing Then
            drDetails = dr
            LoadDetails()
            Common.FormatSeparator(Me)

            AcceptButton = btnSubmit
            btnSubmit.Enabled = True
            txtPaymentAmount.Enabled = True
            txtPaymentAmount.ReadOnly = False
            txtPaymentAmount.Focus()
        End If

        'Dim common As New Common
        'Dim frm As New frmCODataGrid

        'frm.dgList.DataSource = common.GetDetails("DUMMY", "LB-4")

        'If Not frm.dgList.DataSource Is Nothing Then
        '    frm.dgList.ReadOnly = True
        '    frm.ControlBox = True
        '    frm.ShowDialog()
        'End If

        'common = Nothing
        'frm = Nothing
    End Sub

    Private Sub txtPaymentAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPaymentAmount.LostFocus
        Common.FormatSeparator(txtPaymentAmount)
    End Sub

#End Region

#Region "Events"

    Private Sub LoanPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.LoadFrequency(cboPaymentMode)
        Common.LoadLoanType(cboLoanType)
        Common.LoadLoanStatus(cboLoanStatus)
        Common.LoadFrequency(cboFrequency)
        Common.SetControls(Me, Common.EnumSetControls.Enable)
        Common.SetControls(Me, Common.EnumSetControls.Read)
    End Sub

#End Region

End Class
