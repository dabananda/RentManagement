namespace RentManagement.Api.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string userName, IList<string> roles);
    }
}
