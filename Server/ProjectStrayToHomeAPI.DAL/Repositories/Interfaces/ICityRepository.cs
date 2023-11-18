using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;

namespace ProjectStrayToHomeAPI.Repositories.Interfaces
{
    public interface ICityRepository : IRepositoryBase<City, int>
    {
        Task SetObservedCitiesForUserAsync(string userId, int[] cityIds);
        List<City> GetObservedCitiesByUser(string userId);
    }
}
