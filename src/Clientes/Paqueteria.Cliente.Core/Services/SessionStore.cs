namespace Paqueteria.Cliente.Core.Services;

public class SessionStore
{
    private string? _token;

    public string? GetToken()
    {
        return _token;
    }

    public void SetToken(string? token)
    {
        _token = token;
    }
}