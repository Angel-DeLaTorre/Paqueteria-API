using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("roles_permisos", Schema = Constantes.Esquemas.Remisiones)]
public class RolPermiso
{
    #region Columns
    
        [Required]
        [Column("rol_id")]
        public Guid RolId { get; init; }
        
        [Required]
        [Column("permiso_id")]
        public Guid PermisoId { get; init; }

        public Rol Rol { get; private set; } = null!;
        public Permiso Permiso { get; private set; } = null!;
    
    #endregion
    
    #region Constructors
        private RolPermiso() {}

        public static RolPermiso Create(Guid rolId, Guid permisoId)
        {
            return new RolPermiso()
            {
                RolId = rolId,
                PermisoId = permisoId
            };
        }
    
    #endregion
}