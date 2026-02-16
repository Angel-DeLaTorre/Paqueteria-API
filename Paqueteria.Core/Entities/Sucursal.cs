using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities;

[Table("sucursales")]
public class Sucursal
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    [Column("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("codigo")]
    public string Codigo { get; set; } = string.Empty;

    [Column("es_matriz")]
    public bool EsMatriz { get; set; } = false;

    [Column("calle")]
    [MaxLength(100)]
    public string Calle { get; set; } = string.Empty;

    [Column("colonia")]
    [MaxLength(100)]
    public string Colonia { get; set; } = string.Empty;

    [Column("numero_exterior")]
    [MaxLength(10)]
    public string NumeroExterior { get; set; }  = string.Empty;

    [Column("numero_interior")]
    [MaxLength(10)]
    public string? NumeroInterior { get; set; }

    [Column("localidad")]
    [MaxLength(250)]
    public string? Localidad { get; set; }

    [Column("municipio_id")]
    public Guid MunicipioId { get; private set; }

    [Column("telefono")]
    [MaxLength(20)]
    public string? Telefono { get; set; }

    [Column("estatus")]
    public EstatusGenerico Estatus { get; set; } = EstatusGenerico.Activo;

    [Column("servidor_ip")]
    [MaxLength(50)]
    public string? ServidorIp { get; set; }

    // llaves
    [ForeignKey("MunicipioId")]
    public virtual Municipio Municipio { get; private set; } = null!;

    // colleciones
    [InverseProperty("SucursalOrigen")]
    public virtual ICollection<Ruta> RutasOrigen { get; set; } = new List<Ruta>();

    [InverseProperty("SucursalDestino")]
    public virtual ICollection<Ruta> RutasDestino { get; set; } = new List<Ruta>();

    public Sucursal() {}

    public Sucursal(
        string nombre,
        string codigo,
        bool esMatriz,
        string calle,
        string colonia,
        string numeroExterior,
        string? numeroInterior,
        string? localidad,
        Guid municipioId,
        string telefono)
    {
        Nombre = nombre;
        Codigo = codigo;
        EsMatriz = esMatriz;
        Calle = calle;
        Colonia = colonia;
        NumeroExterior = numeroExterior;
        NumeroInterior = numeroInterior;
        Localidad = localidad;
        MunicipioId = municipioId;
        Telefono = telefono;
    }
}