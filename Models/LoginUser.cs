using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.Models
{
    public class LoginUser
    {
        [Required]
        [DefaultValue("Test@mail.com")]
        public string Email { get; set; }
        [DefaultValue("Cool_User_Name")]
        public string UserName { get; set; }
        [Required]
        [DefaultValue("P@ssw0rd")]
        public string Password { get; set; }
    }
}
