using System;
using System.ComponentModel;

namespace Loan.Application.Infrastructure.Enumerations.DropDownItems
{

    #region Charges
    
    public enum DoubleEntry
    {
        [Description("DM")]        
        DM,
        [Description("CM")]
        CM
    }

    public enum ChargeRemarks
    {
        [Description("LRI")]
        LRI,
        [Description("SAV")]
        Savings,
        [Description("CSH")]
        Cash,
        [Description("CHK")]
        Check,
        [Description("AP")]
        AP,
        [Description("AR")]
        AR,
        [Description("OTH")]
        Others,
        [Description("FIX")]
        FixedDeposit,
        [Description("SCH")]
        ServiceCharge
    }

    #endregion

    #region Deductions

    public enum PayTag
    {
        [Description(" ")]
        Untagged,
        [Description("1")]
        Full,
        [Description("2")]
        Arrear,
        [Description("3")]
        OtherAmount,
        [Description("4")]
        LRI
    }

    #endregion

    #region Ledger

    public enum AccountCode
    {
        [Description("PRI")]
        PRI,
        [Description("INT")]
        INT,
        [Description("PEN")]
        PEN,
        [Description("LRI")]
        LRI,
        [Description("SAV")]
        SAV,
        [Description("OTH")]
        OTH
    }

    public enum AccountType
    {
        [Description("ADJ")]
        ADJ,
        [Description("AJT")]
        AJT,
        [Description("AMT")]
        AMT,
        [Description("AP")]
        AP,
        [Description("AR")]
        AR,
        [Description("CAS")]
        CAS,
        [Description("CHK")]
        CHK,
        [Description("COM")]
        COM,
        [Description("CSH")]
        CSH,
        [Description("DEP")]
        DEP,
        [Description("FD")]
        FD,
        [Description("FIX")]
        FIX,
        [Description("INI")]
        INI,
        [Description("INT")]
        INT,
        [Description("LRI")]
        LRI,
        [Description("MSC")]
        MSC,
        [Description("OTH")]
        OTH,
        [Description("PAY")]
        PAY,
        [Description("PRI")]
        PRI,
        [Description("REP")]
        REP,
        [Description("SAV")]
        SAV,
        [Description("SC")]
        SC,
        [Description("SCH")]
        SCH,
        [Description("SD")]
        SD,
        [Description("TER")]
        TER

    }

    #endregion

    #region Loans

    public enum Frequency
    {
        [Description("D")]
        Daily,
        [Description("M")]
        Monthly,
        [Description("Q")]
        Quarterly,
        [Description("S")]
        SemiAnnual,
        [Description("A")]
        Annual
    }

    public enum LedgerType
    {
        [Description("1")]
        OneTimeInterest,
        [Description("0")] //check
        DiminishingPrincipal
    }

    public enum LoanStatus
    {
        [Description("P")]
        PastDue,
        [Description("T")]
        Terminated,
        [Description("F")]
        FullyPaid,
        [Description("R")]
        Released,
        [Description("A")]
        Approved,
        [Description("D")]
        Disapproved,
        [Description("N")] //check
        NewApplication
    }

    public enum LoanTypeAll
    {
        ALL,
        APL,
        CML,
        EDL,
        EML,
        FAL,
        MPL,
        PTL,
        RGL,
        RSL,
        SML,
        SPL,
        STL
    }

    public enum LoanType
    {
        [Description("APL")]
        APL,
        [Description("BFL")]
        BFL,
        [Description("CML")]
        CML,
        [Description("EDL")]
        EDL,
        [Description("EML")]
        EML,
        [Description("FAL")]
        FAL,
        [Description("MPL")]
        MPL,
        [Description("PTL")]
        PTL,
        [Description("RGL")]
        RGL,
        [Description("SML")]
        SML,
        [Description("RSL")]
        RSL,
        [Description("SPL")]
        SPL,
        [Description("STL")]
        STL
    }

    public enum PayMode
    {
        [Description("")]
        Others,
        [Description("1")]
        Payroll,
        [Description("2")]
        PostDatedCheck,
        [Description("3")]
        DebitMemo,
        [Description("4")]
        Offcycle
    }

    public enum NetProceedsMode
    {
        C,
        D,
        F
    }

    #endregion

    #region Members

    public enum CivilStatus
    {
        [Description("S")]
        Single,
        [Description("M")]
        Married,
        [Description("W")]
        WidowOrWidower,
        [Description("")]
        Others
    }

    public enum MembershipCode
    {
        [Description("M")]
        Member,
        [Description("S")]
        Staff,
        [Description("I")]
        Investor
    }

    public enum MembershipStatus
    {
        [Description("R")]
        Retired,
        [Description("A")]
        Active,
        [Description("S")]
        Staff
    }

    public enum Sex
    {
        [Description("M")]
        Male,
        [Description("F")]
        Female
    }

    #endregion

}
