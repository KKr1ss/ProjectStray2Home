using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Repositories
{
    public class UserRepositoryTest : BaseTest
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task GetUserConnectedAsync_Test(bool withImage)
        {
            //Arrange
            using ApplicationDbContext context = GetContextWithSeedData();
            
            UserRepository repository = new(context);
            ApplicationUser user = SeedData.users.First();

            //Act
            var result = await repository.GetUserConnectedAsync(user,withImage);

            //Assert
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.CurrentCityID, result.City?.Id);
        }
    }
}
