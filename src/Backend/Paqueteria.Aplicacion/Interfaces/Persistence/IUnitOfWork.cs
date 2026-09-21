using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Aplicacion.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IArticuloRepositorio Articulos { get; }
    IAsignacionRepositorio Asignaciones { get; }
    IChoferRepositorio Choferes { get; }
    IClienteRepositorio Clientes { get; }
    IEmpresaRepositorio Empresas { get; }
    IEstadoRepositorio Estados { get; }
    IFolioSucursalRepositorio FoliosSucursal { get; }
    IGuiaRepositorio Guias { get; }
    IGuiaTransbordoRepositorio GuiaTransbordos { get; }
    IMunicipioRepositorio Municipios { get; }
    IPermisoRepositorio Permisos { get; }
    IRolRepositorio Roles { get; }
    IRutaRepositorio Rutas { get; }
    ISeguroRepositorio Seguros { get; }
    ISucursalRepositorio Sucursales { get; }
    IUsuarioRepositorio Usuarios { get; }
    
    
    Task<int> GuardarCambiosAsync();
    Task ComenzarTransaccionAsync();
    Task GuardarTransaccionAsync();
    Task ReverzarTransaccionAsync();
}