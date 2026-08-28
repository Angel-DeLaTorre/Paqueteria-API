namespace Paqueteria.Core.Entidades.Remisiones;

public partial class Chofer
{
    private static void ValidarDatosPersonales(string nombre, string apellidoPaterno, string telefono)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es requerido.");

        if (string.IsNullOrWhiteSpace(apellidoPaterno))
            throw new ArgumentException("El apellido paterno es requerido.");

        if (string.IsNullOrWhiteSpace(telefono))
            throw new ArgumentException("El teléfono es requerido.");
    }
}