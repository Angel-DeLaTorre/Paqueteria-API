using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Entities.Sistema;

[Table("usuarios_roles", Schema = Constantes.Esquemas.Remisiones)]
public class UsuarioRol
{
    #region Columns

        [Required]
        [Column("usuario_id")]
        public Guid UsuarioId { get; init; }
        
        [Required]
        [Column("rol_id")]
        public Guid RolId { get; init; }

    #endregion
    
    #region Entities
    
        public Usuario Usuario { get; private set; }  = null!;
        public Rol Rol { get; private set; }  = null!;
        
    #endregion
    
    #region Constructors
    
        private UsuarioRol() { }

        public static UsuarioRol Create(Guid usuarioId, Guid rolId)
        {
            return new UsuarioRol()
            {
                UsuarioId = usuarioId,
                RolId = rolId
            };
        }
    
    #endregion
}