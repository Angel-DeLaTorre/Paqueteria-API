using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Common;

namespace Paqueteria.Core.Entities.Sistema;

[Table("bitacora_accesos", Schema = Constantes.Esquemas.Sistema)]
public class BitacoraAcceso
{

    #region Columns

        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        [Required]
        [Column("usuario_id")]
        public Guid UsiarioId { get; init; }

        [Required]
        [Column("exito")]
        public bool Exito { get; init; }
        
        [Required]
        [Column("fecha_acceso")]
        public DateTime FechaAcceso { get; init; }

        [MaxLength(50)]
        [Column("cliente_ip")]
        public string? ClienteIp { get; init; }

    #endregion

    #region ForeignKeys

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; } = null!;

    #endregion
    
    #region Constructors
        private BitacoraAcceso() {}

        public static BitacoraAcceso Create(Guid usuarioId, bool exito, string? clienteIp)
        {
            return new BitacoraAcceso()
            {
                Id = Guid.NewGuid(),
                UsiarioId = usuarioId,
                Exito = exito,
                FechaAcceso = DateTime.UtcNow,
                ClienteIp = clienteIp
            };
        }
    #endregion
}