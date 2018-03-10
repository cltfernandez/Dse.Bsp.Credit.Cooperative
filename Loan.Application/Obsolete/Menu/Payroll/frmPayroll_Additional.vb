Public Class frmPayroll_Additional

    Private Sub frmPayroll_Additional_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dtMemberList As New DataTable
        Dim common As New Common
        Dim dt As New DataTable
        Dim frm As New frmCODataGridSearch

        CancelButton = btnCancel
        LoadPayrollCode(cboPayrollCode)
    End Sub

    Private Sub btnFeed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFeed.Click
        If txtEmpNo.Text = String.Empty AndAlso cboPayrollCode.SelectedIndex = 0 AndAlso Not IsNumeric(txtAdvice.Text) Then
            MsgBox("Please fill all fields.", MsgBoxStyle.Exclamation)
        End If

        Dim db As New Database.Sql()
        Dim strKey As String = txtEmpNo.Text.Trim + cboPayrollCode.SelectedValue.ToString
        Dim strBegda As String = Format(DateAdd(DateInterval.Month, 1, sysdate), "yyyyMM01")
        Dim strEndda As String

        If cboPayrollCode.SelectedValue = "7610" Then
            strEndda = strBegda
        Else
            strEndda = "99991231"
        End If

        Try
            db.beginTransaction()
            db.addInputParameter("@EMPNO", txtEmpNo.Text.Trim)
            db.addInputParameter("@WAGE_TYPE", cboPayrollCode.SelectedValue)
            db.addInputParameter("@BEGDA", strBegda)
            db.addInputParameter("@ENDDA", strEndda)
            db.addInputParameter("@AMOUNT", txtAdvice.Text)
            db.runUncommittedCommand("dbo.s3p_Payroll_Generate_Feed", CommandType.StoredProcedure)
            db.commitTransaction()
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            MsgBox("Successfully feeded!")
        Catch ex As Exception
            db.rollbackTransaction()
            MsgBox("Not feeded! Reason: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        db = Nothing
    End Sub

    Private Sub btnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStop.Click
        If txtEmpNo.Text = String.Empty AndAlso cboPayrollCode.SelectedIndex = 0 Then
            MsgBox("Please fill all fields.", MsgBoxStyle.Exclamation)
        End If

        Dim db As New Database.Sql()
        Dim strKey As String = txtEmpNo.Text.Trim + cboPayrollCode.SelectedValue.ToString
        Dim sysdatetmp As Date = DateAdd(DateInterval.Month, 1, sysdate)
        sysdatetmp = CDate(sysdatetmp.Month.ToString + "/01/" + sysdatetmp.Year.ToString)
        sysdatetmp = DateAdd(DateInterval.Day, -1, sysdatetmp)
        Dim strEndda As String = Format(sysdatetmp, "yyyyMMdd")

        Try
            db.beginTransaction()
            db.addInputParameter("@EMPNO", txtEmpNo.Text.Trim)
            db.addInputParameter("@WAGE_TYPE", cboPayrollCode.SelectedValue)
            db.addInputParameter("@ENDDA", strEndda)
            db.runUncommittedCommand("dbo.s3p_Payroll_Generate_Stop", CommandType.StoredProcedure)
            db.commitTransaction()
            Common.SetControls(Me, Common.EnumSetControls.Clear)
            MsgBox("Successfully stopped!")
        Catch ex As Exception
            db.rollbackTransaction()
            MsgBox("Not stopped! Reason: " & ex.Message, MsgBoxStyle.Critical)
        End Try

        db = Nothing
    End Sub
End Class