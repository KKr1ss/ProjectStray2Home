using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStrayToHomeAPI.Tests.UnitTestHelpers
{
    public abstract class BaseTest
    {
        protected DbContextOptions options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

        protected ApplicationDbContext GetContextWithSeedData()
        {
            var context = new ApplicationDbContext(options);

            context.Cities.AddRange(SeedData.cities);

            context.Roles.AddRange(SeedData.roles);
            context.Users.AddRange(SeedData.users);
            context.UserRoles.AddRange(SeedData.userRoles);

            context.User_Images.AddRange(SeedData.userImages);

            context.Animals.AddRange(SeedData.animals);
            context.Animal_Images.AddRange(SeedData.animalImages);

            context.SaveChanges();
            context.ChangeTracker.Clear();

            return context;
        }
    }
}
