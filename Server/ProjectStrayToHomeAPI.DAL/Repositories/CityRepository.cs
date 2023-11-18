using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Repositories.Base;
using ProjectStrayToHomeAPI.Repositories.Interfaces;

namespace ProjectStrayToHomeAPI.Repositories
{
    public class CityRepository : RepositoryBase<City, int>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context)
            : base(context)
        {
            
        }

        public async Task SetObservedCitiesForUserAsync(string userId, int[] cityIds)
        {
            List<ApplicationUser_City> cities = new();
            foreach (var cityId in cityIds)
            {
                cities.Add(new ApplicationUser_City
                {
                    CityID = cityId,
                    UserID = userId
                });
            }
            await _context.AddRangeAsync(cities);
        }

        public List<City> GetObservedCitiesByUser(string userId)
        {
            List<City> cities = _context.User_Cities.AsNoTracking().Where(x => x.UserID==userId).Include(x => x.City).Select(x=> x.City ).ToList();

            return cities;
        }
    }
}
