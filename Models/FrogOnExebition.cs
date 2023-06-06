using FrogExebitionAPI.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.Models
{
    public class FrogOnExebition : BaseModel
    {
        [Required]
        public Guid ExebitionId { get; set; }
        [Required]
        public Guid FrogId { get; set; }
        public Exebition Exebition { get; set; } = null!;
        public Frog Frog { get; set; } = null!;
    }
}
