namespace Paqueteria.Core.Entities.Remisiones;

public partial class Camion
{
    public Guid Id { get; init; }
    public string? NumCamion { get; set; }
    public string? Placa { get; set; }
    public Guid EmpresaId { get; init; }
    public Empresa Empresa { get; init; } = null!;
    
    #region Constructors
        
    public Camion()
    {}

    public static Camion Create(string numCamion, string placa, string empresaId)
    {
        return new Camion()
        {
            NumCamion = numCamion,
            Placa = placa,
            EmpresaId = Guid.Parse(empresaId)
        };
    }
    
    #endregion
}