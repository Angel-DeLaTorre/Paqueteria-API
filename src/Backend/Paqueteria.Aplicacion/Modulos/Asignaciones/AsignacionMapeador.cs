using Paqueteria.Aplicacion.Modulos.Sucursales;
using Paqueteria.Comun.Dtos.Asignaciones;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Asignaciones;

public static class AsignacionMapeador
{
    extension(Asignacion asignacion)
    {
        public AsignacionRespuestaDto MapearRespuestaDto()
        {
            var sucursalOrigen = asignacion.SucursalOrigen?.MapeaRespuestaDto();
            var sucursalDestino = asignacion.SucursalDestino?.MapeaRespuestaDto();
            
            return new AsignacionRespuestaDto
            (
                asignacion.Id,
                asignacion.Clave,
                sucursalOrigen,
                sucursalDestino,
                asignacion.ChoferId,
                asignacion.FechaPartida,
                asignacion.St1,
                asignacion.St2,
                asignacion.St3,
                asignacion.St4
            );
        }
    }
}