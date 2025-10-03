namespace RentManagement.Api.Models
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? Email { get; set; }
        public ICollection<RentalAgreement> RentalAgreements { get; set; } = new List<RentalAgreement>();
    }
}
