using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("rutas", Schema = Constantes.Esquemas.Remisiones)]
public class Ruta
{
    #region Column
    
        [Key]
        [Column("id")]
        public Guid Id { get; init; } = Guid.NewGuid();
        
        [MaxLength(200)]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        [Column("sucursal_origen_id")]
        public Guid SucursalOrigenId { get; set; }

        [Column("sucursal_destino_id")]
        public Guid SucursalDestinoId { get; set; }
        
        [Column("empresa_id")]
        public Guid EmpresaId { get; init; }
        
    #endregion

    #region ForeignKey
    
        [ForeignKey("SucursalOrigenId")] public virtual Sucursal SucursalOrigen { get; set; } = null!;

        [ForeignKey("SucursalDestinoId")] public virtual Sucursal SucursalDestino { get; set; } = null!;

        [ForeignKey("EmpresaId")] public Empresa Empresa { get; set; } = null!;

    #endregion
    
    #region Constructor
        private Ruta() { }

        public static Ruta Create(string descipcion, Guid sucursalOrigenId, Guid sucursalDestinoId,  Guid empresaId)
        {
            return new Ruta()
            {
                Id = Guid.NewGuid(),
                Descripcion = descipcion,
                SucursalOrigenId = sucursalOrigenId,
                SucursalDestinoId = sucursalDestinoId,
                EmpresaId = empresaId
            };
        }
    #endregion
}