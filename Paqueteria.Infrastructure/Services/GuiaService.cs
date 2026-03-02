using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;

namespace Paqueteria.Infrastructure.Services;

public class GuiaService(IGuiaRepository guiaRepository) : IGuiaService
{
    
}