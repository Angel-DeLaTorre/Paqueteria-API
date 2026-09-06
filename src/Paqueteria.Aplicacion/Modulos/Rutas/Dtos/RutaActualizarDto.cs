using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Rutas.Dtos;

public record RutaActualizarDto
(
    Guid RutaId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    string? Descripcion
);