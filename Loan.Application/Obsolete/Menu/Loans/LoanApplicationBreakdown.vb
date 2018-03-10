Public Class LoanApplicationBreakdown

    Private dt As New DataTable

    Public Property dtList() As DataTable
        Get
            Return dt
        End Get
        Set(ByVal value As DataTable)
            dt = value
        End Set
    End Property

    Private Sub LoanApplicationBreakdown_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lvwSub As New ListViewItem
        Dim dr As DataRow
        Dim intX As Integer = 0
        lvwBreakdown.Columns.Add("Details")
        lvwBreakdown.Columns.Add("Amount")

        lvwBreakdown.Columns(0).TextAlign = HorizontalAlignment.Left
        lvwBreakdown.Columns(0).Width = 120
        lvwBreakdown.Columns(1).TextAlign = HorizontalAlignment.Right
        lvwBreakdown.Columns(1).Width = 100

        For Each dr In dt.Rows
            If intX = dt.Rows.Count - 1 Then
                lvwSub = lvwBreakdown.Items.Add(" ")
                lvwSub.SubItems.Add(" ")
            End If

            lvwSub = lvwBreakdown.Items.Add(dr.Item("Details"))
            lvwSub.SubItems.Add(Format(Common.IsDBNullNum(dr.Item("Amount")), "#,##0.00"))
            intX += 1
        Next

        AcceptButton = btnOK
        CancelButton = btnOK
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Close()
    End Sub
End Class