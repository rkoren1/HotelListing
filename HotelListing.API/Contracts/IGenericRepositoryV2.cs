using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace HotelListing.API.Contracts
{
    public interface IGenericRepositoryV2
    {
        T GetById<T>(int id) where T : class;
        public ValueTask<T> GetByIdAsync<T>(int id) where T : class;
        IEnumerable<T> GetAll<T>() where T : class;
        public Task<List<T>> GetAllAsync<T>() where T : class;
        IEnumerable<T> Find<T>(Expression<Func<T, bool>> expression) where T : class;
        void Add<T>(T entity) where T : class;
        public ValueTask<EntityEntry<T>> AddAsync<T>(T entity) where T : class;
        void AddRange<T>(IEnumerable<T> entities) where T : class;
        public void AddRangeAsync<T>(IEnumerable<T> entities) where T : class;
        void Remove<T>(T entity) where T : class;
        void RemoveRange<T>(IEnumerable<T> entities) where T : class;
        void UpdateList<T>(IEnumerable<T> entities) where T : class;
        public void UpdateListAsync<T>(IEnumerable<T> entityList) where T : class;
        void Update<T>(T entity) where T : class;
        public Task<int> UpdateAsync<T>(T entity) where T : class;
    }
}
