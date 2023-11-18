using ProjectStrayToHomeAPI.Models.EF.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectStray2HomeAPI.Models.EF
{
    public class ApplicationUser_City : IEntity<int>
    {
        /// <summary>
        /// The unique id and primary key for this City
        /// </summary>
        [Key]
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The user related to the city.
        /// </summary>
        [ForeignKey(nameof(User))]
        [Required]
        public string UserID { get; set; }

        /// <summary>
        /// The city related to user.
        /// </summary>
        [ForeignKey(nameof(City))]
        public int? CityID { get; set; }


        /// <summary>
        /// The user related to the city.
        /// </summary>
        [JsonIgnore]
        public ApplicationUser? User { get; set; }

        /// <summary>
        /// The city related to user.
        /// </summary>
        [JsonIgnore]
        public City? City { get; set; }
    }
}
