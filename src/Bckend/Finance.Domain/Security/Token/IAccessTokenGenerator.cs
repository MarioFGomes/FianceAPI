

using System.Security.Claims;

namespace Finance.Domain.Security.Token;
public interface IAccessTokenGenerator {
    public string Generate(Guid userIdentifier);
    public ClaimsPrincipal ValidateToken(string token);
    public string GetUserIdFromToken(string token);
}
