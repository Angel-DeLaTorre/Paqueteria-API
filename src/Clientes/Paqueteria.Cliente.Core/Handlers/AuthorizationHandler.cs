using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Paqueteria.Cliente.Core.Auth;

namespace Paqueteria.Cliente.Core.Handlers;

public class AuthorizationHandler(
    ITokenStorage tokenStorage,
    NavigationManager navigation,
    AuthenticationStateProvider authStateProvider) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await tokenStorage.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await base.SendAsync(request, cancellationToken);

        // Si la API rechaza la petición por token expirado o inválido
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            await tokenStorage.RemoveTokenAsync();
            
            if (authStateProvider is CustomAuthenticationStateProvider customAuthStateProvider)
            {
                customAuthStateProvider.NotifyUserLogout();
            }

            navigation.NavigateTo("/login", replace: true);
        }

        return response;
    }
}