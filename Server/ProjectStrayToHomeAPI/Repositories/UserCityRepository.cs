using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;
using ProjectStrayToHomeAPI.Repositories.Interfaces;

namespace ProjectStrayToHomeAPI.Repositories
{
    public class UserCityRepository : RepositoryBase<ApplicationUser_City, int>, IUserCityRepository
    {
        public UserCityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
