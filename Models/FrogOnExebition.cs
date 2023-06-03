using FrogExebitionAPI.Models.Base;

namespace FrogExebitionAPI.Models
{
    public class FrogOnExebition : BaseModel
    {
        public Guid ExebitionId { get; set; }
        public Guid FrogId { get; set; }
        public Exebition Exebition { get; set; } = null!;
        public Frog Frog { get; set; } = null!;
    }
}
