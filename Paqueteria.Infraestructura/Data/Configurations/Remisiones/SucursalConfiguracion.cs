using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Infrastructure.Persistence.Extenciones;

namespace Paqueteria.Infrastructure.Data.Configurations.Remisiones;

public class SucursalConfiguracion : IEntityTypeConfiguration<Sucursal>
{
    public void Configure(EntityTypeBuilder<Sucursal> builder)
    {
        builder.ToTable("sucursales");

        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(s => s.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(s => s.Codigo)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(s => s.EsMatriz)
            .HasColumnName("es_matriz")
            .IsRequired();

        builder.Property(s => s.Telefono)
            .HasColumnName("telefono")
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(s => s.Estatus)
            .HasColumnName("estatus")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(s => s.ServidorIp)
            .HasColumnName("servidor_ip")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(s => s.EmpresaId)
            .HasColumnName("empresa_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasIndex(p => p.EmpresaId);

        builder.OwnsOne(s => s.Direccion, d => d.ConfiguracionDireccion());
        
        builder.HasOne(s => s.Empresa)
            .WithMany()
            .HasForeignKey(s => s.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}