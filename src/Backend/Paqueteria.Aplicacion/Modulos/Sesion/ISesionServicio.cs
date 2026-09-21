using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Sesion;

namespace Paqueteria.Aplicacion.Modulos.Sesion;

public interface ISesionServicio
{
    Task<Respuesta<SesionRespuestaDto>> IniciarSesionAsync(LoginSolicitudDto request);
    Task<Respuesta> CambiarContraseniaAsync(LoginSolicitudDto request);
}