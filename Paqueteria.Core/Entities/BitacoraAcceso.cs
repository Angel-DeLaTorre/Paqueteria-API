using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("bitacora_accesos")]
public class BitacoraAcceso
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("usuario_id")]
    public Guid UsiarioId { get; set; }

    [Required]
    [Column("exito")]
    public bool Exito { get; set; }

    [MaxLength(50)]
    [Column("cliente_ip")]
    public string? ClienteIp { get; set; }

    [Required]
    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    // llaves
    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; } = null!;
}