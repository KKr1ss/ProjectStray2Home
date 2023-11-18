using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjectStray2HomeAPI.Models;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Handlers.Interfaces;
using ProjectStrayToHomeAPI.Helpers;
using ProjectStrayToHomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.User;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStrayToHomeAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepositoryManager _repositoryManager;
        private readonly IJwtHandler _jwtHandler;
        private readonly IMapper _mapper;

        public AccountService(UserManager<ApplicationUser> userManager, IRepositoryManager repositoryManager, IJwtHandler jwtHandler, IMapper mapper)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
            _mapper = mapper;
            _repositoryManager = repositoryManager;
        }

        public async Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                return new LoginResultDTO()
                {
                    Success = false,
                    Message = "Hibás felhasználónév vagy jelszó."
                };
            }

            var secToken = await _jwtHandler.GetTokenAsync(user);
            var jwt = _jwtHandler.WriteToken(secToken);
            return new LoginResultDTO()
            {
                Success = true,
                Message = "Sikeres bejelentkezés",
                Token = jwt,
                UserName = user.UserName
            };
        }

        public async Task<APIResultDTO> RegisterAsync(RegisterRequestDTO registerRequestDTO, IFormFile? profileImage)
        {
            try
            {
                var userToAdd = _mapper.Map<ApplicationUser>(registerRequestDTO);
                ApplicationUserHelper.getUserReadyForRegistration(userToAdd);

                if (await _userManager.FindByNameAsync(userToAdd.UserName) != null)
                    throw new ArgumentException("Felhasználónév foglalt");
                if (await _userManager.FindByEmailAsync(userToAdd.Email) != null)
                    throw new ArgumentException("Email foglalt");

                if (await _userManager.CreateAsync(userToAdd, registerRequestDTO.Password) != IdentityResult.Success)
                {
                    throw new ArgumentException("Hiba a regisztráció során");
                };

                await _userManager.AddToRoleAsync(userToAdd, Roles.User.ToString());

                if (!registerRequestDTO.ObservedCityIDs.IsNullOrEmpty())
                {
                    int[] ids = registerRequestDTO.ObservedCityIDs!.Select(x => int.Parse(x)).ToArray();
                    await _repositoryManager.Cities.SetObservedCitiesForUserAsync(userToAdd.Id, ids);
                    await _repositoryManager.SaveAsync();
                }

                if (profileImage != null)
                {
                    await _repositoryManager.UserImages.SetUserImageAsync(profileImage, userToAdd.Id);
                    await _repositoryManager.SaveAsync();
                }

                return new APIResultDTO()
                {
                    Success = true,
                    Message = "Sikeres regisztráció."
                };
            }
            catch (ArgumentException ex)
            {
                return new APIResultDTO()
                {
                    Success = false,
                    Message = "Hiba: " + ex.Message
                };
            }
        }

        public async Task<UserProfileDTO?> GetProfileDetailsAsync(string username)
        {
            var userProfile = await _userManager.FindByNameAsync(username);
            if (userProfile == null)
            {
                return null;
            }

            userProfile = await _repositoryManager.Users.GetUserConnectedAsync(userProfile, false);
            if (userProfile.Animals != null)
            {
                userProfile.Animals = await _repositoryManager.Animals.GetAnimalsConnectedAsync(userProfile.Animals.ToList(), false);
            }

            //remove unwanted mappings
            if (userProfile.Animals != null)
            {
                foreach (Animal animal in userProfile.Animals)
                {
                    animal.User = null;
                }
            }

            var userProfileDTO = _mapper.Map<UserProfileDTO>(userProfile);

            return userProfileDTO;
        }

        public async Task<UserProfileDTO?> GetAccountDetailsAsync(string email)
        {
            var userProfile = await _userManager.FindByEmailAsync(email);
            if (userProfile == null)
            {
                return null;
            }
            userProfile = await _repositoryManager.Users.GetUserConnectedAsync(userProfile, false);

            var userProfileDTO = _mapper.Map<UserProfileDTO>(userProfile);

            return userProfileDTO;
        }
    }
}
