using App.GroupChat.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.GroupChat.Data.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new() {
        private GroupChatContext _chatContext;
        public GroupChatContext ChatContext { get => _chatContext; }
        public GenericRepository(GroupChatContext chatContext) {
            _chatContext = chatContext;
        }
        public void Add(T entity) {
            _chatContext.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities) {
            _chatContext.Set<T>().AddRange(entities);
        }
        public void Delete(T entity) {
            _chatContext.Set<T>().Remove(entity);
        }
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate) {
            IQueryable<T> query = _chatContext.Set<T>().Where(predicate);
            return query;
        }
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) {
            return await _chatContext.Set<T>().AnyAsync(predicate);
        }

        public async Task<ICollection<T>> GetAllAsync() {
            return await _chatContext.Set<T>().ToListAsync();
        }
        public void Update(T entity, T latestEntity) {
            var item = _chatContext.Entry(entity);
            item.CurrentValues.SetValues(latestEntity);
        }
        public void Update(T entity) {
            var item = _chatContext.Entry(entity);
            item.State = EntityState.Modified;
        }
        public async Task SaveAsync() {
            await _chatContext.SaveChangesAsync();
        }
    }
}
