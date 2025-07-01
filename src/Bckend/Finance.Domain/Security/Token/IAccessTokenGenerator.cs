

namespace Finance.Domain.Security.Token;
public interface IAccessTokenGenerator {
    public string Generate(Guid userIdentifier);
}
