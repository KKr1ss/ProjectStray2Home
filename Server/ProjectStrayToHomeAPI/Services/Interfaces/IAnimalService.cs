using ProjectStrayToHomeAPI.Models.DTO.Animals;
using ProjectStrayToHomeAPI.Models.DTO;

namespace ProjectStrayToHomeAPI.Services.Interfaces
{
    public interface IAnimalService
    {
        Task<APIResultDTO> UploadAsync(AnimalRequestDTO animalRequest, List<IFormFile> animalImages);

        Task<APIResultDTO> UploadCommentAsync(AnimalCommentRequestDTO commentRequest);

        Task<AnimalDetailsDTO?> GetAnimalDetails(int id);

        Task<PaginatorResultDTO<AnimalPreviewDTO>> GetAnimals(int pageIndex, int pageSize);
    }
}
