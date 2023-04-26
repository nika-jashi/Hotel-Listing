using HotelListingAPI.Models;

namespace HotelListingAPI.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task<List<T>> GetAllAsync();
        Task<List<TResult>> GetAllAsync<TResult>();
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task<bool> Exists(int id);

    }
}
