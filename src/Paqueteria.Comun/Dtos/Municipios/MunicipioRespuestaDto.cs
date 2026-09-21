namespace Paqueteria.Comun.Dtos.Municipios;

public record MunicipioRespuestaDto(
    Guid MunicipioId,
    string Nombre,
    string EstadoId,
    string? EstadoNombre
);