using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Roles.Dtos;

public record RolCrearDto(
    string Nombre,
    string Descripcion,
    List<Guid>? PermisosIds
);