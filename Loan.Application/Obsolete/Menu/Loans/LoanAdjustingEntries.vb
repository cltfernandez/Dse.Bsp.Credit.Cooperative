Imports Loan.Application.Infrastructure.Enumerations.Popups
Imports Loan.Application.Infrastructure.Enumerations

Public Class LoanAdjustingEntries
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
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents mnuLedger As System.Windows.Forms.MenuItem
    Friend WithEvents mnuAdjust As System.Windows.Forms.MenuItem
    Friend WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtDateDue As System.Windows.Forms.TextBox
    Friend WithEvents txtReleaseDate As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cboLedgerType As System.Windows.Forms.ComboBox
    Friend WithEvents cboLoanStatus As System.Windows.Forms.ComboBox
    Friend WithEvents txtAmortization As System.Windows.Forms.TextBox
    Friend WithEvents txtLoanAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents txtPayStart As System.Windows.Forms.TextBox
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
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
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnXARRD As System.Windows.Forms.Button
    Friend WithEvents btnSubmitArrears As System.Windows.Forms.Button
    Friend WithEvents txtXARRD As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtXARRO As System.Windows.Forms.TextBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtXARRI As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtXARRP As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents txtXPINT As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtXBPRIN As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents grpAdjust As System.Windows.Forms.GroupBox
    Friend WithEvents btnSubmitAdjust As System.Windows.Forms.Button
    Friend WithEvents cboXLDC As System.Windows.Forms.ComboBox
    Friend WithEvents cboXODC As System.Windows.Forms.ComboBox
    Friend WithEvents cboXIDC As System.Windows.Forms.ComboBox
    Friend WithEvents cboXPDC As System.Windows.Forms.ComboBox
    Friend WithEvents txtXLAMT As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtXOAMT As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtXIAMT As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtXPAMT As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtXREFNO As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtXTRAND As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents mnuArrears As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoanAdjustingEntries))
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.mnuAdjust = New System.Windows.Forms.MenuItem
        Me.mnuArrears = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.mnuLedger = New System.Windows.Forms.MenuItem
        Me.cboLoanType = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtDateDue = New System.Windows.Forms.TextBox
        Me.txtReleaseDate = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtAmortization = New System.Windows.Forms.TextBox
        Me.cboLoanStatus = New System.Windows.Forms.ComboBox
        Me.txtLoanAmount = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtPayStart = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboFrequency = New System.Windows.Forms.ComboBox
        Me.cboLedgerType = New System.Windows.Forms.ComboBox
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.btnXARRD = New System.Windows.Forms.Button
        Me.btnSubmitArrears = New System.Windows.Forms.Button
        Me.txtXARRD = New System.Windows.Forms.TextBox
        Me.Label31 = New System.Windows.Forms.Label
        Me.txtXARRO = New System.Windows.Forms.TextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.txtXARRI = New System.Windows.Forms.TextBox
        Me.Label29 = New System.Windows.Forms.Label
        Me.txtXARRP = New System.Windows.Forms.TextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.txtXPINT = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.txtXBPRIN = New System.Windows.Forms.TextBox
        Me.Label26 = New System.Windows.Forms.Label
        Me.grpAdjust = New System.Windows.Forms.GroupBox
        Me.btnSubmitAdjust = New System.Windows.Forms.Button
        Me.cboXLDC = New System.Windows.Forms.ComboBox
        Me.cboXODC = New System.Windows.Forms.ComboBox
        Me.cboXIDC = New System.Windows.Forms.ComboBox
        Me.cboXPDC = New System.Windows.Forms.ComboBox
        Me.txtXLAMT = New System.Windows.Forms.TextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.txtXOAMT = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtXIAMT = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtXPAMT = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtXREFNO = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtXTRAND = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.lblTitle = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.grpAdjust.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuAdjust, Me.mnuArrears, Me.MenuItem3, Me.MenuItem4, Me.mnuLedger})
        '
        'mnuAdjust
        '
        Me.mnuAdjust.Index = 0
        Me.mnuAdjust.Text = "Ad&just"
        '
        'mnuArrears
        '
        Me.mnuArrears.Index = 1
        Me.mnuArrears.Text = "A&rrears"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "&Find"
        Me.MenuItem3.Visible = False
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.Text = "&List"
        Me.MenuItem4.Visible = False
        '
        'mnuLedger
        '
        Me.mnuLedger.Index = 4
        Me.mnuLedger.Text = "&View Ledger"
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
        Me.GroupBox2.Location = New System.Drawing.Point(12, 244)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(632, 125)
        Me.GroupBox2.TabIndex = 11
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
        Me.Label11.Location = New System.Drawing.Point(6, 98)
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
        Me.GroupBox4.Location = New System.Drawing.Point(12, 59)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(632, 100)
        Me.GroupBox4.TabIndex = 15
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
        Me.GroupBox1.Location = New System.Drawing.Point(12, 165)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(632, 73)
        Me.GroupBox1.TabIndex = 16
        Me.GroupBox1.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnXARRD)
        Me.GroupBox3.Controls.Add(Me.btnSubmitArrears)
        Me.GroupBox3.Controls.Add(Me.txtXARRD)
        Me.GroupBox3.Controls.Add(Me.Label31)
        Me.GroupBox3.Controls.Add(Me.txtXARRO)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.txtXARRI)
        Me.GroupBox3.Controls.Add(Me.Label29)
        Me.GroupBox3.Controls.Add(Me.txtXARRP)
        Me.GroupBox3.Controls.Add(Me.Label28)
        Me.GroupBox3.Controls.Add(Me.txtXPINT)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.txtXBPRIN)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Location = New System.Drawing.Point(333, 375)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(311, 205)
        Me.GroupBox3.TabIndex = 58
        Me.GroupBox3.TabStop = False
        '
        'btnXARRD
        '
        Me.btnXARRD.Enabled = False
        Me.btnXARRD.Image = Global.Loan.Application.My.Resources.Resources.search
        Me.btnXARRD.Location = New System.Drawing.Point(273, 149)
        Me.btnXARRD.Name = "btnXARRD"
        Me.btnXARRD.Size = New System.Drawing.Size(23, 23)
        Me.btnXARRD.TabIndex = 81
        Me.btnXARRD.UseVisualStyleBackColor = True
        '
        'btnSubmitArrears
        '
        Me.btnSubmitArrears.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmitArrears.Image = CType(resources.GetObject("btnSubmitArrears.Image"), System.Drawing.Image)
        Me.btnSubmitArrears.Location = New System.Drawing.Point(273, 176)
        Me.btnSubmitArrears.Name = "btnSubmitArrears"
        Me.btnSubmitArrears.Size = New System.Drawing.Size(23, 23)
        Me.btnSubmitArrears.TabIndex = 94
        Me.btnSubmitArrears.Visible = False
        '
        'txtXARRD
        '
        Me.txtXARRD.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXARRD.Location = New System.Drawing.Point(144, 151)
        Me.txtXARRD.Name = "txtXARRD"
        Me.txtXARRD.ReadOnly = True
        Me.txtXARRD.Size = New System.Drawing.Size(121, 21)
        Me.txtXARRD.TabIndex = 92
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(8, 154)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(93, 13)
        Me.Label31.TabIndex = 93
        Me.Label31.Text = "Arrears as of"
        '
        'txtXARRO
        '
        Me.txtXARRO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXARRO.Location = New System.Drawing.Point(144, 124)
        Me.txtXARRO.Name = "txtXARRO"
        Me.txtXARRO.ReadOnly = True
        Me.txtXARRO.Size = New System.Drawing.Size(152, 21)
        Me.txtXARRO.TabIndex = 90
        Me.txtXARRO.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(8, 127)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(86, 13)
        Me.Label30.TabIndex = 91
        Me.Label30.Text = "Arrears PEN"
        '
        'txtXARRI
        '
        Me.txtXARRI.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXARRI.Location = New System.Drawing.Point(144, 97)
        Me.txtXARRI.Name = "txtXARRI"
        Me.txtXARRI.ReadOnly = True
        Me.txtXARRI.Size = New System.Drawing.Size(152, 21)
        Me.txtXARRI.TabIndex = 88
        Me.txtXARRI.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(8, 100)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(84, 13)
        Me.Label29.TabIndex = 89
        Me.Label29.Text = "Arrears INT"
        '
        'txtXARRP
        '
        Me.txtXARRP.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXARRP.Location = New System.Drawing.Point(144, 70)
        Me.txtXARRP.Name = "txtXARRP"
        Me.txtXARRP.ReadOnly = True
        Me.txtXARRP.Size = New System.Drawing.Size(152, 21)
        Me.txtXARRP.TabIndex = 86
        Me.txtXARRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(8, 73)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(83, 13)
        Me.Label28.TabIndex = 87
        Me.Label28.Text = "Arrears PRI"
        '
        'txtXPINT
        '
        Me.txtXPINT.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXPINT.Location = New System.Drawing.Point(144, 43)
        Me.txtXPINT.Name = "txtXPINT"
        Me.txtXPINT.ReadOnly = True
        Me.txtXPINT.Size = New System.Drawing.Size(152, 21)
        Me.txtXPINT.TabIndex = 84
        Me.txtXPINT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(8, 46)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(92, 13)
        Me.Label27.TabIndex = 85
        Me.Label27.Text = "Interest Paid"
        '
        'txtXBPRIN
        '
        Me.txtXBPRIN.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXBPRIN.Location = New System.Drawing.Point(144, 16)
        Me.txtXBPRIN.Name = "txtXBPRIN"
        Me.txtXBPRIN.ReadOnly = True
        Me.txtXBPRIN.Size = New System.Drawing.Size(152, 21)
        Me.txtXBPRIN.TabIndex = 82
        Me.txtXBPRIN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.Location = New System.Drawing.Point(8, 19)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(107, 13)
        Me.Label26.TabIndex = 83
        Me.Label26.Text = "Ledger Balance"
        '
        'grpAdjust
        '
        Me.grpAdjust.Controls.Add(Me.btnSubmitAdjust)
        Me.grpAdjust.Controls.Add(Me.cboXLDC)
        Me.grpAdjust.Controls.Add(Me.cboXODC)
        Me.grpAdjust.Controls.Add(Me.cboXIDC)
        Me.grpAdjust.Controls.Add(Me.cboXPDC)
        Me.grpAdjust.Controls.Add(Me.txtXLAMT)
        Me.grpAdjust.Controls.Add(Me.Label25)
        Me.grpAdjust.Controls.Add(Me.txtXOAMT)
        Me.grpAdjust.Controls.Add(Me.Label24)
        Me.grpAdjust.Controls.Add(Me.txtXIAMT)
        Me.grpAdjust.Controls.Add(Me.Label23)
        Me.grpAdjust.Controls.Add(Me.txtXPAMT)
        Me.grpAdjust.Controls.Add(Me.Label22)
        Me.grpAdjust.Controls.Add(Me.txtXREFNO)
        Me.grpAdjust.Controls.Add(Me.Label21)
        Me.grpAdjust.Controls.Add(Me.txtXTRAND)
        Me.grpAdjust.Controls.Add(Me.Label13)
        Me.grpAdjust.Location = New System.Drawing.Point(12, 375)
        Me.grpAdjust.Name = "grpAdjust"
        Me.grpAdjust.Size = New System.Drawing.Size(312, 205)
        Me.grpAdjust.TabIndex = 95
        Me.grpAdjust.TabStop = False
        Me.grpAdjust.Visible = False
        '
        'btnSubmitAdjust
        '
        Me.btnSubmitAdjust.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSubmitAdjust.Image = CType(resources.GetObject("btnSubmitAdjust.Image"), System.Drawing.Image)
        Me.btnSubmitAdjust.Location = New System.Drawing.Point(273, 177)
        Me.btnSubmitAdjust.Name = "btnSubmitAdjust"
        Me.btnSubmitAdjust.Size = New System.Drawing.Size(23, 23)
        Me.btnSubmitAdjust.TabIndex = 83
        '
        'cboXLDC
        '
        Me.cboXLDC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboXLDC.Location = New System.Drawing.Point(142, 151)
        Me.cboXLDC.Name = "cboXLDC"
        Me.cboXLDC.Size = New System.Drawing.Size(48, 21)
        Me.cboXLDC.TabIndex = 81
        '
        'cboXODC
        '
        Me.cboXODC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboXODC.Location = New System.Drawing.Point(142, 124)
        Me.cboXODC.Name = "cboXODC"
        Me.cboXODC.Size = New System.Drawing.Size(48, 21)
        Me.cboXODC.TabIndex = 79
        '
        'cboXIDC
        '
        Me.cboXIDC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboXIDC.Location = New System.Drawing.Point(142, 97)
        Me.cboXIDC.Name = "cboXIDC"
        Me.cboXIDC.Size = New System.Drawing.Size(48, 21)
        Me.cboXIDC.TabIndex = 77
        '
        'cboXPDC
        '
        Me.cboXPDC.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboXPDC.Location = New System.Drawing.Point(142, 70)
        Me.cboXPDC.Name = "cboXPDC"
        Me.cboXPDC.Size = New System.Drawing.Size(48, 21)
        Me.cboXPDC.TabIndex = 75
        '
        'txtXLAMT
        '
        Me.txtXLAMT.Location = New System.Drawing.Point(190, 151)
        Me.txtXLAMT.Name = "txtXLAMT"
        Me.txtXLAMT.Size = New System.Drawing.Size(104, 20)
        Me.txtXLAMT.TabIndex = 82
        Me.txtXLAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(6, 154)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(28, 13)
        Me.Label25.TabIndex = 83
        Me.Label25.Text = "LRI"
        '
        'txtXOAMT
        '
        Me.txtXOAMT.Location = New System.Drawing.Point(190, 124)
        Me.txtXOAMT.Name = "txtXOAMT"
        Me.txtXOAMT.Size = New System.Drawing.Size(104, 20)
        Me.txtXOAMT.TabIndex = 80
        Me.txtXOAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(6, 127)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(56, 13)
        Me.Label24.TabIndex = 81
        Me.Label24.Text = "Penalty"
        '
        'txtXIAMT
        '
        Me.txtXIAMT.Location = New System.Drawing.Point(190, 97)
        Me.txtXIAMT.Name = "txtXIAMT"
        Me.txtXIAMT.Size = New System.Drawing.Size(104, 20)
        Me.txtXIAMT.TabIndex = 78
        Me.txtXIAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(6, 100)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(60, 13)
        Me.Label23.TabIndex = 79
        Me.Label23.Text = "Interest"
        '
        'txtXPAMT
        '
        Me.txtXPAMT.Location = New System.Drawing.Point(190, 70)
        Me.txtXPAMT.Name = "txtXPAMT"
        Me.txtXPAMT.Size = New System.Drawing.Size(104, 20)
        Me.txtXPAMT.TabIndex = 76
        Me.txtXPAMT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(6, 73)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(33, 13)
        Me.Label22.TabIndex = 77
        Me.Label22.Text = "Prin"
        '
        'txtXREFNO
        '
        Me.txtXREFNO.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXREFNO.Location = New System.Drawing.Point(142, 43)
        Me.txtXREFNO.Name = "txtXREFNO"
        Me.txtXREFNO.Size = New System.Drawing.Size(152, 21)
        Me.txtXREFNO.TabIndex = 74
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(6, 46)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(49, 13)
        Me.Label21.TabIndex = 76
        Me.Label21.Text = "Ref No"
        '
        'txtXTRAND
        '
        Me.txtXTRAND.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtXTRAND.Location = New System.Drawing.Point(142, 16)
        Me.txtXTRAND.Name = "txtXTRAND"
        Me.txtXTRAND.Size = New System.Drawing.Size(152, 21)
        Me.txtXTRAND.TabIndex = 73
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(6, 19)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(71, 13)
        Me.Label13.TabIndex = 74
        Me.Label13.Text = "Tran Date"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(386, 37)
        Me.lblTitle.TabIndex = 96
        Me.lblTitle.Text = "Loan Adjusting Entries"
        '
        'LoanAdjustingEntries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(653, 592)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.grpAdjust)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Menu = Me.MainMenu1
        Me.Name = "LoanAdjustingEntries"
        Me.Text = "Adjusting Entries"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.grpAdjust.ResumeLayout(False)
        Me.grpAdjust.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private drDetails As DataRow
    Private _PN_NO As String

#Region "Events"

    Private Sub LoanAdjustingEntries_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.LoadLoanType(cboLoanType)
        Common.LoadFrequency(cboFrequency)
        Common.LoadFrequency(cboPaymentMode)
        Common.LoadLedgerType(cboLedgerType)
        Common.LoadLoanStatus(cboLoanStatus)
        Common.SetComboBoxAutoComplete(cboXIDC)
        Common.SetComboBoxAutoComplete(cboXLDC)
        Common.SetComboBoxAutoComplete(cboXODC)
        Common.SetComboBoxAutoComplete(cboXPDC)
        LoadDC()
    End Sub

#End Region

#Region "Controls"

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub mnuAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAdjust.Click
        If LoadedLoanDetails("LC-1") Then
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            Common.SetControls(Me, Common.EnumSetControls.Enable)
            Common.SetControls(Me, Common.EnumSetControls.Read)

            Me.txtXTRAND.ReadOnly = False
            Me.txtXREFNO.ReadOnly = False

            Me.cboXPDC.Enabled = True
            Me.cboXIDC.Enabled = True
            Me.cboXODC.Enabled = True
            Me.cboXLDC.Enabled = True

            Me.txtXPAMT.Enabled = True
            Me.txtXPAMT.ReadOnly = False
            Me.txtXIAMT.Enabled = True
            Me.txtXIAMT.ReadOnly = False
            Me.txtXOAMT.Enabled = True
            Me.txtXOAMT.ReadOnly = False
            Me.txtXLAMT.Enabled = True
            Me.txtXLAMT.ReadOnly = False

            grpAdjust.Visible = True
            btnSubmitArrears.Visible = False
            btnSubmitArrears.Enabled = False
            btnSubmitAdjust.Visible = True
            btnSubmitAdjust.Enabled = True
            btnXARRD.Enabled = False
            AcceptButton = btnSubmitAdjust

            Common.LoadControlDetails(Me, drDetails)
            Me.txtKBCI.Text = Common.Format241(drDetails.Item("KBCI_NO"))
            Me.txtPNNo.Text = Common.Format241(drDetails.Item("PN_NO"))
            _PN_NO = drDetails.Item("PN_NO").ToString().Trim()
            Common.FormatSeparator(Me)
        End If
    End Sub

    Private Sub mnuArrears_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuArrears.Click
        If LoadedLoanDetails("LC-2") Then
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            Common.SetControls(Me, Common.EnumSetControls.Enable)
            Common.SetControls(Me, Common.EnumSetControls.Read)

            Me.txtXARRP.Enabled = True
            Me.txtXARRP.ReadOnly = False
            Me.txtXARRI.Enabled = True
            Me.txtXARRI.ReadOnly = False
            Me.txtXARRO.Enabled = True
            Me.txtXARRO.ReadOnly = False
            Me.txtXARRD.Enabled = True
            Me.txtXARRD.ReadOnly = False

            grpAdjust.Visible = False
            btnSubmitArrears.Visible = True
            btnSubmitArrears.Enabled = True
            btnSubmitAdjust.Visible = False
            btnSubmitAdjust.Enabled = False

            btnXARRD.Enabled = True
            AcceptButton = btnSubmitArrears

            Common.LoadControlDetails(Me, drDetails)
            Me.txtKBCI.Text = Common.Format241(drDetails.Item("KBCI_NO"))
            Me.txtPNNo.Text = Common.Format241(drDetails.Item("PN_NO"))
            _PN_NO = drDetails.Item("PN_NO").ToString().Trim()
            Common.FormatSeparator(Me)
        End If
    End Sub

    Private Sub mnuLedger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLedger.Click
        Dim popup As New Business.Popups.PnNoAndDateRange()
        popup.DateFrom = New Date(1900, 1, 1)
        If Common.OpenPopup(popup) <> Popups.PopupResponses.Ok Then Exit Sub
        Common.OpenReport(Of Loan.Application.Report.Loans.SubsidiaryLoanLedger)(popup.PnNo, popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub btnSubmitAdjust_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitAdjust.Click
        Dim strMsg As String = String.Empty

        If Me.txtXTRAND.Text = String.Empty OrElse Not IsDate(Me.txtXTRAND.Text) Then
            MsgBox("ERROR: Transaction date invalid.", MsgBoxStyle.Critical, "ERROR")
            Return
        Else
            Dim XTRAND As DateTime = CDate(Me.txtXTRAND.Text)
            Dim CHKNO_DATE As DateTime = CDate(drDetails.Item("txtReleaseDate"))

            If XTRAND <= CHKNO_DATE Then
                MsgBox("ERROR: Transaction date must be greater than check date.", MsgBoxStyle.Critical, "ERROR")
                Return
            End If
        End If

        If Me.cboXPDC.SelectedIndex > 0 And Not IsNumeric(Replace(Me.txtXPAMT.Text, ",", "")) Then
            MsgBox("ERROR: Prin entry invalid.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf Me.cboXIDC.SelectedIndex > 0 And Not IsNumeric(Replace(Me.txtXIAMT.Text, ",", "")) Then
            MsgBox("ERROR: Interest entry invalid.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf Me.cboXODC.SelectedIndex > 0 And Not IsNumeric(Replace(Me.txtXOAMT.Text, ",", "")) Then
            MsgBox("ERROR: Penalty entry invalid.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf Me.cboXLDC.SelectedIndex > 0 And Not IsNumeric(Replace(Me.txtXLAMT.Text, ",", "")) Then
            MsgBox("ERROR: LRI entry invalid.", MsgBoxStyle.Critical, "ERROR")
            Return
        End If

        Dim ln As New Business.LoanAdjustingEntries

        If Me.cboXPDC.SelectedIndex > 0 Then
            ln.AdjustingLoanEntry(drDetails.Item("PN_NO"), Me.txtXTRAND.Text, cboXPDC.Text & "M", Me.txtXREFNO.Text, "ADJ", "PRI", CDbl(Me.txtXPAMT.Text), "LOAN ADJUSTMENT", drDetails.Item("txtXTRAND"))
        End If

        If Me.cboXIDC.SelectedIndex > 0 Then
            ln.AdjustingLoanEntry(drDetails.Item("PN_NO"), Me.txtXTRAND.Text, cboXIDC.Text & "M", Me.txtXREFNO.Text, "ADJ", "INT", CDbl(Me.txtXIAMT.Text), "LOAN ADJUSTMENT", drDetails.Item("txtXTRAND"))
        End If

        If Me.cboXODC.SelectedIndex > 0 Then
            ln.AdjustingLoanEntry(drDetails.Item("PN_NO"), Me.txtXTRAND.Text, cboXODC.Text & "M", Me.txtXREFNO.Text, "ADJ", "OTH", CDbl(Me.txtXOAMT.Text), "LOAN ADJUSTMENT-PEN", drDetails.Item("txtXTRAND"))
        End If

        If Me.cboXLDC.SelectedIndex > 0 Then
            ln.isLRI = True
            ln.AdjustingLoanEntry(drDetails.Item("PN_NO"), Me.txtXTRAND.Text, cboXLDC.Text & "M", Me.txtXREFNO.Text, "ADJ", "LRI", CDbl(Me.txtXLAMT.Text), "LOAN ADJUSTMENT-LRI", drDetails.Item("txtXTRAND"))
        End If

        If ln.IsLoanAdjusted() Then
            grpAdjust.Visible = False
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            Common.SetControls(Me, Common.EnumSetControls.Disable)
            MsgBox("Loan successfully adjusted", MsgBoxStyle.Information)
            btnSubmitAdjust.Enabled = False
        End If
    End Sub

    Private Sub btnSubmitArrears_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmitArrears.Click
        Dim PN_NO As String = drDetails.Item("PN_NO")
        Dim PAY_START As Date = CDate(drDetails.Item("txtPayStart"))
        Dim DATE_DUE As Date = CDate(drDetails.Item("txtDateDue"))
        Dim AXGOMO As Integer = drDetails.Item("AXGOMO")
        Dim AXFREQ As Integer = drDetails.Item("AXFREQ")
        Dim AMORT_AMT As Double = drDetails.Item("txtAmortization")
        Dim XBPRIN As Double = drDetails.Item("txtXBPrin")
        Dim PRINCIPAL As Double = drDetails.Item("txtLoanAmount")

        Dim dtTemp, XARRD As Date
        Dim XARRDO As Object
        Dim XARRI, XARRO, XOPRI, XARRP, XARRIP, XARROP As Double
        Dim RATE As Double

        Dim CAIT, CAIM, CAPT, CAPM, CLBO, CLBC, CLBX As Double
        Dim CDD1, CDD2, XCDD As Date

        If txtXARRP.Text = String.Empty Then
            MsgBox("ERROR: Arrears PRI field required.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf Not IsNumeric(txtXARRP.Text) OrElse CDbl(txtXARRP.Text) < 0 Then
            MsgBox("ERROR: Arrears PRI invalid.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf txtXARRI.Text = String.Empty Then
            MsgBox("ERROR: Arrears INT field required.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf Not IsNumeric(txtXARRI.Text) OrElse CDbl(txtXARRI.Text) < 0 Then
            MsgBox("ERROR: Arrears INT invalid.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf txtXARRO.Text = String.Empty Then
            MsgBox("ERROR: Arrears OTH field required.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf Not IsNumeric(txtXARRO.Text) OrElse CDbl(txtXARRO.Text) < 0 Then
            MsgBox("ERROR: Arrears OTH invalid.", MsgBoxStyle.Critical, "ERROR")
            Return
        ElseIf txtXARRD.Text = String.Empty Then
            MsgBox("ERROR: Arrear as of field required.", MsgBoxStyle.Critical, "ERROR")
            Return
        Else
            XARRP = CDbl(txtXARRP.Text)
            XARRI = CDbl(txtXARRI.Text)
            XARRO = CDbl(txtXARRO.Text)
            XARRD = CDate(Me.txtXARRD.Text)
            dtTemp = DateAdd(DateInterval.Month, AXGOMO, sysdate)

            If XARRD >= dtTemp Then
                MsgBox("ERROR: Arrear as of should be less than " & CStr(dtTemp), MsgBoxStyle.Critical, "ERROR")
                Return
            End If

            dtTemp = DateAdd(DateInterval.Day, 1, PAY_START)
            If XARRD.Day <> dtTemp.Day Then
                MsgBox("ERROR: Invalid day specified for Arrear as of date!!!" & vbCrLf & "Should be the same day as " & CStr(dtTemp) & ".", MsgBoxStyle.Critical, "ERROR")
                Return
            ElseIf XARRD < DateAdd(DateInterval.Month, AXGOMO, CDate(drDetails.Item("XLASTD"))) Then
                MsgBox("ERROR: Date specified is less than last Payment/Adjustment date!!!", MsgBoxStyle.Critical, "ERROR")
                Return
            End If

            If CStr(drDetails.Item("LOAN_TYPE")) = "STL" Then
                RATE = CDbl(drDetails.Item("RATE"))

                XARRI = Math.Round((XBPRIN * (RATE / 100)) / AXFREQ, 2)
                XARRI = XARRI + (XBPRIN * (RATE / 100 / 360) * (DateDiff(DateInterval.Day, XARRD, sysdate) + 1))

                If drDetails.Item("XLASPD") Is DBNull.Value Then
                    XARRO = AMORT_AMT * (12 / 100 / 360) * (DateDiff(DateInterval.Day, DateAdd(DateInterval.Month, AXGOMO, XARRD), sysdate) + 1)
                Else
                    XARRO = AMORT_AMT * (12 / 100 / 360) * (DateDiff(DateInterval.Day, XARRD, sysdate) + 1)
                End If
            Else
                XARRO = AMORT_AMT * (12 / 100 / 360) * (DateDiff(DateInterval.Day, XARRD, sysdate))
            End If

            If DATE_DUE < sysdate Then
                CAIT = 0
                CAIM = 0
                CAPT = 0
                CAPM = 0
                CLBO = PRINCIPAL
                CLBC = 0
                CLBX = 0
                CDD1 = DateAdd(DateInterval.Day, AXGOMO, PAY_START)
                CDD2 = sysdate
                XCDD = CDD1
                Do While True
                    XCDD = DateAdd(DateInterval.Day, 0 - AXGOMO, XCDD)
                    If XCDD > CDD2 Then
                        Exit Do
                    End If
                    CLBX = Math.Round((CLBO - CLBC), 2)
                    CAIM = Math.Round(CLBX * (RATE / 100) * (1 / AXFREQ), 2)
                    If CAPM > CLBX Then
                        CAPM = CLBX
                    End If
                    CAPT = CAPT + CAPM
                    CLBC = CLBC + CAPM
                Loop
                XOPRI = PRINCIPAL - XBPRIN
                XARRP = CLBC - XOPRI
            End If

            Dim dr As DataRow
            dr = Common.GetDetailsOld("LC-3", PN_NO, CStr(XARRD)).Rows(0)
            XARRIP = Common.IsDBNullNum(dr.Item("XARRIP"))
            XARROP = Common.IsDBNullNum(dr.Item("XARROP"))

            XARRI = XARRI - XARRIP
            XARRO = XARRO - XARROP

            If XARRI < 0 Then XARRI = 0
            If XARRO < 0 Then XARRO = 0

            XARRDO = XARRD

            If XARRP = 0 And XARRI = 0 And XARRO = 0 Then
                If MsgBox("Arrear as will bet set to blank. Continue?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    Me.txtXARRD.Text = String.Empty
                    XARRDO = DBNull.Value
                Else
                    MsgBox("ERROR: Arrear as should be blank !!!", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If

            Dim ln As New Business.LoanAdjustingEntries
            If Not ln.IsArrearsAdjusted(drDetails.Item("PN_NO"), XARRP, XARRI, XARRO, XARRDO) Then
                Exit Sub
            Else
                Common.SetControls(Me, Common.EnumSetControls.Clear)
                Common.SetControls(Me, Common.EnumSetControls.Disable)
                btnSubmitArrears.Enabled = False
            End If

            dr = Nothing
        End If
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

    Private Sub txtXLAMT_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtXLAMT.LostFocus
        Common.FormatSeparator(txtXLAMT)
    End Sub

    Private Sub txtXARRP_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtXARRP.LostFocus
        Common.FormatSeparator(txtXARRP)
    End Sub

#End Region

#Region "Subs"

    Private Sub LoadDC()
        Me.cboXPDC.Items.Insert(0, "[SELECT]")
        Me.cboXPDC.Items.Insert(1, "C")
        Me.cboXPDC.Items.Insert(1, "D")

        Me.cboXIDC.Items.Insert(0, "[SELECT]")
        Me.cboXIDC.Items.Insert(1, "C")
        Me.cboXIDC.Items.Insert(1, "D")

        Me.cboXODC.Items.Insert(0, "[SELECT]")
        Me.cboXODC.Items.Insert(1, "C")
        Me.cboXODC.Items.Insert(1, "D")

        Me.cboXLDC.Items.Insert(0, "[SELECT]")
        Me.cboXLDC.Items.Insert(1, "C")
        Me.cboXLDC.Items.Insert(1, "D")
    End Sub

    Private Function LoadedLoanDetails(ByVal Code As String) As Boolean
        Dim dr As DataRow
        dr = Common.FindLoan(Code, "LB-8")

        If dr IsNot Nothing Then
            drDetails = dr
            Return True
        Else
            Return False
        End If
    End Function

#End Region

End Class
