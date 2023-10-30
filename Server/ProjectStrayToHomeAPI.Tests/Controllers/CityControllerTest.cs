using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectStray2HomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Controllers;
using ProjectStrayToHomeAPI.Services.Interfaces;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Controllers
{
    public class CityControllerTest : BaseTest
    {
        [Fact]
        public async Task GetCities_TestSuccess()
        {
            //Arrange
            //setup dependencies
            var mockList = new Mock<IEnumerable<CityDTO>>();
            using var _context = GetContextWithSeedData();
            ILogger<CityController> logger = new Mock<ILogger<CityController>>().Object;
            var cityServiceMock = new Mock<ICityService>();
            cityServiceMock.Setup(x => x.GetCitiesAsync()).ReturnsAsync(mockList.Object);

            var controller = new CityController(cityServiceMock.Object, logger);

            //Act
            var result = (await controller.GetCities()) as OkObjectResult;

            //Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
