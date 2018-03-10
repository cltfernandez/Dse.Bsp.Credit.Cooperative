using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Jc.Scripts.Database;
using Loan.Application.Domain.Data;
using Loan.Application.Infrastructure.Enumerations.Sql;

namespace Loan.Application.Domain.Loans
{
    public class LoanRepository : ILoanRepository
    {
        public Loan Select(string key)
        {
            return Database.GetObject<Loan>(Query.Loan, key);
        }

        public void Insert(Loan entity)
        {
            //Loan loan = new Loan();
            //const string storedProcedure = "ManageLoans";
        }

        public void Update(Loan entity)
        {
            throw new NotImplementedException();
        }
    }
}
