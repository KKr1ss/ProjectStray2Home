using ProjectStray2HomeAPI.Data;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Repositories
{
    public class AnimalImageRepositoryTest : BaseTest
    {
        [Fact]
        public async Task FindAnimalImagesByAnimalIDAsync()
        {
            //Arrange
            using ApplicationDbContext context = GetContextWithSeedData();

            AnimalImageRepository repository = new(context);

            //Act
            var result = await repository.FindAnimalImagesByAnimalIDAsync(2);

            //Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(2, result.First().AnimalID);
        }
    }
}
