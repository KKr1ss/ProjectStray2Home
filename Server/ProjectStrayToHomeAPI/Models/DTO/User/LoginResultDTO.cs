namespace ProjectStrayToHomeAPI.Models.DTO.User
{
    public class LoginResultDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; } = null!;
        public string? Token { get; set; }
        public string? UserName { get; set; }
    }
}
