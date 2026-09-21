namespace Paqueteria.Cliente.Core.Auth;

public interface ITokenStorage
{
    ValueTask<string?> GetTokenAsync();
    ValueTask SetTokenAsync(string token);
    ValueTask RemoveTokenAsync();
}