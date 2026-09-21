using Paqueteria.Comun.Dtos.Municipios;
using Paqueteria.Dominio.Entidades.Catalogos;

namespace Paqueteria.Aplicacion.Modulos.Municipios;

public static class MunicipioMapeador
{
    extension(Municipio municipio)
    {
        public MunicipioRespuestaDto MapeaRespuestaDto()
        {
            return new MunicipioRespuestaDto(
                municipio.Id,
                municipio.Nombre,
                municipio.EstadoId,
                municipio.Estado?.Nombre ?? "Sin Estado");
        }
    }
}