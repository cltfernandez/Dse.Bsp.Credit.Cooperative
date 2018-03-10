Imports System.Data.SqlClient
Imports Loan.Application.Infrastructure.Enumerations.Popups

Public Class LriMaintenance

    Dim pn_no As String = String.Empty
    Dim loan_status As String = String.Empty

    Private Sub LriMaintenance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub HoldLRIInDIVREF2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HoldLRIInDIVREF2ToolStripMenuItem.Click
        Dim popup As New Business.Popups.DateRange
        If Common.OpenPopup(popup) = PopupResponses.Quit Then Exit Sub

        If Business.LoanReleaseInsurance.IsLriHeld(popup.DateFrom, popup.DateTo) Then
            MessageBox.Show("LRI has been successfully held.", "LRI Hold", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("LRI hold aborted.", "LRI Hold", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub PaymentToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentToolStripMenuItem.Click
        Dim popup As New Business.Popups.InputAmount()
        Dim dr As DataRow
        Dim dt As DataTable

        Dim xpayment As Double
        Dim xtfull As Double

        Dim xp As Integer

        Dim kbci_no As String = String.Empty
        Dim xor_no As String = String.Empty
        Dim xsa_no As String = String.Empty
        Dim xpdc_no As String = String.Empty
        Dim xpdc_bnk As String = String.Empty

        Dim process As Boolean = False
        Dim xfulpay As Boolean = False

        If pn_no = String.Empty Then FindLoan()

        If loan_status = "Released" Then
            dt = Common.GetDetailsOld("OD-3", pn_no)

            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
            Else
                MessageBox.Show("Error getting loan details.", "LRI Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            kbci_no = dr.Item("KBCI_NO")
            xtfull = dr.Item("LRI_DUE_P")
            'xpayment = Common.InputAmountBox("Enter Amount: ", xtfull)
            popup.Amount = xtfull
            If Common.OpenPopup(popup) <> PopupResponses.Ok Then Return
            xpayment = popup.Amount

            If xpayment > 0 Then
                Select Case True
                    Case xpayment > xtfull
                        MessageBox.Show("No overpayment allowed.", "LRI Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    Case xpayment = xtfull
                        MessageBox.Show("LRI will be paid in full.", "LRI Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        xfulpay = True
                End Select

                xp = Common.PopupOptions("OTC", "DM", "PDC", "Cancel")

                Select Case xp
                    Case 1
                        xor_no = Common.InputTextBox("Enter OR No.:")

                        If xor_no.Trim().Length = 0 OrElse Common.GetDetailsOld("OD-4", xor_no).Rows.Count > 1 Then
                            MessageBox.Show("OR number was already posted.", "LRI Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            process = True
                        End If

                    Case 2
                        xsa_no = Common.IsDBNull(dr.Item("FEBTC_SA"))
                        xsa_no = Common.GetSavingsAccount(xsa_no, xpayment).ACCTNO
                        If xsa_no.Trim().Length > 0 Then process = True

                    Case 3
                        xpdc_no = Common.InputTextBox("Check Number:")
                        xpdc_bnk = Common.InputTextBox("Bank Name:")
                        process = True

                    Case 4
                        Exit Sub

                End Select

            Else
                MessageBox.Show("Invalid amount.", "LRI Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Loan is no longer outstanding.", "LRI Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

        If process AndAlso MessageBox.Show("Process Payment?", "LRI Payment", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            If Business.LoanReleaseInsurance.IsLRIPaid(xp, pn_no, kbci_no, xpayment, sysuser, xfulpay, xor_no, xsa_no, xpdc_no, xpdc_bnk) Then
                MessageBox.Show("Payment successfully processed.", "LRI Payment", MessageBoxButtons.OK, MessageBoxIcon.Information)

                If xp <> 1 Then
                    Common.OpenReport(Of Report.Voucher.LoansPayment)(pn_no, xor_no, sysuser, xp)
                End If

                pn_no = String.Empty
                loan_status = String.Empty
                dgvDetails.DataSource = Nothing
                pgRow.SelectedObject = Nothing
            Else
                MessageBox.Show("Payment aborted.", "LRI Payment", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub FindToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FindToolStripMenuItem.Click
        FindLoan()
    End Sub

    Private Sub EditToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditToolStripMenuItem.Click
        Common.ShowTableEditor(Common.MaintainanceTable.Lridue, Common.MaintenanceFilter.PN_NO, pn_no)
    End Sub

    Private Sub ListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListToolStripMenuItem.Click
        Dim dt As New DataTable
        dt = Common.GetDetailsOld("OD-1", "%")

        If dt.Rows.Count > 0 Then
            dgvDetails.DataSource = dt
        Else
            dgvDetails.DataSource = Nothing
        End If
    End Sub

    Private Sub dgvDetails_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles dgvDetails.KeyDown
        If e.KeyCode = Keys.Enter Then
            SetPropertyGrid()
            e.Handled = True
        End If
    End Sub

    Private Sub PaymentOrderToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PaymentOrderToolStripMenuItem.Click
        If pn_no = String.Empty Then FindLoan()
        If pn_no <> String.Empty Then Common.OpenReport(Of Report.PaymentOrder.Lri)(pn_no) 'Common.OpenReportOld("s3p_RPT_PaymentOrder_LriMaintenance", pn_no)
    End Sub

    Private Sub ReportToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReportToolStripMenuItem.Click

    End Sub

    Private Sub PostDIVREF2ToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PostDIVREF2ToolStripMenuItem.Click
        If Business.LoanReleaseInsurance.IsLriPosted(sysuser) Then
            MessageBox.Show("LRI has been successfully posted.", "LRI Hold", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            MessageBox.Show("LRI posting aborted.", "LRI Hold", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub SetPropertyGrid()
        Dim dt As DataTable = dgvDetails.DataSource
        Dim dr As DataRow
        Dim wrapper As RowWrapper

        If dt IsNot Nothing AndAlso dgvDetails.CurrentRow IsNot Nothing Then
            dr = dt.Rows(dgvDetails.CurrentRow.Index)
            pn_no = dr.Item("PN_NO").ToString().Replace("-", "")
            dt = Common.GetDetailsOld("OD-2", pn_no)
            dr = dt.Rows(0)
            loan_status = dr.Item("LOAN_STAT")
            wrapper = New RowWrapper(dr)
            pgRow.SelectedObject = wrapper
        End If
    End Sub

    Private Sub FindLoan()
        Dim kbci_no As String
        Dim dt As DataTable
        Dim dr As DataRow
        Dim member As Business.Objects.MemberList = Common.GetMemberList()

        If member IsNot Nothing Then
            kbci_no = member.KBCI_NO
            dr = Common.FindByListOld("MA-3", kbci_no)

            If dr IsNot Nothing Then
                pn_no = dr.Item("PN_NO")
                dt = Common.GetDetailsOld("OD-1", pn_no)

                If dt.Rows.Count > 0 Then
                    dgvDetails.DataSource = dt
                    SetPropertyGrid()
                Else
                    dgvDetails.DataSource = Nothing
                End If

                dt.Dispose()
            End If
        End If
    End Sub

    Private Sub LRIDueToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LRIDueToolStripMenuItem.Click
        Dim popup As New Business.Popups.DateRange
        If Common.OpenPopup(popup) = PopupResponses.Quit Then Exit Sub
        Common.OpenReport(Of Report.Lri.Due)(popup.DateFrom, popup.DateTo)
    End Sub

    Private Sub DeductionFromDivrefToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeductionFromDivrefToolStripMenuItem.Click
        Common.OpenReport(Of Report.Lri.Deduction)()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub DailyCollectionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DailyCollectionToolStripMenuItem.Click
        Dim popup As New Business.Popups.DateRange
        Dim x_menu As Integer = Common.PopupOptions("Released Loans", "Payment", "All", "Cancel", "Choose Source")

        If x_menu = 4 Then
            Exit Sub
        End If

        If Common.OpenPopup(popup) = PopupResponses.Quit Then Exit Sub
        Common.OpenReport(Of Report.Lri.Collection)(popup.DateFrom, popup.DateTo, x_menu)
    End Sub
End Class