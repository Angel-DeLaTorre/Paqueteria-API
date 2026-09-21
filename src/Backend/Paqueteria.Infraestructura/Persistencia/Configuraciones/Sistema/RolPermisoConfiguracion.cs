using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Sistema;

public class RolPermisoConfiguracion : IEntityTypeConfiguration<RolPermiso>
{
    public void Configure(EntityTypeBuilder<RolPermiso> builder)
    {
        builder.ToTable("roles_permisos");
        
        builder.HasKey(ur => new { ur.RolId, ur.PermisoId });

        builder.Property(ur => ur.PermisoId)
            .HasColumnName("permiso_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.Property(ur => ur.RolId)
            .HasColumnName("rol_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasOne(rp => rp.Permiso)
            .WithMany(p => p.RolPermiso)
            .HasForeignKey(rp => rp.PermisoId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(rp => rp.Rol)
            .WithMany(r => r.Permisos)
            .HasForeignKey(rp => rp.RolId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}