using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entidades.Sistema;
public partial class Permiso
{
    #region Campos
        public Guid Id { get; init; } 
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
        public Guid EmpresaId { get; init; }
        
        public Empresa Empresa { get; init; } = null!;
        public ICollection<RolPermiso> RolPermiso { get; private set; } = new List<RolPermiso>();
    #endregion
    
    #region Constructor
    
        private Permiso() { }

        public static Permiso Create(string nombre, string descripcion, Guid empresaId)
        {
            return new Permiso()
            {
                Nombre = nombre,
                Descripcion = descripcion,
                EmpresaId = empresaId
            };
        }
    
    #endregion
    
}