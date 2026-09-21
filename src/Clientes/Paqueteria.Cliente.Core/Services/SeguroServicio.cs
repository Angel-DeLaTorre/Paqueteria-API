using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Seguros;

namespace Paqueteria.Cliente.Core.Services;

public class SeguroServicio(HttpClient httpClient) : ISeguroServicio
{
    public async Task<Respuesta<List<SeguroRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<SeguroRespuestaDto>>>("v1/seguro"));

    public async Task<Respuesta<SeguroRespuestaDto>> ObtenerPorIdAsync(Guid id) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<SeguroRespuestaDto>>($"v1/seguro/{id}"));

    public async Task<Respuesta<SeguroRespuestaDto>> CrearAsync(SeguroCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<SeguroRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/seguro", dto));

    public async Task<Respuesta> ActualizarAsync(Guid seguroId, SeguroActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync($"v1/seguro/{seguroId}", dto));

    public async Task<Respuesta> EliminarAsync(Guid id) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.DeleteAsync($"v1/seguro/{id}"));
    
    public async Task<Respuesta> ActivarAsync(Guid seguroId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/seguro/{seguroId}/activar", null));
    
    public async Task<Respuesta> DesactivarAsync(Guid seguroId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/seguro/{seguroId}/descativar", null));
}