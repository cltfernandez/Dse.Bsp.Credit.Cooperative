Imports System.Data.SqlClient

Namespace Business

    Public Class LoanRestructuring

        Public Shared Function IsRestructured(ByVal drLoansBlank As DataRow, ByVal dtRDEDBAL As DataTable, ByVal Principal As Double, ByVal rAD_INT As Double, ByVal rSC As Double, ByVal rLRI As Double, ByVal sysdate As DateTime, ByVal rsold As Boolean, ByRef pn_no_new As String) As Boolean
            Dim db As New Data.Database()
            Dim pnNumber As String

            db.BeginTransaction()

            Try
                db.AddParameter("@gen_mode", "P")
                pnNumber = db.ExecuteQuery("dbo.s3p_J_Gen_Lapp", CommandType.StoredProcedure).Tables(0).Rows(0)(0)
                drLoansBlank.Item("PN_NO") = pnNumber
                pn_no_new = pnNumber

                PopulateParameter(db, pnNumber, sysdate, "DM", "AMT", "PRI", Principal, "INITIAL - PRINCIPAL")
                db.ExecuteNonQuery("dbo.s3p_J_U_Ledger", CommandType.StoredProcedure)

                If rAD_INT > 0 Then
                    PopulateParameter(db, pnNumber, sysdate, "CM", "INT", "INT", rAD_INT, "INITIAL - ADD. INTEREST")
                    db.ExecuteNonQuery("dbo.s3p_J_U_Ledger", CommandType.StoredProcedure)
                End If

                If rSC > 0 Then
                    PopulateParameter(db, pnNumber, sysdate, "CM", "SC", "OTH", rSC, "INITIAL - SVC CHARGE")
                    db.ExecuteNonQuery("dbo.s3p_J_U_Ledger", CommandType.StoredProcedure)
                End If

                If rLRI <> 0 Then
                    PopulateParameter(db, pnNumber, sysdate, "CM", "LRI", "OTH", rLRI, "INITIAL - LRI")
                    db.ExecuteNonQuery("dbo.s3p_J_U_Ledger", CommandType.StoredProcedure)
                End If

                If Not rsold Then
                    Dim dr As DataRow
                    Dim rtag As Boolean
                    Dim rpn_no As String
                    Dim rltyp As String

                    For Each dr In dtRDEDBAL.Rows
                        rtag = CBool(dr.Item("rtag"))
                        rpn_no = dr.Item("rpn_no")
                        rltyp = dr.Item("rltyp")

                        If rtag Then
                            db.AddParameter("@mode", "F")
                            db.AddParameter("@nampien", rpn_no)
                            db.AddParameter("@lontayp", rltyp)
                            db.AddParameter("@my_user", sysuser)
                            db.ExecuteNonQuery("dbo.s3p_J_Preterm", CommandType.StoredProcedure)
                        End If
                    Next

                    db.AddParameter("@PN_NO", drLoansBlank.Item("PN_NO"))
                    db.AddParameter("@KBCI_NO", drLoansBlank.Item("KBCI_NO"))
                    db.AddParameter("@APP_DATE", drLoansBlank.Item("APP_DATE"))
                    db.AddParameter("@APP_NO", drLoansBlank.Item("APP_NO"))
                    db.AddParameter("@DATE_GRANT", drLoansBlank.Item("DATE_GRANT"))
                    db.AddParameter("@BY_WHOM", drLoansBlank.Item("BY_WHOM"))
                    db.AddParameter("@DATE_DUE", drLoansBlank.Item("DATE_DUE"))
                    db.AddParameter("@CHKNO_BANK", drLoansBlank.Item("CHKNO_BANK"))
                    db.AddParameter("@CHKNO", drLoansBlank.Item("CHKNO"))
                    db.AddParameter("@CHKNO_AMT", drLoansBlank.Item("CHKNO_AMT"))
                    db.AddParameter("@CHKNO_DATE", drLoansBlank.Item("CHKNO_DATE"))
                    db.AddParameter("@CHKNO_RELS", drLoansBlank.Item("CHKNO_RELS"))
                    db.AddParameter("@CHKNO_ACK", drLoansBlank.Item("CHKNO_ACK"))
                    db.AddParameter("@MOD_PAY", drLoansBlank.Item("MOD_PAY"))
                    db.AddParameter("@AMORT_AMT", drLoansBlank.Item("AMORT_AMT"))
                    db.AddParameter("@PAY_START", drLoansBlank.Item("PAY_START"))
                    db.AddParameter("@RATE", drLoansBlank.Item("RATE"))
                    db.AddParameter("@TERM", drLoansBlank.Item("TERM"))
                    db.AddParameter("@FREQ", drLoansBlank.Item("FREQ"))
                    db.AddParameter("@PRINCIPAL", drLoansBlank.Item("PRINCIPAL"))
                    db.AddParameter("@LED_TYPE", drLoansBlank.Item("LED_TYPE"))
                    db.AddParameter("@ADV_INTE", drLoansBlank.Item("ADV_INTE"))
                    db.AddParameter("@AFT_INTE", drLoansBlank.Item("AFT_INTE"))
                    db.AddParameter("@ACCU_PAYP", drLoansBlank.Item("ACCU_PAYP"))
                    db.AddParameter("@YTD_I", drLoansBlank.Item("YTD_I"))
                    db.AddParameter("@LOAN_TYPE", drLoansBlank.Item("LOAN_TYPE"))
                    db.AddParameter("@LOAN_STAT", drLoansBlank.Item("LOAN_STAT"))
                    db.AddParameter("@ARREAR_I", drLoansBlank.Item("ARREAR_I"))
                    db.AddParameter("@ARREAR_P", drLoansBlank.Item("ARREAR_P"))
                    db.AddParameter("@ARREAR_OTH", drLoansBlank.Item("ARREAR_OTH"))
                    db.AddParameter("@ARREAR_AS", drLoansBlank.Item("ARREAR_AS"))
                    db.AddParameter("@COLLATERAL", drLoansBlank.Item("COLLATERAL"))
                    db.AddParameter("@DED_BAL", drLoansBlank.Item("DED_BAL"))
                    db.AddParameter("@ADD_DATE", drLoansBlank.Item("ADD_DATE"))
                    db.AddParameter("@CHG_DATE", drLoansBlank.Item("CHG_DATE"))
                    db.AddParameter("@USER", drLoansBlank.Item("USER"))
                    db.AddParameter("@P_BAL", drLoansBlank.Item("P_BAL"))
                    db.AddParameter("@I_BAL", drLoansBlank.Item("I_BAL"))
                    db.AddParameter("@O_BAL", drLoansBlank.Item("O_BAL"))
                    db.AddParameter("@REC_STAT", drLoansBlank.Item("REC_STAT"))
                    db.AddParameter("@RENEW", drLoansBlank.Item("RENEW"))
                    db.AddParameter("@ADVANCE", drLoansBlank.Item("ADVANCE"))
                    db.AddParameter("@LRI_IND", drLoansBlank.Item("LRI_IND"))
                    db.AddParameter("@NDUE", drLoansBlank.Item("NDUE"))
                    db.AddParameter("@L_EXT", drLoansBlank.Item("L_EXT"))
                    db.AddParameter("@PD", drLoansBlank.Item("PD"))
                    db.AddParameter("@LRI_DUE", drLoansBlank.Item("LRI_DUE"))
                    db.ExecuteNonQuery("dbo.s3p_Loans_LoanRestructuring", CommandType.StoredProcedure)
                End If

                db.CommitTransaction()
                Return True
            Catch ex As Exception
                db.RollbackTransaction()
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
                Return False
            End Try
        End Function

        Private Shared Sub PopulateParameter(ByVal db As Data.Database, ByVal PN_NO As String, ByVal SYSDATE As DateTime, ByVal DOX_TYPE As String, ByVal ACCT_TYPE As String, ByVal ACCT_CODE As String, ByVal CRDR As Double, ByVal RMK As String)
            db.AddParameter("@PN_NO", PN_NO)
            db.AddParameter("@DATE", SYSDATE)
            db.AddParameter("@DOX_TYPE", DOX_TYPE)
            db.AddParameter("@REF", PN_NO)
            db.AddParameter("@ACCT_TYPE", ACCT_TYPE)
            db.AddParameter("@ACCT_CODE", ACCT_CODE)
            db.AddParameter("@CRDR", CRDR)
            db.AddParameter("@RMK", RMK)
            db.AddParameter("@MY_USER", SystemObjects.sysuser)
        End Sub

    End Class

End Namespace