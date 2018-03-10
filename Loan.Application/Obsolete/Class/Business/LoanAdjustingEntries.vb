Imports System.Data.SqlClient

Namespace Business

    Public Class LoanAdjustingEntries

        Private db As New Data.Database()
        Private dt As New DataTable
        Public isLRI As Boolean = False

        Public Sub AdjustingLoanEntry(ByVal PN_NO As String, ByVal Ledger_DATE As String, ByVal DOXTYPE As String, ByVal REF As String, ByVal ACCT_TYPE As String, ByVal ACCT_CODE As String, ByVal CRDR As Double, ByVal RMK As String, ByVal ADD_DATE As String)
            Dim dr As DataRow
            If dt.Columns.Count <> 9 Then
                dt.Columns.Add(Common.AddColumn("System.String", "PN_NO"))
                dt.Columns.Add(Common.AddColumn("System.String", "DATE"))
                dt.Columns.Add(Common.AddColumn("System.String", "DOX_TYPE"))
                dt.Columns.Add(Common.AddColumn("System.String", "REF"))
                dt.Columns.Add(Common.AddColumn("System.String", "ACCT_TYPE"))
                dt.Columns.Add(Common.AddColumn("System.String", "ACCT_CODE"))
                dt.Columns.Add(Common.AddColumn("System.Double", "CRDR"))
                dt.Columns.Add(Common.AddColumn("System.String", "RMK"))
                dt.Columns.Add(Common.AddColumn("System.String", "ADD_DATE"))
            End If

            dr = dt.NewRow
            dr.Item("PN_NO") = PN_NO
            dr.Item("DATE") = Ledger_DATE
            dr.Item("DOX_TYPE") = DOXTYPE
            dr.Item("REF") = REF
            dr.Item("ACCT_TYPE") = ACCT_TYPE
            dr.Item("ACCT_CODE") = ACCT_CODE
            dr.Item("CRDR") = CRDR
            dr.Item("RMK") = RMK
            dr.Item("ADD_DATE") = ADD_DATE
            dt.Rows.Add(dr)
            dr = Nothing
        End Sub

        Public Sub AdjustLoanUncommittedCommand(ByVal PN_NO As String, ByVal Ledger_DATE As String, ByVal DOXTYPE As String, ByVal REF As String, ByVal ACCT_TYPE As String, ByVal ACCT_CODE As String, ByVal CRDR As Double, ByVal RMK As String, ByVal ADD_DATE As String)
            db.AddParameter("@PN_NO", PN_NO)
            db.AddParameter("@DATE", Ledger_DATE)
            db.AddParameter("@DOX_TYPE", DOXTYPE)
            db.AddParameter("@REF", REF)
            db.AddParameter("@ACCT_TYPE", ACCT_TYPE)
            db.AddParameter("@ACCT_CODE", ACCT_CODE)
            db.AddParameter("@CRDR", CRDR)
            db.AddParameter("@RMK", RMK)
            db.AddParameter("@ADD_DATE", ADD_DATE)
            db.ExecuteNonQuery("dbo.s3p_J_U_Ledger", CommandType.StoredProcedure)
        End Sub

        Public Sub AdjustLoanRollback()
            db.RollbackTransaction()
        End Sub

        Public Sub AdjustLoanCommit()
            db.CommitTransaction()
        End Sub

        Public Sub AdjustLRIDUE(ByVal PN_NO As String, ByVal XLAMT As Double, ByVal XLDC As String)
            Try
                db.AddParameter("@PN_NO", PN_NO)
                db.AddParameter("@XLAMT", XLAMT)
                db.AddParameter("@XLDC", XLDC)
                db.ExecuteNonQuery("dbo.s3p_Loans_LoanAdjustingEntries_LRIDUE", CommandType.StoredProcedure)
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
            End Try
        End Sub

        Public Function IsLoanAdjusted() As Boolean
            Dim dr As DataRow
            Dim PN_NO As String = String.Empty
            Dim CRDR As Double
            Dim DOXTYPE As String

            Try
                db.BeginTransaction()
                Dim intX As Integer = 0
                For Each dr In dt.Rows
                    db.AddParameter("@PN_NO", dr.Item("PN_NO"))
                    db.AddParameter("@DATE", dr.Item("DATE"))
                    db.AddParameter("@DOX_TYPE", dr.Item("DOX_TYPE"))
                    db.AddParameter("@REF", dr.Item("REF"))
                    db.AddParameter("@ACCT_TYPE", dr.Item("ACCT_TYPE"))
                    db.AddParameter("@ACCT_CODE", dr.Item("ACCT_CODE"))
                    db.AddParameter("@CRDR", dr.Item("CRDR"))
                    db.AddParameter("@RMK", dr.Item("RMK"))
                    db.AddParameter("@ADD_DATE", dr.Item("ADD_DATE"))
                    db.AddParameter("@MY_USER", SystemObjects.sysuser)
                    db.ExecuteNonQuery("dbo.s3p_J_U_Ledger", CommandType.StoredProcedure)

                    PN_NO = dr.Item("PN_NO")
                    CRDR = dr.Item("CRDR")
                    DOXTYPE = dr.Item("DOX_TYPE")

                    If isLRI And dt.Rows.Count - 1 = intX Then
                        db.AddParameter("@PN_NO", PN_NO)
                        db.AddParameter("@XLAMT", CRDR)
                        db.AddParameter("@XLDC", DOXTYPE)
                        db.ExecuteNonQuery("dbo.s3p_Loans_LoanAdjustingEntries_LRIDUE", CommandType.StoredProcedure)
                    End If

                    intX += 1
                Next

                If PN_NO <> String.Empty Then
                    db.AddParameter("@PN_NO", PN_NO)
                    db.ExecuteNonQuery("s3p_Loans_LoansNew_RecomputeBalance", CommandType.StoredProcedure)
                End If

                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                MessageBox.Show("ERROR: " & ex.Message, "Loan Adjustment", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End Try
        End Function

        Public Function IsArrearsAdjusted(ByVal PN_NO As String, ByVal XARRP As Double, ByVal XARRI As Double, ByVal XARRO As Double, ByVal XARRD As Object) As Boolean
            Try
                db.AddParameter("@PN_NO", PN_NO)
                db.AddParameter("@ARREAR_P", XARRP)
                db.AddParameter("@ARREAR_I", XARRI)
                db.AddParameter("@ARREAR_OTH", XARRO)
                db.AddParameter("@ARREAR_AS", XARRD)
                db.AddParameter("@USER", sysuser)
                db.AddParameter("@CHG_DATE", sysdate)
                db.ExecuteNonQuery("dbo.s3p_Loans_LoanAdjustingEntries_Arrears", CommandType.StoredProcedure)
                Return True
            Catch ex As Exception
                MsgBox("ERROR: " & ex.Message, MsgBoxStyle.Critical)
                Return False
            End Try
        End Function

    End Class

End Namespace