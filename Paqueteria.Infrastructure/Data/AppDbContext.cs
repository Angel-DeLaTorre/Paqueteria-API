using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sat;

namespace Paqueteria.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Core: Seguridad
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<BitacoraSistema> BitacorasSistema { get; set; }
    public DbSet<BitacoraAcceso> BitacorasAcceso { get; set; }

    // Core: Organización
    public DbSet<Estado> Estados { get; set; }
    public DbSet<Municipio> Municipios { get; set; }
    public DbSet<Sucursal> Sucursales { get; set; }
    public DbSet<Ruta> Rutas { get; set; }

    // Core: Operación
    public DbSet<Chofer> Choferes { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Seguro> Seguros { get; set; }
    public DbSet<Articulo> ArticulosCatalogo { get; set; }

    // Core: Guías y Envíos
    public DbSet<Guia> Guias { get; set; }
    public DbSet<ArticuloGuia> ArticulosGuia { get; set; }
    public DbSet<DireccionGuiaSnapshot> DireccionesGuia { get; set; }
    public DbSet<Asignacion> Asignaciones { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Habilitar extensión para generar UUIDs si la BD no la tiene
        modelBuilder.HasPostgresExtension("uuid-ossp");

        // Configuración de JSONB
        modelBuilder.Entity<BitacoraSistema>(entity =>
        {
            entity.Property(b => b.ValorAnterior).HasColumnType("jsonb");
            entity.Property(b => b.ValorNuevo).HasColumnType("jsonb");
        });

        // Conversión de Enums a String
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Estatus)
            .HasConversion<string>();

        modelBuilder.Entity<Usuario>()
            .Property(u => u.Rol)
            .HasConversion<string>();

        modelBuilder.Entity<Chofer>()
            .Property(c => c.Estatus)
            .HasConversion<string>();

        modelBuilder.Entity<Guia>()
            .Property(g => g.FormaPago)
            .HasConversion<string>();

        // Configuración de Decimales (Dinero)
        modelBuilder.Entity<Guia>()
            .Property(g => g.CostoFlete).HasPrecision(15, 2);

        // ... (puedes repetir para los demás decimales si deseas ser estricto)


        // IMPORTANTE: Evitar borrados en cascada accidentales.
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
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