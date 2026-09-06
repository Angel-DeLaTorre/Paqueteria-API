using Paqueteria.Dominio.Enums;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Cliente
{
    #region Columns
        public Guid Id { get; private init; }
        public string Nombre { get; private set; } = string.Empty;
        public EstatusBasico Estatus { get; private set; }
        public string? Rfc { get; private set; }
        public TipoPersona TipoPersona { get; private set; }
        public string? Telefono { get; private set; }
        public string? Telefono2 { get; private set; }
        public string? Correo { get; private set; }
        public string? Contacto { get; private set; }
        public string? NumConvenio { get; private set; }
        public string? PolizaSeguro { get; private set; }
        public DateTime FechaAlta { get; private init; }
        public Guid EmpresaId { get; private init; }

    #endregion
    
    #region ForeignKeys
        public Empresa Empresa { get; init; } = null!;
        public ICollection<DireccionCliente> Direcciones { get; set; } = new List<DireccionCliente>();

    #endregion

    #region Constructor
        private Cliente() {}

        public static Cliente Crear(
            string nombre, 
            string rfc, 
            TipoPersona tipoPersona,
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
                Id = Guid.CreateVersion7(),
                Nombre = nombre,
                Estatus = EstatusBasico.Activo,
                Rfc = rfc,
                TipoPersona = tipoPersona,
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