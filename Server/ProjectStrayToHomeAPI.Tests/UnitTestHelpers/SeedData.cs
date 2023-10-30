using Microsoft.AspNetCore.Identity;
using ProjectStray2HomeAPI.Models;
using ProjectStray2HomeAPI.Models.EF;

namespace ProjectStrayToHomeAPI.Tests.UnitTestHelpers
{
    public class SeedData
    {
        public static List<City> cities = new()
        {
            new City { Id = 1, Name = "TEST CITY", CreateDate = new DateTime(2023, 07, 01), UpdateDate = new DateTime(2023, 07, 01) },
            new City { Id = 2, Name = "TEST CITY 2", CreateDate = new DateTime(2023, 07, 11), UpdateDate = new DateTime(2023, 07, 12) },
            new City { Id = 3, Name = "TEST CITY 3", CreateDate = new DateTime(2023, 07, 12), UpdateDate = new DateTime(2023, 07, 12) }
        };

        public static List<IdentityRole> roles = new()
        {
            new IdentityRole
            {
                Id = "59c0dc2c-4519-49d8-9585-6486ee9eb891",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "ba75124b-e142-46e4-8bd5-d810423ddc0a"
            },
            new IdentityRole
            {
                Id = "1261aec4-94b4-4e98-87c2-cf89126825c6",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "d602e507-3fbb-4ad7-b6a7-7b57e6993dc1"
            }
        };

        public static List<ApplicationUser> users = new()
        {
            new ApplicationUser
            {
                Id = "437cc5d5-2425-435a-8a3f-18c692113636",
                Email = "user@user.com",
                NormalizedEmail = "USER@USER.COM",
                UserName = "userTest",
                NormalizedUserName = "USERTEST",
                PhoneNumber = "+36301234567",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FirstName = "User",
                LastName = "Test",
                Sex = Sex.Female,
                DateOfBirth = new DateTime(1990, 01, 02),
                CurrentCityID = 1,
                Description = "ITS ME",
                CreateDate = new DateTime(2023, 07, 12),
                UpdateDate = new DateTime(2023, 07, 17),
                PasswordHash = "AQAAAAEAACcQAAAAEIZpJ0CvRQmeaZ8CrwO6ZTH9K1vI7bQ/IsACQrmPhnQXRKahPphiMK9lWp5c8fXx5Q=="
            },
            new ApplicationUser
            {
                Id = "25a66a77-6970-4bc5-ba4d-4f834fd1e4ab",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                UserName = "adminTest",
                NormalizedUserName = "ADMINTEST",
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
                UpdateDate = new DateTime(2023, 07, 16),
                PasswordHash = "AQAAAAEAACcQAAAAEB4nUyI4u5lCVkGNkkVUaeE1S7f8wBYf5zcJKcE0yYNC1hftLRUAwwKNIYsuYVfxCA=="
            }
        };

        public static List<IdentityUserRole<string>> userRoles = new()
        {
            new IdentityUserRole<string>
            {
                UserId = "25a66a77-6970-4bc5-ba4d-4f834fd1e4ab",
                RoleId = "1261aec4-94b4-4e98-87c2-cf89126825c6"
            },
            new IdentityUserRole<string>
            {
                UserId = "25a66a77-6970-4bc5-ba4d-4f834fd1e4ab",
                RoleId = "59c0dc2c-4519-49d8-9585-6486ee9eb891"
            },
            new IdentityUserRole<string>
            {
                UserId = "437cc5d5-2425-435a-8a3f-18c692113636",
                RoleId = "59c0dc2c-4519-49d8-9585-6486ee9eb891"
            }
        };

        public static List<ApplicationUser_Image> userImages = new()
        {
            new ApplicationUser_Image
            {
                Id = 1,
                ApplicationUserID = "437cc5d5-2425-435a-8a3f-18c692113636",
                Image = new byte[] {},
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                RowVersion = new byte[] {}
            }
        };

        public static List<Animal> animals = new()
            {
                new Animal
                {
                    Id = 1,
                    Type = Animal_Type.Cat,
                    Breed = "TESTKeverek",
                    Name = "CATTestHome",
                    Sex = Sex.Male,
                    DateOfBirth = new DateTime(2022,06,05),
                    Characteristic = "CAT TEST HOME CHARACTERISTIC",
                    Behavior = "CAT TEST HOME BEHAVIOR",
                    IsChipped = true,
                    Status = Animal_Status.Home,
                    StatusDescription = "TEST DESC",
                    UserID = "437cc5d5-2425-435a-8a3f-18c692113636",
                    CityID = 1,
                    CreateDate = new DateTime(2023,05,29),
                    UpdateDate = new DateTime(2023,06,10)
                },
                new Animal
                {
                    Id = 2,
                    Type = Animal_Type.Dog,
                    Breed = "Corgi",
                    Name = "DOGTestLost",
                    Sex = Sex.Female,
                    DateOfBirth = new DateTime(2020,05,05),
                    Characteristic = "DOG TEST LOST CHARASTERISTICS",
                    Behavior = "DOG TEST LOST BEHAVIOR",
                    IsChipped = false,
                    Status = Animal_Status.Lost,
                    StatusDescription = "TEST DESC",
                    UserID = "437cc5d5-2425-435a-8a3f-18c692113636",
                    CityID = 1,
                    CreateDate = new DateTime(2023,07,1),
                    UpdateDate = new DateTime(2023,07,2)
                },
                new Animal
                {
                    Id = 3,
                    Type = Animal_Type.Cat,
                    Breed = "Egyiptomi",
                    Name = "Cat test rescued",
                    Sex = Sex.Female,
                    Characteristic = "CAT TEST RESCUED CHAR",
                    Behavior = "CAT TEST RESCUED BEHV",
                    IsChipped = true,
                    Status = Animal_Status.Rescued,
                    UserID = "25a66a77-6970-4bc5-ba4d-4f834fd1e4ab",
                    CityID = 2,
                    CreateDate = new DateTime(2023,07,10),
                    UpdateDate = new DateTime(2023,07,15)
                }
            };

        public static List<Animal_Image>animalImages = new()
        {
            new Animal_Image
            {
                Id = 1,
                AnimalID = 1,
                Image = new byte[] {},
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                RowVersion = new byte[] {}
            },
            new Animal_Image
            {
                Id = 2,
                AnimalID = 2,
                Image = new byte[] {},
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                RowVersion = new byte[] {}
            },
            new Animal_Image
            {
                Id = 3,
                AnimalID = 2,
                Image = new byte[] {},
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                RowVersion = new byte[] {}
            }
        };
    }
}
