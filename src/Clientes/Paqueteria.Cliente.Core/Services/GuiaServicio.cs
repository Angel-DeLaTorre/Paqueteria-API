using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Guias;

namespace Paqueteria.Cliente.Core.Services;

public sealed class GuiaServicio(HttpClient httpClient) : IGuiaServicio
{
    public async Task<Respuesta<List<GuiaRespuestaDto>>> ObtenerTodosAsync() =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<GuiaRespuestaDto>>>("v1/guia"));

    public async Task<Respuesta<GuiaRespuestaDto>> ObtenerPorIdAsync(Guid id) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<GuiaRespuestaDto>>($"v1/guia/{id}"));

    public async Task<Respuesta<GuiaRespuestaDto>> ObtenerPorNumeroGuiaAsync(string numeroGuia) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<GuiaRespuestaDto>>($"v1/guia/rastreo/{numeroGuia}"));

    public async Task<Respuesta<GuiaRespuestaDto>> CrearAsync(GuiaCrearDto dto) =>
        await HttpEjecutor.EjecutarAsync<GuiaRespuestaDto>(() => httpClient.PostAsJsonAsync("v1/guia", dto));
    
    public async Task<Respuesta> ActualizarAsync(GuiaActualizarDto dto) =>
        await HttpEjecutor.EjecutarSinRetornoAsync(() => httpClient.PatchAsJsonAsync($"v1/guia/{dto.GuiaId}", dto));

    public Task<Respuesta> GenerarEtiquetaAsync(Guid guiaId)
    {
        throw new NotImplementedException();
    }

    /*

    public async Task<Respuesta<CotizacionGuiaDto>> CotizarGuiaAsync(CotizacionGuiaDto dto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("guias/cotizar", dto);
            var res = await response.Content.ReadFromJsonAsync<Respuesta<CotizacionGuiaDto>>();
            return res ?? Respuesta<CotizacionGuiaDto>.Error(ErroresInfraestructura.RespuestaNula);
        }
        catch (HttpRequestException)
        {
            return Respuesta<CotizacionGuiaDto>.Error(ErroresInfraestructura.ServidorNoDisponible);
        }
    }
    *
     */
}