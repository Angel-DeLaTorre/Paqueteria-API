using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Infrastructure.Persistencia.Extenciones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class DireccionClienteConfiguracion : IEntityTypeConfiguration<DireccionCliente>
{
    public void Configure(EntityTypeBuilder<DireccionCliente> builder)
    {
        builder.ToTable("direcciones_clientes");

        builder.HasKey(dc => dc.Id);
        
        builder.Property(dc => dc.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(dc => dc.Estatus)
            .HasColumnName("estatus")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(dc => dc.ClienteId)
            .HasColumnName("cliente_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.OwnsOne(dc => dc.Direccion, d => d.ConfiguracionDireccion());
        
        builder.HasIndex(d => d.ClienteId);
    }
}