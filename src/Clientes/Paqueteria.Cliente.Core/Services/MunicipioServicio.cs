using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Municipios;

namespace Paqueteria.Cliente.Core.Services;

public class MunicipioServicio(HttpClient httpClient) : IMunicipioServicio
{
    public async Task<Respuesta<List<MunicipioRespuestaDto>>> ObtenerTodosAsync(string estado) =>
        await HttpEjecutor.EjecutarGetAsync(() => httpClient.GetFromJsonAsync<Respuesta<List<MunicipioRespuestaDto>>>($"v1/municipio/estado/{estado}"));

}