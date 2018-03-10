using System;
using System.ComponentModel;
using Loan.Application.Infrastructure.Enumerations.DropDownItems;
using Loan.Application.Infrastructure.Controls.PropertyEditor.Converters;

namespace Loan.Application.Domain.Members
{
    [DefaultProperty("KBCI_NO")]
    public class Member
    {
        [Browsable(false)]
        public Int64 KBCI_ID { get; set; }

        [Category("(Main)")]
        public string KBCI_NO { get; set; }

        [Category("(MemberName)")]
        public string LNAME { get; set; }

        [Category("(MemberName)")]
        public string FNAME { get; set; }

        [Category("(MemberName)")]
        public string MI { get; set; }

        [Category("(Main)")]
        public MembershipCode MEM_CODE { get; set; }

        [Category("(Main)")]
        public MembershipStatus MEM_STAT { get; set; }

        [Category("(Main)")]
        public DateTime MEM_DATE { get; set; }

        [Browsable(false)]
        public string BY_WHOM { get; set; }

        [Category("Employment")]
        public string CB_EMPNO { get; set; }

        [Category("Employment")]
        public DateTime CB_HIRE { get; set; }

        [Category("Employment")]
        public string DEPT { get; set; }

        [Category("Employment")]
        public string REGION { get; set; }

        [Category("Employment")]
        public string OFF_TEL { get; set; }

        [Category("Employment")]
        public bool DORI { get; set; }

        [Category("Employment")]
        public string REA_DORI { get; set; }

        [Category("Personal")]
        public Sex SEX { get; set; }

        [Category("Personal")]
        public DateTime B_DATE { get; set; }

        [Category("Personal")]
        public string CIV_STAT { get; set; }

        [Category("Personal")]
        public string MEM_ADDR { get; set; }

        [Category("Personal")]
        public string RES_TEL { get; set; }

        [Category("Personal")]
        public string POSITION { get; set; }

        [Category("Finance")]
        [TypeConverter(typeof(MoneyConverter))]
        public double SAL_BAS { get; set; }

        [Category("Finance")]
        [TypeConverter(typeof(MoneyConverter))]
        public double SAL_ALL { get; set; }

        [Category("Finance")]
        [TypeConverter(typeof(MoneyConverter))]
        public double OTH_INC { get; set; }

        [Category("FinancialAccounts")]
        public string FEBTC_SA { get; set; }
        
        [Category("FinancialAccounts")]
        public string FEBTC_CA { get; set; }

        [Category("FinancialAccounts")]
        public string FD_CERTNO { get; set; }
        
        [Category("FinancialAccounts")]
        public DateTime FD_DATE { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double AP_AMOUNT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double AR_AMOUNT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double FD_AMOUNT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double SD_AMOUNT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double TD_AMOUNT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double OTH_AMOUNT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double YTD_DIVAMT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double YTD_LRI { get; set; }

        [Category("Rem")]
        public string REM_PROP { get; set; }

        [Category("Rem")]
        [TypeConverter(typeof(MoneyConverter))]
        public double REM_VALUE { get; set; }

        [Category("Spouse")]
        public uint NO_DEPEND { get; set; }

        [Category("Spouse")]
        public string SP_NAME { get; set; }

        [Category("Spouse")]
        public string SP_EMPLOY { get; set; }

        [Category("Spouse")]
        public string SP_OFFTEL { get; set; }

        [Category("Spouse")]
        public string SP_CBEMPNO { get; set; }

        [Category("Spouse")]
        public string SP_KBCINO { get; set; }

        [Category("Spouse")]
        [TypeConverter(typeof(MoneyConverter))]
        public double SP_SALARY { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double APRUN_AMT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double ARRUN_AMT { get; set; }

        [Category("FinancialDetails")]
        [TypeConverter(typeof(MoneyConverter))]
        public double RUN_AMT { get; set; }
        
        [ReadOnly(true)]
        public DateTime ADD_DATE { get; set; }

        [ReadOnly(true)]
        public DateTime CHG_DATE { get; set; }

        [ReadOnly(true)]
        public string USER { get; set; }

        [ReadOnly(true)]
        public bool REC_STAT { get; set; }
        
        [ReadOnly(true)]
        public uint FD_CNTR { get; set; }

    }
}
