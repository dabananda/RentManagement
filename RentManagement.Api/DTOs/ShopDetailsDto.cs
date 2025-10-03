namespace RentManagement.Api.DTOs
{
    public class ShopDetailsDto
    {
        public int Id { get; set; }
        public string ShopNumber { get; set; } = string.Empty;
        public decimal AreaSqFt { get; set; }
        public string Floor { get; set; } = string.Empty;
        public bool IsOccupied { get; set; }
    }
}
