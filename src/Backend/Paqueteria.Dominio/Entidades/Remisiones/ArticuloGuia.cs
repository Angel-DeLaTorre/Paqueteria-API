using Paqueteria.Dominio.Entidades.Sat;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public class ArticuloGuia
{
    #region Columns
        public Guid Id { get; init; }
        public string ClaveProdServSat { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int Cantidad { get; set; }
        public string ClaveUnidadSat { get; set; } = null!;
        public decimal PesoUnitarioKg { get; set; }
        public decimal PesoTotalKg => Cantidad * PesoUnitarioKg;
        public string? ClaveTipoEmbalajeSat { get; set; }
        public decimal ValorUnidad { get; set; }
        public decimal Largo { get; set; }
        public decimal Ancho { get; set; }
        public decimal Alto { get; set; }
        public bool EsMaterialPeligroso { get; set; }
        public string? ClaveMaterialPeligrosoSat { get; set; }
        public Guid GuiaId { get; set; }
        public Guid? ArticuloId { get; init; }
        public Guia Guia { get; init; } = null!;
        public Articulo Articulo { get; init; } = null!;

    #endregion
    
    #region Constructors
        
        private ArticuloGuia() {}

        public static ArticuloGuia Crear
        (
            string claveProdServSat,
            string descripcion,
            int cantidad,
            string claveUnidadSat,
            decimal pesoUnitarioKg,
            string? claveTipoEmbalajeSat,
            decimal valorUnidad,
            decimal largo,
            decimal ancho,
            decimal alto,
            bool esMaterialPeligroso,
            string? claveMaterialPeligrosoSat
        )
        {
            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción del artículo no puede estar vacía.", nameof(descripcion));

            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.", nameof(cantidad));

            if (pesoUnitarioKg < 0 || valorUnidad < 0)
                throw new ArgumentException("El peso y el valor unitario no pueden ser negativos.");
            
            return new ArticuloGuia()
            {
                ClaveProdServSat = claveProdServSat,
                Descripcion = descripcion,
                Cantidad =  cantidad,
                ClaveUnidadSat = claveUnidadSat,
                PesoUnitarioKg =  pesoUnitarioKg,
                ClaveTipoEmbalajeSat = claveTipoEmbalajeSat,
                ValorUnidad = valorUnidad,
                Largo = largo,
                Ancho = ancho,
                Alto = alto,
                EsMaterialPeligroso = esMaterialPeligroso,
                ClaveMaterialPeligrosoSat = claveMaterialPeligrosoSat
            };
        }
        
        public static ArticuloGuia Crear
        (
            Guid articuloId,
            string descripcion,
            int cantidad,
            decimal pesoUnidad,
            decimal valorUnidad
        )
        {
            if (articuloId == Guid.Empty)
                throw new ArgumentException("El identificador del artículo no es válido.", nameof(articuloId));

            if (string.IsNullOrWhiteSpace(descripcion))
                throw new ArgumentException("La descripción del artículo no puede estar vacía.", nameof(descripcion));

            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.", nameof(cantidad));

            if (pesoUnidad < 0 || valorUnidad < 0)
                throw new ArgumentException("El peso y el valor unitario no pueden ser negativos.");
            
            return new ArticuloGuia()
            {
                Descripcion = descripcion,
                ArticuloId =  articuloId,
                Cantidad =  cantidad,
                PesoUnitarioKg = pesoUnidad,
                ValorUnidad = valorUnidad
            };
        }
    
    #endregion
}