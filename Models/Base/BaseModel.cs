using Microsoft.EntityFrameworkCore;

namespace FrogExebitionAPI.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
    }
}
