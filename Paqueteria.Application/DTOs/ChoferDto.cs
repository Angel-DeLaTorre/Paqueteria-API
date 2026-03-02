using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record ChoferCreateDto(
    string Nombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string Calle,
    string Colonia,
    string NumeroExterior,
    string? NumeroInterior,
    string? Localidad,
    Guid MunicipioId,
    string Telefono,
    int? NumCamion,
    int? NumContenedor,
    int? NumContenedor2
)
{
    public Chofer ToEntity() =>
        new Chofer(
            Nombre,
            ApellidoPaterno,
            ApellidoMaterno,
            Calle,
            Colonia,
            NumeroExterior,
            NumeroInterior,
            Localidad,
            MunicipioId,
            Telefono,
            NumCamion,
            NumContenedor,
            NumContenedor2
        );
};

public record ChoferUpdateDto(
    Guid ChoferId,
    string Nombre,
    string ApellidoPaterno,
    string ApellidoMaterno,
    string Calle,
    string Colonia,
    string NumeroExterior,
    string? NumeroInterior,
    string? Localidad,
    Guid MunicipioId,
    string Telefono,
    int? NumCamion,
    int? NumContenedor,
    int? NumContenedor2
)
{
    public void UpdateEntity(Chofer chofer)
    {
        chofer.Nombre = Nombre;
        chofer.ApellidoPaterno = ApellidoPaterno;
        chofer.ApellidoMaterno = ApellidoMaterno;
        chofer.Calle = Calle;
        chofer.Colonia = Colonia;
        chofer.NumeroExterior = NumeroExterior;
        chofer.NumeroInterior = NumeroInterior;
        chofer.Localidad = Localidad;
        chofer.MunicipioId = MunicipioId;
        chofer.Telefono = Telefono;
        chofer.NumCamion = NumCamion;
        chofer.NumContenedor = NumContenedor;
        chofer.NumContenedor2 = NumContenedor2;
    }
}

public record ChoferResponseDto(
    Guid ChoferId,
    string Nombre,
    string ApellidoPaterno,
    string? ApellidoMaterno,
    string? Calle,
    string? Colonia,
    string? NumeroExterior,
    string? NumeroInterior,
    string? Localidad,
    Guid MunicipioId,
    string MunicipioNombre,
    string? Telefono,
    int? NumCamion,
    int? NumContenedor,
    int? NumContenedor2
)
{
    public static ChoferResponseDto FromEntity(Chofer chofer) =>
        new ChoferResponseDto(
            chofer.ChoferId,
            chofer.Nombre,
            chofer.ApellidoPaterno,
            chofer.ApellidoMaterno,
            chofer.Calle,
            chofer.Colonia,
            chofer.NumeroExterior,
            chofer.NumeroInterior,
            chofer.Localidad,
            chofer.MunicipioId,
            chofer.Municipio.Nombre,
            chofer.Telefono,
            chofer.NumCamion,
            chofer.NumContenedor,
            chofer.NumContenedor2
        );
}