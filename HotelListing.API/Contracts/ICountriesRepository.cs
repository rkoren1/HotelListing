using HotelListing.API.Data;

namespace HotelListing.API.Contracts
{
    public interface ICountriesRepository : IGenericRepositoryV2<Country>
    {
        Task<Country> GetDetails(int id);
    }
}
