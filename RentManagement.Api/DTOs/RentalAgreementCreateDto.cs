using System.ComponentModel.DataAnnotations;

namespace RentManagement.Api.DTOs
{
    public class RentalAgreementCreateDto
    {
        [Required]
        public decimal RentAmount { get; set; }

        [Required]
        public DateOnly EffectiveDate { get; set; }

        [Required]
        public int ShopId { get; set; }

        [Required]
        public int TenantId { get; set; }
    }
}
