using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectStrayToHomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.User;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStray2HomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(
            IAccountService accountService,
            ILogger<AccountController> logger)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                LoginResultDTO loginResult = await _accountService.LoginAsync(loginRequest);
                if (loginResult.Success)
                {
                    _logger.LogInformation("Login: successfull login");
                    return Ok(loginResult);
                }
                _logger.LogInformation("Login: failed password or username");
                return Unauthorized(loginResult);

            }
            catch (Exception ex)
            {
                _logger.LogInformation("API ERROR: Login error: " + ex.Message);
                return Unauthorized(new LoginResultDTO()
                {
                    Success = false,
                    Message = "API hiba."
                });
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDTO registerRequestDTO, IFormFile? profileImage)
        {
            try
            {
                APIResultDTO apiResult = await _accountService.RegisterAsync(registerRequestDTO, profileImage);

                if (apiResult.Success)
                {
                    _logger.LogInformation("Register: successfull registration");
                    return Ok(apiResult);
                }
                _logger.LogInformation("Register: failed attempt");
                return BadRequest(apiResult);
            }
            catch (Exception ex)
            {
                _logger.LogError("API Error: " + ex.Message);
                return BadRequest(new APIResultDTO()
                {
                    Success = false,
                    Message = "API Hiba"
                });
            }
        }

        // GET: api/Account
        [Route("[action]/{username}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProfileDetails(string username)
        {
            try
            {
                UserProfileDTO? userProfileDTO = await _accountService.GetProfileDetailsAsync(username);

                if (userProfileDTO != null)
                {
                    _logger.LogInformation($"ProfileDetails: {username} has been requested");
                    return Ok(userProfileDTO);
                }
                _logger.LogError($"ProfileDetails error: {username} not found");
                return NotFound("Hiba: felhasználó nem találtható");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ProfileDetails API error:{username} API error" + ex.Message);
                return BadRequest("Szerver hiba");
            }
        }

        // GET: api/Account
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> GetAccountDetails()
        {
            try
            {
                string? email = User.Identity?.Name;
                UserProfileDTO? userProfileDTO = await _accountService.GetAccountDetailsAsync(email!);
                if (userProfileDTO == null)
                {
                    _logger.LogError($"AccountDetails error: user not found");
                    return NotFound("Hiba: felhasználó nem található");
                }
                _logger.LogInformation($"AccountDetails: logged in account has been requested");
                return Ok(userProfileDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"AccountDetails API error: user API error: " + ex.Message);
                return BadRequest("Szerver hiba");
            }
        }
    }
}
