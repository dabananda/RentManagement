using System.ComponentModel.DataAnnotations;

namespace RentManagement.Api.DTOs
{
    public class TenantCreateDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Length can't be more than 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }
    }
}
