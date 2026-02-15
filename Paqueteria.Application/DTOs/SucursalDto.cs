using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record SucursalCreateDto(
    string Nombre,
    string Codigo,
    bool EsMatriz,
    string Calle,
    string Colonia,
    string NumeroExterior,
    string? NumeroInterior,
    string? Localidad,
    Guid MunicipioId,
    string Telefono
);

public record SucursaUpdateDto(
    Guid SucursalId,
    string Nombre,
    string Codigo,
    bool EsMatriz,
    string Calle,
    string Colonia,
    string NumeroExterior,
    string NumeroInterior,
    string Localidad,
    Guid MunicipioId,
    string Telefono
    );

public record SucursalResponseDto
(
    Guid SucursalId,
    string Codigo,
    string Calle,
    string Colonia,
    string NumeroExterior,
    string? NumeroInterior,
    string? Localidad,
    string Municipio,
    string? Telefono,
    EstatusGenerico  Estatus
);