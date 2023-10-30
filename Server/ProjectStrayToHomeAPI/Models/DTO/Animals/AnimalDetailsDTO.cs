using ProjectStray2HomeAPI.Models.DTO;
using ProjectStrayToHomeAPI.Models.DTO.User;

namespace ProjectStrayToHomeAPI.Models.DTO.Animals
{
    public class AnimalDetailsDTO
    {
        public int Id { get; set; }
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
        public string City { get; set; }
        public int[] AnimalImagesId { get; set; }

        public UserPreviewDTO UserPreview { get; set; }
        public List<AnimalCommentDTO> Comments { get; set; }
    }
}
