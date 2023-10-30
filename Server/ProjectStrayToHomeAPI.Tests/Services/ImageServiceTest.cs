using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Services
{
    public class ImageServiceTest : BaseTest
    {
        [Fact]
        public async Task GetUserImageAsync_TestSuccess()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            Mock<UserManager<ApplicationUser>> userManagerMock = new(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(SeedData.users.First());
            
            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(X => X.UserImages.FindUserImageByUserIDAsync(It.IsAny<string>())).ReturnsAsync(new ApplicationUser_Image { Image = new byte[] { } });

            var controller = new ImageService(userManagerMock.Object, repositoryManagerMock.Object);

            string userName = "userTest";
            //Act
            var result = await controller.GetUserImageAsync(userName);

            //Assert
            Assert.IsType<FileStreamResult>(result);
        }

        [Fact]
        public async Task GetUserImageAsync_TestImageNotFound()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            Mock<UserManager<ApplicationUser>> userManagerMock = new(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(SeedData.users.First());

            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(X => X.UserImages.FindUserImageByUserIDAsync(It.IsAny<string>())).Throws<KeyNotFoundException>();

            var controller = new ImageService(userManagerMock.Object, repositoryManagerMock.Object);

            string userName = "userTest";
            //Act
            Task result() => controller.GetUserImageAsync(userName);

            //Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(result);
        }

        [Fact]
        public async Task GetAnimalImageAsync_TestSuccessNoImageID()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            Mock<UserManager<ApplicationUser>> userManagerMock = new(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(X => X.AnimalImages.FindAnimalImagesByAnimalIDAsync(It.IsAny<int>())).ReturnsAsync(new List<Animal_Image>(SeedData.animalImages));

            var controller = new ImageService(userManagerMock.Object, repositoryManagerMock.Object);
            //Act
            var result = await controller.GetAnimalImageAsync(2, null);

            //Assert
            Assert.IsType<FileStreamResult>(result);
        }

        [Fact]
        public async Task GetAnimalImageAsync_TestSuccessWithImageID()
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            Mock<UserManager<ApplicationUser>> userManagerMock = new(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(X => X.AnimalImages.FindAnimalImagesByAnimalIDAsync(It.IsAny<int>())).ReturnsAsync(new List<Animal_Image>(SeedData.animalImages));

            var controller = new ImageService(userManagerMock.Object, repositoryManagerMock.Object);
            //Act
            var result = await controller.GetAnimalImageAsync(2, 2);

            //Assert
            Assert.IsType<FileStreamResult>(result);
        }

        [Theory]
        [InlineData(3, null)]
        [InlineData(3, 2)]
        public async Task GetAnimalImageAsync_TestImageNotFound(int animalID, int? imageID)
        {
            //Arrange
            //setup dependencies
            using ApplicationDbContext context = new(options);
            Mock<UserManager<ApplicationUser>> userManagerMock = new(new UserStore<ApplicationUser>(context), null, null, null, null, null, null, null, null);
            Mock<IRepositoryManager> repositoryManagerMock = new();
            repositoryManagerMock.Setup(X => X.AnimalImages.FindAnimalImagesByAnimalIDAsync(It.IsAny<int>())).Throws<InvalidOperationException>();

            var controller = new ImageService(userManagerMock.Object, repositoryManagerMock.Object);

            //Act
            Task result() => controller.GetAnimalImageAsync(animalID, imageID);

            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(result);
        }
    }
}
