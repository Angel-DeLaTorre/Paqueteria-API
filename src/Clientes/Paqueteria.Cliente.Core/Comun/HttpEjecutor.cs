using System.Net.Http.Json;
using System.Text.Json;
using Paqueteria.Comun.Comun;

namespace Paqueteria.Cliente.Core.Comun;

public static class HttpEjecutor
{
    public static async Task<Respuesta<T>> EjecutarAsync<T>(Func<Task<HttpResponseMessage>> peticion)
    {
        try
        {
            var httpRespuesta = await peticion();
            
            // Si la respuesta no es HTTP 2xx, opcionalmente puedes manejarlo aquí
            if (!httpRespuesta.IsSuccessStatusCode)
            {
                // Intenta leer el cuerpo de error si la API responde con la estructura Respuesta<T>
                var respuestaError = await httpRespuesta.Content.ReadFromJsonAsync<Respuesta<T>>();
                return respuestaError ?? Respuesta<T>.Error(ErroresInfraestructura.RespuestaNula);
            }

            var respuesta = await httpRespuesta.Content.ReadFromJsonAsync<Respuesta<T>>();
            return respuesta ?? Respuesta<T>.Error(ErroresInfraestructura.RespuestaNula);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error de conversión JSON: {ex.Message} | Ruta: {ex.Path}");
            return Respuesta<T>.Error("Error al deserializar la respuesta del servidor.");
        }
        catch (HttpRequestException)
        {
            return Respuesta<T>.Error(ErroresInfraestructura.ServidorNoDisponible);
        }
    }

    // Sobrecarga para peticiones GET directas usando GetFromJsonAsync
    public static async Task<Respuesta<T>> EjecutarGetAsync<T>(Func<Task<Respuesta<T>?>> peticion)
    {
        try
        {
            var res = await peticion();
            return res ?? Respuesta<T>.Error(ErroresInfraestructura.RespuestaNula);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error de conversión JSON: {ex.Message} | Ruta: {ex.Path}");
            return Respuesta<T>.Error("Error al deserializar la respuesta del servidor.");
        }
        catch (HttpRequestException)
        {
            return Respuesta<T>.Error(ErroresInfraestructura.ServidorNoDisponible);
        }
    }

    // Sobrecarga para operaciones sin payload de retorno (Respuesta no genérica)
    public static async Task<Respuesta> EjecutarSinRetornoAsync(Func<Task<HttpResponseMessage>> peticion)
    {
        try
        {
            var httpRespuesta = await peticion();
            var respuesta = await httpRespuesta.Content.ReadFromJsonAsync<Respuesta>();
            return respuesta ?? Respuesta.Error(ErroresInfraestructura.RespuestaNula);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error de conversión JSON: {ex.Message} | Ruta: {ex.Path}");
            return Respuesta.Error("Error al deserializar la respuesta del servidor.");
        }
        catch (HttpRequestException)
        {
            return Respuesta.Error(ErroresInfraestructura.ServidorNoDisponible);
        }
    }
}
