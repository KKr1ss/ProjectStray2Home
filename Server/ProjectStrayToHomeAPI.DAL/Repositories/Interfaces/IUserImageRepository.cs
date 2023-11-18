using Microsoft.AspNetCore.Http;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;

namespace ProjectStrayToHomeAPI.Repositories.Interfaces
{
    public interface IUserImageRepository: IRepositoryBase<ApplicationUser_Image, int>
    {
        Task<ApplicationUser_Image> FindUserImageByUserIDAsync(string userId);
        Task SetUserImageAsync(IFormFile profileImage, string userId);
    }
}
