using Paqueteria.Dominio.Enums;
using Paqueteria.Dominio.ValueObjects;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Chofer
{
    #region Columns
        public Guid Id { get; init; }
        public string Nombre { get; private set; } = string.Empty;
        public string ApellidoPaterno { get; private set; } = string.Empty;
        public string? ApellidoMaterno { get; private set; }
        public EstatusBasico Estatus { get; private set; }
        public Direccion? Direccion { get; private set; }
        public string? Telefono { get; private set; }
        public string? NumCamion { get; private set; }
        public string? NumContenedor { get; private set; }
        public string? NumContenedor2 { get; private set; }
        public Guid? CamionId { get; private set; }
        public DateTime FechaAlta { get; private init; }
        public DateTime? FechaBaja { get; private set; }
        public Guid EmpresaId { get; private init; }
        public Camion Camion { get; private set; } = null!;
        public Empresa Empresa { get; private set; } = null!;
    #endregion

    #region Constructors
    
        private Chofer() { }

        public static Chofer Crear(
            string nombre, 
            string apellidoPaterno, 
            string? apellidoMaterno, 
            Direccion direccion, 
            string? telefono,
            string? numCamion,
            string? numContenedor,
            string? numContenedor2,
            Guid empresaId
        )
        {
            return new Chofer()
            {
                Nombre = nombre,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                Estatus = EstatusBasico.Activo,
                Direccion = direccion,
                Telefono = telefono,
                NumCamion = numCamion,
                NumContenedor = numContenedor,
                NumContenedor2 = numContenedor2,
                FechaAlta = DateTime.UtcNow,
                FechaBaja = null,
                EmpresaId = empresaId
            };
        }
        
    #endregion
}