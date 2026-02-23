namespace Paqueteria.Core.Enums;

public enum CodigoRespuesta
{
    Success,       // 200/204 - Todo bien
    BadRequest,       // 400 - Error de validación/regla de negocio
    Unauthorized,  // 401 - Falta token o expiró
    Forbidden,     // 403 - No tiene permisos a esa sucursal
    NotFound,      // 404 - No existe el Guid
    Conflict,      // 409 - Ya existe (ej. mismo código de sucursal)
    Failure        // 500 - Error inesperado
}