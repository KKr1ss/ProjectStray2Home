using Microsoft.AspNetCore.Http;
using ProjectStrayToHomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.User;

namespace ProjectStrayToHomeAPI.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest);
        Task<APIResultDTO> RegisterAsync(RegisterRequestDTO registerRequestDTO, IFormFile? profileImage);
        Task<UserProfileDTO?> GetProfileDetailsAsync(string username);
        Task<UserProfileDTO?> GetAccountDetailsAsync(string email);
    }
}
