using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record AsignacionCreateDto(
    Guid Id,
    Guid ChoferId,
    DateTime FechaAsignacion,
    string? St1,
    string? St2,
    string? St3,
    string? St4,
    int Camion,
    string? NumContenedor,
    string? NumContenedor2
)
{
    public Asignacion ToEntity() =>
        new Asignacion
        {
            Id = Id,
            ChoferId = ChoferId,
            FechaAsignacion = FechaAsignacion,
            St1 = St1,
            St2 = St2,
            St3 = St3,
            St4 = St4,
            Camion = Camion,
            NumContenedor = NumContenedor,
            NumContenedor2 = NumContenedor2
        };
};

public record AsignacionUpdateDto(
    Guid Id,
    DateTime FechaAsignacion,
    string? St1,
    string? St2,
    string? St3,
    string? St4,
    int Camion,
    string? NumContenedor,
    string? NumContenedor2)
{
    public void UpdateEntity(Asignacion entity)
    {
        entity.FechaAsignacion = FechaAsignacion;
        entity.St1 = St1;
        entity.St2 = St2;
        entity.St3 = St3;
        entity.St4 = St4;
        entity.Camion = Camion;
        entity.NumContenedor = NumContenedor;
        entity.NumContenedor2 = NumContenedor2;
    }
};

public record AsignacionResponseDto(
    Guid Id,
    Guid ChoferId,
    DateTime FechaAsignacion,
    string? St1,
    string? St2,
    string? St3,
    string? St4,
    int Camion,
    string? NumContenedor,
    string? NumContenedor2
)
{
    public static AsignacionResponseDto FromEntity(Asignacion entity)
        => new(
            entity.Id,
            entity.ChoferId,
            entity.FechaAsignacion,
            entity.St1,
            entity.St2,
            entity.St3,
            entity.St4,
            entity.Camion,
            entity.NumContenedor,
            entity.NumContenedor2
        );
}