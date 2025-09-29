using System.ComponentModel.DataAnnotations;

namespace RentManagement.Api.DTOs
{
    public class TenantCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string ContactNumber { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public DateOnly LeaseStartDate { get; set; }
        public DateOnly? LeaseEndDate { get; set; }
    }
}
