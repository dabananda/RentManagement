namespace RentManagement.Api.Interfaces
{
    public interface IDashboardService
    {
        Task<object> GetCardDataAsync();
        Task<IEnumerable<object>> GetAgreementTableDataAsync(string? search = null);
    }
}
