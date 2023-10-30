using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Data;
using ProjectStrayToHomeAPI.Models.EF.Base;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace ProjectStrayToHomeAPI.Repositories.Base
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : IRepositoryBase<TEntity, TPrimaryKey> 
        where TPrimaryKey: IConvertible
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected ApplicationDbContext _context { get; set; }
        public RepositoryBase(ApplicationDbContext context)
        {
            _context = context;
        }
        public virtual async Task<TEntity> FindByIDAsync(TPrimaryKey id)
        {
            var result = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (result == null)
                throw new KeyNotFoundException(id + " not found in context");
            return result;
        }
        
        public virtual async Task<IEnumerable<TEntity>> FindAllAsync() =>
            await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        public virtual async Task<IEnumerable<TEntity>> FindByConditionAsync(Expression<Func<TEntity, bool>> expression) =>
           await _context.Set<TEntity>().Where(expression).AsNoTracking().ToListAsync();

        public virtual void Create(TEntity entity) => _context.Set<TEntity>().Add(entity);

        public virtual void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

        public virtual void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);
    }
}
