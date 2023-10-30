using Microsoft.AspNetCore.Mvc;
using ProjectStrayToHomeAPI.Services.Interfaces;

namespace ProjectStrayToHomeAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class SeedController : ControllerBase
    {
        private readonly ISeedService _seedService;
        private readonly ILogger<SeedController> _logger;

        public SeedController(ISeedService seedService, ILogger<SeedController> logger)
        {
            _seedService = seedService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> ImportData()
        {
            try
            {
                var result = await _seedService.ImportDataAsync();

                _logger.LogInformation("Data has been imported to database!");
                return result;
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation("Seed Error: " + ex.Message);
                return new JsonResult("Seed Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Seed Error: API Error: " + ex.Message);
                return new JsonResult("Seed Error: API Error: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ImportAdmin()
        {
            try
            {
                var result = await _seedService.ImportAdminAsync();

                _logger.LogInformation("Admin been seeded to database!");
                return result;
            }
            catch (ArgumentException ex)
            {
                _logger.LogInformation("Seed Error: " + ex.Message);
                return new JsonResult("Seed Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Seed Error: API Error" + ex.Message);
                return new JsonResult("Seed Error: API Error" + ex.Message);
            }

        }
    }
}
