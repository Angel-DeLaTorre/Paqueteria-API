using Paqueteria.Dominio.Enums;
using Paqueteria.Dominio.ValueObjects;

namespace Paqueteria.Dominio.Entidades.Remisiones;

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

        public static DireccionCliente Crear(Direccion direccion, Guid clienteId)
        {
            return new DireccionCliente()
            {
                Id = Guid.CreateVersion7(),
                Direccion = direccion,
                ClienteId = clienteId
            };
        }
    
    #endregion
}