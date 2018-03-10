<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class LriMaintenance
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.LRIMaintenanceMenu = New System.Windows.Forms.MenuStrip
        Me.PaymentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FindToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PaymentOrderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LRIDueToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DailyCollectionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeductionFromDivrefToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DivrefToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HoldLRIInDIVREF2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PostDIVREF2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dgvDetails = New System.Windows.Forms.DataGridView
        Me.pgRow = New System.Windows.Forms.PropertyGrid
        Me.lblTitle = New System.Windows.Forms.Label
        Me.LRIMaintenanceMenu.SuspendLayout()
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LRIMaintenanceMenu
        '
        Me.LRIMaintenanceMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PaymentToolStripMenuItem, Me.FindToolStripMenuItem, Me.ListToolStripMenuItem, Me.PaymentOrderToolStripMenuItem, Me.EditToolStripMenuItem, Me.ReportToolStripMenuItem, Me.DivrefToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.LRIMaintenanceMenu.Location = New System.Drawing.Point(0, 0)
        Me.LRIMaintenanceMenu.Name = "LRIMaintenanceMenu"
        Me.LRIMaintenanceMenu.Size = New System.Drawing.Size(748, 24)
        Me.LRIMaintenanceMenu.TabIndex = 0
        Me.LRIMaintenanceMenu.Text = "MenuStrip1"
        '
        'PaymentToolStripMenuItem
        '
        Me.PaymentToolStripMenuItem.Name = "PaymentToolStripMenuItem"
        Me.PaymentToolStripMenuItem.Size = New System.Drawing.Size(61, 20)
        Me.PaymentToolStripMenuItem.Text = "Pa&yment"
        '
        'FindToolStripMenuItem
        '
        Me.FindToolStripMenuItem.Name = "FindToolStripMenuItem"
        Me.FindToolStripMenuItem.Size = New System.Drawing.Size(39, 20)
        Me.FindToolStripMenuItem.Text = "&Find"
        '
        'ListToolStripMenuItem
        '
        Me.ListToolStripMenuItem.Name = "ListToolStripMenuItem"
        Me.ListToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.ListToolStripMenuItem.Text = "&List"
        '
        'PaymentOrderToolStripMenuItem
        '
        Me.PaymentOrderToolStripMenuItem.Name = "PaymentOrderToolStripMenuItem"
        Me.PaymentOrderToolStripMenuItem.Size = New System.Drawing.Size(92, 20)
        Me.PaymentOrderToolStripMenuItem.Text = "&Payment Order"
        '
        'EditToolStripMenuItem
        '
        Me.EditToolStripMenuItem.Name = "EditToolStripMenuItem"
        Me.EditToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.EditToolStripMenuItem.Text = "&Edit"
        '
        'ReportToolStripMenuItem
        '
        Me.ReportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LRIDueToolStripMenuItem, Me.DailyCollectionToolStripMenuItem, Me.DeductionFromDivrefToolStripMenuItem})
        Me.ReportToolStripMenuItem.Name = "ReportToolStripMenuItem"
        Me.ReportToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.ReportToolStripMenuItem.Text = "&Report"
        '
        'LRIDueToolStripMenuItem
        '
        Me.LRIDueToolStripMenuItem.Name = "LRIDueToolStripMenuItem"
        Me.LRIDueToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.LRIDueToolStripMenuItem.Text = "LRI Due"
        '
        'DailyCollectionToolStripMenuItem
        '
        Me.DailyCollectionToolStripMenuItem.Name = "DailyCollectionToolStripMenuItem"
        Me.DailyCollectionToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.DailyCollectionToolStripMenuItem.Text = "Daily Collection"
        '
        'DeductionFromDivrefToolStripMenuItem
        '
        Me.DeductionFromDivrefToolStripMenuItem.Name = "DeductionFromDivrefToolStripMenuItem"
        Me.DeductionFromDivrefToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
        Me.DeductionFromDivrefToolStripMenuItem.Text = "Deduction from Divref"
        '
        'DivrefToolStripMenuItem
        '
        Me.DivrefToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HoldLRIInDIVREF2ToolStripMenuItem, Me.PostDIVREF2ToolStripMenuItem})
        Me.DivrefToolStripMenuItem.Name = "DivrefToolStripMenuItem"
        Me.DivrefToolStripMenuItem.Size = New System.Drawing.Size(48, 20)
        Me.DivrefToolStripMenuItem.Text = "&Divref"
        '
        'HoldLRIInDIVREF2ToolStripMenuItem
        '
        Me.HoldLRIInDIVREF2ToolStripMenuItem.Name = "HoldLRIInDIVREF2ToolStripMenuItem"
        Me.HoldLRIInDIVREF2ToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.HoldLRIInDIVREF2ToolStripMenuItem.Text = "Hold LRI"
        '
        'PostDIVREF2ToolStripMenuItem
        '
        Me.PostDIVREF2ToolStripMenuItem.Name = "PostDIVREF2ToolStripMenuItem"
        Me.PostDIVREF2ToolStripMenuItem.Size = New System.Drawing.Size(125, 22)
        Me.PostDIVREF2ToolStripMenuItem.Text = "Post"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.ExitToolStripMenuItem.Text = "E&xit"
        '
        'dgvDetails
        '
        Me.dgvDetails.AllowUserToAddRows = False
        Me.dgvDetails.AllowUserToDeleteRows = False
        Me.dgvDetails.AllowUserToResizeRows = False
        Me.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvDetails.DefaultCellStyle = DataGridViewCellStyle1
        Me.dgvDetails.Location = New System.Drawing.Point(12, 73)
        Me.dgvDetails.MultiSelect = False
        Me.dgvDetails.Name = "dgvDetails"
        Me.dgvDetails.ReadOnly = True
        Me.dgvDetails.RowHeadersVisible = False
        Me.dgvDetails.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.dgvDetails.Size = New System.Drawing.Size(456, 292)
        Me.dgvDetails.TabIndex = 1
        '
        'pgRow
        '
        Me.pgRow.Enabled = False
        Me.pgRow.HelpVisible = False
        Me.pgRow.Location = New System.Drawing.Point(474, 73)
        Me.pgRow.Name = "pgRow"
        Me.pgRow.PropertySort = System.Windows.Forms.PropertySort.NoSort
        Me.pgRow.Size = New System.Drawing.Size(262, 292)
        Me.pgRow.TabIndex = 2
        Me.pgRow.ToolbarVisible = False
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 33)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(290, 37)
        Me.lblTitle.TabIndex = 99
        Me.lblTitle.Text = "LRI Maintenance"
        '
        'LriMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(748, 379)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.pgRow)
        Me.Controls.Add(Me.dgvDetails)
        Me.Controls.Add(Me.LRIMaintenanceMenu)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.LRIMaintenanceMenu
        Me.Name = "LriMaintenance"
        Me.Text = "LRI Maintenance"
        Me.LRIMaintenanceMenu.ResumeLayout(False)
        Me.LRIMaintenanceMenu.PerformLayout()
        CType(Me.dgvDetails, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LRIMaintenanceMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents PaymentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FindToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PaymentOrderToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DivrefToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HoldLRIInDIVREF2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PostDIVREF2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dgvDetails As System.Windows.Forms.DataGridView
    Friend WithEvents pgRow As System.Windows.Forms.PropertyGrid
    Friend WithEvents LRIDueToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DailyCollectionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeductionFromDivrefToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblTitle As System.Windows.Forms.Label
End Class
