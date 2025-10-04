using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Data;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly AppDbContext _context;

        public ShopRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            return await _context.Shops
                .Include(a => a.RentalAgreements)
                .ThenInclude(t => t.Tenant)
                .ToListAsync();
        }

        public async Task<Shop?> GetShopByIdAsync(int id)
        {
            return await _context.Shops
                .Include(a => a.RentalAgreements)
                .ThenInclude(t => t.Tenant)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddShopAsync(Shop shop)
        {
            await _context.Shops.AddAsync(shop);
        }

        public void DeleteShop(Shop shop)
        {
            _context.Shops.Remove(shop);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void UpdateShop(Shop shop)
        {
            _context.Shops.Update(shop);
        }
    }
}