using System.ComponentModel.DataAnnotations;

namespace RentManagement.Api.DTOs
{
    public class ShopCreateDto
    {
        [Required]
        [StringLength(10, ErrorMessage = "Shop number cannot be longer than 10 characters.")]
        public string ShopNumber { get; set; } = string.Empty;

        [Required]
        public string Floor { get; set; } = string.Empty;

        [Range(10, 10000, ErrorMessage = "Area must be between 10 and 10000 sq ft.")]
        public decimal AreaSqFt { get; set; }
    }
}
