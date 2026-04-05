using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Infrastructure.Data.Configurations;

public class ChoferConfiguration : IEntityTypeConfiguration<Chofer>
{
    public void Configure(EntityTypeBuilder<Chofer> builder)
    {
        /*
        builder.ToTable("choferes");
        builder.HasKey(c => c.Id);
        /*
        builder.OwnsOne(c => c.Direccion, d =>
        {
            d.Property(p => p.Calle).HasColumnName("calle");
            d.Property(p => p.NumeroExterior).HasColumnName("numero_exterior");
            d.Property(p => p.NumeroInterior).HasColumnName("numero_interior");
            d.Property(p => p.Colonia).HasColumnName("colonia");
            d.Property(p => p.CodigoPostal).HasColumnName("codigo_postal");
            d.Property(p => p.Localidad).HasColumnName("localidad");
            d.Property(p => p.MunicipioId).HasColumnName("id_municipio");
        });

        builder.HasOne(c => c.Municipio)
            .WithMany()
            .HasForeignKey("municipio_id") // Debe coincidir con el nombre de columna del VO
            .OnDelete(DeleteBehavior.Restrict);

        */
    }
}