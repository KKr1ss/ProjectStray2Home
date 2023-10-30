using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;
using ProjectStrayToHomeAPI.Repositories.Interfaces;

namespace ProjectStrayToHomeAPI.Repositories
{
    public class AnimalRepository : RepositoryBase<Animal, int>, IAnimalRepository
    {
        public AnimalRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<List<Animal>> GetAnimalsConnectedAsync(List<Animal> animals, bool withImages)
        {
            if (animals == null)
                return new List<Animal>();
            List<Animal> connectedAnimals = new List<Animal>();
            foreach (var animal in animals)
            {
                var connectedAnimal = await GetAnimalConnectedAsync(animal, withImages);
                connectedAnimals.Add(connectedAnimal);
            }

            return connectedAnimals;
        }

        public async Task<Animal> GetAnimalConnectedAsync(Animal animal, bool withImages)
        {
            Animal animalToReturn = await _context.Animals.FirstAsync(x => x.Id == animal.Id);
            animalToReturn.User = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == animalToReturn.UserID);
            animalToReturn.City = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == animalToReturn.CityID);
            animalToReturn.Comments = await _context.Animal_Comments.AsNoTracking().Where(x => x.AnimalID == animalToReturn.Id).Include(x=>x.User).OrderByDescending(x=>x.CreateDate).ToListAsync();
            if (withImages)
                animalToReturn.Images = await _context.Animal_Images.AsNoTracking().Where(x => x.AnimalID == animalToReturn.Id).OrderBy(x => x.CreateDate).ToListAsync();

            return animalToReturn;
        }

        public async Task<List<Animal>> GetPaginatedAnimalsAsync(int pageIndex, int pageSize)
        {
            var result = await _context.Animals.AsNoTracking()
                .OrderByDescending(x=>x.StatusDate)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return result;
        }
    }
}
