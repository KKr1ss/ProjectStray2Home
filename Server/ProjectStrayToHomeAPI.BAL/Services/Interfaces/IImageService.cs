using Microsoft.AspNetCore.Mvc;

namespace ProjectStrayToHomeAPI.Services.Interfaces
{
    public interface IImageService
    {
        Task<FileStreamResult> GetUserImageAsync(string username);
        Task<FileStreamResult> GetAnimalImageAsync(int animalId, int? imageId);
    }
}
