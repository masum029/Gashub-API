using Microsoft.EntityFrameworkCore;
using Project.Domail.Abstractions.CommandRepositories.Base;
using Project.Infrastructure.DataContext;

namespace Project.Infrastructure.Implementation.Command.Base
{
    public class CommandRepository<T> : ICommandRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> _dbSet;

        public CommandRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
