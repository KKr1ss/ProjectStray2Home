using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProjectStrayToHomeAPI.Models.DTO.User
{
    public class RegisterRequestDTO
    {
        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Sex { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; } = null!;
        
        public string CurrentCityID { get; set; }

        public string[]? ObservedCityIDs { get; set; } = null!;

        public string? Description { get; set; } = null!;

    }
}
