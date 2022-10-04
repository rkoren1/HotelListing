using HotelListing.API.Contracts;
using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace HotelListing.API.Repository
{
    public class GenericRepositoryV2 : IGenericRepositoryV2
    {
        protected readonly HotelListingDbContext _context;
        public GenericRepositoryV2(HotelListingDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Set<T>().Add(entity);
        }
        public ValueTask<EntityEntry<T>> AddAsync<T>(T entity) where T : class
        {
            return _context.Set<T>().AddAsync(entity);
        }
        public void AddRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().AddRange(entities);

        }
        public void AddRangeAsync<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().AddRangeAsync(entities);

        }
        public IEnumerable<T> Find<T>(Expression<Func<T, bool>> expression) where T : class
        {
            return _context.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }
        public Task<List<T>> GetAllAsync<T>() where T : class
        {
            return _context.Set<T>().ToListAsync();
        }
        public ValueTask<T> GetByIdAsync<T>(int id) where T : class
        {
            return _context.Set<T>().FindAsync(id);
        }
        public T GetById<T>(int id) where T : class
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange<T>(IEnumerable<T> entities) where T : class
        {
            _context.Set<T>().RemoveRange(entities);
        }

        private void CommitTransaction()
        {
            _context.SaveChanges();
        }
        public Task<int> CommitTransactionAsync()
        {
           return _context.SaveChangesAsync();
        }

        /// Updates existing entity and save (U)
        public void Update<T>(T entity) where T : class
        {
            UpdateWithoutSave(entity);
            CommitTransaction();
        }

        public Task<int> UpdateAsync<T>(T entity) where T : class
        {
            UpdateWithoutSave(entity);
           return CommitTransactionAsync();
        }

        /// Updates list of entitites and saves (U)
        public void UpdateList<T>(IEnumerable<T> entityList) where T : class
        {
            foreach (T entity in entityList)
            {
                UpdateWithoutSave(entity);
            }
            CommitTransaction();
        }
        public void UpdateListAsync<T>(IEnumerable<T> entityList) where T : class
        {
            foreach (T entity in entityList)
            {
                UpdateWithoutSave(entity);
            }
            CommitTransactionAsync();
        }

        // Update without save (commit)
        private void UpdateWithoutSave<T>(T entity) where T : class
        {
            var entry = _context.Entry(entity);
            if (entry == null) throw new Exception(string.Format("Error occurred during UPDATE operation: entity of type {0} does not exist in the database.", typeof(T).Name));
            entry.State = EntityState.Modified;
        }

    }
}
