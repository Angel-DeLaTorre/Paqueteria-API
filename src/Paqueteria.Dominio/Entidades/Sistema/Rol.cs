using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Dominio.Entidades.Sistema;

public partial class Rol
{
    public Guid Id { get; init; }
    public string Nombre { get; set; } = null!;
    public string Descripcion { get; set; } = null!;
    public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
    public Guid EmpresaId { get; init; }
    
    public Empresa Empresa { get; init; } = null!;
    public ICollection<UsuarioRol> UsuarioRol { get; private set; } = new List<UsuarioRol>();
    public ICollection<RolPermiso> Permisos { get; private set; } = new List<RolPermiso>();
    
    #region Constructor
    
        private Rol() {}

        public static Rol Crear(string nombre, string descripcion, Guid empresaId)
        {
            return new Rol()
            {
                Id = Guid.CreateVersion7(),
                Nombre = nombre,
                Descripcion = descripcion,
                Estatus =  EstatusBasico.Activo,
                EmpresaId = empresaId
            };
        }
    
    #endregion
}