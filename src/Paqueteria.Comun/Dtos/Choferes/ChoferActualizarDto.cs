using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Comun.Dtos.Choferes;

public record ChoferActualizarDto
(
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