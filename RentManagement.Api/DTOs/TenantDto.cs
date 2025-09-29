namespace RentManagement.Api.DTOs
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? Email { get; set; }
        public DateOnly LeaseStartDate { get; set; }
        public DateOnly? LeaseEndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
