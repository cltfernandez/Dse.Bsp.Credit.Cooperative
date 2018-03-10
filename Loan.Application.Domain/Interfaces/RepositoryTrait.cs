using System.Collections.Generic;

namespace Loan.Application.Domain.Interfaces
{
    public interface ISelectable<TEntity, TKey>
    {
        TEntity Select(TKey id);
    }

    public interface IInsertable<T>
    {
        void Insert(T entity);
    }

    public interface IUpdatable<T>
    {
        void Update(T entity);
    }

    public interface IDeletable<T>
    {
        void Delete(T entity);
    }
}
