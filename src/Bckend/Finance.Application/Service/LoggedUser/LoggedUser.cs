using Finance.Domain.Entities;
using Finance.Domain.Repositories;
using Finance.Domain.Security.Token;
using Microsoft.AspNetCore.Http;

namespace Finance.Application.Service.LoggedUser;
public class LoggedUser : ILoggedUser {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _UserRepository;
    private readonly IAccessTokenGenerator _AccessTokenGenerator;
    public LoggedUser(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IAccessTokenGenerator accessTokenGenerator)
    {
        _httpContextAccessor = httpContextAccessor;
        _UserRepository = userRepository;
        _AccessTokenGenerator = accessTokenGenerator;
    }
    public async Task<User?> RecoverUser() {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
            return null;

        var authHeader = httpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return null;

        var token = authHeader["Bearer ".Length..].Trim();
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var userId = _AccessTokenGenerator.GetUserIdFromToken(token);
        if (string.IsNullOrEmpty(userId))
           throw new UnauthorizedAccessException("Token inválido ou expirado.");

        var user = await _UserRepository.GetOneAsync(u => u.Id.ToString() == userId);

        return user;
    }
}
