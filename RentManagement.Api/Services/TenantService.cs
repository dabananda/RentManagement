using Microsoft.AspNetCore.Http.HttpResults;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Services
{
    public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ITenantRepository tenantRepository) => _tenantRepository = tenantRepository;

        public async Task<TenantDto> CreateTenantAsync(TenantCreateDto tenantDto)
        {
            var tenant = new Tenant
            {
                Name = tenantDto.Name,
                ContactNumber = tenantDto.ContactNumber,
                Email = tenantDto.Email,
                LeaseStartDate = tenantDto.LeaseStartDate,
                LeaseEndDate = tenantDto.LeaseEndDate,
                IsActive = true
            };

            await _tenantRepository.AddTenantAsync(tenant);
            await _tenantRepository.SaveChangesAsync();

            return new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                ContactNumber = tenant.ContactNumber,
                Email = tenant.Email,
                LeaseStartDate = tenant.LeaseStartDate,
                LeaseEndDate = tenant.LeaseEndDate,
                IsActive = tenant.IsActive
            };
        }
        
        public async Task<IEnumerable<TenantDto>> GetAllTenantsAsync()
        {
            var tenants = await _tenantRepository.GetAllTenantsAsync();

            return tenants.Select(tenant => new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                ContactNumber = tenant.ContactNumber,
                Email = tenant.Email,
                LeaseStartDate = tenant.LeaseStartDate,
                LeaseEndDate = tenant.LeaseEndDate,
                IsActive = tenant.IsActive
            }).ToList();
        }
        
        public async Task<TenantDto?> GetTenantByIdAsync(int id)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(id);

            if (tenant == null)
            {
                return null;
            }

            return new TenantDto
            {
                Id = tenant.Id,
                Name = tenant.Name,
                ContactNumber = tenant.ContactNumber,
                Email = tenant.Email,
                LeaseStartDate = tenant.LeaseStartDate,
                LeaseEndDate = tenant.LeaseEndDate,
                IsActive = tenant.IsActive
            };
        }
        
        public async Task<bool> UpdateTenantAsync(int id, TenantCreateDto tenantDto)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(id);

            if (tenant == null)
            {
                return false;
            }

            tenant.Name = tenantDto.Name;
            tenant.ContactNumber = tenantDto.ContactNumber;
            tenant.Email = tenantDto.Email;
            tenant.LeaseStartDate = tenantDto.LeaseStartDate;
            tenant.LeaseEndDate = tenantDto.LeaseEndDate;

            _tenantRepository.UpdateTenant(tenant);
            return await _tenantRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteTenantAsync(int id)
        {
            var tenant = await _tenantRepository.GetTenantByIdAsync(id);

            if (tenant == null)
            {
                return false;
            }

            _tenantRepository.DeleteTenant(tenant);
            return await _tenantRepository.SaveChangesAsync();
        }
    }
}