using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Sistema;

[Table("roles", Schema = Constantes.Esquemas.Remisiones)]
public class Rol
{
    [Key]
    [Column("id")]
    public Guid Id { get; init; }
    
    [Required]
    [Column("nombre")]
    public string Nombre { get; set; } = null!;
    
    [Required]
    [Column("descripcion")]
    public string Descripcion { get; set; } = null!;
    
    [Column("estatus")]
    public EstatusBasico Estatus { get; set; } = EstatusBasico.Activo;
    
    [Required]
    [Column("empresa_id")]
    public Guid EmpresaId { get; init; }

    public ICollection<UsuarioRol> UsuarioRol { get; private set; } = new List<UsuarioRol>();
    public ICollection<RolPermiso> RolPermiso { get; private set; } = new List<RolPermiso>();
    
    #region ForeignKey
    
    [ForeignKey("EmpresaId")] public Empresa Empresa { get; init; } = null!;
    
    #endregion
    
    #region Constructor
    
    private Rol() {}

    public static Rol Create(string nombre, string descripcion, Guid empresaId)
    {
        return new Rol()
        {
            Id = Guid.NewGuid(),
            Nombre = nombre,
            Descripcion = descripcion,
            EmpresaId = empresaId
        };
    }
    
    #endregion
}