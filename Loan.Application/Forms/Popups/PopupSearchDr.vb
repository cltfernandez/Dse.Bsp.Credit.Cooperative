Public Class PopupSearchDr

    Private drOut As DataRow
    Private dtMain As New DataTable

    Public Property Record() As DataRow
        Get
            Return drOut
        End Get
        Set(ByVal value As DataRow)
            drOut = value
        End Set
    End Property

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        GetRecord()
    End Sub

    Private Sub PopupSearch_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dgvc As New DataGridViewColumn
        Dim dt As New DataTable
        Dim intX As Integer

        CancelButton = btnOK

        dt = dgvList.DataSource
        dtMain = dt

        For intX = 0 To dt.Columns.Count - 1
            cboColumns.Items.Insert(intX, dt.Columns(intX).ColumnName)
            cboColumns.SelectedIndex = 0
        Next

        For Each dgvc In dgvList.Columns
            dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        dgvList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        txtSearch.Focus()
    End Sub

    Private Sub dgvList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvList.Click
        GetRecord()
    End Sub

    Private Sub GetRecord()
        Dim dt As DataTable

        If TypeOf dgvList.DataSource Is DataView Then
            dt = CType(dgvList.DataSource, DataView).ToTable
        Else
            dt = dgvList.DataSource
        End If

        If dt.Rows.Count > 0 Then
            drOut = dt.Rows(dgvList.CurrentRow.Index)
            Close()
        End If
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Search()
    End Sub

    Private Sub dgvList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvList.KeyDown
        If e.KeyCode = Keys.Enter Then
            GetRecord()
        End If
    End Sub

    Private Sub txtSearch_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Search()
        End If
    End Sub

    Private Sub Search()
        Try
            Dim dv As New DataView(dtMain)

            If TypeOf dtMain.Rows(0).Item(cboColumns.Text) Is String Then
                dv.RowFilter = cboColumns.Text & " like '%" & txtSearch.Text.Trim & "%'"
            ElseIf IsNumeric(dtMain.Rows(0).Item(cboColumns.Text)) Then
                dv.RowFilter = cboColumns.Text & " = " & txtSearch.Text.Trim
            Else
                Exit Sub
            End If

            dgvList.DataSource = dv
            dv = Nothing
            txtSearch.Focus()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        End Try
    End Sub
End Class