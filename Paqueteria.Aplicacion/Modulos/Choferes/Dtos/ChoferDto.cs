using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Choferes.Dtos;

public record ChoferCreateDto(
    [property: Required] string Nombre,
    [property: Required] string ApellidoPaterno,
    [property: Required] string ApellidoMaterno,
    [property: Required] DireccionDto Direccion,
    [property: Required] string Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
);

public record ChoferUpdateDto(
    [property: Required] Guid ChoferId,
    [property: Required] string Nombre,
    [property: Required] string ApellidoPaterno,
    [property: Required] string ApellidoMaterno,
    [property: Required] DireccionDto Direccion,
    [property: Required] string Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
);

public record ChoferResponseDto(
    [property: Required] Guid ChoferId,
    [property: Required] string Nombre,
    [property: Required] string ApellidoPaterno,
    string? ApellidoMaterno,
    [property: Required] DireccionResponseDto Direccion,
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