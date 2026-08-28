using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entidades.Remisiones;

public partial class Ruta
{
    #region Column
        public Guid Id { get; init; }
        public string? Descripcion { get; set; }
        public Guid SucursalOrigenId { get; set; }
        public Guid SucursalDestinoId { get; set; }
        public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
        public Guid EmpresaId { get; init; }
        
    #endregion

    #region ForeignKey
    
        public Sucursal SucursalOrigen { get; private set; } = null!;
        public Sucursal SucursalDestino { get; private set; } = null!;
        public Empresa Empresa { get; init; } = null!;

    #endregion
    
    #region Constructor
        private Ruta() { }

        public static Ruta Create(string descipcion, Guid sucursalOrigenId, Guid sucursalDestinoId,  Guid empresaId)
        {
            return new Ruta()
            {
                Descripcion = descipcion,
                SucursalOrigenId = sucursalOrigenId,
                SucursalDestinoId = sucursalDestinoId,
                EmpresaId = empresaId
            };
        }
    #endregion
}