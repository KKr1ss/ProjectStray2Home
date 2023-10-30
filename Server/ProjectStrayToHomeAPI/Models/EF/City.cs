using ProjectStrayToHomeAPI.Models.EF.Base;
using System.ComponentModel.DataAnnotations;

namespace ProjectStray2HomeAPI.Models.EF
{
    public class City : IEntity<int>
    {
        /// <summary>
        /// The unique id and primary key for this City
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name of the city
        /// </summary>
        [Required]
        public string Name { get; set; }

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
    }
}