using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("bitacora_sistema")]
public class BitacoraSistema
{
    [Key]
    [Column("id_bitacora")]
    public Guid IdBitacora { get; set; } = Guid.NewGuid();

    [Column("id_sucursal")]
    public Guid IdSucursal { get; set; }

    [Column("id_usuario")]
    public Guid? IdUsuario { get; set; }

    [Column("accion")]
    public AccionBitacora Accion { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("tabla_afectada")]
    public string TablaAfectada { get; set; } = string.Empty;

    [Column("id_registro_afectado")]
    public Guid IdRegistroAfectado { get; set; }

    [Column("valores_anteriores", TypeName = "jsonb")]
    public string? ValoresAnteriores { get; set; }

    [Column("valores_nuevos", TypeName = "jsonb")]
    public string? ValoresNuevos { get; set; }

    [MaxLength(50)]
    [Column("ip_cliente")]
    public string? IpCliente { get; set; }

    [MaxLength(200)]
    [Column("user_agent")]
    public string? UserAgent { get; set; }

    [Column("fecha_evento")]
    public DateTime FechaEvento { get; set; } = DateTime.UtcNow;

    //Llaves

    [ForeignKey("IdUsuario")]
    public virtual Usuario? Usuario { get; set; }

    [ForeignKey("IdSucursal")]
    public virtual Sucursal? Sucursal { get; set; } = null!;
}