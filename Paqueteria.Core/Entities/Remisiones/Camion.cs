using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("camiones", Schema = Constantes.Esquemas.Remisiones)]
public class Camion
{
    [Key]
    [Column("id")]
    public Guid Id { get; init; }
    
    [Column("num_camion")]
    public string? NumCamion { get; set; }
    
    [Column("placa")]
    public string? Placa { get; set; }
    
    [Column("empresa_id")]
    public Guid EmpresaId { get; init; }
    
    #region ForeignKeys
    
    [ForeignKey("EmpresaId")] public Empresa Empresa { get; set; } = null!;
    
    #endregion
    
    #region Constructors
        
    public Camion()
    {}

    public static Camion Create(string numCamion, string placa, string empresaId)
    {
        return new Camion()
        {
            Id =  Guid.NewGuid(),
            NumCamion = numCamion,
            Placa = placa,
            EmpresaId = Guid.Parse(empresaId)
        };
    }
    
    #endregion
}