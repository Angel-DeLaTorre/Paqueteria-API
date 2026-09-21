namespace Paqueteria.Aplicacion.Comun.Interfaces;

public interface IHashServicio
{
    string Hash(string texto);
    bool Verificar(string texto, string hash);
}