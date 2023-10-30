using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ProjectStrayToHomeAPI.Models.DTO.User;

namespace ProjectStrayToHomeAPI.Models.DTO.Animals
{
    public class AnimalCommentDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public UserPreviewDTO User { get; set; }
    }
}
