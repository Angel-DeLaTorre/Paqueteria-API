using Paqueteria.Application.Modulos.Rutas.Dtos;
using Paqueteria.Application.Modulos.Sucursales;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Rutas;

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