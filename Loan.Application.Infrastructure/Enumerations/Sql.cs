using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Loan.Application.Infrastructure.Enumerations.Sql
{
    public enum Query
    {
        [Description("CTD")]
        Collaterals,
        [Description("COMAKER")]
        Comaker,
        [Description("CTRL")]
        Control,
        [Description("SDEDBAL")]
        Deductions,
        [Description("FD")]
        FixedDeposit,
        [Description("LEDGER")]
        Ledger,
        [Description("LNHOLD")]
        LoanHold,
        [Description("LRIDUE")]
        LoanReleaseInsurance,
        [Description("LOANS")]
        Loan,
        LoanTypeDetails,
        MemberCount,
        MemberList,
        MemberLoans,
        [Description("MEMBERS")]
        Member,
        [Description("MO_DEDN")]
        MonthlyDeduction,
        [Description("MO_DEDNO")]
        OffCycleDeduction,
        [Description("RLRIDUE")]
        RLoanReleaseInsurance,
        [Description("SD")]
        SavingsDeposit,
        [Description("CTRL_S")]
        SavingsControl,
        [Description("SDMASTER")]
        SavingsDepositMaster,
        Do_Payroll_Generate,
        [Description("USER")]
        User
    }

    public enum NonQuery
    {
        Do_Payroll_Generate,
        Do_AsOf_Payroll_Generate,
        Do_Admin_AdminDate,
        Do_Admin_Archive,
        Do_Admin_Close,
        Do_Admin_DailyReversion,
        Do_Admin_EndOfDay,
        Do_Admin_ExtractInterest,
        Do_Admin_Open,
        Do_Admin_TagReport
    }
}
