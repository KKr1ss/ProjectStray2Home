using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectStray2HomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Repositories.Interfaces;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStrayToHomeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;

        public CityController(ICityService cityService,
            ILogger<CityController> logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        // GET: api/Cities
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            var citiesDTO = await _cityService.GetCitiesAsync();
            _logger.LogInformation("Cities has been sent to client");
            return Ok(citiesDTO);
        }
    }
}
