using Microsoft.AspNetCore.Identity;

namespace FrogExebitionAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Photo { get; set; }
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
