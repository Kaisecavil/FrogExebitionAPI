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
        public virtual Exebition Exebition { get; set; } = null!;
        public virtual Frog Frog { get; set; } = null!;
    }
}
