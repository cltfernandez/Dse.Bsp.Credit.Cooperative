Public Class LoanApplicationOld
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtKBCI As System.Windows.Forms.TextBox
    Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDateApplied As System.Windows.Forms.TextBox
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtCoMaker1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCoMaker2 As System.Windows.Forms.TextBox
    Friend WithEvents mnuNew As System.Windows.Forms.MenuItem
    Friend WithEvents mnuSave As System.Windows.Forms.MenuItem
    Friend WithEvents mnuCancel As System.Windows.Forms.MenuItem
    Friend WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents cboLedgerType As System.Windows.Forms.ComboBox
    Friend WithEvents btnKBCI As System.Windows.Forms.Button
    Friend WithEvents btnCoMaker2 As System.Windows.Forms.Button
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkLRI As System.Windows.Forms.CheckBox
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtCheckNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtPayStart As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtPNNo As System.Windows.Forms.TextBox
    Friend WithEvents txtLoanCondition As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtNetProceeds As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtAmortization As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtReleaseDate As System.Windows.Forms.TextBox
    Friend WithEvents txtDateDue As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtCoMakerName1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCoMakerName2 As System.Windows.Forms.TextBox
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCollateral As System.Windows.Forms.TextBox
    Friend WithEvents txtLoanAmount As System.Windows.Forms.TextBox
    Friend WithEvents btnCoMaker1 As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoanApplicationOld))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuNew = New System.Windows.Forms.MenuItem
        Me.mnuSave = New System.Windows.Forms.MenuItem
        Me.mnuCancel = New System.Windows.Forms.MenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtKBCI = New System.Windows.Forms.TextBox
        Me.txtFullName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtDateApplied = New System.Windows.Forms.TextBox
        Me.cboFrequency = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnKBCI = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cboLedgerType = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cboLoanType = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.chkLRI = New System.Windows.Forms.CheckBox
        Me.txtCoMaker2 = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.txtCoMaker1 = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtBank = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtCheckNo = New System.Windows.Forms.TextBox
        Me.GroupBox5 = New System.Windows.Forms.GroupBox
        Me.txtCoMakerName1 = New System.Windows.Forms.TextBox
        Me.txtCoMakerName2 = New System.Windows.Forms.TextBox
        Me.btnCoMaker1 = New System.Windows.Forms.Button
        Me.btnCoMaker2 = New System.Windows.Forms.Button
        Me.GroupBox6 = New System.Windows.Forms.GroupBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtPayStart = New System.Windows.Forms.TextBox
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtPNNo = New System.Windows.Forms.TextBox
        Me.txtLoanCondition = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtNetProceeds = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtAmortization = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtReleaseDate = New System.Windows.Forms.TextBox
        Me.txtDateDue = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtCollateral = New System.Windows.Forms.TextBox
        Me.btnView = New System.Windows.Forms.Button
        Me.GroupBox7 = New System.Windows.Forms.GroupBox
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtLoanAmount = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuNew, Me.mnuSave, Me.mnuCancel})
        '
        'mnuNew
        '
        Me.mnuNew.Index = 0
        Me.mnuNew.Shortcut = System.Windows.Forms.Shortcut.CtrlN
        Me.mnuNew.Text = "&New"
        '
        'mnuSave
        '
        Me.mnuSave.Index = 1
        Me.mnuSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS
        Me.mnuSave.Text = "&Save"
        '
        'mnuCancel
        '
        Me.mnuCancel.Index = 2
        Me.mnuCancel.Shortcut = System.Windows.Forms.Shortcut.CtrlC
        Me.mnuCancel.Text = "&Cancel"
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(8, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "KBCI No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(8, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Full Name"
        '
        'txtKBCI
        '
        Me.txtKBCI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtKBCI.Location = New System.Drawing.Point(144, 12)
        Me.txtKBCI.MaxLength = 9
        Me.txtKBCI.Name = "txtKBCI"
        Me.txtKBCI.Size = New System.Drawing.Size(118, 21)
        Me.txtKBCI.TabIndex = 1
        '
        'txtFullName
        '
        Me.txtFullName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtFullName.Location = New System.Drawing.Point(144, 39)
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.ReadOnly = True
        Me.txtFullName.Size = New System.Drawing.Size(468, 21)
        Me.txtFullName.TabIndex = 4
        Me.txtFullName.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.Location = New System.Drawing.Point(324, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Date Applied"
        '
        'txtDateApplied
        '
        Me.txtDateApplied.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtDateApplied.Location = New System.Drawing.Point(460, 12)
        Me.txtDateApplied.Name = "txtDateApplied"
        Me.txtDateApplied.ReadOnly = True
        Me.txtDateApplied.Size = New System.Drawing.Size(152, 21)
        Me.txtDateApplied.TabIndex = 2
        Me.txtDateApplied.TabStop = False
        '
        'cboFrequency
        '
        Me.cboFrequency.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboFrequency.Location = New System.Drawing.Point(460, 40)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(152, 21)
        Me.cboFrequency.TabIndex = 5
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label10.Location = New System.Drawing.Point(324, 43)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(115, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Freq of Payment"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(324, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(102, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Payment Mode"
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboPaymentMode.Location = New System.Drawing.Point(460, 13)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.Size = New System.Drawing.Size(152, 21)
        Me.cboPaymentMode.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnKBCI)
        Me.GroupBox1.Controls.Add(Me.txtKBCI)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtDateApplied)
        Me.GroupBox1.Controls.Add(Me.txtFullName)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(626, 67)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'btnKBCI
        '
        Me.btnKBCI.Image = Global.Loan.Application.My.Resources.Resources.search
        Me.btnKBCI.Location = New System.Drawing.Point(268, 11)
        Me.btnKBCI.Name = "btnKBCI"
        Me.btnKBCI.Size = New System.Drawing.Size(28, 21)
        Me.btnKBCI.TabIndex = 7
        Me.btnKBCI.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboLedgerType)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.cboPaymentMode)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.cboLoanType)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.cboFrequency)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 85)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(626, 68)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'cboLedgerType
        '
        Me.cboLedgerType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboLedgerType.FormattingEnabled = True
        Me.cboLedgerType.Location = New System.Drawing.Point(144, 40)
        Me.cboLedgerType.Name = "cboLedgerType"
        Me.cboLedgerType.Size = New System.Drawing.Size(152, 21)
        Me.cboLedgerType.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.Location = New System.Drawing.Point(8, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Loan Type"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.Location = New System.Drawing.Point(8, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Ledger Type"
        '
        'cboLoanType
        '
        Me.cboLoanType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.cboLoanType.Location = New System.Drawing.Point(144, 13)
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.Size = New System.Drawing.Size(152, 21)
        Me.cboLoanType.TabIndex = 2
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label13.Location = New System.Drawing.Point(324, 43)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(116, 13)
        Me.Label13.TabIndex = 56
        Me.Label13.Text = "Exempt from LRI"
        '
        'chkLRI
        '
        Me.chkLRI.Checked = True
        Me.chkLRI.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLRI.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.chkLRI.Location = New System.Drawing.Point(460, 38)
        Me.chkLRI.Name = "chkLRI"
        Me.chkLRI.Size = New System.Drawing.Size(104, 24)
        Me.chkLRI.TabIndex = 9
        '
        'txtCoMaker2
        '
        Me.txtCoMaker2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCoMaker2.Location = New System.Drawing.Point(144, 40)
        Me.txtCoMaker2.Name = "txtCoMaker2"
        Me.txtCoMaker2.Size = New System.Drawing.Size(118, 21)
        Me.txtCoMaker2.TabIndex = 14
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label26.Location = New System.Drawing.Point(8, 43)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(81, 13)
        Me.Label26.TabIndex = 50
        Me.Label26.Text = "Co-Maker 2"
        '
        'txtCoMaker1
        '
        Me.txtCoMaker1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCoMaker1.Location = New System.Drawing.Point(144, 13)
        Me.txtCoMaker1.Name = "txtCoMaker1"
        Me.txtCoMaker1.Size = New System.Drawing.Size(118, 21)
        Me.txtCoMaker1.TabIndex = 12
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label25.Location = New System.Drawing.Point(8, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(81, 13)
        Me.Label25.TabIndex = 49
        Me.Label25.Text = "Co-Maker 1"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label23.Location = New System.Drawing.Point(8, 16)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(39, 13)
        Me.Label23.TabIndex = 59
        Me.Label23.Text = "Bank"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label14.Location = New System.Drawing.Point(324, 16)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 57
        Me.Label14.Text = "Term"
        '
        'txtRate
        '
        Me.txtRate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtRate.Location = New System.Drawing.Point(144, 40)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(152, 21)
        Me.txtRate.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label15.Location = New System.Drawing.Point(8, 43)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(36, 13)
        Me.Label15.TabIndex = 58
        Me.Label15.Text = "Rate"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.Location = New System.Drawing.Point(8, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 52
        Me.Label9.Text = "Loan Amount"
        '
        'txtBank
        '
        Me.txtBank.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtBank.Location = New System.Drawing.Point(144, 13)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(152, 21)
        Me.txtBank.TabIndex = 10
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label24.Location = New System.Drawing.Point(324, 16)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(71, 13)
        Me.Label24.TabIndex = 60
        Me.Label24.Text = "Check No."
        '
        'txtCheckNo
        '
        Me.txtCheckNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCheckNo.Location = New System.Drawing.Point(460, 13)
        Me.txtCheckNo.Name = "txtCheckNo"
        Me.txtCheckNo.Size = New System.Drawing.Size(152, 21)
        Me.txtCheckNo.TabIndex = 11
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.txtCoMakerName1)
        Me.GroupBox5.Controls.Add(Me.txtCoMakerName2)
        Me.GroupBox5.Controls.Add(Me.Label25)
        Me.GroupBox5.Controls.Add(Me.txtCoMaker1)
        Me.GroupBox5.Controls.Add(Me.btnCoMaker1)
        Me.GroupBox5.Controls.Add(Me.btnCoMaker2)
        Me.GroupBox5.Controls.Add(Me.Label26)
        Me.GroupBox5.Controls.Add(Me.txtCoMaker2)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 282)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(626, 69)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        '
        'txtCoMakerName1
        '
        Me.txtCoMakerName1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCoMakerName1.Location = New System.Drawing.Point(327, 13)
        Me.txtCoMakerName1.Name = "txtCoMakerName1"
        Me.txtCoMakerName1.ReadOnly = True
        Me.txtCoMakerName1.Size = New System.Drawing.Size(285, 20)
        Me.txtCoMakerName1.TabIndex = 52
        Me.txtCoMakerName1.TabStop = False
        '
        'txtCoMakerName2
        '
        Me.txtCoMakerName2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCoMakerName2.Location = New System.Drawing.Point(327, 41)
        Me.txtCoMakerName2.Name = "txtCoMakerName2"
        Me.txtCoMakerName2.ReadOnly = True
        Me.txtCoMakerName2.Size = New System.Drawing.Size(285, 20)
        Me.txtCoMakerName2.TabIndex = 51
        Me.txtCoMakerName2.TabStop = False
        '
        'btnCoMaker1
        '
        Me.btnCoMaker1.Image = CType(resources.GetObject("btnCoMaker1.Image"), System.Drawing.Image)
        Me.btnCoMaker1.Location = New System.Drawing.Point(268, 13)
        Me.btnCoMaker1.Name = "btnCoMaker1"
        Me.btnCoMaker1.Size = New System.Drawing.Size(28, 21)
        Me.btnCoMaker1.TabIndex = 13
        Me.btnCoMaker1.UseVisualStyleBackColor = True
        '
        'btnCoMaker2
        '
        Me.btnCoMaker2.Image = CType(resources.GetObject("btnCoMaker2.Image"), System.Drawing.Image)
        Me.btnCoMaker2.Location = New System.Drawing.Point(268, 39)
        Me.btnCoMaker2.Name = "btnCoMaker2"
        Me.btnCoMaker2.Size = New System.Drawing.Size(28, 21)
        Me.btnCoMaker2.TabIndex = 15
        Me.btnCoMaker2.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.Label17)
        Me.GroupBox6.Controls.Add(Me.txtPayStart)
        Me.GroupBox6.Controls.Add(Me.txtStatus)
        Me.GroupBox6.Controls.Add(Me.txtPNNo)
        Me.GroupBox6.Controls.Add(Me.txtLoanCondition)
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.txtNetProceeds)
        Me.GroupBox6.Controls.Add(Me.Label22)
        Me.GroupBox6.Controls.Add(Me.Label19)
        Me.GroupBox6.Controls.Add(Me.txtAmortization)
        Me.GroupBox6.Controls.Add(Me.Label18)
        Me.GroupBox6.Controls.Add(Me.Label20)
        Me.GroupBox6.Controls.Add(Me.txtReleaseDate)
        Me.GroupBox6.Controls.Add(Me.txtDateDue)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Controls.Add(Me.Label11)
        Me.GroupBox6.Location = New System.Drawing.Point(12, 409)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(626, 123)
        Me.GroupBox6.TabIndex = 7
        Me.GroupBox6.TabStop = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label17.Location = New System.Drawing.Point(324, 43)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 13)
        Me.Label17.TabIndex = 55
        Me.Label17.Text = "Pay Start"
        '
        'txtPayStart
        '
        Me.txtPayStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPayStart.Location = New System.Drawing.Point(460, 40)
        Me.txtPayStart.Name = "txtPayStart"
        Me.txtPayStart.ReadOnly = True
        Me.txtPayStart.Size = New System.Drawing.Size(152, 21)
        Me.txtPayStart.TabIndex = 48
        Me.txtPayStart.TabStop = False
        '
        'txtStatus
        '
        Me.txtStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtStatus.Location = New System.Drawing.Point(144, 94)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(152, 21)
        Me.txtStatus.TabIndex = 50
        Me.txtStatus.TabStop = False
        '
        'txtPNNo
        '
        Me.txtPNNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtPNNo.Location = New System.Drawing.Point(144, 13)
        Me.txtPNNo.Name = "txtPNNo"
        Me.txtPNNo.ReadOnly = True
        Me.txtPNNo.Size = New System.Drawing.Size(152, 21)
        Me.txtPNNo.TabIndex = 45
        Me.txtPNNo.TabStop = False
        '
        'txtLoanCondition
        '
        Me.txtLoanCondition.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtLoanCondition.Location = New System.Drawing.Point(460, 94)
        Me.txtLoanCondition.Name = "txtLoanCondition"
        Me.txtLoanCondition.ReadOnly = True
        Me.txtLoanCondition.Size = New System.Drawing.Size(152, 21)
        Me.txtLoanCondition.TabIndex = 54
        Me.txtLoanCondition.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(8, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "PN No."
        '
        'txtNetProceeds
        '
        Me.txtNetProceeds.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtNetProceeds.Location = New System.Drawing.Point(144, 40)
        Me.txtNetProceeds.Name = "txtNetProceeds"
        Me.txtNetProceeds.ReadOnly = True
        Me.txtNetProceeds.Size = New System.Drawing.Size(152, 21)
        Me.txtNetProceeds.TabIndex = 52
        Me.txtNetProceeds.TabStop = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label22.Location = New System.Drawing.Point(324, 97)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(103, 13)
        Me.Label22.TabIndex = 60
        Me.Label22.Text = "Loan Condition"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label19.Location = New System.Drawing.Point(8, 98)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(83, 13)
        Me.Label19.TabIndex = 57
        Me.Label19.Text = "Loan Status"
        '
        'txtAmortization
        '
        Me.txtAmortization.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtAmortization.Location = New System.Drawing.Point(460, 13)
        Me.txtAmortization.Name = "txtAmortization"
        Me.txtAmortization.ReadOnly = True
        Me.txtAmortization.Size = New System.Drawing.Size(152, 21)
        Me.txtAmortization.TabIndex = 46
        Me.txtAmortization.TabStop = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label18.Location = New System.Drawing.Point(8, 70)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(92, 13)
        Me.Label18.TabIndex = 56
        Me.Label18.Text = "Release Date"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label20.Location = New System.Drawing.Point(324, 70)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 13)
        Me.Label20.TabIndex = 58
        Me.Label20.Text = "Date Due"
        '
        'txtReleaseDate
        '
        Me.txtReleaseDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtReleaseDate.Location = New System.Drawing.Point(144, 67)
        Me.txtReleaseDate.Name = "txtReleaseDate"
        Me.txtReleaseDate.ReadOnly = True
        Me.txtReleaseDate.Size = New System.Drawing.Size(152, 21)
        Me.txtReleaseDate.TabIndex = 49
        Me.txtReleaseDate.TabStop = False
        '
        'txtDateDue
        '
        Me.txtDateDue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtDateDue.Location = New System.Drawing.Point(460, 67)
        Me.txtDateDue.Name = "txtDateDue"
        Me.txtDateDue.ReadOnly = True
        Me.txtDateDue.Size = New System.Drawing.Size(152, 21)
        Me.txtDateDue.TabIndex = 51
        Me.txtDateDue.TabStop = False
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label21.Location = New System.Drawing.Point(8, 43)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(93, 13)
        Me.Label21.TabIndex = 59
        Me.Label21.Text = "Net Proceeds"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label11.Location = New System.Drawing.Point(324, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(131, 13)
        Me.Label11.TabIndex = 53
        Me.Label11.Text = "Amortization Value"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.txtCollateral)
        Me.GroupBox4.Controls.Add(Me.btnView)
        Me.GroupBox4.Location = New System.Drawing.Point(11, 357)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(626, 46)
        Me.GroupBox4.TabIndex = 6
        Me.GroupBox4.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.Location = New System.Drawing.Point(9, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 54
        Me.Label5.Text = "Collateral"
        Me.Label5.Visible = False
        '
        'txtCollateral
        '
        Me.txtCollateral.Enabled = False
        Me.txtCollateral.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtCollateral.Location = New System.Drawing.Point(144, 13)
        Me.txtCollateral.Name = "txtCollateral"
        Me.txtCollateral.Size = New System.Drawing.Size(433, 21)
        Me.txtCollateral.TabIndex = 53
        Me.txtCollateral.Text = "CTD # "
        Me.txtCollateral.Visible = False
        '
        'btnView
        '
        Me.btnView.Image = Global.Loan.Application.My.Resources.Resources.open
        Me.btnView.Location = New System.Drawing.Point(585, 12)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(28, 21)
        Me.btnView.TabIndex = 18
        Me.btnView.UseVisualStyleBackColor = True
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtTerm)
        Me.GroupBox7.Controls.Add(Me.Label13)
        Me.GroupBox7.Controls.Add(Me.chkLRI)
        Me.GroupBox7.Controls.Add(Me.txtLoanAmount)
        Me.GroupBox7.Controls.Add(Me.txtRate)
        Me.GroupBox7.Controls.Add(Me.Label15)
        Me.GroupBox7.Controls.Add(Me.Label14)
        Me.GroupBox7.Controls.Add(Me.Label9)
        Me.GroupBox7.Location = New System.Drawing.Point(12, 159)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(625, 68)
        Me.GroupBox7.TabIndex = 3
        Me.GroupBox7.TabStop = False
        '
        'txtTerm
        '
        Me.txtTerm.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtTerm.Location = New System.Drawing.Point(460, 13)
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.Size = New System.Drawing.Size(152, 21)
        Me.txtTerm.TabIndex = 7
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.txtCheckNo)
        Me.GroupBox3.Controls.Add(Me.txtBank)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 233)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(625, 43)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold)
        Me.txtLoanAmount.Location = New System.Drawing.Point(144, 13)
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.Size = New System.Drawing.Size(152, 21)
        Me.txtLoanAmount.TabIndex = 6
        '
        'LoanApplication
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(649, 545)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Menu = Me.MainMenu1
        Me.Name = "LoanApplication"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LOANS FORM"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

#Region "Declarations"
    Private strID As String

    Private ds As DataSet
    Private dt As DataTable
    Private dr As DataRow
    Private dtSTL As DataTable
    Private dtSDEDBAL As DataTable
    Private dtCTD As DataTable
    Private dtMemberList As DataTable
    Private dtCTDList As DataTable
    Private drCTRL As DataRow
    Private drMember As DataRow
    Private drLoansBlank As DataRow
    Private mterm As Double = 0
    Private reyt As Integer = 0

    Private xplnamt As Double = 0
    Private xtotamt As Double = 0
    Private amortint As Double = 0
    Private ylri As Double = 0
    Private max As Double = 0
    Private min As Double = 0

    Private xrenew As Boolean = False
    Private xrenew1 As Boolean = False
    Private xrenew2 As Boolean = False
    Private xrenewSTL As Boolean = False

    Private resign As Boolean = False
    Private dulin As Boolean = False

    Private boDgvCollateral As Boolean = False
#End Region

    Private Sub LoanApplication_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCBO()
        NewForm()
    End Sub

    Private Sub mnuNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNew.Click
        NewRecord()
    End Sub

    Private Sub mnuCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCancel.Click
        NewForm()
    End Sub

    Private Sub mnuSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        SaveRecord()
    End Sub

    Private Sub tsbNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        NewRecord()
    End Sub

    Private Sub tsbSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SaveRecord()
    End Sub

    Private Sub tsbCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        NewForm()
    End Sub

    Private Sub btnKBCI_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKBCI.Click
        Dim dr As DataRow
        dr = getKBCIDetails()

        If dr Is Nothing Then
            Exit Sub
        Else
            txtKBCI.Text = dr.Item("KBCI_NO")
            txtFullName.Text = dr.Item("FULL_NAME")
        End If
    End Sub

    Private Function getKBCIDetails() As DataRow
        Dim dt As New DataTable
        Dim form As New PopupSearchDr
        Dim dr As DataRow

        If Not dtMemberList Is Nothing Then
            dt = dtMemberList
        Else
            dt = Common.GetDetailsOld("MemberList", "%")
            dtMemberList = dt
        End If

        form.dgvList.DataSource = dt
        form.ShowDialog()

        If Not form.Record Is Nothing Then
            dr = form.Record
        Else
            dr = Nothing
        End If

        dt = Nothing
        form = Nothing

        Return dr
    End Function

    Private Sub btnCoMaker1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCoMaker1.Click
        Dim dt As New DataTable
        Dim form As New PopupSearchDr

        If Not dtMemberList Is Nothing Then
            dt = dtMemberList
        Else
            dt = Common.GetDetailsOld("MemberList", "")
            dtMemberList = dt
        End If

        form.dgvList.DataSource = dt
        form.ShowDialog()

        If Not form.Record Is Nothing Then
            txtCoMaker1.Text = form.Record.Item("KBCI_NO")
            txtCoMakerName1.Text = form.Record.Item("FULL_NAME")
        End If

        dt = Nothing
        form = Nothing
    End Sub

    Private Sub btnCoMaker2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCoMaker2.Click
        Dim dt As New DataTable
        Dim form As New PopupSearchDr

        If Not dtMemberList Is Nothing Then
            dt = dtMemberList
        Else
            dt = Common.GetDetailsOld("MemberList", "")
            dtMemberList = dt
        End If

        form.dgvList.DataSource = dt
        form.ShowDialog()

        If Not form.Record Is Nothing Then
            txtCoMaker2.Text = form.Record.Item("KBCI_NO")
            txtCoMakerName2.Text = form.Record.Item("FULL_NAME")
        End If

        dt = Nothing
        form = Nothing
    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click
        'If txtFullName.Text.Trim = String.Empty Then Exit Sub

        Dim dr As DataRow
        Dim kbcino As String = String.Empty


        Select Case MessageBox.Show("Use " & txtFullName.Text & "'s CTD?", "Inquiry", MessageBoxButtons.YesNoCancel)
            Case Windows.Forms.DialogResult.Cancel
                Exit Sub
            Case Windows.Forms.DialogResult.Yes
                kbcino = txtKBCI.Text.Trim
            Case Windows.Forms.DialogResult.No
                kbcino = getKBCIDetails().Item("KBCI_NO")
                If kbcino = String.Empty Then
                    Exit Sub
                End If
        End Select

        dr = Common.FindByListOld("Collaterals", kbcino)

        If dr Is Nothing Then
            Exit Sub
        End If

        If txtCollateral.Text.Trim().Length() = 5 Then
            txtCollateral.Text = "CTD # " & dr.Item("CTD_NO")
        Else
            If txtCollateral.Text.Contains(dr.Item("CTD_NO").ToString()) Then
                MsgBox("CTD tagged already")
                Exit Sub
            Else
                txtCollateral.Text = txtCollateral.Text & "/" & dr.Item("CTD_NO")
            End If
        End If
    End Sub

    Private Sub txtTerm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTerm.TextChanged
        Dim dRate As Double

        If Not IsNumeric(txtTerm.Text.Trim) Then
            Exit Sub
        End If

        dRate = Common.GetLoanRate(cboLoanType.SelectedValue, CInt(txtTerm.Text))

        If dRate > 0 Then
            txtRate.Text = dRate.ToString()
        Else
            Select Case CInt(txtTerm.Text)
                Case 12
                    txtRate.Text = "9"
                Case 24
                    txtRate.Text = "10"
                Case 30
                    txtRate.Text = "17"
                Case 36
                    txtRate.Text = "11"
                Case 48
                    txtRate.Text = "12"
                Case 60
                    txtRate.Text = "13"
                Case 120
                    txtRate.Text = "17"
                Case Else
                    txtRate.Text = "17"
            End Select
        End If
    End Sub

    Private Sub txtKBCI_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKBCI.LostFocus
        If txtKBCI.Text.Trim = String.Empty Then
            Exit Sub
        Else
            Try
                GetName(txtFullName, txtKBCI.Text.Trim)
            Catch ex As Exception
                dt = New DataTable
            End Try
        End If
    End Sub

    Private Sub txtCoMaker1_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCoMaker1.LostFocus
        If txtCoMaker1.Text.Trim = String.Empty Then
            Exit Sub
        Else
            GetName(txtCoMakerName1, txtCoMaker1.Text.Trim)
        End If
    End Sub

    Private Sub txtCoMaker2_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCoMaker2.LostFocus
        If txtCoMaker2.Text.Trim = String.Empty Then
            Exit Sub
        Else
            GetName(txtCoMakerName2, txtCoMaker2.Text.Trim)
        End If
    End Sub

    Private Sub txtLoanAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLoanAmount.LostFocus
        If IsNumeric(txtLoanAmount.Text.Trim) Then txtLoanAmount.Text = Format(CDbl(txtLoanAmount.Text.Trim), "#,##0.00")
    End Sub

    Private Sub cboLoanType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLoanType.SelectedIndexChanged
        If cboLoanType.SelectedIndex = 0 Then
            txtRate.Text = "0"
            cboFrequency.SelectedIndex = 0
            Exit Sub
        ElseIf cboLoanType.SelectedValue = "STL" Then
            txtCollateral.Enabled = True
            btnView.Enabled = True
            chkLRI.Checked = True
            cboPaymentMode.SelectedValue = "3"
        Else
            txtCollateral.Enabled = True
            btnView.Enabled = False
            chkLRI.Checked = False
            cboPaymentMode.SelectedValue = ""
            cboPaymentMode.SelectedValue = "1"
        End If

        Dim dt As New DataTable
        dt = Common.GetDetailsOld("PARAM", cboLoanType.SelectedValue)

        If dt.Rows.Count = 0 Then
            MsgBox("ERROR: Loan type is not in parameter file!", MsgBoxStyle.Critical, "Error")
        Else
            min = CDbl(dt.Rows(0).Item("MIN"))
            max = CDbl(dt.Rows(0).Item("MAX"))
            cboFrequency.SelectedValue = CStr(dt.Rows(0).Item("FREQ"))
            txtTerm.Text = CDbl(dt.Rows(0).Item("TERM"))
            txtRate.Text = CDbl(dt.Rows(0).Item("RATE"))
        End If
    End Sub

    Private Function lontayp() As Boolean
        If resign And drLoansBlank.Item("LOAN_TYPE") <> "STL" Then
            MsgBox("Only STL is available for resigned members.", MsgBoxStyle.Exclamation)
            Return False
        End If

        If Common.GetDetailsOld("LE-2", drMember.Item("KBCI_NO").ToString).Rows.Count > 0 Then
            MsgBox("ERROR: Borrower already has an existing Restructured Loan.", MsgBoxStyle.Critical, "Error")
            Return False
        End If

        drLoansBlank.Item("RENEW") = False

        If cboLoanType.SelectedValue <> "STL" AndAlso cboLoanType.SelectedValue <> "SML" Then
            ds = Common.GetDetailsAsDataSet(cboLoanType.SelectedValue & txtKBCI.Text.Trim & "R", "LA-2")

            'added, not in original
            drLoansBlank.Item("L_EXT") = 0

            If ds.Tables(0).Rows.Count > 0 Then
                MsgBox("Warning! Borrower has existing loan of the same type.", MsgBoxStyle.Exclamation)
                drLoansBlank.Item("RENEW") = True
                xplnamt = ds.Tables(0).Rows(0).Item("XPLNAMT")
                xtotamt = ds.Tables(1).Rows(0).Item("XTOTAMT")
            End If
        Else
            xrenew = False
            drLoansBlank.Item("L_EXT") = 0
            If MsgBox("Renewal of existing " & cboLoanType.SelectedValue & "?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                'xrenewSTL = True
                xrenewSTL = False
                xrenew2 = True
                drLoansBlank.Item("L_EXT") = 1
            Else
                If drMember.Item("MEM_STAT") <> "S" AndAlso MsgBox("Would you like to exempt Share Capital?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    xrenew2 = True
                End If
            End If
        End If

        dt = Nothing

        Return True
    End Function

    Private Function lonamt() As Boolean
        If IsNumeric(drLoansBlank.Item("PRINCIPAL")) AndAlso CDbl(drLoansBlank.Item("PRINCIPAL")) + xtotamt > CDbl(Common.IsDBNullNum(drCTRL.Item("CEILING"))) Then
            MsgBox("You are already over the prescribed Aggregate Ceiling.", MsgBoxStyle.Exclamation)
            Return False
        End If

        If IsNumeric(drLoansBlank.Item("PRINCIPAL")) AndAlso CDbl(drLoansBlank.Item("PRINCIPAL")) = 0 Then
            Return False
        End If

        Return True
    End Function

    Private Function lblfreq() As Boolean
        Dim freq As String = drLoansBlank.Item("FREQ")

        If drLoansBlank.Item("LED_TYPE") = 1 And drLoansBlank.Item("FREQ") <> "M" Then
            MsgBox("For ledger type 1, frequency should be monthly.", MsgBoxStyle.Exclamation)
            Return False
        End If

        mterm = drLoansBlank.Item("TERM")

        Select Case freq
            Case "M"
                reyt = 12
                mterm = mterm
            Case "Q"
                If CInt(drLoansBlank("TERM")) < 3 Then
                    MsgBox("ERROR: Term should be at least 3 months.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
                If CInt(drLoansBlank("TERM")) Mod 3 > 0 Then
                    MsgBox("ERROR: Term should be in multiples of 3 months.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
                reyt = 4
                mterm = mterm / 3
            Case "S"
                If CInt(drLoansBlank("TERM")) < 6 Then
                    MsgBox("ERROR: Term should be at least 6 months.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
                If CInt(drLoansBlank("TERM")) Mod 6 > 0 Then
                    MsgBox("ERROR: Term should be in multiples of 6 months.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
                reyt = 2
                mterm = mterm / 6
            Case "A"
                If CInt(drLoansBlank("TERM")) < 12 Then
                    MsgBox("ERROR: Term should be at least 12 months.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
                If CInt(drLoansBlank("TERM")) Mod 12 > 0 Then
                    MsgBox("ERROR: Term should be in multiples of 12 months.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
                reyt = 1
                mterm = mterm / 12
            Case "D"
                If CInt(drLoansBlank("TERM")) > 30 Then
                    MsgBox("ERROR: Term should not be greater than 30 days.", MsgBoxStyle.Critical, "Error")
                    Return False
                End If
                reyt = 360
                mterm = CInt(drLoansBlank("TERM"))
        End Select

        Return True
    End Function

    Private Function lterm() As Boolean
        Dim xxa As Double = CDbl(drLoansBlank.Item("TERM")) / 12
        If drLoansBlank.Item("LED_TYPE") = 1 AndAlso (xxa - Int(xxa)) > 0 Then
            MsgBox("For One Time Deduction, Loan Term should always fall in a full year.", MsgBoxStyle.Exclamation)
            Return False
        End If
        Return True
    End Function

    Private Function inte() As Boolean
        Dim strFreq As String = CStr(drLoansBlank.Item("FREQ"))
        Dim tmpDate As Date

        Select Case strFreq
            Case "D"
                reyt = 360
                mterm = CDbl(drLoansBlank.Item("TERM"))
            Case "M"
                reyt = 12
                mterm = CDbl(drLoansBlank.Item("TERM"))
            Case "Q"
                reyt = 4
                mterm = CDbl(drLoansBlank.Item("TERM")) / 3
            Case "S"
                reyt = 2
                mterm = CDbl(drLoansBlank.Item("TERM")) / 6
            Case "A"
                reyt = 1
                mterm = CDbl(drLoansBlank.Item("TERM")) / 12
        End Select

        If cboPaymentMode.SelectedValue = "1" Then
            tmpDate = sysdate

            If sysdate.Day <= 15 Then
                tmpDate = sysdate.AddMonths(1)
            Else
                tmpDate = sysdate.AddMonths(2)
            End If

            tmpDate = New Date(tmpDate.Year, tmpDate.Month, CDate(drCTRL.Item("PAY_DAY")).Day)
            drLoansBlank.Item("PAY_START") = tmpDate
        Else
            drLoansBlank.Item("PAY_START") = CDate(drLoansBlank.Item("CHKNO_DATE")).AddMonths(1)

            If drLoansBlank.Item("FREQ") = "D" Then
                drLoansBlank.Item("PAY_START") = CDate(drLoansBlank.Item("CHKNO_DATE")).AddDays(CDbl(drLoansBlank.Item("TERM")))
            End If

            If (drLoansBlank.Item("TERM") = 1 And drLoansBlank.Item("FREQ") = "M") Or drLoansBlank.Item("FREQ") = "D" Then
                If CDate(drLoansBlank.Item("PAY_START")).DayOfWeek = 1 Then
                    drLoansBlank.Item("PAY_START") = CDate(drLoansBlank.Item("PAY_START")).AddDays(1)
                ElseIf CDate(drLoansBlank.Item("PAY_START")).DayOfWeek = 7 Then
                    drLoansBlank.Item("PAY_START") = CDate(drLoansBlank.Item("PAY_START")).AddDays(2)
                End If
            End If
        End If

        If drLoansBlank.Item("LOAN_TYPE") = "STL" And ((drLoansBlank.Item("FREQ") = "D" And drLoansBlank.Item("TERM") >= 30) Or (drLoansBlank.Item("FREQ") = "M" And drLoansBlank.Item("TERM") = 1)) Then
            drLoansBlank.Item("DATE_DUE") = drLoansBlank.Item("PAY_START")
        Else
            drLoansBlank.Item("DATE_DUE") = Common.GoDueOld(drLoansBlank.Item("PAY_START"), drLoansBlank.Item("TERM"), drLoansBlank.Item("FREQ"))
        End If

        If drLoansBlank.Item("FREQ") = "D" Then
            If drLoansBlank.Item("TERM") < DateDiff(DateInterval.Day, CDate(drLoansBlank.Item("CHKNO_DATE")), CDate(drLoansBlank.Item("DATE_DUE"))) Then
                drLoansBlank.Item("TERM") = DateDiff(DateInterval.Day, CDate(drLoansBlank.Item("CHKNO_DATE")), CDate(drLoansBlank.Item("DATE_DUE")))
            End If
        End If

        AdjustSTLTerm()

        If drLoansBlank.Item("LED_TYPE") <> "1" Then
            drLoansBlank.Item("AMORT_AMT") = Common.Payment(drLoansBlank.Item("PRINCIPAL"), (drLoansBlank.Item("RATE") / 100) / reyt, mterm)
            If drLoansBlank.Item("FREQ").ToString = "M" And CInt(drLoansBlank.Item("TERM")) = 1 Then
                amortint = CDbl(drLoansBlank.Item("PRINCIPAL")) * (CDbl(drLoansBlank.Item("RATE")) / 100 / 360) * DateDiff(DateInterval.Day, drLoansBlank.Item("CHKNO_DATE"), drLoansBlank.Item("DATE_DUE"))
                drLoansBlank.Item("AMORT_AMT") = drLoansBlank.Item("PRINCIPAL") + amortint
            End If
            If drLoansBlank.Item("FREQ").ToString = "D" Then
                amortint = CDbl(drLoansBlank.Item("PRINCIPAL")) * (CDbl(drLoansBlank.Item("RATE")) / 100 / 360) * drLoansBlank.Item("TERM")
                drLoansBlank.Item("AMORT_AMT") = drLoansBlank.Item("PRINCIPAL") + amortint
            End If
        End If

        txtFullName.Text = _
            IIf(drMember.Item("LNAME") Is DBNull.Value OrElse drMember.Item("LNAME") = String.Empty, String.Empty, drMember.Item("LNAME").ToString & ", ") & _
            IIf(drMember.Item("FNAME") Is DBNull.Value OrElse drMember.Item("FNAME") = String.Empty, String.Empty, drMember.Item("FNAME").ToString & " ") & _
            IIf(drMember.Item("MI") Is DBNull.Value OrElse drMember.Item("MI") = String.Empty, String.Empty, drMember.Item("MI").ToString & ".")
        Return True
    End Function

    Private Function svalctd() As Boolean
        'TODO
        'Dim dr As DataRow

        'For Each dr In CType(dgvCollateral.DataSource, DataTable).Rows
        '    If CDate(dr.Item("DUE_DATE")) < CDate(txtDateDue.Text) Then
        '        MsgBox("CTD due date is less than loan due date!", MsgBoxStyle.Exclamation)
        '    End If
        'Next
    End Function

    Private Sub SetReadOnly()
        txtDateApplied.ReadOnly = True
        txtFullName.ReadOnly = True
        txtPNNo.ReadOnly = True
        txtAmortization.ReadOnly = True
        txtNetProceeds.ReadOnly = True
        txtPayStart.ReadOnly = True
        txtReleaseDate.ReadOnly = True
        txtDateDue.ReadOnly = True
        txtStatus.ReadOnly = True
        txtLoanCondition.ReadOnly = True
        'txtCoMaker1.ReadOnly = True
        'txtCoMaker2.ReadOnly = True
        txtCoMakerName1.ReadOnly = True
        txtCoMakerName2.ReadOnly = True
    End Sub

    Private Sub ClearVars(Optional ByVal ALL As Boolean = True)
        If ALL Then
            strID = String.Empty
            min = 0
            max = 0
        End If

        ds = New DataSet
        dt = New DataTable
        dtSTL = New DataTable
        dtSDEDBAL = New DataTable
        dtMemberList = New DataTable
        dtCTDList = New DataTable
        dtCTD = New DataTable

        dtMemberList = Nothing
        dtCTDList = Nothing

        mterm = 0
        reyt = 0

        xplnamt = 0
        xtotamt = 0
        amortint = 0
        ylri = 0

        xrenew = False
        xrenew1 = False
        xrenew2 = False
        xrenewSTL = False
        resign = False
        dulin = False
    End Sub

    Private Sub LoadCBO()
        Common.LoadFrequency(cboFrequency)
        Common.LoadPaymentMode(cboPaymentMode)
        Common.LoadLedgerType(cboLedgerType)
        Common.LoadLoanType(cboLoanType)
    End Sub

    Private Sub NewForm()

        ClearVars()
        Common.SetControls(Me, Common.EnumSetControls.ReadWrite)
        Common.SetControls(Me, Common.EnumSetControls.Clear)
        Common.SetControls(Me, Common.EnumSetControls.Disable)
        SetReadOnly()
        btnKBCI.Enabled = False
        btnCoMaker1.Enabled = False
        btnCoMaker2.Enabled = False
        btnView.Enabled = False

        txtCollateral.Visible = Not boDgvCollateral
        txtCollateral.Text = "CTD # "
        Label5.Visible = Not boDgvCollateral

        mnuNew.Enabled = True
        mnuSave.Enabled = False
        mnuCancel.Enabled = False
        strID = sysuser & Format(Now, "yyyyMMddhhmmss")
    End Sub

    Private Sub SDEDBAL()
        ds = Common.GetSDEDBAL(drMember.Item("KBCI_NO").ToString, drLoansBlank.Item("LOAN_TYPE"), xrenewSTL, strID, sysuser)
        dtSDEDBAL = ds.Tables(0)
        ylri = Common.IsDBNullNum(ds.Tables(1).Rows(0).Item("YLRI"))

        Do While True
            Select Case Common.PopupOptions("Add charges to new loan", "Deduct Other Loans", "Continue", "")
                Case 1
                    Dim formODEDBAL As New LoanApplicationOdedbal
                    formODEDBAL.KBCI_NO = drMember.Item("KBCI_NO").ToString
                    formODEDBAL.PN_NO = 0
                    formODEDBAL.ID = strID
                    formODEDBAL.LoanType = cboLoanType.SelectedValue
                    formODEDBAL.ShowDialog()
                    formODEDBAL = Nothing
                Case 2
                    Dim formSDEDBAL As New LoanApplicationSdedbal
                    Dim intX As Integer = 0

                    formSDEDBAL.ID = strID
                    formSDEDBAL.dgvList.DataSource = dtSDEDBAL
                    formSDEDBAL.ShowDialog()
                    If formSDEDBAL.yearly_lri > 0 Then
                        ylri += formSDEDBAL.yearly_lri
                    End If
                Case 3
                    Exit Do
            End Select
        Loop

    End Sub

    Private Sub GetName(ByVal txtBox As TextBox, ByVal KBCI_No As String)
        dt = New DataTable
        dt = Common.GetDetailsOld("Member", KBCI_No)

        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            txtBox.Text = IIf(dr.Item("LNAME") Is DBNull.Value OrElse dr.Item("LNAME").ToString.Trim = String.Empty, String.Empty, dr.Item("LNAME").ToString.Trim & ", ")
            txtBox.Text &= IIf(dr.Item("FNAME") Is DBNull.Value OrElse dr.Item("FNAME").ToString.Trim = String.Empty, String.Empty, dr.Item("FNAME").ToString.Trim & " ")
            txtBox.Text &= IIf(dr.Item("MI") Is DBNull.Value OrElse dr.Item("MI").ToString.Trim = String.Empty, String.Empty, dr.Item("MI").ToString.Trim & ".")
        End If
    End Sub

    Private Sub NewRecord()
        'If MsgBox("Add a new loan to file?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '    Exit Sub
        'End If

        btnKBCI.Enabled = True
        btnCoMaker1.Enabled = True
        btnCoMaker2.Enabled = True
        'btnView.Enabled = True
        btnView.Enabled = False

        mnuNew.Enabled = False
        mnuSave.Enabled = True
        mnuCancel.Enabled = True

        Common.SetControls(Me, Common.EnumSetControls.Enable)
        cboLedgerType.SelectedValue = "0"
        cboPaymentMode.SelectedIndex = 1
        txtKBCI.Focus()
    End Sub

    Private Sub AdjustSTLTerm()
        'adjust term
        'adjust date due before computing amortization

        Dim dtDue As Date = sysdate.AddDays(30)
        If cboLoanType.SelectedValue = "STL" And cboFrequency.SelectedValue = "D" And txtTerm.Text = "30" Then
            Select Case dtDue.DayOfWeek
                Case DayOfWeek.Saturday
                    drLoansBlank.Item("TERM") = 32
                    drLoansBlank.Item("DATE_DUE") = CDate(drLoansBlank.Item("DATE_DUE")).AddDays(2)
                Case DayOfWeek.Sunday
                    drLoansBlank.Item("TERM") = 31
                    drLoansBlank.Item("DATE_DUE") = CDate(drLoansBlank.Item("DATE_DUE")).AddDays(1)
            End Select
        End If
    End Sub

    Private Sub SaveRecord()
        Dim pnNumber As String = String.Empty
        Dim message As String = String.Empty
        Dim term As Integer
        Dim dblX As Double = 0

        ClearVars(False)

        If cboFrequency.SelectedIndex = 0 Then
            MsgBox("Invalid frequency.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf cboLedgerType.SelectedIndex = 0 Then
            MsgBox("Invalid ledger type.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf cboLoanType.SelectedIndex = 0 Then
            MsgBox("Invalid loan type.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf cboPaymentMode.SelectedIndex = 0 Then
            MsgBox("Invalid payment mode.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf txtLoanAmount.Text = String.Empty OrElse Not IsNumeric(txtLoanAmount.Text) Then
            MsgBox("Invalid loan amount.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf CDbl(txtLoanAmount.Text) < min OrElse CDbl(txtLoanAmount.Text) > max Then
            MsgBox("Loan amount range is " & Format(min, "#,##0.00") & " - " & Format(max, "#,##0.00"), MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf txtRate.Text = String.Empty OrElse Not IsNumeric(txtRate.Text) Then
            MsgBox("Invalid rate.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf txtBank.Text = String.Empty Then
            MsgBox("Bank is required.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf txtCheckNo.Text = String.Empty Then
            MsgBox("Check number is required.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtKBCI.Text.Trim = String.Empty Then
            MsgBox("KBCI No is required.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf txtKBCI.Text.Trim = txtCoMaker1.Text.Trim Then
            MsgBox("Comaker 1 and the borrower is the same.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf txtKBCI.Text.Trim = txtCoMaker2.Text.Trim Then
            MsgBox("Comaker 2 and the borrower is the same.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf txtCoMaker1.Text.Trim <> String.Empty And txtCoMaker1.Text.Trim = txtCoMaker2.Text.Trim Then
            MsgBox("Comakers are the same.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'CTD PROC
        'TODO
        'If boDgvCollateral AndAlso cboLoanType.SelectedValue = "STL" Then
        '    If Not dgvCollateral.DataSource Is Nothing Then
        '        For Each dr In CType(dgvCollateral.DataSource, DataTable).Rows
        '            dblX += CDbl(dr.Item("CTD_AMT"))
        '        Next
        '    End If

        '    If dblX < CDbl(txtLoanAmount.Text) Then
        '        MsgBox("CTD amount cannot cover loan amount!", MsgBoxStyle.Exclamation)
        '        Exit Sub
        '    End If
        'End If

        dt = Common.GetDetailsOld("Member", txtKBCI.Text.Trim)
        If dt.Rows.Count <= 0 Then
            MsgBox("KBCI number is invalid!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtCoMaker1.Text <> String.Empty AndAlso Common.GetDetailsOld("Member", txtCoMaker1.Text.Trim).Rows.Count <= 0 Then
            MsgBox("Comaker 1 is invalid!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtCoMaker2.Text <> String.Empty AndAlso Common.GetDetailsOld("Member", txtCoMaker2.Text.Trim).Rows.Count <= 0 Then
            MsgBox("Comaker 2 is invalid!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        drMember = dt.Rows(0)
        drLoansBlank = Common.GetDetailsOld("Loan", "").NewRow

        ds = Common.GetDetailsAsDataSet(txtKBCI.Text.Trim, "LA-1")
        dtSTL = ds.Tables(1)
        drCTRL = ds.Tables(0).Rows(0)

        If drMember.Item("MEM_STAT").ToString = "R" Then
            If cboLoanType.SelectedValue = "STL" And Common.GetDetailsOld("CTD", txtKBCI.Text.Trim).Rows.Count > 0 Then
                resign = True
                dtSTL = New DataTable
            Else
                MsgBox("Member is already resigned. Only STL is available for members with CTD.", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        End If

        dulin = True

        If Not drMember.Item("MEM_DATE") Is DBNull.Value AndAlso drMember.Item("MEM_DATE").ToString <> String.Empty Then
            term = DateDiff(DateInterval.Month, CDate(drMember.Item("MEM_DATE")), sysdate)
            If term < 6 Then
                MsgBox("Borrower membership term is only " & CStr(term) & " month(s).", MsgBoxStyle.Exclamation)
            End If
        End If

        Try
            drLoansBlank.Item("KBCI_NO") = drMember("KBCI_NO")
            drLoansBlank.Item("APP_DATE") = sysdate
            drLoansBlank.Item("CHKNO_DATE") = sysdate
            drLoansBlank.Item("USER") = sysuser
            drLoansBlank.Item("LOAN_STAT") = "R"
            drLoansBlank.Item("MOD_PAY") = cboPaymentMode.SelectedValue

            drLoansBlank.Item("LOAN_TYPE") = cboLoanType.SelectedValue
            drLoansBlank.Item("LED_TYPE") = cboLedgerType.SelectedValue
            drLoansBlank.Item("PRINCIPAL") = Common.IsDBNullNum(txtLoanAmount.Text.Trim)
            drLoansBlank.Item("TERM") = CInt(txtTerm.Text.Trim)
            drLoansBlank.Item("FREQ") = cboFrequency.SelectedValue
            drLoansBlank.Item("RATE") = Common.IsDBNullNum(txtRate.Text.Trim)
            drLoansBlank.Item("LRI_IND") = chkLRI.Checked
            drLoansBlank.Item("CHKNO_BANK") = txtBank.Text.ToUpper().Trim()
            drLoansBlank.Item("CHKNO") = txtCheckNo.Text.Trim
            drLoansBlank.Item("COLLATERAL") = txtCollateral.Text
        Catch ex As Exception
            MsgBox("Please fill all required fields.", MsgBoxStyle.Exclamation)
            Return
        End Try

        If lontayp() AndAlso lonamt() AndAlso lterm() AndAlso lblfreq() AndAlso inte() Then
            SDEDBAL()
            Dim jmsc As Boolean

            If cboLoanType.SelectedValue = "STL" Then
                jmsc = False
            Else
                jmsc = MessageBox.Show("Deduct Miscellaneous Liabilities?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes
            End If

            With drLoansBlank
                ds = Business.LoanApplication.N_Proc( _
                    "C", _
                    Common.IsDBNull(.Item("PN_NO")), _
                    Common.IsDBNull(.Item("KBCI_NO")), _
                    Common.IsDBNull(.Item("LOAN_TYPE")), _
                    CInt(.Item("LED_TYPE")), _
                    Common.IsDBNullNum(.Item("ADV_INTE")), _
                    Common.IsDBNullNum(.Item("PRINCIPAL")), _
                    Common.IsDBNullNum(.Item("RATE")), _
                    Common.IsDBNullNum(.Item("TERM")), _
                    Common.IsDBNull(.Item("MOD_PAY")), _
                    Common.IsDBNull(.Item("FREQ")), _
                    .Item("LRI_IND"), _
                    .Item("RENEW"), _
                    xrenew2, _
                    strID, _
                    sysuser, _
                    jmsc _
                    )
            End With

            'in original - removed here
            'drLoansBlank.Item("L_EXT") = 0

            If ds.Tables.Count > 1 Then
                For Each dr In ds.Tables(1).Rows
                    message = IIf(message = String.Empty, String.Empty, message & vbCrLf) & dr.Item("proc_msg")

                    'If drLoansBlank.Item("LOAN_TYPE").ToString() <> "STL" AndAlso Strings.Left(dr.Item("proc_msg"), 18) = "Loan renewal worth" Then
                    '    drLoansBlank.Item("L_EXT") = 1
                    'End If
                Next

                MsgBox(message, MsgBoxStyle.Information)
                dt = ds.Tables(0)
                drLoansBlank.Item("CHKNO_AMT") = dt.Rows(dt.Rows.Count - 1)("AMOUNT")
            End If

            txtDateApplied.Text = Format(CDate(drCTRL.Item("SYSDATE")), "MM/dd/yyyy")
            txtAmortization.Text = Common.FormatCS(drLoansBlank.Item("AMORT_AMT"))
            txtPayStart.Text = drLoansBlank.Item("PAY_START")
            txtDateDue.Text = drLoansBlank.Item("DATE_DUE")
            txtNetProceeds.Text = Format(drLoansBlank.Item("CHKNO_AMT"), "#,##0.00")
            txtReleaseDate.Text = drLoansBlank.Item("CHKNO_DATE")
            txtBank.Text = drLoansBlank.Item("CHKNO_BANK")
            txtCheckNo.Text = drLoansBlank.Item("CHKNO")

            Dim form As New LoanApplicationBreakdown
            form.dtList = dt
            form.ShowDialog()

            If Common.IsDBNullNum(drLoansBlank.Item("CHKNO_AMT")) < 0 Then
                MsgBox("Net proceeds is either 0 or negative !!!", MsgBoxStyle.Exclamation)
                Exit Sub
            End If

            Select Case Common.PopupOptions("Accept", "Edit", "Cancel", "")
                Case 2
                    Exit Sub
                Case 3
                    NewForm()
                    Exit Sub
            End Select

            With drLoansBlank
                pnNumber = Business.LoanApplication.Insert(drLoansBlank, _
                    "F", _
                    Common.IsDBNull(.Item("KBCI_NO")), _
                    Common.IsDBNull(.Item("LOAN_TYPE")), _
                    CInt(.Item("LED_TYPE")), _
                    Common.IsDBNullNum(.Item("ADV_INTE")), _
                    Common.IsDBNullNum(.Item("PRINCIPAL")), _
                    Common.IsDBNullNum(.Item("RATE")), _
                    Common.IsDBNullNum(.Item("TERM")), _
                    Common.IsDBNull(.Item("MOD_PAY")), _
                    Common.IsDBNull(.Item("FREQ")), _
                    .Item("LRI_IND"), _
                    .Item("RENEW"), _
                    xrenew2, _
                    strID, _
                    sysuser, _
                    txtCoMaker1.Text.Trim, _
                    txtCoMaker2.Text.Trim, _
                    jmsc _
                    )

                If pnNumber.Length > 0 Then
                    MsgBox("Loan successfully processed !!!", MsgBoxStyle.Information)
                    Common.OpenReport(Of Report.Voucher.Release)(pnNumber, sysuser)
                    NewForm()
                Else
                    MsgBox("Loan processing aborted !!!", MsgBoxStyle.Critical)
                End If
            End With

            form.Dispose()
        End If
    End Sub

    Private Sub txtRate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRate.LostFocus
        If IsNumeric(txtRate.Text) Then
            txtRate.Text = Format(CDbl(txtRate.Text), "#,#0.00")
        End If
    End Sub

    Private Sub txtBank_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBank.LostFocus
        txtBank.Text = txtBank.Text.Trim().ToUpper()
    End Sub

    Private Sub txtCheckNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCheckNo.LostFocus
        txtCheckNo.Text = txtCheckNo.Text.Trim().ToUpper()
    End Sub

#Region "Obsolete"

    'Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim dt As New DataTable
    '    Dim dr As DataRow
    '    Dim amountCTD As Double = 0

    '    If Not dgvCollateral.DataSource Is Nothing AndAlso CType(dgvCollateral.DataSource, DataTable).Rows.Count > 0 Then
    '        For Each dr In CType(dgvCollateral.DataSource, DataTable).Rows
    '            amountCTD += Common.IsDBNullNum((dr.Item("CTD_AMT")))
    '        Next
    '    End If

    '    If Not IsNumeric(txtLoanAmount.Text) Then
    '        MsgBox("Enter valid loan amount first.", MsgBoxStyle.Exclamation)
    '        Exit Sub
    '    ElseIf amountCTD >= CDbl(txtLoanAmount.Text) Then
    '        MsgBox("CTDs tagged are already sufficient.", MsgBoxStyle.Exclamation)
    '        Exit Sub
    '    End If

    '    Dim frm As New frmCODataGridSearch
    '    Dim validCTD As Boolean = True

    '    Do While True
    '        If Not dgvCollateral.DataSource Is Nothing Then
    '            If CType(dgvCollateral.DataSource, DataTable).Rows.Count = 5 Then
    '                MsgBox("Up to 5 CTDs only.", MsgBoxStyle.Exclamation)
    '                Exit Sub
    '            End If
    '        End If

    '        amountCTD = 0

    '        If Not dtCTDList Is Nothing Then
    '            dt = dtCTDList
    '        Else
    '            dt = Common.GetDetails("", "LA-5")
    '            dtCTDList = dt
    '        End If

    '        frm.dgvList.DataSource = dt
    '        frm.ShowDialog()

    '        If Not frm.Record Is Nothing Then
    '            If dgvCollateral.DataSource Is Nothing And dtCTD.Columns.Count = 0 Then
    '                dtCTD.Columns.Add(Common.AddColumn("System.String", "CTD_NO"))
    '                dtCTD.Columns.Add(Common.AddColumn("System.String", "CTD_AMT"))
    '                dtCTD.Columns.Add(Common.AddColumn("System.Double", "RATE"))
    '                dtCTD.Columns.Add(Common.AddColumn("System.DateTime", "DUE_DATE"))
    '                dtCTD.Columns.Add(Common.AddColumn("System.String", "KBCI_NO"))
    '                dtCTD.Columns.Add(Common.AddColumn("System.String", "NAME"))
    '            End If

    '            If Not dgvCollateral.DataSource Is Nothing Then
    '                For Each dr In CType(dgvCollateral.DataSource, DataTable).Rows
    '                    If frm.Record.Item("CTD_NO") = dr.Item("CTD_NO") Then
    '                        MsgBox("CTD already tagged!", MsgBoxStyle.Exclamation)
    '                        validCTD = False
    '                    Else
    '                        validCTD = True
    '                    End If
    '                Next
    '            End If

    '            If validCTD Then
    '                dr = dtCTD.NewRow
    '                dr.Item("CTD_NO") = frm.Record.Item("CTD_NO")
    '                dr.Item("CTD_AMT") = Format(frm.Record.Item("CTD_AMT"), "#,##0.00")
    '                dr.Item("RATE") = frm.Record.Item("RATE")
    '                dr.Item("DUE_DATE") = frm.Record.Item("DUE_DATE")
    '                dr.Item("KBCI_NO") = frm.Record.Item("_KBCI_NO_")
    '                dr.Item("NAME") = frm.Record.Item("NAME")
    '                dtCTD.Rows.Add(dr)

    '                dgvCollateral.DataSource = dtCTD
    '                dgvCollateral.Refresh()
    '            End If

    '            Select Case CType(dgvCollateral.DataSource, DataTable).Rows.Count
    '                Case 0
    '                    btnAdd.Enabled = True
    '                    btnDelete.Enabled = False
    '                Case 5
    '                    btnAdd.Enabled = False
    '                    btnDelete.Enabled = True
    '                Case Else
    '                    btnAdd.Enabled = True
    '                    btnDelete.Enabled = True
    '            End Select

    '            For Each dr In dtCTD.Rows
    '                amountCTD += CDbl(dr.Item("CTD_AMT"))
    '            Next

    '            If amountCTD >= CDbl(txtLoanAmount.Text) Then
    '                Exit Do
    '            End If
    '        End If

    '    Loop

    '    dr = Nothing
    '    dt = Nothing
    '    frm = Nothing

    'End Sub

    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If dgvCollateral.DataSource Is Nothing OrElse CType(dgvCollateral.DataSource, DataTable).Rows.Count <= 0 Then
    '        MsgBox("Nothing to delete.", MsgBoxStyle.Exclamation)
    '    Else
    '        Dim dt As New DataTable
    '        dt = dgvCollateral.DataSource

    '        If dt.Rows.Count > 0 Then
    '            dt.Rows.RemoveAt(dgvCollateral.CurrentRow.Index)
    '            dgvCollateral.DataSource = dt
    '            dgvCollateral.Refresh()
    '        End If

    '        Select Case CType(dgvCollateral.DataSource, DataTable).Rows.Count
    '            Case 0
    '                btnAdd.Enabled = True
    '                btnDelete.Enabled = False
    '            Case 5
    '                btnAdd.Enabled = False
    '                btnDelete.Enabled = True
    '            Case Else
    '                btnAdd.Enabled = True
    '                btnDelete.Enabled = True
    '        End Select

    '        dt = Nothing
    '    End If
    'End Sub

    'Private Sub txtLoanAmount_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    If Not dgvCollateral.DataSource Is Nothing Then
    '        dgvCollateral.DataSource = Nothing
    '        dtCTD = New DataTable
    '    End If
    'End Sub

#End Region

End Class
