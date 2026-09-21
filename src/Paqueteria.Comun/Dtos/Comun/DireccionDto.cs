namespace Paqueteria.Comun.Dtos.Comun;

public record DireccionDto(
    string Calle,
    string NumeroExterior,
    string? NumeroInterior,
    string Colonia,
    string CodigoPostal,
    string? Localidad,
    Guid MunicipioId
) ;