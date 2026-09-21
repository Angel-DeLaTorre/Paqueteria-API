using System.Net.Http.Json;
using Paqueteria.Cliente.Core.Auth;
using Paqueteria.Cliente.Core.Comun;
using Paqueteria.Cliente.Core.Interfaces;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Sesion;

namespace Paqueteria.Cliente.Core.Services;

public class AuthService(HttpClient httpClient, ITokenStorage tokenStorage) : IAuthService
{
    public async Task<Respuesta<SesionRespuestaDto>> LoginAsync(LoginSolicitudDto dto)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("v1/auth/login", dto);
            var result = await response.Content.ReadFromJsonAsync<Respuesta<SesionRespuestaDto>>();
            return result ?? Respuesta<SesionRespuestaDto>.Error(ErroresInfraestructura.RespuestaNula);
        }
        catch (HttpRequestException)
        {
            return Respuesta<SesionRespuestaDto>.Error(ErroresInfraestructura.ServidorNoDisponible);
        }
    }
}