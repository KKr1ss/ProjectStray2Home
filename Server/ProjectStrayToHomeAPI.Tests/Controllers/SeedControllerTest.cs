using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectStrayToHomeAPI.Controllers;
using ProjectStrayToHomeAPI.Services.Interfaces;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Controllers
{
    public class SeedControllerTest : BaseTest
    {
        [Fact]
        public async Task getImportData_TestSuccess()
        {
            //Arrange
            //setup dependencies
            Mock<ISeedService> seedServiceMock = new();
            ILogger<SeedController> logger = new Mock<ILogger<SeedController>>().Object;
            seedServiceMock.Setup(x => x.ImportDataAsync()).ReturnsAsync(new JsonResult("test"));

            var controller = new SeedController(seedServiceMock.Object, logger);

            //Act
            var result = await controller.ImportData();
            JsonResult? actual = result as JsonResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("test", actual?.Value?.ToString());
        }

        [Fact]
        public async Task getImportData_TestFailedCitiesExist()
        {
            //Arrange
            //setup dependencies
            Mock<ISeedService> seedServiceMock = new();
            ILogger<SeedController> logger = new Mock<ILogger<SeedController>>().Object;
            seedServiceMock.Setup(x => x.ImportDataAsync()).Throws<ArgumentException>();

            var controller = new SeedController(seedServiceMock.Object, logger);

            //Act
            var result = await controller.ImportData();
            JsonResult? actual = result as JsonResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Contains("Seed Error", ((JsonResult)result).Value!.ToString());
        }

        [Fact]
        public async Task getImportAdmin_TestSuccess()
        {
            //Arrange
            Mock<ISeedService> seedServiceMock = new();
            ILogger<SeedController> logger = new Mock<ILogger<SeedController>>().Object;
            seedServiceMock.Setup(x => x.ImportAdminAsync()).ReturnsAsync(new JsonResult("test"));

            var controller = new SeedController(seedServiceMock.Object, logger);

            //Act
            var result = await controller.ImportAdmin();
            JsonResult? actual = result as JsonResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Equal("test", actual?.Value);
        }

        [Fact]
        public async Task getImportAdmin_TestFailed()
        {
            //Arrange
            //setup dependencies
            Mock<ISeedService> seedServiceMock = new();
            ILogger<SeedController> logger = new Mock<ILogger<SeedController>>().Object;
            seedServiceMock.Setup(x => x.ImportAdminAsync()).Throws<ArgumentException>();

            var controller = new SeedController(seedServiceMock.Object, logger);

            //Act
            var result = await controller.ImportAdmin();
            JsonResult? actual = result as JsonResult;

            //Assert
            Assert.NotNull(actual);
            Assert.Contains("Seed Error", ((JsonResult)result).Value!.ToString());
        }
    }
}
