using Microsoft.AspNetCore.Identity;
using ProjectStray2HomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.Animals;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectStrayToHomeAPI.Models.DTO.User
{
    public class ApplicationUserDTO
    {
        public string? Id { get; set; }
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

        public ICollection<AnimalRequestDTO>? Animals { get; set; } = null!;
        public ICollection<CityDTO>? User_Cities { get; set; } = null!;
    }
}
