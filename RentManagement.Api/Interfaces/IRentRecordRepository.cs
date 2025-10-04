using RentManagement.Api.Models;

namespace RentManagement.Api.Interfaces
{
    public interface IRentRecordRepository
    {
        Task<IEnumerable<RentRecord>> GetAllAsync(
            int? shopId = null,
            int? tenantId = null,
            int? year = null,
            int? month = null);
        Task<RentRecord?> GetByIdAsync(int id);
        Task AddAsync(RentRecord record);
        void Update(RentRecord record);
        void Delete(RentRecord record);
        Task<bool> SaveChangesAsync();
        Task<RentRecord?> FindByAgreementAndPeriodAsync(
            int agreementId, int year, int month);
    }
}
