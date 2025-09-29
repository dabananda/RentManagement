namespace RentManagement.Api.DTOs
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string ShopNumber { get; set; } = string.Empty;
        public string Floor { get; set; } = string.Empty;
        public decimal AreaSqFt { get; set; }
        public bool IsOccupied { get; set; }
    }
}
