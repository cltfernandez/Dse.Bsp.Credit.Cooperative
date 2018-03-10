Public Class PopupPasswordChange
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim db As Loan.Application.Infrastructure.Data.Database
        Dim user As Business.Objects.User
        Dim userPass As String = Loan.Application.Infrastructure.Data.Database.GetEncryptedText(txtCurrent.Text)
        user = Common.GetObject(Of Business.Objects.User)(sysuser, userPass)

        If user IsNot Nothing Then
            If txtNew.Text.Trim().Length = 0 Then
                Common.PopupExclamation("Please enter new password.")
            ElseIf txtNew.Text = txtConfirm.Text Then
                user.USERPASS = Loan.Application.Infrastructure.Data.Database.GetEncryptedText(txtNew.Text)
                db = New Loan.Application.Infrastructure.Data.Database()
                db.UpdateRecord(Of Business.Objects.User)(user, String.Format("USERNAME = '{0}'", user.USERNAME))
                Common.PopupInformation("Password changed.")
                Me.Close()
            Else
                Common.PopupExclamation("Password confirmation failed.")
            End If
        Else
            Common.PopupExclamation("Wrong current password.")
        End If
    End Sub

    Private Sub PopupPasswordChange_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.AcceptButton = btnOK
        Me.CancelButton = btnCancel
    End Sub
End Class