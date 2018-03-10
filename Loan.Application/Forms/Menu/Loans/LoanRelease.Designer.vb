<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LoanRelease
    Inherits Loan.Application.Infrastructure.Forms.Windows.BaseForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.tpCharges = New System.Windows.Forms.TabPage
        Me.dgvCharges = New System.Windows.Forms.DataGridView
        Me.tpDeductions = New System.Windows.Forms.TabPage
        Me.ucDeductions = New Loan.Application.SearchAndSelect
        Me.tpCollaterals = New System.Windows.Forms.TabPage
        Me.ucCollaterals = New Loan.Application.SearchAndSelect
        Me.tpDetails = New System.Windows.Forms.TabPage
        Me.pgDetails = New System.Windows.Forms.PropertyGrid
        Me.tcMain = New System.Windows.Forms.TabControl
        Me.tpNetProceeds = New System.Windows.Forms.TabPage
        Me.dgvNetProceeds = New System.Windows.Forms.DataGridView
        Me.mnuLoanRelease = New System.Windows.Forms.MenuStrip
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.lblTitle = New System.Windows.Forms.Label
        Me.tpCharges.SuspendLayout()
        CType(Me.dgvCharges, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tpDeductions.SuspendLayout()
        Me.tpCollaterals.SuspendLayout()
        Me.tpDetails.SuspendLayout()
        Me.tcMain.SuspendLayout()
        Me.tpNetProceeds.SuspendLayout()
        CType(Me.dgvNetProceeds, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuLoanRelease.SuspendLayout()
        Me.SuspendLayout()
        '
        'tpCharges
        '
        Me.tpCharges.Controls.Add(Me.dgvCharges)
        Me.tpCharges.Location = New System.Drawing.Point(4, 22)
        Me.tpCharges.Name = "tpCharges"
        Me.tpCharges.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCharges.Size = New System.Drawing.Size(512, 412)
        Me.tpCharges.TabIndex = 3
        Me.tpCharges.Text = "Charges"
        Me.tpCharges.UseVisualStyleBackColor = True
        '
        'dgvCharges
        '
        Me.dgvCharges.AllowUserToResizeColumns = False
        Me.dgvCharges.AllowUserToResizeRows = False
        Me.dgvCharges.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCharges.Location = New System.Drawing.Point(6, 6)
        Me.dgvCharges.Name = "dgvCharges"
        Me.dgvCharges.Size = New System.Drawing.Size(500, 400)
        Me.dgvCharges.TabIndex = 0
        '
        'tpDeductions
        '
        Me.tpDeductions.Controls.Add(Me.ucDeductions)
        Me.tpDeductions.Location = New System.Drawing.Point(4, 22)
        Me.tpDeductions.Name = "tpDeductions"
        Me.tpDeductions.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDeductions.Size = New System.Drawing.Size(512, 412)
        Me.tpDeductions.TabIndex = 4
        Me.tpDeductions.Text = "Deductions"
        Me.tpDeductions.UseVisualStyleBackColor = True
        '
        'ucDeductions
        '
        Me.ucDeductions.Location = New System.Drawing.Point(6, 6)
        Me.ucDeductions.Name = "ucDeductions"
        Me.ucDeductions.Size = New System.Drawing.Size(500, 400)
        Me.ucDeductions.TabIndex = 0
        '
        'tpCollaterals
        '
        Me.tpCollaterals.Controls.Add(Me.ucCollaterals)
        Me.tpCollaterals.Location = New System.Drawing.Point(4, 22)
        Me.tpCollaterals.Name = "tpCollaterals"
        Me.tpCollaterals.Padding = New System.Windows.Forms.Padding(3)
        Me.tpCollaterals.Size = New System.Drawing.Size(512, 412)
        Me.tpCollaterals.TabIndex = 1
        Me.tpCollaterals.Text = "Collaterals"
        Me.tpCollaterals.UseVisualStyleBackColor = True
        '
        'ucCollaterals
        '
        Me.ucCollaterals.Location = New System.Drawing.Point(6, 6)
        Me.ucCollaterals.Name = "ucCollaterals"
        Me.ucCollaterals.Size = New System.Drawing.Size(500, 400)
        Me.ucCollaterals.TabIndex = 0
        '
        'tpDetails
        '
        Me.tpDetails.Controls.Add(Me.pgDetails)
        Me.tpDetails.Location = New System.Drawing.Point(4, 22)
        Me.tpDetails.Name = "tpDetails"
        Me.tpDetails.Padding = New System.Windows.Forms.Padding(3)
        Me.tpDetails.Size = New System.Drawing.Size(512, 412)
        Me.tpDetails.TabIndex = 0
        Me.tpDetails.Text = "Details"
        Me.tpDetails.UseVisualStyleBackColor = True
        '
        'pgDetails
        '
        Me.pgDetails.HelpVisible = False
        Me.pgDetails.Location = New System.Drawing.Point(6, 6)
        Me.pgDetails.Name = "pgDetails"
        Me.pgDetails.PropertySort = System.Windows.Forms.PropertySort.NoSort
        Me.pgDetails.Size = New System.Drawing.Size(500, 400)
        Me.pgDetails.TabIndex = 0
        Me.pgDetails.ToolbarVisible = False
        '
        'tcMain
        '
        Me.tcMain.Controls.Add(Me.tpDetails)
        Me.tcMain.Controls.Add(Me.tpCollaterals)
        Me.tcMain.Controls.Add(Me.tpDeductions)
        Me.tcMain.Controls.Add(Me.tpCharges)
        Me.tcMain.Controls.Add(Me.tpNetProceeds)
        Me.tcMain.Location = New System.Drawing.Point(12, 91)
        Me.tcMain.Name = "tcMain"
        Me.tcMain.SelectedIndex = 0
        Me.tcMain.Size = New System.Drawing.Size(520, 438)
        Me.tcMain.TabIndex = 0
        '
        'tpNetProceeds
        '
        Me.tpNetProceeds.Controls.Add(Me.dgvNetProceeds)
        Me.tpNetProceeds.Location = New System.Drawing.Point(4, 22)
        Me.tpNetProceeds.Name = "tpNetProceeds"
        Me.tpNetProceeds.Padding = New System.Windows.Forms.Padding(3)
        Me.tpNetProceeds.Size = New System.Drawing.Size(512, 412)
        Me.tpNetProceeds.TabIndex = 5
        Me.tpNetProceeds.Text = "Net Proceeds"
        Me.tpNetProceeds.UseVisualStyleBackColor = True
        '
        'dgvNetProceeds
        '
        Me.dgvNetProceeds.AllowUserToAddRows = False
        Me.dgvNetProceeds.AllowUserToDeleteRows = False
        Me.dgvNetProceeds.AllowUserToResizeRows = False
        Me.dgvNetProceeds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvNetProceeds.Location = New System.Drawing.Point(6, 6)
        Me.dgvNetProceeds.Name = "dgvNetProceeds"
        Me.dgvNetProceeds.RowHeadersVisible = False
        Me.dgvNetProceeds.Size = New System.Drawing.Size(500, 400)
        Me.dgvNetProceeds.TabIndex = 0
        '
        'mnuLoanRelease
        '
        Me.mnuLoanRelease.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem})
        Me.mnuLoanRelease.Location = New System.Drawing.Point(0, 0)
        Me.mnuLoanRelease.Name = "mnuLoanRelease"
        Me.mnuLoanRelease.Size = New System.Drawing.Size(544, 24)
        Me.mnuLoanRelease.TabIndex = 1
        Me.mnuLoanRelease.Text = "MenuStrip1"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Enabled = False
        Me.SaveToolStripMenuItem.Image = Global.Loan.Application.My.Resources.Resources.save
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(59, 20)
        Me.SaveToolStripMenuItem.Text = "&Save"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 34)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(231, 37)
        Me.lblTitle.TabIndex = 10
        Me.lblTitle.Text = "Loan Release"
        '
        'LoanRelease
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 541)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.tcMain)
        Me.Controls.Add(Me.mnuLoanRelease)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.mnuLoanRelease
        Me.Name = "LoanRelease"
        Me.Text = "Loan Release"
        Me.tpCharges.ResumeLayout(False)
        CType(Me.dgvCharges, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tpDeductions.ResumeLayout(False)
        Me.tpCollaterals.ResumeLayout(False)
        Me.tpDetails.ResumeLayout(False)
        Me.tcMain.ResumeLayout(False)
        Me.tpNetProceeds.ResumeLayout(False)
        CType(Me.dgvNetProceeds, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuLoanRelease.ResumeLayout(False)
        Me.mnuLoanRelease.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents tpCharges As System.Windows.Forms.TabPage
    Friend WithEvents tpDeductions As System.Windows.Forms.TabPage
    Friend WithEvents ucDeductions As Loan.Application.SearchAndSelect
    Friend WithEvents tpCollaterals As System.Windows.Forms.TabPage
    Friend WithEvents ucCollaterals As Loan.Application.SearchAndSelect
    Friend WithEvents tpDetails As System.Windows.Forms.TabPage
    Friend WithEvents pgDetails As System.Windows.Forms.PropertyGrid
    Friend WithEvents tcMain As System.Windows.Forms.TabControl
    Friend WithEvents dgvCharges As System.Windows.Forms.DataGridView
    Friend WithEvents mnuLoanRelease As System.Windows.Forms.MenuStrip
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tpNetProceeds As System.Windows.Forms.TabPage
    Friend WithEvents dgvNetProceeds As System.Windows.Forms.DataGridView
    Friend WithEvents lblTitle As System.Windows.Forms.Label
End Class
