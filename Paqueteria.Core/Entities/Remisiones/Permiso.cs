using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("permisos", Schema = Constantes.Esquemas.Remisiones)]
public class Permiso
{
    [Key]
    [Column("id")]
    public Guid Id { get; init; } 

    [Required] [Column("nombre")] 
    public string Nombre { get; set; } = null!;
    
    [Required]
    [Column("descripcion")]
    public string Descripcion { get; set; } = null!;
    
    [Required]
    [Column("empresa_id")]
    public Guid EmpresaId { get; init; }
    
    public ICollection<RolPermiso> RolPermiso { get; private set; } = new List<RolPermiso>();

    
    #region ForeignKey
    
    [ForeignKey("EmpresaId")] public Empresa Empresa { get; init; } = null!;
    
    #endregion
    
    #region Constructor
    
    private Permiso() { }

    public static Permiso Create(string nombre, string descripcion, Guid empresaId)
    {
        return new Permiso()
        {
            Id = Guid.NewGuid(),
            Nombre = nombre,
            Descripcion = descripcion,
            EmpresaId = empresaId
        };
    }
    
    #endregion
    
}