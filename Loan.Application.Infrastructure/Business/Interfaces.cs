using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loan.Application.Infrastructure.Business.Interfaces
{
    public interface IDateRange
    {
        DateTime DateFrom { get; set; }
        DateTime DateTo { get; set; }
    }

    public interface ITextRange
    {
        String TextFrom { get; set; }
        String TextTo { get; set; }
    }

    public interface IIntRange
    {
        String IntFrom { get; set; }
        String IntTo { get; set; }
    }
}
