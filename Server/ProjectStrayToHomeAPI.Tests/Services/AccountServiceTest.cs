using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Handlers.Interfaces;
using ProjectStrayToHomeAPI.Models.DTO.User;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;
using System.IdentityModel.Tokens.Jwt;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace ProjectStrayToHomeAPI.Tests.Services
{
    [Collection("Sequential")]
    public class AccountServiceTest : BaseTest
    {
        [Fact]
        public async Task LoginAsync_TestSuccess()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            RepositoryManager repositoryManager = new Mock<RepositoryManager>(context).Object;
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(SeedData.users.First());
            userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true);

            var jwtHandler = new Mock<IJwtHandler>();
            jwtHandler.Setup(token => token.GetTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new JwtSecurityToken());
            jwtHandler.Setup(token => token.WriteToken(It.IsAny<JwtSecurityToken?>())).Returns("Good token");

            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            LoginRequestDTO loginRequest = new()
            {
                UserName = "userTest",
                Password = "Jelszo1234#"
            };

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManager, jwtHandler.Object, mapper);

            //Act
            var result = await controller.LoginAsync(loginRequest);

            //Assert
            Assert.True(result.Success);
            Assert.Equal(loginRequest.UserName, result.UserName);
        }

        [Fact]
        public async Task LoginAsync_TestFailedUserNotFound()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            RepositoryManager repositoryManager = new Mock<RepositoryManager>(context).Object;
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);

            var jwtHandler = new Mock<IJwtHandler>();

            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            LoginRequestDTO loginRequest = new()
            {
                UserName = "userTest",
                Password = "Jelszo1234#"
            };

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManager, jwtHandler.Object, mapper);

            //Act
            var result = await controller.LoginAsync(loginRequest);

            //Assert
            Assert.False(result.Success);
        }

        [Fact]
        public async Task LoginAsync_TestFailedPasswordValidation()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            RepositoryManager repositoryManager = new Mock<RepositoryManager>(context).Object;
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(SeedData.users.First());
            userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(false);

            var jwtHandler = new Mock<IJwtHandler>();

            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            LoginRequestDTO loginRequest = new()
            {
                UserName = "userTest",
                Password = "Jelszo1234#"
            };

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManager, jwtHandler.Object, mapper);

            //Act
            var result = await controller.LoginAsync(loginRequest);

            //Assert
            Assert.False(result.Success);
        }

        [Theory]
        [InlineData(new string[] { "1", "2" }, "Test", true)]
        [InlineData(null, "Test", true)]
        [InlineData(new string[] { "1", "2" }, null, true)]
        [InlineData(new string[] { "1", "2" }, "Test", false)]
        public async Task RegisterAsync_TestSuccess(string[]? observedCityIDs, string? description, bool photoUploaded)
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = GetContextWithSeedData();
            Mock<RepositoryManager> repositoryManager = new Mock<RepositoryManager>(context);

            var userManager = ArrangeHelper.GetUserManager(
                new UserStore<ApplicationUser>(context));

            var jwtHandler = new Mock<IJwtHandler>();
            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            RegisterRequestDTO registerRequest = new()
            {
                Email = "test@test.com",
                UserName = "testTest",
                PhoneNumber = "+36301234567",
                FirstName = "User",
                LastName = "Test",
                Sex = "Female",
                DateOfBirth = new DateTime(1990, 01, 02),
                CurrentCityID = "1",
                Description = description,
                ObservedCityIDs = observedCityIDs,
                Password = "Password"
            };
            var fileMock = new Mock<IFormFile>();
            var file = photoUploaded ? fileMock.Object : null; 

            AccountService controller = new AccountService(userManager, repositoryManager.Object, jwtHandler.Object, mapper);

            //Act
            var result = await controller.RegisterAsync(registerRequest, file);

            //Assert
            Assert.True(result.Success);
            Assert.True(context.Users.Any(x => x.UserName == registerRequest.UserName));
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        public async Task RegisterAsync_TestExistingEmailOrUsername(bool isUserNameExist, bool isEmailExist)
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = GetContextWithSeedData();
            Mock<RepositoryManager> repositoryManager = new Mock<RepositoryManager>(context);

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            ApplicationUser? nameUser = isUserNameExist ? new ApplicationUser () : null;
            ApplicationUser? emailUser = isEmailExist ? new ApplicationUser() : null;

            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(nameUser);
            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(emailUser);

            var jwtHandler = new Mock<IJwtHandler>();
            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            RegisterRequestDTO registerRequest = new()
            {
                Email = "test@test.com",
                UserName = "testTest",
                PhoneNumber = "+36301234567",
                FirstName = "User",
                LastName = "Test",
                Sex = "Female",
                DateOfBirth = new DateTime(1990, 01, 02),
                CurrentCityID = "1",
                Description = "Test",
                ObservedCityIDs = new string[] { "1", "2" },
                Password = "Password"
            };
            var fileMock = new Mock<IFormFile>();

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManager.Object, jwtHandler.Object, mapper);

            //Act
            var result = await controller.RegisterAsync(registerRequest, fileMock.Object);

            //Assert
            Assert.False(result.Success);
            Assert.False(context.Users.Any(x => x.UserName == registerRequest.UserName));
        }

       [Fact]
        public async Task RegisterAsync_TestFailAtCreate()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = GetContextWithSeedData();
            Mock<RepositoryManager> repositoryManager = new Mock<RepositoryManager>(context);
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null!);
            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null!);
            var failedIdentity = IdentityResult.Failed(new IdentityError[] { new IdentityError { Code = "0001", Description = "error test" } });
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(failedIdentity);

            var jwtHandler = new Mock<IJwtHandler>();
            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            RegisterRequestDTO registerRequest = new()
            {
                Email = "test@test.com",
                UserName = "testTest",
                PhoneNumber = "+36301234567",
                FirstName = "User",
                LastName = "Test",
                Sex = "Female",
                DateOfBirth = new DateTime(1990, 01, 02),
                CurrentCityID = "1",
                Description = "Test",
                ObservedCityIDs = new string[] { "1", "2" },
                Password = "Password"
            };
            var fileMock = new Mock<IFormFile>();

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManager.Object, jwtHandler.Object, mapper);

            //Act
            var result = await controller.RegisterAsync(registerRequest, fileMock.Object);

            //Assert
            Assert.False(result.Success);
            Assert.False(context.Users.Any(x => x.UserName == registerRequest.UserName));
        }

        [Theory]
        [InlineData("userTest")]
        [InlineData("adminTest")]
        public async Task GetProfileDetailsAsync_TestSuccess(string username)
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = GetContextWithSeedData();
            Mock<IRepositoryManager> repositoryManagerMock = new Mock<IRepositoryManager>();
            repositoryManagerMock.Setup(x => x.Users.GetUserConnectedAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>())).ReturnsAsync(new ApplicationUser
            {
                UserName = username,
                Animals = new List<Animal>()
            });
            repositoryManagerMock.Setup(x => x.Animals.GetAnimalsConnectedAsync(It.IsAny<List<Animal>>(), It.IsAny<bool>())).ReturnsAsync(new List<Animal>());

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(SeedData.users.First(x=>x.UserName == username));
            var jwtHandler = new Mock<IJwtHandler>();
            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManagerMock.Object, jwtHandler.Object, mapper);

            //Act
            var result = await controller.GetProfileDetailsAsync(username);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(username, result.UserName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("notExistTest")]
        public async Task GetProfileDetailsAsync_TestUserNotFoundFail(string username)
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = GetContextWithSeedData();
            Mock<IRepositoryManager> repositoryManagerMock = new Mock<IRepositoryManager>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            var jwtHandler = new Mock<IJwtHandler>();
            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManagerMock.Object, jwtHandler.Object, mapper);

            //Act
            var result = await controller.GetProfileDetailsAsync(username);

            //Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("user@user.com")]
        [InlineData("admin@admin.com")]
        public async Task GetAccountDetailsAsync_TestSuccess(string email)
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = GetContextWithSeedData();
            Mock<IRepositoryManager> repositoryManagerMock = new Mock<IRepositoryManager>();
            repositoryManagerMock.Setup(x => x.Users.GetUserConnectedAsync(It.IsAny<ApplicationUser>(), It.IsAny<bool>())).ReturnsAsync(new ApplicationUser
            {
                UserName = email,
            });

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(SeedData.users.First(x => x.Email == email));
            var jwtHandler = new Mock<IJwtHandler>();
            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManagerMock.Object, jwtHandler.Object, mapper);

            //Act
            var result = await controller.GetAccountDetailsAsync(email);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.UserName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("notExistTest")]
        public async Task GetAccountDetailsAsync_TestUserNotFoundFail(string username)
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = GetContextWithSeedData();
            Mock<IRepositoryManager> repositoryManagerMock = new Mock<IRepositoryManager>();

            var userManagerMock = new Mock<UserManager<ApplicationUser>>(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((ApplicationUser)null);
            var jwtHandler = new Mock<IJwtHandler>();
            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            AccountService controller = new AccountService(userManagerMock.Object, repositoryManagerMock.Object, jwtHandler.Object, mapper);
            
            //Act
            var result = await controller.GetAccountDetailsAsync(username);

            //Assert
            Assert.Null(result);
        }
    }
}
