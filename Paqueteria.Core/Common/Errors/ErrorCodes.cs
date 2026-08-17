namespace Paqueteria.Core.Common.Errors;

public abstract class CodigosError
{
    public static class Generic
    {
        public static readonly Error NoEncontrado = new((string)"ERROR_001", (string)"Elemento no encontrado.");
        public static readonly Error NoCreado = new((string)"ERROR_002", (string)"Elemento no creado.");
        public static readonly Error NoActualizado = new((string)"ERROR_003", (string)"Elemento no actualizado.");
        public static readonly Error NoEliminado = new((string)"ERROR_004", (string)"Elemento no eliminado.");
        public static readonly Error Conflicto = new((string)"ERROR_005", (string)"Elemento ya existe.");
    }
    public static class Validacion
    {
        public static readonly Error NoEncontrado = new((string)"PARAM_001", (string)"Parametro no enviado.");
    }
    public static class Users
    {
        public static readonly Error NotFound = new((string)"USER_001", (string)"Usuario no encontrado.");
        public static readonly Error InvalidUser = new((string)"USER_002", (string)"El correo electrónico no tiene un formato válido.");
        public static readonly Error Bloqueado = new((string)"USER_003", (string)"El usuario se encuentra desactivado.");
        public static readonly Error ContrasennaErronea = new((string)"USER_004", (string)"Contraseña equivocada.");
    }

    public static class Roles
    {
        public static readonly Error NotFound = new((string)"ROLE_015", (string)"Usuario no encontrado.");
    }

    public static class Reportes
    {
        public static readonly Error SinDatos = new((string)"REPORTES_01", (string)"Sin datos para el reporte.");
    }
}