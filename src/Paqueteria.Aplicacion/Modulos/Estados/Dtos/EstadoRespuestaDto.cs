using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Estados.Dtos;

public record EstadoRespuestaDto(
    string EstadoId,
    string Nombre,
    string Acronimo,
    string Pais
);