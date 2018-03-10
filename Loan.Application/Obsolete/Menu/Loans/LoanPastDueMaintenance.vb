Public Class LoanPastDueMaintenance
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
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPNNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboLoanType As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtLoanAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtTerm As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblTerm As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cboPaymentMode As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents txtDateDue As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtNetProceeds As System.Windows.Forms.TextBox
    Friend WithEvents txtArPri As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtArInt As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtArPen As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtPayInt As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPayPri As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtKBCI = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPNNo = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.cboLoanType = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtLoanAmount = New System.Windows.Forms.TextBox
        Me.txtTerm = New System.Windows.Forms.TextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.lblTerm = New System.Windows.Forms.Label
        Me.txtRate = New System.Windows.Forms.TextBox
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label16 = New System.Windows.Forms.Label
        Me.cboPaymentMode = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboFrequency = New System.Windows.Forms.ComboBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.txtDateDue = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.Label21 = New System.Windows.Forms.Label
        Me.txtNetProceeds = New System.Windows.Forms.TextBox
        Me.txtArPri = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtArInt = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtArPen = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtPayInt = New System.Windows.Forms.TextBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtPayPri = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtKBCI
        '
        Me.txtKBCI.Location = New System.Drawing.Point(88, 8)
        Me.txtKBCI.MaxLength = 9
        Me.txtKBCI.Name = "txtKBCI"
        Me.txtKBCI.Size = New System.Drawing.Size(152, 20)
        Me.txtKBCI.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 23)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "KBCI No."
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(416, 8)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(312, 20)
        Me.txtName.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(280, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 23)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Name"
        '
        'txtPNNo
        '
        Me.txtPNNo.Location = New System.Drawing.Point(88, 32)
        Me.txtPNNo.Name = "txtPNNo"
        Me.txtPNNo.Size = New System.Drawing.Size(152, 20)
        Me.txtPNNo.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(8, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 23)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "PN No."
        '
        'cboLoanType
        '
        Me.cboLoanType.Location = New System.Drawing.Point(416, 32)
        Me.cboLoanType.Name = "cboLoanType"
        Me.cboLoanType.Size = New System.Drawing.Size(152, 21)
        Me.cboLoanType.TabIndex = 50
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(280, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 23)
        Me.Label3.TabIndex = 51
        Me.Label3.Text = "Loan Type / Amount"
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.Location = New System.Drawing.Point(576, 32)
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.Size = New System.Drawing.Size(152, 20)
        Me.txtLoanAmount.TabIndex = 52
        '
        'txtTerm
        '
        Me.txtTerm.Location = New System.Drawing.Point(88, 56)
        Me.txtTerm.MaxLength = 10
        Me.txtTerm.Name = "txtTerm"
        Me.txtTerm.Size = New System.Drawing.Size(96, 20)
        Me.txtTerm.TabIndex = 53
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(8, 56)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 23)
        Me.Label14.TabIndex = 54
        Me.Label14.Text = "Term"
        '
        'lblTerm
        '
        Me.lblTerm.Location = New System.Drawing.Point(192, 56)
        Me.lblTerm.Name = "lblTerm"
        Me.lblTerm.Size = New System.Drawing.Size(48, 23)
        Me.lblTerm.TabIndex = 55
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(416, 56)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(152, 20)
        Me.txtRate.TabIndex = 56
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(280, 56)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 23)
        Me.Label15.TabIndex = 57
        Me.Label15.Text = "Rate"
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(568, 56)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(48, 23)
        Me.Label16.TabIndex = 58
        Me.Label16.Text = "%"
        '
        'cboPaymentMode
        '
        Me.cboPaymentMode.Location = New System.Drawing.Point(88, 80)
        Me.cboPaymentMode.Name = "cboPaymentMode"
        Me.cboPaymentMode.Size = New System.Drawing.Size(152, 21)
        Me.cboPaymentMode.TabIndex = 59
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(8, 80)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(80, 23)
        Me.Label12.TabIndex = 60
        Me.Label12.Text = "Payment Mode"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(280, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(120, 23)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "Frequency of Payment"
        '
        'cboFrequency
        '
        Me.cboFrequency.Location = New System.Drawing.Point(416, 80)
        Me.cboFrequency.Name = "cboFrequency"
        Me.cboFrequency.Size = New System.Drawing.Size(152, 21)
        Me.cboFrequency.TabIndex = 61
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(280, 104)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(120, 23)
        Me.Label19.TabIndex = 64
        Me.Label19.Text = "Loan Status"
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(416, 104)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(152, 20)
        Me.txtStatus.TabIndex = 63
        '
        'txtDateDue
        '
        Me.txtDateDue.Location = New System.Drawing.Point(416, 128)
        Me.txtDateDue.Name = "txtDateDue"
        Me.txtDateDue.Size = New System.Drawing.Size(152, 20)
        Me.txtDateDue.TabIndex = 65
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(280, 128)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(120, 23)
        Me.Label20.TabIndex = 66
        Me.Label20.Text = "Date Due"
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(280, 152)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(120, 23)
        Me.Label21.TabIndex = 68
        Me.Label21.Text = "Net Proceeds"
        '
        'txtNetProceeds
        '
        Me.txtNetProceeds.Location = New System.Drawing.Point(416, 152)
        Me.txtNetProceeds.Name = "txtNetProceeds"
        Me.txtNetProceeds.Size = New System.Drawing.Size(152, 20)
        Me.txtNetProceeds.TabIndex = 67
        '
        'txtArPri
        '
        Me.txtArPri.Location = New System.Drawing.Point(80, 24)
        Me.txtArPri.MaxLength = 9
        Me.txtArPri.Name = "txtArPri"
        Me.txtArPri.Size = New System.Drawing.Size(152, 20)
        Me.txtArPri.TabIndex = 70
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 23)
        Me.Label5.TabIndex = 71
        Me.Label5.Text = "PRI"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 48)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(48, 23)
        Me.Label6.TabIndex = 73
        Me.Label6.Text = "INT"
        '
        'txtArInt
        '
        Me.txtArInt.Location = New System.Drawing.Point(80, 48)
        Me.txtArInt.MaxLength = 9
        Me.txtArInt.Name = "txtArInt"
        Me.txtArInt.Size = New System.Drawing.Size(152, 20)
        Me.txtArInt.TabIndex = 72
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(8, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(48, 23)
        Me.Label8.TabIndex = 75
        Me.Label8.Text = "PEN"
        '
        'txtArPen
        '
        Me.txtArPen.Location = New System.Drawing.Point(80, 72)
        Me.txtArPen.MaxLength = 9
        Me.txtArPen.Name = "txtArPen"
        Me.txtArPen.Size = New System.Drawing.Size(152, 20)
        Me.txtArPen.TabIndex = 74
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(16, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(48, 23)
        Me.Label11.TabIndex = 80
        Me.Label11.Text = "INT"
        '
        'txtPayInt
        '
        Me.txtPayInt.Location = New System.Drawing.Point(80, 48)
        Me.txtPayInt.MaxLength = 9
        Me.txtPayInt.Name = "txtPayInt"
        Me.txtPayInt.Size = New System.Drawing.Size(152, 20)
        Me.txtPayInt.TabIndex = 79
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(16, 24)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(48, 23)
        Me.Label13.TabIndex = 78
        Me.Label13.Text = "PRI"
        '
        'txtPayPri
        '
        Me.txtPayPri.Location = New System.Drawing.Point(80, 24)
        Me.txtPayPri.MaxLength = 9
        Me.txtPayPri.Name = "txtPayPri"
        Me.txtPayPri.Size = New System.Drawing.Size(152, 20)
        Me.txtPayPri.TabIndex = 77
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtArPri)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtArInt)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtArPen)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(244, 104)
        Me.GroupBox1.TabIndex = 81
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Arrears"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtPayInt)
        Me.GroupBox2.Controls.Add(Me.txtPayPri)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 224)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(244, 80)
        Me.GroupBox2.TabIndex = 82
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Total Payments Made"
        '
        'LoanPastDueMaintenance
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(744, 317)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.txtNetProceeds)
        Me.Controls.Add(Me.txtDateDue)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboFrequency)
        Me.Controls.Add(Me.cboPaymentMode)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtRate)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblTerm)
        Me.Controls.Add(Me.txtTerm)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.txtLoanAmount)
        Me.Controls.Add(Me.cboLoanType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtPNNo)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtKBCI)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "LoanPastDueMaintenance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "PAST DUE LOANS - MAINTENANCE"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

End Class
