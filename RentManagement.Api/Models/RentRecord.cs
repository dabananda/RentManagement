using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentManagement.Api.Models
{
    public class RentRecord
    {
        public int Id { get; set; }
        [Range(2000, 3000)]
        public int Year { get; set; }
        [Range(1, 12)]
        public int Month { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public DateOnly PaidOn { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow.Date);
        public string? Notes { get; set; }
        public int AgreementId { get; set; }
        public RentalAgreement Agreement { get; set; } = null!;
        public int ShopId { get; set; }
        public int TenantId { get; set; }
        public string CreatedByUserId { get; set; } = null!;
    }
}
