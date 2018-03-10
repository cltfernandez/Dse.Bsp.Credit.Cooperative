Public Class LoanApplicationSdedbal

    Private strID As String
    Private ylri As Double = 0

    Public Property yearly_lri() As Double
        Get
            Return ylri
        End Get
        Set(ByVal value As Double)
            ylri = value
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

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAddLoan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLoan.Click
        Dim PN_No As String = Common.InputTextBox("Enter PN_No")
        Dim dt As New DataTable
        Dim dr As DataRow

        If PN_No = String.Empty Then Exit Sub

        dt = dgvList.DataSource

        For Each dr In dt.Rows
            If dr.Item("PN_NO") = PN_No Then
                MsgBox("Loan is already in deduction file.", MsgBoxStyle.Information)
                Exit Sub
            End If
        Next

        dt = New DataTable
        dt = Common.GetAdditionalSDEDBAL(PN_No, strID, sysuser)

        If dt.Rows.Count <= 0 Then
            MsgBox("PN number does not exist or is not outstanding.")
            Exit Sub
        End If

        dr = CType(dgvList.DataSource, DataTable).NewRow

        With dt.Rows(0)
            dr.Item("TAG") = .Item("TAG")
            dr.Item("PN_NO") = .Item("PN_NO")
            dr.Item("TYPE") = .Item("TYPE")
            dr.Item("LOAN_STAT") = .Item("LOAN_STAT")
            dr.Item("GRANTED") = .Item("GRANTED")
            dr.Item("DUE") = .Item("DUE")
            dr.Item("KBCI_NO") = .Item("KBCI_NO")
            dr.Item("LOAN_AMOUNT") = .Item("LOAN_AMOUNT")
            dr.Item("BALANCE") = .Item("BALANCE")
            dr.Item("RENEW") = .Item("RENEW")
            dr.Item("ARREARS") = .Item("ARREARS")
            dr.Item("DEDUCT") = .Item("DEDUCT")
            dr.Item("DEDUCTION") = .Item("DEDUCTION")
            dr.Item("LRI_DUE") = .Item("LRI_DUE")
            dr.Item("SDEDBAL_ID") = .Item("SDEDBAL_ID")
        End With

        dt = dgvList.DataSource
        dt.Rows.Add(dr)

        dt = Nothing
        dr = Nothing
    End Sub

    Private Sub dgvList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList.CellClick
        If e.RowIndex < 0 Then
            Exit Sub
        Else
            dgvList_Clicks(e.RowIndex)
        End If
    End Sub

    Private Sub dgvList_Clicks(ByVal RowIndex As Integer)
        Dim intOption As Integer
        Dim dt As New DataTable
        Dim dtLRIDUE As New DataTable
        Dim dblDeduction As Double

        dt = dgvList.DataSource

        With dt.Rows(RowIndex)
            If CBool(.Item("TAG")) Then
                If Not CBool(.Item("RENEW")) Then
                    .Item("TAG") = False
                    .Item("DEDUCT") = " "
                    .Item("DEDUCTION") = 0
                    ylri -= Common.IsDBNullNum(.Item("YEARLY_LRI"))
                End If
            Else
                intOption = Common.PopupOptions("Full balance", "Arrear", "Other Amount", "LRI Due", "Cancel", "")

                If intOption = 5 Then Exit Sub

                If intOption = 2 And Common.IsDBNullNum(.Item("ARREARS")) <= 0 Then
                    MsgBox("Loan has no arrears.", MsgBoxStyle.Information)
                    intOption = 0
                End If

                If intOption = 4 And Common.IsDBNullNum(.Item("LRI_DUE")) <= 0 Then
                    MsgBox("Loan has no LRI due.", MsgBoxStyle.Information)
                    intOption = 0
                End If

                If intOption = 3 Then
                    dblDeduction = Common.InputAmountBox("Enter Amount.")
                    Do While dblDeduction < 0
                        dblDeduction = Common.InputAmountBox("Amount must be numeric and greater than 0. Please reenter amount.")
                    Loop
                End If

                If intOption <> 0 Then
                    dtLRIDUE = Common.GetDetailsOld("LRIDUE", .Item("PN_NO"))
                    If intOption = 4 Then
                        If dtLRIDUE.Rows.Count > 0 Then
                            .Item("LRI_DUE") = dtLRIDUE.Rows(0).Item("LRI_DUE_P")
                        End If
                    Else
                        If dtLRIDUE.Rows.Count > 0 Then
                            .Item("LRI_DUE") = dtLRIDUE.Rows(0).Item("LRI_DUE_C")
                            ylri += Common.IsDBNullNum(.Item("YEARLY_LRI"))
                        End If
                    End If
                    .Item("TAG") = True

                    Select Case intOption
                        Case 1
                            .Item("DEDUCTION") = .Item("BALANCE")
                            .Item("DEDUCT") = "1"
                        Case 2
                            .Item("DEDUCTION") = .Item("ARREARS")
                            .Item("DEDUCT") = "2"
                        Case 3
                            .Item("DEDUCTION") = dblDeduction
                        Case 4
                            .Item("DEDUCTION") = .Item("LRI_DUE")
                            .Item("DEDUCT") = "4"
                        Case Else
                            .Item("DEDUCTION") = 0
                            .Item("DEDUCT") = "    "
                    End Select
                End If
            End If

            Business.LoanApplication.UpdateSDEDBAL(.Item("SDEDBAL_ID"), .Item("TAG"), .Item("RENEW"), .Item("PN_NO"), .Item("DEDUCT"), .Item("DEDUCTION"), .Item("LRI_DUE"))

            Select Case intOption
                Case 1
                    .Item("DEDUCTION") = .Item("BALANCE")
                    .Item("DEDUCT") = "FULL"
                Case 2
                    .Item("DEDUCTION") = .Item("ARREARS")
                    .Item("DEDUCT") = "ARR"
                Case 4
                    .Item("DEDUCTION") = .Item("LRI_DUE")
                    .Item("DEDUCT") = "LRI"
                Case Else
                    .Item("DEDUCTION") = 0
                    .Item("DEDUCT") = "    "
            End Select

        End With

        dgvList.DataSource = dt
        dgvList.Refresh()

        dt.Dispose()
        dtLRIDUE.Dispose()
    End Sub

    'Private Sub btnAddEntry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEntry.Click
    '    If CType(dgvList.DataSource, DataTable).Rows.Count <= 0 Then
    '        MsgBox("No other loans to process.", MsgBoxStyle.Exclamation)
    '        Exit Sub
    '    End If

    '    Dim frmODEDBAL As New frmLoanNewODEDBAL
    '    Dim pn_no As String = Common.IsDBNull(CType(dgvList.DataSource, DataTable).Rows(dgvList.CurrentRow.Index).Item("PN_NO"))
    '    Dim loan_type As String = Common.IsDBNull(CType(dgvList.DataSource, DataTable).Rows(dgvList.CurrentRow.Index).Item("TYPE"))
    '    frmODEDBAL.ID = strID
    '    frmODEDBAL.LoanType = loan_type
    '    frmODEDBAL.PN_NO = pn_no
    '    frmODEDBAL.ShowDialog()
    '    frmODEDBAL = Nothing
    'End Sub

    Private Sub LoanApplicationSdedbal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dgvc As New DataGridViewColumn

        With dgvList
            For Each dgvc In .Columns
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable
            Next
        End With
    End Sub
End Class