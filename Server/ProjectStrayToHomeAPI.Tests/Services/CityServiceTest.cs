using AutoMapper;
using Moq;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Services
{
    public class CityServiceTest
    {
        [Fact]
        public async Task GetCitiesAsync_TestSuccess()
        {
            //Arrange
            //setup dependencies
            List<City> list = new() { new City { Id = 1, Name = "Test" } };
            Mock<IRepositoryManager> repositoryManagerMock = new Mock<IRepositoryManager>();
            repositoryManagerMock.Setup(x => x.Cities.FindAllAsync()).ReturnsAsync(list);
            IMapper mapper = new Mock<Mapper>(ArrangeHelper.MapperConfiguration).Object;

            var controller = new CityService(repositoryManagerMock.Object, mapper);

            //Act
            var result = await controller.GetCitiesAsync();

            //Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }
    }
}
