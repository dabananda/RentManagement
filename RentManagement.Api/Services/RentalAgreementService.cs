using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Services
{
    public class RentalAgreementService : IRentalAgreementService
    {
        private readonly IRentalAgreementRepository _agreementRepository;
        private readonly IShopRepository _shopRepository;
        private readonly ITenantRepository _tenantRepository;

        public RentalAgreementService(
            IRentalAgreementRepository agreementRepository,
            IShopRepository shopRepository,
            ITenantRepository tenantRepository)
        {
            _agreementRepository = agreementRepository;
            _shopRepository = shopRepository;
            _tenantRepository = tenantRepository;
        }

        public async Task<Tuple<RentalAgreementDto?, string?>> CreateAgreementAsync(RentalAgreementCreateDto agreementDto)
        {
            var shop = await _shopRepository.GetShopByIdAsync(agreementDto.ShopId);
            if (shop == null)
            {
                return new Tuple<RentalAgreementDto?, string?>(null, "Shop not found.");
            }

            var tenant = await _tenantRepository.GetTenantByIdAsync(agreementDto.TenantId);
            if (tenant == null)
            {
                return new Tuple<RentalAgreementDto?, string?>(null, "Tenant not found.");
            }

            if (shop.IsOccupied)
            {
                return new Tuple<RentalAgreementDto?, string?>(null, $"Shop {shop.ShopNumber} is already occupied.");
            }

            shop.IsOccupied = true;
            tenant.IsActive = true;

            _shopRepository.UpdateShop(shop);
            _tenantRepository.UpdateTenant(tenant);

            var agreement = new RentalAgreement
            {
                RentAmount = agreementDto.RentAmount,
                EffectiveDate = agreementDto.EffectiveDate,
                ShopId = agreementDto.ShopId,
                TenantId = agreementDto.TenantId
            };

            await _agreementRepository.AddAgreementAsync(agreement);
            await _agreementRepository.SaveChangesAsync();

            var agreementDtoOut = new RentalAgreementDto
            {
                Id = agreement.Id,
                RentAmount = agreement.RentAmount,
                EffectiveDate = agreement.EffectiveDate,
                ShopId = shop.Id,
                ShopNumber = shop.ShopNumber,
                TenantId = tenant.Id,
                TenantName = tenant.Name
            };

            return new Tuple<RentalAgreementDto?, string?>(agreementDtoOut, null);
        }

        public async Task<bool> EndAgreementAsync(int id)
        {
            var agreement = await _agreementRepository.GetAgreementByIdAsync(id);

            if (agreement == null)
            {
                return false;
            }

            var shop = await _shopRepository.GetShopByIdAsync(agreement.ShopId);
            var tenant = await _tenantRepository.GetTenantByIdAsync(agreement.TenantId);

            if (shop != null)
            {
                shop.IsOccupied = false;
                _shopRepository.UpdateShop(shop);
            }

            if (tenant != null)
            {
                tenant.IsActive = false;
                _tenantRepository.UpdateTenant(tenant);
            }

            _agreementRepository.DeleteAgreement(agreement);

            return await _agreementRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RentalAgreementDto>> GetAllAgreementsAsync()
        {
            var agreements = await _agreementRepository.GetAllAgreementsAsync();

            return agreements.Select(a => new RentalAgreementDto
            {
                Id = a.Id,
                RentAmount = a.RentAmount,
                EffectiveDate = a.EffectiveDate,
                ShopId = a.ShopId,
                ShopNumber = a.Shop.ShopNumber,
                TenantId = a.TenantId,
                TenantName = a.Tenant.Name
            }).ToList();
        }

        public async Task<RentalAgreementDto?> GetAgreementByIdAsync(int id)
        {
            var agreement = await _agreementRepository.GetAgreementByIdAsync(id);

            if (agreement == null)
            {
                return null;
            }

            return new RentalAgreementDto
            {
                Id = agreement.Id,
                RentAmount = agreement.RentAmount,
                EffectiveDate = agreement.EffectiveDate,
                ShopId = agreement.ShopId,
                ShopNumber = agreement.Shop.ShopNumber,
                TenantId = agreement.TenantId,
                TenantName = agreement.Tenant.Name
            };
        }

        public async Task<bool> UpdateAgreementAsync(int id, RentalAgreementCreateDto agreementDto)
        {
            var agreement = await _agreementRepository.GetAgreementByIdAsync(id);

            if (agreement == null)
            {
                return false;
            }

            agreement.RentAmount = agreementDto.RentAmount;
            agreement.EffectiveDate = agreementDto.EffectiveDate;
            agreement.ShopId = agreementDto.ShopId;
            agreement.TenantId = agreementDto.TenantId;

            _agreementRepository.UpdateAgreement(agreement);
            return await _agreementRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAgreementAsync(int id)
        {
            var agreement = await _agreementRepository.GetAgreementByIdAsync(id);

            if (agreement == null)
            {
                return false;
            }

            _agreementRepository.DeleteAgreement(agreement);
            return await _agreementRepository.SaveChangesAsync();
        }
    }
}