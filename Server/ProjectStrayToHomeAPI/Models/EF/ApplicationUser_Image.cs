using ProjectStrayToHomeAPI.Models.EF.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectStray2HomeAPI.Models.EF
{
    public class ApplicationUser_Image : IImageEntity<int>
    {
        /// <summary>
        /// The unique id and primary key for the user
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The id of the user
        /// </summary>
        [ForeignKey(nameof(User))]
        [Required]
        public string ApplicationUserID { get; set; }

        /// <summary>
        /// Image of the user
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
        /// The user whom the picture belongs to
        /// </summary>
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
    }
}
