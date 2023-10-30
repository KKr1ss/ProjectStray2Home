using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectStray2HomeAPI.Models.EF;
using ProjectStrayToHomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.Animals;
using ProjectStrayToHomeAPI.Models.DTO.User;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using ProjectStrayToHomeAPI.Services;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStrayToHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly ILogger<AnimalController> _logger;
        private readonly IAnimalService _animalService;


        public AnimalController(IAnimalService animalService, ILogger<AnimalController> logger)
        {
            _animalService = animalService;
            _logger = logger;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] AnimalRequestDTO animalRequest, List<IFormFile> animalImages)
        {
            try
            {
                APIResultDTO result = await _animalService.UploadAsync(animalRequest, animalImages);

                if (result.Success)
                {
                    _logger.LogInformation("Anima upload: Animal upload success");
                    return Ok(result);
                }
                _logger.LogInformation("Animal upload object error");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Animal upload API error: " + ex.Message);
                return BadRequest(new APIResultDTO()
                {
                    Success = false,
                    Message = "Szerver hiba."
                });
            }
        }

        [HttpPost("UploadComment")]
        public async Task<IActionResult> UploadComment(AnimalCommentRequestDTO commentRequest)
        {
            try
            {
                var result = await _animalService.UploadCommentAsync(commentRequest);
                if (result.Success)
                {
                    _logger.LogInformation("Animacomment upload: Animal upload success");
                    return Ok(result);
                }
                _logger.LogError("Animalcomment upload error: object error");
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("AnimalComment upload API error: " + ex.Message);
                return BadRequest(new APIResultDTO()
                {
                    Success = false,
                    Message = "Szerver hiba."
                });
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnimalDetails(int id)
        {
            try
            {
                var animalDTO = await _animalService.GetAnimalDetails(id);
                if (animalDTO != null)
                {
                    _logger.LogInformation($"animal with {id} id has been requested succesfully");
                    return Ok(animalDTO);
                }

                _logger.LogError($"Error: animal with {id} id not found");
                return StatusCode(404, "Hiba: állat nem található");
            }
            catch (Exception ex)
            {
                _logger.LogError("API Error: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAnimals")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAnimals(int pageIndex = 0, int pageSize = 20)
        {
            try
            {
                var result = await _animalService.GetAnimals(pageIndex, pageSize);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("API Error: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
