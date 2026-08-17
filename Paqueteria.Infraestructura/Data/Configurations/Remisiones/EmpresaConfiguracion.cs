using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Infrastructure.Persistence.Extenciones;

namespace Paqueteria.Infrastructure.Data.Configurations.Remisiones;

public class EmpresaConfiguracion : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("empresas");

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();
        
        builder.Property(e => e.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(e => e.NombreCorto)
            .HasColumnName("nombre_corto")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(e => e.Rfc)
            .HasColumnName("rfc")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(e => e.FechaAlta)
            .HasColumnName("fecha_alta")
            .IsRequired();
        
        builder.OwnsOne(e => e.Direccion, d => d.ConfiguracionDireccion());
        
        builder.HasIndex(e => e.Rfc).IsUnique();
    }
}