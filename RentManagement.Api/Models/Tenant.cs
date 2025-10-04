namespace RentManagement.Api.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<RentalAgreement> RentalAgreements { get; set; } = null!;
        public string CreatedByUserId { get; set; } = null!;
    }
}
