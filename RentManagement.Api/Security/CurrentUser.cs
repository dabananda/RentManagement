using System.Security.Claims;

namespace RentManagement.Api.Security
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _http;
        public CurrentUser(IHttpContextAccessor http) => _http = http;

        public string? UserId =>
            _http.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        public bool IsAuthenticated =>
            _http.HttpContext?.User?.Identity?.IsAuthenticated == true;
    }
}
