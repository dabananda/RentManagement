using AutoMapper;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Services
{
    public class ShopService : IShopService
    {
        private readonly IShopRepository _shopRepository;
        private readonly IMapper _mapper;

        public ShopService(IShopRepository shopRepository, IMapper mapper)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
        }

        public async Task<ShopDto> CreateShopAsync(ShopCreateDto shopDto)
        {
            var shop = _mapper.Map<Shop>(shopDto);

            await _shopRepository.AddShopAsync(shop);
            await _shopRepository.SaveChangesAsync();

            return _mapper.Map<ShopDto>(shop);
        }

        public async Task<IEnumerable<ShopDto>> GetAllShopsAsync()
        {
            var shops = await _shopRepository.GetAllShopsAsync();

            return _mapper.Map<IEnumerable<ShopDto>>(shops);
        }

        public async Task<ShopDto?> GetShopByIdAsync(int id)
        {
            var shop = await _shopRepository.GetShopByIdAsync(id);

            if (shop == null) return null;

            return _mapper.Map<ShopDto>(shop);
        }

        public async Task<bool> UpdateShopAsync(int id, ShopCreateDto shopDto)
        {
            var shopToUpdate = await _shopRepository.GetShopByIdAsync(id);

            if (shopToUpdate == null) return false;

            if (shopToUpdate.IsOccupied && shopToUpdate.ShopNumber != shopDto.ShopNumber) return false;

            shopToUpdate.ShopNumber = shopDto.ShopNumber;
            shopToUpdate.Floor = shopDto.Floor;
            shopToUpdate.AreaSqFt = shopDto.AreaSqFt;

            return await _shopRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteShopAsync(int id)
        {
            var shopToDelete = await _shopRepository.GetShopByIdAsync(id);

            if (shopToDelete == null) return false;

            if (shopToDelete.IsOccupied) return false;

            _shopRepository.DeleteShop(shopToDelete);

            return await _shopRepository.SaveChangesAsync();
        }
    }
}