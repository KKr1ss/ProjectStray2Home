using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;

namespace ProjectStrayToHomeAPI.Tests.Repositories
{
    public class AnimalCommentRepositoryTest : BaseTest
    {
        [Fact]
        public async Task GetAnimalComment_Test()
        {
            //Arrange
            using ApplicationDbContext context = new(options);
            Animal_Comment comment = new()
            {
                AnimalID = 1,
                UserID = "1",
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Comment = "Szép állat"
            };
            context.Add(comment);
            context.SaveChanges();

            AnimalCommentRepository repository = new(context);

            //Act
            var result = await repository.FindAllAsync();

            //Assert
            Assert.Single(result);
            Assert.Equal(comment.Comment, result.First().Comment);
        }
    }
}
