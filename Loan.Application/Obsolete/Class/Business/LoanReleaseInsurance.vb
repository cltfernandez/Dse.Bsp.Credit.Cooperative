Imports System.Data.SqlClient

Namespace Business

    Public Class LoanReleaseInsurance

        Public Shared Function IsLRIPaid(ByVal xp As Integer, ByVal pn_no As String, ByVal kbci_no As String, ByVal xpayment As Double, ByVal my_user As String, _
            ByVal xfulpay As Boolean, ByRef xor_no As String, ByVal xsa_no As String, ByVal xpdc_no As String, ByVal xpdc_bank As String) As Boolean

            Dim db As New Data.Database()

            Try
                db.BeginTransaction()
                If xp <> 1 Then
                    db.AddParameter("@gen_mode", "V")
                    xor_no = db.ExecuteQuery("dbo.s3p_J_GEN_LAPP", CommandType.StoredProcedure).Tables(0).Rows(0).Item("VOUCHER")
                End If
                db.AddParameter("@xp", xp)
                db.AddParameter("@pn_no", pn_no)
                db.AddParameter("@kbci_no", kbci_no)
                db.AddParameter("@xpayment", xpayment)
                db.AddParameter("@my_user", my_user)
                db.AddParameter("@xfulpay", xfulpay)
                db.AddParameter("@xor_no", xor_no)
                db.AddParameter("@xsa_no", xsa_no)
                db.AddParameter("@xpdc_no", xpdc_no)
                db.AddParameter("@xpdc_bnk", xpdc_bank)
                db.ExecuteNonQuery("s3p_Others_LriMaintenance_Payment", CommandType.StoredProcedure)
                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                Return False
            End Try
        End Function

        Public Shared Function IsLriHeld(ByVal date1 As DateTime, ByVal date2 As DateTime) As Boolean
            Dim db As New Data.Database()

            Try
                db.BeginTransaction()
                db.AddParameter("@dt1", date1)
                db.AddParameter("@dt2", date2)
                db.ExecuteNonQuery("s3p_Others_LriMaintenance_Divref2_HoldLri", CommandType.StoredProcedure)
                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                Return False
            End Try
        End Function

        Public Shared Function IsLriPosted(ByVal my_user As String) As Boolean
            Dim db As New Data.Database()

            Try
                db.BeginTransaction()
                db.AddParameter("@my_user", my_user)
                db.ExecuteNonQuery("s3p_Others_LriMaintenance_Divref2_Post", CommandType.StoredProcedure)
                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                Return False
            End Try
        End Function

    End Class

End Namespace