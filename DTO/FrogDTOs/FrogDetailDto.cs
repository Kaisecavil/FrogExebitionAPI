using FrogExebitionAPI.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FrogExebitionAPI.Models.Base;
using FrogExebitionAPI.AppConstants;

namespace FrogExebitionAPI.DTO.FrogDTOs
{
    public class FrogDetailDto
    {
        public string Genus { get; set; }
        public string Species { get; set; }
        public string Color { get; set; }
        public string Habitat { get; set; }
        public bool Poisonous { get; set; }
        public string Sex { get; set; }
        public bool HouseKeepable { get; set; }
        public float Size { get; set; }
        public float Weight { get; set; }
        public int CurrentAge { get; set; }
        public int MaxAge { get; set; }
        public string Diet { get; set; }
        public string Features { get; set; }
        public string Photo { get; set; }
    }
}
