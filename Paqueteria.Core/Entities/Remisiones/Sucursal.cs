using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.Enums;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("sucursales", Schema = Constantes.Esquemas.Remisiones)]
public class Sucursal
{
    #region Columns
    
        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("codigo")]
        public string Codigo { get; set; }  = string.Empty;

        [Column("es_matriz")]
        public bool EsMatriz { get; set; }

        public Direccion Direccion { get; set; } = null!;

        [Column("telefono")]
        [MaxLength(20)]
        public string? Telefono { get; set; }

        [Column("estatus")]
        public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

        [Column("servidor_ip")]
        [MaxLength(50)]
        public string? ServidorIp { get; set; }
        
        [Column("empresa_id")]
        public Guid EmpresaId { get; init; }
        
    #endregion

    #region ForeignKey
        [ForeignKey("EmpresaId")] public Empresa Empresa { get; init; } = null!;
    #endregion

    #region Constructors
    
        private Sucursal() { }

        public static Sucursal Create(string nombre,  string codigo, bool esMatriz,  Direccion direccion, string telefono, Guid empresaId )
        {
            return new Sucursal()
            {
                Id = Guid.NewGuid(),
                Nombre = nombre,
                Codigo = codigo,
                EsMatriz = esMatriz,
                Direccion = direccion,
                Telefono = telefono,
                Estatus = EstatusGenerico.Activo,
                EmpresaId = empresaId
            };
        }
    
    #endregion
}