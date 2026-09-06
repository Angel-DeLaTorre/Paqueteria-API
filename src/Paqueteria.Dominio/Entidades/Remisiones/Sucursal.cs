using Paqueteria.Dominio.Enums;
using Paqueteria.Dominio.ValueObjects;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Sucursal
{
    #region Columns
        public Guid Id { get; private init; }
        public string Nombre { get; private set; } = string.Empty;
        public string Codigo { get; private set; }  = string.Empty;
        public bool EsMatriz { get; private set; }
        public Direccion Direccion { get; private set; } = null!;
        public string? Telefono { get; private set; }
        public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
        public string? ServidorIp { get; private set; }
        public Guid EmpresaId { get; private init; }
        public Empresa Empresa { get; private init; } = null!;
    #endregion

    #region Constructors
    
        private Sucursal() { }

        public static Sucursal Crear(string nombre,  string codigo, bool esMatriz,  Direccion direccion, string telefono, Guid empresaId )
        {
            return new Sucursal()
            {
                Id = Guid.CreateVersion7(),
                Nombre = nombre,
                Codigo = codigo,
                EsMatriz = esMatriz,
                Direccion = direccion,
                Telefono = telefono,
                Estatus = EstatusBasico.Activo,
                EmpresaId = empresaId
            };
        }
    
    #endregion
}