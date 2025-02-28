using System.ComponentModel.DataAnnotations;

namespace NewZelandAPI.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be min od 3 characters")]
        [MaxLength(3, ErrorMessage = "Code must be max of 3 characters")]

        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
