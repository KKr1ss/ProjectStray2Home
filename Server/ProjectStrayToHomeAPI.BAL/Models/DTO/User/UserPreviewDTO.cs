namespace ProjectStrayToHomeAPI.Models.DTO.User
{
    public class UserPreviewDTO
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CurrentCity { get; set; }
    }
}
