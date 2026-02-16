using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("bitacora_sistema")]
public class BitacoraSistema
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [Column("sucursal_id")]
    public Guid SucursalId { get; set; }

    [Required]
    [Column("usuario_id")]
    public Guid UsuarioId { get; set; }

    [Required]
    [Column("accion")]
    public AccionBitacora Accion { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("tabla")]
    public string Tabla { get; set; } = string.Empty;

    [Column("registro_id")]
    public Guid RegistroId { get; set; }

    [Column("valor_anterior", TypeName = "jsonb")]
    public string? ValorAnterior { get; set; }

    [Column("valor_nuevo", TypeName = "jsonb")]
    public string? ValorNuevo { get; set; }

    [MaxLength(50)]
    [Column("cliente_ip")]
    public string? ClienteIp { get; set; }

    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    //Llaves

    [ForeignKey("UsuarioId")]
    public virtual Usuario Usuario { get; set; } = null!;

    [ForeignKey("SucursalId")]
    public virtual Sucursal Sucursal { get; set; } = null!;
}