using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Sistema;

public partial class Rol
{
    public Guid Id { get; init; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
    public Guid EmpresaId { get; init; }
    
    public Empresa Empresa { get; init; } = null!;
    public ICollection<UsuarioRol> UsuarioRol { get; private set; } = new List<UsuarioRol>();
    public ICollection<RolPermiso> RolPermiso { get; private set; } = new List<RolPermiso>();
    
    #region Constructor
    
        private Rol() {}

        public static Rol Create(string nombre, string descripcion, Guid empresaId)
        {
            return new Rol()
            {
                Nombre = nombre,
                Descripcion = descripcion,
                EmpresaId = empresaId
            };
        }
    
    #endregion
}