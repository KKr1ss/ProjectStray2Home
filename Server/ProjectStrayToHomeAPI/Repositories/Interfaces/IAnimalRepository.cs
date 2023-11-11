using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;

namespace ProjectStrayToHomeAPI.Repositories.Interfaces
{
    public interface IAnimalRepository : IRepositoryBase<Animal, int>
    {
        Task<List<Animal>> GetAnimalsConnectedAsync(List<Animal> animals, bool withImages);
        Task<Animal> GetAnimalConnectedAsync(Animal animal, bool withImages);
        List<Animal> GetPaginatedAnimals(List<Animal> animals, int pageIndex, int pageSize);
        List<Animal> GetFilteredAnimals(List<Animal> animals, string? name, string? status, string? city, string? type, string? sex);
    }
}
