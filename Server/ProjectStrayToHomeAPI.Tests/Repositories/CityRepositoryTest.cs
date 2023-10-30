using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectStray2HomeAPI.Data;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Controllers;
using ProjectStrayToHomeAPI.Tests.UnitTestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectStrayToHomeAPI.Repositories;

namespace ProjectStrayToHomeAPI.Tests.Repositories
{
    public class CityRepositoryTest : BaseTest
    {
        [Fact]
        public async Task FindByID_Test()
        {
            //Arrange
            int existID = 1, nonExistID = 2;

            using var _context = new ApplicationDbContext(options);
            var cityToAdd = SeedData.cities.First();
            _context.Cities.Add(cityToAdd);
            await _context.SaveChangesAsync();

            var cityRepository = new CityRepository(_context);
            //Act
            City foundCity = await cityRepository.FindByIDAsync(existID);
            Task notFoundResult() => cityRepository.FindByIDAsync(nonExistID);

            //Assert
            Assert.Equal(cityToAdd.Name, foundCity.Name);
            await Assert.ThrowsAsync<KeyNotFoundException>(notFoundResult);
        }

        [Fact]
        public async Task FindAll_Test()
        {
            //Arrange
            using var _context = new ApplicationDbContext(options);
            _context.Cities.AddRange(SeedData.cities);
            await _context.SaveChangesAsync();

            var cityRepository = new CityRepository(_context);
            //Act
            var foundCities = await cityRepository.FindAllAsync();

            //Assert
            Assert.Equal(3, foundCities.Count());
        }

        [Fact]
        public async Task FindByCondition_Test()
        {
            //Arrange
            using var _context = new ApplicationDbContext(options);
            _context.Cities.AddRange(SeedData.cities);
            await _context.SaveChangesAsync();

            var cityRepository = new CityRepository(_context);
            //Act
            var foundCitiesByID = await cityRepository.FindByConditionAsync(x=>x.Id == 1);
            var foundCitiesByName = await cityRepository.FindByConditionAsync(x => x.Name.Contains("CITY"));

            //Assert
            Assert.Single(foundCitiesByID);
            Assert.Equal(3, foundCitiesByName.Count());
        }

        [Fact]
        public async Task CreateCity_Test()
        {
            //Arrange
            using var _context = new ApplicationDbContext(options);

            var cityToAdd = SeedData.cities.First();

            var cityRepository = new CityRepository(_context);
            //Act
            cityRepository.Create(cityToAdd);
            await _context.SaveChangesAsync();

            //Assert
            Assert.Equal(1, _context.Cities.Count());
        }

        [Fact]
        public async Task UpdateCity_Test()
        {
            //Arrange
            using var _context = new ApplicationDbContext(options);

            var cityToUpdate = SeedData.cities.First();
            _context.Cities.Add(cityToUpdate);
            await _context.SaveChangesAsync();

            string testName = "UPDATED CITY";
            cityToUpdate = _context.Cities.First();

            cityToUpdate.Name = testName;
            var cityRepository = new CityRepository(_context);
            //Act
            cityRepository.Update(cityToUpdate);
            await _context.SaveChangesAsync();

            //Assert
            Assert.Equal(1, _context.Cities.Count());
            Assert.Equal(testName, cityToUpdate.Name);
        }

        [Fact]
        public async Task DeleteCity_Test()
        {
            //Arrange
            using var _context = new ApplicationDbContext(options);

            var cityToDelete = SeedData.cities.First();
            _context.Cities.Add(cityToDelete);
            await _context.SaveChangesAsync();

            cityToDelete = _context.Cities.First();

            var cityRepository = new CityRepository(_context);
            //Act
            cityRepository.Delete(cityToDelete);
            await _context.SaveChangesAsync();

            //Assert
            Assert.Equal(0, _context.Cities.Count());
        }

    }


    //    [Fact]
    //    public async Task CreateCity_Test()
    //    {
    //        //Arrange
    //        using var _context = new ApplicationDbContext(options);

    //        //Act

    //        //Assert
    //    }
}

