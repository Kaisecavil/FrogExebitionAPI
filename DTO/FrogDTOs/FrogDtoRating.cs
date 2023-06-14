using FrogExebitionAPI.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FrogExebitionAPI.Models.Base;
using FrogExebitionAPI.AppConstants;

namespace FrogExebitionAPI.DTO.FrogDTOs
{
    public class FrogDtoRating
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3)]
        [DefaultValue("Lithobates")]
        public string Genus { get; set; }

        [Required]
        [MinLength(3)]
        [DefaultValue("L. catesbeianus")]
        public string Species { get; set; }

        [Required]
        [MinLength(3)]
        [DefaultValue("Green-brown")]
        public string Color { get; set; }

        [Required]
        [MinLength(3)]
        [DefaultValue("North America")]
        public string Habitat { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool Poisonous { get; set; }

        [Required]
        [ValidStrings(new string[] { "Male", "Female", "Hermaphrodite" }, ErrorMessage = "Valid options: Male, Female, Hermaphrodite")]
        [DefaultValue("Male")]
        public string Sex { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool HouseKeepable { get; set; }

        [Required]
        [Range(1, 100)]
        [DefaultValue(15.5f)]
        public float Size { get; set; }

        [Required]
        [Range(1, 1000)]
        [DefaultValue(350)]
        public float Weight { get; set; }

        [Required]
        [Range(1, Constants.MaxAge)]
        [DefaultValue(3)]
        public int CurrentAge { get; set; }
        [Required]
        [Range(1, Constants.MaxAge)]
        [DefaultValue(10)]
        public int MaxAge { get; set; }
        public List<string> PhotoPaths { get; set; }
        [Required]
        public int VotesCount { get; set; }
    }
}
