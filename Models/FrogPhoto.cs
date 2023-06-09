using FrogExebitionAPI.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.Models
{
    public class FrogPhoto : BaseModel
    {
        [Required]
        public Guid FrogId { get; set; }
        public Frog Frog { get; set; } = null!;
        [Required]
        public string Photo { get; set; }
    }
}
