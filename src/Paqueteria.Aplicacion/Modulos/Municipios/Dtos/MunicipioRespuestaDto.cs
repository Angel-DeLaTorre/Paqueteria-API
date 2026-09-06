using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Municipios.Dtos;

public record MunicipioRespuestaDto(
    Guid MunicipioId,
    string Nombre,
    string EstadoId,
    string? EstadoNombre
);