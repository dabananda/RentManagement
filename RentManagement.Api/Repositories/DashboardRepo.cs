using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<object>> GetAgreementTableDataAsync([FromQuery] string? search = null)
        {
            var query = _context.RentalAgreements
                .Include(s => s.Shop)
                .Include(t => t.Tenant)
                .AsQueryable();

            if (search != null)
            {
                search = search.ToLower();
                query = query.Where(q =>
                    q.Shop.ShopNumber.Contains(search) ||
                    q.Tenant.Name.ToLower().Contains(search) ||
                    q.RentAmount.ToString().Contains(search) ||
                    q.SecurityFee.ToString().Contains(search) ||
                    q.Id.ToString().Contains(search)
                 );
            }

            var agreements = await query
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
