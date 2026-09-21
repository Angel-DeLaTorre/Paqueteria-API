namespace Paqueteria.Aplicacion.Comun.Interfaces;

public interface IUsuarioContextoServicio
{
    Guid UsuarioId { get; }
    string Username { get; }
    Guid SucursalId { get; }
    Guid EmpresaId { get; }
    string DireccionIp { get; }
}