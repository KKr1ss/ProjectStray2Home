using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectStray2HomeAPI.Models;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Controllers;
using ProjectStrayToHomeAPI.Data.Handlers;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStrayToHomeAPI.Services
{
    public class SeedService : ISeedService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SeedService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IRepositoryManager repositoryManager, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _repositoryManager = repositoryManager;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<JsonResult> ImportDataAsync()
        {
            
                // checks if the dataabse emtpy
                if ((await _repositoryManager.Cities.FindAllAsync()).Count() != 0)
                    throw new ArgumentException("Method has been called already!");

                // create the cities
                int citiesResult = await ImportCities();

                // create the default roles (if they don't exist yet)
                int rolesResult = await ImportRoles();

                return new JsonResult(new
                {
                    citiesAdded = citiesResult,
                    rolesAdded = rolesResult
                });
        }

        private async Task<int> ImportCities()
        {
            var cityList = CityHandler.getCities(_webHostEnvironment.ContentRootPath);

            foreach (var city in cityList)
            {
                _repositoryManager.Cities.Create(city);
            }

            return await _repositoryManager.SaveAsync();
        }

        private async Task<int> ImportRoles()
        {
            int roleCounter = 0;
            if (await _roleManager.FindByNameAsync(Roles.User.ToString()) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
                roleCounter++;
            }

            if (await _roleManager.FindByNameAsync(Roles.Admin.ToString()) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                roleCounter++;
            }

            return roleCounter;
        }


        public async Task<JsonResult> ImportAdminAsync()
        {
            if ((await _repositoryManager.Cities.FindAllAsync()).Count() < 3)
                throw new ArgumentException("Call ImportData first!");

            string admin_name = "seedAdmin";
            string admin_email = "admin@admin.com";
            var admin = new ApplicationUser
            {
                Email = admin_email,
                NormalizedEmail = admin_email,
                UserName = admin_name,
                NormalizedUserName = admin_name,
                PhoneNumber = "+36207654321",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FirstName = "Admin",
                LastName = "Test",
                Sex = Sex.Male,
                DateOfBirth = new DateTime(1995, 10, 07),
                CurrentCityID = 2,
                CreateDate = new DateTime(2023, 07, 10),
                UpdateDate = new DateTime(2023, 07, 16)
            };

            if (await _userManager.FindByNameAsync(admin.UserName) != null || await _userManager.FindByEmailAsync(admin.Email) != null)
                throw new ArgumentException("Admin user already exists!");

            var result = await _userManager.CreateAsync(admin, "Password99#");
            if (!result.Succeeded)
                throw new ArgumentException("Error at creation!");

            // assign the roles to the admin
            if (!await _userManager.IsInRoleAsync(admin, Roles.User.ToString()))
                await _userManager.AddToRoleAsync(admin, Roles.User.ToString());
            if (!await _userManager.IsInRoleAsync(admin, Roles.Admin.ToString()))
                await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString());

            await _repositoryManager.SaveAsync();

            return new JsonResult("Admin been seeded to database!");

        }
    }
}
