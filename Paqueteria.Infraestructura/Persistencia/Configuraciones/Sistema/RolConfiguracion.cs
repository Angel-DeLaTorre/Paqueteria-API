using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Core.Entidades.Sistema;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Sistema;

public class RolConfiguracion : IEntityTypeConfiguration<Rol>
{
    public void Configure(EntityTypeBuilder<Rol> builder)
    {
        builder.ToTable("roles");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();
        
        builder.Property(r => r.Nombre).
            HasColumnName("nombre")
            .IsRequired();
        
        builder.Property(r => r.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(r => r.Estatus)
            .HasColumnName("estatus")
            .IsRequired();
        
        builder.Property(r => r.EmpresaId)
            .HasColumnName("empresa_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasIndex(r => r.EmpresaId);
        
        builder.HasOne(r => r.Empresa)
            .WithMany()
            .HasForeignKey(r => r.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}