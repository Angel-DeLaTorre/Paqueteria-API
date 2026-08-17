using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Infrastructure.Data.Configurations.Sistema;

public class UsuarioRolConfiguracion : IEntityTypeConfiguration<UsuarioRol>
{
    public void Configure(EntityTypeBuilder<UsuarioRol> builder)
    {
        builder.ToTable("usuarios_roles");
        
        builder.HasKey(ur => new { ur.UsuarioId, ur.RolId });
        
        builder.Property(ur => ur.UsuarioId)
            .HasColumnName("usuario_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(ur => ur.RolId)
            .HasColumnName("rol_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasOne(ur => ur.Usuario)
            .WithMany(u => u.UsuarioRoles)
            .HasForeignKey(ur => ur.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(ur => ur.Rol)
            .WithMany(r => r.UsuarioRol)
            .HasForeignKey(ur => ur.RolId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}