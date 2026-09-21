using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Clientes;
using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Cliente.Core.Services;

public sealed class ClienteServicio(HttpClient httpClient) : IClienteServicio
{
    public async Task<Respuesta<List<ClienteRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<ClienteRespuestaDto>>>("v1/cliente"));
    
    public async Task<Respuesta<ClienteRespuestaDto>> ObtenerPorIdAsync(int id) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<ClienteRespuestaDto>>($"v1/cliente/{id}"));
    
    public async Task<Respuesta<ClienteRespuestaDto>> CrearAsync(ClienteCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<ClienteRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/cliente", dto));
    
    public async Task<Respuesta> ActualizarAsync(Guid id, ClienteActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync("v1/cliente", dto));
    
    public async Task<Respuesta> AgregarDireccion(Guid clienteId, DireccionDto dto) =>
        await  HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PostAsJsonAsync($"v1/cliente/{clienteId}/direccion", dto));

    public async Task<Respuesta> EliminarAsync(Guid clienteId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.DeleteAsync($"v1/cliente/{clienteId}"));
    
    public async Task<Respuesta> ActivarAsync(Guid clienteId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/cliente/{clienteId}/activar", null));
    
    public async Task<Respuesta> DesactivarAsync(Guid clienteId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/cliente/{clienteId}/desactivar", null));
    
    public async Task<Respuesta> ActivarDireccionAsync(Guid clienteId, Guid direccionId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/cliente/{clienteId}/direccion/{direccionId}/activar", null));
    
    public async Task<Respuesta> DesactivarDireccionAsync(Guid clienteId, Guid direccionId) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsync($"v1/cliente/{clienteId}/direccion/{direccionId}/activar", null));
}
