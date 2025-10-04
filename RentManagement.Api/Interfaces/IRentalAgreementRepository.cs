using RentManagement.Api.Models;

namespace RentManagement.Api.Interfaces
{
    public interface IRentalAgreementRepository
    {
        Task<IEnumerable<RentalAgreement>> GetAllAgreementsAsync();
        Task<RentalAgreement?> GetAgreementByIdAsync(int id);
        Task AddAgreementAsync(RentalAgreement agreement);
        void UpdateAgreement(RentalAgreement agreement);
        void DeleteAgreement(RentalAgreement agreement);
        Task<bool> SaveChangesAsync();
        Task<RentalAgreement?> GetActiveAgreementForShopAsync(int shopId, DateOnly onDate);
    }
}
