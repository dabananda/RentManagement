using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Data;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tenant>> GetAllTenantsAsync()
        {
            return await _context.Tenants
                            .Include(t => t.RentalAgreements.Where(a => a.IsActive))
                            .ThenInclude(a => a.Shop)
                            .ToListAsync();
        }

        public async Task<Tenant?> GetTenantByIdAsync(int id)
        {
            return await _context.Tenants
                .Include(t => t.RentalAgreements.Where(a => a.IsActive))
                .ThenInclude(a => a.Shop)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTenantAsync(Tenant tenant) => await _context.Tenants.AddAsync(tenant);

        public void UpdateTenant(Tenant tenant) => _context.Tenants.Update(tenant);

        public void DeleteTenant(Tenant tenant) => _context.Tenants.Remove(tenant);

        public async Task<bool> SaveChangesAsync() => (await _context.SaveChangesAsync() >= 0);
    }
}
