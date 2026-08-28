using System.Linq.Expressions;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Application.Dtos;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Application.Comun.Mapeador;

public static class DireccionMapeador
{
    public static Direccion ToEntity(this DireccionDto dto)
    {
        return Direccion.Create(
            dto.Calle,
            dto.NumeroExterior,
            dto.NumeroInterior,
            dto.Colonia,
            dto.CodigoPostal,
            dto.Localidad,
            dto.MunicipioId
        );
    }

    // Expresión de Proyección para LINQ / EF Core (Reemplazo de FromEntity en Consultas)
    public static Expression<Func<Direccion, DireccionResponseDto>> ProyeccionRespuesta => entidad => new DireccionResponseDto(
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
    
    public static DireccionRespuestaDto ARespuestaDto(this Direccion? entidad)
    {
        if (entidad is null) return null!;

        return new DireccionRespuestaDto(
            entidad.Calle,
            entidad.NumeroExterior,
            entidad.NumeroInterior,
            entidad.Colonia,
            entidad.CodigoPostal,
            entidad.Localidad,
            entidad.MunicipioId,
            entidad.Municipio?.Nombre,
            entidad.Municipio?.EstadoId
        );
    }
}