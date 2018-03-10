Public Class LoanStaffPayment
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
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuExtract As System.Windows.Forms.MenuItem
    Friend WithEvents mnuEdit As System.Windows.Forms.MenuItem
    Friend WithEvents mnuPost As System.Windows.Forms.MenuItem
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKBCI As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtDateApplied As System.Windows.Forms.TextBox
    Friend WithEvents txtMI As System.Windows.Forms.TextBox
    Friend WithEvents txtPNNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents cboLedgerType As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtAmortization As System.Windows.Forms.TextBox
    Friend WithEvents cboLoanStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtLoanAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPayStart As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtDateDue As System.Windows.Forms.TextBox
    Friend WithEvents txtReleaseDate As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents grpAdjust As System.Windows.Forms.GroupBox
    Friend WithEvents txtXOAMT As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtXIAMT As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtXPAMT As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnXARRD As System.Windows.Forms.Button
    Friend WithEvents txtXARRD As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtXARRO As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtXARRI As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtXARRP As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents mnuPrint As System.Windows.Forms.MenuItem
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents mnuExit As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoanStaffPayment))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuExtract = New System.Windows.Forms.MenuItem
        Me.mnuEdit = New System.Windows.Forms.MenuItem
        Me.mnuPrint = New System.Windows.Forms.MenuItem
        Me.mnuPost = New System.Windows.Forms.MenuItem
        Me.mnuExit = New System.Windows.Forms.MenuItem
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txtKBCI = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtLName = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtDateApplied = New System.Windows.Forms.TextBox
        Me.txtMI = New System.Windows.Forms.TextBox
        Me.txtPNNo = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtFName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cboLoanType = New System.Windows.Forms.ComboBox
        Me.cboLedgerType = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboFrequency = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtAmortization = New System.Windows.Forms.TextBox
        Me.cboLoanStatus = New System.Windows.Forms.ComboBox
        Me.txtLoanAmount = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtPayStart = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtDateDue = New System.Windows.Forms.TextBox
        Me.txtReleaseDate = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.grpAdjust = New System.Windows.Forms.GroupBox
        Me.txtXOAMT = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtXIAMT = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtXPAMT = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnXARRD = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.txtXARRD = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtXARRO = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtXARRI = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtXARRP = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpAdjust.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuExtract, Me.mnuEdit, Me.mnuPrint, Me.mnuPost, Me.mnuExit})
        '
        'mnuExtract
        '
        Me.mnuExtract.Index = 0
        Me.mnuExtract.Text = "Ex&tract"
        '
        'mnuEdit
        '
        Me.mnuEdit.Index = 1
        Me.mnuEdit.Text = "&Edit"
        '
        'mnuPrint
        '
        Me.mnuPrint.Index = 2
        Me.mnuPrint.Text = "&Print"
        '
        'mnuPost
        '
        Me.mnuPost.Index = 3
        Me.mnuPost.Text = "P&ost"
        '
        'mnuExit
        '
        Me.mnuExit.Index = 4
        Me.mnuExit.Text = "E&xit"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtKBCI)
        Me.GroupBox4.Controls.Add(Me.Label1)
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.txtLName)
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.txtDateApplied)
        Me.GroupBox4.Controls.Add(Me.txtMI)
        Me.GroupBox4.Controls.Add(Me.txtPNNo)
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.txtFName)
        Me.GroupBox4.Controls.Add(Me.Label7)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 55)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(632, 100)
        Me.GroupBox4.TabIndex = 16
        Me.GroupBox4.TabStop = False
        '
        'txtKBCI
        '
        Me.txtKBCI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKBCI.Location = New System.Drawing.Point(148, 13)
        Me.txtKBCI.MaxLength = 9
        Me.txtKBCI.Name = "txtKBCI"
        Me.txtKBCI.ReadOnly = True
        Me.txtKBCI.Size = New System.Drawing.Size(148, 21)
        Me.txtKBCI.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "KBCI No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(326, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "LName"
        '
        'txtLName
        '
        Me.txtLName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLName.Location = New System.Drawing.Point(462, 15)
        Me.txtLName.Name = "txtLName"
        Me.txtLName.ReadOnly = True
        Me.txtLName.Size = New System.Drawing.Size(152, 21)
        Me.txtLName.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(326, 70)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "MI"
        '
        'txtDateApplied
        '
        Me.txtDateApplied.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateApplied.Location = New System.Drawing.Point(148, 67)
        Me.txtDateApplied.Name = "txtDateApplied"
        Me.txtDateApplied.ReadOnly = True
        Me.txtDateApplied.Size = New System.Drawing.Size(148, 21)
        Me.txtDateApplied.TabIndex = 18
        '
        'txtMI
        '
        Me.txtMI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMI.Location = New System.Drawing.Point(462, 68)
        Me.txtMI.Name = "txtMI"
        Me.txtMI.ReadOnly = True
        Me.txtMI.Size = New System.Drawing.Size(152, 21)
        Me.txtMI.TabIndex = 22
        '
        'txtPNNo
        '
        Me.txtPNNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPNNo.Location = New System.Drawing.Point(148, 40)
        Me.txtPNNo.Name = "txtPNNo"
        Me.txtPNNo.ReadOnly = True
        Me.txtPNNo.Size = New System.Drawing.Size(148, 21)
        Me.txtPNNo.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 70)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Date Applied"
        '
        'txtFName
        '
        Me.txtFName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFName.Location = New System.Drawing.Point(462, 42)
        Me.txtFName.Name = "txtFName"
        Me.txtFName.ReadOnly = True
        Me.txtFName.Size = New System.Drawing.Size(152, 21)
        Me.txtFName.TabIndex = 21
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "PN No."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(326, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 24
        Me.Label5.Text = "FName"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.cboPaymentMode)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cboLoanType)
        Me.GroupBox1.Controls.Add(Me.cboLedgerType)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cboFrequency)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 161)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(632, 73)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Loan Type"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(326, 43)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(102, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Payment Mode"
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.Enabled = False
        Me.cboPaymentMode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentMode.ItemHeight = 13
        Me.cboPaymentMode.Location = New System.Drawing.Point(462, 43)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.Size = New System.Drawing.Size(152, 21)
        Me.cboPaymentMode.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(326, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Ledger Type"
        '
        'cboLoanType
        '
        Me.cboLoanType.Enabled = False
        Me.cboLoanType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLoanType.ItemHeight = 13
        Me.cboLoanType.Location = New System.Drawing.Point(142, 16)
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.Size = New System.Drawing.Size(152, 21)
        Me.cboLoanType.TabIndex = 9
        '
        'cboLedgerType
        '
        Me.cboLedgerType.Enabled = False
        Me.cboLedgerType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLedgerType.FormattingEnabled = True
        Me.cboLedgerType.Location = New System.Drawing.Point(462, 16)
        Me.cboLedgerType.Name = "cboLedgerType"
        Me.cboLedgerType.Size = New System.Drawing.Size(152, 21)
        Me.cboLedgerType.TabIndex = 53
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(6, 43)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 13)
        Me.Label10.TabIndex = 57
        Me.Label10.Text = "Freq of Payment"
        '
        'cboFrequency
        '
        Me.cboFrequency.Enabled = False
        Me.cboFrequency.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFrequency.ItemHeight = 13
        Me.cboFrequency.Location = New System.Drawing.Point(142, 43)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(152, 21)
        Me.cboFrequency.TabIndex = 55
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtTerm)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtRate)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.txtAmortization)
        Me.GroupBox2.Controls.Add(Me.cboLoanStatus)
        Me.GroupBox2.Controls.Add(Me.txtLoanAmount)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtPayStart)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.txtDateDue)
        Me.GroupBox2.Controls.Add(Me.txtReleaseDate)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 240)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(632, 125)
        Me.GroupBox2.TabIndex = 58
        Me.GroupBox2.TabStop = False
        '
        'txtTerm
        '
        Me.txtTerm.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerm.Location = New System.Drawing.Point(142, 41)
        Me.txtTerm.MaxLength = 10
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.ReadOnly = True
        Me.txtTerm.Size = New System.Drawing.Size(152, 21)
        Me.txtTerm.TabIndex = 62
        Me.txtTerm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(6, 44)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 64
        Me.Label14.Text = "Term"
        '
        'txtRate
        '
        Me.txtRate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(142, 68)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.ReadOnly = True
        Me.txtRate.Size = New System.Drawing.Size(152, 21)
        Me.txtRate.TabIndex = 63
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(6, 71)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(36, 13)
        Me.Label15.TabIndex = 65
        Me.Label15.Text = "Rate"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(326, 17)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(83, 13)
        Me.Label19.TabIndex = 37
        Me.Label19.Text = "Loan Status"
        '
        'txtAmortization
        '
        Me.txtAmortization.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmortization.Location = New System.Drawing.Point(142, 95)
        Me.txtAmortization.Name = "txtAmortization"
        Me.txtAmortization.ReadOnly = True
        Me.txtAmortization.Size = New System.Drawing.Size(152, 21)
        Me.txtAmortization.TabIndex = 59
        Me.txtAmortization.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboLoanStatus
        '
        Me.cboLoanStatus.Enabled = False
        Me.cboLoanStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLoanStatus.FormattingEnabled = True
        Me.cboLoanStatus.Location = New System.Drawing.Point(462, 14)
        Me.cboLoanStatus.Name = "cboLoanStatus"
        Me.cboLoanStatus.Size = New System.Drawing.Size(152, 21)
        Me.cboLoanStatus.TabIndex = 54
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanAmount.Location = New System.Drawing.Point(142, 14)
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.ReadOnly = True
        Me.txtLoanAmount.Size = New System.Drawing.Size(152, 21)
        Me.txtLoanAmount.TabIndex = 58
        Me.txtLoanAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(6, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "Loan Amount"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 98)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(131, 13)
        Me.Label11.TabIndex = 61
        Me.Label11.Text = "Amortization Value"
        '
        'txtPayStart
        '
        Me.txtPayStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayStart.Location = New System.Drawing.Point(462, 41)
        Me.txtPayStart.Name = "txtPayStart"
        Me.txtPayStart.ReadOnly = True
        Me.txtPayStart.Size = New System.Drawing.Size(152, 21)
        Me.txtPayStart.TabIndex = 56
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(326, 44)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 13)
        Me.Label17.TabIndex = 33
        Me.Label17.Text = "Pay Start"
        '
        'txtDateDue
        '
        Me.txtDateDue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateDue.Location = New System.Drawing.Point(462, 95)
        Me.txtDateDue.Name = "txtDateDue"
        Me.txtDateDue.ReadOnly = True
        Me.txtDateDue.Size = New System.Drawing.Size(152, 21)
        Me.txtDateDue.TabIndex = 19
        '
        'txtReleaseDate
        '
        Me.txtReleaseDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReleaseDate.Location = New System.Drawing.Point(462, 68)
        Me.txtReleaseDate.Name = "txtReleaseDate"
        Me.txtReleaseDate.ReadOnly = True
        Me.txtReleaseDate.Size = New System.Drawing.Size(152, 21)
        Me.txtReleaseDate.TabIndex = 17
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(326, 98)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 13)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "Date Due"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(326, 71)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(92, 13)
        Me.Label18.TabIndex = 35
        Me.Label18.Text = "Release Date"
        '
        'grpAdjust
        '
        Me.grpAdjust.Controls.Add(Me.txtXOAMT)
        Me.grpAdjust.Controls.Add(Me.Label24)
        Me.grpAdjust.Controls.Add(Me.txtXIAMT)
        Me.grpAdjust.Controls.Add(Me.Label23)
        Me.grpAdjust.Controls.Add(Me.txtXPAMT)
        Me.grpAdjust.Controls.Add(Me.Label22)
        Me.grpAdjust.Location = New System.Drawing.Point(12, 371)
        Me.grpAdjust.Name = "grpAdjust"
        Me.grpAdjust.Size = New System.Drawing.Size(305, 148)
        Me.grpAdjust.TabIndex = 96
        Me.grpAdjust.TabStop = False
        '
        'txtXOAMT
        '
        Me.txtXOAMT.Enabled = False
        Me.txtXOAMT.Location = New System.Drawing.Point(142, 64)
        Me.txtXOAMT.Name = "txtXOAMT"
        Me.txtXOAMT.Size = New System.Drawing.Size(152, 20)
        Me.txtXOAMT.TabIndex = 80
        Me.txtXOAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(6, 64)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(56, 13)
        Me.Label24.TabIndex = 81
        Me.Label24.Text = "Penalty"
        '
        'txtXIAMT
        '
        Me.txtXIAMT.Enabled = False
        Me.txtXIAMT.Location = New System.Drawing.Point(142, 40)
        Me.txtXIAMT.Name = "txtXIAMT"
        Me.txtXIAMT.Size = New System.Drawing.Size(152, 20)
        Me.txtXIAMT.TabIndex = 78
        Me.txtXIAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(6, 40)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(60, 13)
        Me.Label23.TabIndex = 79
        Me.Label23.Text = "Interest"
        '
        'txtXPAMT
        '
        Me.txtXPAMT.Enabled = False
        Me.txtXPAMT.Location = New System.Drawing.Point(142, 16)
        Me.txtXPAMT.Name = "txtXPAMT"
        Me.txtXPAMT.Size = New System.Drawing.Size(152, 20)
        Me.txtXPAMT.TabIndex = 76
        Me.txtXPAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(6, 16)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(33, 13)
        Me.Label22.TabIndex = 77
        Me.Label22.Text = "Prin"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnXARRD)
        Me.GroupBox3.Controls.Add(Me.btnSave)
        Me.GroupBox3.Controls.Add(Me.txtXARRD)
        Me.GroupBox3.Controls.Add(Me.Label31)
        Me.GroupBox3.Controls.Add(Me.txtXARRO)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.txtXARRI)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.txtXARRP)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Location = New System.Drawing.Point(333, 371)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(311, 148)
        Me.GroupBox3.TabIndex = 97
        Me.GroupBox3.TabStop = False
        '
        'btnXARRD
        '
        Me.btnXARRD.Enabled = False
        Me.btnXARRD.Image = Global.Loan.Application.My.Resources.Resources.search
        Me.btnXARRD.Location = New System.Drawing.Point(269, 85)
        Me.btnXARRD.Name = "btnXARRD"
        Me.btnXARRD.Size = New System.Drawing.Size(23, 23)
        Me.btnXARRD.TabIndex = 92
        Me.btnXARRD.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Enabled = False
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.Location = New System.Drawing.Point(269, 112)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(23, 23)
        Me.btnSave.TabIndex = 94
        '
        'txtXARRD
        '
        Me.txtXARRD.Enabled = False
        Me.txtXARRD.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXARRD.Location = New System.Drawing.Point(142, 85)
        Me.txtXARRD.Name = "txtXARRD"
        Me.txtXARRD.Size = New System.Drawing.Size(121, 21)
        Me.txtXARRD.TabIndex = 91
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(6, 88)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(93, 13)
        Me.Label31.TabIndex = 93
        Me.Label31.Text = "Arrears as of"
        '
        'txtXARRO
        '
        Me.txtXARRO.Enabled = False
        Me.txtXARRO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXARRO.Location = New System.Drawing.Point(142, 61)
        Me.txtXARRO.Name = "txtXARRO"
        Me.txtXARRO.Size = New System.Drawing.Size(152, 21)
        Me.txtXARRO.TabIndex = 90
        Me.txtXARRO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(6, 64)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(86, 13)
        Me.Label30.TabIndex = 91
        Me.Label30.Text = "Arrears PEN"
        '
        'txtXARRI
        '
        Me.txtXARRI.Enabled = False
        Me.txtXARRI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXARRI.Location = New System.Drawing.Point(142, 37)
        Me.txtXARRI.Name = "txtXARRI"
        Me.txtXARRI.Size = New System.Drawing.Size(152, 21)
        Me.txtXARRI.TabIndex = 88
        Me.txtXARRI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(6, 40)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(84, 13)
        Me.Label29.TabIndex = 89
        Me.Label29.Text = "Arrears INT"
        '
        'txtXARRP
        '
        Me.txtXARRP.Enabled = False
        Me.txtXARRP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXARRP.Location = New System.Drawing.Point(142, 13)
        Me.txtXARRP.Name = "txtXARRP"
        Me.txtXARRP.Size = New System.Drawing.Size(152, 21)
        Me.txtXARRP.TabIndex = 86
        Me.txtXARRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(6, 16)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(83, 13)
        Me.Label28.TabIndex = 87
        Me.Label28.Text = "Arrears PRI"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(352, 37)
        Me.lblTitle.TabIndex = 98
        Me.lblTitle.Text = "Staff Loan Payments"
        '
        'LoanStaffPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(651, 531)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.grpAdjust)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Menu = Me.MainMenu1
        Me.Name = "LoanStaffPayment"
        Me.Text = "Staff Loan Payments"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpAdjust.ResumeLayout(False)
        Me.grpAdjust.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private drDetails As DataRow

#Region "Events"

    Private Sub LoanStaffPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.LoadLoanType(cboLoanType)
        Common.LoadFrequency(cboFrequency)
        Common.LoadFrequency(cboPaymentMode)
        Common.LoadLedgerType(cboLedgerType)
        Common.LoadLoanStatus(cboLoanStatus)
    End Sub

#End Region

#Region "Controls"

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
    End Sub

    Private Sub mnuEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEdit.Click
        If LoadedLoanDetails("LH-1") Then
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            Common.SetControls(Me, Common.EnumSetControls.Enable)
            Common.SetControls(Me, Common.EnumSetControls.Read)

            Me.txtXPAMT.ReadOnly = False
            Me.txtXIAMT.ReadOnly = False
            Me.txtXOAMT.ReadOnly = False
            Me.txtXARRP.ReadOnly = False
            Me.txtXARRI.ReadOnly = False
            Me.txtXARRO.ReadOnly = False
            Me.txtXARRD.ReadOnly = False

            btnXARRD.Enabled = True
            btnSave.Enabled = True
            AcceptButton = btnSave

            Common.LoadControlDetails(Me, drDetails)
            Me.txtKBCI.Text = Common.Format241(drDetails.Item("KBCI_NO"))
            Me.txtPNNo.Text = Common.Format241(drDetails.Item("PN_NO"))
            Common.FormatSeparator(Me)

            txtXPAMT.Focus()
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If txtXARRP.Text = String.Empty Then txtXARRP.Text = "0"
        If txtXARRI.Text = String.Empty Then txtXARRI.Text = "0"
        If txtXARRO.Text = String.Empty Then txtXARRO.Text = "0"
        If txtXPAMT.Text = String.Empty Then txtXPAMT.Text = "0"
        If txtXIAMT.Text = String.Empty Then txtXIAMT.Text = "0"
        If txtXOAMT.Text = String.Empty Then txtXOAMT.Text = "0"

        If IsNumeric(txtXARRP.Text) And IsNumeric(txtXARRI.Text) And IsNumeric(txtXARRO.Text) And IsNumeric(txtXPAMT.Text) And IsNumeric(txtXIAMT.Text) And IsNumeric(txtXOAMT.Text) And (IsDate(txtXARRD.Text) Or txtXARRD.Text = String.Empty) Then
            If Business.LoanStaffPayment.IsEdited(drDetails.Item("PN_NO"), txtXARRP.Text, txtXARRI.Text, txtXARRO.Text, txtXPAMT.Text, txtXIAMT.Text, txtXOAMT.Text, txtXARRD.Text) Then
                MsgBox("Adjustment successfully processed !!!", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("Some amounts not numeric or arreas_as is not a valid date.", MsgBoxStyle.Critical)
        End If

        Common.SetControls(Me, Common.EnumSetControls.Clear)
        Common.SetControls(Me, Common.EnumSetControls.Disable)

        btnXARRD.Enabled = True
        btnSave.Enabled = True
    End Sub

    Private Sub btnXARRD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnXARRD.Click
        Me.txtXARRD.Text = Common.ShowCalendar()
    End Sub

    Private Sub txtXPAMT_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtXPAMT.LostFocus
        Common.FormatSeparator(txtXPAMT)
    End Sub

    Private Sub txtXIAMT_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtXIAMT.LostFocus
        Common.FormatSeparator(txtXIAMT)
    End Sub

    Private Sub txtXOAMT_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtXOAMT.LostFocus
        Common.FormatSeparator(txtXOAMT)
    End Sub

    Private Sub txtXARRP_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtXARRP.LostFocus
        Common.FormatSeparator(txtXARRP)
    End Sub

#End Region

#Region "Subs"

    Private Function LoadedLoanDetails(ByVal Code As String) As Boolean
        Dim dr As DataRow
        dr = Common.FindActiveLoans(Code)

        If dr IsNot Nothing Then
            drDetails = dr
            Return True
        Else
            Return False
        End If
    End Function

#End Region

    Private Sub mnuExtract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExtract.Click
        If sysdate.Day > 7 Then
            MsgBox("ERROR: This can only be done from 1 to 7 of the month.", MsgBoxStyle.Critical)
        Else
            If Business.LoanStaffPayment.IsExtracted() Then
                MsgBox("Adjustment successfully processed !!!", MsgBoxStyle.Information)
            End If
        End If
    End Sub

    Private Sub mnuPost_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPost.Click
        If sysdate.Day <> 7 Then
            MsgBox("ERROR: Revisions can only be posted every 7th day of the month.", MsgBoxStyle.Critical)
        Else
            If MsgBox("WARNING: This will post edited payment records for Staff ... Continue ?", MsgBoxStyle.OkCancel) = MsgBoxResult.Ok Then
                If Business.LoanStaffPayment.IsPosted Then
                    MsgBox("Adjustment successfully processed !!!", MsgBoxStyle.Information)
                End If
            End If
        End If
    End Sub

    Private Sub mnuPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrint.Click
        Common.OpenReport(Of Loan.Application.Report.Loans.StaffPayment)()
    End Sub
End Class
