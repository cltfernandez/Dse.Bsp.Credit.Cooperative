Imports System.Collections.Generic
Imports Loan.Application.Infrastructure.Enumerations
Imports Loan.Application.Infrastructure.Enumerations.Popups
Imports Loan.Application.Report

Namespace Business.Rules

    Namespace Loans

        Public Class NetProceeds

            Public ReadOnly Property Proceeds() As Decimal
                Get
                    Return m_loan.PRINCIPAL - (xad_int + xsc + xlri + xsd + xfd + xrenamt + lproc + othded + xmsc)
                End Get
            End Property

            Public Property message() As List(Of String)
                Get
                    Return m_message
                End Get
                Set(ByVal value As List(Of String))
                    m_message = value
                End Set
            End Property

            Private m_message As List(Of String)
            Private xad_int As Decimal
            Private xsc As Decimal
            Private xmsc As Decimal
            Private xlri As Decimal
            Private xsd As Decimal
            Private xfd As Decimal
            Private xrenamt As Decimal
            Private lproc As Decimal
            Private othded As Decimal

            Private m_mode As DropDownItems.NetProceedsMode
            Private m_loan As Business.Objects.Loan
            Private m_loanInput As Business.Objects.LoanInput
            Private m_member As Business.Objects.Member
            Private m_deductions As List(Of Business.Objects.Deductions)
            Private m_breakdown As List(Of Business.Objects.NetProceeds)
            Private m_charges As List(Of Business.Objects.Charge)
            Private db As Data.Database

            Public Sub New(ByVal mode As DropDownItems.NetProceedsMode, ByVal loan As Business.Objects.Loan, ByVal loanInput As Business.Objects.LoanInput, ByVal member As Business.Objects.Member, ByVal deductions As List(Of Business.Objects.Deductions), ByVal charges As List(Of Business.Objects.Charge))
                m_mode = mode
                m_loan = loan
                m_loanInput = loanInput
                m_member = member
                m_deductions = deductions
                m_charges = charges
                message = New List(Of String)
            End Sub

            Public Function InsertLedger(ByVal ledger As Business.Objects.Ledger) As Boolean
                Return db.InsertRecord(Of Business.Objects.Ledger)(ledger)
            End Function

            Private Function GetLedger(ByVal pnNo As String, ByVal entry As DropDownItems.DoubleEntry, ByVal ref As String, ByVal amount As Decimal, ByVal accountType As DropDownItems.AccountType, ByVal accountCode As DropDownItems.AccountCode, ByVal remarks As String) As Business.Objects.Ledger
                Dim ledger As New Business.Objects.Ledger()
                ledger.PN_NO = pnNo
                ledger.DATE = sysdate
                ledger.DOX_TYPE = entry
                ledger.REF = ref
                ledger.ACCT_TYPE = accountType
                ledger.ACCT_CODE = accountCode
                ledger.DR = IIf(entry = DropDownItems.DoubleEntry.DM, amount, 0)
                ledger.CR = IIf(entry = DropDownItems.DoubleEntry.CM, amount, 0)
                ledger.RMK = remarks
                ledger.ADD_DATE = sysdate
                ledger.USER = sysuser
                Return ledger
            End Function

            Private Function GetSavingsDeposit(ByVal loan As Business.Objects.Loan, ByVal tranCode As String, ByVal xsd As Decimal, ByVal remarks As String, ByVal febtcSa As String, ByVal balance As Decimal) As Business.Objects.SavingsDeposit
                Dim sd As New Business.Objects.SavingsDeposit()
                sd.KBCI_NO = loan.KBCI_NO
                sd.REF = loan.PN_NO
                sd.TRAN_CODE = tranCode
                sd.DATE = sysdate
                sd.AMOUNT = xsd
                sd.RMK = remarks
                sd.ADD_DATE = sysdate
                sd.USER = sysuser
                sd.FEBTC_SA = febtcSa
                sd.BALANCE = balance
                Return sd
            End Function

            Private Function GetFixedDeposit(ByVal loan As Business.Objects.Loan, ByVal tranCode As String, ByVal xfd As Decimal, ByVal drcr As String, ByVal balance As Decimal, ByVal remarks As String) As Business.Objects.FixedDeposit
                Dim fd As New Business.Objects.FixedDeposit()
                fd.KBCI_NO = loan.KBCI_NO
                fd.REF = loan.PN_NO
                fd.TRAN_CODE = tranCode
                fd.DATE = sysdate
                fd.AMOUNT = xfd
                fd.DRCR = drcr
                fd.TPOSTED = True
                fd.BALANCE = balance
                fd.RMK = remarks
                fd.ADD_DATE = sysdate
                fd.USER = sysuser
                fd.LPOSTED = 0
                Return fd
            End Function

#Region "Compute"

            Public Function GetNetProceeds() As List(Of Business.Objects.NetProceeds)
                Common.GetLatestControl()

                Select Case m_mode
                    Case DropDownItems.NetProceedsMode.C
                        message.Add("Computing for net proceeds.")
                    Case DropDownItems.NetProceedsMode.D, DropDownItems.NetProceedsMode.F
                        message.Add("Deducting from loan proceeds.")
                End Select

                GetAdditionalInterest()
                GetServiceCharge()
                GetMiscellaneousLiability()
                GetLri()
                GetSavingsDeposit()
                GetFixedDeposit()
                GetLoanRenewalAmount()
                GetLoanDeductionsAmount()
                GetOtherDeductionsAmount()
                GetNetProceedsBreakdown()
                Return m_breakdown
            End Function

            Private Sub GetAdditionalInterest()
                If m_loan.LOAN_TYPE = DropDownItems.LoanType.BFL Then
                    xad_int = m_loan.PRINCIPAL * 0.01
                Else
                    Select Case m_loan.LED_TYPE
                        Case DropDownItems.LedgerType.OneTimeInterest
                            If m_loan.ADV_INTE <> 0 Then
                                xad_int = m_loan.PRINCIPAL * (m_loan.RATE / 100) * (m_loan.ADV_INTE / 12)
                            Else
                                xad_int = 0
                            End If
                        Case DropDownItems.LedgerType.DiminishingPrincipal
                            Select Case m_loan.MOD_PAY
                                Case DropDownItems.PayMode.Payroll
                                    If sysdate.Day < 7 Then
                                        xad_int = m_loan.PRINCIPAL * (m_loan.RATE / 36000) * (7 - sysdate.Day)
                                    ElseIf sysdate.Day > 15 Then
                                        xad_int = m_loan.PRINCIPAL * (m_loan.RATE / 36000) * DateDiff(DateInterval.Day, sysdate, New Date(sysdate.AddMonths(1).Year, sysdate.AddMonths(1).Month, 7))
                                    End If
                                Case Else
                                    xad_int = 0
                            End Select
                    End Select
                End If

                xad_int = Math.Round(xad_int, 2)
            End Sub

            Private Sub GetServiceCharge()
                If m_loan.LOAN_TYPE = DropDownItems.LoanType.SML Then
                    xsc = 100
                ElseIf m_loan.LOAN_TYPE = DropDownItems.LoanType.BFL Then
                    xsc = m_loan.PRINCIPAL * 0.01
                Else
                    If m_loan.TERM < 12 Then
                        xsc = (0.01 * m_loan.PRINCIPAL * m_loan.TERM) / 12
                    Else
                        xsc = 0.01 * m_loan.PRINCIPAL
                    End If

                    If m_loan.FREQ = DropDownItems.Frequency.Daily Then
                        If m_loan.TERM > 30 Then
                            xsc = (0.01 * m_loan.PRINCIPAL * 30) / 360
                        Else
                            xsc = (0.01 * m_loan.PRINCIPAL * m_loan.TERM) / 360
                        End If
                    End If

                    xsc = Math.Round(xsc, 2)
                End If
            End Sub

            Private Sub GetMiscellaneousLiability()
                If m_loan.LOAN_TYPE = DropDownItems.LoanType.BFL Then
                    xmsc = 0
                Else
                    xmsc = IIf(m_loanInput.MISC, 100, 0)
                End If
            End Sub

            Private Sub GetLri()
                xlri = IIf(m_loanInput.LRI_IND, 0, 0.01 * m_loan.PRINCIPAL)
                xlri = Math.Round(xlri, 2)
            End Sub

            Private Sub GetSavingsDeposit()
                If m_loan.LOAN_TYPE = DropDownItems.LoanType.BFL Then
                    xsd = 0
                Else
                    Dim sd As Business.Objects.SavingsDepositMaster = Common.GetSavingsDeposit(SavingsDepositQuery.KBCI_NO, m_loan.KBCI_NO)

                    Select Case m_loan.LOAN_TYPE
                        Case DropDownItems.LoanType.EML, DropDownItems.LoanType.STL
                            message.Add("Exempted from SA deduction.")
                        Case Else
                            Select Case m_member.MEM_STAT
                                Case DropDownItems.MembershipStatus.Retired, DropDownItems.MembershipStatus.Staff
                                    message.Add("Exempted from SA deduction.")
                                Case Else
                                    If sd.ACCTABAL < sd.ACCTMAINT Then
                                        xsd = sd.ACCTMAINT - sd.ACCTABAL
                                    Else
                                        xsd = 0
                                    End If
                            End Select
                    End Select
                End If
            End Sub

            Private Sub GetFixedDeposit()
                Dim osdfd As Decimal
                Dim sdfd As Decimal

                If (m_loan.LOAN_TYPE = DropDownItems.LoanType.EML AndAlso m_loan.PRINCIPAL <= 5000 And m_member.FD_AMOUNT >= 5000) OrElse m_loanInput.xrenew2 Then
                    message.Add("Exempted from FD deduction.")
                ElseIf m_loan.LOAN_TYPE = DropDownItems.LoanType.BFL Then
                    xfd = 0
                Else
                    Select Case m_member.MEM_STAT
                        Case DropDownItems.MembershipStatus.Retired, DropDownItems.MembershipStatus.Staff
                            message.Add("Exempted from FD deduction.")
                        Case Else
                            osdfd = m_member.FD_AMOUNT
                            If osdfd >= loanControl.FDMAX Then
                                message.Add("FD limit reached")
                            Else
                                sdfd = m_loan.PRINCIPAL * 0.02
                                If osdfd < loanControl.FDMIN AndAlso sdfd < loanControl.FDMIN - osdfd Then sdfd = loanControl.FDMIN - osdfd
                                If (sdfd + osdfd) >= (loanControl.FDMAX - 100) Then sdfd = loanControl.FDMAX - osdfd
                                xfd = Math.Round(sdfd / 100, 0) * 100
                            End If
                    End Select
                End If
            End Sub

            Private Sub GetLoanRenewalAmount()
                Dim deduction As Business.Objects.Deductions
                If m_loan.RENEW Then
                    deduction = m_deductions.Where(Function(x) x.LOAN_TYPE = m_loan.LOAN_TYPE AndAlso x.KBCI_NO = m_loan.KBCI_NO AndAlso x.LOAN_STAT = DropDownItems.LoanStatus.Released).FirstOrDefault()
                    If deduction IsNot Nothing AndAlso deduction.PN_NO IsNot Nothing AndAlso deduction.PN_NO.Trim().Length > 0 Then
                        xrenamt = deduction.OUTSBAL
                    End If
                End If
            End Sub

            Private Sub GetLoanDeductionsAmount()
                message.AddRange(m_deductions.Where(Function(x) x.PN_TAG AndAlso x.RENEW = False).Select(Function(x) String.Format("Deduct {0} worth {1}.", x.LOAN_TYPE, x.PAY_AMT.ToString("#,##0.00"))).AsEnumerable())
                lproc = m_deductions.Where(Function(x) x.PN_TAG AndAlso x.RENEW = False).Select(Function(x) x.PAY_AMT).Sum()
            End Sub

            Private Sub GetOtherDeductionsAmount()
                message.Add("Other deductions")
                message.AddRange(m_charges.Where(Function(x) x.AMOUNT > 0 AndAlso x.OREF.Trim().Length() > 0).Select(Function(x) String.Format("{0} {1} worth {2}", IIf(x.ODOX_TYPE = DropDownItems.DoubleEntry.DM, "Add", "Deduct"), x.OREF, IIf(x.ODOX_TYPE = DropDownItems.DoubleEntry.DM, x.ODR.ToString("#,##0.00"), x.OCR.ToString("#,##0.00")))).AsEnumerable())
                othded = m_charges.Select(Function(x) x.OCR - x.ODR).Sum()
            End Sub

            Private Sub GetNetProceedsBreakdown()
                m_breakdown = New List(Of Business.Objects.NetProceeds)
                AddNetProceedsBreakdown("Loan Principal", m_loan.PRINCIPAL, 0)
                AddNetProceedsBreakdown("Additional Interest", 0, xad_int)
                AddNetProceedsBreakdown("Service Charge", 0, xsc)
                AddNetProceedsBreakdown("Loan Redemption Insurance", 0, xlri)
                AddNetProceedsBreakdown("Misc. Liabilities", 0, xmsc)
                AddNetProceedsBreakdown("Savings Deposit", 0, xsd)
                AddNetProceedsBreakdown("Fixed Deposit", 0, xfd)
                AddNetProceedsBreakdownDeductions()
                AddNetProceedsBreakdownCharges()
                AddNetProceedsBreakdownTotal()
            End Sub

            Private Sub AddNetProceedsBreakdown(ByVal account As String, ByVal debit As Decimal, ByVal credit As Decimal)
                If debit + credit > 0 Then
                    m_breakdown.Add(New Business.Objects.NetProceeds(account, debit, credit))
                End If
            End Sub

            Private Sub AddNetProceedsBreakdownDeductions()
                For Each deduction As Business.Objects.Deductions In m_deductions
                    AddNetProceedsBreakdown(deduction.LOAN_TYPE.ToString(), 0, deduction.PAY_AMT)
                Next
            End Sub

            Private Sub AddNetProceedsBreakdownCharges()
                For Each charge As Business.Objects.Charge In m_charges
                    AddNetProceedsBreakdown(charge.OREF, charge.ODR, charge.OCR)
                Next
            End Sub

            Private Sub AddNetProceedsBreakdownTotal()
                Dim debit As Decimal = m_breakdown.Select(Function(x) x.DEBIT - x.CREDIT).Sum()
                m_breakdown.Add(New Business.Objects.NetProceeds("Net Proceeds", debit, 0))
            End Sub

#End Region

#Region "Deduct"

            Public Function SetNetProceeds() As Boolean
                db = New Data.Database()
                db.BeginTransaction(IsolationLevel.ReadUncommitted)
                Try
                    GetPromissoryNoteNumber()
                    InsertLedgers()
                    InsertLoan()
                    InsertComaker()
                    InsertLriDue()
                    UpdateLedgerRunningBalance()
                    db.CommitTransaction()
                    Common.OpenReport(Of Voucher.Release)(m_loan.PN_NO, sysuser)
                    Return True
                Catch ex As Exception
                    db.RollbackTransaction()
                    Common.PopupError(ex.Message)
                    Return False
                End Try
            End Function

            Private Sub GetPromissoryNoteNumber()
                db.AddParameter("@gen_mode", "P")
                m_loan.PN_NO = CStr(db.ExecuteQuery("dbo.s3p_J_Gen_Lapp", CommandType.StoredProcedure).Tables(0).Rows(0)(0))
            End Sub

            Private Sub InsertLedgers()
                AddPrincipalLedger()
                AddAdditionalInterestLedger()
                AddServiceChargeLedger()
                AddLriLedger()
                AddMiscellaneousLiabilityLedger()
                AddSavingsDepositLedger()
                AddFixedDepositLedger()
                UpdateMemberAmounts()
                AddChargesLedger()
                AddDeductionsLedger()
            End Sub

            Private Sub InsertLoan()
                db.InsertRecord(Of Business.Objects.Loan)(m_loan)
            End Sub

            Private Sub InsertComaker()
                Dim comaker As New Business.Objects.Comaker()
                comaker.PN_NO = m_loan.PN_NO
                comaker.ADD_DATE = sysdate
                comaker.ADD_USER = sysuser

                If m_loanInput.COMAKER1 IsNot Nothing AndAlso m_loanInput.COMAKER1.Trim().Length() > 0 Then
                    comaker.COMAKER1 = m_loanInput.COMAKER1
                End If

                If (m_loanInput.COMAKER1 Is Nothing OrElse m_loanInput.COMAKER1.Trim().Length() <= 0) Then
                    If m_loanInput.COMAKER2 IsNot Nothing AndAlso m_loanInput.COMAKER2.Trim().Length() > 0 Then
                        comaker.COMAKER1 = m_loanInput.COMAKER2
                    End If
                Else
                    If m_loanInput.COMAKER2 IsNot Nothing AndAlso m_loanInput.COMAKER2.Trim().Length() > 0 Then
                        comaker.COMAKER2 = m_loanInput.COMAKER2
                    End If
                End If

                db.InsertRecord(Of Business.Objects.Comaker)(comaker)
            End Sub

            Private Sub InsertLriDue()
                Dim lri As New Business.Objects.LoanReleaseInsurance
                lri.PN_NO = m_loan.PN_NO
                lri.KBCI_NO = m_loan.KBCI_NO
                lri.LRI_BALDA = DateAdd(DateInterval.Month, 11, m_loan.PAY_START)
                lri.LOAN_BAL = 0
                lri.LRI_DUE_C = 0
                lri.LRI_DUE_P = 0
                db.InsertRecord(Of Business.Objects.LoanReleaseInsurance)(lri)
            End Sub

            Private Sub UpdateLedgerRunningBalance()
                db.AddParameter("@PN_NO", m_loan.PN_NO)
                db.ExecuteNonQuery("s3p_Loans_LoansNew_RecomputeBalance", CommandType.StoredProcedure)

                For Each dedn As Business.Objects.Deductions In m_deductions
                    db.AddParameter("@PN_NO", dedn.PN_NO)
                    db.ExecuteNonQuery("s3p_Loans_LoansNew_RecomputeBalance", CommandType.StoredProcedure)
                Next
            End Sub

            Private Sub AddPrincipalLedger()
                Dim amount As Decimal = m_loan.PRINCIPAL
                If amount > 0 Then
                    Dim pnNo As String = m_loan.PN_NO
                    Const remarks As String = "INITIAL - PRINCIPAL"
                    Const entry As DropDownItems.DoubleEntry = DropDownItems.DoubleEntry.DM
                    Const accountType As DropDownItems.AccountType = DropDownItems.AccountType.AMT
                    Const accountCode As DropDownItems.AccountCode = DropDownItems.AccountCode.PRI
                    InsertLedger(GetLedger(pnNo, entry, pnNo, amount, accountType, accountCode, remarks))
                End If
            End Sub

            Private Sub AddAdditionalInterestLedger()
                Dim amount As Decimal = xad_int
                If amount > 0 Then
                    Dim pnNo As String = m_loan.PN_NO
                    Const remarks As String = "INITIAL - ADD. INTEREST"
                    Const entry As DropDownItems.DoubleEntry = DropDownItems.DoubleEntry.CM
                    Const accountType As DropDownItems.AccountType = DropDownItems.AccountType.INT
                    Const accountCode As DropDownItems.AccountCode = DropDownItems.AccountCode.INT
                    InsertLedger(GetLedger(pnNo, entry, pnNo, amount, accountType, accountCode, remarks))
                End If
            End Sub

            Private Sub AddServiceChargeLedger()
                Dim amount As Decimal = xsc
                If amount > 0 Then
                    Dim pnNo As String = m_loan.PN_NO
                    Const remarks As String = "INITIAL - SVC CHARGE"
                    Const entry As DropDownItems.DoubleEntry = DropDownItems.DoubleEntry.CM
                    Const accountType As DropDownItems.AccountType = DropDownItems.AccountType.SC
                    Const accountCode As DropDownItems.AccountCode = DropDownItems.AccountCode.OTH
                    InsertLedger(GetLedger(pnNo, entry, pnNo, amount, accountType, accountCode, remarks))
                End If
            End Sub

            Private Sub AddLriLedger()
                Dim amount As Decimal = xlri
                If amount > 0 Then
                    Dim pnNo As String = m_loan.PN_NO
                    Const remarks As String = "INITIAL - LRI"
                    Const entry As DropDownItems.DoubleEntry = DropDownItems.DoubleEntry.CM
                    Const accountType As DropDownItems.AccountType = DropDownItems.AccountType.LRI
                    Const accountCode As DropDownItems.AccountCode = DropDownItems.AccountCode.OTH
                    InsertLedger(GetLedger(pnNo, entry, pnNo, amount, accountType, accountCode, remarks))
                End If
            End Sub

            Private Sub AddMiscellaneousLiabilityLedger()
                Dim amount As Decimal = xmsc
                If amount > 0 Then
                    Dim pnNo As String = m_loan.PN_NO
                    Const remarks As String = "INITIAL - MISC LIABILITY"
                    Const entry As DropDownItems.DoubleEntry = DropDownItems.DoubleEntry.CM
                    Const accountType As DropDownItems.AccountType = DropDownItems.AccountType.MSC
                    Const accountCode As DropDownItems.AccountCode = DropDownItems.AccountCode.OTH
                    InsertLedger(GetLedger(pnNo, entry, pnNo, amount, accountType, accountCode, remarks))
                End If
            End Sub

            Private Sub AddSavingsDepositLedger()
                Dim amount As Decimal = xsd

                If amount > 0 Then
                    Dim sdAmount As Decimal = m_member.SD_AMOUNT + amount
                    Dim pnNo As String = m_loan.PN_NO
                    Dim sdRemarks As String = String.Format("INITIAL CHARGES FOR LOAN {0}-{1}", m_loan.PN_NO, m_loan.LOAN_TYPE)
                    Const remarks As String = "INITIAL - SD"
                    Const entry As DropDownItems.DoubleEntry = DropDownItems.DoubleEntry.CM
                    Const accountType As DropDownItems.AccountType = DropDownItems.AccountType.SD
                    Const accountCode As DropDownItems.AccountCode = DropDownItems.AccountCode.OTH
                    InsertLedger(GetLedger(pnNo, entry, pnNo, amount, accountType, accountCode, remarks))
                    db.InsertRecord(Of Business.Objects.SavingsDeposit)(GetSavingsDeposit(m_loan, "1", amount, sdRemarks, m_member.FEBTC_SA, sdAmount))
                End If
            End Sub

            Private Sub AddFixedDepositLedger()
                Dim amount As Decimal = xfd

                If amount > 0 Then
                    Dim fdAmount As Decimal = m_member.FD_AMOUNT + amount
                    Dim pnNo As String = m_loan.PN_NO
                    Dim fdRemarks As String = String.Format("INITIAL CHARGES FOR LOAN {0}-{1}", m_loan.PN_NO, m_loan.LOAN_TYPE)
                    Const remarks As String = "INITIAL - FD"
                    Const entry As DropDownItems.DoubleEntry = DropDownItems.DoubleEntry.CM
                    Const accountType As DropDownItems.AccountType = DropDownItems.AccountType.FD
                    Const accountCode As DropDownItems.AccountCode = DropDownItems.AccountCode.OTH
                    InsertLedger(GetLedger(pnNo, entry, pnNo, amount, accountType, accountCode, remarks))
                    db.InsertRecord(Of Business.Objects.FixedDeposit)(GetFixedDeposit(m_loan, "7", amount, "CR", fdAmount, fdRemarks))
                End If
            End Sub

            Private Sub UpdateMemberAmounts()
                If xsd + xfd > 0 Then
                    m_member.FD_AMOUNT = IIf(xfd > 0, xfd + m_member.FD_AMOUNT, m_member.FD_AMOUNT)
                    m_member.SD_AMOUNT = IIf(xsd > 0, xsd + m_member.SD_AMOUNT, m_member.SD_AMOUNT)
                    m_member.CHG_DATE = sysdate
                    m_member.USER = sysuser
                    db.UpdateRecord(Of Business.Objects.Member)(m_member, String.Format("KBCI_NO = '{0}'", m_member.KBCI_NO))
                End If
            End Sub

            Private Sub AddChargesLedger()
                Dim ledger As Business.Objects.Ledger
                Dim accountType As DropDownItems.AccountType
                Dim remarks As String
                For Each charge As Business.Objects.Charge In m_charges
                    accountType = Infrastructure.Data.Parser.GetEnumFromDescription(Of DropDownItems.AccountType)(Infrastructure.Data.Parser.GetDescriptionFromEnum(charge.OACCT_TYPE))
                    If charge.OPN_NO = "This Loan" Then
                        remarks = String.Format("{0}{1}", charge.ORMK, IIf(charge.OBANK.Trim().Length() > 0, String.Format("{0}-{1}", charge.OBANK, charge.OCHKNO), String.Empty))
                        ledger = GetLedger(m_loan.PN_NO, charge.ODOX_TYPE, m_loan.PN_NO, charge.ODR + charge.OCR, accountType, charge.OACCT_CODE, remarks)
                        InsertLedger(ledger)
                    Else
                        remarks = String.Format("{0}-{1}-{2}/{3}", charge.ORMK, charge.OPN_NO, m_loan.PN_NO, m_loan.LOAN_TYPE)
                        ledger = GetLedger(charge.OPN_NO, charge.ODOX_TYPE, m_loan.PN_NO, charge.ODR + charge.OCR, accountType, charge.OACCT_CODE, remarks)
                        InsertLedger(ledger)
                        remarks = String.Format("{0}-{1}-{2}", charge.ORMK, charge.OPN_NO, m_loan.LOAN_TYPE)
                        ledger = GetLedger(m_loan.PN_NO, charge.ODOX_TYPE, m_loan.PN_NO, charge.ODR + charge.OCR, accountType, charge.OACCT_CODE, remarks)
                        InsertLedger(ledger)
                    End If

                    Select Case True
                        Case charge.ODOX_TYPE = DropDownItems.DoubleEntry.DM
                            m_loan.LRI_DUE = IIf(m_loan.LRI_DUE + charge.ODR > 0, m_loan.LRI_DUE + charge.ODR, 0)
                        Case charge.ODOX_TYPE = DropDownItems.DoubleEntry.CM
                            m_loan.LRI_DUE = IIf(m_loan.LRI_DUE - charge.OCR > 0, m_loan.LRI_DUE - charge.OCR, 0)
                    End Select

                    If charge.REMARKS = DropDownItems.ChargeRemarks.Savings Then
                        AddLoanHold(charge)
                    End If
                Next
            End Sub

            Private Sub AddLoanHold(ByVal charge As Business.Objects.Charge)
                Dim hold As New Business.Objects.LoanHold
                hold.ACCTNO = charge.OACCTNO
                hold.HOLDCD = "INI"
                hold.HOLDTYPE = charge.ODOX_TYPE.ToString()
                hold.HOLDAMT = IIf(charge.ODOX_TYPE = DropDownItems.DoubleEntry.DM, charge.ODR, charge.OCR)
                hold.HOLDDATE = sysdate
                hold.HOLDUSER = sysuser
                hold.HOLDRMKS = String.Format("INITIAL: {0}-{1}", m_loan.LOAN_TYPE.ToString(), m_loan.PN_NO)
                db.InsertRecord(Of Business.Objects.LoanHold)(hold)
            End Sub

            Private Sub AddDeductionsLedger()
                For Each deduction As Business.Objects.Deductions In m_deductions
                    If deduction.RENEW Then
                        Pretermination.Full(db, m_mode, m_loan.PN_NO, m_loan.LOAN_TYPE, deduction.PN_NO)
                    Else
                        Select Case deduction.PAY_TAG
                            Case DropDownItems.PayTag.Full
                                Pretermination.Part(db, 3, DropDownItems.NetProceedsMode.F, m_loan.PN_NO, deduction.PN_NO, deduction.LOAN_TYPE, deduction.PAY_AMT, deduction.PAY_TAG)
                            Case DropDownItems.PayTag.LRI
                                Pretermination.Part(db, 5, DropDownItems.NetProceedsMode.F, m_loan.PN_NO, deduction.PN_NO, deduction.LOAN_TYPE, deduction.PAY_AMT, deduction.PAY_TAG)
                            Case Else
                                Pretermination.Part(db, 4, DropDownItems.NetProceedsMode.F, m_loan.PN_NO, deduction.PN_NO, deduction.LOAN_TYPE, deduction.PAY_AMT, deduction.PAY_TAG)
                        End Select
                    End If
                Next
            End Sub

#End Region

        End Class

        Public Class Pretermination

            Public Shared Function Preterm(ByVal mode As String, ByVal nampien As String, ByVal lontayp As String, ByVal my_user As String, Optional ByVal xno_pn As String = "") As DataTable
                Dim db As New Data.Database()
                db.AddParameter("@mode", mode)
                db.AddParameter("@nampien", nampien)
                db.AddParameter("@lontayp", lontayp)
                db.AddParameter("@my_user", my_user)
                If xno_pn.Trim().Length() > 0 Then db.AddParameter("@xno_pn", xno_pn)
                Return db.ExecuteQuery("dbo.s3p_J_Preterm", CommandType.StoredProcedure).Tables(0)
            End Function

            Public Shared Sub Full(ByVal db As Data.Database, ByVal mode As DropDownItems.NetProceedsMode, ByVal pnNo As String, ByVal loanType As DropDownItems.LoanType, Optional ByVal renewPnNo As String = "")
                db.AddParameter("@mode", mode.ToString())
                db.AddParameter("@nampien", pnNo)
                db.AddParameter("@lontayp", loanType.ToString())
                db.AddParameter("@my_user", sysuser)
                If renewPnNo.Trim().Length() > 0 Then db.AddParameter("@xno_pn", renewPnNo)
                db.ExecuteNonQuery("dbo.s3p_J_Preterm", CommandType.StoredProcedure)
            End Sub

            Public Shared Sub Part(ByVal db As Data.Database, ByVal parameter As Integer, ByVal mode As DropDownItems.NetProceedsMode, ByVal pnNo As String, ByVal otherPnNo As String, ByVal loanType As DropDownItems.LoanType, Optional ByVal payAmount As Decimal = 0, Optional ByVal payTag As DropDownItems.PayTag = DropDownItems.PayTag.Untagged)
                db.AddParameter("@my_user", sysuser)
                db.AddParameter("@para", parameter)
                db.AddParameter("@mode", mode.ToString())
                db.AddParameter("@nampien", pnNo)
                db.AddParameter("@xno_pn", otherPnNo)
                db.AddParameter("@lontayp", loanType.ToString())
                If payAmount > 0 Then db.AddParameter("@ppayamt", payAmount)
                If payTag = DropDownItems.PayTag.LRI Then db.AddParameter("@pay_tag", Infrastructure.Data.Parser.GetDescriptionFromEnum(payTag))
                db.ExecuteNonQuery("s3p_J_Parterm", CommandType.StoredProcedure)
            End Sub

        End Class

    End Namespace

End Namespace