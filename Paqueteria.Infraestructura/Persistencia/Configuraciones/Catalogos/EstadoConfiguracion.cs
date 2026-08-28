using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Core.Entidades.Catalogos;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Catalogos;

public class EstadoConfiguracion : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("estados");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(r => r.Id)
            .HasColumnName("id");
        
        builder.Property(r => r.Nombre).
            HasColumnName("nombre")
            .IsRequired();
        
        builder.Property(r => r.Pais).
            HasColumnName("pais")
            .IsRequired();
        
        builder.Property(r => r.Acronimo).
            HasColumnName("acronimo")
            .IsRequired();
    }
}