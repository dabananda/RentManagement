namespace RentManagement.Api.DTOs
{
    public class AgreementWithTenantDto
    {
        public int Id { get; set; }
        public decimal RentAmount { get; set; }
        public decimal SecurityFee { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; }
        public int TenantId { get; set; }
        public TenantDetailsDto Tenant { get; set; } = null!;
    }
}
