using System.ComponentModel.DataAnnotations.Schema;

namespace RentManagement.Api.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string ShopNumber { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AreaSqFt { get; set; }
        public string Floor { get; set; } = string.Empty;
        public bool IsOccupied { get; set; }
        public IEnumerable<RentalAgreement> RentalAgreements { get; set; } = null!;
        public string CreatedByUserId { get; set; } = null!;
    }
}
