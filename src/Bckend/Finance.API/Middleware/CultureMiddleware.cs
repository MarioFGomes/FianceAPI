using System.Globalization;

namespace Finance.API.Middleware;
public class CultureMiddleware {

    private RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next) {
        _next = next;
    }
    public async Task Invoke(HttpContext context) {

        var suportedLanguage = CultureInfo.GetCultures(CultureTypes.AllCultures);

        var requestLanguage = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureinfo = new CultureInfo("en");

        if (!string.IsNullOrWhiteSpace(requestLanguage) &&
           suportedLanguage.Any(c => c.Name.Equals(requestLanguage))) {

            cultureinfo = new CultureInfo(requestLanguage);

        }

        CultureInfo.CurrentCulture = cultureinfo;
        CultureInfo.CurrentUICulture = cultureinfo;

        await _next(context);

    }
}
