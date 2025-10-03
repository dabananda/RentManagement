using System.ComponentModel.DataAnnotations.Schema;

namespace RentManagement.Api.Models
{
    public class RentalAgreement
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RentAmount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SecurityFee { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; } = null!;
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; } = null!;
    }
}
