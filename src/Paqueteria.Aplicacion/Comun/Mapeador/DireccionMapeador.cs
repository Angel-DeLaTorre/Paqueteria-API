using System.Linq.Expressions;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Dominio.ValueObjects;

namespace Paqueteria.Application.Comun.Mapeador;

public static class DireccionMapeador
{
    extension(Direccion direccion)
    {
        public DireccionRespuestaDto MapeaRespuestaDto()
        {
            return new DireccionRespuestaDto
            (
                Calle : direccion.Calle,
                NumeroExterior : direccion.NumeroExterior,
                NumeroInterior : direccion.NumeroInterior,
                Colonia : direccion.Colonia,
                CodigoPostal : direccion.CodigoPostal,
                Localidad : direccion.Localidad,
                MunicipioId : direccion.MunicipioId,
                MunicipioNombre : direccion.Municipio.Nombre,
                Estado : direccion.Municipio.EstadoId
            );
        }
    }

    extension(DireccionDto direccion)
    {
        public Direccion MapeaEntidad()
        {
            return Direccion.Crear
            (
                direccion.Calle,
                direccion.NumeroExterior,
                direccion.NumeroInterior,
                direccion.Colonia,
                direccion.CodigoPostal,
                direccion.Localidad,
                direccion.MunicipioId
            );
        }
    }

    // Expresión de Proyección para LINQ / EF Core (Reemplazo de FromEntity en Consultas)
    public static Expression<Func<Direccion, DireccionRespuestaDto>> ProyeccionRespuesta => entidad => new DireccionRespuestaDto(
        entidad.Calle,
        entidad.NumeroExterior,
        entidad.NumeroInterior,
        entidad.Colonia,
        entidad.CodigoPostal,
        entidad.Localidad,
        entidad.MunicipioId,
        entidad.Municipio != null ? entidad.Municipio.Nombre : string.Empty,
        entidad.Municipio != null ? entidad.Municipio.EstadoId.ToString() : string.Empty
    );
}