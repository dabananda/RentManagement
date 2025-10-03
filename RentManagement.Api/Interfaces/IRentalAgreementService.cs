using RentManagement.Api.DTOs;

namespace RentManagement.Api.Interfaces
{
    public interface IRentalAgreementService
    {
        Task<IEnumerable<AgreementDetailsDto>> GetAllAgreementsAsync();
        Task<RentalAgreementDto?> GetAgreementByIdAsync(int id);
        Task<Tuple<RentalAgreementDto?, string?>> CreateAgreementAsync(RentalAgreementCreateDto agreementDto);
        Task<bool> EndAgreementAsync(int id);
        Task<bool> UpdateAgreementAsync(int id, RentalAgreementCreateDto agreementDto);
        Task<bool> DeleteAgreementAsync(int id);
    }
}
