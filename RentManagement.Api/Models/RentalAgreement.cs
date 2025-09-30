using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentManagement.Api.Models
{
    public class RentalAgreement
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RentAmount { get; set; }

        [Required]
        public DateOnly EffectiveDate { get; set; }

        [Required]
        public int ShopId { get; set; }
        public Shop Shop { get; set; } = null!;

        [Required]
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
