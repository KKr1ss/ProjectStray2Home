using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStray2HomeAPI.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Text;
namespace ProjectStrayToHomeAPI.Tests.UnitTestHelpers
{
    public static class ArrangeHelper
    {
        public static MapperConfiguration MapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfiles(new List<Profile>() { new CityProfile(), new AnimalCommentProfile(), new AnimalProfile(), new ApplicationUserProfile() });
        });

        public static RoleManager<TIdentityRole>
         GetRoleManager<TIdentityRole>(
            IRoleStore<TIdentityRole> roleStore) where TIdentityRole :
             IdentityRole
        {
            return new RoleManager<TIdentityRole>(
                    roleStore,
                    new IRoleValidator<TIdentityRole>[0],
                    new UpperInvariantLookupNormalizer(),
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<ILogger<RoleManager<TIdentityRole>>>(
                    ).Object);
        }
        public static UserManager<TIDentityUser> GetUserManager<TIDentityUser>(
            IUserStore<TIDentityUser> userStore) where TIDentityUser :
             IdentityUser
        {
            return new UserManager<TIDentityUser>(
                    userStore,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<TIDentityUser>>().Object,
                    new IUserValidator<TIDentityUser>[0],
                    new IPasswordValidator<TIDentityUser>[0],
                    new UpperInvariantLookupNormalizer(),
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<TIDentityUser>>>(
                    ).Object);

            //var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
            //var mockUserRoleStore = mockUserStore.As<IUserRoleStore<ApplicationUser>>();
            //var mockedUserManager = new Mock<UserManager<ApplicationUser>>(mockUserRoleStore.Object,
            //    new Mock<IOptions<IdentityOptions>>().Object,
            //    new Mock<IPasswordHasher<ApplicationUser>>().Object,
            //    new IUserValidator<ApplicationUser>[0],
            //    new IPasswordValidator<ApplicationUser>[0],
            //    new Mock<ILookupNormalizer>().Object,
            //    new Mock<IdentityErrorDescriber>().Object,
            //    new Mock<IServiceProvider>().Object,
            //    new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
        }
    }
}