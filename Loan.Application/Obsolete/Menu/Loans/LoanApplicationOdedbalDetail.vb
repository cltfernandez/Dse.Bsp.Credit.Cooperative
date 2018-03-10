Public Class LoanApplicationOdedbalDetail

    Private strKBCI As String
    Private strID As String
    Private oloan_type As String

    Public Property KBCI_NO() As String
        Get
            Return strKBCI
        End Get
        Set(ByVal value As String)
            strKBCI = value
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

    Public Property LoanType() As String
        Get
            Return oloan_type
        End Get
        Set(ByVal value As String)
            oloan_type = value
        End Set
    End Property

    Private Sub LoanApplicationOdedbalDetail_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AcceptButton = btnOK
        CancelButton = btnCancel

        cboDoxType.Focus()

        Dim dtDOX_TYPE As New DataTable
        dtDOX_TYPE.Columns.Add(Common.AddColumn("System.String", "RMKNAME"))
        dtDOX_TYPE.Columns.Add(Common.AddColumn("System.String", "RMKCODE"))

        AddRowRmk(dtDOX_TYPE, "CM", "CM")
        AddRowRmk(dtDOX_TYPE, "DM", "DM")

        cboDoxType.DataSource = dtDOX_TYPE
        cboDoxType.DisplayMember = "RMKNAME"
        cboDoxType.ValueMember = "RMKCODE"

        cboDoxType.SelectedIndex = 1
        dtDOX_TYPE = Nothing

        Dim dt As New DataTable
        dt.Columns.Add(Common.AddColumn("System.String", "RMKNAME"))
        dt.Columns.Add(Common.AddColumn("System.String", "RMKCODE"))

        AddRowRmk(dt, "LRI", "LRI")
        AddRowRmk(dt, "SAVINGS", "SAV")
        AddRowRmk(dt, "CASH", "CSH")
        AddRowRmk(dt, "COH-CHECK", "CHK")
        AddRowRmk(dt, "A/P", "AP")
        AddRowRmk(dt, "A/R", "AR")
        AddRowRmk(dt, "OTHERS", "OTH")
        AddRowRmk(dt, "FIXED DEP", "FIX")
        AddRowRmk(dt, "S-CHARGE", "SCH")

        cboRMK.DataSource = dt
        cboRMK.DisplayMember = "RMKNAME"
        cboRMK.ValueMember = "RMKCODE"

        Common.SetComboBoxAutoComplete(cboDoxType)
        Common.SetComboBoxAutoComplete(cboRMK)

        dt = Nothing
    End Sub

    Private Sub AddRowRmk(ByVal dt As DataTable, ByVal RMKNAME As String, ByVal RMKCODE As String)
        Dim dr As DataRow
        dr = dt.NewRow
        dr.Item("RMKNAME") = RMKNAME
        dr.Item("RMKCODE") = RMKCODE
        dt.Rows.Add(dr)
        dr = Nothing
    End Sub

    Private Sub btnOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim dtSDMASTER As New DataTable
        Dim dtLNHOLD As New DataTable
        Dim xsavbal As Double
        Dim bank As String = String.Empty
        Dim chkno As String = String.Empty
        Dim acctno As String = String.Empty
        Dim acctnm As String = String.Empty

        If txtDR.Text = String.Empty Then txtDR.Text = "0"
        If txtCR.Text = String.Empty Then txtCR.Text = "0"

        If txtPNNo.Text = String.Empty Then
            MsgBox("PN NO is required") : Exit Sub
            'ElseIf txtRefNo.Text = String.Empty Then
            '    MsgBox("Ref is required") : Exit Sub
        End If

        If Not IsNumeric(txtDR.Text) Or Not IsNumeric(txtCR.Text) Then
            MsgBox("Amounts must be numeric.")
            Exit Sub
        End If

        If cboRMK.SelectedValue = "CHK" Then
            bank = InputBox("Enter bank")
            If bank = String.Empty Then Exit Sub
            chkno = InputBox("Check No")
            If chkno = String.Empty Then Exit Sub
        ElseIf cboRMK.SelectedValue = "SAV" Then
            dtSDMASTER = Common.GetDetailsOld("SDMASTER", strKBCI)

            If dtSDMASTER.Rows.Count > 0 AndAlso Not dtSDMASTER.Rows(0).Item("ACCTNO") Is DBNull.Value Then
                acctno = dtSDMASTER.Rows(0).Item("ACCTNO")

                If MsgBox("Use " & acctno & "?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    acctno = InputBox("Enter account number")
                    If acctno = String.Empty Then Exit Sub
                    dtSDMASTER = Common.GetDetailsOld("SDMASTER", acctno)
                End If
            End If

            If dtSDMASTER.Rows.Count <= 0 Then
                MsgBox("Account not found.", MsgBoxStyle.Information)
                Exit Sub
            Else
                acctnm = dtSDMASTER.Rows(0).Item("ACCTNAME")
                xsavbal = dtSDMASTER.Rows(0).Item("ACCTABAL") - 500

                dtLNHOLD = Common.GetDetailsOld("LA-3", acctno)
                If dtLNHOLD.Rows.Count > 0 Then
                    xsavbal += dtLNHOLD.Rows(0).Item("XSAVBAL")
                End If

                If xsavbal - CDbl(txtDR.Text) < 0 Then
                    MsgBox("ERROR: Insufficient Balance.", MsgBoxStyle.Exclamation)
                    Exit Sub
                End If
            End If
        End If

        dtLNHOLD.Dispose()
        dtSDMASTER.Dispose()
        Business.LoanApplication.UpdateODEDBAL("UPD", 0, ID, txtPNNo.Text.Trim, LoanType, cboDoxType.Text, cboRMK.Text.Trim, CDbl(txtDR.Text), CDbl(txtCR.Text), cboRMK.Text.Trim, cboRMK.SelectedValue, bank, chkno, acctno, acctnm)
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cboDoxType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDoxType.SelectedIndexChanged
        If cboDoxType.SelectedIndex = 0 Then
            txtCR.Enabled = True
            txtDR.Enabled = False
            txtDR.Text = "0"
            txtCR.Text = String.Empty
        Else
            txtDR.Enabled = True
            txtCR.Enabled = False
            txtCR.Text = "0"
            txtDR.Text = String.Empty
        End If
    End Sub

    Private Sub txtDR_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDR.LostFocus
        If IsNumeric(txtDR.Text) Then
            txtDR.Text = Format(CDbl(txtDR.Text), "#,##0.00")
        End If
    End Sub

    Private Sub txtCR_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCR.LostFocus
        If IsNumeric(txtCR.Text) Then
            txtCR.Text = Format(CDbl(txtCR.Text), "#,##0.00")
        End If
    End Sub
End Class