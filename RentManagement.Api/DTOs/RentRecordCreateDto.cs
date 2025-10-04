using System.ComponentModel.DataAnnotations;

namespace RentManagement.Api.DTOs
{
    public class RentRecordCreateDto
    {
        [Required]
        public int ShopId { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public decimal? Amount { get; set; }
        public string? Notes { get; set; }
    }
}
