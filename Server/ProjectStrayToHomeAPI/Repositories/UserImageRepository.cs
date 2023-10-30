using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;
using ProjectStrayToHomeAPI.Repositories.Interfaces;

namespace ProjectStrayToHomeAPI.Repositories
{
    public class UserImageRepository : RepositoryBase<ApplicationUser_Image, int>, IUserImageRepository
    {
        public UserImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task SetUserImageAsync(IFormFile profileImage, string userId)
        {
            using MemoryStream memoryStream = new MemoryStream();
            var dateImageNow = DateTime.Now;
            profileImage.CopyTo(memoryStream);
            var userImage = new ApplicationUser_Image();
            userImage.ApplicationUserID = userId;
            userImage.Image = memoryStream.ToArray();
            userImage.CreateDate = dateImageNow;
            userImage.UpdateDate = dateImageNow;
            
            var imageToRemove = await _context.User_Images.FirstOrDefaultAsync(x => x.ApplicationUserID == userId);
            if (imageToRemove != null)
                Delete(imageToRemove);
            Create(userImage);
        }

        public async Task<ApplicationUser_Image> FindUserImageByUserIDAsync(string id)
        {
            var result = await _context.User_Images.AsNoTracking().FirstOrDefaultAsync(x => x.ApplicationUserID == id);
            //TODO: Implement my own expression
            if (result == null)
                throw new KeyNotFoundException("File not found expression");
            return result;
        }
    }
}
