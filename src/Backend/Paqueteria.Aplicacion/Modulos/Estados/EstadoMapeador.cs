using Paqueteria.Comun.Dtos.Estados;
using Paqueteria.Dominio.Entidades.Catalogos;

namespace Paqueteria.Aplicacion.Modulos.Estados;

public static class EstadoMapeador
{
    extension(Estado estado)
    {
        public EstadoRespuestaDto MapeaRespuestaDto()
        {
            return new EstadoRespuestaDto
            (
                estado.Id,
                estado.Nombre,
                estado.Acronimo,
                estado.Pais
            );
        }
    }
}