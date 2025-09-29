using RentManagement.Api.DTOs;

namespace RentManagement.Api.Interfaces
{
    public interface IShopService
    {
        Task<IEnumerable<ShopDto>> GetAllShopsAsync();
        Task<ShopDto?> GetShopByIdAsync(int id);
        Task<ShopDto> CreateShopAsync(ShopCreateDto shopDto);
        Task<bool> UpdateShopAsync(int id, ShopCreateDto shopDto);
        Task<bool> DeleteShopAsync(int id);
    }
}
