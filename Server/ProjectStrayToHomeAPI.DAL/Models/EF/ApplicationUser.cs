using Microsoft.AspNetCore.Identity;
using ProjectStrayToHomeAPI.Models.EF.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectStray2HomeAPI.Models.EF
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        /// <summary>
        /// First name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// The gender of the user
        /// </summary>
        [Required]
        public Sex Sex { get; set; }

        /// <summary>
        /// Date of birth
        /// </summary>
        [Required]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// The home city's ID of the user
        /// </summary>
        [ForeignKey(nameof(City))]
        [Required]
        public int CurrentCityID { get; set; }

        /// <summary>
        /// Self description
        /// </summary>
        public string? Description { get; set; }

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
        /// The city related to this animal.
        /// </summary>
        [JsonIgnore]
        public City? City { get; set; }

        /// <summary>
        /// Picture of the user
        /// </summary>
        [JsonIgnore]
        public ApplicationUser_Image? User_Image { get; set; } = null!;

        /// <summary>
        /// A collection of all the animals related to this user.
        /// </summary>
        [JsonIgnore]
        public ICollection<Animal>? Animals { get; set; } = null!;

        /// <summary>
        /// A collection of all the cities the user watches
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<ApplicationUser_City>? User_Cities { get; set; } = null!;
    }
}
