using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Dominio.Entidades.Sistema;

[Table("bitacora_sistema", Schema = Constantes.Esquemas.Sistema)]
public class BitacoraSistema
{
    #region Columns

        [Key]
        [Column("id")]
        public Guid Id { get; init; }

        [Required]
        [Column("sucursal_id")]
        public Guid SucursalId { get; init; }

        [Required]
        [Column("usuario_id")]
        public Guid UsuarioId { get; init; }

        [Required]
        [Column("accion")]
        public AccionBitacora Accion { get; init; }

        [Required]
        [MaxLength(50)]
        [Column("tabla")]
        public string Tabla { get; init; } = string.Empty;

        [Column("registro_id")]
        public Guid RegistroId { get; init; }

        [Column("valor_anterior", TypeName = "jsonb")]
        public string? ValorAnterior { get; init; }

        [Column("valor_nuevo", TypeName = "jsonb")]
        public string? ValorNuevo { get; init; }

        [MaxLength(50)]
        [Column("cliente_ip")]
        public string? ClienteIp { get; init; }

        [Required]
        [Column("fecha")]
        public DateTime Fecha { get; init; }

    #endregion

    #region ForeignKeys

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; } = null!;

        [ForeignKey("SucursalId")]
        public Sucursal Sucursal { get; set; } = null!;

    #endregion
    
    #region Constructors
        private BitacoraSistema() { }

        public static BitacoraSistema Create(Guid sucursalId, Guid usuario, AccionBitacora accion, string tabla, string? valorAnterior, string? valorNuevo, string clienteIp)
        {
            //TODO Validar campos

            return new BitacoraSistema()
            {
                SucursalId = sucursalId,
                UsuarioId = usuario,
                Accion = accion,
                Tabla = tabla,
                ValorAnterior = valorAnterior,
                ValorNuevo = valorNuevo,
                ClienteIp = clienteIp,
                Fecha = DateTime.UtcNow
            };
        }
    #endregion
}