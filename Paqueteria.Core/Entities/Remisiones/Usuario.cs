using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("usuarios", Schema = Constantes.Esquemas.Remisiones)]
public class Usuario
{
    
    #region Columns
    
        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("usuario")]
        public string Username { get; init; }  = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("password")]
        public string Password { get; set; }  = string.Empty;

        [Column("rol")]
        public RolUsuario Rol { get; set; } 

        [Required] [Column("estatus")] 
        public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

        [Column("fecha_creacion")]
        public DateTime FechaCreacion { get; init; } = DateTime.UtcNow;

        [Column("fecha_ultimo_acceso")]
        public DateTime? FechaUltimoAcceso { get; set; }
        
        [Column("empresa_id")]
        public Guid EmpresaId { get; init; }
        
    #endregion
    
    #region ForeignKey
    
        [ForeignKey("EmpresaId")] public Empresa Empresa { get; init; } = null!;
    
    #endregion

    #region Constructor
    
        private Usuario() {}
    
        public static Usuario Create(string nombre, string username, string password, RolUsuario rol, Guid empresaId)
        {
            //TODO Valida campos
            
            return new Usuario()
            {
                Nombre = nombre,
                Username = username,
                Password = password,
                Rol = rol,
                Estatus = EstatusGenerico.Activo,
                FechaCreacion =  DateTime.UtcNow,
                FechaUltimoAcceso = null,
                EmpresaId = empresaId
            };
        }
    
    #endregion

}