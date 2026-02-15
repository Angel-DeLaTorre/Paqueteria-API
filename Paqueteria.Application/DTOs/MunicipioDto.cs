namespace Paqueteria.Application.DTOs;

public record MunicipioDto(
    Guid IdMunicipio,
    string Nombre,
    string Estado
);