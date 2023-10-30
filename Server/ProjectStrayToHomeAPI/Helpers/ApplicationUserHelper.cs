using ProjectStray2HomeAPI.Models.EF;

namespace ProjectStrayToHomeAPI.Helpers
{
    public class ApplicationUserHelper
    {
        public static ApplicationUser getUserReadyForRegistration(ApplicationUser user)
        {
            var dateNow = DateTime.Now;
            user.CreateDate = dateNow;
            user.UpdateDate = dateNow;
            user.EmailConfirmed = true;
            user.PhoneNumberConfirmed = true;
            user.LockoutEnabled = false;
            user.SecurityStamp = Guid.NewGuid().ToString("D");

            return user;
        }
    }
}
