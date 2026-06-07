using Microsoft.AspNetCore.Mvc;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.API.Configurations
{
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
                    
                    string descripcionError = string.Join(" ", erroresLista.Select(e => 
                        $"{e.Campo}: {string.Join(", ", e.Mensajes)}"));
                    
                    return new BadRequestObjectResult(Result<string>.Failure(descripcionError, ErrorCodes.Validacion.NoEncontrado));
                };
            });
        }
    }
}