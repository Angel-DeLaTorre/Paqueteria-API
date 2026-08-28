using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class GuiaTransbordoConfiguracion : IEntityTypeConfiguration<GuiaTransbordo>
{
    public void Configure(EntityTypeBuilder<GuiaTransbordo> builder)
    {
        builder.ToTable("guia_transbordos");

        // 2. Clave primaria
        builder.HasKey(gt => gt.Id);
        
        builder.Property(gt => gt.FechaEscaneoIngreso)
            .IsRequired();

        builder.Property(gt => gt.FechaEscaneoSalida)
            .IsRequired(false);

        builder.Property(gt => gt.Observaciones)
            .HasMaxLength(500)
            .IsRequired(false);

        // 4. Relaciones (Foreign Keys)

        // Relación con Guía (Muchas transbordos pertenecen a una Guía)
        builder.HasOne(gt => gt.Guia)
            .WithMany() // O .WithMany(g => g.Transbordos) si agregas ICollection<GuiaTransbordo> en Guia
            .HasForeignKey(gt => gt.GuiaId)
            .OnDelete(DeleteBehavior.Cascade); // Si se elimina la guía, se eliminan sus registros de transbordo

        // Relación con Asignación (Muchos transbordos pertenecen a una Asignación)
        builder.HasOne(gt => gt.Asignacion)
            .WithMany()
            .HasForeignKey(gt => gt.AsignacionId)
            .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada para mantener la integridad

        // Relación con Sucursal de Transbordo
        builder.HasOne(gt => gt.SucursalTransbordo)
            .WithMany()
            .HasForeignKey(gt => gt.SucursalTransbordoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasIndex(gt => gt.GuiaId);
        builder.HasIndex(gt => gt.AsignacionId);
        builder.HasIndex(gt => new { gt.GuiaId, gt.FechaEscaneoIngreso });
    }
}