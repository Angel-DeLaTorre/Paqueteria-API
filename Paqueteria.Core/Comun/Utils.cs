using System.Globalization;

namespace Paqueteria.Core.Comun;

public class Utils
{
    //Genera una cadena en Letras capital omitiendo articulos
    public static string ToProperCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return string.Empty;

        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        var result = textInfo.ToTitleCase(input);

        // Lista de excepciones que queremos en minúscula
        string[] minusculas = [" De ", " Del ", " La ", " Y ", " En "];

        return minusculas.Aggregate(result, (current, palabra) => current.Replace(palabra, palabra.ToLower()));
    }

    public static string ToTitleCase(string input)
    {
        return string.IsNullOrWhiteSpace(input) ? string.Empty : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
    }
}