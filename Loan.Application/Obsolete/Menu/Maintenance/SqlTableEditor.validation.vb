Imports System.Windows
Imports System.Data.SqlClient

Public Class SqlTableEditor

    Dim bindingSource As BindingSource
    Dim dataTable As DataTable
    Dim dataAdapter As SqlDataAdapter
    Dim commandText As String

    Public Sub New(ByVal caption As String, ByVal selectCommand As String)
        InitializeComponent()
        Me.Text = caption
        lblTitle.Text = GetTitle(caption)
        commandText = selectCommand
        dgvTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells

        SetDataGrid()
        SetPropertyGrid()
    End Sub

    Private Function GetTitle(ByVal caption As String) As String
        Dim text As String()
        text = caption.Replace("_", " ").Split("-")
        If text.Count = 2 Then
            Return text(1)
        Else
            Return text(2) & ":" & text(3) & " [ " & text(4).Trim & " ]"
        End If
    End Function

    Private Sub SetDataGrid()
        Dim db As New Data.Database()
        Dim commandBuilder As SqlCommandBuilder

        dataAdapter = New SqlDataAdapter(commandText, Data.Database.GetConnectionString)
        commandBuilder = New SqlCommandBuilder(dataAdapter)
        dataTable = New DataTable
        bindingSource = New BindingSource

        dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand
        dataAdapter.Fill(dataTable)
        bindingSource.DataSource = dataTable
        dgvTable.DataSource = bindingSource
        dgvTable.Select()

        'SetCaption(dataTable)
        db = Nothing
    End Sub

    Private Sub SetCaption(ByVal dt As DataTable)
        Dim dtMember As DataTable
        Dim dr As DataRow
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 And dt.Rows(0).Item("KBCI_NO") IsNot Nothing Then
            dtMember = Common.GetDetailsOld("Member", dt.Rows(0).Item("KBCI_NO"))
            If dtMember IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                dr = dtMember.Rows(0)
                Me.Text = String.Format("{0} - {1}, {2} {3}.", Me.Text, dr.Item("LNAME"), dr.Item("FNAME"), dr.Item("MI"))
            End If
        End If
    End Sub

    Private Sub SetPropertyGrid()
        Dim dt As DataTable = bindingSource.DataSource

        If dt IsNot Nothing AndAlso dgvTable.CurrentRow IsNot Nothing Then
            Dim wrapper As New RowWrapper(dt.Rows(dgvTable.CurrentRow.Index))
            wrapper.Exclude.Add(GetExcluded)
            pgRow.SelectedObject = wrapper
        End If
    End Sub

    Private Function GetExcluded() As String
        Dim table As String = GetTable()

        If table = "Members" Then
            Return "KBCI_ID"
        Else
            Return String.Format("{0}_ID", table.ToUpper)
        End If
    End Function

    Private Sub dgvTable_RowValidating(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles dgvTable.RowValidating
        Dim dr As DataRow = dataTable.Rows(dgvTable.CurrentRow.Index)
        If dr.RowState = DataRowState.Modified Then
            If Common.PopupQuestion("Record has been modified. Discard changes?") = Forms.DialogResult.Yes Then
                dr.RejectChanges()
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub dgvTable_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTable.SelectionChanged
        SetPropertyGrid()
    End Sub

    Private Sub pgRow_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgRow.PropertyValueChanged
        Dim dr As DataRow = dataTable.Rows(dgvTable.CurrentRow.Index)
        dr.Item(e.ChangedItem.Label) = e.ChangedItem.Value
    End Sub

    Private Function GetTable() As String
        Dim commandText As String = dataAdapter.UpdateCommand.CommandText
        commandText = commandText.Substring(commandText.IndexOf("[") + 1, commandText.IndexOf("]") - commandText.IndexOf("[") - 1)
        Return commandText
    End Function

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        Dim updateLri As Boolean = False
        Dim updateBalance As Boolean = False
        Dim dr As DataRow = dataTable.Rows(dgvTable.CurrentRow.Index)
        Dim table As String = GetTable()
        Dim lriColumn As String = String.Empty

        dr.AcceptChanges()
        bindingSource.EndEdit()
        dataAdapter.Update(dataTable)
        dgvTable.Refresh()

        Select Case table
            Case "Lridue"
                lriColumn = "LRI_DUE_C"
                updateLri = True
            Case "Loans"
                lriColumn = "LRI_DUE"
                updateLri = True
            Case "Ledger"
                updateBalance = True
        End Select

        If updateLri Then
            Business.Maintenance.UpdateLriDue(dr.Item("PN_NO"), dr.Item(lriColumn), sysuser)
        End If

        If updateBalance Then
            Business.Maintenance.UpdateRunningBalance(dr.Item("PN_NO"))
        End If

        Common.PopupInformation("Record saved.")
    End Sub

    Private Sub SqlTableEditor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim dr As DataRow = dataTable.Rows(dgvTable.CurrentRow.Index)
        If dr.RowState = DataRowState.Modified Then
            e.Cancel = Common.PopupQuestion("Record has been modified. Discard changes and exit?") = Forms.DialogResult.No
        End If
    End Sub

End Class