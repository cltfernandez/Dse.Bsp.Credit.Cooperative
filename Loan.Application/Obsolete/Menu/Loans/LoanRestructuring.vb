Imports Loan.Application.Infrastructure.Forms.Popups

Public Class LoanRestructuring
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
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Friend WithEvents txtAmortization As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtLoanAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtDateDue As System.Windows.Forms.TextBox
    Friend WithEvents txtReleaseDate As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtNetProceeds As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtPayStart As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtCheckNo As System.Windows.Forms.TextBox
    Friend WithEvents chkLRI As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtKBCI As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtPNNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents txtMI As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents txtDateApplied As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents mnuNew As System.Windows.Forms.MenuItem
    Friend WithEvents txtAdvInt As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtAmortizationPri As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblAdvInteX As System.Windows.Forms.Label
    Friend WithEvents lblAdvInteY As System.Windows.Forms.Label
    Friend WithEvents txtxint As System.Windows.Forms.TextBox
    Friend WithEvents txtyint As System.Windows.Forms.TextBox
    Friend WithEvents cboLedgerType As System.Windows.Forms.ComboBox
    Friend WithEvents mnuSave As System.Windows.Forms.MenuItem
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtyint = New System.Windows.Forms.TextBox
        Me.txtxint = New System.Windows.Forms.TextBox
        Me.lblAdvInteY = New System.Windows.Forms.Label
        Me.lblAdvInteX = New System.Windows.Forms.Label
        Me.txtAmortizationPri = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtAdvInt = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtAmortization = New System.Windows.Forms.TextBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtDateDue = New System.Windows.Forms.TextBox
        Me.txtReleaseDate = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtNetProceeds = New System.Windows.Forms.TextBox
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtPayStart = New System.Windows.Forms.TextBox
        Me.cboLedgerType = New System.Windows.Forms.ComboBox
        Me.cboLoanType = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox
        Me.txtLoanAmount = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.cboFrequency = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtBank = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtCheckNo = New System.Windows.Forms.TextBox
        Me.chkLRI = New System.Windows.Forms.CheckBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtKBCI = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPNNo = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtFName = New System.Windows.Forms.TextBox
        Me.txtMI = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtLName = New System.Windows.Forms.TextBox
        Me.txtDateApplied = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuNew = New System.Windows.Forms.MenuItem
        Me.mnuSave = New System.Windows.Forms.MenuItem
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.lblTitle = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtyint)
        Me.GroupBox2.Controls.Add(Me.txtxint)
        Me.GroupBox2.Controls.Add(Me.lblAdvInteY)
        Me.GroupBox2.Controls.Add(Me.lblAdvInteX)
        Me.GroupBox2.Controls.Add(Me.txtAmortizationPri)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.txtAdvInt)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.txtAmortization)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.txtDateDue)
        Me.GroupBox2.Controls.Add(Me.txtReleaseDate)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.txtNetProceeds)
        Me.GroupBox2.Controls.Add(Me.txtStatus)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtPayStart)
        Me.GroupBox2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(8, 344)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(667, 151)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        '
        'txtyint
        '
        Me.txtyint.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtyint.Location = New System.Drawing.Point(166, 122)
        Me.txtyint.Name = "txtyint"
        Me.txtyint.Size = New System.Drawing.Size(152, 21)
        Me.txtyint.TabIndex = 17
        '
        'txtxint
        '
        Me.txtxint.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtxint.Location = New System.Drawing.Point(166, 95)
        Me.txtxint.Name = "txtxint"
        Me.txtxint.Size = New System.Drawing.Size(152, 21)
        Me.txtxint.TabIndex = 16
        '
        'lblAdvInteY
        '
        Me.lblAdvInteY.AutoSize = True
        Me.lblAdvInteY.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdvInteY.Location = New System.Drawing.Point(6, 125)
        Me.lblAdvInteY.Name = "lblAdvInteY"
        Me.lblAdvInteY.Size = New System.Drawing.Size(129, 13)
        Me.lblAdvInteY.TabIndex = 58
        Me.lblAdvInteY.Text = "After 0 Months INT"
        '
        'lblAdvInteX
        '
        Me.lblAdvInteX.AutoSize = True
        Me.lblAdvInteX.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdvInteX.Location = New System.Drawing.Point(6, 98)
        Me.lblAdvInteX.Name = "lblAdvInteX"
        Me.lblAdvInteX.Size = New System.Drawing.Size(126, 13)
        Me.lblAdvInteX.TabIndex = 57
        Me.lblAdvInteX.Text = "First 0 Months INT"
        '
        'txtAmortizationPri
        '
        Me.txtAmortizationPri.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmortizationPri.Location = New System.Drawing.Point(166, 68)
        Me.txtAmortizationPri.Name = "txtAmortizationPri"
        Me.txtAmortizationPri.Size = New System.Drawing.Size(152, 21)
        Me.txtAmortizationPri.TabIndex = 15
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(6, 71)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(129, 13)
        Me.Label28.TabIndex = 56
        Me.Label28.Text = "Amortization (PRI)"
        '
        'txtAdvInt
        '
        Me.txtAdvInt.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAdvInt.Location = New System.Drawing.Point(166, 41)
        Me.txtAdvInt.Name = "txtAdvInt"
        Me.txtAdvInt.Size = New System.Drawing.Size(152, 21)
        Me.txtAdvInt.TabIndex = 14
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(6, 44)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(129, 13)
        Me.Label22.TabIndex = 54
        Me.Label22.Text = "Advance Int (mos)"
        '
        'txtAmortization
        '
        Me.txtAmortization.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAmortization.Location = New System.Drawing.Point(166, 14)
        Me.txtAmortization.Name = "txtAmortization"
        Me.txtAmortization.Size = New System.Drawing.Size(152, 21)
        Me.txtAmortization.TabIndex = 11
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(343, 17)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(67, 13)
        Me.Label17.TabIndex = 33
        Me.Label17.Text = "Pay Start"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(343, 125)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(93, 13)
        Me.Label21.TabIndex = 42
        Me.Label21.Text = "Net Proceeds"
        '
        'txtDateDue
        '
        Me.txtDateDue.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateDue.Location = New System.Drawing.Point(501, 95)
        Me.txtDateDue.Name = "txtDateDue"
        Me.txtDateDue.Size = New System.Drawing.Size(152, 21)
        Me.txtDateDue.TabIndex = 24
        '
        'txtReleaseDate
        '
        Me.txtReleaseDate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtReleaseDate.Location = New System.Drawing.Point(501, 41)
        Me.txtReleaseDate.Name = "txtReleaseDate"
        Me.txtReleaseDate.Size = New System.Drawing.Size(152, 21)
        Me.txtReleaseDate.TabIndex = 22
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(343, 98)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 13)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "Date Due"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(343, 44)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(92, 13)
        Me.Label18.TabIndex = 35
        Me.Label18.Text = "Release Date"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(343, 71)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(83, 13)
        Me.Label19.TabIndex = 37
        Me.Label19.Text = "Loan Status"
        '
        'txtNetProceeds
        '
        Me.txtNetProceeds.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetProceeds.Location = New System.Drawing.Point(501, 122)
        Me.txtNetProceeds.Name = "txtNetProceeds"
        Me.txtNetProceeds.Size = New System.Drawing.Size(152, 21)
        Me.txtNetProceeds.TabIndex = 25
        '
        'txtStatus
        '
        Me.txtStatus.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStatus.Location = New System.Drawing.Point(501, 68)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(152, 21)
        Me.txtStatus.TabIndex = 23
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(6, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(131, 13)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Amortization Value"
        '
        'txtPayStart
        '
        Me.txtPayStart.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPayStart.Location = New System.Drawing.Point(501, 14)
        Me.txtPayStart.Name = "txtPayStart"
        Me.txtPayStart.Size = New System.Drawing.Size(152, 21)
        Me.txtPayStart.TabIndex = 21
        '
        'cboLedgerType
        '
        Me.cboLedgerType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLedgerType.FormattingEnabled = True
        Me.cboLedgerType.Location = New System.Drawing.Point(501, 40)
        Me.cboLedgerType.Name = "cboLedgerType"
        Me.cboLedgerType.Size = New System.Drawing.Size(152, 21)
        Me.cboLedgerType.TabIndex = 63
        '
        'cboLoanType
        '
        Me.cboLoanType.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboLoanType.Location = New System.Drawing.Point(166, 13)
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.Size = New System.Drawing.Size(152, 21)
        Me.cboLoanType.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Loan Type"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(341, 43)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 13)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Ledger Type"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(341, 16)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(154, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Frequency of Payment"
        '
        'txtTerm
        '
        Me.txtTerm.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTerm.Location = New System.Drawing.Point(166, 40)
        Me.txtTerm.MaxLength = 10
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.Size = New System.Drawing.Size(152, 21)
        Me.txtTerm.TabIndex = 19
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboPaymentMode.Location = New System.Drawing.Point(166, 40)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.Size = New System.Drawing.Size(152, 21)
        Me.cboPaymentMode.TabIndex = 12
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLoanAmount.Location = New System.Drawing.Point(166, 13)
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.Size = New System.Drawing.Size(154, 21)
        Me.txtLoanAmount.TabIndex = 9
        Me.txtLoanAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(10, 70)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(39, 13)
        Me.Label23.TabIndex = 46
        Me.Label23.Text = "Bank"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(10, 43)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(41, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Term"
        '
        'txtRate
        '
        Me.txtRate.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(501, 40)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(152, 21)
        Me.txtRate.TabIndex = 20
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(341, 16)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(116, 13)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Exempt from LRI"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(341, 43)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(36, 13)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Rate"
        '
        'cboFrequency
        '
        Me.cboFrequency.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboFrequency.Location = New System.Drawing.Point(501, 13)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(152, 21)
        Me.cboFrequency.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(10, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(92, 13)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Loan Amount"
        '
        'txtBank
        '
        Me.txtBank.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBank.Location = New System.Drawing.Point(166, 67)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(152, 21)
        Me.txtBank.TabIndex = 26
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(341, 70)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(71, 13)
        Me.Label24.TabIndex = 48
        Me.Label24.Text = "Check No."
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(8, 43)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(102, 13)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Payment Mode"
        '
        'txtCheckNo
        '
        Me.txtCheckNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCheckNo.Location = New System.Drawing.Point(501, 67)
        Me.txtCheckNo.Name = "txtCheckNo"
        Me.txtCheckNo.Size = New System.Drawing.Size(152, 21)
        Me.txtCheckNo.TabIndex = 27
        '
        'chkLRI
        '
        Me.chkLRI.Checked = True
        Me.chkLRI.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLRI.Location = New System.Drawing.Point(501, 11)
        Me.chkLRI.Name = "chkLRI"
        Me.chkLRI.Size = New System.Drawing.Size(104, 24)
        Me.chkLRI.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtKBCI)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtPNNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtFName)
        Me.GroupBox1.Controls.Add(Me.txtMI)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtLName)
        Me.GroupBox1.Controls.Add(Me.txtDateApplied)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(8, 58)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(667, 99)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        '
        'txtKBCI
        '
        Me.txtKBCI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtKBCI.Location = New System.Drawing.Point(168, 16)
        Me.txtKBCI.MaxLength = 9
        Me.txtKBCI.Name = "txtKBCI"
        Me.txtKBCI.Size = New System.Drawing.Size(150, 21)
        Me.txtKBCI.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(343, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "LName"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "KBCI No."
        '
        'txtPNNo
        '
        Me.txtPNNo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPNNo.Location = New System.Drawing.Point(168, 42)
        Me.txtPNNo.Name = "txtPNNo"
        Me.txtPNNo.Size = New System.Drawing.Size(150, 21)
        Me.txtPNNo.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(10, 45)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "PN No."
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(343, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "FName"
        '
        'txtFName
        '
        Me.txtFName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFName.Location = New System.Drawing.Point(503, 39)
        Me.txtFName.Name = "txtFName"
        Me.txtFName.Size = New System.Drawing.Size(150, 21)
        Me.txtFName.TabIndex = 5
        '
        'txtMI
        '
        Me.txtMI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMI.Location = New System.Drawing.Point(503, 62)
        Me.txtMI.Name = "txtMI"
        Me.txtMI.Size = New System.Drawing.Size(150, 21)
        Me.txtMI.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(345, 65)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(23, 13)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "MI"
        '
        'txtLName
        '
        Me.txtLName.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLName.Location = New System.Drawing.Point(503, 16)
        Me.txtLName.Name = "txtLName"
        Me.txtLName.Size = New System.Drawing.Size(150, 21)
        Me.txtLName.TabIndex = 4
        '
        'txtDateApplied
        '
        Me.txtDateApplied.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDateApplied.Location = New System.Drawing.Point(168, 68)
        Me.txtDateApplied.Name = "txtDateApplied"
        Me.txtDateApplied.Size = New System.Drawing.Size(150, 21)
        Me.txtDateApplied.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(10, 71)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Date Applied"
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuNew, Me.mnuSave})
        '
        'mnuNew
        '
        Me.mnuNew.Index = 0
        Me.mnuNew.Text = "&New"
        '
        'mnuSave
        '
        Me.mnuSave.Enabled = False
        Me.mnuSave.Index = 1
        Me.mnuSave.Text = "&Save"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cboLedgerType)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.cboLoanType)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.cboFrequency)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.cboPaymentMode)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 163)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(667, 69)
        Me.GroupBox3.TabIndex = 64
        Me.GroupBox3.TabStop = False
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtLoanAmount)
        Me.GroupBox4.Controls.Add(Me.Label9)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.txtRate)
        Me.GroupBox4.Controls.Add(Me.txtTerm)
        Me.GroupBox4.Controls.Add(Me.txtBank)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.txtCheckNo)
        Me.GroupBox4.Controls.Add(Me.Label13)
        Me.GroupBox4.Controls.Add(Me.chkLRI)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 238)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(667, 100)
        Me.GroupBox4.TabIndex = 65
        Me.GroupBox4.TabStop = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(8, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(328, 37)
        Me.lblTitle.TabIndex = 66
        Me.lblTitle.Text = "Loan Restructuring"
        '
        'LoanRestructuring
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(683, 507)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Menu = Me.MainMenu1
        Me.Name = "LoanRestructuring"
        Me.Text = "Loan Restructuring"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

#Region "Declaration"

    Dim dr As DataRow
    Dim drMember As DataRow
    Dim drLoansBlank As DataRow
    Dim drRDEDBAL As DataRow

    Dim dt As New DataTable
    Dim dtLoans As DataTable
    Dim dtRDEDBAL As New DataTable

    Dim dulin As Boolean = False
    Dim resign As Boolean = False
    Dim rsold As Boolean = False
    Dim rAD_INT As Double = 0
    Dim rSC As Double = 0
    Dim rLRI As Double = 0
    Dim rINIT As Double = 0
    Dim rEDL As Double = 0
    Dim rAPL As Double = 0
    Dim rRGL As Double = 0
    Dim rEML As Double = 0
    Dim rSPL As Double = 0
    Dim rRSL As Double = 0
    Dim rPTL As Double = 0
    Dim rSTL As Double = 0
    Dim xbloan As Double = 0

    Dim xbalp As Double = 0
    Dim rpamt As Double = 0
    Dim xplnamt As Double = 0
    Dim xtotamt As Double = 0

    Dim mterm As Double = 0

    Dim rltyp As String = ""

    Dim xx As Integer = 0
    Dim xmod_pay As Integer = 0
    Dim reyt As Integer = 0

    Dim rtag As Boolean
    Dim pay_day As DateTime

    Dim Rate As Double
    Dim Term As Integer
    Dim Principal As Double
    Dim Chkno_Amt As Double

    Dim OldPrincipal As Double
    Dim OldChkno_Amt As Double
    Dim OldLoanTypeIndex As Integer

#End Region

#Region "Controls"

    Private Sub cboLoanType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboLoanType.SelectedIndexChanged
        If cboLoanType.SelectedIndex = 0 Then
            Exit Sub
        End If

        Dim dt As New DataTable
        dt = Common.GetDetailsOld("PARAM", cboLoanType.SelectedValue)

        If dt.Rows.Count = 0 Then
            MsgBox("ERROR: Loan type is not in parameter file!", MsgBoxStyle.Critical, "Error")
        Else
            cboFrequency.SelectedValue = CStr(dt.Rows(0).Item("FREQ"))
            txtRate.Text = CDbl(dt.Rows(0).Item("RATE"))
            txtTerm.Text = CDbl(dt.Rows(0).Item("TERM"))
        End If
    End Sub

    Private Sub mnuNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNew.Click
        If MsgBox("Add RSL loan to file?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            Exit Sub
        End If

        ClearVars()
        Common.SetControls(Me, Common.EnumSetControls.Clear)
        Common.SetControls(Me, Common.EnumSetControls.Disable)

        Select Case Common.PopupOptions("Find by List", "Find by KBCI", "Cancel", "")
            Case 1
                drMember = Common.FindByList("MB-2", "MEMBERS", "KBCI_NO")
            Case 2
                drMember = Common.FindByInput("MEMBERS", "Enter KBCI No.")
            Case 3
                Exit Sub
        End Select

        If drMember Is Nothing Then
            Exit Sub
        End If

        dulin = True

        If CStr(drMember.Item("MEM_STAT")) = "R" Then resign = True : MsgBox("Member is already resigned.", MsgBoxStyle.Information)

        dtLoans = Common.GetDetailsOld("LE-1", drMember.Item("KBCI_NO"))

        If dtLoans.Rows.Count > 0 Then
            drLoansBlank = dtLoans.NewRow
            drLoansBlank.Item("KBCI_NO") = drMember.Item("KBCI_NO")

            drLoansBlank.Item("YTD_I") = 0
            drLoansBlank.Item("P_BAL") = 0
            drLoansBlank.Item("I_BAL") = 0
            drLoansBlank.Item("O_BAL") = 0
            drLoansBlank.Item("APP_NO") = 0
            drLoansBlank.Item("ADVANCE") = 0
            drLoansBlank.Item("LRI_DUE") = 0
            drLoansBlank.Item("LED_TYPE") = 0
            drLoansBlank.Item("ADV_INTE") = 0
            drLoansBlank.Item("AFT_INTE") = 0
            drLoansBlank.Item("ARREAR_I") = 0
            drLoansBlank.Item("ARREAR_P") = 0
            drLoansBlank.Item("ARREAR_OTH") = 0

            drLoansBlank.Item("USER") = String.Empty
            drLoansBlank.Item("PN_NO") = String.Empty
            drLoansBlank.Item("BY_WHOM") = String.Empty
            drLoansBlank.Item("DED_BAL") = String.Empty
            drLoansBlank.Item("CHKNO_RELS") = String.Empty
            drLoansBlank.Item("COLLATERAL") = String.Empty

            drLoansBlank.Item("PD") = False
            drLoansBlank.Item("RENEW") = False
            drLoansBlank.Item("L_EXT") = False
            drLoansBlank.Item("LRI_IND") = False
            drLoansBlank.Item("REC_STAT") = False

            pay_day = dtLoans.Rows(0)("PAY_DAY")
        Else
            MsgBox("No loan for restructuring.", MsgBoxStyle.Information)
            Exit Sub
        End If

        If dulin Then
            dtRDEDBAL = New DataTable
            dtRDEDBAL.Columns.Add(Common.AddColumn("System.Boolean", "rtag"))
            dtRDEDBAL.Columns.Add(Common.AddColumn("System.String", "rlstat"))
            dtRDEDBAL.Columns.Add(Common.AddColumn("System.String", "rpn_no"))
            dtRDEDBAL.Columns.Add(Common.AddColumn("System.String", "rltyp"))
            dtRDEDBAL.Columns.Add(Common.AddColumn("System.Double", "rlamt"))
            dtRDEDBAL.Columns.Add(Common.AddColumn("System.Double", "rpamt"))

            Try
                For Each dr In dtLoans.Rows
                    If Not dr.Item("LOAN_STAT") <> "R" And dr.Item("LOAN_TYPE") <> "RSL" Then
                        dt = Business.Rules.Loans.Pretermination.Preterm("C", dr.Item("PN_NO"), dr.Item("LOAN_TYPE"), sysuser)
                        xbloan = dt.Rows(0)("xbfull")
                        drRDEDBAL = dtRDEDBAL.NewRow
                        drRDEDBAL.Item("rtag") = True
                        drRDEDBAL.Item("rlstat") = "OUTS"
                        drRDEDBAL.Item("rpn_no") = dr.Item("PN_NO")
                        drRDEDBAL.Item("rltyp") = dr.Item("LOAN_TYPE")
                        drRDEDBAL.Item("rlamt") = dr.Item("PRINCIPAL")
                        drRDEDBAL.Item("rpamt") = xbloan
                        dtRDEDBAL.Rows.Add(drRDEDBAL)
                    End If
                Next

                If Not dtRDEDBAL.Rows.Count > 0 Then
                    MsgBox("ERROR: No loans for restructuring.", MsgBoxStyle.Critical, "Error")
                    If MsgBox("Setup old RSL?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                        dulin = True
                        rsold = True
                    Else
                        dulin = False
                    End If
                Else
                    Dim form As New PopupDataGridOptions
                    form.GetDataGrid().ReadOnly = False
                    form.ControlBox = True
                    form.GetDataGrid().DataSource = dtRDEDBAL
                    form.ShowDialog()

                    If form.IsCanceled Then
                        Exit Sub
                    End If

                    dtRDEDBAL = form.GetDataGrid().DataSource
                    form = Nothing

                    For Each dr In dtRDEDBAL.Rows
                        rtag = CBool(dr.Item("rtag"))
                        If rtag Then
                            xx += 1
                        End If
                    Next

                    If xx = 0 Then
                        MsgBox("ERROR: No loans tagged for restructuring.", MsgBoxStyle.Critical, "Error")
                    ElseIf MsgBox("Continue with Loan Restructuring ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                        dulin = False
                    Else
                        dulin = True
                    End If
                End If

                If Not dulin Then
                    MsgBox("ERROR: No loans for restructuring.", MsgBoxStyle.Critical, "Error")
                    Exit Sub
                End If

                If Not rsold Then
                    For Each dr In dtRDEDBAL.Rows
                        If CBool(dr.Item("rtag")) Then
                            xbalp += CDbl(dr.Item("rpamt"))
                            rltyp = dr.Item("rltyp")
                            rpamt = dr.Item("rpamt")

                            Select Case rltyp
                                Case "EDL"
                                    rEDL += rpamt
                                Case "APL"
                                    rAPL += rpamt
                                Case "RGL"
                                    rRGL += rpamt
                                Case "EML"
                                    rEML += rpamt
                                Case "SPL"
                                    rSPL += rpamt
                                Case "RSL"
                                    rRSL += rpamt
                                Case "PTL"
                                    rPTL += rpamt
                                Case "STL"
                                    rSTL += rpamt
                            End Select
                        End If
                    Next
                End If

                dulin = True

                drLoansBlank.Item("APP_DATE") = sysdate
                drLoansBlank.Item("CHKNO_DATE") = sysdate
                drLoansBlank.Item("LOAN_STAT") = "R"
                drLoansBlank.Item("LOAN_TYPE") = "RSL"
                drLoansBlank.Item("PRINCIPAL") = xbalp
                drLoansBlank.Item("CHKNO_BANK") = "X       "
                drLoansBlank.Item("CHKNO") = "X     "
                xmod_pay = 1

                txtMI.Text = Common.IsDBNull(drMember.Item("MI"))
                txtKBCI.Text = Common.IsDBNull(drLoansBlank.Item("KBCI_NO"))
                txtLName.Text = Common.IsDBNull(drMember.Item("LNAME"))
                txtFName.Text = Common.IsDBNull(drMember.Item("FNAME"))
                txtLoanAmount.Text = Format(xbalp, "#,##0.00")
                txtDateApplied.Text = Common.IsDBNull(drLoansBlank.Item("APP_DATE"))

                cboLedgerType.SelectedIndex = 1
                cboLoanType.Enabled = True
                cboPaymentMode.Enabled = True
                cboFrequency.Enabled = True

                txtLoanAmount.Enabled = True
                txtTerm.Enabled = True
                txtBank.Enabled = True
                txtCheckNo.Enabled = True
                txtRate.Enabled = True

                chkLRI.Enabled = True

                mnuSave.Enabled = True
                mnuNew.Enabled = False
                cboLoanType.Focus()
            Catch ex As Exception
                MsgBox(ex.Message)
                Common.SetControls(Me, Common.EnumSetControls.Clear)
                Common.SetControls(Me, Common.EnumSetControls.Disable)
                mnuSave.Enabled = False
                mnuNew.Enabled = True
                Exit Sub
            End Try
        End If

    End Sub

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        If Not IsValid() Then
            Exit Sub
        End If

        Try
            OldChkno_Amt = Chkno_Amt

            If Not rsold Then
                Chkno_Amt = xbalp
            Else
                Chkno_Amt = Principal - (rAD_INT + rSC + rLRI)
            End If

            drLoansBlank.Item("CHKNO_AMT") = Chkno_Amt
            txtNetProceeds.Text = Format(drLoansBlank.Item("CHKNO_AMT"), "#,##0.00")

            If drLoansBlank.Item("CHKNO_AMT") <= 0 Then
                MsgBox("ERROR: Net proceeds is either 0 or negative.", MsgBoxStyle.Critical, "Error")
                dulin = False
            Else
                Dim formRslSummary As New LoanRestructuringSummary
                Principal = drLoansBlank.Item("PRINCIPAL")

                formRslSummary.lblrSC.Text = Format(rSC, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrLRI.Text = Format(rLRI, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrAD_INT.Text = Format(rAD_INT, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblPrincipal.Text = Format(Principal, "#,###,##0.00").PadLeft(12)

                formRslSummary.lblrEDL.Text = Format(rEDL, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrAPL.Text = Format(rAPL, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrRGL.Text = Format(rRGL, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrEML.Text = Format(rEML, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrSPL.Text = Format(rSPL, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrRSL.Text = Format(rRSL, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrSTL.Text = Format(rSTL, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblrPTL.Text = Format(rPTL, "#,###,##0.00").PadLeft(12)
                formRslSummary.lblNetProceeds.Text = Format(Chkno_Amt, "#,###,##0.00").PadLeft(12)

                formRslSummary.ShowDialog()
                formRslSummary.Close()

                formRslSummary = Nothing
            End If

            If dulin Then
                Select Case Common.PopupOptions("Accept", "Edit", "Cancel", "")
                    Case 1
                        Dim pnNumberNew As String = "0000000"

                        If Not Business.LoanRestructuring.IsRestructured(drLoansBlank, dtRDEDBAL, Principal, rAD_INT, rSC, rLRI, sysdate, rsold, pnNumberNew) Then
                            MsgBox("Loan processing aborted !!!", MsgBoxStyle.Exclamation)
                            Exit Sub
                        Else
                            mnuSave.Enabled = False
                            mnuNew.Enabled = True
                            Common.SetControls(Me, Common.EnumSetControls.Clear)
                            Common.SetControls(Me, Common.EnumSetControls.Disable)
                            MsgBox("Loan successfully processed !!!", MsgBoxStyle.Information)
                            Common.OpenReport(Of Report.Voucher.Release)(pnNumberNew, sysuser)
                        End If
                    Case 2
                        Chkno_Amt = OldChkno_Amt
                        Principal = OldPrincipal
                        txtLoanAmount.Text = Format(OldPrincipal, "#,##0.00")
                        cboLoanType.SelectedIndex = OldLoanTypeIndex

                        txtxint.Text = String.Empty
                        txtyint.Text = String.Empty
                        txtDateDue.Text = String.Empty
                        txtPayStart.Text = String.Empty
                        txtNetProceeds.Text = String.Empty
                        txtReleaseDate.Text = String.Empty
                        txtAmortization.Text = String.Empty
                        txtAmortizationPri.Text = String.Empty

                        Exit Sub
                    Case 3
                        mnuSave.Enabled = False
                        mnuNew.Enabled = True
                        Common.SetControls(Me, Common.EnumSetControls.Clear)
                        Common.SetControls(Me, Common.EnumSetControls.Disable)
                End Select
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
            mnuSave.Enabled = True
            mnuNew.Enabled = False
            Exit Sub
        End Try
    End Sub

#End Region

#Region "Subs"

    Private Sub ClearVars()
        dt = New DataTable
        dtLoans = New DataTable
        dtRDEDBAL = New DataTable
        dulin = False
        resign = False
        rsold = False
        rAD_INT = 0
        rSC = 0
        rLRI = 0
        rINIT = 0
        rEDL = 0
        rAPL = 0
        rRGL = 0
        rEML = 0
        rSPL = 0
        rRSL = 0
        rPTL = 0
        rSTL = 0
        xbloan = 0
        xbalp = 0
        rpamt = 0
        xplnamt = 0
        xtotamt = 0
        mterm = 0
        rltyp = ""
        xx = 0
        xmod_pay = 0
        reyt = 0
    End Sub

    Private Function IsValid() As Boolean
        Try
            drLoansBlank.Item("TERM") = txtTerm.Text.Trim
            drLoansBlank.Item("FREQ") = cboFrequency.SelectedValue
            drLoansBlank.Item("RATE") = txtRate.Text.Trim
            drLoansBlank.Item("CHKNO") = txtCheckNo.Text.Trim
            drLoansBlank.Item("LRI_IND") = chkLRI.Checked
            drLoansBlank.Item("MOD_PAY") = cboPaymentMode.SelectedValue
            drLoansBlank.Item("LED_TYPE") = cboLedgerType.SelectedValue
            drLoansBlank.Item("PRINCIPAL") = CDbl(txtLoanAmount.Text.Trim)
            drLoansBlank.Item("CHKNO_BANK") = txtBank.Text.Trim
            xmod_pay = cboPaymentMode.SelectedValue
        Catch ex As Exception
            MsgBox("Please fill all required fields properly.", MsgBoxStyle.Exclamation)
            Return False
        End Try

        If Not (CInt(txtTerm.Text) >= 1 And CInt(txtTerm.Text) <= 120) Then
            MsgBox("Valid term ranges from 1 to 120")
            Return False
        ElseIf txtCheckNo.Text.Trim = String.Empty Then
            MsgBox("Enter Check No.", MsgBoxStyle.Information)
            Return False
        ElseIf txtBank.Text.Trim = String.Empty Then
            MsgBox("Enter Bank.", MsgBoxStyle.Information)
            Return False
        ElseIf cboLedgerType.SelectedIndex = 0 Then
            MsgBox("Select ledger type.", MsgBoxStyle.Information)
            Return False
        End If

        If lontayp() AndAlso lonamt() AndAlso lblfreq() AndAlso lterm() AndAlso inte() Then
            Return True
        End If

        Return False
    End Function

    Private Function lontayp() As Boolean
        If drLoansBlank.Item("LOAN_TYPE") <> "RSL" Then
            MsgBox("ERROR: This module is for RSL data entry only !!!", MsgBoxStyle.Critical, "Error")
            Return False
        End If

        If resign And drLoansBlank.Item("LOAN_TYPE") <> "STL" Then
            MsgBox("Only STL is available for resigned members.", MsgBoxStyle.Information)
            OldLoanTypeIndex = cboLoanType.SelectedIndex
            drLoansBlank.Item("LOAN_TYPE") = "STL"
            cboLoanType.SelectedValue = "STL"
        End If

        If Common.GetDetailsOld("LE-2", drMember.Item("KBCI_NO").ToString).Rows.Count > 0 Then
            MsgBox("ERROR: Borrower already has an existing Restructured Loan.", MsgBoxStyle.Critical, "Error")
            Return False
        End If

        dt = Nothing

        Return True
    End Function

    Private Function lonamt() As Boolean
        If IsNumeric(drLoansBlank.Item("PRINCIPAL")) AndAlso CDbl(drLoansBlank.Item("PRINCIPAL")) = 0 Then
            MsgBox("Enter loan amount.", MsgBoxStyle.Information)
            Return False
        End If
        Return True
    End Function

    Private Function lblfreq() As Boolean
        Dim freq As String = drLoansBlank.Item("FREQ")

        If drLoansBlank.Item("LED_TYPE") = 1 And drLoansBlank.Item("FREQ") <> "M" Then
            MsgBox("For ledger type 1, frequency should be monthly.", MsgBoxStyle.Information)
            Return False
        End If

        mterm = drLoansBlank.Item("TERM")

        Select Case freq
            Case "M"
                reyt = 12
                mterm = mterm
            Case "Q"
                reyt = 4
                mterm = mterm / 3
            Case "S"
                reyt = 2
                mterm = mterm / 6
            Case "A"
                reyt = 1
                mterm = mterm / 12
            Case "D"
                MsgBox("Daily not yet handled.", MsgBoxStyle.Information)
                Return False
        End Select

        Return True
    End Function

    Private Function lterm() As Boolean
        Dim xxa As Double = CDbl(drLoansBlank.Item("TERM")) / 12
        If drLoansBlank.Item("LED_TYPE") = 1 AndAlso (xxa - Int(xxa)) > 0 Then
            MsgBox("For One Time Deduction, Loan Term should always fall in a full year.", MsgBoxStyle.Information)
            Return False
        End If
        Return True
    End Function

    Private Function inte() As Boolean
        Dim tdey, dmon, xjt, adv_inte As Integer
        Dim xmon, tmon, pay_start As DateTime
        Dim xpri, xint, xint1, yint As Double

        Principal = drLoansBlank.Item("PRINCIPAL")
        Rate = drLoansBlank.Item("RATE")
        Term = drLoansBlank.Item("TERM")

        Dim freq As String = drLoansBlank.Item("FREQ")
        mterm = CDbl(drLoansBlank.Item("TERM"))

        If Not rsold Then
            rAD_INT = 0
            If xmod_pay = 1 Then
                tdey = sysdate.Day
                If tdey < 7 Then
                    rAD_INT = Principal * ((Rate / 100) / 360) * (7 - tdey)
                End If
                If tdey > 15 Then
                    xmon = DateAdd(DateInterval.Month, 1, sysdate)
                    tmon = CDate(CStr(xmon.Month) + "/07/" + CStr(xmon.Year))
                    dmon = DateDiff(DateInterval.Day, sysdate, tmon)
                    rAD_INT = Principal * ((Rate / 100) / 360) * dmon
                End If
            End If

            rSC = 0
            If Term < 12 Then
                rSC = (0.01 / 12) * Term * Principal
            Else
                rSC = Principal * 0.01
            End If

            rLRI = 0
            If Not chkLRI.Checked Then
                rLRI = Principal * 0.01
                If rLRI > 1500 Then
                    rLRI = 1500
                End If
            End If

            rINIT = rAD_INT + rSC + rLRI

            If MsgBox("Include charges " & Format(rINIT, "#,##0.00") & " to principal?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OldPrincipal = Principal
                Principal = Principal + rINIT
                drLoansBlank.Item("PRINCIPAL") = Principal
            End If
        End If

        Select Case freq
            Case "M"
                reyt = 12
                mterm = mterm
            Case "Q"
                reyt = 4
                mterm = mterm / 3
            Case "S"
                reyt = 2
                mterm = mterm / 6
            Case "A"
                reyt = 1
                mterm = mterm / 12
        End Select

        If cboLedgerType.SelectedValue <> 1 Then
            drLoansBlank.Item("AMORT_AMT") = Common.Payment(Principal, (Rate / 100) / reyt, Term)
        End If

        If xmod_pay = 1 Then
            If sysdate.Day <= 15 Then
                pay_start = DateAdd(DateInterval.Month, 1, sysdate)
            Else
                pay_start = DateAdd(DateInterval.Month, 2, sysdate)
            End If
            pay_start = CDate(CStr(pay_start.Month) + "/" + CStr(pay_day.Day) + "/" + CStr(pay_start.Year))
        Else
            pay_start = drLoansBlank.Item("CHKNO_DATE")
            pay_start = DateAdd(DateInterval.Day, 30, pay_start)
        End If
        drLoansBlank.Item("PAY_START") = pay_start
        drLoansBlank.Item("DATE_DUE") = Common.GoDueOld(pay_start, Term, freq)

        txtDateDue.Text = drLoansBlank.Item("DATE_DUE")
        txtPayStart.Text = drLoansBlank.Item("PAY_START")
        txtLoanAmount.Text = Format(drLoansBlank.Item("PRINCIPAL"), "#,##0.00")
        txtReleaseDate.Text = drLoansBlank.Item("CHKNO_DATE")
        txtAmortization.Text = Format(drLoansBlank.Item("AMORT_AMT"), "#,##0.00")
        'txtNetProceeds.Text = Format(drLoansBlank.Item("CHKNO_AMT"), "#,##0.00")

        If cboLedgerType.SelectedValue = 1 Then
            txtAdvInt.Text = Format(drLoansBlank.Item("ADV_INTE"), "#,##0.00")
            xpri = Principal / Term
            adv_inte = CInt(drLoansBlank.Item("ADV_INTE"))
            If adv_inte <> 0 Then
                xint = Principal * (Rate / 100) * (adv_inte / 12)
                xint1 = (Principal * (Rate / 100)) - xint
                For xjt = 12 To Term Step 12
                    xint1 = xint1 + (Principal - (xpri * xjt)) * (Rate / 100)
                Next
                If adv_inte = Term Then
                    yint = 0
                Else
                    yint = Math.Round(xint1 / (Term - adv_inte), 2)
                End If
            End If
        Else
            xint = 0
            xint1 = 0
            yint = 0
        End If
        txtAmortizationPri.Text = Format(xpri, "#,##0.00")
        txtxint.Text = Format(xint, "#,##0.00")
        txtyint.Text = Format(yint, "#,##0.00")
        lblAdvInteX.Text = "First " & CStr(adv_inte) & "Months INT"
        lblAdvInteY.Text = "After " & CStr(adv_inte) & "Months INT"

        Return True
    End Function

#End Region

#Region "Events"

    Private Sub LoanRestructuring_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.LoadLedgerType(cboLedgerType)
        Common.LoadLoanType(cboLoanType)
        Common.LoadFrequency(cboFrequency)
        Common.LoadFrequency(cboPaymentMode)

        Common.SetControls(Me, Common.EnumSetControls.Clear)
        Common.SetControls(Me, Common.EnumSetControls.Disable)
    End Sub

#End Region

End Class
