using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loan.Application.Infrastructure.Enumerations.Popups
{
    public enum PopupType
    {
        ControlDate,
        UserDate,
        DateRange,
        PnNoRange,
        PnNoAndDateRange,
        KbciNoRange,
        KbciNoAndDate,
        KbciNoAndDateRange,
        BankAndCheckNo
    }

    public enum PopupResponses
    {
        Ok,
        Nok,
        Quit
    }

    public enum LambdaFiltering
    {
        Equals,
        Contains
    }

    public enum SavingsDepositQuery
    {
        KBCI_NO,
        ACCTNO
    }

}
