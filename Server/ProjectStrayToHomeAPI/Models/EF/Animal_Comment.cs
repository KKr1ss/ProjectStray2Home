using Microsoft.EntityFrameworkCore;
using ProjectStrayToHomeAPI.Models.EF.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectStray2HomeAPI.Models.EF
{
    public class Animal_Comment : IEntity<int>
    {
        /// <summary>
        /// The unique id and primary key for this City
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Comment 
        /// </summary>
        [Required]
        public string Comment { get; set; }

        /// <summary>
        /// Animal ID (foreign key)
        /// </summary>
        [ForeignKey(nameof(Animal))]
        [Required]
        public int AnimalID { get; set; }

        /// <summary>
        /// User ID (foreign key)
        /// </summary>
        [ForeignKey(nameof(User))]
        public string? UserID { get; set; }

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
        /// The comment related to this animal.
        /// </summary>
        [JsonIgnore]
        public Animal? Animal { get; set; }

        /// <summary>
        /// The user related to this comment.
        /// </summary>
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
    }
}
