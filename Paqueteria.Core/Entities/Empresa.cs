using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("empresas")]
public class Empresa
{
    [Key]
    [Column("id_empresa")]
    public Guid IdEmpresa { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(50)]
    [Column("nombre_corto")]
    public string? NombreCorto { get; set; }

    [Required]
    [MaxLength(20)]
    public string Rfc { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Calle { get; set; }

    [MaxLength(6)]
    [Column("codigo_postal")]
    public string? CodigoPostal { get; set; }

    [MaxLength(100)]
    public string? Ciudad { get; set; }

    [MaxLength(100)]
    public string? Estado { get; set; }

    [Column("fecha_alta")]
    public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
}