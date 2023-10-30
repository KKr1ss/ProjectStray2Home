using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ProjectStrayToHomeAPI.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<ApplicationUser, string>
    {
        Task<ApplicationUser> GetUserConnectedAsync(ApplicationUser user, bool withImages);
    }
}
