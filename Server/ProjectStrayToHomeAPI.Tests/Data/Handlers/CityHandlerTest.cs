using Microsoft.AspNetCore.Hosting;
using Moq;
using ProjectStrayToHomeAPI.Data.Handlers;

namespace ProjectStrayToHomeAPI.Tests.Data.Handlers
{
    public class CityHandlerTest
    {
        [Fact]
        public void getCities_TestRead()
        {
            //Arrange
            var webHostEnvironment = new Mock<IWebHostEnvironment>();
            webHostEnvironment.Setup(x => x.ContentRootPath).Returns(Directory.GetCurrentDirectory());

            //Act
            var result = CityHandler.getCities(webHostEnvironment.Object.ContentRootPath);

            //Assert
            Assert.Equal(3178, result.Count());
        }
    }
}
