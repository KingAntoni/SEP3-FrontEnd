using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
namespace Model.Auth;

public class AuthorizationPolicies
{
    public static void AddPolicies(IServiceCollection services)
    {
        services.AddAuthorizationCore(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimTypes.Role, "Admin");
            });
            options.AddPolicy("User", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimTypes.Role, "User");
            });
        });
    }
}