using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Loan.Application.Infrastructure.Enumerations.System
{
    public enum AccessLevels
    {
        [Description("0")]
        None,
        [Description("1")]
        Level1,
        [Description("2")]
        Level2,
        [Description("3")]
        Level3,
        [Description("4")]
        Level4
    }

    public enum AuthenticationResponses
    {
        Granted,
        Overriden,
        InvalidCredentials,
        Denied
    }

    public enum AsyncProcess
    {
        Close,
        EndOfDay,
        Backup,
        Restore,
        Payroll,
        OffCycle
    }
}
