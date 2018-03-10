Public Class frmLoan
    Inherits KBCIForms

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
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem6 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem7 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem8 As System.Windows.Forms.MenuItem
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtKBCI As System.Windows.Forms.TextBox
    Friend WithEvents txtLName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtFName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPNNo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtMI As System.Windows.Forms.TextBox
    Friend WithEvents txtDateApplied As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtLoanAmount As System.Windows.Forms.TextBox
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtAmortization As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents chkLRI As System.Windows.Forms.CheckBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents lblTerm As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtPayStart As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtReleaseDate As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtDateDue As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtNetProceeds As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtLoanCondition As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtCheckNo As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtCoMaker1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCoMaker2 As System.Windows.Forms.TextBox
    Friend WithEvents MenuItem9 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem10 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem11 As System.Windows.Forms.MenuItem
    Friend WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLedgerType As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents txtCollateral1 As System.Windows.Forms.TextBox
    Friend WithEvents txtCollateral2 As System.Windows.Forms.TextBox
    Friend WithEvents txtCollateral3 As System.Windows.Forms.TextBox
    Friend WithEvents txtCollateral4 As System.Windows.Forms.TextBox
    Friend WithEvents txtCollateral5 As System.Windows.Forms.TextBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.MainMenu1 = New System.Windows.Forms.MainMenu
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem9 = New System.Windows.Forms.MenuItem
        Me.MenuItem10 = New System.Windows.Forms.MenuItem
        Me.MenuItem11 = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.MenuItem6 = New System.Windows.Forms.MenuItem
        Me.MenuItem7 = New System.Windows.Forms.MenuItem
        Me.MenuItem8 = New System.Windows.Forms.MenuItem
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtKBCI = New System.Windows.Forms.TextBox
        Me.txtLName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtFName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtPNNo = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtMI = New System.Windows.Forms.TextBox
        Me.txtDateApplied = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtLoanAmount = New System.Windows.Forms.TextBox
        Me.cboFrequency = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtAmortization = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.chkLRI = New System.Windows.Forms.CheckBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.lblTerm = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.txtPayStart = New System.Windows.Forms.TextBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.txtReleaseDate = New System.Windows.Forms.TextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.Label20 = New System.Windows.Forms.Label
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtDateDue = New System.Windows.Forms.TextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtNetProceeds = New System.Windows.Forms.TextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.txtLoanCondition = New System.Windows.Forms.TextBox
        Me.Label23 = New System.Windows.Forms.Label
        Me.txtBank = New System.Windows.Forms.TextBox
        Me.Label24 = New System.Windows.Forms.Label
        Me.txtCheckNo = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.cboLoanType = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtLedgerType = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txtCollateral5 = New System.Windows.Forms.TextBox
        Me.txtCollateral4 = New System.Windows.Forms.TextBox
        Me.txtCollateral3 = New System.Windows.Forms.TextBox
        Me.txtCollateral2 = New System.Windows.Forms.TextBox
        Me.txtCollateral1 = New System.Windows.Forms.TextBox
        Me.txtCoMaker2 = New System.Windows.Forms.TextBox
        Me.txtCoMaker1 = New System.Windows.Forms.TextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label26 = New System.Windows.Forms.Label
        Me.Label25 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3, Me.MenuItem4, Me.MenuItem5, Me.MenuItem6, Me.MenuItem7, Me.MenuItem8})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem9, Me.MenuItem10, Me.MenuItem11})
        Me.MenuItem1.Text = "F&ile"
        '
        'MenuItem9
        '
        Me.MenuItem9.Index = 0
        Me.MenuItem9.Text = "&New"
        '
        'MenuItem10
        '
        Me.MenuItem10.Index = 1
        Me.MenuItem10.Text = "&Save"
        '
        'MenuItem11
        '
        Me.MenuItem11.Index = 2
        Me.MenuItem11.Text = "&Cancel"
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.Text = "&Find"
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.Text = "N&ext"
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 3
        Me.MenuItem4.Text = "&Previous"
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 4
        Me.MenuItem5.Text = "&Top"
        '
        'MenuItem6
        '
        Me.MenuItem6.Index = 5
        Me.MenuItem6.Text = "&Bottom"
        '
        'MenuItem7
        '
        Me.MenuItem7.Index = 6
        Me.MenuItem7.Text = "&List"
        '
        'MenuItem8
        '
        Me.MenuItem8.Index = 7
        Me.MenuItem8.Text = "E&xit"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "KBCI No."
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 40)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 23)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "LName"
        '
        'txtKBCI
        '
        Me.txtKBCI.Location = New System.Drawing.Point(88, 16)
        Me.txtKBCI.MaxLength = 9
        Me.txtKBCI.Name = "txtKBCI"
        Me.txtKBCI.Size = New System.Drawing.Size(152, 20)
        Me.txtKBCI.TabIndex = 1
        Me.txtKBCI.Text = ""
        '
        'txtLName
        '
        Me.txtLName.Location = New System.Drawing.Point(88, 40)
        Me.txtLName.Name = "txtLName"
        Me.txtLName.Size = New System.Drawing.Size(152, 20)
        Me.txtLName.TabIndex = 4
        Me.txtLName.Text = ""
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(264, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 23)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Date Applied"
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(264, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(72, 23)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "FName"
        '
        'txtFName
        '
        Me.txtFName.Location = New System.Drawing.Point(344, 40)
        Me.txtFName.Name = "txtFName"
        Me.txtFName.Size = New System.Drawing.Size(152, 20)
        Me.txtFName.TabIndex = 5
        Me.txtFName.Text = ""
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(520, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 23)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "PN No."
        '
        'txtPNNo
        '
        Me.txtPNNo.Location = New System.Drawing.Point(600, 16)
        Me.txtPNNo.Name = "txtPNNo"
        Me.txtPNNo.Size = New System.Drawing.Size(152, 20)
        Me.txtPNNo.TabIndex = 3
        Me.txtPNNo.Text = ""
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(520, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 23)
        Me.Label8.TabIndex = 14
        Me.Label8.Text = "MI"
        '
        'txtMI
        '
        Me.txtMI.Location = New System.Drawing.Point(600, 40)
        Me.txtMI.Name = "txtMI"
        Me.txtMI.Size = New System.Drawing.Size(152, 20)
        Me.txtMI.TabIndex = 6
        Me.txtMI.Text = ""
        '
        'txtDateApplied
        '
        Me.txtDateApplied.Location = New System.Drawing.Point(344, 16)
        Me.txtDateApplied.Name = "txtDateApplied"
        Me.txtDateApplied.Size = New System.Drawing.Size(152, 20)
        Me.txtDateApplied.TabIndex = 2
        Me.txtDateApplied.Text = ""
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(8, 40)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(120, 23)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "Loan Amount"
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.Location = New System.Drawing.Point(144, 40)
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.Size = New System.Drawing.Size(152, 20)
        Me.txtLoanAmount.TabIndex = 9
        Me.txtLoanAmount.Text = ""
        '
        'cboFrequency
        '
        Me.cboFrequency.Location = New System.Drawing.Point(144, 64)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(152, 21)
        Me.cboFrequency.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(8, 64)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 23)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Frequency of Payment"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(8, 88)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(120, 23)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Amortization Value"
        '
        'txtAmortization
        '
        Me.txtAmortization.Location = New System.Drawing.Point(144, 88)
        Me.txtAmortization.Name = "txtAmortization"
        Me.txtAmortization.Size = New System.Drawing.Size(152, 20)
        Me.txtAmortization.TabIndex = 11
        Me.txtAmortization.Text = ""
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(8, 112)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(120, 23)
        Me.Label12.TabIndex = 23
        Me.Label12.Text = "Payment Mode"
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.Location = New System.Drawing.Point(144, 112)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.Size = New System.Drawing.Size(152, 21)
        Me.cboPaymentMode.TabIndex = 12
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(8, 136)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(120, 23)
        Me.Label13.TabIndex = 25
        Me.Label13.Text = "Exempt from LRI"
        '
        'chkLRI
        '
        Me.chkLRI.Checked = True
        Me.chkLRI.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkLRI.Location = New System.Drawing.Point(144, 136)
        Me.chkLRI.Name = "chkLRI"
        Me.chkLRI.TabIndex = 13
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(392, 40)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(120, 23)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Term"
        '
        'txtTerm
        '
        Me.txtTerm.Location = New System.Drawing.Point(528, 40)
        Me.txtTerm.MaxLength = 10
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.Size = New System.Drawing.Size(152, 20)
        Me.txtTerm.TabIndex = 14
        Me.txtTerm.Text = ""
        '
        'lblTerm
        '
        Me.lblTerm.Location = New System.Drawing.Point(680, 40)
        Me.lblTerm.Name = "lblTerm"
        Me.lblTerm.Size = New System.Drawing.Size(48, 23)
        Me.lblTerm.TabIndex = 29
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(392, 64)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 23)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Rate"
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(528, 64)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(152, 20)
        Me.txtRate.TabIndex = 15
        Me.txtRate.Text = ""
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(680, 64)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(48, 23)
        Me.Label16.TabIndex = 32
        Me.Label16.Text = "%"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(392, 88)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(120, 23)
        Me.Label17.TabIndex = 33
        Me.Label17.Text = "Pay Start"
        '
        'txtPayStart
        '
        Me.txtPayStart.Location = New System.Drawing.Point(528, 88)
        Me.txtPayStart.Name = "txtPayStart"
        Me.txtPayStart.Size = New System.Drawing.Size(152, 20)
        Me.txtPayStart.TabIndex = 16
        Me.txtPayStart.Text = ""
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(392, 112)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(120, 23)
        Me.Label18.TabIndex = 35
        Me.Label18.Text = "Release Date"
        '
        'txtReleaseDate
        '
        Me.txtReleaseDate.Location = New System.Drawing.Point(528, 112)
        Me.txtReleaseDate.Name = "txtReleaseDate"
        Me.txtReleaseDate.Size = New System.Drawing.Size(152, 20)
        Me.txtReleaseDate.TabIndex = 17
        Me.txtReleaseDate.Text = ""
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(392, 136)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(120, 23)
        Me.Label19.TabIndex = 37
        Me.Label19.Text = "Loan Status"
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(392, 160)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(120, 23)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "Date Due"
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(528, 136)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(152, 20)
        Me.txtStatus.TabIndex = 18
        Me.txtStatus.Text = ""
        '
        'txtDateDue
        '
        Me.txtDateDue.Location = New System.Drawing.Point(528, 160)
        Me.txtDateDue.Name = "txtDateDue"
        Me.txtDateDue.Size = New System.Drawing.Size(152, 20)
        Me.txtDateDue.TabIndex = 19
        Me.txtDateDue.Text = ""
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(392, 184)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(120, 23)
        Me.Label21.TabIndex = 42
        Me.Label21.Text = "Net Proceeds"
        '
        'txtNetProceeds
        '
        Me.txtNetProceeds.Location = New System.Drawing.Point(528, 184)
        Me.txtNetProceeds.Name = "txtNetProceeds"
        Me.txtNetProceeds.Size = New System.Drawing.Size(152, 20)
        Me.txtNetProceeds.TabIndex = 20
        Me.txtNetProceeds.Text = ""
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(392, 208)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(120, 23)
        Me.Label22.TabIndex = 44
        Me.Label22.Text = "Loan Condition"
        '
        'txtLoanCondition
        '
        Me.txtLoanCondition.Location = New System.Drawing.Point(528, 208)
        Me.txtLoanCondition.Name = "txtLoanCondition"
        Me.txtLoanCondition.Size = New System.Drawing.Size(152, 20)
        Me.txtLoanCondition.TabIndex = 21
        Me.txtLoanCondition.Text = ""
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(392, 232)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(120, 23)
        Me.Label23.TabIndex = 46
        Me.Label23.Text = "Bank"
        '
        'txtBank
        '
        Me.txtBank.Location = New System.Drawing.Point(528, 232)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(152, 20)
        Me.txtBank.TabIndex = 22
        Me.txtBank.Text = ""
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(392, 256)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(120, 23)
        Me.Label24.TabIndex = 48
        Me.Label24.Text = "Check No."
        '
        'txtCheckNo
        '
        Me.txtCheckNo.Location = New System.Drawing.Point(528, 256)
        Me.txtCheckNo.Name = "txtCheckNo"
        Me.txtCheckNo.Size = New System.Drawing.Size(152, 20)
        Me.txtCheckNo.TabIndex = 23
        Me.txtCheckNo.Text = ""
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
        Me.GroupBox1.Location = New System.Drawing.Point(8, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(768, 72)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cboLoanType)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtLedgerType)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtTerm)
        Me.GroupBox2.Controls.Add(Me.cboPaymentMode)
        Me.GroupBox2.Controls.Add(Me.lblTerm)
        Me.GroupBox2.Controls.Add(Me.txtAmortization)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.txtLoanAmount)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.txtDateDue)
        Me.GroupBox2.Controls.Add(Me.txtReleaseDate)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtRate)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.txtNetProceeds)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.txtLoanCondition)
        Me.GroupBox2.Controls.Add(Me.cboFrequency)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtBank)
        Me.GroupBox2.Controls.Add(Me.txtStatus)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtPayStart)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.txtCheckNo)
        Me.GroupBox2.Controls.Add(Me.chkLRI)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 80)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(768, 288)
        Me.GroupBox2.TabIndex = 9
        Me.GroupBox2.TabStop = False
        '
        'cboLoanType
        '
        Me.cboLoanType.Location = New System.Drawing.Point(144, 16)
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.Size = New System.Drawing.Size(152, 21)
        Me.cboLoanType.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 23)
        Me.Label3.TabIndex = 49
        Me.Label3.Text = "Loan Type"
        '
        'txtLedgerType
        '
        Me.txtLedgerType.Location = New System.Drawing.Point(528, 16)
        Me.txtLedgerType.Name = "txtLedgerType"
        Me.txtLedgerType.Size = New System.Drawing.Size(152, 20)
        Me.txtLedgerType.TabIndex = 14
        Me.txtLedgerType.Text = ""
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(392, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 23)
        Me.Label6.TabIndex = 52
        Me.Label6.Text = "Ledger Type"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtCollateral5)
        Me.GroupBox3.Controls.Add(Me.txtCollateral4)
        Me.GroupBox3.Controls.Add(Me.txtCollateral3)
        Me.GroupBox3.Controls.Add(Me.txtCollateral2)
        Me.GroupBox3.Controls.Add(Me.txtCollateral1)
        Me.GroupBox3.Controls.Add(Me.txtCoMaker2)
        Me.GroupBox3.Controls.Add(Me.txtCoMaker1)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Location = New System.Drawing.Point(8, 376)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(768, 96)
        Me.GroupBox3.TabIndex = 24
        Me.GroupBox3.TabStop = False
        '
        'txtCollateral5
        '
        Me.txtCollateral5.Location = New System.Drawing.Point(568, 64)
        Me.txtCollateral5.Name = "txtCollateral5"
        Me.txtCollateral5.Size = New System.Drawing.Size(120, 20)
        Me.txtCollateral5.TabIndex = 30
        Me.txtCollateral5.Text = ""
        '
        'txtCollateral4
        '
        Me.txtCollateral4.Location = New System.Drawing.Point(448, 64)
        Me.txtCollateral4.Name = "txtCollateral4"
        Me.txtCollateral4.Size = New System.Drawing.Size(120, 20)
        Me.txtCollateral4.TabIndex = 29
        Me.txtCollateral4.Text = ""
        '
        'txtCollateral3
        '
        Me.txtCollateral3.Location = New System.Drawing.Point(328, 64)
        Me.txtCollateral3.Name = "txtCollateral3"
        Me.txtCollateral3.Size = New System.Drawing.Size(120, 20)
        Me.txtCollateral3.TabIndex = 28
        Me.txtCollateral3.Text = ""
        '
        'txtCollateral2
        '
        Me.txtCollateral2.Location = New System.Drawing.Point(208, 64)
        Me.txtCollateral2.Name = "txtCollateral2"
        Me.txtCollateral2.Size = New System.Drawing.Size(120, 20)
        Me.txtCollateral2.TabIndex = 27
        Me.txtCollateral2.Text = ""
        '
        'txtCollateral1
        '
        Me.txtCollateral1.Location = New System.Drawing.Point(88, 64)
        Me.txtCollateral1.Name = "txtCollateral1"
        Me.txtCollateral1.Size = New System.Drawing.Size(120, 20)
        Me.txtCollateral1.TabIndex = 26
        Me.txtCollateral1.Text = ""
        '
        'txtCoMaker2
        '
        Me.txtCoMaker2.Location = New System.Drawing.Point(88, 40)
        Me.txtCoMaker2.Name = "txtCoMaker2"
        Me.txtCoMaker2.Size = New System.Drawing.Size(152, 20)
        Me.txtCoMaker2.TabIndex = 25
        Me.txtCoMaker2.Text = ""
        '
        'txtCoMaker1
        '
        Me.txtCoMaker1.Location = New System.Drawing.Point(88, 16)
        Me.txtCoMaker1.Name = "txtCoMaker1"
        Me.txtCoMaker1.Size = New System.Drawing.Size(152, 20)
        Me.txtCoMaker1.TabIndex = 24
        Me.txtCoMaker1.Text = ""
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(8, 64)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(72, 23)
        Me.Label27.TabIndex = 52
        Me.Label27.Text = "Collateral"
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(8, 40)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(72, 23)
        Me.Label26.TabIndex = 50
        Me.Label26.Text = "Co-Maker 2"
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(8, 16)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(72, 23)
        Me.Label25.TabIndex = 49
        Me.Label25.Text = "Co-Maker 1"
        '
        'frmLoan
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(794, 478)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox3)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Menu = Me.MainMenu1
        Me.Name = "frmLoan"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LOANS FORM"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private strKBCI As String

    Private Sub frmLoan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Common.LoadLoanType(cboLoanType)
        Common.LoadFrequency(cboFrequency)
        Common.LoadFrequency(cboPaymentMode)
        Width = 800
        Height = 520
    End Sub

    Private Sub txtTerm_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTerm.TextChanged
        If IsNumeric(txtTerm.Text) Then
            If Val(txtTerm.Text) = 1 Then
                lblTerm.Text = "month"
            Else
                lblTerm.Text = "months"
            End If
        Else
            lblTerm.Text = String.Empty
        End If
    End Sub

    Private Sub txtKBCI_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKBCI.TextChanged
        If Len(txtKBCI.Text) - Len(strKBCI) Then

        ElseIf (Len(txtKBCI.Text) = 2 Or Len(txtKBCI.Text) = 7) And Len(strKBCI) < Len(txtKBCI.Text) Then
            txtKBCI.AppendText("-")
        End If

        strKBCI = txtKBCI.Text
    End Sub
End Class
