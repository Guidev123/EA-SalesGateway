using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace Sales.API.Services.User;

public sealed class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public HttpContext GetHttpContext() => _httpContextAccessor.HttpContext!;
    public string GetToken()
    {
        var authorizationHeader = GetHttpContext().Request.Headers.Authorization.ToString();

        if (authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return authorizationHeader["Bearer ".Length..].Trim();

        return string.Empty;
    }

    public Guid? GetUserId()
    {
        var token = GetToken();
        if (string.IsNullOrEmpty(token)) return null;

        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
        if (Guid.TryParse(userIdClaim, out var userId)) return userId;

        return null;
    }
}
