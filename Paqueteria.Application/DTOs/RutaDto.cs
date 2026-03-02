using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record RutaCreateDto(
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    string? Descripcion,
    int? NumCamion,
    int? NumContenedor,
    int? NumContenedor2
)
{
    public Ruta ToEntity() =>
        new Ruta
        {
            SucursalOrigenId = SucursalOrigenId,
            SucursalDestinoId = SucursalDestinoId,
            Descripcion = Descripcion,
            NumCamion = NumCamion,
            NumContenedor = NumContenedor,
            NumContenedor2 = NumContenedor2
        };
};

public record RutaUpdateDto(
    Guid RutaId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    string? Descripcion,
    int? NumCamion,
    int? NumContenedor,
    int? NumContenedor2
)
{
    public void UpdateEntity(Ruta entity)
    {
        entity.SucursalOrigenId = SucursalOrigenId;
        entity.SucursalDestinoId = SucursalDestinoId;
        entity.Descripcion = Descripcion;
        entity.NumCamion = NumCamion;
        entity.NumContenedor = NumContenedor;
        entity.NumContenedor2 = NumContenedor2;
    }
}

public record RutaResponseDto(
    Guid RutaId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    string? Descripcion,
    int? NumCamion,
    int? NumContenedor,
    int? NumContenedor2
)
{
    public static RutaResponseDto FromEntity(Ruta entity) =>
        new RutaResponseDto(
            entity.Id,
            entity.SucursalOrigenId,
            entity.SucursalDestinoId,
            entity.Descripcion,
            entity.NumCamion,
            entity.NumContenedor,
            entity.NumContenedor2
        );
}