namespace Paqueteria.Core.Entities.Catalogos;

public partial class Estado
{
    
    #region Columns
        public string Id { get; init; } = string.Empty;
        public string Nombre { get; init; } = string.Empty;
        public string Pais { get; init; } = string.Empty;
        public string Acronimo { get; init; } = string.Empty;
    #endregion
}