using Paqueteria.Comun.Comun.Errores;

namespace Paqueteria.Dominio.Comun;

public static class FabricaErrores
{
    public static Error NoEncontrado<T>() => 
        CodigosError.Comun.NoEncontrado.WithArgs(EntidadesCatalogo.ObtenerNombre<T>());
    public static Error Duplicado<T>() => 
        CodigosError.Comun.Duplicado.WithArgs(EntidadesCatalogo.ObtenerNombre<T>());
    public static Error NoCreado<T>() => 
        CodigosError.Comun.NoCreado.WithArgs(EntidadesCatalogo.ObtenerNombre<T>());
    public static Error NoActualizado<T>() => 
        CodigosError.Comun.NoActualizado.WithArgs(EntidadesCatalogo.ObtenerNombre<T>());
    public static Error NoEliminado<T>() => 
        CodigosError.Comun.NoEliminado.WithArgs(EntidadesCatalogo.ObtenerNombre<T>());

}