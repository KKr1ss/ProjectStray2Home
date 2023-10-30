using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;

namespace ProjectStrayToHomeAPI.Repositories.Interfaces
{
    public interface IAnimalRepository : IRepositoryBase<Animal, int>
    {
        Task<List<Animal>> GetAnimalsConnectedAsync(List<Animal> animals, bool withImages);
        Task<Animal> GetAnimalConnectedAsync(Animal animal, bool withImages);
        Task<List<Animal>> GetPaginatedAnimalsAsync(int pageIndex, int pageSize);
    }
}
