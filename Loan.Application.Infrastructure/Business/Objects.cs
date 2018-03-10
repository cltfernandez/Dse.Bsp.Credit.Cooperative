using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using Loan.Application.Infrastructure.Controls.PropertyEditor.Converters;
using Loan.Application.Infrastructure.Controls.PropertyEditor.Editors;
using Loan.Application.Infrastructure.Enumerations.DropDownItems;
using Loan.Application.Infrastructure.Enumerations.System;

namespace Loan.Application.Infrastructure.Business.Objects
{
    #region Charge

    public class Charge
    {   
        [DisplayName("PN No.")]
        public string OPN_NO { get { return m_OPN_NO; } set { m_OPN_NO = value;} }
        public string OLOAN_TYPE { get; set; }
        [DisplayName("Doc Type")]
        public DoubleEntry ODOX_TYPE { get; set; }
        public string OREF { get; set; }
        public ChargeRemarks OACCT_TYPE { get; set; }
        public AccountCode OACCT_CODE { get; set; }
        public decimal ODR { get; set; }
        public decimal OCR { get; set; }
        public string ORMK { get; set; }
        public string OBANK { get; set; }
        public string OCHKNO { get; set; }
        public string OACCTNO { get; set; }
        public string OACCTNM { get; set; }
        [DisplayName("Amount")]
        public decimal AMOUNT { get; set; }
        [DisplayName("Remarks")]
        public ChargeRemarks REMARKS { get; set; }
        private string m_OPN_NO = "This Loan";
        private bool ShouldSerializeOPN_NO() { return false; }
        private bool ShouldSerializeODOX_TYPE() { return false; }
        private bool ShouldSerializeAMOUNT() { return false; }
        private bool ShouldSerializeREMARKS() { return false; }
    }

    #endregion

    #region Collaterals

    public class Collaterals
    {
        public string CTD_NO { get; set; }
        public string KBCI_NO { get; set; }
        [TypeConverter(typeof(RateConverter))]
        public decimal RATE { get; set; }
        public DateTime DUE_DATE { get; set; }
        public decimal PRINCIPAL { get; set; }
        public string COLLATERAL { get; set; }
        public string STATUS { get; set; }
    }

    #endregion

    #region Comaker

    public class Comaker
    {
        public string PN_NO { get; set; }
        public string COMAKER1 { get; set; }
        public string COMAKER2 { get; set; }
        public DateTime ADD_DATE { get; set; }
        public string ADD_USER { get; set; }
    }

    #endregion

    #region Control
    public class Control
    {

        public string MAPP_NO { get; set; }
        public DateTime LAPP_DATE { get; set; }
        public decimal LAPP_NO { get; set; }
        public string KBCI_NO { get; set; }
        public decimal PN_NO { get; set; }
        public DateTime PAY_DAY { get; set; }
        public DateTime SYSDATE { get; set; }
        public DateTime ADMDATE { get; set; }
        public decimal L_DM { get; set; }
        public decimal L_CM { get; set; }
        public DateTime ADD_DATE { get; set; }
        public DateTime CHG_DATE { get; set; }
        public string USER { get; set; }
        public decimal CEILING { get; set; }
        public decimal TD_CVNO { get; set; }
        public decimal SDRATE { get; set; }
        public DateTime PROC { get; set; }
        public decimal FDMIN { get; set; }
        public decimal FDMAX { get; set; }
        public decimal LRIMAX { get; set; }
        public bool FD_REP { get; set; }
        public bool REP1 { get; set; }
        public bool REP2 { get; set; }
        public bool REP3 { get; set; }
        public bool REP4 { get; set; }
        public bool REP5 { get; set; }
        public bool TD_REP { get; set; }
        public DateTime APRUN_DATE { get; set; }
        public DateTime ARRUN_DATA { get; set; }
        public DateTime RUN_DATE { get; set; }
        public string ACCTBR { get; set; }
        public decimal ACCTSEQ { get; set; }
        public decimal VOUCHER { get; set; }
        public bool CLOSE { get; set; }
        public decimal PMAXL { get; set; }
        public decimal LMAXL { get; set; }
        public bool BINIT { get; set; }
        public bool BPOST { get; set; }
        public decimal CLR_ONUS { get; set; }
        public decimal CLR_LOCAL { get; set; }
        public decimal CLR_REG { get; set; }
        public decimal MINBAL { get; set; }
        public decimal DBDORMANT { get; set; }
        public bool EOM_FLAG { get; set; }
        public bool EOQ_FLAG { get; set; }
        public bool EOY_FLAG { get; set; }
        public bool OTC_FLAG { get; set; }
        public bool EOD_FLAG { get; set; }
        public string P_PRINT { get; set; }
        public string R_PRINT { get; set; }
        public DateTime BFL_DATE_DUE { get; set; }
    }
    #endregion

    #region Deductions

    public class Deductions
    {
        //PN_TAG - PN_TAG
        //PN_NO - PN_NO
        //LOAN_TYPE - LOAN_TYPE
        //LOAN_STAT - LOAN_STAT
        //DATE_GRANT - GRANTED
        //DATE_DUE - DUE
        //KBCI_NO - KBCI_NO
        //PRINCIPAL - LOAN_AMOUNT
        //OUTSBAL - BALANCE
        //RENEW - RENEW
        //ARREARS - ARREARS
        //PAY_TAG - DEDUCT
        //PAY_AMT - DEDUCTION
        //LRI_DUE - LRI_DUE
        //YEARLY_LRI - YEARLY_LRI
        public bool PN_TAG { get; set; }
        public string PN_NO { get; set; }
        public LoanType LOAN_TYPE { get; set; }
        public LoanStatus LOAN_STAT { get; set; }
        public decimal OUTSBAL { get; set; }
        public DateTime DATE_GRANT { get; set; }
        public DateTime DATE_DUE { get; set; }
        public string KBCI_NO { get; set; }
        public decimal PRINCIPAL { get; set; }
        public bool RENEW { get; set; }
        public decimal ARREARS { get; set; }
        public PayTag PAY_TAG { get; set; }
        public decimal PAY_AMT { get; set; }
        public decimal LRI_DUE { get; set; }
        public decimal YEARLY_LRI { get; set; }
    }

    #endregion

    #region FixedDeposit

    public class FixedDeposit
    {
        public string KBCI_NO { get; set; }
        public string TRAN_CODE { get; set; }
        public DateTime DATE { get; set; }
        public string REF { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal BALANCE { get; set; }
        private decimal m_BALANCE;
        public string RMK { get; set; }
        public DateTime ADD_DATE { get; set; }
        public string USER { get; set; }
        public decimal LPOSTED { get; set; }
        public string DRCR { get; set; }
        public string BANK_CODE { get; set; }
        public string CHECKNO { get; set; }
        public bool TPOSTED { get; set; }
        public bool TREVERSED { get; set; }
    }

    #endregion

    #region Ledger

    public class Ledger
    {
        public string PN_NO { get; set; }
        public DateTime DATE { get; set; }
        public DoubleEntry DOX_TYPE { get; set; }
        public string REF { get; set; }
        public AccountType ACCT_TYPE { get; set; }
        public AccountCode ACCT_CODE { get; set; }
        public decimal BEGBAL { get; set; }
        public decimal DR { get; set; }
        public decimal CR { get; set; }
        public decimal ENDBAL { get; set; }
        public string RMK { get; set; }
        public DateTime ADD_DATE { get; set; }
        public string USER { get; set; }
    }

    #endregion

    #region Loan

    [DefaultProperty("PN_NO")]
    public class Loan
    {

        [Browsable(false)]
        public Int64 LOANS_ID { get; set; }
        public string PN_NO { get; set; }
        public string KBCI_NO { get; set; }
        public LoanType LOAN_TYPE { get; set; }
        public LoanStatus LOAN_STAT { get; set; }
        public DateTime APP_DATE { get; set; }
        public decimal APP_NO { get; set; }
        public DateTime DATE_GRANT { get; set; }
        public string BY_WHOM { get; set; }
        public DateTime DATE_DUE { get; set; }
        public string CHKNO_BANK { get; set; }
        public string CHKNO { get; set; }
        public decimal CHKNO_AMT { get; set; }
        public DateTime CHKNO_DATE { get; set; }
        public string CHKNO_RELS { get; set; }
        public DateTime CHKNO_ACK { get; set; }
        public PayMode MOD_PAY { get; set; }
        public decimal AMORT_AMT { get; set; }
        public DateTime PAY_START { get; set; }
        [TypeConverter(typeof(RateConverter))]
        public decimal RATE { get; set; }
        public decimal TERM { get; set; }
        public Frequency FREQ { get; set; }
        public decimal PRINCIPAL { get; set; }
        public LedgerType LED_TYPE { get; set; }
        public decimal ADV_INTE { get; set; }
        public decimal AFT_INTE { get; set; }
        public decimal ACCU_PAYP { get; set; }
        public decimal YTD_I { get; set; }
        public decimal ARREAR_I { get; set; }
        public decimal ARREAR_P { get; set; }
        public decimal ARREAR_OTH { get; set; }
        public DateTime ARREAR_AS { get; set; }
        public string COLLATERAL { get; set; }
        public string DED_BAL { get; set; }
        public DateTime ADD_DATE { get; set; }
        [ReadOnly(true)]
        public DateTime CHG_DATE { get; set; }
        [ReadOnly(true)]
        public string USER { get; set; }
        public decimal P_BAL { get; set; }
        public decimal I_BAL { get; set; }
        public decimal O_BAL { get; set; }
        public bool REC_STAT { get; set; }
        public bool RENEW { get; set; }
        public decimal ADVANCE { get; set; }
        public bool LRI_IND { get; set; }
        public DateTime NDUE { get; set; }
        public bool L_EXT { get; set; }
        public bool PD { get; set; }
        public decimal LRI_DUE { get; set; }

        private bool ShouldSerializePN_NO()
        {
            return false;
        }
        private bool ShouldSerializeKBCI_NO()
        {
            return false;
        }
        private bool ShouldSerializeAPP_DATE()
        {
            return false;
        }
        private bool ShouldSerializeAPP_NO()
        {
            return false;
        }
        private bool ShouldSerializeDATE_GRANT()
        {
            return false;
        }
        private bool ShouldSerializeBY_WHOM()
        {
            return false;
        }
        private bool ShouldSerializeDATE_DUE()
        {
            return false;
        }
        private bool ShouldSerializeCHKNO_BANK()
        {
            return false;
        }
        private bool ShouldSerializeCHKNO()
        {
            return false;
        }
        private bool ShouldSerializeCHKNO_AMT()
        {
            return false;
        }
        private bool ShouldSerializeCHKNO_DATE()
        {
            return false;
        }
        private bool ShouldSerializeCHKNO_RELS()
        {
            return false;
        }
        private bool ShouldSerializeCHKNO_ACK()
        {
            return false;
        }
        private bool ShouldSerializeMOD_PAY()
        {
            return false;
        }
        private bool ShouldSerializeAMORT_AMT()
        {
            return false;
        }
        private bool ShouldSerializePAY_START()
        {
            return false;
        }
        private bool ShouldSerializeRATE()
        {
            return false;
        }
        private bool ShouldSerializeTERM()
        {
            return false;
        }
        private bool ShouldSerializeFREQ()
        {
            return false;
        }
        private bool ShouldSerializePRINCIPAL()
        {
            return false;
        }
        private bool ShouldSerializeLED_TYPE()
        {
            return false;
        }
        private bool ShouldSerializeADV_INTE()
        {
            return false;
        }
        private bool ShouldSerializeAFT_INTE()
        {
            return false;
        }
        private bool ShouldSerializeACCU_PAYP()
        {
            return false;
        }
        private bool ShouldSerializeYTD_I()
        {
            return false;
        }
        private bool ShouldSerializeLOAN_TYPE()
        {
            return false;
        }
        private bool ShouldSerializeLOAN_STAT()
        {
            return false;
        }
        private bool ShouldSerializeARREAR_I()
        {
            return false;
        }
        private bool ShouldSerializeARREAR_P()
        {
            return false;
        }
        private bool ShouldSerializeARREAR_OTH()
        {
            return false;
        }
        private bool ShouldSerializeARREAR_AS()
        {
            return false;
        }
        private bool ShouldSerializeCOLLATERAL()
        {
            return false;
        }
        private bool ShouldSerializeDED_BAL()
        {
            return false;
        }
        private bool ShouldSerializeADD_DATE()
        {
            return false;
        }
        private bool ShouldSerializeCHG_DATE()
        {
            return false;
        }
        private bool ShouldSerializeUSER()
        {
            return false;
        }
        private bool ShouldSerializeP_BAL()
        {
            return false;
        }
        private bool ShouldSerializeI_BAL()
        {
            return false;
        }
        private bool ShouldSerializeO_BAL()
        {
            return false;
        }
        private bool ShouldSerializeREC_STAT()
        {
            return false;
        }
        private bool ShouldSerializeRENEW()
        {
            return false;
        }
        private bool ShouldSerializeADVANCE()
        {
            return false;
        }
        private bool ShouldSerializeLRI_IND()
        {
            return false;
        }
        private bool ShouldSerializeNDUE()
        {
            return false;
        }
        private bool ShouldSerializeL_EXT()
        {
            return false;
        }
        private bool ShouldSerializePD()
        {
            return false;
        }
        private bool ShouldSerializeLRI_DUE()
        {
            return false;
        }

    }

    #endregion

    #region LoanHold

    public class LoanHold
    {

        public string ACCTNO { get; set; }
        public string HOLDCD { get; set; }
        public string HOLDTYPE { get; set; }
        public decimal HOLDAMT { get; set; }
        public DateTime HOLDDATE { get; set; }
        public string HOLDUSER { get; set; }
        public string HOLDRMKS { get; set; }
        public string POSTSTAT { get; set; }
        public DateTime POSTDATE { get; set; }
        public string POSTUSER { get; set; }
    }

    #endregion

    #region LoanInput

    public class LoanInput
    {

        [Editor(typeof(KbciNoEditor), typeof(UITypeEditor))]
        [DisplayName("KBCI No.")]
        public string KBCI_NO { get; set; }
        
        [ReadOnly(true)]
        [DisplayName("Full Name")]
        public string FULL_NAME { get; set; }
        
        [DisplayName("Loan Type")]
        public LoanType LOAN_TYPE { get; set; }
        
        [DisplayName("Payment Mode")]
        public PayMode MOD_PAY { get; set; }
        
        [DisplayName("Ledger Type")]
        public LedgerType LED_TYPE { get; set; }
        
        [DisplayName("Frequency")]
        public Frequency FREQ { get; set; }
        
        [DisplayName("Loan Amount")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal PRINCIPAL { get; set; }
        
        [DisplayName("Term")]
        public decimal TERM { get; set; }
        
        [DisplayName("Rate")]
        [TypeConverter(typeof(RateConverter))]
        public decimal RATE { get; set; }
        
        [DisplayName("Exempt from LRI")]
        public bool LRI_IND { get; set; }
        
        [DisplayName("Bank")]
        public string CHKNO_BANK { get; set; }
        
        [DisplayName("Check No.")]
        public string CHKNO { get; set; }
        
        [Editor(typeof(KbciNoEditor), typeof(UITypeEditor))]
        [DisplayName("Co-Maker 1")]
        public string COMAKER1 { get; set; }
        
        [ReadOnly(true)]
        [DisplayName("Co-Maker 1 Name")]
        public string COMAKER_NAME1 { get; set; }
        
        [Editor(typeof(KbciNoEditor), typeof(UITypeEditor))]
        [DisplayName("Co-Maker 2")]
        public string COMAKER2 { get; set; }
        
        [ReadOnly(true)]
        [DisplayName("Co-Maker 2 Name")]
        public string COMAKER_NAME2 { get; set; }
        
        [DisplayName("Remarks")]
        public string COLLATERAL { get; set; }
        
        [DisplayName("Deduct Miscellaneous Liabilities")]
        public bool MISC { get; set; }
        
        [Browsable(false)]
        public bool xrenew { get { return m_xrenew; } set { m_xrenew = value; } }
        private bool m_xrenew = false;
        
        [Browsable(false)]
        public bool xrenew2 { get { return m_xrenew2; } set { m_xrenew2 = value; } }
        private bool m_xrenew2 = false;
        
        [Browsable(false)]
        public bool xrenewSTL { get { return m_xrenewSTL; } set { m_xrenewSTL = value; } }
        private bool m_xrenewSTL = false;

        [Browsable(false)]
        public bool resigned { get { return m_resigned; } set { m_resigned = value; } }
        private bool m_resigned = false;

        [Browsable(false)]
        public decimal xplnamt { get; set; }
        
        [Browsable(false)]
        public decimal xtotamt { get; set; }

        [Browsable(false)]
        public Int32 reyt { get; set; }

        [Browsable(false)]
        public decimal mterm { get; set; }

        [Browsable(false)]
        public Int32 month { get; set; }

        private bool ShouldSerializeKBCI_NO() { return false; }
        private bool ShouldSerializeFULL_NAME() { return false; }
        private bool ShouldSerializeLOAN_TYPE() { return false; }
        private bool ShouldSerializeMOD_PAY() { return false; }
        private bool ShouldSerializeLED_TYPE() { return false; }
        private bool ShouldSerializeFREQ() { return false; }
        private bool ShouldSerializePRINCIPAL() { return false; }
        private bool ShouldSerializeTERM() { return false; }
        private bool ShouldSerializeRATE() { return false; }
        private bool ShouldSerializeLRI_IND() { return false; }
        private bool ShouldSerializeCHKNO_BANK() { return false; }
        private bool ShouldSerializeCHKNO() { return false; }
        private bool ShouldSerializeCOMAKER1() { return false; }
        private bool ShouldSerializeCOMAKER_NAME1() { return false; }
        private bool ShouldSerializeCOMAKER2() { return false; }
        private bool ShouldSerializeCOMAKER_NAME2() { return false; }
        private bool ShouldSerializeCOLLATERAL() { return false; }
        private bool ShouldSerializeMISC() { return false; }

    }

    #endregion

    #region LoanList

    public class LoanList
    {
        public string PN_NO { get; set; }
    }

    #endregion

    #region LoanReleaseInsurance

    public class LoanReleaseInsurance
    {
        public string PN_NO { get; set; }
        public string KBCI_NO { get; set; }
        public DateTime LRI_DUE { get; set; }
        public DateTime LRI_BALDA { get; set; }
        public decimal LOAN_BAL { get; set; }
        public decimal LRI_DUE_C { get; set; }
        public decimal LRI_DUE_P { get; set; }
        public decimal LRI_DUE_Y { get; set; }
    }

    #endregion

    #region LoanTypeDetail
    public class LoanTypeDetail
    {
        public Int64 LOAN_TYPE_ID { get; set; }
        public Int64 PARAM_ID { get; set; }
        public LoanType LOAN_TYPE { get; set; }
        public string LOAN_DESC { get; set; }
        public string CODE5 { get; set; }
        public string MBAT { get; set; }
        public decimal TERM { get; set; }
        public string FREQ { get; set; }
        public decimal RATE { get; set; }
        public decimal MAX { get; set; }
        public decimal MIN { get; set; }
    }
    #endregion

    #region Member

    public class Member
    {
        public Int64 KBCI_ID { get; set; }
        public string KBCI_NO { get; set; }
        public string LNAME { get; set; }
        public string FNAME { get; set; }
        public string MI { get; set; }
        public MembershipCode MEM_CODE { get; set; }
        public MembershipStatus MEM_STAT { get; set; }
        public DateTime MEM_DATE { get; set; }
        public string BY_WHOM { get; set; }
        public string CB_EMPNO { get; set; }
        public DateTime CB_HIRE { get; set; }
        public string DEPT { get; set; }
        public string REGION { get; set; }
        public string OFF_TEL { get; set; }
        public bool DORI { get; set; }
        public string REA_DORI { get; set; }
        public Sex SEX { get; set; }
        public DateTime B_DATE { get; set; }
        public CivilStatus CIV_STAT { get; set; }
        public string MEM_ADDR { get; set; }
        public string RES_TEL { get; set; }
        public string POSITION { get; set; }
        public decimal SAL_BAS { get; set; }
        public decimal SAL_ALL { get; set; }
        public decimal OTH_INC { get; set; }
        public string FEBTC_SA { get; set; }
        public string FEBTC_CA { get; set; }
        public string FD_CERTNO { get; set; }
        public DateTime FD_DATE { get; set; }
        public decimal AP_AMOUNT { get; set; }
        public decimal AR_AMOUNT { get; set; }
        public decimal FD_AMOUNT { get; set; }
        public decimal SD_AMOUNT { get; set; }
        public decimal TD_AMOUNT { get; set; }
        public decimal OTH_AMOUNT { get; set; }
        public decimal YTD_DIVAMT { get; set; }
        public decimal YTD_LRI { get; set; }
        public string REM_PROP { get; set; }
        public decimal REM_VALUE { get; set; }
        public decimal NO_DEPEND { get; set; }
        public string SP_NAME { get; set; }
        public string SP_EMPLOY { get; set; }
        public string SP_OFFTEL { get; set; }
        public string SP_CBEMPNO { get; set; }
        public string SP_KBCINO { get; set; }
        public decimal SP_SALARY { get; set; }
        public decimal APRUN_AMT { get; set; }
        public decimal ARRUN_AMT { get; set; }
        public decimal RUN_AMT { get; set; }
        public DateTime ADD_DATE { get; set; }
        public DateTime CHG_DATE { get; set; }
        public string USER { get; set; }
        public bool REC_STAT { get; set; }
        public decimal FD_CNTR { get; set; }
    }

    #endregion

    #region MemberList

    public class MemberList
    {

        public string KBCI_NO { get; set; }
        public string FULL_NAME { get; set; }
    }

    #endregion

    #region MonthlyDeduction
    public class MonthlyDeduction
    {
        public string EMPNO { get; set; }
        public string KBCI_NO { get; set; }
        public string NAME { get; set; }
        public string PN_NO { get; set; }
        public string LOAN_TYPE { get; set; }
        public decimal AMORT_PRI { get; set; }
        public decimal AMORT_INT { get; set; }
        public decimal DEDUCTION { get; set; }
        public decimal PRINCIPAL { get; set; }
        public decimal INTEREST { get; set; }
        public decimal ARREARS { get; set; }
        public decimal ADVANCE { get; set; }
        public DateTime DATE { get; set; }
        public string USER { get; set; }
        public decimal ARR_PRI { get; set; }
        public decimal ARR_INT { get; set; }
        public decimal CODE5 { get; set; }
        public bool PD { get; set; }
    }

    public class OffCycleDeduction
    {
        public string EMPNO { get; set; }
        public string KBCI_NO { get; set; }
        public string NAME { get; set; }
        public string PN_NO { get; set; }
        public string LOAN_TYPE { get; set; }
        public decimal AMORT_PRI { get; set; }
        public decimal AMORT_INT { get; set; }
        public decimal DEDUCTION { get; set; }
        public decimal PRINCIPAL { get; set; }
        public decimal INTEREST { get; set; }
        public decimal ARREARS { get; set; }
        public decimal ADVANCE { get; set; }
        public DateTime DATE { get; set; }
        public string USER { get; set; }
        public decimal ARR_PRI { get; set; }
        public decimal ARR_INT { get; set; }
        public decimal CODE5 { get; set; }
        public bool PD { get; set; }
        public string PAYCODE { get; set; }
    }
    #endregion

    #region NetProceeds
    public class NetProceeds
    {

        public NetProceeds(string account, decimal debit, decimal credit)
        {
            ACCOUNT = account;
            DEBIT = debit;
            CREDIT = credit;
        }

        public decimal DEBIT { get; set; }
        public string ACCOUNT { get; set; }
        public decimal CREDIT { get; set; }
    }
    #endregion

    #region RLoanReleaseInsurance

    public class RLoanReleaseInsurance : LoanReleaseInsurance
    {
    }

    #endregion

    #region SavingsDeposit

    public class SavingsDeposit
    {
        public string KBCI_NO { get; set; }
        public string FEBTC_SA { get; set; }
        public string TRAN_CODE { get; set; }
        public DateTime DATE { get; set; }
        public string REF { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal BALANCE { get; set; }
        public string RMK { get; set; }
        public DateTime ADD_DATE { get; set; }
        public string USER { get; set; }
    }

    #endregion

    #region SavingsControl

    public class SavingsControl
    {

        public DateTime SYSDATE { get; set; }
        public DateTime ADMDATE { get; set; }
        public DateTime ADD_DATE { get; set; }
        public DateTime CHG_DATE { get; set; }
        public string USER { get; set; }
        public decimal SDRATE { get; set; }
        public string ACCTBR { get; set; }
        public decimal ACCTSEQ { get; set; }
        public decimal PMAXL { get; set; }
        public decimal LMAXL { get; set; }
        public bool BINIT { get; set; }
        public bool BPOST { get; set; }
        public decimal CLR_ONUS { get; set; }
        public decimal CLR_LOCAL { get; set; }
        public decimal CLR_REG { get; set; }
        public decimal MINBAL { get; set; }
        public decimal DBDORMANT { get; set; }
        public bool EOM_FLAG { get; set; }
        public bool EOQ_FLAG { get; set; }
        public bool EOY_FLAG { get; set; }
        public bool OTC_FLAG { get; set; }
        public bool EOD_FLAG { get; set; }
        public string P_PRINT { get; set; }
        public string R_PRINT { get; set; }
    }

    #endregion

    #region SavingsDepositMaster

    public class SavingsDepositMaster
    {

        public string ACCTNO { get; set; }
        public string KBCI_NO { get; set; }
        public string CBEMPNO { get; set; }
        public string ACCTNAME { get; set; }
        public string ACCTADDR1 { get; set; }
        public string ACCTADDR2 { get; set; }
        public string ACCTSNAME { get; set; }
        public string ACCTPHONE1 { get; set; }
        public string ACCTPHONE2 { get; set; }
        public string ACCTDTYPE { get; set; }
        public string ACCTCCODE { get; set; }
        public string ACCTICODE { get; set; }
        public string ACCTATX { get; set; }
        public string ACCTOTH1 { get; set; }
        public string ACCTOTH2 { get; set; }
        public decimal ACCTMAINT { get; set; }
        public decimal ACCTIDEP { get; set; }
        public decimal ACCTPBAL { get; set; }
        public decimal ACCTLBAL { get; set; }
        public decimal ACCTOBAL { get; set; }
        public decimal ACCTABAL { get; set; }
        public decimal ACCTFLOATS { get; set; }
        public string ACCTTEX { get; set; }
        public string ACCTACLO { get; set; }
        public string ACCTWMIN { get; set; }
        public string ACCTWSC { get; set; }
        public string ACCTWINT { get; set; }
        public string ACCTSTAT { get; set; }
        public decimal PLINE { get; set; }
        public decimal LLINE { get; set; }
        public decimal UNPOSTED { get; set; }
        public decimal HOLDOUT { get; set; }
        public decimal LSEQ { get; set; }
        public DateTime LTRANDATE { get; set; }
        public DateTime ADD_DATE { get; set; }
        public DateTime CHG_DATE { get; set; }
        public string UPD_USER { get; set; }
        public bool REC_STAT { get; set; }
    }

    #endregion

    #region User

    public class User
    {
        //public Int64 USER_ID { get; set; }
        public string USERNAME { get; set; }
        public string USERPASS { get; set; }
        public AccessLevels LEVEL { get; set; }
        //public DateTime ADD_DATE { get; set; }
        //public DateTime CHG_DATE { get; set; }
        //public string NAME { get; set; }
        //public string POSITION { get; set; }
        //public string USER { get; set; }
        //public bool LOGGED { get; set; }
    }

    #endregion

}
