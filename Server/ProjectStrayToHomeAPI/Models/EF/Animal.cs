using ProjectStrayToHomeAPI.Models.EF.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectStray2HomeAPI.Models.EF
{
    public class Animal: IEntity<int>
    {
        /// <summary>
        /// The unique id and primary key for this City
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Type of the pet
        /// </summary>
        [Required]
        public Animal_Type Type { get; set; }

        /// <summary>
        /// Breed
        /// </summary>
        [Required]
        public string Breed { get; set; }

        /// <summary>
        /// Name of the animal.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gender of the animal.
        /// </summary>
        [Required]
        public Sex Sex { get; set; }

        /// <summary>
        /// Age
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Charasteristic
        /// </summary>
        [Required]
        public string Characteristic { get; set; }

        /// <summary>
        /// Behavior
        /// </summary>
        [Required]
        public string Behavior { get; set; }

        /// <summary>
        /// Does the animal have chip
        /// </summary>
        public bool? IsChipped { get; set; }

        /// <summary>
        /// The current status of the animal
        /// </summary>
        [Required]
        public Animal_Status Status { get; set; }

        /// <summary>
        /// Destrictiopn regarding the status
        /// </summary>
        public string? StatusDescription { get; set; }

        /// <summary>
        /// The date of the status change
        /// </summary>
        public DateTime StatusDate { get; set; }

        /// <summary>
        /// The user(owner) related to this animal.
        /// </summary>
        [ForeignKey(nameof(User))]
        [Required]
        public string UserID { get; set; }

        /// <summary>
        /// The city related to this animal.
        /// </summary>
        [ForeignKey(nameof(City))]
        public int? CityID { get; set; }

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
        /// The user (owner) related to this animal.
        /// </summary>
        [JsonIgnore]
        public ApplicationUser? User { get; set; }

        /// <summary>
        /// The city related to this animal.
        /// </summary>
        [JsonIgnore]
        public City? City { get; set; }

        /// <summary>
        /// A collection of all the images related to this animal.
        /// </summary>
        [JsonIgnore]
        public ICollection<Animal_Image>? Images { get; set; } = null!;

        /// <summary>
        /// A collection of all the comments related to this animal.
        /// </summary>
        [JsonIgnore]
        public ICollection<Animal_Comment>? Comments { get; set; } = null!;
    }
}
