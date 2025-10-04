using RentManagement.Api.DTOs;

namespace RentManagement.Api.Interfaces
{
    public interface IShopService
    {
        Task<IEnumerable<ShopListDto>> GetAllShopsAsync();
        Task<ShopListDto?> GetShopByIdAsync(int id);
        Task<ShopDto> CreateShopAsync(ShopCreateDto shopDto);
        Task<bool> UpdateShopAsync(int id, ShopCreateDto shopDto);
        Task<bool> DeleteShopAsync(int id);
    }
}
