using ProjectStray2HomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.Animals;

namespace ProjectStrayToHomeAPI.Models.DTO.User
{
    public class UserProfileDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? CurrentCity { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public ICollection<AnimalPreviewDTO>? Animals { get; set; } = null!;
        public ICollection<string>? User_Cities { get; set; } = null!;
    }
}
