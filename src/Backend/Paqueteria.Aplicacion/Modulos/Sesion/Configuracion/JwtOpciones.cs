namespace Paqueteria.Aplicacion.Modulos.Sesion.Configuracion;

public class JwtOpciones
{
    public const string SectionName = "Jwt";
    public string Key { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int DurationInMinutes { get; init; }
}