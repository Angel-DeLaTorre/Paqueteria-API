using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("direcciones_cliente", Schema = Constantes.Esquemas.Remisiones)]
public class DireccionCliente
{
    #region Columns

        [Key]
        [Column("id")]
        public Guid Id { get; init; } = Guid.NewGuid();

        public Direccion Direccion { get; set; } = null!;
        
        [Column("estatus")]
        public EstatusBasico Estatus { get; set; }
        
        [Column("cliente_id")]
        public Guid ClienteId { get; set; }

    #endregion
    
    #region Foreign Keys
    
    [ForeignKey("ClienteId")] public Cliente Cliente { get; set; } = null!;
    
    #endregion
    
    #region Constructors
    
        private DireccionCliente () {}

        public static DireccionCliente Create(Direccion direccion, Guid clienteId)
        {
            //TODO Validar campos

            return new DireccionCliente()
            {
                Id = Guid.NewGuid(),
                Direccion = direccion,
                ClienteId = clienteId
            };
        }
    
    #endregion
}