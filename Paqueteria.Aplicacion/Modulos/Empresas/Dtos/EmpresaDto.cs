using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Empresas.Dtos;

public record EmpresaCreateDto(
    string Nombre,
    string NombreCorto,
    string Rfc,
    DireccionDto Direccion
)
{
    public Empresa ToEntity()
    {
        var direccion = Direccion.ToEntity();
        return Empresa.Create(Nombre, NombreCorto, Rfc, direccion);
    }
}

public record EmpresaUpdateDto(
    [property: Required] Guid EmpresaId,
    string Nombre,
    string? NombreCorto,
    string Rfc,
    DireccionDto Direccion
)
{
    public void UpdateEntity(Empresa empresa)
    {
        empresa.Nombre = Nombre;
        empresa.NombreCorto = NombreCorto;
        empresa.Rfc = Rfc;
        empresa.Direccion = Direccion.ToEntity();
    }
}

public record EmpresaResponseDto(
    [property: Required] Guid EmpresaId,
    string Nombre,
    string? NombreCorto,
    string Rfc,
    DireccionResponseDto Direccion,
    DateTime FechaAlta
)
{
    public static EmpresaResponseDto FromEntity(Empresa empresa) =>
        new
        (
            empresa.Id,
            empresa.Nombre,
            empresa.NombreCorto,
            empresa.Rfc,
            DireccionResponseDto.FromEntity(empresa.Direccion),
            empresa.FechaAlta
        );
}