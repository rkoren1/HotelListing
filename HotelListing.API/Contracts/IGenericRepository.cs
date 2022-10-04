using HotelListing.API.Data;

namespace HotelListing.API.Contracts
{
    public interface IGenericRepositoryV2<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
        Task<bool> Exists(int id);
    }
    
}
