using Paqueteria.Application.Modulos.Municipios.Dtos;
using Paqueteria.Dominio.Entidades.Catalogos;

namespace Paqueteria.Application.Modulos.Municipios;

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