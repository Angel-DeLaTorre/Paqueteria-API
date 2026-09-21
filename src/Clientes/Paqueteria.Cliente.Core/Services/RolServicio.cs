using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Roles;

namespace Paqueteria.Cliente.Core.Services;

public class RolServicio(HttpClient httpClient) : IRolServicio
{
    public async Task<Respuesta<List<RolRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<RolRespuestaDto>>>("v1/rol"));

    public async Task<Respuesta<RolRespuestaDto>> ObtenerPorIdAsync(Guid rolId) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<RolRespuestaDto>>($"v1/rol/{rolId}"));

    public async Task<Respuesta<RolRespuestaDto>> CrearAsync(RolCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<RolRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/rol", dto));

    public async Task<Respuesta> ActualizarAsync(Guid rolId, RolActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync($"v1/rol/{rolId}", dto));

    public async Task<Respuesta> EliminarAsync(Guid rolId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.DeleteAsync($"v1/rol/{rolId}"));
    
    public async Task<Respuesta> ActivarAsync(Guid rolId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/rol/{rolId}/activar", null));
    
    public async Task<Respuesta> DesactivarAsync(Guid rolId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/rol/{rolId}/descativar", null));
}