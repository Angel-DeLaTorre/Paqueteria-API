using Paqueteria.Comun.Dtos.Seguros;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Seguros;

public static class SeguroMapeador
{
    extension(Seguro seguro)
    {
        public SeguroRespuestaDto MapeaRespuestaDto()
        {
            return new SeguroRespuestaDto
            (
                SeguroId: seguro.Id,
                Nombre: seguro.Nombre,
                Estatus: seguro.Estatus
            );
        }
    }
}