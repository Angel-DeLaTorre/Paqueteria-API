namespace Paqueteria.Application.DTOs;

public record ClieteDto(
    Guid Id,
    string Nombre,
    string RFC,
    string Ciudad,
    string Telefono
);