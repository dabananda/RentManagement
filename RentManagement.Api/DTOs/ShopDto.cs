using System.ComponentModel.DataAnnotations.Schema;

namespace RentManagement.Api.DTOs
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string ShopNumber { get; set; } = string.Empty;
        public string Floor { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AreaSqFt { get; set; }
        public bool IsOccupied { get; set; }
        public ICollection<RentalAgreementDto>? RentalAgreements { get; set; }
    }
}
