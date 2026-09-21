using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Asignaciones;

namespace Paqueteria.Cliente.Core.Services;

public sealed class AsignacionServicio(HttpClient httpClient) : IAsignacionServicio
{
    public async Task<Respuesta<List<AsignacionRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<AsignacionRespuestaDto>>>("v1/asignacion"));

    public async Task<Respuesta<AsignacionRespuestaDto>> ObtenerPorIdAsync(Guid id) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<AsignacionRespuestaDto>>($"v1/asignacion/{id}"));
    
    public async Task<Respuesta<AsignacionRespuestaDto>> CrearAsync(AsignacionCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<AsignacionRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/asignacion", dto));

    public async Task<Respuesta> CambiarEstadoAsync(int id, int estadoId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PostAsJsonAsync($"v1/Asignacion/{id}/estado", estadoId));

    public Task<Respuesta> GenerarReporteSalidaAsync(Guid asignacionId)
    {
        throw new NotImplementedException();
    }
}