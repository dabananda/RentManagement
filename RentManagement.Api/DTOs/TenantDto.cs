using RentManagement.Api.Models;

namespace RentManagement.Api.DTOs
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int? CurrentAgreementId { get; set; }
        public string? CurrentShopNumber { get; set; }
    }
}
