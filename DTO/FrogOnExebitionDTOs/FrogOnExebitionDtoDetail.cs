using System.ComponentModel.DataAnnotations;

namespace FrogExebitionAPI.DTO.FrogOnExebitionDTOs
{
    public class FrogOnExebitionDtoDetail
    {
        public Guid Id { get; set; }
        [Required]
        public Guid ExebitionId { get; set; }
        [Required]
        public Guid FrogId { get; set; }
    }
}
