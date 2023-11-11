using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Repositories
{
    public class AnimalRepositoryTest : BaseTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetAnimalConnectedAsync_TestSuccess(bool withImage)
        {
            //Arrange
            using ApplicationDbContext context = GetContextWithSeedData();

            AnimalRepository repository = new(context);
            Animal animal = SeedData.animals.First();

            //Act
            var result = await repository.GetAnimalConnectedAsync(animal, withImage);

            //Assert
            Assert.Equal(animal.Name, result.Name);
            Assert.Equal(animal.CityID, result.City?.Id);
        }

        ///TODO: REPAIR TEST
        //[Theory]
        //[InlineData(0)]
        //[InlineData(1)]
        //public async Task GetPaginatedAnimalsAsync_TestPaginated(int startIndex)
        //{
        //    //Arrange
        //    using ApplicationDbContext context = GetContextWithSeedData();

        //    AnimalRepository repository = new(context);
        //    Animal animal = SeedData.animals[startIndex*2];

        //    //Act
        //    var result = repository.GetPaginatedAnimals(startIndex, 2);

        //    //Assert
        //    Assert.Equal(2-startIndex, result.Count());
        //    Assert.Equal(animal.Name, result.First().Name);
        //}
    }
}
