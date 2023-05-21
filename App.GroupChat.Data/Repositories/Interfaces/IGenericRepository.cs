using System.Linq.Expressions;

namespace App.GroupChat.Data.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Update(T entity, T latestEntity);
        void Update(T entity);
        Task SaveAsync();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    }
}
