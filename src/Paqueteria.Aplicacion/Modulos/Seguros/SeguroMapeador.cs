using Paqueteria.Application.Modulos.Seguros.Dtos;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Seguros;

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