namespace Paqueteria.Dominio.Comun.Errors;

public abstract class CodigosError
{
    public static class TipoError
    {
        public const string NoEncontrado = "ERR_NO_ENCONTRADO";
        public const string Duplicado = "ERR_DUPLICADO";
        public const string NoCreado = "ERR_NO_CREADO";
        public const string NoActualizado = "ERR_NO_ACTUALIZADO";
        public const string NoEliminado = "ERR_NO_ELIMINADO";
        public const string NoAutorizado = "USER_004";
        public const string Prohibido = "USER_003";
    }

    
    public static class Comun
    {
        public static readonly Error NoEncontrado = new(TipoError.NoEncontrado, "No se encontro {0}.");
        public static readonly Error NoCreado = new(TipoError.NoCreado, (string)"Elemento no creado.");
        public static readonly Error NoActualizado = new(TipoError.NoActualizado, (string)"Elemento no actualizado.");
        public static readonly Error NoEliminado = new(TipoError.NoEliminado, (string)"Elemento no eliminado.");
        public static readonly Error Duplicado = new(TipoError.Duplicado, (string)"Ya existe un registro con el valor indicado para {0}.");
    }
    
    public static class Usuario
    {
        public static readonly Error Desactivado = new((string)"USER_003", (string)"El usuario se encuentra desactivado.");
        public static readonly Error ContrasennaErronea = new((string)"USER_004", (string)"Contraseña equivocada.");
    }
}