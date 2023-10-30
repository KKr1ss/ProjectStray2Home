using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Services
{
    public class SeedServiceTest : BaseTest
    {
        [Fact]
        public async Task ImportDataAsync_TestSuccess()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            var roleManager = ArrangeHelper.GetRoleManager(
                new RoleStore<IdentityRole>(context));
            var userManager = ArrangeHelper.GetUserManager(
                new UserStore<ApplicationUser>(context));
            var repositoryManager = new Mock<RepositoryManager>(context).Object;
            var webHostEnvironment = new Mock<IWebHostEnvironment>();
            webHostEnvironment.Setup(x => x.EnvironmentName).Returns("Development");
            webHostEnvironment.Setup(x => x.ContentRootPath).Returns(Directory.GetCurrentDirectory());


            var service = new SeedService(userManager, roleManager, repositoryManager, webHostEnvironment.Object);

            //Act
            var result = await service.ImportDataAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("{ citiesAdded = 3178, rolesAdded = 2 }", result.Value?.ToString());
            Assert.Equal(3178, context.Cities.Count());
            Assert.Equal(2, context.Roles.Count());
        }

        [Fact]
        public async Task ImportDataAsync_TestFailedCitiesExist()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            var roleManager = ArrangeHelper.GetRoleManager(
                new RoleStore<IdentityRole>(context));
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(x => x.Cities.FindAllAsync()).Throws<ArgumentException>();
            var webHostEnvironment = new Mock<IWebHostEnvironment>();

            var service = new SeedService(userManagerMock.Object, roleManager, repositoryManagerMock.Object, webHostEnvironment.Object);

            //Act
            Task result() => service.ImportDataAsync();

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Fact]
        public async Task ImportAdminAsync_TestSuccess()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            var roleManager = ArrangeHelper.GetRoleManager(
                new RoleStore<IdentityRole>(context));
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null!);
            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null!);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(false);
            userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(x => x.Cities.FindAllAsync()).ReturnsAsync(SeedData.cities);
            var webHostEnvironment = new Mock<IWebHostEnvironment>();

            var service = new SeedService(userManagerMock.Object, roleManager, repositoryManagerMock.Object, webHostEnvironment.Object);

            //Act
            var result = await service.ImportAdminAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Equal("Admin been seeded to database!", result.Value?.ToString());
        }

        [Fact]
        public async Task ImportAdminAsync_TestFailedCallCities()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            var roleManager = ArrangeHelper.GetRoleManager(
                new RoleStore<IdentityRole>(context));
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(x => x.Cities.FindAllAsync()).ReturnsAsync(new List<City>());
            var webHostEnvironment = new Mock<IWebHostEnvironment>();

            var service = new SeedService(userManagerMock.Object, roleManager, repositoryManagerMock.Object, webHostEnvironment.Object);

            //Act
            Task result() => service.ImportAdminAsync();

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Fact]
        public async Task ImportAdminAsync_TestFoundName()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            var roleManager = ArrangeHelper.GetRoleManager(
                new RoleStore<IdentityRole>(context));
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());

            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(x => x.Cities.FindAllAsync()).ReturnsAsync(SeedData.cities);
            var webHostEnvironment = new Mock<IWebHostEnvironment>();

            var service = new SeedService(userManagerMock.Object, roleManager, repositoryManagerMock.Object, webHostEnvironment.Object);

            //Act
            Task result() => service.ImportAdminAsync();

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Fact]
        public async Task ImportAdminAsync_TestFoundEmail()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            var roleManager = ArrangeHelper.GetRoleManager(
                new RoleStore<IdentityRole>(context));
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null!);
            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser());

            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(x => x.Cities.FindAllAsync()).ReturnsAsync(SeedData.cities);
            var webHostEnvironment = new Mock<IWebHostEnvironment>();

            var service = new SeedService(userManagerMock.Object, roleManager, repositoryManagerMock.Object, webHostEnvironment.Object);

            //Act
            Task result() => service.ImportAdminAsync();

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }

        [Fact]
        public async Task ImportAdminAsync_TestCreateFailed()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            var roleManager = ArrangeHelper.GetRoleManager(
                new RoleStore<IdentityRole>(context));
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null!);
            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null!);
            var failedIdentity = IdentityResult.Failed(new IdentityError[] { new IdentityError { Code = "0001", Description = "error test" } });
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(failedIdentity);

            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(x => x.Cities.FindAllAsync()).ReturnsAsync(SeedData.cities);
            var webHostEnvironment = new Mock<IWebHostEnvironment>();

            var service = new SeedService(userManagerMock.Object, roleManager, repositoryManagerMock.Object, webHostEnvironment.Object);

            //Act
            Task result() => service.ImportAdminAsync();

            //Assert
            await Assert.ThrowsAsync<ArgumentException>(result);
        }
    }
}
