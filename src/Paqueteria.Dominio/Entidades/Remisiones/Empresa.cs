using Paqueteria.Dominio.ValueObjects;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Empresa
{
    #region Columns
        public Guid Id { get; init; }
        public string Nombre { get; set; } = string.Empty;
        public string? NombreCorto { get; set; }
        public string Rfc { get; set; } = string.Empty;
        public Direccion? Direccion { get; set; }
        public DateTime FechaAlta { get; init; }
        
    #endregion
    
    #region Constructors
    
        private Empresa() { }

        public static Empresa Crear(string nombre, string nombreCorto, string rfc, Direccion direccion)
        {
            return new Empresa()
            {
                Nombre = nombre,
                NombreCorto = nombreCorto,
                Rfc = rfc,
                Direccion = direccion,
                FechaAlta = DateTime.UtcNow
            };
        }
    
    #endregion
}