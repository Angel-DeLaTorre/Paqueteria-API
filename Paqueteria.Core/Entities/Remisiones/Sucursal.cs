using Paqueteria.Core.Enums;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entities.Remisiones;

public partial class Sucursal
{
    #region Columns
        public Guid Id { get; init; }
        public string Nombre { get; set; } = string.Empty;
        public string Codigo { get; set; }  = string.Empty;
        public bool EsMatriz { get; set; }
        public Direccion Direccion { get; set; } = null!;
        public string? Telefono { get; set; }
        public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
        public string? ServidorIp { get; init; }
        public Guid EmpresaId { get; init; }
        public Empresa Empresa { get; init; } = null!;
    #endregion

    #region Constructors
    
        private Sucursal() { }

        public static Sucursal Create(string nombre,  string codigo, bool esMatriz,  Direccion direccion, string telefono, Guid empresaId )
        {
            return new Sucursal()
            {
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