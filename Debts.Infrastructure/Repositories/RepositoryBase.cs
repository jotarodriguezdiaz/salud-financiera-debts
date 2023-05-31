using Debts.Application.Contracts.Persistence;
using Debts.Domain.Common;
using Debts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Debts.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : BaseDomainModel
    {
        protected readonly DebtsContext _context;

        public RepositoryBase(DebtsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        { 
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;  
        }

        public async Task<T> UpdateAsync(T entity)
        { 
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
