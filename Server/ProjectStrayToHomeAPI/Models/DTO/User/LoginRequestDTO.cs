using System.ComponentModel.DataAnnotations;

namespace ProjectStrayToHomeAPI.Models.DTO.User
{
    public class LoginRequestDTO
    {
        [Required(ErrorMessage = "UserName is required.")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = null!;
    }
}
