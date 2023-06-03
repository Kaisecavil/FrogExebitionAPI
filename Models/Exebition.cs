using FrogExebitionAPI.Models.Base;

namespace FrogExebitionAPI.Models
{
    public class Exebition : BaseModel
    {
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }

        public List<Frog> Frogs { get; } = new();
        public List<FrogOnExebition> FrogsOnExebitions { get; } = new();
    }
}
