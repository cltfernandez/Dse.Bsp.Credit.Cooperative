using Loan.Application.Domain.Interfaces;

namespace Loan.Application.Domain.Loans
{
    public interface ILoanRepository : 
        IInsertable<Loan>, 
        IUpdatable<Loan>, 
        ISelectable<Loan, string>
    {
    }
}
