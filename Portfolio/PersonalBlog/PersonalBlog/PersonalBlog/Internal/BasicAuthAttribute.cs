using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PersonalBlog.Internal;

public class BasicAuthAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var session = context.HttpContext.Session;
        if (session.GetString("IsAdmin") == "true")
        {
            return;
        }

        var config = context.HttpContext.RequestServices.GetService(typeof(IConfiguration)) as IConfiguration;
        var username = config["AdminAuth:Username"];
        var password = config["AdminAuth:Password"];

        var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();

        if (authHeader != null && authHeader.StartsWith("Basic "))
        {
            var encodedUsernamePassword = authHeader["Basic ".Length..].Trim();
            var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
            var parts = decodedUsernamePassword.Split(':');

            if (parts.Length == 2 && parts[0] == username && parts[1] == password)
            {
                session.SetString("IsAdmin", "true");
                
                return; // Authorized
            }
        }

        context.Result = new UnauthorizedResult();
        context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Admin Area\"";
    }
}
