using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;

namespace ProjectStrayToHomeAPI.Repositories.Interfaces
{
    public interface IAnimalImageRepository : IRepositoryBase<Animal_Image, int>
    {
        Task<List<Animal_Image>> FindAnimalImagesByAnimalIDAsync(int animalID);

        Task SetAnimalImagesAsync(List<IFormFile> animalImagesToAdd, int animalID);
    }
}
