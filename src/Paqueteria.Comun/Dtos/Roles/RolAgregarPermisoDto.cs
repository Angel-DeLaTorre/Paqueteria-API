namespace Paqueteria.Comun.Dtos.Roles;

public record RolAgregarPermisoDto
(
    Guid RolId,
    List<Guid> PermisosIds
);