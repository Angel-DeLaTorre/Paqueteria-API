using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Rutas.Dtos;

public record RutaCrearDto
(
    string Descripcion,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId
);