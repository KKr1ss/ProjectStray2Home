using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;
using ProjectStrayToHomeAPI.Repositories.Interfaces;

namespace ProjectStrayToHomeAPI.Repositories
{
    public class UserRepository : RepositoryBase<ApplicationUser, string>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ApplicationUser> GetUserConnectedAsync(ApplicationUser user, bool withImages)
        {
            var returnedUser = _context.Users.AsNoTracking().First(x => x.Id == user.Id);
            returnedUser.City = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == returnedUser.CurrentCityID);
            returnedUser.Animals = await _context.Animals.AsNoTracking().Where(x => x.UserID == returnedUser.Id).OrderByDescending(x=>x.CreateDate).ToListAsync();
            returnedUser.User_Cities = await _context.User_Cities.AsNoTracking().Where(x => x.UserID == returnedUser.Id).Include(x => x.City).ToListAsync();
            if (withImages)
                returnedUser.User_Image = await _context.User_Images.AsNoTracking().FirstOrDefaultAsync(x => x.ApplicationUserID == returnedUser.Id);

            return returnedUser;
        }
    }
}
