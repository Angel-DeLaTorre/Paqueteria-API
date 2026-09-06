namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Asignacion
{
    #region Columns
    
        public Guid Id { get; init; } = Guid.CreateVersion7();
        public string Clave { get; private set; }
        public DateTime FechaCreacion { get; private init; } = DateTime.UtcNow;
        public DateTime? FechaPartida { get; private set; } = DateTime.UtcNow;
        public Guid SucursalOrigenId { get; private set; }
        public Guid SucursalDestinoId { get; private set; }
        public string? St1 { get; private set; }
        public string? St2 { get; private set; }
        public string? St3 { get; private set; }
        public string? St4 { get; private set; }
        public Guid? ChoferId { get; private init; }
        public Guid EmpresaId { get; private init; }

        public Sucursal? SucursalOrigen { get; set; }
        public Sucursal? SucursalDestino { get; set; }
        public Chofer? Chofer { get; init; }
        public Empresa Empresa { get; init; } = null!;
        
        public ICollection<Guia> Guias { get; private set; } = new List<Guia>();

    #endregion

    #region Constructos

        private Asignacion() { }

        public static Asignacion Crear(
            string clave,
            Guid sucursalOrigenId,
            Guid sucursalDestinoId,
            DateTime? fechaPartida,
            string? st1, 
            string? st2, 
            string? st3, 
            string? st4,
            Guid? choferId, 
            Guid empresaId
            )
        {
            return new Asignacion()
            {
                Clave = clave,
                SucursalOrigenId = sucursalOrigenId,
                SucursalDestinoId = sucursalDestinoId,
                FechaCreacion = DateTime.UtcNow,
                FechaPartida = fechaPartida,
                St1 = st1,
                St2 = st2,
                St3 = st3,
                St4 = st4,
                ChoferId = choferId,
                EmpresaId = empresaId
            };
        }

    #endregion
}