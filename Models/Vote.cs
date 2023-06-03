using FrogExebitionAPI.Models.Base;

namespace FrogExebitionAPI.Models
{
    public class Vote : BaseModel 
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid FrogOnExebitionId { get; set; }
        public FrogOnExebition FrogOnExebition { get; set; } = null!;
    }
}
