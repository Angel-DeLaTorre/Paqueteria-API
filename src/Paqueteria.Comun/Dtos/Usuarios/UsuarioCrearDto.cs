namespace Paqueteria.Comun.Dtos.Usuarios;

public record UsuarioCrearDto
(
    string Nombre,
    string Username,
    string Password,
    List<Guid> Roles
);