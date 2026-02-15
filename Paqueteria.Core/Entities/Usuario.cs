using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("usuarios")]
public class Usuario
{
    [Key]
    [Column("id_usuario")]
    public Guid IdUsuario { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("usuario")]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [Column("password")]
    public string Password { get; set; } = string.Empty;

    [Column("rol")]
    public RolUsuario Rol { get; set; } = RolUsuario.Reportes;

    [Column("estatus")]
    public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [Column("fecha_ultimo_acceso")]
    public DateTime? FechaUltimoAcceso { get; set; }

    // colecciones
    public virtual ICollection<BitacoraSistema> BitacorasSistema { get; set; } = new List<BitacoraSistema>();
    public virtual ICollection<BitacoraAcceso> Accesos { get; set; } = new List<BitacoraAcceso>();
}