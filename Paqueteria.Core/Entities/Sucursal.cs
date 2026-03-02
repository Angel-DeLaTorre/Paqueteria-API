using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("sucursales")]
public class Sucursal
{
    #region Campos
        [Key]
        [Column("id")]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre
        {
            get;
            set => field = Utils.ToTitleCase(value).Trim();
        } = string.Empty;

        [Required]
        [MaxLength(50)]
        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("es_matriz")]
        public bool EsMatriz { get; set; }

        [Column("calle")]
        [MaxLength(100)]
        public string Calle { get; set; }

        [Column("colonia")]
        [MaxLength(100)]
        public string Colonia { get; set; }

        [Column("numero_exterior")]
        [MaxLength(10)]
        public string NumeroExterior { get; set; }

        [Column("numero_interior")]
        [MaxLength(10)]
        public string? NumeroInterior { get; set; }

        [Column("localidad")]
        [MaxLength(250)]
        public string? Localidad { get; set; }

        [Column("municipio_id")]
        public Guid MunicipioId { get; set; }

        [Column("telefono")]
        [MaxLength(20)]
        public string? Telefono { get; set; }

        [Column("estatus")]
        public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

        [Column("servidor_ip")]
        [MaxLength(50)]
        public string? ServidorIp { get; set; }
    #endregion

    #region Llaves
        [ForeignKey("MunicipioId")]
        public virtual Municipio Municipio { get; private set; } = null!;
    #endregion

}