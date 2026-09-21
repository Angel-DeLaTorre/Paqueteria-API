using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class CamionConfiguracion : IEntityTypeConfiguration<Camion>
{
    public void Configure(EntityTypeBuilder<Camion> builder)
    {
        builder.ToTable("camiones");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(c => c.NumCamion)
            .HasColumnName("num_camion");

        builder.Property(c => c.Placa)
            .HasColumnName("placa");
        
        builder.Property(c => c.EmpresaId)
            .HasColumnName("empresa_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasIndex(c => c.Placa).IsUnique();

        builder.HasOne(c => c.Empresa)
            .WithMany()
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}