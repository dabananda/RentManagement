namespace RentManagement.Api.DTOs
{
    public class RentRecordDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaidOn { get; set; }
        public string? Notes { get; set; }
        public AgreementWithTenantDto Agreement { get; set; } = null!;
        public ShopDto Shop { get; set; } = null!;
    }
}
