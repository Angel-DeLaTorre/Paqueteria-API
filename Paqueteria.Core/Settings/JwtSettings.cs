namespace Paqueteria.Core.Settings;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    public string Key { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int DurationInMinutes { get; init; }
}