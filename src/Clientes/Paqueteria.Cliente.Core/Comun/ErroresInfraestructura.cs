using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;

namespace Paqueteria.Cliente.Core.Comun;

public static class ErroresInfraestructura
{
    public static readonly Error ServidorNoDisponible = new("Http.Unavailable", "No se pudo conectar con el servidor.");
    public static readonly Error RespuestaNula = new("Http.NullResponse", "El servidor retornó una respuesta vacía.");
    public static readonly Error ErrorDesconocido = new("Http.Unknown", "Ocurrió un error inesperado en la petición.");
}