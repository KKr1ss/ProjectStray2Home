using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectStrayToHomeAPI.Models.DTO.User
{
    public class ApplicationUserImageDTO
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }

        public byte[]? RowVersion { get; set; }
    }
}
