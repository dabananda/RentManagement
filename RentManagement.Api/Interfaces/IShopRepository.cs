using RentManagement.Api.Models;

namespace RentManagement.Api.Interfaces
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllShopsAsync();
        Task<Shop?> GetShopByIdAsync(int id);
        Task AddShopAsync(Shop shop);
        void UpdateShop(Shop shop);
        void DeleteShop(Shop shop);
        Task<bool> SaveChangesAsync();
    }
}
