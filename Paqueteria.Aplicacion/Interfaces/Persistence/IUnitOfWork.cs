using Paqueteria.Core.Interfaces.Repositories;

namespace Paqueteria.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IArticuloRepository Articulos { get; }
    IAsignacionRepository Asignaciones { get; }
    IChoferRepository Choferes { get; }
    IClienteRepository Clientes { get; }
    IEmpresaRepository Empresas { get; }
    IEstadoRepository Estados { get; }
    IFolioSucursalRepository FoliosSucursal { get; }
    IGuiaRepository Guias { get; }
    IGuiaTransbordoRepositorio GuiaTransbordos { get; }
    IMunicipioRepository Municipios { get; }
    IPermisoRepository Permisos { get; }
    IRolRepository Roles { get; }
    IRutaRepository Rutas { get; }
    ISeguroRepository Seguros { get; }
    ISucursalRepository Sucursales { get; }
    IUsuarioRepository Usuarios { get; }
    
    
    Task<int> CompletarAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}