namespace Paqueteria.Core.Entities.Sistema;

public partial class UsuarioRol
{
    #region Columns

        public Guid UsuarioId { get; private init; }
        public Guid RolId { get; private init; }

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