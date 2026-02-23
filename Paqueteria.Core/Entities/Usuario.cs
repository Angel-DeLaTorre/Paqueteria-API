using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("usuarios")]
public class Usuario
{
    [Key]
    [Column("id")]
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("nombre")]
    public string Nombre { get; set; }

    [Required]
    [MaxLength(50)]
    [Column("usuario")]
    public string Username { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("password")]
    public string Password { get; set; }

    [Column("rol")]
    public RolUsuario Rol { get; set; }

    [Column("estatus")]
    public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

    [Column("fecha_creacion")]
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    [Column("fecha_ultimo_acceso")]
    public DateTime? FechaUltimoAcceso { get; set; }

    public Usuario(string nombre, string username, string password, RolUsuario rol)
    {
        Nombre = nombre;
        Username = username;
        Password = password;
        Rol = rol;
    }

}