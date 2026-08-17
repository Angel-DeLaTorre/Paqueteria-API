using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Infrastructure.Data.Configurations.Remisiones;

public class GuiaConfiguracion : IEntityTypeConfiguration<Guia>
{
    public void Configure(EntityTypeBuilder<Guia> builder)
    {
        builder.ToTable("guias");

        builder.HasKey(g => g.Id);
        
        builder.Property(g => g.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(g => g.Clave)
            .HasColumnName("clave")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(g => g.Estatus)
            .HasColumnName("estatus")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(g => g.FormaPago)
            .HasColumnName("forma_pago")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(g => g.FechaCaptura)
            .HasColumnName("fecha_captura")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(g => g.FechaEnvio)
            .HasColumnName("fecha_envio")
            .HasColumnType("timestamp with time zone")
            .IsRequired(false);

        builder.Property(g => g.FechaPago)
            .HasColumnName("fecha_pago")
            .HasColumnType("timestamp with time zone")
            .IsRequired(false);
        
        // IDs de Relaciones
        builder.Property(g => g.ClienteOrigenId).HasColumnName("cliente_origen_id").IsRequired();
        builder.Property(g => g.DireccionOrigenId).HasColumnName("direccion_origen_id").IsRequired();
        builder.Property(g => g.ClienteDestinoId).HasColumnName("cliente_destino_id").IsRequired();
        builder.Property(g => g.DireccionDestinoId).HasColumnName("direccion_destino_id").IsRequired();
        builder.Property(g => g.SucursalOrigenId).HasColumnName("sucursal_origen_id").IsRequired();
        builder.Property(g => g.SucursalDestinoId).HasColumnName("sucursal_destino_id").IsRequired();
        builder.Property(g => g.UsuarioAltaId).HasColumnName("usuario_alta_id").IsRequired();
        builder.Property(g => g.UsuarioCobroId).HasColumnName("usuario_cobro_id").IsRequired(false);
        builder.Property(g => g.SeguroId).HasColumnName("seguro_id").IsRequired(false);
        builder.Property(g => g.AsignacionId).HasColumnName("asignacion_id").IsRequired(false);
        builder.Property(g => g.EmpresaId).HasColumnName("empresa_id").IsRequired();
        
        // Precisión Financiera (18,2 o 15,2 según tu modelo)
        builder.Property(g => g.Flete).HasColumnName("flete").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.Recoleccion).HasColumnName("recoleccion").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.EntregaA).HasColumnName("entrega_a").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.Maniobras).HasColumnName("maniobras").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.Peaje).HasColumnName("peaje").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.Lineas).HasColumnName("lineas").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.CondonaIva).HasColumnName("condona_iva").IsRequired();
        builder.Property(g => g.EstaAsegurado).HasColumnName("esta_asegurado").IsRequired();

        builder.Property(g => g.Iva).HasColumnName("iva").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.IvaRetenido).HasColumnName("ivar").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.Subtotal).HasColumnName("subtotal").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.Total).HasColumnName("total").HasPrecision(15, 2).IsRequired();
        builder.Property(g => g.CobroSeguro).HasColumnName("cobro_seguro").HasPrecision(15, 2).IsRequired();

        // Textos y Varios
        builder.Property(g => g.ImporteTexto).HasColumnName("importe_texto").HasMaxLength(200).IsRequired(false);
        builder.Property(g => g.Observaciones).HasColumnName("observaciones").HasMaxLength(500).IsRequired(false);
        builder.Property(g => g.PolizaSeguro).HasColumnName("poliza_seguro").HasMaxLength(50).IsRequired(false);
        
        // ==========================================
        // RELACIONES (FOREIGN KEYS) - Compilado de Restricciones
        // ==========================================
        
        builder.HasIndex(g => g.DireccionOrigenId);
        builder.HasIndex(g => g.DireccionDestinoId);
        builder.HasIndex(g => g.SucursalOrigenId);
        builder.HasIndex(g => g.SucursalDestinoId);
        builder.HasIndex(g => g.UsuarioAltaId);
        builder.HasIndex(g => g.AsignacionId);
        builder.HasIndex(g => g.EmpresaId);
        
        // Clientes
        builder.HasOne(g => g.ClienteOrigen).WithMany().HasForeignKey(g => g.ClienteOrigenId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(g => g.ClienteDestino).WithMany().HasForeignKey(g => g.ClienteDestinoId).OnDelete(DeleteBehavior.Restrict);

        // Snapshots de Direcciones (Históricas para auditoría fiscal/operativa)
        builder.HasOne(g => g.DireccionOrigen).WithMany().HasForeignKey(g => g.DireccionOrigenId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(g => g.DireccionDestino).WithMany().HasForeignKey(g => g.DireccionDestinoId).OnDelete(DeleteBehavior.Restrict);

        // Sucursales
        builder.HasOne(g => g.SucursalOrigen).WithMany().HasForeignKey(g => g.SucursalOrigenId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(g => g.SucursalDestino).WithMany().HasForeignKey(g => g.SucursalDestinoId).OnDelete(DeleteBehavior.Restrict);

        // Usuarios
        builder.HasOne(g => g.UsuarioAlta).WithMany().HasForeignKey(g => g.UsuarioAltaId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(g => g.UsuarioCobro).WithMany().HasForeignKey(g => g.UsuarioCobroId).OnDelete(DeleteBehavior.Restrict);

        // Entidades complementarias u opcionales
        builder.HasOne(g => g.Asignacion).WithMany().HasForeignKey(g => g.AsignacionId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(g => g.Asignacion)
            .WithMany(a => a.Guias)
            .HasForeignKey(g => g.AsignacionId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);
        
        
        
        builder.HasOne(g => g.Seguro).WithMany().HasForeignKey(g => g.SeguroId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(g => g.Empresa).WithMany().HasForeignKey(g => g.EmpresaId).OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(g => g.ArticulosGuia)
            .WithOne() 
            .HasForeignKey("guia_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}