using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Finance.Domain.Repositories;
using Finance.Domain.Security.Token;
using Finance.Exception.Exceptions;
using Finance.Communication.Response;
using Finance.Exception;


namespace Finance.API.Filter;
public class AuthenticatedUser : AuthorizeAttribute, IAsyncAuthorizationFilter {
    private readonly IUserRepository _UserRepository;
    private readonly IAccessTokenGenerator _AccessTokenGenerator;
    public AuthenticatedUser(IAccessTokenGenerator accessTokenGenerator, IUserRepository userRepository) {
       _AccessTokenGenerator = accessTokenGenerator;
        _UserRepository = userRepository;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context) {
        try {
            var (token, httpContext) = RequestToken(context);

            var UserId = _AccessTokenGenerator.GetUserIdFromToken(token);

            var User = await _UserRepository.GetOneAsync(u => u.Id.ToString() == UserId) ?? throw new FinanceException(string.Empty);

            if (User != null) {
                
                httpContext.Items["User"] = User;
            }
            

        } catch (SecurityTokenExpiredException) {
            ExpiredToken(context);
        } catch {
            UserUnauthorized(context);
        }
    }

    private static (string,HttpContext) RequestToken(AuthorizationFilterContext context) {
        var httpContext = context.HttpContext;
        var authorization = context.HttpContext.Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(authorization)) throw new FinanceException(string.Empty);

        return (authorization["Bearer".Length..].Trim(), httpContext);

    }

    private static void ExpiredToken(AuthorizationFilterContext context) {
        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessage.ExpiredToken));
        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }

    private static void UserUnauthorized(AuthorizationFilterContext context) {
        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceMessage.UserWithoutPermission));
        context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
    }
}
