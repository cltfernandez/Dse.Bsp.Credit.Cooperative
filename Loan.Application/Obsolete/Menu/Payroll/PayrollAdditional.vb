Public Class PayrollAdditional

    Private Sub Payroll_Additional_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtMemberList As New DataTable
        Dim dt As New DataTable
        Dim form As New PopupSearchDr

        CancelButton = btnCancel
        Common.LoadPayrollCode(cboPayrollCode)
        cboPayrollCode.SelectedValue = "7635"
    End Sub

    Private Sub btnFeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeed.Click
        Dim name As String
        Dim dt As DataTable
        Dim oldAmount As Double
        Dim newAmount As Double
        Dim employeeNumber As String = txtEmpNo.Text.Trim()

        If employeeNumber.Length = 0 OrElse cboPayrollCode.SelectedIndex = 0 Then
            MsgBox("Please fill all fields.", MsgBoxStyle.Exclamation)
            Exit Sub
        ElseIf Not IsNumeric(txtAdvice.Text) Then
            MsgBox("Invalid amount.", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        dt = Common.GetDetailsOld("PG-1", employeeNumber)

        If dt.Rows.Count <= 0 Then
            MessageBox.Show("Employee not found.", "Feed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            name = dt.Rows(0).Item("FULL_NAME")
            oldAmount = dt.Rows(0).Item("AMOUNT")
            newAmount = CDbl(txtAdvice.Text.Trim())
        End If

        If oldAmount = newAmount Then
            MessageBox.Show("Feed record already existing.", "Feed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf MessageBox.Show(String.Format("Feed {0}?", name), "Feed", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim db As New Data.Database()
        Dim strKey As String = txtEmpNo.Text.Trim + cboPayrollCode.SelectedValue.ToString
        Dim strBegda As String = Format(DateAdd(DateInterval.Month, 1, sysdate), "yyyyMM01")
        Dim strEndda As String

        If cboPayrollCode.SelectedValue = "7610" Then
            strEndda = strBegda
        Else
            strEndda = "99991231"
        End If

        Try
            db.BeginTransaction()
            db.AddParameter("@EMPNO", employeeNumber)
            db.AddParameter("@WAGE_TYPE", cboPayrollCode.SelectedValue)
            db.AddParameter("@BEGDA", strBegda)
            db.AddParameter("@ENDDA", strEndda)
            db.AddParameter("@AMOUNT", newAmount)
            db.ExecuteNonQuery("dbo.s3p_Payroll_Generate_Feed", CommandType.StoredProcedure)
            db.CommitTransaction()
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            cboPayrollCode.SelectedValue = "7635"
            Me.Text = ""
            MessageBox.Show("Successfully feeded.", "Feed", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            db.RollbackTransaction()
            MessageBox.Show(String.Format("Not feeded! Reason: {0}", ex.Message), "Feed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        db = Nothing
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        If txtEmpNo.Text = String.Empty AndAlso cboPayrollCode.SelectedIndex = 0 Then
            MsgBox("Please fill all fields.", MsgBoxStyle.Exclamation)
        End If

        Dim db As New Data.Database()
        Dim strKey As String = txtEmpNo.Text.Trim + cboPayrollCode.SelectedValue.ToString
        Dim sysdatetmp As Date = DateAdd(DateInterval.Month, 1, sysdate)
        sysdatetmp = CDate(sysdatetmp.Month.ToString + "/01/" + sysdatetmp.Year.ToString)
        sysdatetmp = DateAdd(DateInterval.Day, -1, sysdatetmp)
        Dim strEndda As String = Format(sysdatetmp, "yyyyMMdd")

        Try
            db.BeginTransaction()
            db.AddParameter("@EMPNO", txtEmpNo.Text.Trim)
            db.AddParameter("@WAGE_TYPE", cboPayrollCode.SelectedValue)
            db.AddParameter("@ENDDA", strEndda)
            db.ExecuteNonQuery("dbo.s3p_Payroll_Generate_Stop", CommandType.StoredProcedure)
            db.CommitTransaction()
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            cboPayrollCode.SelectedValue = "7635"
            Me.Text = ""
            MsgBox("Successfully stopped!")
        Catch ex As Exception
            db.RollbackTransaction()
            MsgBox("Not stopped! Reason: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        db = Nothing
    End Sub

    Private Sub txtAdvice_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAdvice.LostFocus
        If IsNumeric(txtAdvice.Text) Then
            txtAdvice.Text = CDbl(txtAdvice.Text).ToString("#,##0,00")
        End If
    End Sub

    Private Sub txtEmpNo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpNo.LostFocus
        Dim dt As DataTable = Common.GetDetailsOld("PG-1", txtEmpNo.Text.Trim())

        If dt.Rows.Count <= 0 Then
            MessageBox.Show("Employee not found.", "Feed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            Me.Text = dt.Rows(0).Item("FULL_NAME")
        End If
    End Sub
End Class