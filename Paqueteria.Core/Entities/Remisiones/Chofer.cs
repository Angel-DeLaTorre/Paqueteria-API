using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.Enums;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entities.Remisiones
{
    [Table("choferes", Schema = Constantes.Esquemas.Remisiones)]
    public class Chofer
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
            [MaxLength(100)]
            [Column("apellido_paterno")]
            public string ApellidoPaterno { get; set; } = string.Empty;

            [MaxLength(100)]
            [Column("apellido_materno")]
            public string? ApellidoMaterno { get; set; }

            [Column("estatus")]
            public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

            public Direccion? Direccion { get; set; }

            [MaxLength(20)]
            [Column("telefono")]
            public string? Telefono { get; set; }

            [Column("num_camion")]
            public string? NumCamion { get; set; }

            [Column("num_contenedor")]
            public string? NumContenedor { get; set; }

            [Column("num_contenedor2")]
            public string? NumContenedor2 { get; set; }

            [Column("fecha_alta")]
            public DateTime FechaAlta { get; init; } = DateTime.UtcNow;

            [Column("fecha_baja")]
            public DateTime? FechaBaja { get; set; }
            
            [Column("empresa_id")]
            public Guid EmpresaId { get; init; }

        #endregion

        #region ForeignKeys
            [ForeignKey("Direccion.MunicipioId")] public virtual Municipio? Municipio { get; set; }
            
            [ForeignKey("EmpresaId")] public Empresa Empresa { get; set; } = null!;
        #endregion

        #region Constructors
        
            private Chofer() { }

            public static Chofer Create(
                string nombre, 
                string apellidoPaterno, 
                string? apellidoMaterno, 
                Direccion direccion, 
                string? telefono,
                string? numCamion,
                string? numContenedor,
                string? numContenedor2,
                Guid empresaId
                )
            {
                return new Chofer()
                {
                    Nombre = nombre,
                    ApellidoPaterno = apellidoPaterno,
                    ApellidoMaterno = apellidoMaterno,
                    Estatus = EstatusGenerico.Activo,
                    Direccion = direccion,
                    Telefono = telefono,
                    NumCamion = numCamion,
                    NumContenedor = numContenedor,
                    NumContenedor2 = numContenedor2,
                    FechaAlta = DateTime.UtcNow,
                    FechaBaja = null,
                    EmpresaId = empresaId
                };
            }
            
        #endregion
    }
}