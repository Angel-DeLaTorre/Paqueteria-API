using Microsoft.EntityFrameworkCore.Storage;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia;

public class UnidadDeTrabajo(
    AppDbContext context,
    IArticuloRepositorio articuloRepositorio,
    IAsignacionRepositorio asignacionRepositorio,
    IBitacoraAccesoRepositorio bitacoraAccesoRepositorio,
    IBitacoraSistemaRepositorio bitacoraSistemaRepositorio,
    IChoferRepositorio choferRepositorio,
    IClienteRepositorio clienteRepositorio,
    IEmpresaRepositorio empresaRepositorio,
    IEstadoRepositorio estadoRepositorio,
    IFolioSucursalRepositorio folioSucursalRepositorio,
    IGuiaRepositorio guiaRepositorio,
    IGuiaTransbordoRepositorio guiaTransbordosRepositorio,
    IMunicipioRepositorio municipioRepositorio,
    IPermisoRepositorio permissionRepositorio,
    IRolRepositorio roleRepositorio,
    IRutaRepositorio rutaRepositorio,
    ISeguroRepositorio seguroRepositorio,
    ISucursalRepositorio sucursalRepositorio,
    IUsuarioRepositorio userRepositorio
    
) : IUnitOfWork
{
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private IDbContextTransaction? _currentTransaction;
    private bool _disposed = false;

    public IArticuloRepositorio Articulos { get; } = articuloRepositorio;
    public IAsignacionRepositorio Asignaciones { get; } = asignacionRepositorio;
    public IBitacoraAccesoRepositorio BitacoraAccesos { get; } = bitacoraAccesoRepositorio;
    public IBitacoraSistemaRepositorio BitacoraSistema { get; } = bitacoraSistemaRepositorio;
    public IChoferRepositorio Choferes { get; } = choferRepositorio;
    public IClienteRepositorio Clientes { get; } = clienteRepositorio;
    public IEmpresaRepositorio Empresas { get; } = empresaRepositorio;
    public IEstadoRepositorio Estados { get; } = estadoRepositorio;
    public IFolioSucursalRepositorio FoliosSucursal { get; } = folioSucursalRepositorio;
    public IGuiaRepositorio Guias { get; } = guiaRepositorio;
    public IGuiaTransbordoRepositorio GuiaTransbordos { get; } = guiaTransbordosRepositorio;
    public IMunicipioRepositorio Municipios { get; } = municipioRepositorio;
    public IPermisoRepositorio Permisos { get; } = permissionRepositorio;
    public IRolRepositorio Roles { get; } = roleRepositorio;
    public IUsuarioRepositorio Usuarios { get; } = userRepositorio;
    public IRutaRepositorio Rutas  { get; } = rutaRepositorio;
    public ISeguroRepositorio Seguros  { get; } = seguroRepositorio;
    public ISucursalRepositorio Sucursales { get; } = sucursalRepositorio;
    
    public async Task<int> GuardarCambiosAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public async Task ComenzarTransaccionAsync()
    {
        if (_currentTransaction != null) return;
        
        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }
    
    public async Task GuardarTransaccionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
            }
        }
        catch
        {
            await ReverzarTransaccionAsync();
            throw;
        }
        finally
        {
            DisposeTransaction();
        }
    }
    
    public async Task ReverzarTransaccionAsync()
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
            }
        }
        finally
        {
            DisposeTransaction();
        }
    }

    private void DisposeTransaction()
    {
        if (_currentTransaction == null) return;
        _currentTransaction.Dispose();
        _currentTransaction = null;
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            DisposeTransaction();
            _context.Dispose();
        }
        _disposed = true;
    }
}