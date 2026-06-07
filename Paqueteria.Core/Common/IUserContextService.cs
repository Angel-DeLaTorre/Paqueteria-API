namespace Paqueteria.Core.Common;

public interface IUserContextService
{
    Guid UserId { get; }
    string Username { get; }
    Guid SucursalId { get; }
    Guid EmpresaId { get; }
    string IpAddress { get; }
}