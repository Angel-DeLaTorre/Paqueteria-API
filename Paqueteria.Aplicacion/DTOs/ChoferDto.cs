using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Enums;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Application.DTOs;

public record ChoferCreateDto(
    string Nombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    DireccionDto Direccion,
    string Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
);

public record ChoferUpdateDto(
    Guid ChoferId,
    string Nombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    DireccionDto Direccion,
    string Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
);

public record ChoferResponseDto(
    Guid ChoferId,
    string Nombre,
    string ApellidoPaterno,
    string? ApellidoMaterno,
    DireccionResponseDto Direccion,
    string? Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
)
{
    public static ChoferResponseDto FromEntity(Chofer chofer) =>
        new ChoferResponseDto(
            chofer.Id,
            chofer.Nombre,
            chofer.ApellidoPaterno,
            chofer.ApellidoMaterno,
            DireccionResponseDto.FromEntity(chofer.Direccion),
            chofer.Telefono,
            chofer.NumCamion,
            chofer.NumContenedor,
            chofer.NumContenedor2
        );
}