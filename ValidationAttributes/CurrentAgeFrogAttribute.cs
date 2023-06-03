using FrogExebitionAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.ValidationAttributes
{
    public class CurrentAgeFrogAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Frog f = value as Frog;
            if (f.MaxAge < f.CurrentAge)
            {
                return false;
            }
            return true;
        }
    }
}
