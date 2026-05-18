using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Domain.Contracts;
using Talabat.Domain.Models;
using Talabat.Persistence.Data.Context;

namespace Talabat.Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> 
        : IGenericRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
