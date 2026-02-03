using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("bitacora_accesos")]
public class BitacoraAcceso
{
    [Key]
    [Column("id_acceso")]
    public Guid IdAcceso { get; set; } = Guid.NewGuid();

    [Column("id_usuario")]
    public Guid? IdUsuario { get; set; }

    [Column("exito")]
    public bool Exito { get; set; }

    [MaxLength(50)]
    [Column("ip_cliente")]
    public string? IpCliente { get; set; }

    [MaxLength(200)]
    [Column("user_agent")]
    public string? UserAgent { get; set; }

    [Column("fecha_acceso")]
    public DateTime FechaAcceso { get; set; } = DateTime.UtcNow;

    // llaves
    [ForeignKey("IdUsuario")]
    public virtual Usuario? Usuario { get; set; }
}