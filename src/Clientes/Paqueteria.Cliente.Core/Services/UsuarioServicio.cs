using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Usuarios;

namespace Paqueteria.Cliente.Core.Services;

public sealed class UsuarioServicio(HttpClient httpClient) : IUsuarioServicio
{
    public async Task<Respuesta<List<UsuarioRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<UsuarioRespuestaDto>>>("v1/usuario"));

    public async Task<Respuesta<UsuarioRespuestaDto>> ObtenerPorIdAsync(Guid id) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<UsuarioRespuestaDto>>($"v1/usuario/{id}"));

    public async Task<Respuesta<UsuarioRespuestaDto>> CrearAsync(UsuarioCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<UsuarioRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/usuario", dto));

    public async Task<Respuesta> ActualizarAsync(Guid id, UsuarioActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync($"v1/usuario/{id}", dto));

    public async Task<Respuesta> EliminarAsync(Guid id) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.DeleteAsync($"v1/usuario/{id}"));
    
}