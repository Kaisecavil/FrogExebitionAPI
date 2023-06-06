using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FrogExebitionAPI.Dto
{
    public class ExebitionDto
    {
        public string Name { get; set; }

        public DateTime? Date { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }
    }
}
