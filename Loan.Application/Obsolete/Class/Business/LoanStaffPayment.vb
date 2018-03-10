Imports System.Data.SqlClient

Namespace Business

    Public Class LoanStaffPayment

        Public Shared Function IsEdited(ByVal pn_no As String, ByVal XARRP As Double, ByVal XARRI As Double, ByVal XARRO As Double, ByVal XPAMT As Double, ByVal XIAMT As Double, ByVal XOAMT As Double, ByVal XARRD As String) As Boolean
            Dim db As New Data.Database()

            Try
                db.AddParameter("@PN_NO", pn_no)
                db.AddParameter("@ARREAR_P", XARRP)
                db.AddParameter("@ARREAR_I", XARRI)
                db.AddParameter("@ARREAR_OTH", XARRO)
                db.AddParameter("@ARREAR_AS", IIf(XARRD = String.Empty, DBNull.Value, XARRD))
                db.AddParameter("@P_BAL", XPAMT)
                db.AddParameter("@I_BAL", XIAMT)
                db.AddParameter("@O_BAL", XOAMT)
                db.ExecuteNonQuery("s3p_Loans_LoanStaffPayment_Edit", CommandType.StoredProcedure)
                Return True
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try
        End Function

        Public Shared Function IsExtracted() As Boolean
            Dim db As New Data.Database()

            Try
                db.ExecuteNonQuery("s3p_Loans_LoanStaffPayment_Extract", CommandType.StoredProcedure)
                Return True
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try
        End Function

        Public Shared Function IsPosted() As Boolean
            Dim db As New Data.Database()

            Try
                db.AddParameter("@MY_USER", SystemObjects.sysuser)
                db.ExecuteNonQuery("s3p_Loans_LoanStaffPayment_Post", CommandType.StoredProcedure)
                Return True
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try
        End Function

    End Class

End Namespace