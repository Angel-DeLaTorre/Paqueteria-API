using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entidades.Remisiones;

public partial class Seguro
{
    #region Columns
        public Guid Id { get; init; }
        public string Nombre { get; set; } = string.Empty;
        public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
        public Guid EmpresaId { get; init; }
        
        public Empresa Empresa { get; init; } = null!;
        
    #endregion
    
    #region Constructors
        
        private Seguro() {}

        public static Seguro Create(string nombre, Guid empresaId)
        {
            return new Seguro()
            {
                Nombre = nombre,
                EmpresaId = empresaId
            };
        }
    
    #endregion
}