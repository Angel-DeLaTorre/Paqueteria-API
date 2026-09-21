using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Sistema;

public class BitacoraSistemaConfiguracion : IEntityTypeConfiguration<BitacoraSistema>
{
    public void Configure(EntityTypeBuilder<BitacoraSistema> builder)
    {
        builder.ToTable("bitacora_sistema");
        
        builder.Property(b => b.ValorAnterior)
            .HasColumnType("jsonb");

        builder.Property(b => b.ValorNuevo)
            .HasColumnType("jsonb");
    }
}