<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PropertyDataGrid
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.grpChargesList = New System.Windows.Forms.GroupBox
        Me.dgvList = New System.Windows.Forms.DataGridView
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.grpInput = New System.Windows.Forms.GroupBox
        Me.pgInput = New System.Windows.Forms.PropertyGrid
        Me.grpChargesList.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpInput.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpChargesList
        '
        Me.grpChargesList.Controls.Add(Me.dgvList)
        Me.grpChargesList.Location = New System.Drawing.Point(258, 0)
        Me.grpChargesList.Name = "grpChargesList"
        Me.grpChargesList.Size = New System.Drawing.Size(242, 400)
        Me.grpChargesList.TabIndex = 11
        Me.grpChargesList.TabStop = False
        '
        'dgvList
        '
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvList.Location = New System.Drawing.Point(6, 19)
        Me.dgvList.Name = "dgvList"
        Me.dgvList.Size = New System.Drawing.Size(230, 375)
        Me.dgvList.TabIndex = 0
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(229, 177)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(23, 23)
        Me.btnRemove.TabIndex = 10
        Me.btnRemove.Text = "<"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(229, 148)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(23, 23)
        Me.btnAdd.TabIndex = 9
        Me.btnAdd.Text = ">"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'grpInput
        '
        Me.grpInput.Controls.Add(Me.pgInput)
        Me.grpInput.Location = New System.Drawing.Point(0, 0)
        Me.grpInput.Name = "grpInput"
        Me.grpInput.Size = New System.Drawing.Size(223, 400)
        Me.grpInput.TabIndex = 8
        Me.grpInput.TabStop = False
        '
        'pgInput
        '
        Me.pgInput.HelpVisible = False
        Me.pgInput.Location = New System.Drawing.Point(6, 19)
        Me.pgInput.Name = "pgInput"
        Me.pgInput.PropertySort = System.Windows.Forms.PropertySort.NoSort
        Me.pgInput.Size = New System.Drawing.Size(211, 375)
        Me.pgInput.TabIndex = 0
        Me.pgInput.ToolbarVisible = False
        '
        'PropertyPicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.grpChargesList)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.grpInput)
        Me.Name = "PropertyPicker"
        Me.Size = New System.Drawing.Size(500, 400)
        Me.grpChargesList.ResumeLayout(False)
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpInput.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpChargesList As System.Windows.Forms.GroupBox
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents grpInput As System.Windows.Forms.GroupBox
    Friend WithEvents pgInput As System.Windows.Forms.PropertyGrid

End Class
