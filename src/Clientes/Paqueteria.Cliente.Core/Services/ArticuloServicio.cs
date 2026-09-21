using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Articulos;

namespace Paqueteria.Cliente.Core.Services;

public sealed class ArticuloServicio(HttpClient httpClient) : IArticuloServicio
{
    public async Task<Respuesta<List<ArticuloRespuestaDto>>> ObtenerTodosAsync() => 
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<ArticuloRespuestaDto>>>("v1/articulo"));
    
    public async Task<Respuesta<ArticuloRespuestaDto>> CrearAsync(ArticuloCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<ArticuloRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/articulo", dto));
    
}