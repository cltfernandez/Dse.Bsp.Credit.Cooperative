using System;
using System.ComponentModel;
using Loan.Application.Infrastructure.Controls.PropertyEditor.Converters;
using Loan.Application.Infrastructure.Enumerations.DropDownItems;

namespace Loan.Application.Domain.Loans
{
    [DefaultProperty("PN_NO")]
    public class Loan
    {
        [Browsable(false)]
        public Int64 LOANS_ID { get; set; }
        
        [Category("(Main)")]
        public string PN_NO { get; set; }

        [Category("(Main)")]
        public string KBCI_NO { get; set; }

        [Category("Dates")]
        public DateTime APP_DATE { get; set; }

        [Category("Details")]
        public decimal APP_NO { get; set; }

        [Category("Dates")]
        public DateTime DATE_GRANT { get; set; }

        [Category("Details")]
        public string BY_WHOM { get; set; }

        [Category("Dates")]
        public DateTime DATE_DUE { get; set; }
        
        [Category("Details")]
        public string CHKNO_BANK { get; set; }

        [Category("Details")]
        public string CHKNO { get; set; }

        [Category("Amounts")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal CHKNO_AMT { get; set; }

        [Category("Dates")]
        public DateTime CHKNO_DATE { get; set; }

        [Category("Details")]
        public string CHKNO_RELS { get; set; }

        [Category("Dates")]
        public DateTime CHKNO_ACK { get; set; }

        [Category("Details")]
        public PayMode MOD_PAY { get; set; }

        [Category("(Main)")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal AMORT_AMT { get; set; }

        [Category("Dates")]
        public DateTime PAY_START { get; set; }

        [Category("(Main)")]
        [TypeConverter(typeof(RateConverter))]
        public decimal RATE { get; set; }

        [Category("(Main)")]
        public decimal TERM { get; set; }
        
        [Category("(Main)")]
        public Frequency FREQ { get; set; }

        [Category("(Main)")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal PRINCIPAL { get; set; }

        [Category("(Main)")]
        public LedgerType LED_TYPE { get; set; }

        [Category("Amounts")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal ADV_INTE { get; set; }

        [Category("Amounts")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal AFT_INTE { get; set; }

        [Category("Amounts")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal ACCU_PAYP { get; set; }

        [Category("Amounts")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal YTD_I { get; set; }

        [Category("(Main)")]
        public string LOAN_TYPE { get; set; }

        [Category("Details")]
        public LoanStatus LOAN_STAT { get; set; }

        [Category("Arrears")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal ARREAR_I { get; set; }

        [Category("Arrears")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal ARREAR_P { get; set; }

        [Category("Arrears")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal ARREAR_OTH { get; set; }

        [Category("Arrears")]
        public DateTime ARREAR_AS { get; set; }

        [Category("Details")]
        public string COLLATERAL { get; set; }

        [Category("Details")]
        public string DED_BAL { get; set; }

        [Category("Dates")]
        public DateTime ADD_DATE { get; set; }

        [ReadOnly(true)]
        public DateTime CHG_DATE { get; set; }

        [ReadOnly(true)]
        public string USER { get; set; }

        [Category("Balances")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal P_BAL { get; set; }

        [Category("Balances")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal I_BAL { get; set; }

        [Category("Balances")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal O_BAL { get; set; }
        
        [Category("Flags")]
        public bool REC_STAT { get; set; }

        [Category("Flags")]
        public bool RENEW { get; set; }
        [TypeConverter(typeof(MoneyConverter))]

        [Category("Amounts")]
        public decimal ADVANCE { get; set; }

        [Category("Flags")]
        public bool LRI_IND { get; set; }

        [Category("Details")]
        public DateTime NDUE { get; set; }

        [Category("Flags")]
        public bool L_EXT { get; set; }

        [Category("Flags")]
        public bool PD { get; set; }

        [Category("Amounts")]
        [TypeConverter(typeof(MoneyConverter))]
        public decimal LRI_DUE { get; set; }

        bool ShouldSerializePN_NO() { return false; }
        bool ShouldSerializeKBCI_NO() { return false; }
        bool ShouldSerializeAPP_DATE() { return false; }
        bool ShouldSerializeAPP_NO() { return false; }
        bool ShouldSerializeDATE_GRANT() { return false; }
        bool ShouldSerializeBY_WHOM() { return false; }
        bool ShouldSerializeDATE_DUE() { return false; }
        bool ShouldSerializeCHKNO_BANK() { return false; }
        bool ShouldSerializeCHKNO() { return false; }
        bool ShouldSerializeCHKNO_AMT() { return false; }
        bool ShouldSerializeCHKNO_DATE() { return false; }
        bool ShouldSerializeCHKNO_RELS() { return false; }
        bool ShouldSerializeCHKNO_ACK() { return false; }
        bool ShouldSerializeMOD_PAY() { return false; }
        bool ShouldSerializeAMORT_AMT() { return false; }
        bool ShouldSerializePAY_START() { return false; }
        bool ShouldSerializeRATE() { return false; }
        bool ShouldSerializeTERM() { return false; }
        bool ShouldSerializeFREQ() { return false; }
        bool ShouldSerializePRINCIPAL() { return false; }
        bool ShouldSerializeLED_TYPE() { return false; }
        bool ShouldSerializeADV_INTE() { return false; }
        bool ShouldSerializeAFT_INTE() { return false; }
        bool ShouldSerializeACCU_PAYP() { return false; }
        bool ShouldSerializeYTD_I() { return false; }
        bool ShouldSerializeLOAN_TYPE() { return false; }
        bool ShouldSerializeLOAN_STAT() { return false; }
        bool ShouldSerializeARREAR_I() { return false; }
        bool ShouldSerializeARREAR_P() { return false; }
        bool ShouldSerializeARREAR_OTH() { return false; }
        bool ShouldSerializeARREAR_AS() { return false; }
        bool ShouldSerializeCOLLATERAL() { return false; }
        bool ShouldSerializeDED_BAL() { return false; }
        bool ShouldSerializeADD_DATE() { return false; }
        bool ShouldSerializeCHG_DATE() { return false; }
        bool ShouldSerializeUSER() { return false; }
        bool ShouldSerializeP_BAL() { return false; }
        bool ShouldSerializeI_BAL() { return false; }
        bool ShouldSerializeO_BAL() { return false; }
        bool ShouldSerializeREC_STAT() { return false; }
        bool ShouldSerializeRENEW() { return false; }
        bool ShouldSerializeADVANCE() { return false; }
        bool ShouldSerializeLRI_IND() { return false; }
        bool ShouldSerializeNDUE() { return false; }
        bool ShouldSerializeL_EXT() { return false; }
        bool ShouldSerializePD() { return false; }
        bool ShouldSerializeLRI_DUE() { return false; }

    }
}
