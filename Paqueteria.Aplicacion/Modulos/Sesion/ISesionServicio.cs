using Paqueteria.Application.DTOs;
using Paqueteria.Application.Modulos.Sesion.Dtos;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Sesion;

public interface ISesionServicio
{
    Task<Resultado<SesionRespuestaDto>> IniciarSesionAsync(LoginSolicitudDto request);
    Task<Resultado> CambiarContraseniaAsync(LoginSolicitudDto request);
}