using FrogExebitionAPI.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.Models
{
    public class Vote : BaseModel 
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid FrogOnExebitionId { get; set; }
        public User User { get; set; } = null!;
        public FrogOnExebition FrogOnExebition { get; set; } = null!;
    }
}
