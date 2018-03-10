<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SqlTableEditor
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
        Me.pgRow = New System.Windows.Forms.PropertyGrid
        Me.dgvTable = New System.Windows.Forms.DataGridView
        Me.lblTitle = New System.Windows.Forms.Label
        Me.grpDataGrid = New System.Windows.Forms.GroupBox
        Me.grpProperty = New System.Windows.Forms.GroupBox
        Me.mnuSub = New System.Windows.Forms.MenuStrip
        Me.mnuSave = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.dgvTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDataGrid.SuspendLayout()
        Me.grpProperty.SuspendLayout()
        Me.mnuSub.SuspendLayout()
        Me.SuspendLayout()
        '
        'pgRow
        '
        Me.pgRow.HelpVisible = False
        Me.pgRow.Location = New System.Drawing.Point(6, 19)
        Me.pgRow.Name = "pgRow"
        Me.pgRow.PropertySort = System.Windows.Forms.PropertySort.Categorized
        Me.pgRow.Size = New System.Drawing.Size(222, 259)
        Me.pgRow.TabIndex = 0
        Me.pgRow.ToolbarVisible = False
        '
        'dgvTable
        '
        Me.dgvTable.AllowUserToAddRows = False
        Me.dgvTable.AllowUserToDeleteRows = False
        Me.dgvTable.AllowUserToResizeRows = False
        Me.dgvTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvTable.Location = New System.Drawing.Point(6, 19)
        Me.dgvTable.MultiSelect = False
        Me.dgvTable.Name = "dgvTable"
        Me.dgvTable.ReadOnly = True
        Me.dgvTable.RowHeadersVisible = False
        Me.dgvTable.Size = New System.Drawing.Size(472, 259)
        Me.dgvTable.TabIndex = 1
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 37)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(176, 30)
        Me.lblTitle.TabIndex = 99
        Me.lblTitle.Text = "Maintenance"
        '
        'grpDataGrid
        '
        Me.grpDataGrid.Controls.Add(Me.dgvTable)
        Me.grpDataGrid.Location = New System.Drawing.Point(12, 89)
        Me.grpDataGrid.Name = "grpDataGrid"
        Me.grpDataGrid.Size = New System.Drawing.Size(484, 284)
        Me.grpDataGrid.TabIndex = 100
        Me.grpDataGrid.TabStop = False
        '
        'grpProperty
        '
        Me.grpProperty.Controls.Add(Me.pgRow)
        Me.grpProperty.Location = New System.Drawing.Point(502, 89)
        Me.grpProperty.Name = "grpProperty"
        Me.grpProperty.Size = New System.Drawing.Size(234, 284)
        Me.grpProperty.TabIndex = 101
        Me.grpProperty.TabStop = False
        '
        'mnuSub
        '
        Me.mnuSub.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSave})
        Me.mnuSub.Location = New System.Drawing.Point(0, 0)
        Me.mnuSub.Name = "mnuSub"
        Me.mnuSub.Size = New System.Drawing.Size(748, 24)
        Me.mnuSub.TabIndex = 102
        Me.mnuSub.Text = "MenuStrip1"
        '
        'mnuSave
        '
        Me.mnuSave.Image = Global.Loan.Application.My.Resources.Resources.save
        Me.mnuSave.Name = "mnuSave"
        Me.mnuSave.Size = New System.Drawing.Size(59, 20)
        Me.mnuSave.Text = "&Save"
        '
        'SqlTableEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(748, 385)
        Me.Controls.Add(Me.mnuSub)
        Me.Controls.Add(Me.grpProperty)
        Me.Controls.Add(Me.grpDataGrid)
        Me.Controls.Add(Me.lblTitle)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "SqlTableEditor"
        Me.Text = "SqlTableEditor"
        CType(Me.dgvTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDataGrid.ResumeLayout(False)
        Me.grpProperty.ResumeLayout(False)
        Me.mnuSub.ResumeLayout(False)
        Me.mnuSub.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pgRow As System.Windows.Forms.PropertyGrid
    Friend WithEvents dgvTable As System.Windows.Forms.DataGridView
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents grpDataGrid As System.Windows.Forms.GroupBox
    Friend WithEvents grpProperty As System.Windows.Forms.GroupBox
    Friend WithEvents mnuSub As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuSave As System.Windows.Forms.ToolStripMenuItem
End Class
