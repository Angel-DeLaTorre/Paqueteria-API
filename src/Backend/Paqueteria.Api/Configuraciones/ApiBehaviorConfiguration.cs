using Microsoft.AspNetCore.Mvc;
using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.API.Configuraciones;

public static class ApiBehaviorConfiguration
{
    public static IMvcBuilder ConfigureCustomValidationErrorResponse(this IMvcBuilder builder)
    {
        return builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var erroresLista = context.ModelState
                    .Where(x => x.Value?.Errors.Count > 0)
                    .Select(x => new
                    {
                        Campo = x.Key,
                        Mensajes = x.Value!.Errors.Select(e => e.ErrorMessage).ToList()
                    })
                    .ToList();

                var descripcionError = string.Join(" ", erroresLista.Select(e =>
                    $"{e.Campo}: {string.Join(", ", e.Mensajes)}"));

                return new BadRequestObjectResult(Respuesta<string>.Error(descripcionError,
                    FabricaErrores.NoEncontrado<Usuario>()));
            };
        });
    }
}