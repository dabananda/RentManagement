using RentManagement.Api.Models;

namespace RentManagement.Api.Interfaces
{
    public interface ITenantRepository
    {
        Task<IEnumerable<Tenant>> GetAllTenantsAsync();
        Task<Tenant?> GetTenantByIdAsync(int id);
        Task AddTenantAsync(Tenant tenant);
        void UpdateTenant(Tenant tenant);
        void DeleteTenant(Tenant tenant);
        Task<bool> SaveChangesAsync();
    }
}
