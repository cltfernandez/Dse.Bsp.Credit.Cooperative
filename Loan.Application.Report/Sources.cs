using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loan.Application.Report
{
    namespace Adhoc
    {
        public class Runup
        {
            public bool SORT_BY_NAME { get; set; }
            public string SORT { get; set; }
            public string DETAIL { get; set; }
            public string KBCI_NO { get; set; }
            public string PN_NO { get; set; }
            public DateTime DATE_GRANT { get; set; }
            public DateTime NDUE { get; set; }
            public Int32 TERM { get; set; }
            public decimal LOAN_AMOUNT { get; set; }
            public decimal NET_PROCEEDS { get; set; }
            public decimal LOAN_BALANCE { get; set; }
            public string STAT { get; set; }
            public decimal RATE { get; set; }
        }

        public class MiscellaneousLiability
        {
            public string KBCI_NO { get; set; }
            public string NAME { get; set; }
            public string PN_NO { get; set; }
            public string TYPE { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public decimal CR { get; set; }
        }

        public class MissedPayments : Runup { }

        public class Philam { }

        internal class Refund
        {
            public Int64 ID { get; set; }
            public Int32 DECEMBER_ANNIV { get; set; }
            public Int32 YEAR { get; set; }
            public string LNAME { get; set; }
            public string FNAME { get; set; }
            public string MI { get; set; }
            public DateTime B_DATE { get; set; }
            public Int32 AGE { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public DateTime DATE_DUE { get; set; }
            public string PN_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal AMOUNT_PAID { get; set; }
            public decimal ANNIVERSARY_BALANCE { get; set; }
            public decimal LRI_REFUND { get; set; }
            public DateTime ANNIV_DATE { get; set; }
            public DateTime CHG_DATE { get; set; }
            public Int32 MULTIPLIER { get; set; }
        }

        internal class Remit
        {
            public Int64 ID { get; set; }
            public bool FLAG { get; set; }
            public Int32 YEAR { get; set; }
            public string LNAME { get; set; }
            public string FNAME { get; set; }
            public string MI { get; set; }
            public DateTime B_DATE { get; set; }
            public Int32 AGE { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public DateTime DATE_DUE { get; set; }
            public string PN_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal PROCEEDS { get; set; }
            public decimal BALANCE { get; set; }
            public decimal PREMIUM { get; set; }
        }

        public class TotalExposure
        {
            public string PN_NO { get; set; }
            public string KBCI_NO { get; set; }
            public string FULL_NAME { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal CHKNO_AMT { get; set; }
            public decimal BALANCE { get; set; }
            public string LOAN_TYPE { get; set; }
            public bool PD { get; set; }
            public string LOAN_STAT { get; set; }
            public DateTime DATE_GRANT { get; set; }
            public DateTime DATE_DUE { get; set; }
            public DateTime START_DATE { get; set; }
            public DateTime END_DATE { get; set; }
        }

        public class Arrears
        {
            public DateTime DATE_FROM { get; set; }
            public DateTime DATE_TO { get; set; }
            public string NAME { get; set; }
            public string PN_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public string PAID { get; set; }
            public DateTime END_DATE { get; set; }
            public string LABEL { get; set; }
            public decimal AMOUNT { get; set; }
        }
    }

    namespace Admin
    {
        public class DailyTransactionRegister
        {
            public Int32 RANKING { get; set; }
            public string KBCI_NO { get; set; }
            public string NAME { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal BEGBAL { get; set; }
            public decimal DR { get; set; }
            public decimal CR { get; set; }
            public decimal ENDBAL { get; set; }
            public decimal INTE { get; set; }
            public decimal PEN { get; set; }
            public decimal SC { get; set; }
            public decimal FD { get; set; }
            public decimal SD { get; set; }
            public decimal LRI { get; set; }
            public decimal BEGBAL_S { get; set; }
            public decimal DR_S { get; set; }
            public decimal CR_S { get; set; }
            public decimal ENDBAL_S { get; set; }
            public decimal INT_AMNT_S { get; set; }
            public decimal INTCR_S { get; set; }
            public decimal INTDR_S { get; set; }
            public decimal FINT_AMNT_S { get; set; }
            public decimal PEN_S { get; set; }
            public decimal SC_S { get; set; }
            public decimal FD_S { get; set; }
            public decimal SD_S { get; set; }
            public decimal LRI_S { get; set; }
            public string PN_NO { get; set; }
            public DateTime SYSDATE { get; set; }
        }

        public class DailyTransactionRegisterDetails
        {
            public string KBCI_NO { get; set; }
            public string NAME { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal BEG_BAL { get; set; }
            public decimal DR { get; set; }
            public decimal CR { get; set; }
            public decimal ENDBAL { get; set; }
            public decimal INTE { get; set; }
            public decimal PEN { get; set; }
            public decimal SC { get; set; }
            public decimal FD { get; set; }
            public decimal SD { get; set; }
            public decimal LRI { get; set; }
            public decimal BEGBAL_S { get; set; }
            public decimal DR_S { get; set; }
            public decimal CR_S { get; set; }
            public decimal ENDBAL_S { get; set; }
            public decimal INT_AMNT_S { get; set; }
            public decimal INTCR_S { get; set; }
            public decimal INTDR_S { get; set; }
            public decimal FINT_AMNT_S { get; set; }
            public decimal PEN_S { get; set; }
            public decimal SC_S { get; set; }
            public decimal FD_S { get; set; }
            public decimal SD_S { get; set; }
            public decimal LRI_S { get; set; }
            public string PN_NO { get; set; }
            public DateTime SYSDATE { get; set; }
        }

        public class DailyTransactionRegisterSummary
        {
            public DateTime SYSDATE { get; set; }
            public bool PD { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal DR { get; set; }
            public decimal CR { get; set; }
            public decimal INTDR { get; set; }
            public decimal INTCR { get; set; }
            public decimal PEN { get; set; }
            public decimal SC { get; set; }
            public decimal FD { get; set; }
            public decimal SD { get; set; }
            public decimal LRI { get; set; }
        }

        public class FullyPaidLoans
        {
            public string PN_NO { get; set; }
            public string KBCI_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal CHKNO_AMT { get; set; }
            public string MOD_PAY { get; set; }
            public decimal TERM { get; set; }
            public string FREQ { get; set; }
            public decimal RATE { get; set; }
            public DateTime PAY_START { get; set; }
            public DateTime DATE_DUE { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public string MEMBER { get; set; }
            public DateTime SYSDATE { get; set; }
        }

        public class InterestDetails
        {
            public string MOYEAR { get; set; }
            public string KBCI_NO { get; set; }
            public string MEMBER { get; set; }
            public string LOAN_TYPE { get; set; }
            public DateTime ADD_DATE { get; set; }
            public decimal BEGBAL { get; set; }
            public decimal DR { get; set; }
            public decimal CR { get; set; }
            public decimal ENDBAL { get; set; }
            public string RMK { get; set; }
            public string SYSDATE { get; set; }
        }

        public class InterestSummary
        {
            public string MOYEAR { get; set; }
            public decimal INTE { get; set; }
            public string SYSDATE { get; set; }
        }

        public class MonthlyRunup
        {
            public bool SHOW_SUB_TOTAL { get; set; }
            public DateTime SYSDATE { get; set; }
            public bool PD { get; set; }
            public string LOAN_TYPE { get; set; }
            public string KBCI_NO { get; set; }
            public string BORROWER { get; set; }
            public DateTime B_DATE { get; set; }
            public Int32 AGE { get; set; }
            public string PN_NO { get; set; }
            public DateTime DATE_GRANT { get; set; }
            public DateTime DATE_DUE { get; set; }
            public decimal TERM { get; set; }
            public decimal RATE { get; set; }
            public decimal AMORT_AMT { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal BALANCE { get; set; }
            public DateTime LAST_TRAN_DT { get; set; }
            public decimal ARREARS { get; set; }
            public Int32 MONTHS { get; set; }
        }

        public class MonthlyRunupBreakdown
        {
            public string MONTH_NAME { get; set; }
            public string ACCTNO { get; set; }
            public string KBCI_NO { get; set; }
            public string CB_EMPNO { get; set; }
            public string NAME { get; set; }
            public string CODE5 { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal AMORT_AMT { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal CHKNO_AMT { get; set; }
            public decimal BALANCE { get; set; }
            public decimal ARREARS { get; set; }
            public string PN_NO { get; set; }
        }

        public class MonthlyRunupConsolidation
        {
            public string MONTH_NAME { get; set; }
            public string ACCTNO { get; set; }
            public string KBCI_NO { get; set; }
            public string CB_EMPNO { get; set; }
            public string NAME { get; set; }
            public string CODE5 { get; set; }
            public decimal AMORT_AMT { get; set; }
        }

        public class ReleasedLoans
        {
            public string PN_NO { get; set; }
            public string KBCI_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal CHKNO_AMT { get; set; }
            public string MOD_PAY { get; set; }
            public decimal TERM { get; set; }
            public string FREQ { get; set; }
            public decimal RATE { get; set; }
            public DateTime PAY_START { get; set; }
            public DateTime DATE_DUE { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public string MEMBER { get; set; }
            public DateTime SYSDATE { get; set; }
        }

        public class ReversedTransactionRegisterDetails
        {
            public string KBCI_NO { get; set; }
            public string NAME { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal BEG_BAL { get; set; }
            public decimal DR { get; set; }
            public decimal CR { get; set; }
            public decimal ENDBAL { get; set; }
            public decimal INTE { get; set; }
            public decimal PEN { get; set; }
            public decimal SC { get; set; }
            public decimal FD { get; set; }
            public decimal SD { get; set; }
            public decimal LRI { get; set; }
            public decimal BEGBAL_S { get; set; }
            public decimal DR_S { get; set; }
            public decimal CR_S { get; set; }
            public decimal ENDBAL_S { get; set; }
            public decimal INT_AMNT_S { get; set; }
            public Int32 INTCR_S { get; set; }
            public Int32 INTDR_S { get; set; }
            public decimal FINT_AMNT_S { get; set; }
            public decimal PEN_S { get; set; }
            public decimal SC_S { get; set; }
            public decimal FD_S { get; set; }
            public decimal SD_S { get; set; }
            public decimal LRI_S { get; set; }
            public string PN_NO { get; set; }
            public DateTime SYSDATE { get; set; }
        }

        public class ReversedTransactionRegisterSummary
        {
            public string LOAN_TYPE { get; set; }
            public decimal DR { get; set; }
            public decimal CR { get; set; }
            public decimal INT { get; set; }
            public decimal PEN { get; set; }
            public decimal SC { get; set; }
            public decimal FD { get; set; }
            public decimal SD { get; set; }
            public decimal LRI { get; set; }
            public DateTime SYSDATE { get; set; }
        }

        public class StaffPtlInterestPaid
        {
            public string KBCI_NO { get; set; }
            public string FULL_NAME { get; set; }
            public decimal Q01 { get; set; }
            public decimal Q02 { get; set; }
            public decimal Q03 { get; set; }
            public decimal Q04 { get; set; }
            public decimal M01 { get; set; }
            public decimal M02 { get; set; }
            public decimal M03 { get; set; }
            public decimal M04 { get; set; }
            public decimal M05 { get; set; }
            public decimal M06 { get; set; }
            public decimal M07 { get; set; }
            public decimal M08 { get; set; }
            public decimal M09 { get; set; }
            public decimal M10 { get; set; }
            public decimal M11 { get; set; }
            public decimal M12 { get; set; }
            public int Year { get; set; }
        }

        public class StaffPtlBalance
        {
            public int REPORT_YEAR { get; set; }
            public string FULL_NAME { get; set; }
            public string PN_NO { get; set; }
            public decimal Q01 { get; set; }
            public decimal Q02 { get; set; }
            public decimal Q03 { get; set; }
            public decimal Q04 { get; set; }
            public decimal M01 { get; set; }
            public decimal M02 { get; set; }
            public decimal M03 { get; set; }
            public decimal M04 { get; set; }
            public decimal M05 { get; set; }
            public decimal M06 { get; set; }
            public decimal M07 { get; set; }
            public decimal M08 { get; set; }
            public decimal M09 { get; set; }
            public decimal M10 { get; set; }
            public decimal M11 { get; set; }
            public decimal M12 { get; set; }
        }

        public class TotalInterestPaid
        {
            public string KBCI_NO { get; set; }
            public string FULL_NAME { get; set; }
            public string MEM_STAT { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal INTEREST { get; set; }
            public int Year { get; set; }
        }

        public class TotalInterestPaidByRange
        {
            public string KBCI_NO { get; set; }
            public string FULL_NAME { get; set; }
            public string MEM_STAT { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal INTEREST { get; set; }
            public DateTime DATE_FROM { get; set; }
            public DateTime DATE_TO { get; set; }
        }

    }

    namespace CashDividend
    {
        public class Register
        {
            public string KBCI_NO { get; set; }
            public string FULL_NAME { get; set; }
            public DateTime DATES { get; set; }
            public decimal FD_AMOUNT { get; set; }
            public decimal DIVIDEND { get; set; }
            public decimal DEDUCTIONS { get; set; }
            public decimal NET_DIVIDEND { get; set; }
            public Int32 ORDER { get; set; }
            public string REGIONS { get; set; }
        }
    }

    namespace Loans
    {
        public class AmortizationSchedule
        {
            public DateTime DATES { get; set; }
            public decimal OUTS_BALANCE { get; set; }
            public decimal AMORT_PRIN { get; set; }
            public decimal AMORT_INT { get; set; }
            public decimal TOTAL_AMORT { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal INTEREST { get; set; }
            public decimal TOTAL_PAYMENT { get; set; }
            public string FULL_NAME { get; set; }
            public string LOAN_DESC { get; set; }
        }

        public class CashDisbursementOrder
        {
            public string FULL_NAME { get; set; }
            public string XOR_NO { get; set; }
            public DateTime SYSDATE { get; set; }
            public string MY_USER { get; set; }
            public string PAYREM { get; set; }
            public DateTime PAYDATE { get; set; }
            public string LOAN_TYPE { get; set; }
            public string PN_NO { get; set; }
            public decimal DEBIT { get; set; }
            public string REMARKS { get; set; }
            public decimal CREDIT { get; set; }
        }

        public class FullyPaidLoans
        {
            public string PN_NO { get; set; }
            public string KBCI_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal BALANCE_PAID { get; set; }
            public string MOD_PAY { get; set; }
            public decimal TERM { get; set; }
            public string FREQ { get; set; }
            public decimal RATE { get; set; }
            public DateTime PAY_START { get; set; }
            public DateTime DATE_DUE { get; set; }
            public string FULL_NAME { get; set; }
            public string CB_EMPNO { get; set; }
            public DateTime DT1 { get; set; }
            public DateTime DT2 { get; set; }
        }

        public class KbciDeductionRegister
        {
            public Int32 ID { get; set; }
            public string FULL_NAME { get; set; }
            public string LOAN_TYPE { get; set; }
            public DateTime DATES { get; set; }
            public decimal XPRI { get; set; }
            public decimal XINT { get; set; }
            public decimal TOTAL { get; set; }
            public string PN_NO { get; set; }
            public DateTime PAYDATE { get; set; }
            public string MONTH_NAME { get; set; }
        }

        public class LoanArrears
        {
            public string LOAN_TYPE { get; set; }
            public string KBCI_NO { get; set; }
            public string CB_EMPNO { get; set; }
            public string FULL_NAME { get; set; }
            public decimal AMORT_AMT { get; set; }
            public string PN_NO { get; set; }
            public decimal ARREAR_P { get; set; }
            public decimal ARREAR_I { get; set; }
            public decimal ARREAR_OTH { get; set; }
            public decimal ARREAR_TOTAL { get; set; }
            public Int32 Months { get; set; }
            public string MOD_PAY { get; set; }
            public decimal OUTSBAL { get; set; }
            public DateTime SYSDATE { get; set; }
        }

        public class LoansDue
        {
            public DateTime fromDate { get; set; }
            public DateTime toDate { get; set; }
            public DateTime sysDate { get; set; }
            public string myUser { get; set; }
            public string PN_NO { get; set; }
            public string KBCI_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal AMORT_AMT { get; set; }
            public string MOD_PAY { get; set; }
            public decimal TERM { get; set; }
            public string FREQ { get; set; }
            public decimal RATE { get; set; }
            public DateTime PAY_START { get; set; }
            public DateTime DATE_DUE { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public string FULL_NAME { get; set; }
        }
                
        public class PaymentRegister
        {
            public DateTime PAYDATE { get; set; }
            public string PAYTYPE { get; set; }
            public decimal PAYAMT { get; set; }
            public DateTime ADDATE { get; set; }
            public string UPDUSER { get; set; }
            public string PAYOR { get; set; }
            public string PAYREM { get; set; }
            public string FULL_NAME { get; set; }
            public string LOAN_DESC { get; set; }
            public DateTime SYSDATE { get; set; }
        }

        public class PreterminatedLoans
        {
            public string PN_NO { get; set; }
            public string KBCI_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal CHKNO_AMT { get; set; }
            public string MOD_PAY { get; set; }
            public decimal TERM { get; set; }
            public string FREQ { get; set; }
            public decimal RATE { get; set; }
            public DateTime PAY_START { get; set; }
            public DateTime DATE_DUE { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public DateTime DT1 { get; set; }
            public DateTime DT2 { get; set; }
        }

        public class ReleasedLoans
        {
            public DateTime DT1 { get; set; }
            public DateTime DT2 { get; set; }
            public string LOAN_TYPE { get; set; }
            public Int64 SEQ { get; set; }
            public string FULL_NAME { get; set; }
            public string BDATE { get; set; }
            public int AGE { get; set; }
            public string RELDATE { get; set; }
            public string DUEDATE { get; set; }
            public string PN_NO { get; set; }
            public string CHKNO { get; set; }
            public string BANK { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal PROCEEDS { get; set; }
            public decimal BALANCE { get; set; }
        }

        public class RestructuredLoans
        {
            public string PN_NO { get; set; }
            public string KBCI_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal CHKNO_AMT { get; set; }
            public string MOD_PAY { get; set; }
            public decimal TERM { get; set; }
            public string FREQ { get; set; }
            public decimal RATE { get; set; }
            public DateTime PAY_START { get; set; }
            public DateTime DATE_DUE { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public DateTime DT1 { get; set; }
            public DateTime DT2 { get; set; }
        }

        public class SavingsAccountTransactionRegister
        {
            public DateTime SYSDATE { get; set; }
            public string ACCTNO { get; set; }
            public string ACCTNAME { get; set; }
            public string HOLDCD { get; set; }
            public string HOLDTYPE { get; set; }
            public decimal HOLDAMT { get; set; }
            public decimal DEBIT { get; set; }
            public decimal CREDIT { get; set; }
            public DateTime HOLDDATE { get; set; }
            public string HOLDUSER { get; set; }
            public string HOLDRMKS { get; set; }
            public string POSTSTAT { get; set; }
            public DateTime POSTDATE { get; set; }
            public string POSTUSER { get; set; }
            public DateTime DT1 { get; set; }
            public DateTime DT2 { get; set; }
        }

        public class StaffPayment
        {
            public string KBCI_NO { get; set; }
            public string NAME { get; set; }
            public decimal P_BAL { get; set; }
            public decimal I_BAL { get; set; }
            public string PN_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public DateTime sysdate { get; set; }
        }

        public class SubsidiaryLoanLedger { }

        internal class SubsidiaryLoanLedgerHeader
        {
            public string SUBTITLE { get; set; }
            public string FULL_NAME { get; set; }
            public string DEPT { get; set; }
            public string OFF_TEL { get; set; }
            public string KBCI_NO { get; set; }
            public string FEBTC_SA { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public string PN_NO { get; set; }
            public decimal PRINCIPAL { get; set; }
            public DateTime DATE_DUE { get; set; }
            public decimal TERM { get; set; }
            public decimal AMORT_AMT { get; set; }
            public decimal RATE { get; set; }
            public string MOD_PAY { get; set; }
            public string REMARKS { get; set; }
            public DateTime DT1 { get; set; }
            public DateTime DT2 { get; set; }
        }

        internal class SubsidiaryLoanLedgerBody
        {
            public DateTime DATES { get; set; }
            public string REF { get; set; }
            public string RMK { get; set; }
            public Int64 LEDGER_ID { get; set; }
            public decimal AMOUNT_LOANED { get; set; }
            public decimal PRINCIPAL_PAID { get; set; }
            public decimal BALANCE { get; set; }
            public decimal INTEREST_PAID { get; set; }
            public decimal PENALTIES { get; set; }
            public decimal LRI { get; set; }
            public decimal OTHERS { get; set; }
        }

        public class TransactionSchedule
        {
            public string KBCI_NO { get; set; }
            public string PN_NO { get; set; }
            public string FULL_NAME { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal DR { get; set; }
            public decimal CR { get; set; }
            public string RMK { get; set; }
            public string ACCT_CODE { get; set; }
            public string ACCT_TYPE { get; set; }
            public decimal DAMT { get; set; }
            public decimal DSC { get; set; }
            public decimal DLRI { get; set; }
            public decimal DOTH { get; set; }
            public decimal DINT { get; set; }
            public decimal DTERP { get; set; }
            public decimal DTERI { get; set; }
            public decimal DTERO { get; set; }
            public decimal DPAYP { get; set; }
            public decimal DPAYI { get; set; }
            public decimal CAMT { get; set; }
            public decimal CSC { get; set; }
            public decimal CLRI { get; set; }
            public decimal COTH { get; set; }
            public decimal CINTS { get; set; }
            public decimal CTERP { get; set; }
            public decimal CTERI { get; set; }
            public decimal CTERO { get; set; }
            public decimal CPAYP { get; set; }
            public decimal CPAYI { get; set; }
            public DateTime SYSDATE { get; set; }
        }

    }

    namespace Lri
    {
        public class Collection
        {
            public DateTime DATE_FROM { get; set; }
            public DateTime DATE_TO { get; set; }
            public string KBCI_NO { get; set; }
            public string FULL_NAME { get; set; }
            public decimal XLRI { get; set; }
        }

        public class Deduction
        {
            public string KBCI_NO { get; set; }
            public string LNAME { get; set; }
            public string FNAME { get; set; }
            public string FEBTC_SA { get; set; }
            public decimal DIVIDEND { get; set; }
            public decimal REFUND { get; set; }
            public decimal TOTAL { get; set; }
            public decimal DEDUCTIONS { get; set; }
            public decimal GTOTAL { get; set; }
            public decimal FOR_LRI { get; set; }
        }

        public class Due
        {
            public DateTime DATE_FROM { get; set; }
            public DateTime DATE_TO { get; set; }
            public string PN_NO { get; set; }
            public string FULL_NAME { get; set; }
            public string LOAN_TYPE { get; set; }
            public DateTime DATE_GRANT { get; set; }
            public DateTime DATE_DUE { get; set; }
            public DateTime LRI_DUE { get; set; }
            public decimal LRI_DUE_P { get; set; }
            public string SAVINGS { get; set; }
        }
    }

    namespace Maintenance
    {
        public class LoansStatement { }

        internal class LoansStatementHeader
        {
            public string KBCI_NO { get; set; }
            public decimal TOTAL { get; set; }
            public decimal TOTAL_STL { get; set; }
            public decimal INT { get; set; }
            public decimal PEN { get; set; }
            public decimal SAV { get; set; }
            public decimal FIX { get; set; }
            public decimal LRI { get; set; }
            public string USER { get; set; }
            public DateTime SEP_DATE { get; set; }
            public DateTime PRE_DATE { get; set; }
            public string MARK { get; set; }
            public decimal NO_UPDATE { get; set; }
            public DateTime L_UPDATE { get; set; }
            public string NOTE { get; set; }
            public decimal ADV { get; set; }
            public string FULL_NAME { get; set; }
            public Int32 AGE { get; set; }
            public string SYSDATE { get; set; }
            public string MY_USER { get; set; }
            public string DEPT { get; set; }
            public string FEBTC_SA { get; set; }
            public string LABEL { get; set; }
            public decimal CTD_COLLATERAL_0 { get; set; }
            public decimal CTD_COLLATERAL_1 { get; set; }
            public DateTime B_DATE { get; set; }
        }

        internal class LoansStatementBody
        {
            public string LOAN_TYPE { get; set; }
            public decimal BALANCE { get; set; }
            public Boolean PD { get; set; }
        }

        public class LoansMonitoring
        {
            public DateTime ASODATE { get; set; }
            public string BORROWER { get; set; }
            public string ACCOUNT_NO { get; set; }
            public string DEPT { get; set; }
            public string OFF_TEL { get; set; }
            public DateTime B_DATE { get; set; }
            public decimal FD_AMOUNT { get; set; }
            public decimal ACCTOBAL { get; set; }
            public decimal CTD_AMOUNT { get; set; }
            public Int32 AGE { get; set; }
            public string LOAN_APPLIED { get; set; }
            public decimal AMOUNT { get; set; }
            public DateTime DATE_GRANTED { get; set; }
            public DateTime DATE_DUE { get; set; }
            public string TF { get; set; }
            public decimal DIRECT { get; set; }
            public decimal PAYROLL { get; set; }
            public decimal BALANCE { get; set; }
            public decimal ARREARS { get; set; }
            public string PN_NO { get; set; }
            public string TYP { get; set; }
            public decimal RATE { get; set; }
            public decimal TERM { get; set; }
            public string FREQ { get; set; }
            public decimal ARREAR_P { get; set; }
            public decimal ARREAR_I { get; set; }
            public decimal ARREAR_OTH { get; set; }
            public string MEM_DATE { get; set; }
            public string MEM_TERM { get; set; }
            public string PROCESSOR { get; set; }
            public string REPORT_DATE { get; set; }
        }

        public class ProcessingSheet : LoansMonitoring { }

        public class OutstandingBalance : LoansMonitoring { }

        public class OutstandingBalanceAsOf : LoansMonitoring { }        
    }

    namespace Members
    {
        public class List
        {
            public string KBCI_NO { get; set; }
            public string FULL_NAME { get; set; }
            public string MEM_CODE { get; set; }
            public string MEM_STAT { get; set; }
            public bool DORI { get; set; }
            public string REA_DORI { get; set; }
            public string FEBTC_SA { get; set; }
            public string CB_EMPNO { get; set; }
            public string REGIONS { get; set; }
            public string OFF_TEL { get; set; }
            public decimal SAL_BAS { get; set; }
            public string SEX { get; set; }
            public string CIV_STAT { get; set; }
            public decimal NO_DEPEND { get; set; }
            public DateTime B_DATE { get; set; }
            public string SP_NAME { get; set; }
            public string MEM_ADDR { get; set; }
            public DateTime CB_HIRE { get; set; }
            public string DEPT { get; set; }
            public string RES_TEL { get; set; }
            public decimal SAL_ALL { get; set; }
            public string SP_EMPLOY { get; set; }
            public string POSITION { get; set; }
            public decimal OTH_INC { get; set; }
            public string SP_CBEMPNO { get; set; }
            public string SP_OFFTEL { get; set; }
            public decimal SP_SALARY { get; set; }
            public DateTime SYSDATE { get; set; }
        }
    }

    namespace PaymentOrder
    {
        public class Loans { }

        internal class LoansHeader
        {
            public string FULL_NAME { get; set; }
            public string KBCI_NO { get; set; }
            public string FEBTC_SA { get; set; }
            public string COLLATERAL { get; set; }
            public string LOAN_TYPE { get; set; }
            public string LOAN_STAT { get; set; }
            public string PN_NO { get; set; }
            public DateTime DATE_GRANT { get; set; }
            public DateTime DATE_DUE { get; set; }
            public decimal RATE { get; set; }
            public DateTime XLASTD { get; set; }
        }

        internal class LoansBody
        {
            public string CODE { get; set; }
            public string DETAIL { get; set; }
            public decimal AMOUNT { get; set; }
        }

        public class Lri
        {
            public string FULL_NAME { get; set; }
            public string KBCI_NO { get; set; }
            public string FEBTC_SA { get; set; }
            public string COLLATERAL { get; set; }
            public bool PD { get; set; }
            public string LOAN_TYPE { get; set; }
            public string PN_NO { get; set; }
            public decimal PRINCIPAL { get; set; }
            public DateTime DATE_GRANT { get; set; }
            public DateTime DATE_DUE { get; set; }
            public decimal RATE { get; set; }
            public DateTime LRI_DUE { get; set; }
            public decimal LRI_DUE_P { get; set; }
            public decimal XBPRIN { get; set; }
        }
    }

    namespace Payroll
    {
        public class AdvancePayments
        {
            public bool PD { get; set; }
            public DateTime DATE_FROM { get; set; }
            public DateTime DATE_TO { get; set; }
            public DateTime SYSDATE { get; set; }
            public string KBCI_NO { get; set; }
            public string PN_NO { get; set; }
            public string LOAN_TYPE { get; set; }
            public string MEMBER { get; set; }
            public decimal AMOUNT { get; set; }
            public DateTime ADD_DATE { get; set; }
            public string REMARKS { get; set; }
            public string ACCTNO { get; set; }
            public string STATUS { get; set; }
        }

        public class Advice
        {
            public string EMPNO { get; set; }
            public string WAGE_TYPE { get; set; }
            public string BEGDA { get; set; }
            public string ENDDA { get; set; }
            public decimal AMOUNT { get; set; }
        }

        public class Stop
        {
            public string EMPNO { get; set; }
            public string WAGE_TYPE { get; set; }
            public string ENDDA { get; set; }
        }

        public class PayrollDeductionRegisterDetails
        {
            public bool PD { get; set; }
            public string PAYDATE { get; set; }
            public string TITLE { get; set; }
            public string LOAN_TYPE { get; set; }
            public string LNAME { get; set; }
            public string KBCI_NO { get; set; }
            public string EMPNAME { get; set; }
            public decimal DEDUCTION { get; set; }
            public string PN_NO { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal INTEREST { get; set; }
            public decimal ARR_PRI { get; set; }
            public decimal ARR_INT { get; set; }
            public decimal ARREARS { get; set; }
            public decimal ADVANCE { get; set; }
            public string EMPNO { get; set; }
            public Int32 RANKING { get; set; }
        }

        public class PayrollDeductionRegisterSummary
        {
            public bool PD { get; set; }
            public string PAYDATE { get; set; }
            public string TITLE { get; set; }
            public string LOAN_TYPE { get; set; }
            public decimal MONTHLY_DEDN { get; set; }
            public decimal PRINCIPAL { get; set; }
            public decimal INTEREST { get; set; }
            public decimal PENALTIES { get; set; }
            public decimal REFUND { get; set; }
            public decimal FD_SA { get; set; }
            public decimal MA { get; set; }
            public decimal AP { get; set; }
        }

        public class NoDeductionRegister
        {
            public string PAYDATE { get; set; }
            public string LOAN_TYPE { get; set; }
            public string KBCI_NO { get; set; }
            public string EMPNO { get; set; }
            public string EMPNAME { get; set; }
            public decimal DEDUCTION { get; set; }
            public string PN_NO { get; set; }
            public decimal AMORT_PRI { get; set; }
            public decimal AMORT_INT { get; set; }
            public decimal ARR_PRI { get; set; }
            public decimal ARR_INT { get; set; }
            public decimal ARREARS { get; set; }
            public string LOAN_STAT { get; set; }
        }
    }

    namespace Voucher
    {
        public class LoansPayment { }

        internal class LoansPaymentHeader
        {
            public string MEMBERNAME { get; set; }
            public string XOR_NO { get; set; }
            public DateTime SYSDATE { get; set; }
            public string TIME { get; set; }
            public string MY_USER { get; set; }
            public string PAYREM { get; set; }
            public DateTime PAYDATE { get; set; }
            public string LOAN_TYPE { get; set; }
            public string PN_NO { get; set; }
            public string TITLE { get; set; }
        }

        internal class LoansPaymentBody
        {
            public decimal DEBIT { get; set; }
            public string REMARKS { get; set; }
            public decimal CREDIT { get; set; }
        }

        public class PayrollDeduction
        {
            public string MY_USER { get; set; }
            public DateTime SYSDATE { get; set; }
            public string VNO { get; set; }
            public string VNO_TXT { get; set; }
            public decimal VNO_TOT { get; set; }
            public DateTime PAYROLLDATE { get; set; }
            public string DETAIL { get; set; }
            public decimal VALUE { get; set; }
        }

        public class Release 
        {
            public string FULL_NAME { get; set; }
            public string PN_NO { get; set; }
            public string KBCI_NO { get; set; }
            public DateTime CHKNO_DATE { get; set; }
            public string FEBTC_SA { get; set; }
            public Int32 L_EXT { get; set; }
            public string CHKNO { get; set; }
            public string CHKNO_BANK { get; set; }
            public string MY_USER { get; set; }
            public decimal TOTAL { get; set; }
            public string TOTAL_WORDS { get; set; }
            public string LOAN_TYPE { get; set; }
            public string LED_TYPE { get; set; }
            public string TERM { get; set; }
            public decimal ADV_INTE { get; set; }
            public decimal RATE { get; set; }
            public decimal AFT_INTE { get; set; }
            public decimal AMORT_AMT { get; set; }
            public string DATE_DUE { get; set; }
            public string FREQ { get; set; }
            public string COMAKER1 { get; set; }
            public string COMAKER2 { get; set; }
            public string COLLATERAL { get; set; }
            public DateTime SYSDATE { get; set; }
            public Int32 FLAG { get; set; }
            public decimal DR { get; set; }
            public decimal CR { get; set; }
            public string LED_DESC { get; set; }
        }
    }
}
