Imports Loan.Application.Infrastructure.Enumerations.Popups

Public Class PayrollAdvancePaymentsMaintenance
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
    Friend WithEvents dgvAdvanceList As System.Windows.Forms.DataGridView
    Friend WithEvents dgvMemberList As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents cboColumns As System.Windows.Forms.ComboBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.dgvAdvanceList = New System.Windows.Forms.DataGridView
        Me.dgvMemberList = New System.Windows.Forms.DataGridView
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtSearch = New System.Windows.Forms.TextBox
        Me.cboColumns = New System.Windows.Forms.ComboBox
        Me.lblTitle = New System.Windows.Forms.Label
        CType(Me.dgvAdvanceList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.dgvMemberList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'dgvAdvanceList
        '
        Me.dgvAdvanceList.AllowUserToAddRows = False
        Me.dgvAdvanceList.AllowUserToDeleteRows = False
        Me.dgvAdvanceList.AllowUserToResizeColumns = False
        Me.dgvAdvanceList.AllowUserToResizeRows = False
        Me.dgvAdvanceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAdvanceList.Location = New System.Drawing.Point(227, 17)
        Me.dgvAdvanceList.Name = "dgvAdvanceList"
        Me.dgvAdvanceList.ReadOnly = True
        Me.dgvAdvanceList.RowHeadersVisible = False
        Me.dgvAdvanceList.Size = New System.Drawing.Size(534, 236)
        Me.dgvAdvanceList.TabIndex = 3
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
        Me.dgvMemberList.Size = New System.Drawing.Size(201, 183)
        Me.dgvMemberList.StandardTab = True
        Me.dgvMemberList.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.dgvAdvanceList)
        Me.GroupBox1.Controls.Add(Me.dgvMemberList)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.cboColumns)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(774, 267)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(13, 44)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(201, 20)
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
        Me.cboColumns.Size = New System.Drawing.Size(201, 21)
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
        Me.lblTitle.Size = New System.Drawing.Size(647, 37)
        Me.lblTitle.TabIndex = 99
        Me.lblTitle.Text = "Payroll Advance Payment Maintenance"
        '
        'PayrollAdvancePaymentsMaintenance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(798, 336)
        Me.Controls.Add(Me.lblTitle)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "PayrollAdvancePaymentsMaintenance"
        Me.Text = "Payroll Advance Payment Maintenance"
        CType(Me.dgvAdvanceList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.dgvMemberList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub PayrollAdvancePaymentsMaintenance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dgvc As New DataGridViewColumn
        Dim intX As Integer

        dtMain = Common.GetDetailsOld("PD-1", "")
        dgvMemberList.DataSource = dtMain

        For intX = 0 To dtMain.Columns.Count - 1
            cboColumns.Items.Insert(intX, dtMain.Columns(intX).ColumnName)
            cboColumns.SelectedIndex = 0
        Next

        With dgvMemberList
            For Each dgvc In .Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End With

        With dgvAdvanceList
            For Each dgvc In .Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
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

    Private Sub dgvMemberList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvMemberList.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.Handled = True
            RefreshList()
        End If
    End Sub

    Private Sub dgvAdvanceList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvAdvanceList.KeyDown
        Dim advanceID As Integer
        Dim db As New Data.Database()
        Dim tempInput As String = String.Empty

        If e.KeyCode = Keys.Enter Then
            e.Handled = True

            Try
                Select Case Common.PopupOptions("Delete", "Print", "Edit", "Extract", "Update Acct No", "Exit", "")
                    Case 1
                        If MsgBox("Delete current record?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        advanceID = CInt((CType(dgvAdvanceList.DataSource, DataTable).Rows(dgvAdvanceList.CurrentCell.RowIndex).Item("ADVANCE_ID")))
                        db.AddParameter("@ADVANCE_ID", advanceID)

                        db.AddParameter("@COMMAND", "DELETE")
                        db.AddParameter("@MY_USER", sysuser)
                        db.ExecuteNonQuery("s3p_Payroll_AdvancePayments", CommandType.StoredProcedure)
                        RefreshList()

                    Case 2
                        Dim popup As New Business.Popups.DateRange
                        If Common.OpenPopup(popup) = PopupResponses.Quit Then Exit Sub

                        Dim x1 As Integer

                        Select Case Common.PopupOptions("Advance", "Extract", "Cancel", "")
                            Case 1
                                x1 = 1
                            Case 2
                                x1 = 2
                            Case 3
                                Exit Sub
                        End Select

                        Common.OpenReport(Of Report.Payroll.AdvancePayments)(popup.DateFrom, popup.DateTo, x1)
                    Case 3
                        If MsgBox("Edit current record?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        tempInput = String.Empty
                        tempInput = InputBox("Enter Amount.")

                        If tempInput = String.Empty Then
                            db = Nothing
                            Exit Sub
                        End If

                        If Not IsNumeric(tempInput) Then
                            db = Nothing
                            MsgBox("Amount must be numeric.", MsgBoxStyle.Exclamation)
                            Exit Sub
                        Else
                            db.AddParameter("@AMOUNT", CDbl(tempInput))
                        End If

                        tempInput = String.Empty
                        tempInput = InputBox("Enter Remarks.")

                        If tempInput = String.Empty Then
                            db = Nothing
                            MsgBox("Remarks required", MsgBoxStyle.Exclamation)
                            Exit Sub
                        Else
                            db.AddParameter("@REMARKS", tempInput)
                        End If

                        advanceID = CInt((CType(dgvAdvanceList.DataSource, DataTable).Rows(dgvAdvanceList.CurrentCell.RowIndex).Item("ADVANCE_ID")))
                        db.AddParameter("@ADVANCE_ID", advanceID)

                        db.AddParameter("@COMMAND", "EDIT")
                        db.AddParameter("@MY_USER", sysuser)
                        db.ExecuteNonQuery("s3p_Payroll_AdvancePayments", CommandType.StoredProcedure)
                        RefreshList()

                    Case 4
                        Dim dt As DataTable
                        Dim popup As New Business.Popups.DateRange()
                        If Common.OpenPopup(popup) = PopupResponses.Quit Then Exit Sub

                        dt = New DataTable()
                        db.AddParameter("@DATE1", popup.DateFrom)
                        db.AddParameter("@DATE2", popup.DateTo)
                        db.AddParameter("@COMMAND", "EXTRACT-1")
                        db.AddParameter("@MY_USER", sysuser)
                        dt = db.ExecuteQuery("s3p_Payroll_AdvancePayments", CommandType.StoredProcedure).Tables(0)

                        If MsgBox("Extracted Records: " & vbCrLf & dt.Rows(0).Item("tcnt") & vbCrLf & vbCrLf & "Total Amount: " & vbCrLf & dt.Rows(0).Item("tsum") & vbCrLf & vbCrLf & "Finalize?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        db.AddParameter("@DATE1", popup.DateFrom)
                        db.AddParameter("@DATE2", popup.DateTo)
                        db.AddParameter("@COMMAND", "EXTRACT-2")
                        db.AddParameter("@MY_USER", sysuser)
                        dt = db.ExecuteQuery("s3p_Payroll_AdvancePayments", CommandType.StoredProcedure).Tables(0)

                        MsgBox("Extracted Records: " & vbCrLf & dt.Rows(0).Item("tcnt") & vbCrLf & vbCrLf & "Total Amount: " & vbCrLf & dt.Rows(0).Item("tsum") & vbCrLf & vbCrLf & "Finalized.", MsgBoxStyle.OkOnly)
                        RefreshList()
                    Case 5
                        If MsgBox("Update acct no for the current record?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            Exit Sub
                        End If

                        advanceID = CInt((CType(dgvAdvanceList.DataSource, DataTable).Rows(dgvAdvanceList.CurrentCell.RowIndex).Item("ADVANCE_ID")))
                        db.AddParameter("@ADVANCE_ID", advanceID)

                        db.AddParameter("@COMMAND", "UPDATE")
                        db.AddParameter("@MY_USER", sysuser)
                        db.ExecuteNonQuery("s3p_Payroll_AdvancePayments", CommandType.StoredProcedure)
                        RefreshList()
                End Select
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
            End Try

        End If
    End Sub

    Private Sub RefreshList()
        Dim iIndex As Integer = dgvMemberList.CurrentCell.RowIndex
        If TypeOf dgvMemberList.DataSource Is DataTable Then
            dgvAdvanceList.DataSource = Common.GetDetailsOld("PD-2", dgvMemberList.DataSource.Rows(iIndex).Item("KBCI_NO").ToString)
        ElseIf TypeOf dgvMemberList.DataSource Is DataView Then
            dgvAdvanceList.DataSource = Common.GetDetailsOld("PD-2", CType(dgvMemberList.DataSource, DataView).ToTable.Rows(iIndex).Item("KBCI_NO").ToString)
        End If
    End Sub

End Class
