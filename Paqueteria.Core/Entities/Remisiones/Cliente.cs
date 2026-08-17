using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Remisiones;

public partial class Cliente
{
    #region Columns
        public Guid Id { get; init; }
        public string Nombre { get; set; } = string.Empty;
        public EstatusBasico Estatus { get; private set; }
        public string? Rfc { get; set; }
        public string? Telefono { get; set; }
        public string? Telefono2 { get; set; }
        public string? Correo { get; set; }
        public string? Contacto { get; set; }
        public string? NumConvenio { get; set; }
        public string? PolizaSeguro { get; set; }
        public DateTime FechaAlta { get; init; }
        public Guid EmpresaId { get; init; }

    #endregion
    
    #region ForeignKeys
        public Empresa Empresa { get; init; } = null!;
        public ICollection<DireccionCliente> Direcciones { get; set; } = new List<DireccionCliente>();

    #endregion

    #region Constructor
        private Cliente() {}

        public static Cliente Create(
            string nombre, 
            string rfc, 
            string telefono, 
            string? telefono2, 
            string? correo, 
            string? contacto,
            string? numConvenio,
            string? polizaSeguro,
            Guid empresaId
        ) 
        {
            return new Cliente()
            {
                Nombre = nombre,
                Estatus = EstatusBasico.Activo,
                Rfc = rfc,
                Telefono = telefono,
                Telefono2 = telefono2,
                Correo = correo,
                Contacto = contacto,
                NumConvenio = numConvenio,
                PolizaSeguro = polizaSeguro,
                FechaAlta = DateTime.UtcNow,
                EmpresaId = empresaId
                
            };
        }
    #endregion
}