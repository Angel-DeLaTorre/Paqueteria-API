using Microsoft.EntityFrameworkCore.Storage;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Persistence;

public class UnitOfWork(
    AppDbContext context,
    IArticuloRepository articuloRepository,
    IAsignacionRepository asignacionRepository,
    IBitacoraAccesoRepository bitacoraAccesoRepository,
    IBitacoraSistemaRepository bitacoraSistemaRepository,
    IChoferRepository choferRepository,
    IClienteRepository clienteRepository,
    IEmpresaRepository empresaRepository,
    IEstadoRepository estadoRepository,
    IGuiaRepository guiaRepository,
    IMunicipioRepository municipioRepository,
    IPermisoRepository permissionRepository,
    IRolRepository roleRepository,
    IRutaRepository rutaRepository,
    ISeguroRepository seguroRepository,
    ISucursalRepository sucursalRepository,
    IUsuarioRepository userRepository
    
) : IUnitOfWork
{
    private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private IDbContextTransaction? _currentTransaction;
    private bool _disposed = false;

    public IArticuloRepository Articulos { get; } = articuloRepository;
    public IAsignacionRepository Asignaciones { get; } = asignacionRepository;
    public IBitacoraAccesoRepository BitacoraAccesos { get; } = bitacoraAccesoRepository;
    public IBitacoraSistemaRepository BitacoraSistema { get; } = bitacoraSistemaRepository;
    public IChoferRepository Choferes { get; } = choferRepository;
    public IClienteRepository Clientes { get; } = clienteRepository;
    public IEmpresaRepository Empresas { get; } = empresaRepository;
    public IEstadoRepository Estados { get; } = estadoRepository;
    public IGuiaRepository Guias { get; } = guiaRepository;
    public IMunicipioRepository Municipios { get; } = municipioRepository;
    public IPermisoRepository Permisos { get; } = permissionRepository;
    public IRolRepository Roles { get; } = roleRepository;
    public IUsuarioRepository Usuarios { get; } = userRepository;
    public IRutaRepository Rutas  { get; } = rutaRepository;
    public ISeguroRepository Seguros  { get; } = seguroRepository;
    public ISucursalRepository Sucursales { get; } = sucursalRepository;
    
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }
    
    public async Task BeginTransactionAsync()
    {
        if (_currentTransaction != null) return;
        
        _currentTransaction = await _context.Database.BeginTransactionAsync();
    }
    
    public async Task CommitTransactionAsync()
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
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            DisposeTransaction();
        }
    }
    
    public async Task RollbackTransactionAsync()
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