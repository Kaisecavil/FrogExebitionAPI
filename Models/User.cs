using FrogExebitionAPI.Models.Base;

namespace FrogExebitionAPI.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Vote> Vote { get; set; } = new List<Vote>();
    }
}
