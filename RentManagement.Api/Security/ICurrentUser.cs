namespace RentManagement.Api.Security
{
    public interface ICurrentUser
    {
        string? UserId { get; }
        bool IsAuthenticated { get; }
    }
}
