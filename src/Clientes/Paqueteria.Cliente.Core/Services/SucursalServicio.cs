using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Sucursales;

namespace Paqueteria.Cliente.Core.Services;

public sealed class SucursalServicio(HttpClient httpClient) : ISucursalServicio
{
    public async Task<Respuesta<List<SucursalRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<SucursalRespuestaDto>>>("v1/sucursal"));

    public async Task<Respuesta<SucursalRespuestaDto>> ObtenerPorIdAsync(Guid id) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<SucursalRespuestaDto>>($"v1/sucursal/{id}"));

    public async Task<Respuesta<SucursalRespuestaDto>> CrearAsync(SucursalCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<SucursalRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/sucursal", dto));

    public async Task<Respuesta> ActualizarAsync(Guid id, SucursalActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync($"v1/sucursal/{id}", dto));

    public async Task<Respuesta> EliminarAsync(Guid id) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.DeleteAsync($"v1/sucursal/{id}"));
    
    
}