using FrogExebitionAPI.AppConstants;
using FrogExebitionAPI.Models.Base;
using FrogExebitionAPI.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.Models
{
    [CurrentAgeFrog(ErrorMessage ="Current age can't be greater than max age")]
    public class Frog : BaseModel
    {
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
        [ValidStrings(new string[] { "Male", "Female", "Hermaphrodite"}, ErrorMessage = "Valid options: Male, Female, Hermaphrodite")]
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

        [Required]
        [MinLength(3)]
        [DefaultValue("Bugs, insects")]
        public string Diet { get; set; }

        [Required]
        [MinLength(3)]
        [DefaultValue("The are so Cool loking!")]
        public string Features { get; set; }
        public string Photo { get; set; }

        public List<Exebition> Exebitions { get; } = new();
        public List<FrogOnExebition> FrogsOnExebitions { get; } = new();
        public List<FrogPhoto> FrogPhotos { get; } = new();

    }
}
