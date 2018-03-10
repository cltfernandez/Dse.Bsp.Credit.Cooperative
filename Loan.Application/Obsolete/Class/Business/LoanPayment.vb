Imports System.Data.SqlClient

Namespace Business

    Public Class LoanPayment

        Public Shared Function GetFullAmount(ByVal PN_No As String) As DataRow
            Dim db As New Data.Database()
            db.AddParameter("@vPN_NO", PN_No)
            Return db.ExecuteQuery("dbo.s3p_Loans_LoansPayment_GetFullAmount", CommandType.StoredProcedure).Tables(0).Rows(0)
        End Function

        Public Shared Function ProcessPayment(ByVal MY_USER As String, ByVal KBCI_NO As String, ByVal PN_NO As String, ByVal DATE_DUE As String, _
            ByVal LOAN_TYPE As String, ByVal LRI_DUE As Double, ByVal XBPRIN As Double, ByVal XADVINT As Double, ByVal XLASTD As String, ByVal XPINT As Double, _
            ByVal XPARR As Double, ByVal XIARR As Double, ByVal XPEN As Double, ByVal XIOUTS As Double, ByVal XPOUTS As Double, _
            ByVal XP As Short, ByVal XPAYMENT As Double, ByVal XFULPAY As Boolean, ByVal XOR_NO As String, ByVal XPDC_BNK As String, _
            ByVal XPDC_NO As String, ByVal XSA_NO_OTH As String) As DataSet

            Dim db As New Data.Database()
            Dim ds As New DataSet

            db.AddParameter("@MY_USER", MY_USER)
            db.AddParameter("@KBCI_NO", KBCI_NO)
            db.AddParameter("@PN_NO", PN_NO)
            db.AddParameter("@DATE_DUE", DATE_DUE)
            db.AddParameter("@LOAN_TYPE", LOAN_TYPE)
            db.AddParameter("@LRI_DUE", LRI_DUE)
            db.AddParameter("@XBPRIN", XBPRIN)
            db.AddParameter("@XADVINT", XADVINT)
            db.AddParameter("@XLASTD", XLASTD)
            db.AddParameter("@XPINT", XPINT)
            db.AddParameter("@XPARR", XPARR)
            db.AddParameter("@XIARR", XIARR)
            db.AddParameter("@XPEN", XPEN)
            db.AddParameter("@XIOUTS", XIOUTS)
            db.AddParameter("@XPOUTS", XPOUTS)
            db.AddParameter("@XP", XP)
            db.AddParameter("@XPAYMENT", XPAYMENT)
            db.AddParameter("@XFULPAY", XFULPAY)
            db.AddParameter("@XOR_NO", XOR_NO)
            db.AddParameter("@XPDC_BNK", XPDC_BNK)
            db.AddParameter("@XPDC_NO", XPDC_NO)
            db.AddParameter("@XSA_NO_OTH", XSA_NO_OTH)
            ds = db.ExecuteQuery("dbo.s3p_Loans_LoansPayment_ProcessPayment", CommandType.StoredProcedure)

            db.AddParameter("@PN_NO", PN_NO)
            db.ExecuteNonQuery("s3p_Loans_LoansNew_RecomputeBalance", CommandType.StoredProcedure)
            Return ds
        End Function

    End Class

End Namespace