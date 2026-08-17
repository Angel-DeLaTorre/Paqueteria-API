using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Infrastructure.Persistence.Extenciones;

public static class OwnedTypeExtensions 
{
    public static void ConfiguracionDireccion<T>(this OwnedNavigationBuilder<T, Direccion> d) where T : class
    {
        d.Property(p => p.Calle)
            .HasColumnName("calle")
            .HasMaxLength(255)
            .IsRequired();

        d.Property(p => p.NumeroExterior)
            .HasColumnName("numero_exterior")
            .HasMaxLength(50)
            .IsRequired();

        d.Property(p => p.NumeroInterior)
            .HasColumnName("numero_interior")
            .HasMaxLength(50)
            .IsRequired(false);

        d.Property(p => p.Colonia)
            .HasColumnName("colonia")
            .HasMaxLength(100)
            .IsRequired();

        d.Property(p => p.CodigoPostal)
            .HasColumnName("codigo_postal")
            .HasMaxLength(6)
            .IsRequired();

        d.Property(p => p.Localidad)
            .HasColumnName("localidad")
            .HasMaxLength(100)
            .IsRequired(false);

        d.Property(p => p.MunicipioId)
            .HasColumnName("municipio_id")
            .IsRequired();
        
        d.HasOne(p => p.Municipio)
            .WithMany()
            .HasForeignKey(p => p.MunicipioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}