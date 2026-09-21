using Paqueteria.Aplicacion.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.GuiasTransbordo.Mapeador;

public static class GuiaTransbordoMapeador
{

    extension(GuiaTransbordo transbordo)
    {
        public GuiaTransbordoRespuestaDto MapearGuiaTransbordoRespuestaDto()
        {
            return new GuiaTransbordoRespuestaDto(
                transbordo.Id,
                transbordo.GuiaId,
                transbordo.AsignacionId,
                transbordo.SucursalTransbordoId,
                transbordo.SucursalTransbordo?.Nombre ?? string.Empty,
                transbordo.FechaEscaneoIngreso,
                transbordo.FechaEscaneoSalida,
                transbordo.Observaciones
            );
        }
    }
}