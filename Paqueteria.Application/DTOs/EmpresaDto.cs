using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record EmpresaCreateDto(
    string Nombre,
    string? NombreCorto,
    string Rfc,
    string Calle,
    string CodigoPostal,
    Guid MunicipioId
)
{
    public Empresa ToEntity() => new Empresa
    {
        Nombre = Nombre,
        NombreCorto = NombreCorto,
        Rfc = Rfc,
        Calle = Calle,
        CodigoPostal = CodigoPostal,
        MunicipioId = MunicipioId,
    };
}

public record EmpresaUpdateDto(
    Guid EmpresaId,
    string Nombre,
    string? NombreCorto,
    string Rfc,
    string Calle,
    string CodigoPostal,
    Guid MunicipioId
)
{
    public void UpdateEntity(Empresa empresa)
    {
        empresa.Nombre = Nombre;
        empresa.NombreCorto = NombreCorto;
        empresa.Rfc = Rfc;
        empresa.Calle = Calle;
        empresa.CodigoPostal = CodigoPostal;
        empresa.MunicipioId = MunicipioId;
    }
}

public record EmpresaResponseDto(
    Guid EmpresaId,
    string Nombre,
    string? NombreCorto,
    string Rfc,
    string Calle,
    string CodigoPostal,
    Guid MunicipioId,
    DateTime FechaAlta
)
{
    public EmpresaResponseDto FromEntity(Empresa empresa) =>
        new
        (
            empresa.Id,
            empresa.Nombre,
            empresa.NombreCorto,
            empresa.Rfc,
            empresa.Calle,
            empresa.CodigoPostal,
            empresa.MunicipioId,
            empresa.FechaAlta
        );
}