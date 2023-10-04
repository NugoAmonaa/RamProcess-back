using Microsoft.EntityFrameworkCore;
using WebApplication1.Repository;

namespace VacationScaffold.RepositoryImplementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ProjectContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ProjectContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();

        }

        public async Task<IEnumerable<T>> GetList()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Insert(T obj)
        {
            await _dbSet.AddAsync(obj);
        }

        public async Task Update(T obj)
        {
            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public async Task DeleteById(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }


    }
}