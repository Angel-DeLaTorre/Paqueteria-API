namespace Paqueteria.Dominio.Entidades.Sat;

public class Articulo
{
    #region Columns
        public Guid Id { get; init; }
        public string ClaveSat { get; init; } = string.Empty;
        public string Texto { get; set; } =  string.Empty;
        public string Similares { get; set; } =  string.Empty;
        public string MaterialPeligroso { get; set; } =  string.Empty;
        public DateTime VigenciaDesde { get; set; }
        public DateTime VigenciaHasta { get; set; }
        

    #endregion
    
    #region Constructors
    
        private Articulo() { }

        public static Articulo Create(string texto, string similares, string materialPeligroso, DateTime vigenciaDesde, DateTime vigenciaHasta)
        {
            return new Articulo
            {
                Texto = texto,
                Similares = similares,
                MaterialPeligroso = materialPeligroso,
                VigenciaDesde = vigenciaDesde,
                VigenciaHasta = vigenciaHasta
            };
        }
        
    #endregion
}