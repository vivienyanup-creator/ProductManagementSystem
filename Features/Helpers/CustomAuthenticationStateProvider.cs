using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ProductManagement.Features.Helpers;

public class CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor) : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var user = httpContext.User;
        return new AuthenticationState(user);
    }

    public async Task MarkUserAsAuthenticated(string username, string role)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return;

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, role)
        };
        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);

        await httpContext.SignInAsync("Cookies", principal);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task MarkUserAsLoggedOut()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null) return;

        await httpContext.SignOutAsync("Cookies");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}

