using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Domain.Contracts;
using Talabat.Domain.Models;

namespace Talabat.Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>;
    }
}
