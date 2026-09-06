namespace Paqueteria.Application.Comun.Dtos;

public record DireccionRespuestaDto
(
    string? Calle,
    string? NumeroExterior,
    string? NumeroInterior,
    string? Colonia,
    string? CodigoPostal,
    string? Localidad,
    Guid? MunicipioId,
    string? MunicipioNombre,
    string? Estado
);