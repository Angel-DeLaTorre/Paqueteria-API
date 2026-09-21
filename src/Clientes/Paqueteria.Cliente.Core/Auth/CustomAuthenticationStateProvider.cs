using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Authorization;

namespace Paqueteria.Cliente.Core.Auth;

public class CustomAuthenticationStateProvider(ITokenStorage tokenStorage) : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await tokenStorage.GetTokenAsync();

        if (string.IsNullOrWhiteSpace(token)) 
            return new AuthenticationState(_anonymous);

        var claims = ParseClaimsFromJwt(token).ToList();

        // Validar expiración (claim 'exp')
        var expClaim = claims.FirstOrDefault(c => c.Type == "exp")?.Value;
        if (!string.IsNullOrEmpty(expClaim) && long.TryParse(expClaim, out var expSeconds))
        {
            var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expSeconds).UtcDateTime;
            if (expirationTime <= DateTime.UtcNow)
            {
                // El token caducó
                await tokenStorage.RemoveTokenAsync();
                return new AuthenticationState(_anonymous);
            }
        }

        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(string token)
    {
        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var parts = jwt.Split('.');
        if (parts.Length < 2) return claims;

        var payload = parts[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs is null) return claims;

        foreach (var kvp in keyValuePairs) 
            claims.Add(new Claim(kvp.Key, kvp.Value.ToString() ?? string.Empty));

        return claims;
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }

        return Convert.FromBase64String(base64);
    }
}
