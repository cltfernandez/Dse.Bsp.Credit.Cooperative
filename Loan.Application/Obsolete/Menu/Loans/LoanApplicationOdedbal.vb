Public Class LoanApplicationOdedbal

    Private strKBCI As String
    Private strID As String
    Private strPN_NO As String = String.Empty
    Private strLoanType As String
    Private dtODEDBAL As New DataTable

    Public Property KBCI_NO() As String
        Get
            Return strKBCI
        End Get
        Set(ByVal value As String)
            strKBCI = value
        End Set
    End Property

    Public Property LoanType() As String
        Get
            Return strLoanType
        End Get
        Set(ByVal value As String)
            strLoanType = value
        End Set
    End Property

    Public Property ID() As String
        Get
            Return strID
        End Get
        Set(ByVal value As String)
            strID = value
        End Set
    End Property

    Public Property PN_NO() As String
        Get
            Return strPN_NO
        End Get
        Set(ByVal value As String)
            strPN_NO = value
        End Set
    End Property

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim form As New LoanApplicationOdedbalDetail

        If strPN_NO <> String.Empty Then
            form.KBCI_NO = strKBCI
            form.txtPNNo.Text = strPN_NO
            form.txtPNNo.ReadOnly = True
            form.txtPNNo.TabStop = False
        End If

        form.ID = strID
        form.LoanType = strLoanType
        form.ShowDialog()
        form = Nothing
        dgvList.DataSource = Common.GetDetailsOld("ODEDBAL", strID)
        dgvList.Refresh()
    End Sub

    Private Sub LoanApplicationOdedbal_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AcceptButton = btnClose
        CancelButton = btnClose

        Dim dgvc As New DataGridViewColumn

        With dgvList
            For Each dgvc In .Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End With

        dgvList.DataSource = Common.GetDetailsOld("ODEDBAL", strID)
        dgvList.Refresh()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Close()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If dgvList.DataSource Is Nothing OrElse CType(dgvList.DataSource, DataTable).Rows.Count <= 0 Then
            MsgBox("No entry to delete.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Business.LoanApplication.UpdateODEDBAL("DEL", CType(dgvList.DataSource, DataTable).Rows(dgvList.CurrentRow.Index).Item("ODEDBAL_ID"))
        dgvList.DataSource = Common.GetDetailsOld("ODEDBAL", strID)
        dgvList.Refresh()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If dgvList.DataSource Is Nothing OrElse CType(dgvList.DataSource, DataTable).Rows.Count <= 0 Then
            MsgBox("No entry to edit.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim form As New LoanApplicationOdedbalDetail

        With CType(dgvList.DataSource, DataTable).Rows(dgvList.CurrentRow.Index)
            form.txtPNNo.Text = .Item("OPN_NO")
            form.txtDR.Text = .Item("ODR")
            form.txtCR.Text = .Item("OCR")
            form.cboRMK.SelectedValue = .Item("OACCT_TYPE")
            form.cboRMK.SelectedText = .Item("ODOX_TYPE")
        End With

        form.LoanType = strLoanType
        form.ShowDialog()
        form = Nothing
        dgvList.DataSource = Common.GetDetailsOld("ODEDBAL", strID)
        dgvList.Refresh()
    End Sub
End Class