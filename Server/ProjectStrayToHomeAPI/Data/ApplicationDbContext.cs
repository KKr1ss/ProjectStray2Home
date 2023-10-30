using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Models.EF;

namespace ProjectStray2HomeAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base()
        {
        }
        public ApplicationDbContext(DbContextOptions options)
         : base(options)
        {
        }

        public DbSet<Animal> Animals => Set<Animal>();
        public DbSet<Animal_Image> Animal_Images => Set<Animal_Image>();
        public DbSet<Animal_Comment> Animal_Comments => Set<Animal_Comment>();
        public DbSet<ApplicationUser_Image> User_Images => Set<ApplicationUser_Image>();
        public DbSet<ApplicationUser_City> User_Cities => Set<ApplicationUser_City>();
        public DbSet<City> Cities => Set<City>();
    }
}
