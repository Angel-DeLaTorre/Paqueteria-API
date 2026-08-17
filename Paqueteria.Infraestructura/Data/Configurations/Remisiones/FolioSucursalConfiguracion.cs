using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Infrastructure.Data.Configurations.Remisiones;

public class FolioSucursalConfiguracion : IEntityTypeConfiguration<FolioSucursal>
{
    public void Configure(EntityTypeBuilder<FolioSucursal> builder)
    {
        builder.ToTable("folios_sucursales");
        
        builder.HasKey(f => f.SucursalId);

        builder.Property(f => f.SucursalId)
            .HasColumnName("sucursal_id")
            .HasColumnType("uuid");

        builder.Property(f => f.UltimoConsecutivo)
            .HasColumnName("ultimo_consecutivo")
            .IsRequired();

        builder.HasOne(f => f.Sucursal)
            .WithOne()
            .HasForeignKey<FolioSucursal>(f => f.SucursalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}