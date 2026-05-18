using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Domain.Models;

namespace Talabat.Domain.Contracts
{
    public interface IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity); // we make it async because sometimes efCore make checks in database table checks identity value internally
        void Update(TEntity entity); // this works locally in memory and when we call save changes it will update the database
        void Delete(TEntity entity); // this works locally in memory and when we call save changes it will update the database
    }
}

