using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Dominio.Entidades.Sistema;

public partial class Usuario
{
    
    #region Columnas
        public Guid Id { get; private init; }
        public string Nombre { get; private set; } = null!;
        public string Username { get; private init; } = null!;
        public string Password { get; private set; } = null!;
        public EstatusBasico Estatus { get; private set; } = EstatusBasico.Activo;
        public DateTime FechaCreacion { get; private init; }
        public DateTime? FechaUltimoAcceso { get; private set; }
        public Guid EmpresaId { get; private init; }
    #endregion

    #region Navegacion
        public Empresa Empresa { get; private init; } = null!;
        public ICollection<UsuarioRol> UsuarioRoles { get; private set; } = new List<UsuarioRol>();
    #endregion

    #region Constructor
    
        private Usuario() {}
    
        public static Usuario Create(string nombre, string username, string password, Guid empresaId)
        {
            return new Usuario()
            {
                Nombre = nombre,
                Username = username,
                Password = password,
                Estatus = EstatusBasico.Activo,
                FechaCreacion =  DateTime.UtcNow,
                FechaUltimoAcceso = null,
                EmpresaId = empresaId
            };
        }
    
    #endregion

}