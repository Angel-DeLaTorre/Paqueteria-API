using Paqueteria.Dominio.Enums;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Seguro
{
    #region Columns
        public Guid Id { get; private init; }
        public string Nombre { get; private set; } = string.Empty;
        public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
        public Guid EmpresaId { get; private init; }
        
        public Empresa Empresa { get; init; } = null!;
        
    #endregion
    
    #region Constructors
        
        private Seguro() {}

        public static Seguro Crear(string nombre, Guid empresaId)
        {
            return new Seguro()
            {
                Id = Guid.CreateVersion7(),
                Nombre = nombre,
                EmpresaId = empresaId
            };
        }
    
    #endregion
}