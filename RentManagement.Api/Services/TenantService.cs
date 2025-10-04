using AutoMapper;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;

        public TenantService(
            ITenantRepository tenantRepository,
            IMapper mapper)
        {
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public async Task<TenantDetailsDto> CreateTenantAsync(TenantCreateDto tenantDto)
        {
            var tenant = _mapper.Map<Tenant>(tenantDto);

            await _tenantRepository.AddTenantAsync(tenant);
            await _tenantRepository.SaveChangesAsync();

            return _mapper.Map<TenantDetailsDto>(tenant);
        }
        
        public async Task<IEnumerable<TenantDto>> GetAllTenantsAsync()
        {
            var tenants = await _tenantRepository.GetAllTenantsAsync();

            return _mapper.Map<IEnumerable<TenantDto>>(tenants);
        }
        
        public async Task<TenantDto?> GetTenantByIdAsync(int id)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(id);

            if (tenant == null) return null;

            return _mapper.Map<TenantDto>(tenant);
        }
        
        public async Task<bool> UpdateTenantAsync(int id, TenantCreateDto tenantDto)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(id);

            if (tenant == null) return false;

            tenant.Name = tenantDto.Name;
            tenant.Phone = tenantDto.Phone;
            tenant.Email = tenantDto.Email;

            _tenantRepository.UpdateTenant(tenant);

            return await _tenantRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteTenantAsync(int id)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(id);

            if (tenant == null) return false;

            _tenantRepository.DeleteTenant(tenant);

            return await _tenantRepository.SaveChangesAsync();
        }
    }
}