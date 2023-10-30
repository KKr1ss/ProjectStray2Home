using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;
using ProjectStrayToHomeAPI.Repositories.Interfaces;

namespace ProjectStrayToHomeAPI.Repositories
{
    public class AnimalCommentRepository : RepositoryBase<Animal_Comment, int>, IAnimalCommentRepository
    {
        public AnimalCommentRepository(ApplicationDbContext context)
            : base(context)
        {

        }
    }
}
