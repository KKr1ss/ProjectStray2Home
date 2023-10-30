using Microsoft.AspNetCore.Mvc;

namespace ProjectStrayToHomeAPI.Services.Interfaces
{
    public interface ISeedService
    {
        Task<JsonResult> ImportDataAsync();
        Task<JsonResult> ImportAdminAsync();
    }
}
