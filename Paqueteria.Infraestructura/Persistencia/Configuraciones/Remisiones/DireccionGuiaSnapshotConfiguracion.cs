using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Infrastructure.Persistencia.Extenciones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class DireccionGuiaSnapshotConfiguracion : IEntityTypeConfiguration<DireccionGuiaSnapshot>
{
    public void Configure(EntityTypeBuilder<DireccionGuiaSnapshot> builder)
    {
        builder.ToTable("direcciones_guia_snapshot");

        builder.HasKey(dc => dc.Id);
        
        builder.Property(dc => dc.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();
        
        builder.OwnsOne(dc => dc.Direccion, d => d.ConfiguracionDireccion());
    }
}