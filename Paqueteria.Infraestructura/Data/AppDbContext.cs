using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sat;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<BitacoraSistema> BitacorasSistema { get; set; }
    public DbSet<BitacoraAcceso> BitacorasAcceso { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<Rol> Roles { get; set; }
    public DbSet<RolPermiso> RolPermiso { get; set; }
    public DbSet<UsuarioRol> UsuarioRol { get; set; }
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<Sucursal> Sucursales { get; set; }
    public DbSet<FolioSucursal> FoliosSucursal { get; set; }
    public DbSet<Ruta> Rutas { get; set; }
    public DbSet<Chofer> Choferes { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<DireccionCliente> DireccionClientes { get; set; }
    public DbSet<Seguro> Seguros { get; set; }
    public DbSet<Articulo> Articulos { get; set; }
    public DbSet<Guia> Guias { get; set; }
    public DbSet<GuiaTransbordo> GuiaTransbordos { get; set; }
    public DbSet<DireccionGuiaSnapshot> DireccionGuiaSnapshots { get; set; }
    public DbSet<ArticuloGuia> ArticulosGuia { get; set; }
    public DbSet<DireccionGuiaSnapshot> DireccionesGuia { get; set; }
    public DbSet<Asignacion> Asignaciones { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Habilitar extensión para generar UUIDs si la BD no la tiene
        modelBuilder.HasPostgresExtension("uuid-ossp");

        // IMPORTANTE: Evitar borrados en cascada accidentales.
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            if (relationship.DeclaringEntityType.ClrType == typeof(UsuarioRol) || 
                relationship.DeclaringEntityType.ClrType == typeof(RolPermiso))
            {
                continue;
            }
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Convierte todos los Enums a int automáticamente en toda la solución
        configurationBuilder
            .Properties<Enum>()
            .HaveConversion<int>();
    }
}