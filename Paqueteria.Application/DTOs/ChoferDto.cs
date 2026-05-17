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
)
{
    public Chofer ToEntity(Guid empresaId)
    {
        var direccion = Direccion.ToEntity();
        
        return Chofer.Create
        (
            Nombre,
            ApellidoPaterno,
            ApellidoMaterno,
            direccion,
            Telefono,
            NumCamion,
            NumContenedor,
            NumContenedor2,
            empresaId
        );
    }
};

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
)
{
    public void UpdateEntity(Chofer chofer)
    {
        chofer.Nombre = Nombre;
        chofer.ApellidoPaterno = ApellidoPaterno;
        chofer.ApellidoMaterno = ApellidoMaterno;
        chofer.Telefono = Telefono;
        chofer.NumCamion = NumCamion;
        chofer.NumContenedor = NumContenedor;
        chofer.NumContenedor2 = NumContenedor2;
        chofer.Direccion = Direccion.ToEntity();
    }
}

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