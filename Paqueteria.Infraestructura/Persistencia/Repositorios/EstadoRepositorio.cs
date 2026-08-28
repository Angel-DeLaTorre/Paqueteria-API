using Paqueteria.Core.Entidades.Catalogos;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class EstadoRepositorio(AppDbContext context): EntityRepositorio<Estado>(context), IEstadoRepositorio
{

}