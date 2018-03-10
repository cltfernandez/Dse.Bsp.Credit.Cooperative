Imports System.Windows
Imports System.Data.SqlClient

Public Class SqlTableEditor

    Dim bindingSource As BindingSource
    Dim dataTable As DataTable
    Dim dataAdapter As SqlDataAdapter

    Public Sub New(ByVal caption As String, ByVal selectCommand As String)
        InitializeComponent()
        Me.Text = caption
        lblTitle.Text = GetTitle(caption)
        SetDataGrid(selectCommand)
        SetPropertyGrid()
        dgvTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    End Sub

    Private Function GetTitle(ByVal caption As String) As String
        Dim text As String()
        text = caption.Split("-")
        If text.Count = 2 Then
            Return text(1)
        Else
            Return text(2) & ":" & text(3) & " (" & text(4) & ")"
        End If
    End Function

    Private Sub SetDataGrid(ByVal selectCommand As String)
        Dim db As New Data.Database()
        Dim commandBuilder As SqlCommandBuilder

        dataAdapter = New SqlDataAdapter(selectCommand, db.GetConnectionString)
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

    Private Sub dgvTable_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgvTable.SelectionChanged
        SetPropertyGrid()
    End Sub

    Private Sub pgRow_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles pgRow.PropertyValueChanged
        Dim updateLri As Boolean = False
        Dim updateBalance As Boolean = False
        Dim dr As DataRow = dataTable.Rows(dgvTable.CurrentRow.Index)
        Dim table As String = GetTable()

        dr.Item(e.ChangedItem.Label) = e.ChangedItem.Value
        bindingSource.EndEdit()
        dataAdapter.Update(dataTable)
        dgvTable.RefreshEdit()

        Select Case table
            Case "Lridue"
                If e.ChangedItem.Label = "LRI_DUE_C" Then updateLri = True
            Case "Loans"
                If e.ChangedItem.Label = "LRI_DUE" Then updateLri = True
            Case "Ledger"
                updateBalance = True
        End Select

        If updateLri Then
            Business.Maintenance.UpdateLriDue(dr.Item("PN_NO"), e.ChangedItem.Value, sysuser)
        End If

        If updateBalance Then
            Business.Maintenance.UpdateRunningBalance(dr.Item("PN_NO"))
        End If
    End Sub

    Private Function GetTable() As String
        Dim commandText As String = dataAdapter.UpdateCommand.CommandText
        commandText = commandText.Substring(commandText.IndexOf("[") + 1, commandText.IndexOf("]") - commandText.IndexOf("[") - 1)
        Return commandText
    End Function

End Class