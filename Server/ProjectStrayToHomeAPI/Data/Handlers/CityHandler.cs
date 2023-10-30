using ProjectStray2HomeAPI.Models.EF;
using System.Security;

namespace ProjectStrayToHomeAPI.Data.Handlers
{
    public class CityHandler
    {
        public static List<City> getCities( string contentRootPath)
        {
            var path = Path.Combine(contentRootPath, "Data/DataFiles/HungarianCities.txt");
            string[] citiesTextFileLines = File.ReadAllLines(path);

            var cities = new List<City>();
            foreach (string cityLine in citiesTextFileLines)
            {
                cities.Add(new City
                {
                    Name = cityLine,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                });
            }

            return cities;
        }

    }
}
