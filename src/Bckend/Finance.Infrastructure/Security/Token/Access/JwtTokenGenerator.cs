using Finance.Domain.Security.Token;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Finance.Infrastructure.Security.Token.Access;
public class JwtTokenGenerator : IAccessTokenGenerator {
    private readonly uint _expirationTimeMinute;
    private readonly string _siginKey;
    public JwtTokenGenerator(uint expirationTimeMinute, string siginKey) {
        _expirationTimeMinute = expirationTimeMinute;
        _siginKey = siginKey;
    }
    public string Generate(Guid userIdentifier) {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid,userIdentifier.ToString())
        };

        var TokenDescripotor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_expirationTimeMinute),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(TokenDescripotor);
        return tokenHandler.WriteToken(securityToken);
    }

    private SymmetricSecurityKey SecurityKey() {
        var bytes = Encoding.UTF8.GetBytes(_siginKey);
        return new SymmetricSecurityKey(bytes);
    }
}

