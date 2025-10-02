using Microsoft.EntityFrameworkCore;
using RentManagement.Api.Data;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Repositories
{
    public class RentalAgreementRepository : IRentalAgreementRepository
    {
        private readonly AppDbContext _context;

        public RentalAgreementRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<RentalAgreement>> GetAllAgreementsAsync()
        {
            return await _context.RentalAgreements.Include(ra => ra.Shop).Include(ra => ra.Tenant).ToListAsync();
        }

        public async Task<RentalAgreement?> GetAgreementByIdAsync(int id)
        {
            return await _context.RentalAgreements.Include(ra => ra.Shop).Include(ra => ra.Tenant).FirstOrDefaultAsync(ra => ra.Id == id);
        }
        public async Task AddAgreementAsync(RentalAgreement agreement)
        {
            await _context.RentalAgreements.AddAsync(agreement);
        }

        public void UpdateAgreement(RentalAgreement agreement)
        {
            _context.RentalAgreements.Update(agreement);
        }
        public void DeleteAgreement(RentalAgreement agreement)
        {
            _context.RentalAgreements.Remove(agreement);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}