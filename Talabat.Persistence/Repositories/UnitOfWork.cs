using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Domain.Contracts;
using Talabat.Domain.Models;
using Talabat.Persistence.Data.Context;

namespace Talabat.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;

        private readonly Dictionary<Type, object> _repositories = [];
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repository))
            {
                return (IGenericRepository<TEntity, TKey>)repository;
            }
            var newRepository = new GenericRepository<TEntity, TKey>(_context);
            _repositories[entityType] = newRepository;
            return newRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
