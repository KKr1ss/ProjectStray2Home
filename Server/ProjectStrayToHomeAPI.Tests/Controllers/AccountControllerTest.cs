using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectStray2HomeAPI.Controllers;
using ProjectStrayToHomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.User;
using ProjectStrayToHomeAPI.Services.Interfaces;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;
using System.Security.Claims;
using System.Security.Principal;

namespace ProjectStrayToHomeAPI.Tests.Controllers
{
    [Collection("Sequential")]
    public class AccountControllerTest
    {


        [Fact]
        public async Task Login_TestSuccess()
        {
            //Arrange
            LoginResultDTO loginResultDTO = new() { Message = "test", Success = true, UserName = "test" };

            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDTO>())).ReturnsAsync(loginResultDTO);

            ILogger<AccountController> logger = new Mock<ILogger<AccountController>>().Object;

            AccountController controller = new(accountService.Object, logger);

            //Act
            var result = await controller.Login(new LoginRequestDTO()) as OkObjectResult;
            var value = result?.Value as LoginResultDTO;

            //Assert
            Assert.NotNull(value);
            Assert.Equal(200, result.StatusCode);
            Assert.True(value.Success);
        }

        [Fact]
        public async Task Login_TestFailedEmailOrPassword()
        {
            //Arrange
            LoginResultDTO loginResultDTO = new() { Message = "test", Success = false, UserName = "test" };

            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.LoginAsync(It.IsAny<LoginRequestDTO>())).ReturnsAsync(loginResultDTO);

            ILogger<AccountController> logger = new Mock<ILogger<AccountController>>().Object;

            AccountController controller = new(accountService.Object, logger);

            //Act
            var result = await controller.Login(new LoginRequestDTO()) as UnauthorizedObjectResult;
            var value = result?.Value as LoginResultDTO;

            //Assert
            Assert.NotNull(value);
            Assert.Equal(401, result.StatusCode);
            Assert.False(value.Success);
        }

        [Fact]
        public async Task Register_TestSuccess()
        {
            //Arrange
            APIResultDTO apiResultDTO = new()
            {
                Success = true,
                Message = "test",
            };
            var fileMock = new Mock<IFormFile>();

            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.RegisterAsync(It.IsAny<RegisterRequestDTO>(), It.IsAny<IFormFile?>())).ReturnsAsync(apiResultDTO);

            ILogger<AccountController> logger = new Mock<ILogger<AccountController>>().Object;

            AccountController controller = new(accountService.Object, logger);

            //Act
            var result = await controller.Register(new RegisterRequestDTO(), fileMock.Object) as OkObjectResult;
            var value = result?.Value as APIResultDTO;

            //Assert
            Assert.NotNull(value);
            Assert.Equal(200, result.StatusCode);
            Assert.True(value.Success);
        }

        [Fact]
        public async Task Register_TestFailedAttempt()
        {
            //Arrange
            APIResultDTO apiResultDTO = new()
            {
                Success = false,
                Message = "test",
            };
            var fileMock = new Mock<IFormFile>();

            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.RegisterAsync(It.IsAny<RegisterRequestDTO>(), It.IsAny<IFormFile?>())).ReturnsAsync(apiResultDTO);

            ILogger<AccountController> logger = new Mock<ILogger<AccountController>>().Object;

            AccountController controller = new(accountService.Object, logger);

            //Act
            var result = await controller.Register(new RegisterRequestDTO(), fileMock.Object) as BadRequestObjectResult;
            var value = result?.Value as APIResultDTO;

            //Assert
            Assert.NotNull(value);
            Assert.Equal(400, result.StatusCode);
            Assert.False(value.Success);
        }

        [Fact]
        public async Task GetProfileDetails_TestSuccess()
        {
            //Arrange
            string username = "userName";

            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.GetProfileDetailsAsync(It.IsAny<string>())).ReturnsAsync(new UserProfileDTO() { UserName = username });

            ILogger<AccountController> logger = new Mock<ILogger<AccountController>>().Object;

            AccountController controller = new(accountService.Object, logger);

            //Act
            var result = await controller.GetProfileDetails(username) as OkObjectResult;
            var value = result?.Value as UserProfileDTO;

            //Assert
            Assert.NotNull(value);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(username, value.UserName);
        }

        [Fact]
        public async Task GetProfileDetails_TestUserNotFound()
        {
            //Arrange
            string username = "userName";
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.GetProfileDetailsAsync(It.IsAny<string>())).ReturnsAsync((UserProfileDTO)null);

            ILogger<AccountController> logger = new Mock<ILogger<AccountController>>().Object;

            AccountController controller = new(accountService.Object, logger);

            //Act
            var result = await controller.GetProfileDetails(username) as NotFoundObjectResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetAccountDetails_TestSuccess()
        {
            //Arrange
            string email = "user@user.com";
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.GetAccountDetailsAsync(It.IsAny<string>())).ReturnsAsync(new UserProfileDTO() { Email = "user@user.com" });

            ILogger<AccountController> logger = new Mock<ILogger<AccountController>>().Object;

            var identity = new GenericIdentity(email, "test");
            var contextUser = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext()
            {
                User = contextUser
            };
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            AccountController controller = new(accountService.Object, logger)
            {
                ControllerContext = controllerContext
            };
            
            //Act
            var result = await controller.GetAccountDetails() as OkObjectResult;
            var value = result?.Value as UserProfileDTO;

            //Assert
            Assert.NotNull(value);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(email, value.Email);
        }

        [Fact]
        public async Task GetAccountDetails_TestUserNotFound()
        {
            //Arrange
            string email = "user@user.com";
            Mock<IAccountService> accountService = new Mock<IAccountService>();
            accountService.Setup(x => x.GetAccountDetailsAsync(It.IsAny<string>())).ReturnsAsync((UserProfileDTO)null);

            ILogger<AccountController> logger = new Mock<ILogger<AccountController>>().Object;

            var identity = new GenericIdentity(email, "test");
            var contextUser = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext()
            {
                User = contextUser
            };
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            AccountController controller = new(accountService.Object, logger)
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = await controller.GetAccountDetails() as NotFoundObjectResult;

            //Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Hiba: felhasználó nem található", result.Value);
        }
    }
}
