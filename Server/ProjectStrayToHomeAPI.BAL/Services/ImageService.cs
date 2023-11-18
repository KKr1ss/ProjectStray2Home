using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Helpers;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStrayToHomeAPI.Services
{
    public class ImageService : IImageService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepositoryManager _repositoryManager;

        public ImageService(UserManager<ApplicationUser> userManager, IRepositoryManager repositoryManager)
        {
            _userManager = userManager;
            _repositoryManager = repositoryManager;
        }

        public async Task<FileStreamResult> GetUserImageAsync(string username)
        {
            string id = (await _userManager.FindByNameAsync(username)).Id;
            var image = await _repositoryManager.UserImages.FindUserImageByUserIDAsync(id);
            var result = FileStreamConverter.ConvertByteArrayToFileStreamResult(image.Image, "user");
            return result;
        }

        public async Task<FileStreamResult> GetAnimalImageAsync(int animalId, int? imageId)
        {
            var imageList = await _repositoryManager.AnimalImages.FindAnimalImagesByAnimalIDAsync(animalId);
            var image = imageId == null ? imageList.First() : imageList.First(x => x.Id == imageId);
            var result = FileStreamConverter.ConvertByteArrayToFileStreamResult(image.Image, "animal");
            return result;
        }
    }
}
