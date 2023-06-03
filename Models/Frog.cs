using FrogExebitionAPI.AppConstants;
using FrogExebitionAPI.Models.Base;
using FrogExebitionAPI.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.Models
{
    [CurrentAgeFrog(ErrorMessage ="Current age can't be greater than max age")]
    public class Frog : BaseModel
    {
        [Required]
        [MinLength(3)]
        public string Genus { get; set; }
        [Required]
        [MinLength(3)]
        public string Species { get; set; }
        [Required]
        [MinLength(3)]
        public string Color { get; set; }
        [Required]
        [MinLength(3)]
        public string Habitat { get; set; }
        [Required]
        public bool Poisonous { get; set; }
        [Required]
        [ValidStrings(new string[] { "Male", "Female", "Hermaphrodite"})]
        public string Sex { get; set; }
        [Required]
        public bool HouseKeepable { get; set; }
        [Required]
        [Range(1, 100)]
        public float Size { get; set; }
        [Required]
        [Range(1, 1000)]
        public float Weight { get; set; }
        [Required]
        [Range(1, Constants.MaxAge)]
        public int CurrentAge { get; set; }
        [Required]
        public int MaxAge { get; set; }
        public string Diet { get; set; }
        public string Features { get; set; }
        public string Photo { get; set; }

        public List<Exebition> Exebitions { get; } = new();
        public List<FrogOnExebition> FrogsOnExebitions { get; } = new();

    }
}
