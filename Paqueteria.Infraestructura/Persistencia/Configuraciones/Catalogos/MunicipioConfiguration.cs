using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Core.Entidades.Catalogos;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Catalogos;

public class MunicipioConfiguration : IEntityTypeConfiguration<Municipio>
{
    public void Configure(EntityTypeBuilder<Municipio> builder)
    {
        builder.ToTable("municipios");
        
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("id");

        builder.Property(m => m.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(70)
            .IsRequired();
        
        builder.Property(m => m.SatId)
            .HasColumnName("sat_id")
            .IsRequired();
        
        builder.Property(m => m.Nombre)
            .HasColumnName("nombre")
            .IsRequired();
        
        builder.Property(m => m.EstadoId)
            .HasColumnName("estado_id")
            .IsRequired();
        
        builder.HasIndex(m => m.EstadoId);
        
        builder.HasOne(m => m.Estado )
            .WithMany()
            .HasForeignKey(m => m.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}