using FrogExebitionAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.ValidationAttributes
{
    public class CurrentAgeFrogAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            Frog f = value as Frog;
            try
            {
                if (f.MaxAge < f.CurrentAge)
                {
                    return false;
                }
            }
            catch(NullReferenceException ex) 
            {
                return false; // можно ли так или нужно выкидывать наверх? 
            }
            return true;
        }
    }
}
