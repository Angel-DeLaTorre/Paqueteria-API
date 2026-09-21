using Paqueteria.Cliente.Core.Auth;

namespace Paqueteria.Desktop.Services;

public class DesktopTokenStorage : ITokenStorage
{
    private const string TokenKey = "authToken";

    public async ValueTask<string?> GetTokenAsync()
    {
#if WINDOWS
        return await Microsoft.Maui.Storage.SecureStorage.Default.GetAsync(TokenKey);
#else
        // Retorno simulado para que tu Mac compile sin errores en desarrollo
        return await ValueTask.FromResult<string?>(null);
#endif
    }

    public async ValueTask SetTokenAsync(string token)
    {
#if WINDOWS
        await Microsoft.Maui.Storage.SecureStorage.Default.SetAsync(TokenKey, token);
#else
        await ValueTask.CompletedTask;
#endif
    }

    public ValueTask RemoveTokenAsync()
    {
#if WINDOWS
        Microsoft.Maui.Storage.SecureStorage.Default.Remove(TokenKey);
#endif
        return ValueTask.CompletedTask;
    }
}