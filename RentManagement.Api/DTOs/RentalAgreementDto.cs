namespace RentManagement.Api.DTOs
{
    public class RentalAgreementDto
    {
        public int Id { get; set; }
        public decimal RentAmount { get; set; }
        public DateOnly EffectiveDate { get; set; }

        public int ShopId { get; set; }
        public string ShopNumber { get; set; } = string.Empty;

        public int TenantId { get; set; }
        public string TenantName { get; set; } = string.Empty;
    }
}
