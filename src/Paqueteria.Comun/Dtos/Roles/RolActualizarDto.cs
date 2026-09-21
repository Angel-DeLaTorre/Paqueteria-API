namespace Paqueteria.Comun.Dtos.Roles;

public record RolActualizarDto
(
    Guid RoleId,
    string Nombre,
    string Descripcion
);