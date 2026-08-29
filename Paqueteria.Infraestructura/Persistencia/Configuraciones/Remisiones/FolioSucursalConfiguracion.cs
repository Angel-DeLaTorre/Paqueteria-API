using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class FolioSucursalConfiguracion : IEntityTypeConfiguration<FolioSucursal>
{
    public void Configure(EntityTypeBuilder<FolioSucursal> builder)
    {
        builder.ToTable("folios_sucursales");
        
        builder.HasKey(fs => new { fs.SucursalId, fs.Tipo });

        builder.Property(f => f.SucursalId)
            .HasColumnName("sucursal_id")
            .HasColumnType("uuid");
        
        builder.Property(fs => fs.Tipo)
            .HasColumnName("tipo")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(f => f.UltimoConsecutivo)
            .HasColumnName("ultimo_consecutivo")
            .IsRequired();

        builder.HasOne(f => f.Sucursal)
            .WithMany()
            .HasForeignKey(f => f.SucursalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}