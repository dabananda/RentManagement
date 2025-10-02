using AutoMapper;
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
        private readonly IMapper _mapper;

        public RentalAgreementService(
            IRentalAgreementRepository agreementRepository,
            IShopRepository shopRepository,
            ITenantRepository tenantRepository,
            IMapper mapper)
        {
            _agreementRepository = agreementRepository;
            _shopRepository = shopRepository;
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<Tuple<RentalAgreementDto?, string?>> CreateAgreementAsync(RentalAgreementCreateDto agreementDto)
        {
            var shop = await _shopRepository.GetShopByIdAsync(agreementDto.ShopId);
            
            if (shop == null) return new Tuple<RentalAgreementDto?, string?>(null, "Shop not found.");

            var tenant = await _tenantRepository.GetTenantByIdAsync(agreementDto.TenantId);
            
            if (tenant == null) return new Tuple<RentalAgreementDto?, string?>(null, "Tenant not found.");

            if (shop.IsOccupied) return new Tuple<RentalAgreementDto?, string?>(null, $"Shop {shop.ShopNumber} is already occupied.");

            var agreement = _mapper.Map<RentalAgreement>(agreementDto);

            agreement.IsActive = true;

            shop.CurrentAgreement = agreement;
            tenant.CurrentAgreement = agreement;

            shop.IsOccupied = true;

            _shopRepository.UpdateShop(shop);
            _tenantRepository.UpdateTenant(tenant);

            await _agreementRepository.AddAgreementAsync(agreement);
            await _agreementRepository.SaveChangesAsync();

            var agreementDtoOut = _mapper.Map<RentalAgreementDto>(agreement);

            return new Tuple<RentalAgreementDto?, string?>(agreementDtoOut, null);
        }

        public async Task<bool> EndAgreementAsync(int id)
        {
            var agreement = await _agreementRepository.GetAgreementByIdAsync(id);

            if (agreement == null) return false;

            var shop = await _shopRepository.GetShopByIdAsync(agreement.ShopId);

            if (shop != null)
            {
                shop.IsOccupied = false;
                shop.CurrentAgreement = null;
                _shopRepository.UpdateShop(shop);
            }

            var tenant = await _tenantRepository.GetTenantByIdAsync(agreement.TenantId);

            if (tenant != null)
            {
                tenant.CurrentAgreement = null;
                _tenantRepository.UpdateTenant(tenant);
            }

            agreement.IsActive = false;
            agreement.EndDate = DateOnly.FromDateTime(DateTime.Now);

            _agreementRepository.UpdateAgreement(agreement);

            return await _agreementRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<RentalAgreementDto>> GetAllAgreementsAsync()
        {
            var agreements = await _agreementRepository.GetAllAgreementsAsync();

            return _mapper.Map<IEnumerable<RentalAgreementDto>>(agreements);
        }

        public async Task<RentalAgreementDto?> GetAgreementByIdAsync(int id)
        {
            var agreement = await _agreementRepository.GetAgreementByIdAsync(id);

            if (agreement == null) return null;

            return _mapper.Map<RentalAgreementDto>(agreement);
        }

        public async Task<bool> UpdateAgreementAsync(int id, RentalAgreementCreateDto agreementDto)
        {
            var agreement = await _agreementRepository.GetAgreementByIdAsync(id);

            if (agreement == null) return false;

            agreement.RentAmount = agreementDto.RentAmount;
            agreement.StartDate = agreementDto.StartDate;
            agreement.EndDate = agreementDto.EndDate;
            agreement.IsActive = agreement.IsActive;
            agreement.ShopId = agreementDto.ShopId;
            agreement.TenantId = agreementDto.TenantId;

            _agreementRepository.UpdateAgreement(agreement);

            return await _agreementRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAgreementAsync(int id)
        {
            var agreement = await _agreementRepository.GetAgreementByIdAsync(id);

            if (agreement == null) return false;
            if (agreement.IsActive) return false;

            _agreementRepository.DeleteAgreement(agreement);

            return await _agreementRepository.SaveChangesAsync();
        }
    }
}