Imports System.Data.SqlClient

Namespace Business

    Public Class LoanApplication

        Public Shared Function N_Proc( _
            ByVal xmode As String, ByVal pn_no As String, ByVal kbci_no As String, ByVal loan_type As String, _
            ByVal led_type As Integer, ByVal adv_inte As Double, ByVal principal As Double, ByVal rate As Double, _
            ByVal term As Double, ByVal mod_pay As String, ByVal freq As String, ByVal lri_ind As Boolean, _
            ByVal renew As Boolean, ByVal xrenew2 As Boolean, ByVal id As String, ByVal my_user As String, _
            ByVal jmsc As Boolean _
            ) As DataSet

            Dim db As New Data.Database()

            db.AddParameter("@xmode", xmode)
            db.AddParameter("@pn_no", pn_no)
            db.AddParameter("@kbci_no", kbci_no)
            db.AddParameter("@loan_type", loan_type)
            db.AddParameter("@led_type", led_type)
            db.AddParameter("@adv_inte", adv_inte)
            db.AddParameter("@principal", principal)
            db.AddParameter("@rate", rate)
            db.AddParameter("@term", term)
            db.AddParameter("@mod_pay", mod_pay)
            db.AddParameter("@freq", freq)
            db.AddParameter("@lri_ind", lri_ind)
            db.AddParameter("@renew", renew)
            db.AddParameter("@xrenew2", xrenew2)
            db.AddParameter("@id", id)
            db.AddParameter("@my_user", my_user)
            db.AddParameter("@jmsc", jmsc)
            Return db.ExecuteQuery("dbo.s3p_J_N_Proc", CommandType.StoredProcedure)
        End Function

        Public Shared Function UpdateSDEDBAL( _
            ByVal sdedbal_id As Integer, ByVal pn_tag As Boolean, ByVal renew As Boolean, ByVal pn_no As String, _
            ByVal pay_tag As String, ByVal pay_amt As Double, ByVal lri_due As Double _
            ) As Boolean

            Dim db As New Data.Database()

            db.AddParameter("@SDEDBAL_ID", sdedbal_id)
            db.AddParameter("@PN_TAG", pn_tag)
            db.AddParameter("@RENEW", renew)
            db.AddParameter("@PN_NO", pn_no)
            db.AddParameter("@PAY_TAG", pay_tag)
            db.AddParameter("@PAY_AMT", pay_amt)
            db.AddParameter("@LRI_DUE", lri_due)
            Try
                db.ExecuteNonQuery("dbo.s3p_Loans_LoansNew_UpdateSDEDBAL", CommandType.StoredProcedure)
                Return True
            Catch ex As Exception
                Throw ex
                Return False
            End Try
        End Function

        Public Shared Function UpdateODEDBAL( _
            ByVal mode As String, ByVal odedbal_id As String, ByVal id As String, ByVal opn_no As String, ByVal oloan_type As String, _
            ByVal odox_type As String, ByVal oref As String, ByVal odr As Double, ByVal ocr As Double, ByVal ormk As String, _
            ByVal ormkc As String, ByVal obank As String, ByVal ochkno As String, ByVal oacctno As String, ByVal oacctnm As String _
            ) As Boolean

            Dim db As New Data.Database()

            db.AddParameter("@MODE", mode)
            db.AddParameter("@ODEDBAL_ID", odedbal_id)
            db.AddParameter("@ID", id)
            db.AddParameter("@OPN_NO", opn_no)
            db.AddParameter("@OLOAN_TYPE", oloan_type)
            db.AddParameter("@ODOX_TYPE", odox_type)
            db.AddParameter("@OREF", oref)
            db.AddParameter("@ODR", odr)
            db.AddParameter("@OCR", ocr)
            db.AddParameter("@ORMK", ormk)
            db.AddParameter("@ORMKC", ormkc)
            db.AddParameter("@OBANK", obank)
            db.AddParameter("@OCHKNO", ochkno)
            db.AddParameter("@OACCTNO", oacctno)
            db.AddParameter("@OACCTNM", oacctnm)
            Try
                db.ExecuteNonQuery("dbo.s3p_Loans_LoansNew_UpdateODEDBAL", CommandType.StoredProcedure)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Function

        Public Shared Function UpdateODEDBAL(ByVal mode As String, ByVal odedbal_id As String) As Boolean
            Dim db As New Data.Database()

            db.AddParameter("@MODE", mode)
            db.AddParameter("@ODEDBAL_ID", odedbal_id)
            Try
                db.ExecuteNonQuery("dbo.s3p_Loans_LoansNew_UpdateODEDBAL", CommandType.StoredProcedure)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End Function

        Public Shared Function Insert( _
            ByVal drLoansBlank As DataRow, _
            ByVal xmode As String, _
            ByVal kbci_no As String, _
            ByVal loan_type As String, _
            ByVal led_type As Integer, _
            ByVal adv_inte As Double, _
            ByVal principal As Double, _
            ByVal rate As Double, _
            ByVal term As Double, _
            ByVal mod_pay As String, _
            ByVal freq As String, _
            ByVal lri_ind As Boolean, _
            ByVal renew As Boolean, _
            ByVal xrenew2 As Boolean, _
            ByVal id As String, _
            ByVal my_user As String, _
            ByVal strComaker1 As String, _
            ByVal strComaker2 As String, _
            ByVal jmsc As Boolean _
            ) As String

            Dim db As New Data.Database()
            Dim pnNumber As String
            Dim dblPrincipal As Double = drLoansBlank.Item("PRINCIPAL")
            drLoansBlank.Item("USER") = my_user

            Try
                db.BeginTransaction()

                db.AddParameter("@gen_mode", "P")
                pnNumber = CStr(db.ExecuteQuery("dbo.s3p_J_Gen_Lapp", CommandType.StoredProcedure).Tables(0).Rows(0)(0))

                MsgBox("PN No: " & pnNumber, MsgBoxStyle.Information)
                drLoansBlank.Item("PN_NO") = pnNumber

                'db.addInputParameter("@pn_no", pn_no)
                'db.addInputParameter("@id", id)
                'db.runUncommittedCommand("dbo.s3p_Loans_LoansNew_UpdateODEDBAL_PN", CommandType.StoredProcedure)

                db.AddParameter("@xmode", xmode)
                db.AddParameter("@pn_no", pnNumber)
                db.AddParameter("@kbci_no", kbci_no)
                db.AddParameter("@loan_type", loan_type)
                db.AddParameter("@led_type", led_type)
                db.AddParameter("@adv_inte", adv_inte)
                db.AddParameter("@principal", principal)
                db.AddParameter("@rate", rate)
                db.AddParameter("@term", term)
                db.AddParameter("@mod_pay", mod_pay)
                db.AddParameter("@freq", freq)
                db.AddParameter("@lri_ind", lri_ind)
                db.AddParameter("@renew", renew)
                db.AddParameter("@xrenew2", xrenew2)
                db.AddParameter("@id", id)
                db.AddParameter("@my_user", my_user)
                db.AddParameter("@jmsc", jmsc)
                db.ExecuteNonQuery("dbo.s3p_J_N_Proc", CommandType.StoredProcedure)

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
                db.AddParameter("@COMAKER1", strComaker1)
                db.AddParameter("@COMAKER2", strComaker2)
                db.ExecuteNonQuery("dbo.s3p_Loans_LoansNew_Insert", CommandType.StoredProcedure)

                db.AddParameter("@PN_NO", drLoansBlank.Item("PN_NO"))
                db.ExecuteNonQuery("s3p_Loans_LoansNew_RecomputeBalance", CommandType.StoredProcedure)
                db.CommitTransaction()
                Return pnNumber
            Catch ex As Exception
                db.RollbackTransaction()
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                Return String.Empty
            End Try
        End Function

    End Class

End Namespace