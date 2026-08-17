namespace Paqueteria.Core.Helpers;

public static class NumeroALetrasHelper
{
    public static string Convertir(decimal numero)
    {
        // 1. Validar rangos máximos soportados por el algoritmo
        if (numero < 0 || numero > 999999999.99m)
            throw new ArgumentOutOfRangeException(nameof(numero), "El monto excede el límite soportado por el convertidor.");

        // 2. Extraer y formatear los centavos (ej: 50/100 M.N.)
        var entero = (long)Math.Truncate(numero);
        var centavos = (int)Math.Round((numero - entero) * 100);
        var sufijoCentavos = $"{centavos:D2}/100 M.N.";

        if (entero == 0)
            return $"CERO PESOS {sufijoCentavos}";

        // 3. Procesar las secciones numéricas (Millones, Miles, Cientos)
        var resultado = ConvertirBloque(entero).Trim();

        // 4. Ajustar concordancia gramatical para el uno
        if (resultado.EndsWith("UN"))
            resultado += "O"; // Cambia "UN PESOS" a "UNO PESOS" si fuera el caso aislado, pero aquí ajustamos a "PESOS"
        
        if (entero == 1)
            return $"UN PESO {sufijoCentavos}";
        
        if (resultado.EndsWith("MILLON") || resultado.EndsWith("MILLONES"))
            return $"{resultado} DE PESOS {sufijoCentavos}";

        return $"{resultado} PESOS {sufijoCentavos}";
    }

    private static string ConvertirBloque(long numero)
    {
        if (numero == 0) return "";

        if (numero < 20)
        {
            string[] unidades = { "", "UN", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE", "DIEZ", 
                                  "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISEIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE" };
            return unidades[numero];
        }

        if (numero < 100)
        {
            string[] decenas = { "", "", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" };
            long residuo = numero % 10;
            
            if (numero == 20) return "VEINTE";
            if (numero < 30) return $"VEINTI{ConvertirBloque(residuo)}";
            
            return residuo == 0 ? decenas[numero / 10] : $"{decenas[numero / 10]} Y {ConvertirBloque(residuo)}";
        }

        if (numero < 1000)
        {
            string[] centenas = { "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS" };
            long residuo = numero % 100;
            
            if (numero == 100) return "CIEN";
            return $"{centenas[numero / 100]} {ConvertirBloque(residuo)}";
        }

        if (numero < 1000000)
        {
            long miles = numero / 1000;
            long residuo = numero % 1000;
            
            string textoMiles = miles == 1 ? "MIL" : $"{ConvertirBloque(miles)} MIL";
            return $"{textoMiles} {ConvertirBloque(residuo)}";
        }

        // Millones
        long millones = numero / 1000000;
        long residuoMillones = numero % 1000000;
        
        string textoMillones = millones == 1 ? "UN MILLON" : $"{ConvertirBloque(millones)} MILLONES";
        return $"{textoMillones} {ConvertirBloque(residuoMillones)}";
    }
}