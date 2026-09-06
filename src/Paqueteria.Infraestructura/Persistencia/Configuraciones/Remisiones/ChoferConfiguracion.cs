using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Infrastructure.Persistencia.Extenciones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class ChoferConfiguracion : IEntityTypeConfiguration<Chofer>
{
    public void Configure(EntityTypeBuilder<Chofer> builder)
    {
        builder.ToTable("choferes");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();
        
        builder.Property(c => c.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(c => c.ApellidoPaterno)
            .HasColumnName("apellido_paterno")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(c => c.ApellidoMaterno)
            .HasColumnName("apellido_materno")
            .HasMaxLength(100);

        builder.Property(c => c.Estatus)
            .HasColumnName("estatus")
            .IsRequired();

        builder.Property(c => c.Telefono)
            .HasColumnName("telefono");

        builder.Property(c => c.FechaAlta)
            .HasColumnName("fecha_alta")
            .HasColumnType("date");
        
        builder.Property(c => c.FechaBaja)
            .HasColumnName("fecha_baja")
            .HasColumnType("date");

        builder.Property(c => c.CamionId)
            .HasColumnName("camion_id")
            .HasColumnType("uuid");

        builder.Property(c => c.EmpresaId)
            .HasColumnName("empresa_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.OwnsOne(dc => dc.Direccion, d => d.ConfiguracionDireccion());

        builder.HasIndex(c => c.CamionId);
        builder.HasIndex(c => c.EmpresaId);

        builder.HasOne(c => c.Camion)
            .WithMany()
            .HasForeignKey(c => c.CamionId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(c => c.Empresa)
            .WithMany()
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}