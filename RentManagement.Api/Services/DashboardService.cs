using RentManagement.Api.Interfaces;

namespace RentManagement.Api.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepo _repo;

        public DashboardService(IDashboardRepo repo)
        {
            _repo = repo;
        }

        public async Task<object> GetCardDataAsync()
        {
            return await _repo.GetCardDataAsync();
        }

        public async Task<IEnumerable<object>> GetAgreementTableDataAsync()
        {
            return await _repo.GetAgreementTableDataAsync();
        }
    }
}
