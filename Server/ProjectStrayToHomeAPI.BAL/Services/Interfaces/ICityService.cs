using ProjectStray2HomeAPI.Models.DTO;

namespace ProjectStrayToHomeAPI.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetCitiesAsync();
    }
}
