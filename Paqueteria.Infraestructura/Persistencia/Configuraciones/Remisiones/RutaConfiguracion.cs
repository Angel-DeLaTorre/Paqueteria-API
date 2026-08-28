using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class RutaConfiguracion : IEntityTypeConfiguration<Ruta>
{
    public void Configure(EntityTypeBuilder<Ruta> builder)
    {
        builder.ToTable("rutas");

        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(r => r.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(200)
            .IsRequired(false);

        builder.Property(r => r.SucursalOrigenId)
            .HasColumnName("sucursal_origen_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(r => r.SucursalDestinoId)
            .HasColumnName("sucursal_destino_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(r => r.Estatus)
            .HasColumnName("estatus")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(r => r.EmpresaId)
            .HasColumnName("empresa_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.HasIndex(r => r.SucursalOrigenId);
        builder.HasIndex(r => r.SucursalDestinoId);

        builder.HasOne(r => r.SucursalOrigen)
            .WithMany()
            .HasForeignKey(r => r.SucursalOrigenId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.SucursalDestino)
            .WithMany()
            .HasForeignKey(r => r.SucursalDestinoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(r => r.Empresa)
            .WithMany() 
            .HasForeignKey(r => r.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
}