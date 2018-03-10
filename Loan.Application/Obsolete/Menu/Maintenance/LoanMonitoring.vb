Imports Loan.Application.Infrastructure.Enumerations.Popups

Public Class LoanMonitoring
    Inherits Loan.Application.Infrastructure.Forms.Windows.BaseForm
    Private dtMain As New DataTable

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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents dgvMemberList As System.Windows.Forms.DataGridView
    Friend WithEvents dgvLoanList As System.Windows.Forms.DataGridView
    Friend WithEvents lblTitle As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.dgvLoanList = New System.Windows.Forms.DataGridView
        Me.dgvMemberList = New System.Windows.Forms.DataGridView
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cboColumns = New System.Windows.Forms.ComboBox
        Me.lblTitle = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        CType(Me.dgvLoanList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvMemberList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgvLoanList)
        Me.GroupBox1.Controls.Add(Me.dgvMemberList)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.cboColumns)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(774, 267)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'dgvLoanList
        '
        Me.dgvLoanList.AllowUserToAddRows = False
        Me.dgvLoanList.AllowUserToDeleteRows = False
        Me.dgvLoanList.AllowUserToResizeColumns = False
        Me.dgvLoanList.AllowUserToResizeRows = False
        Me.dgvLoanList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvLoanList.Location = New System.Drawing.Point(307, 17)
        Me.dgvLoanList.Name = "dgvLoanList"
        Me.dgvLoanList.ReadOnly = True
        Me.dgvLoanList.RowHeadersVisible = False
        Me.dgvLoanList.Size = New System.Drawing.Size(454, 236)
        Me.dgvLoanList.TabIndex = 3
        '
        'dgvMemberList
        '
        Me.dgvMemberList.AllowUserToAddRows = False
        Me.dgvMemberList.AllowUserToDeleteRows = False
        Me.dgvMemberList.AllowUserToResizeRows = False
        Me.dgvMemberList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.dgvMemberList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvMemberList.Location = New System.Drawing.Point(13, 70)
        Me.dgvMemberList.Name = "dgvMemberList"
        Me.dgvMemberList.ReadOnly = True
        Me.dgvMemberList.RowHeadersVisible = False
        Me.dgvMemberList.Size = New System.Drawing.Size(288, 183)
        Me.dgvMemberList.StandardTab = True
        Me.dgvMemberList.TabIndex = 2
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(13, 44)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(288, 20)
        Me.txtSearch.TabIndex = 1
        '
        'cboColumns
        '
        Me.cboColumns.AllowDrop = True
        Me.cboColumns.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboColumns.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboColumns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboColumns.FormattingEnabled = True
        Me.cboColumns.Location = New System.Drawing.Point(13, 17)
        Me.cboColumns.Name = "cboColumns"
        Me.cboColumns.Size = New System.Drawing.Size(288, 21)
        Me.cboColumns.Sorted = True
        Me.cboColumns.TabIndex = 4
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.BackColor = System.Drawing.SystemColors.Control
        Me.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTitle.Font = New System.Drawing.Font("Verdana", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.Location = New System.Drawing.Point(12, 9)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(423, 37)
        Me.lblTitle.TabIndex = 100
        Me.lblTitle.Text = "Loan Account Monitoring"
        '
        'LoanMonitoring
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(798, 336)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "LoanMonitoring"
        Me.Text = "Loan Account Monitoring"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.dgvLoanList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvMemberList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub frmLoanAccountMonitoring_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dgvc As New DataGridViewColumn
        Dim intX As Integer

        dtMain = Common.GetDetailsOld("ML-1", "")
        dgvMemberList.DataSource = dtMain

        For intX = 0 To dtMain.Columns.Count - 1
            cboColumns.Items.Insert(intX, dtMain.Columns(intX).ColumnName)
            cboColumns.SelectedIndex = 0
        Next

        With dgvMemberList
            For Each dgvc In .Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        End With

        With dgvLoanList
            For Each dgvc In .Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next

            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        End With

        cboColumns.SelectedIndex = 1
        txtSearch.Focus()
    End Sub

    Private Sub txtSearch_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSearch.KeyPress
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Return) Then
            Dim dv As New DataView(CType(dtMain, DataTable))
            dv.RowFilter = cboColumns.Text & " like '%" & txtSearch.Text.Trim & "%'"
            dgvMemberList.DataSource = dv
            dv = Nothing
        End If
    End Sub

    Private Sub OpenMenu(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMemberList.KeyDown, dgvLoanList.KeyDown
        Dim popup As New Business.Popups.InputDate()
        Dim dt As New DataTable()
        Dim viewReport As Boolean = True
        Dim updateme As Boolean = False
        Dim reprintme As Boolean = False
        Dim xgo As Boolean = False
        Dim c_upd As Boolean = False
        Dim s_date As DateTime = Date.MinValue
        Dim s_rema As String = String.Empty

        If e.KeyCode = Keys.Enter Then
            e.Handled = True

            Dim iIndex As Integer = dgvMemberList.CurrentCell.RowIndex
            Dim KBCI_NO As String = String.Empty
            If TypeOf dgvMemberList.DataSource Is DataTable Then
                KBCI_NO = dgvMemberList.DataSource.Rows(iIndex).Item("KBCI_NO").ToString
                dgvLoanList.DataSource = Common.GetDetailsOld("ML-2", KBCI_NO)
            ElseIf TypeOf dgvMemberList.DataSource Is DataView Then
                KBCI_NO = CType(dgvMemberList.DataSource, DataView).ToTable.Rows(iIndex).Item("KBCI_NO").ToString
                dgvLoanList.DataSource = Common.GetDetailsOld("ML-2", KBCI_NO)
            End If

            Select Case Common.PopupOptions("Processing Sheet", "Outstanding Balance", "Outstanding As Of", "Statement", "Cancel", "")
                Case 1
                    Common.OpenReport(Of Loan.Application.Report.Maintenance.ProcessingSheet)(KBCI_NO, sysuser, New Date(2999, 1, 1))
                Case 2
                    Common.OpenReport(Of Loan.Application.Report.Maintenance.OutstandingBalance)(KBCI_NO, sysuser, sysdate)
                Case 3
                    If Common.OpenPopup(popup) = PopupResponses.Quit Then Exit Sub
                    Common.OpenReport(Of Loan.Application.Report.Maintenance.OutstandingBalanceAsOf)(KBCI_NO, sysuser, popup.Date)
                Case 4

                    If Common.GetDetailsOld("ML-3", KBCI_NO).Rows.Count > 0 Then
                        Select Case Common.PopupOptions("Update", "Reprint", "Cancel", "")
                            Case 1
                                updateme = True
                                'todo: confirmation
                                xgo = True
                            Case 2
                                reprintme = True
                            Case 3
                                viewReport = False
                        End Select
                    Else
                        xgo = True
                    End If

                    If xgo Or updateme Then
                        If xgo Then
                            If Common.OpenPopup(popup, "Separation Date") = PopupResponses.Quit Then Exit Sub
                            s_date = popup.Date

                            Select Case Common.PopupOptions("RESIGNED", "RETIRED", "DECEASED", "")
                                Case 1
                                    s_rema = "RESIGNED"
                                Case 2
                                    s_rema = "RETIRED"
                                Case 3
                                    s_rema = "DECEASED"
                            End Select

                            If MessageBox.Show("Continue?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No Then Exit Sub
                        End If

                        Dim db As New Data.Database()
                        Try
                            db.AddParameter("@my_user", sysuser)
                            db.AddParameter("@kbci_no", KBCI_NO)
                            db.AddParameter("@s_rema", s_rema)
                            db.AddParameter("@s_date", s_date)
                            db.AddParameter("@myldate", sysdate)
                            db.AddParameter("@updateme", updateme)
                            db.AddParameter("@reprintme", reprintme)
                            db.AddParameter("@xgo", xgo)
                            db.AddParameter("@c_upd", c_upd)
                            db.ExecuteNonQuery("Update_LoanStatement", CommandType.StoredProcedure)
                        Catch ex As Exception
                            db.RollbackTransaction()
                            MsgBox("Error processing statement! Transaction cancelled.", MsgBoxStyle.Critical, "Error")
                            viewReport = False
                        End Try
                        db = Nothing
                    End If

                    If viewReport Then Common.OpenReport(Of Loan.Application.Report.Maintenance.LoansStatement)(KBCI_NO, sysuser)
                Case 5
                    Exit Sub
            End Select
        End If
    End Sub

End Class
