using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Data;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Repositories
{
    public class RentRecordRepository : IRentRecordRepository
    {
        private readonly AppDbContext _context;
        public RentRecordRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<RentRecord>> GetAllAsync(
            int? shopId = null, int? tenantId = null, int? year = null, int? month = null)
        {
            var q = _context.RentRecords
                .Include(r => r.Agreement).ThenInclude(a => a.Shop)
                .Include(r => r.Agreement).ThenInclude(a => a.Tenant)
                .AsQueryable();

            if (shopId.HasValue) q = q.Where(r => r.ShopId == shopId.Value);
            if (tenantId.HasValue) q = q.Where(r => r.TenantId == tenantId.Value);
            if (year.HasValue) q = q.Where(r => r.Year == year.Value);
            if (month.HasValue) q = q.Where(r => r.Month == month.Value);

            return await q.OrderByDescending(r => r.Year)
                          .ThenByDescending(r => r.Month)
                          .ToListAsync();
        }

        public async Task<RentRecord?> GetByIdAsync(int id) =>
            await _context.RentRecords
                .Include(r => r.Agreement).ThenInclude(a => a.Shop)
                .Include(r => r.Agreement).ThenInclude(a => a.Tenant)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddAsync(RentRecord record) => await _context.RentRecords.AddAsync(record);
        public void Update(RentRecord record) => _context.RentRecords.Update(record);
        public void Delete(RentRecord record) => _context.RentRecords.Remove(record);
        public async Task<bool> SaveChangesAsync() => (await _context.SaveChangesAsync() >= 0);

        public async Task<RentRecord?> FindByAgreementAndPeriodAsync(int agreementId, int year, int month) =>
            await _context.RentRecords.FirstOrDefaultAsync(r =>
                r.AgreementId == agreementId && r.Year == year && r.Month == month);
    }
}
