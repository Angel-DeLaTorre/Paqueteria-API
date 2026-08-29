using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Choferes.Dtos;

public record ChoferCreateDto(
    [param: Required] string Nombre,
    [param: Required] string ApellidoPaterno,
    [param: Required] string ApellidoMaterno,
    [param: Required] DireccionDto Direccion,
    [param: Required] string Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
);

public record ChoferUpdateDto(
    [param: Required] Guid ChoferId,
    [param: Required] string Nombre,
    [param: Required] string ApellidoPaterno,
    [param: Required] string ApellidoMaterno,
    [param: Required] DireccionDto Direccion,
    [param: Required] string Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
);

public record ChoferResponseDto(
    [param: Required] Guid ChoferId,
    [param: Required] string Nombre,
    [param: Required] string ApellidoPaterno,
    string? ApellidoMaterno,
    [param: Required] DireccionResponseDto Direccion,
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