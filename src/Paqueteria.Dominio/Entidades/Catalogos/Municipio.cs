namespace Paqueteria.Dominio.Entidades.Catalogos;

public class Municipio
{
    #region Columns
        public Guid Id { get; init; }
        public string SatId { get; init; } = string.Empty;
        public string Nombre { get; init; } = string.Empty;
        public string EstadoId { get; init; } = string.Empty;
        public Estado Estado { get; init; } = null!;
    #endregion
}