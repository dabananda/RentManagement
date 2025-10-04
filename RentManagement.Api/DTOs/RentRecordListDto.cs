namespace RentManagement.Api.DTOs
{
    public class RentRecordListDto
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public DateOnly PaidOn { get; set; }
        public int AgreementId { get; set; }
        public int ShopId { get; set; }
        public int TenantId { get; set; }
    }
}
