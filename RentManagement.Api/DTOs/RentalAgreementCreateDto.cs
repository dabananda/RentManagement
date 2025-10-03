using System.ComponentModel.DataAnnotations;

namespace RentManagement.Api.DTOs
{
    public class RentalAgreementCreateDto
    {
        [Required]
        public decimal RentAmount { get; set; }
        public decimal SecurityFee { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        [Required]
        public int ShopId { get; set; }

        [Required]
        public int TenantId { get; set; }
    }
}
