using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Data;

namespace RentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboardData()
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

            return Ok(dashboardData);
        }

        [HttpGet("table")]
        public async Task<IActionResult> GetDashboardTableData()
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

            return Ok(agreements);
        }
    }
}
