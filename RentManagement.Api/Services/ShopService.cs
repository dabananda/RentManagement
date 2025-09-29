using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;

        public ShopService(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public async Task<ShopDto> CreateShopAsync(ShopCreateDto shopDto)
        {
            var shop = new Shop
            {
                ShopNumber = shopDto.ShopNumber,
                Floor = shopDto.Floor,
                AreaSqFt = shopDto.AreaSqFt,
                IsOccupied = false
            };

            await _shopRepository.AddShopAsync(shop);
            await _shopRepository.SaveChangesAsync();

            return new ShopDto
            {
                Id = shop.Id,
                ShopNumber = shop.ShopNumber,
                Floor = shop.Floor,
                AreaSqFt = shop.AreaSqFt,
                IsOccupied = shop.IsOccupied
            };
        }

        public async Task<IEnumerable<ShopDto>> GetAllShopsAsync()
        {
            var shops = await _shopRepository.GetAllShopsAsync();

            return shops.Select(s => new ShopDto
            {
                Id = s.Id,
                ShopNumber = s.ShopNumber,
                Floor = s.Floor,
                AreaSqFt = s.AreaSqFt,
                IsOccupied = s.IsOccupied
            }).ToList();
        }

        public async Task<ShopDto?> GetShopByIdAsync(int id)
        {
            var shop = await _shopRepository.GetShopByIdAsync(id);

            if (shop == null)
            {
                return null;
            }

            return new ShopDto
            {
                Id = shop.Id,
                ShopNumber = shop.ShopNumber,
                Floor = shop.Floor,
                AreaSqFt = shop.AreaSqFt,
                IsOccupied = shop.IsOccupied
            };
        }

        public async Task<bool> UpdateShopAsync(int id, ShopCreateDto shopDto)
        {
            var shopToUpdate = await _shopRepository.GetShopByIdAsync(id);
            if (shopToUpdate == null)
            {
                return false;
            }

            if (shopToUpdate.IsOccupied && (shopToUpdate.ShopNumber != shopDto.ShopNumber || shopToUpdate.AreaSqFt != shopDto.AreaSqFt))
            {
                return false;
            }

            shopToUpdate.ShopNumber = shopDto.ShopNumber;
            shopToUpdate.Floor = shopDto.Floor;
            shopToUpdate.AreaSqFt = shopDto.AreaSqFt;

            return await _shopRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteShopAsync(int id)
        {
            var shopToDelete = await _shopRepository.GetShopByIdAsync(id);
            if (shopToDelete == null)
            {
                return false;
            }

            if (shopToDelete.IsOccupied)
            {
                return false;
            }

            _shopRepository.DeleteShop(shopToDelete);
            return await _shopRepository.SaveChangesAsync();
        }
    }
}