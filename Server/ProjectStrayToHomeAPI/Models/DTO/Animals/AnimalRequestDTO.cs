using ProjectStray2HomeAPI.Models.EF;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProjectStray2HomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.User;

namespace ProjectStrayToHomeAPI.Models.DTO.Animals
{
    public class AnimalRequestDTO
    {
        public string? Id { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Characteristic { get; set; }
        public string Behavior { get; set; }
        public bool IsChipped { get; set; }
        public string Status { get; set; }
        public string? StatusDescription { get; set; }
        public string? UserID { get; set; }
        public string CityID { get; set; }
        public string UserName { get; set; }
    }
}
