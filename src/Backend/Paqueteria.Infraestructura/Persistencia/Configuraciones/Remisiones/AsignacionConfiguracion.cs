using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class AsignacionConfiguracion : IEntityTypeConfiguration<Asignacion>
{
    public void Configure(EntityTypeBuilder<Asignacion> builder)
    {
        builder.ToTable("asignaciones");
        
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Clave)
            .HasColumnName("clave")
            .IsRequired();

        builder.Property(a => a.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .HasColumnType("timestamp with time zone")
            .IsRequired();
        
        builder.Property(a => a.FechaPartida)
            .HasColumnName("fecha_partida")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(a => a.SucursalOrigenId)
            .HasColumnName("sucursal_origen_id")
            .HasColumnType("uuid");
        
        builder.Property(a => a.SucursalDestinoId)
            .HasColumnName("sucursal_destino_id")
            .HasColumnType("uuid");
        
        builder.Property(a => a.St1)
            .HasColumnName("st1")
            .HasMaxLength(10);
        
        builder.Property(a => a.St2)
            .HasColumnName("st2")
            .HasMaxLength(10);
        
        builder.Property(a => a.St3)
            .HasColumnName("st3")
            .HasMaxLength(10);
        
        builder.Property(a => a.St4)
            .HasColumnName("st4")
            .HasMaxLength(10);
        
        builder.Property(a => a.ChoferId)
            .HasColumnName("chofer_id")
            .HasColumnType("uuid");
        
        builder.Property(a => a.EmpresaId)
            .HasColumnName("empresa_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.HasIndex(a => a.SucursalOrigenId);
        builder.HasIndex(a => a.SucursalDestinoId);
        builder.HasIndex(a => a.ChoferId);
        builder.HasIndex(a => a.EmpresaId);
        
        builder.HasOne(a => a.SucursalOrigen)
            .WithMany()
            .HasForeignKey(a => a.SucursalOrigenId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(a => a.SucursalDestino)
            .WithMany()
            .HasForeignKey(a => a.SucursalDestinoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(a => a.Chofer)
            .WithMany()
            .HasForeignKey(a => a.ChoferId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}