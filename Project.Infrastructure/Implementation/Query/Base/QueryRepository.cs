using Microsoft.EntityFrameworkCore;
using Project.Domail.Abstractions.QueryRepositories.Base;
using Project.Infrastructure.DataContext;

namespace Project.Infrastructure.Implementation.Query.Base
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> _dbSet;
        public QueryRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
