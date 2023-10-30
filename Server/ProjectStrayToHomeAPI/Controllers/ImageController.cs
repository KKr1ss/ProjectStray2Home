using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Helpers;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStrayToHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly ILogger<ImageController> _logger;

        public ImageController(IImageService imageService, ILogger<ImageController> logger)
        {
            _imageService = imageService;
            _logger = logger;
        }

        [Route("[action]/{username}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserImage(string username)
        {
            try
            {
                var result = await _imageService.GetUserImageAsync(username);
                return result;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation("File not found: " + ex.Message);
                return NotFound("File not found");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [Route("[action]/{animalId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnimalImage(int animalId, int? imageId)
        {
            try
            {
                var result = await _imageService.GetAnimalImageAsync(animalId, imageId);
                return result;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogInformation("File not found: " + ex.Message);
                return NotFound("File not found");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Error: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}