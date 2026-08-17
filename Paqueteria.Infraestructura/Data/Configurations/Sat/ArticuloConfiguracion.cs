using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Core.Entities.Sat;

namespace Paqueteria.Infrastructure.Data.Configurations.Sat;

public class ArticuloConfiguracion : IEntityTypeConfiguration<Articulo>
{
    public void Configure(EntityTypeBuilder<Articulo> builder)
    {
        builder.ToTable("articulos");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();
        
        builder.Property(a => a.ClaveSat)
            .HasColumnName("clave")
            .HasMaxLength(50);
        
        builder.Property(x => x.Texto)
            .HasColumnName("texto")
            .IsRequired();
        
        builder.Property(a => a.Similares)
            .HasColumnName("similares")
            .IsRequired();
        
        builder.Property(a => a.MaterialPeligroso)
            .HasColumnName("material_peligroso");
        
        builder.Property(a => a.VigenciaDesde)
            .HasColumnName("vigencia_desde");
        
        builder.Property(a => a.VigenciaHasta)
            .HasColumnName("vigencia_hasta");
        
        builder.HasIndex(a => a.ClaveSat);
    }
}