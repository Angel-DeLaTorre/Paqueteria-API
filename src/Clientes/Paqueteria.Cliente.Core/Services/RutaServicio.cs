using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Rutas;

namespace Paqueteria.Cliente.Core.Services;

public sealed class RutaServicio(HttpClient httpClient) : IRutaServicio
{
    public async Task<Respuesta<List<RutaRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<RutaRespuestaDto>>>("v1/ruta"));

    public async Task<Respuesta<RutaRespuestaDto>> ObtenerPorIdAsync(Guid id) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<RutaRespuestaDto>>($"v1/ruta/{id}"));

    public async Task<Respuesta<RutaRespuestaDto>> CrearAsync(RutaCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<RutaRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/ruta", dto));

    public async Task<Respuesta> ActualizarAsync(Guid id, RutaActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync($"v1/ruta/{id}", dto));

    public async Task<Respuesta> EliminarAsync(Guid id) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.DeleteAsync($"v1/ruta/{id}"));
 
}