using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Dominio.Entidades.Sistema;
public partial class Permiso
{
    #region Campos
        public Guid Id { get; init; } 
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
        public Guid? EmpresaId { get; init; }
        
        public Empresa? Empresa { get; init; }
        public ICollection<RolPermiso> RolPermiso { get; private set; } = new List<RolPermiso>();
    #endregion
    
    #region Constructor
    
        private Permiso() { }

        public static Permiso Crear(string nombre, string descripcion, Guid? empresaId)
        {
            return new Permiso()
            {
                Id = Guid.CreateVersion7(),
                Nombre = nombre,
                Descripcion = descripcion,
                EmpresaId = empresaId
            };
        }
    
    #endregion
    
}