using Microsoft.Extensions.Logging;
using Moq;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Controllers;
using ProjectStrayToHomeAPI.Repositories;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectStrayToHomeAPI.Tests.Repositories
{
    //TODO : Implement tests
    public class RepositoryManagerTest : BaseTest
    {
        [Fact]
        public async Task saveAsync_Test()
        {
            //Arrange
            List<City> cityList = new List<City>()
            {
                new City { Name = "TEST CITY", CreateDate = new DateTime(2023, 07, 01), UpdateDate = new DateTime(2023, 07, 01) },
                new City { Name = "TEST CITY 2", CreateDate = new DateTime(2023, 07, 11), UpdateDate = new DateTime(2023, 07, 12) }
            };

            using var _context = GetContextWithSeedData();
            await _context.SaveChangesAsync();

            ILogger<RepositoryManager> _logger = new Mock<ILogger<RepositoryManager>>().Object;

            var repositoryManager = new RepositoryManager(_context);

            //Act
            foreach (var city in cityList)
            {
                repositoryManager.Cities.Create(city);
            }
            var result = await repositoryManager.SaveAsync();

            //Assert
            Assert.Equal(2, result);
        }
    }
}
