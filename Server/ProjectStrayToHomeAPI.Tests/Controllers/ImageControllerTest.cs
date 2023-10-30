using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Controllers;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Services.Interfaces;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;
using Serilog.Core;
using System.IO;

namespace ProjectStrayToHomeAPI.Tests.Controllers
{
    public class ImageControllerTest : BaseTest
    {
        [Fact]
        public async Task GetUserImage_GetImage()
        {
            //Arrange
            //setup dependencies
            Mock<IImageService> imageServiceMock = new();
            var fileStreamResult = new FileStreamResult(new MemoryStream(), "application/octet-stream");
            imageServiceMock.Setup(x => x.GetUserImageAsync(It.IsAny<string>())).ReturnsAsync(fileStreamResult);

            ILogger<ImageController> logger = new Mock<ILogger<ImageController>>().Object;

            var controller = new ImageController(imageServiceMock.Object, logger);
            string userName = "userTest";
            //Act
            var result = await controller.GetUserImage(userName);

            //Assert
            Assert.IsType<FileStreamResult>(result);
        }

        [Fact]
        public async Task GetUserImage_GetNonExistingImage()
        {
            //Arrange
            //setup dependencies
            Mock<IImageService> imageServiceMock = new();
            var fileStreamResult = new FileStreamResult(new MemoryStream(), "application/octet-stream");
            var userName = "nonExists";
            imageServiceMock.Setup(x => x.GetUserImageAsync(It.IsAny<string>())).Throws<KeyNotFoundException>();

            ILogger<ImageController> logger = new Mock<ILogger<ImageController>>().Object;

            var controller = new ImageController(imageServiceMock.Object, logger);
            //Act
            var result = await controller.GetUserImage(userName);
            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((ObjectResult)result).StatusCode);
        }

        [Theory]
        [InlineData(2, null)]
        [InlineData(2, 2)]
        public async Task GetAnimalImage_GetImage(int animalID, int? imageID)
        {
            //Arrange
            //setup dependencies
            Mock<IImageService> imageServiceMock = new();
            var fileStreamResult = new FileStreamResult(new MemoryStream(), "application/octet-stream");
            imageServiceMock.Setup(x => x.GetAnimalImageAsync(It.IsAny<int>(), It.IsAny<int?>())).ReturnsAsync(fileStreamResult);

            ILogger<ImageController> logger = new Mock<ILogger<ImageController>>().Object;

            var controller = new ImageController(imageServiceMock.Object, logger);
            //Act
            var result = await controller.GetAnimalImage(animalID, imageID);

            //Assert
            Assert.IsType<FileStreamResult>(result);
        }

        [Theory]
        [InlineData(3, null)]
        [InlineData(3, 2)]
        public async Task GetAnimalImage_GetNonExistingImage(int animalID, int? imageID)
        {
            //Arrange
            //setup dependencies
            Mock<IImageService> imageServiceMock = new();
            var fileStreamResult = new FileStreamResult(new MemoryStream(), "application/octet-stream");
            imageServiceMock.Setup(x => x.GetAnimalImageAsync(It.IsAny<int>(), It.IsAny<int?>())).Throws<InvalidOperationException>();

            ILogger<ImageController> logger = new Mock<ILogger<ImageController>>().Object;

            var controller = new ImageController(imageServiceMock.Object, logger);
            //Act
            var result = await controller.GetAnimalImage(animalID, imageID);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, ((ObjectResult)result).StatusCode);
        }
    }
}
