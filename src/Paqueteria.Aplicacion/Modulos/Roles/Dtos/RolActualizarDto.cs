using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Roles.Dtos;

public record RolActualizarDto
(
    Guid RoleId,
    string Nombre,
    string Descripcion
);