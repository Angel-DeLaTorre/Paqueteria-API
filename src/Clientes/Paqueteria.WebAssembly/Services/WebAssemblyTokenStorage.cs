using Microsoft.JSInterop;
using Paqueteria.Cliente.Core.Auth;

namespace Paqueteria.WebAssembly.Services;

public class WebAssemblyTokenStorage(IJSRuntime jsRuntime) : ITokenStorage
{
    private const string TokenKey = "authToken";

    public async ValueTask<string?> GetTokenAsync()
    {
        return await jsRuntime.InvokeAsync<string?>("localStorage.getItem", TokenKey);
    }

    public async ValueTask SetTokenAsync(string token)
    {
        await jsRuntime.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
    }

    public async ValueTask RemoveTokenAsync()
    {
        await jsRuntime.InvokeVoidAsync("localStorage.removeItem", TokenKey);
    }
}