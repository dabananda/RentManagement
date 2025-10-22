using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Data;
using RentManagement.Api.Interfaces;

namespace RentManagement.Api.Repositories
{
    public class DashboardRepo : IDashboardRepo
    {
        private readonly AppDbContext _context;

        public DashboardRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetCardDataAsync()
        {
            var totalShops = await _context.Shops.CountAsync();
            var totalTenants = await _context.Tenants.CountAsync();
            var totalActiveAgreements = await _context.RentalAgreements.CountAsync(ra => ra.IsActive);
            var totalRentCollected = await _context.RentRecords.SumAsync(s => s.Amount);

            var dashboardData = new
            {
                TotalShops = totalShops,
                TotalTenants = totalTenants,
                TotalActiveAgreements = totalActiveAgreements,
                TotalRentCollected = totalRentCollected
            };

            return dashboardData;
        }

        public async Task<IEnumerable<object>> GetAgreementTableDataAsync()
        {
            var agreements = await _context.RentalAgreements
                .Include(ra => ra.Shop)
                .Include(ra => ra.Tenant)
                .Select(ra => new
                {
                    ra.Id,
                    ra.Shop.ShopNumber,
                    ra.Tenant.Name,
                    ra.StartDate,
                    ra.EndDate,
                    ra.RentAmount,
                    ra.SecurityFee,
                    ra.IsActive
                })
                .ToListAsync();

            return agreements;
        }
    }
}
