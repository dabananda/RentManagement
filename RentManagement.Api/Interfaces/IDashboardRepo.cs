namespace RentManagement.Api.Interfaces
{
    public interface IDashboardRepo
    {
        Task<object> GetCardDataAsync();
        Task<IEnumerable<object>> GetAgreementTableDataAsync();
    }
}
