using Paqueteria.Application.Modulos.Sesion.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Sesion;

public interface ISesionServicio
{
    Task<Respuesta<SesionRespuestaDto>> IniciarSesionAsync(LoginSolicitudDto request);
    Task<Respuesta> CambiarContraseniaAsync(LoginSolicitudDto request);
}