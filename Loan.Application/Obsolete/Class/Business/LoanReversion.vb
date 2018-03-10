Imports System.Data.SqlClient

Namespace Business

    Public Class LoanReversion

        Public Shared Function IsReversed(ByVal xtdate As DateTime, ByVal xpn_no As String, ByVal roth As Double, ByVal rint As Double, ByVal rpri As Double, ByVal opri As Double, ByVal oint As Double, ByVal date_godue As DateTime) As Boolean
            Dim db As New Data.Database()

            db.AddParameter("@xtdate", xtdate)
            db.AddParameter("@xpn_no", xpn_no)
            db.AddParameter("@roth", roth)
            db.AddParameter("@rint", rint)
            db.AddParameter("@rpri", rpri)
            db.AddParameter("@opri", opri)
            db.AddParameter("@oint", oint)
            db.AddParameter("@date_godue", date_godue)
            Try
                db.ExecuteNonQuery("dbo.s3p_Loans_LoanReversion", CommandType.StoredProcedure)
                Return True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                Return False
            End Try
        End Function

    End Class

End Namespace