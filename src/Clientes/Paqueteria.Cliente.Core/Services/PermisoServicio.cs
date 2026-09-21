using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Permisos;

namespace Paqueteria.Cliente.Core.Services;

public class PermisoServicio(HttpClient httpClient) : IPermisoServicio
{
    public async Task<Respuesta<List<PermisoRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<PermisoRespuestaDto>>>("v1/permiso"));

    public async Task<Respuesta<PermisoRespuestaDto>> ObtenerPorIdAsync(Guid permisoId) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<PermisoRespuestaDto>>($"v1/permiso/{permisoId}"));

    public async Task<Respuesta<PermisoRespuestaDto>> CrearAsync(PermisoCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<PermisoRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/permiso", dto));

    public async Task<Respuesta> ActualizarAsync(Guid permisoId, PermisoActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync($"v1/permiso/{permisoId}", dto));

    public async Task<Respuesta> EliminarAsync(Guid permisoId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.DeleteAsync($"v1/permiso/{permisoId}"));
    
    public async Task<Respuesta> ActivarAsync(Guid permisoId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/permiso/{permisoId}/activar", null));
    
    public async Task<Respuesta> DesactivarAsync(Guid permisoId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/permiso/{permisoId}/descativar", null));
}