using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class SeguroConfiguracion : IEntityTypeConfiguration<Seguro>
{
    public void Configure(EntityTypeBuilder<Seguro> builder)
    {
        builder.ToTable("seguros");

        builder.HasKey(dc => dc.Id);
        
        builder.Property(dc => dc.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();
        
        builder.Property(dc => dc.Nombre)
            .HasColumnName("nombre")
            .IsRequired();
        
        builder.Property(dc => dc.Estatus)
            .HasColumnName("estatus")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(c => c.EmpresaId)
            .HasColumnName("empresa_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasIndex(p => p.EmpresaId);

        builder.HasOne(c => c.Empresa)
            .WithMany()
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}