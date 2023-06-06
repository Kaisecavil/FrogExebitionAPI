using FrogExebitionAPI.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FrogExebitionAPI.Models
{
    public class Exebition : BaseModel
    {
        [Required]
        [MinLength(3)]
        [DefaultValue("Exebition name")]
        public string Name { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        [MinLength(3)]
        [DefaultValue("Country")]
        public string Country { get; set; }
        [Required]
        [MinLength(3)]
        [DefaultValue("City")]
        public string City { get; set; }
        [Required]
        [MinLength(3)]
        [DefaultValue("Street")]
        public string Street { get; set; }
        [Required]
        [MinLength(3)]
        [DefaultValue("House")]
        public string House { get; set; }

        public List<Frog> Frogs { get; } = new();
        public List<FrogOnExebition> FrogsOnExebitions { get; } = new();
    }
}
