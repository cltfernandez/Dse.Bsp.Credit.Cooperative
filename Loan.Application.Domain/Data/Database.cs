using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loan.Application.Infrastructure.Enumerations.Sql;

namespace Loan.Application.Domain.Data
{
    public static class Database
    {
        public static T GetObject<T>(Query type, params object[] parameters)
        {
            return Loan.Application.Infrastructure.Data.Database.GetObject<T>(type, parameters);
        }
        
    }
}
