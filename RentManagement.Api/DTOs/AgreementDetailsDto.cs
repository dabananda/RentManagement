namespace RentManagement.Api.DTOs
{
    public class AgreementDetailsDto
    {
        public int Id { get; set; }
        public decimal RentAmount { get; set; }
        public decimal SecurityFee { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; }
        public ShopDetailsDto Shop { get; set; } = null!;
        public TenantDetailsDto Tenant { get; set; } = null!;
    }
}
