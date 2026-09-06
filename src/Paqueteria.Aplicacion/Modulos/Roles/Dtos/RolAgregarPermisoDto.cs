using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Roles.Dtos;

public record RolAgregarPermisoDto
(
    Guid RolId,
    List<Guid> PermisosIds
);