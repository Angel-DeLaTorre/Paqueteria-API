namespace Paqueteria.Dominio.Entidades.Sistema;

public partial class RolPermiso
{
    #region Columns
    
        public Guid RolId { get; private init; }
        public Guid PermisoId { get; private init; }

        public Rol Rol { get; private set; } = null!;
        public Permiso Permiso { get; private set; } = null!;
    
    #endregion
    
    #region Constructors
        private RolPermiso() {}

        public static RolPermiso Crear(Guid rolId, Guid permisoId)
        {
            return new RolPermiso()
            {
                RolId = rolId,
                PermisoId = permisoId
            };
        }
    
    #endregion
}