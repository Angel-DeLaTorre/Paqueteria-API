using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Sesion;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IAuthService
{
    Task<Respuesta<SesionRespuestaDto>> LoginAsync(LoginSolicitudDto request);
}