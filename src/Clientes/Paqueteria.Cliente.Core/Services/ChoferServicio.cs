using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Choferes;

namespace Paqueteria.Cliente.Core.Services;

public sealed class ChoferServicio(HttpClient httpClient) : IChoferServicio
{
    
    public async Task<Respuesta<List<ChoferRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<ChoferRespuestaDto>>>("v1/chofer"));
    
    public async Task<Respuesta<ChoferRespuestaDto>> ObtenerPorIdAsync(int id) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<ChoferRespuestaDto>>($"v1/chofer/{id}"));

    public async Task<Respuesta<ChoferRespuestaDto>> CrearAsync(ChoferCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<ChoferRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/chofer", dto));
    
    public async Task<Respuesta> ActualizarAsync(Guid id, ChoferActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync("v1/chofer", dto));

    public async Task<Respuesta> EliminarAsync(Guid id) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.DeleteAsync($"v1/chofer/{id}"));
}