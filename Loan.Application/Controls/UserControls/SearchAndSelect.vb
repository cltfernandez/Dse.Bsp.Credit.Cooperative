Public Class SearchAndSelect

    Private Sub dgvSearch_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvSearch.DataBindingComplete
        Common.SetDataGridView(dgvSearch)
    End Sub

    Private Sub dgvSelect_DataBindingComplete(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs) Handles dgvSelect.DataBindingComplete
        Common.SetDataGridView(dgvSelect)
    End Sub

End Class
