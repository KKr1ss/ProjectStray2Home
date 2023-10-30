using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Repositories
{
    public class UserImageRepositoryTest : BaseTest
    {
        [Fact]
        public async Task FindUserImageByUserIDAsync()
        {
            //Arrange
            using ApplicationDbContext context = GetContextWithSeedData();

            UserImageRepository repository = new(context);

            //Act
            var result = await repository.FindUserImageByUserIDAsync("437cc5d5-2425-435a-8a3f-18c692113636");

            //Assert
            Assert.IsType<ApplicationUser_Image>(result);
            Assert.Equal("437cc5d5-2425-435a-8a3f-18c692113636", result.ApplicationUserID);
        }
    }
}
