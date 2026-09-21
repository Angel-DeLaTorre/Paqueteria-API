using Paqueteria.Aplicacion.Modulos.Sucursales;
using Paqueteria.Comun.Dtos.Rutas;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Rutas;

public static class RutaMapeador
{
    extension(Ruta ruta)
    {
        public RutaRespuestaDto MapeaRespuestaDto()
        {
            var sucursalOrigen = ruta.SucursalOrigen.MapeaRespuestaDto();
            var sucursalDestino = ruta.SucursalOrigen.MapeaRespuestaDto();
            
            return new RutaRespuestaDto
            (
                RutaId: ruta.Id,
                SucursalOrigenId: ruta.SucursalOrigenId,
                SucursalOrigen: sucursalOrigen,
                SucursalDestinoId: ruta.SucursalDestinoId,
                SucursalDestino: sucursalDestino,
                Descripcion: ruta.Descripcion
            );
        }
    }
}