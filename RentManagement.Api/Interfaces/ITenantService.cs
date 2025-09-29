using RentManagement.Api.DTOs;

namespace RentManagement.Api.Interfaces
{
    public interface ITenantService
    {
        Task<IEnumerable<TenantDto>> GetAllTenantsAsync();
        Task<TenantDto?> GetTenantByIdAsync(int id);
        Task<TenantDto> CreateTenantAsync(TenantCreateDto tenantDto);
        Task<bool> UpdateTenantAsync(int id, TenantCreateDto tenantDto);
        Task<bool> DeleteTenantAsync(int id);
    }
}
