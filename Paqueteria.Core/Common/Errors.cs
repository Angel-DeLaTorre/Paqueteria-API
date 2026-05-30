namespace Paqueteria.Core.Common;

public class Errors
{
    public static class Generic
    {
        public static readonly Error NoEncontrado = new("ERROR_001", "Elemento no encontrado.");
        public static readonly Error NoCreado = new("ERROR_002", "Elemento no creado.");
        public static readonly Error NoActualizado = new("ERROR_003", "Elemento no actualizado.");
        public static readonly Error NoEliminado = new("ERROR_004", "Elemento no eliminado.");
        public static readonly Error Conflicto = new("ERROR_005", "Elemento ya existe.");
    }
    public static class Users
    {
        public static readonly Error NotFound = new("USER_001", "Usuario no encontrado.");
        public static readonly Error InvalidUser = new("USER_002", "El correo electrónico no tiene un formato válido.");
        public static readonly Error Bloqueado = new("USER_003", "El usuario se encuentra desactivado.");
    }

    public static class Roles
    {
        public static readonly Error NotFound = new("ROLE_015", "Usuario no encontrado.");
    }
}