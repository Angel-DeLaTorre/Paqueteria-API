using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Infrastructure.Persistencia.Configuraciones.Remisiones;

public class ClienteConfiguracion : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes");
        
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasValueGenerator<SequentialGuidValueGenerator>()
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Estatus)
            .HasColumnName("estatus")
            .IsRequired()
            .HasConversion<int>();

        builder.Property(c => c.Rfc)
            .HasColumnName("rfc")
            .HasMaxLength(20)
            .IsRequired(false);
        
        builder.Property(c => c.TipoPersona)
            .HasColumnName("tipo_persona")
            .IsRequired();

        builder.Property(c => c.Telefono)
            .HasColumnName("telefono")
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(c => c.Telefono2)
            .HasColumnName("telefono_2")
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(c => c.Correo)
            .HasColumnName("correo")
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(c => c.Contacto)
            .HasColumnName("contacto")
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(c => c.NumConvenio)
            .HasColumnName("num_convenio")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(c => c.PolizaSeguro)
            .HasColumnName("poliza_seguro")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(c => c.FechaAlta)
            .HasColumnName("fecha_alta")
            .IsRequired();

        builder.Property(c => c.EmpresaId)
            .HasColumnName("empresa_id")
            .HasColumnType("uuid")
            .IsRequired();
        
        builder.HasIndex(e => e.Rfc).IsUnique();
        builder.HasIndex(e => e.EmpresaId);

        builder.HasOne(c => c.Empresa)
            .WithMany() // Ajustar si Empresa tiene un ICollection<Cliente>
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict); // Evitamos borrados accidentales en cascada para catálogos

        builder.HasMany(c => c.Direcciones)
            .WithOne(d => d.Cliente)
            .HasForeignKey(d => d.ClienteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}