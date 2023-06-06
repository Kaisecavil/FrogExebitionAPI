using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.DTO.FrogOnExebitionDTOs
{
    public class FrogOnExebitionDtoForCreate
    {
        [Required]
        public Guid ExebitionId { get; set; }
        [Required]
        public Guid FrogId { get; set; }
    }
}
