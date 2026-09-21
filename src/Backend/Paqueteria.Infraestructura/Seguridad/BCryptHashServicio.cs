using Paqueteria.Aplicacion.Comun.Interfaces;

namespace Paqueteria.Infrastructure.Seguridad;

public class BCryptHashServicio : IHashServicio
{
    public string Hash(string texto) => BCrypt.Net.BCrypt.HashPassword(texto);
    public bool Verificar(string texto, string hash) => BCrypt.Net.BCrypt.Verify(texto, hash);
}