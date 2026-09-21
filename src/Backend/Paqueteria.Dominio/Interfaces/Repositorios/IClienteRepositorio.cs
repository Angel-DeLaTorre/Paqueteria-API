using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IClienteRepositorio
{
    Task<Cliente?> ObtenerPorIdAsync(Guid clienteId, Guid empresaId, bool asTraking = true);
    Task<DireccionCliente?> ObtenerDireccionPorIdAsync(Guid clienteId, Guid direccionId, bool asTracking = true);
    Task<IReadOnlyList<Cliente>> ObtenerTodosAsync(Guid empresaId);
    Task<bool> ExisteAsync(Guid clienteId, CancellationToken cancellationToken = default);
    Task<Cliente> AgregarAsync(Cliente entity);
    void Eliminar(Cliente entity);
    
    Task AgregarDireccionAsync(DireccionCliente dir);
}