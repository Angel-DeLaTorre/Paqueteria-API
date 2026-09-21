using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Comun.Dtos.Choferes;

public record ChoferRespuestaDto
(
    Guid ChoferId,
    string Nombre,
    string ApellidoPaterno,
    string? ApellidoMaterno,
    DireccionRespuestaDto? Direccion,
    string? Telefono,
    string? NumCamion,
    string? NumContenedor,
    string? NumContenedor2
);