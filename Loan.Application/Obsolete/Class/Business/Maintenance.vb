Imports System.Data.SqlClient

Namespace Business

    Public Class Maintenance

        Public Shared Function IsMemberInserted(ByVal SQLCommand As SqlCommand, ByVal MY_USER As String) As Boolean
            Dim db As New Data.Database()

            Try
                Dim kbci_no As String
                db.BeginTransaction()
                db.AddParameter("@gen_mode", "K")
                kbci_no = CStr(db.ExecuteQuery("dbo.s3p_J_Gen_Lapp", CommandType.StoredProcedure).Tables(0).Rows(0)(0))

                db.Command = SQLCommand
                db.AddParameter("@KBCI_NO", kbci_no)
                db.AddParameter("@MY_USER", MY_USER)
                db.ExecuteNonQuery("dbo.s3p_MT_Member_Insert", CommandType.StoredProcedure)
                db.CommitTransaction()
                MsgBox("Record saved with KBCI No. " & kbci_no, MsgBoxStyle.Information)
                db = Nothing
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
                db = Nothing
                Return False
            End Try
        End Function

        Public Shared Sub UpdateRunningBalance(ByVal pn_no As String)
            Dim db As New Data.Database()

            If pn_no <> String.Empty Then
                db.AddParameter("@PN_NO", pn_no)
                db.ExecuteNonQuery("s3p_Loans_LoansNew_RecomputeBalance", CommandType.StoredProcedure)
            End If

            db = Nothing
        End Sub

        Public Shared Sub UpdateLriDue(ByVal pn_no As String, ByVal lridue As Double, ByVal my_user As String)
            Dim db As New Data.Database()
            db.AddParameter("@PN_NO", pn_no)
            db.AddParameter("@LRIDUE", lridue)
            db.AddParameter("@MY_USER", my_user)
            db.ExecuteNonQuery("dbo.s3p_MT_LRIDUE_Update", CommandType.StoredProcedure)
            db = Nothing
        End Sub

    End Class

End Namespace