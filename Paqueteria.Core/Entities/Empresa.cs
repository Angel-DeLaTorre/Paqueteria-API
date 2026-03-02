using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paqueteria.Core.Entities;

[Table("empresas")]
public class Empresa
{
    #region Campos

        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(150)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("nombre_corto")]
        public string? NombreCorto { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("rfc")]
        public string Rfc { get; set; } = string.Empty;

        [MaxLength(200)]
        [Column("calle")]
        public string Calle { get; set; } = string.Empty;

        [MaxLength(6)]
        [Column("codigo_postal")]
        public string CodigoPostal { get; set; } = string.Empty;

        [Column("municipio_id")]
        public Guid MunicipioId { get; set; }

        [Column("fecha_alta")]
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

    #endregion

    #region Llaves
        [ForeignKey("MunicipioId")]
        public virtual Municipio Municipio { get; set; } = null!;
    #endregion
}