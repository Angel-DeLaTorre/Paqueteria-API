using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class ArticuloGuiaConfiguracion : IEntityTypeConfiguration<ArticuloGuia>
{
    public void Configure(EntityTypeBuilder<ArticuloGuia> builder)
    {
        builder.ToTable("articulos_guia");
        
        builder.HasKey(x => x.Id);

        builder.Property(ag => ag.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(ag => ag.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(250)
            .IsRequired();
        
        builder.Property(ag => ag.Cantidad)
            .HasColumnName("cantidad")
            .HasPrecision(10)
            .IsRequired();
        
        builder.Property(ag => ag.PesoUnitarioKg)
            .HasColumnName("peso_unidad")
            .HasPrecision(10, 2)
            .IsRequired();
        
        builder.Property(ag => ag.ValorUnidad)
            .HasColumnName("valor_unidad")
            .HasPrecision(10, 2)
            .IsRequired();
        
        builder.Property(ag => ag.ArticuloId)
            .HasColumnName("articulo_id")
            .HasColumnType("uuid");
        
        builder.Property(ag => ag.GuiaId)
            .HasColumnName("guia_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasIndex(ag => ag.ArticuloId);
        builder.HasIndex(ag => ag.GuiaId);
        
        builder.HasOne(ag => ag.Articulo)
            .WithMany()
            .HasForeignKey(ag => ag.ArticuloId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ag => ag.Guia)
            .WithMany(g => g.ArticulosGuia)
            .HasForeignKey(ag => ag.GuiaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}