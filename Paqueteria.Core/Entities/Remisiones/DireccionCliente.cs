using Paqueteria.Core.Enums;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entities.Remisiones;

public partial class DireccionCliente
{
    #region Columns
        public Guid Id { get; init; }
        public Direccion Direccion { get; private set; } = null!;
        public EstatusBasico Estatus { get; private set; }
        public Guid ClienteId { get; init; }

    #endregion
    
    #region Foreign Keys
    
        public Cliente Cliente { get; init; } = null!;
    
    #endregion
    
    #region Constructors
    
        private DireccionCliente () {}

        public static DireccionCliente Create(Direccion direccion, Guid clienteId)
        {
            //TODO Validar campos

            return new DireccionCliente()
            {
                Direccion = direccion,
                ClienteId = clienteId
            };
        }
    
    #endregion
}