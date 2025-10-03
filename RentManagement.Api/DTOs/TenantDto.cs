using RentManagement.Api.Models;

namespace RentManagement.Api.DTOs
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<RentalAgreement> RentalAgreements { get; set; } = null!;
    }
}
