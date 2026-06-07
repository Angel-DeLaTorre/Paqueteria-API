using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Remisiones;

[Table("clientes", Schema = Constantes.Esquemas.Remisiones)]
public class Cliente
{
    #region Columns

        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("estatus")]
        public EstatusBasico Estatus { get; set; }

        [MaxLength(20)]
        [Column("rfc")]
        public string? Rfc { get; set; }

        [MaxLength(20)]
        [Column("telefono")]
        public string? Telefono { get; set; }

        [MaxLength(20)]
        [Column("telefono_2")]
        public string? Telefono2 { get; set; }

        [MaxLength(100)]
        [Column("correo")]
        public string? Correo { get; set; }

        [MaxLength(100)]
        [Column("contacto")]
        public string? Contacto { get; set; }

        [MaxLength(50)]
        [Column("num_convenio")]
        public string? NumConvenio { get; set; }

        [MaxLength(50)]
        [Column("poliza_seguro")]
        public string? PolizaSeguro { get; set; }

        [Column("fecha_alta")]
        public DateTime FechaAlta { get; init; }
        
        [Column("empresa_id")]
        public Guid EmpresaId { get; init; }

    #endregion
    
    #region ForeignKeys

        [ForeignKey("EmpresaId")] public Empresa Empresa { get; init; } = null!;
        
        public ICollection<DireccionCliente> Direcciones { get; set; } = new List<DireccionCliente>();

    #endregion

    #region Constructor
        private Cliente() {}

        public static Cliente Create(
            string nombre, 
            string rfc, 
            string telefono, 
            string? telefono2, 
            string? correo, 
            string? contacto,
            string? numConvenio,
            string? polizaSeguro,
            Guid empresaId
        ) 
        {
            return new Cliente()
            {
                Id = Guid.NewGuid(),
                Nombre = nombre,
                Estatus = EstatusBasico.Activo,
                Rfc = rfc,
                Telefono = telefono,
                Telefono2 = telefono2,
                Correo = correo,
                Contacto = contacto,
                NumConvenio = numConvenio,
                PolizaSeguro = polizaSeguro,
                FechaAlta = DateTime.UtcNow,
                EmpresaId = empresaId
                
            };
        }
    #endregion
}