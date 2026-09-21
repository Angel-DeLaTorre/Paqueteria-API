namespace Paqueteria.Comun.Dtos.Roles;

public record RolCrearDto(
    string Nombre,
    string Descripcion,
    List<Guid>? PermisosIds
);