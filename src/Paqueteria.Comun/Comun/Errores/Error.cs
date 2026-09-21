namespace Paqueteria.Comun.Comun.Errores;

public record Error(string Code, string Description)
{
    public Error WithArgs(params object[] args)
    {
        return this with { Description = string.Format(Description, args) };
    }
}