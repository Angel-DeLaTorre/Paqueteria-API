namespace Paqueteria.Application.Modulos.Usuarios.Dtos;

public record UsuarioCrearDto
(
    string Nombre,
    string Username,
    string Password,
    List<Guid> Roles
);