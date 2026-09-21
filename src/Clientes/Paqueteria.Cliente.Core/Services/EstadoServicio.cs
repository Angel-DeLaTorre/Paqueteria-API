using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Estados;

namespace Paqueteria.Cliente.Core.Services;

public class EstadoServicio(HttpClient httpClient) : IEstadoServicio
{

    public async Task<Respuesta<List<EstadoRespuestaDto>>> ObtenerTodosAsync(string pais) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<EstadoRespuestaDto>>>($"v1/estado/{pais}" ));

}