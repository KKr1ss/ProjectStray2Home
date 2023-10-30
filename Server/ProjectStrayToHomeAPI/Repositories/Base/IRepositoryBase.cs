using System.Linq.Expressions;

namespace ProjectStrayToHomeAPI.Repositories.Base
{
    public interface IRepositoryBase<T,Y>
    {
        Task<T> FindByIDAsync(Y id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
