using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Choferes.Dtos;

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
    [property: Required] Guid ChoferId,
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
    [property: Required] Guid ChoferId,
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