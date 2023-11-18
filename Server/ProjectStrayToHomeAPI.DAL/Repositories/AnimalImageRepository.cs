using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;
using ProjectStrayToHomeAPI.Repositories.Interfaces;

namespace ProjectStrayToHomeAPI.Repositories
{
    public class AnimalImageRepository : RepositoryBase<Animal_Image, int>, IAnimalImageRepository
    {
        public AnimalImageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Animal_Image>> FindAnimalImagesByAnimalIDAsync(int animalID)
        {
            var result = await _context.Animal_Images.AsNoTracking().Where(x => x.AnimalID == animalID).ToListAsync();
            if (result.Count == 0)
                //TODO: Implement my own exception
                throw new InvalidOperationException("Sequence contains no elements");

            return result;
        }

        public async Task SetAnimalImagesAsync(List<IFormFile> animalImagesToAdd, int animalID)
        {
            List<Animal_Image> animalImagesToRemove = await _context.Animal_Images.Where(x => x.AnimalID == animalID).ToListAsync();
            foreach (var animalImage in animalImagesToRemove)
            {
                Delete(animalImage);
            }
            foreach(var image in animalImagesToAdd)
            {
                using MemoryStream memoryStream = new MemoryStream();
                var dateImageNow = DateTime.Now;
                image.CopyTo(memoryStream);

                var animalImage = new Animal_Image();
                animalImage.AnimalID = animalID;
                animalImage.Image = memoryStream.ToArray();
                animalImage.CreateDate = dateImageNow;
                animalImage.UpdateDate = dateImageNow;

                Create(animalImage);
            }
        }
    }
}
