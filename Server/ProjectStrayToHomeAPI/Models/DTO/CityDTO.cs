using System.ComponentModel.DataAnnotations;

namespace ProjectStray2HomeAPI.Models.DTO
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public byte[]? RowVersion { get; set; }
    }
}