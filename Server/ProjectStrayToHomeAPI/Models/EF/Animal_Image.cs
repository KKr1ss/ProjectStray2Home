using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProjectStrayToHomeAPI.Models.EF.Base;

namespace ProjectStray2HomeAPI.Models.EF
{
    public class Animal_Image : IImageEntity<int>
    {
        /// <summary>
        /// The unique id and primary key for this Animal's image
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The id of the animal
        /// </summary>
        [ForeignKey(nameof(Animal))]
        [Required]
        public int AnimalID { get; set; }

        /// <summary>
        /// Image of the animal
        /// </summary>
        [Required]
        public byte[] Image { get; set; }

        /// <summary>
        /// Creation date.
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Update date.
        /// </summary>
        [Required]
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Timestamp for the last modification
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();


        /// <summary>
        /// The animal whom the picture belongs to
        /// </summary>
        [JsonIgnore]
        public Animal? Animal { get; set; }
    }
}
