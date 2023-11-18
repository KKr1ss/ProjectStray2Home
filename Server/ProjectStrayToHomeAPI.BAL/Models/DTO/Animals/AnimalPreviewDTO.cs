using ProjectStray2HomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.User;
using System.ComponentModel.DataAnnotations;

namespace ProjectStrayToHomeAPI.Models.DTO.Animals
{
    public class AnimalPreviewDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public string Status { get; set; }
        public DateTime StatusDate { get; set; }
        public string City { get; set; }
        public UserPreviewDTO? UserPreview { get; set; }
    }
}
