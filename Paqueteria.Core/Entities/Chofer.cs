using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities
{
    [Table("choferes")]
    public class Chofer
    {
        #region Campos

            [Key]
            [Column("chofer_id")]
            public Guid ChoferId { get; init; } = Guid.NewGuid();

            [Required]
            [MaxLength(100)]
            [Column("nombre")]
            public string Nombre { get; set; }

            [Required]
            [MaxLength(100)]
            [Column("apellido_paterno")]
            public string ApellidoPaterno { get; set; }

            [MaxLength(100)]
            [Column("apellido_materno")]
            public string? ApellidoMaterno { get; set; }

            [Column("estatus")]
            public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

            [Column("calle")]
            [MaxLength(100)]
            public string? Calle { get; set; }

            [Column("colonia")]
            [MaxLength(100)]
            public string? Colonia { get; set; }

            [Column("numero_exterior")]
            [MaxLength(10)]
            public string? NumeroExterior { get; set; }

            [Column("numero_interior")]
            [MaxLength(10)]
            public string? NumeroInterior { get; set; }

            [Column("localidad")]
            [MaxLength(250)]
            public string? Localidad { get; set; }

            [Column("municipio_id")]
            public Guid MunicipioId { get; set; }

            [MaxLength(20)]
            [Column("telefono")]
            public string? Telefono { get; set; }

            [Column("num_camion")]
            public int? NumCamion { get; set; }

            [Column("num_contenedor")]
            public int? NumContenedor { get; set; }

            [Column("num_contenedor2")]
            public int? NumContenedor2 { get; set; }

            [Column("fecha_alta")]
            public DateTime FechaAlta { get; init; } = DateTime.UtcNow;

            [Column("fecha_baja")]
            public DateTime? FechaBaja { get; set; }

        #endregion

        #region Llaves

            [ForeignKey("IdMunicipio")]
            public virtual Municipio Municipio { get; private set; } = null!;

        #endregion


        public Chofer(string nombre,
            string apellidoPaterno,
            string apellidoMaterno,
            string calle,
            string colonia,
            string numeroExterior,
            string? numeroInterior,
            string? localidad,
            Guid municipioId,
            string telefono,
            int? numCamion,
            int? numContenedor,
            int? numContenedor2)
        {
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            Calle = calle;
            Colonia = colonia;
            NumeroExterior = numeroExterior;
            NumeroInterior = numeroInterior;
            Localidad = localidad;
            MunicipioId = municipioId;
            Telefono = telefono;
            NumCamion = numCamion;
            NumContenedor = numContenedor;
            NumContenedor2 = numContenedor2;
        }
    }
}