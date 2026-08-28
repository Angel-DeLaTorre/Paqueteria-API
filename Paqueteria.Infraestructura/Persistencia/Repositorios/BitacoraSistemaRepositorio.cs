using Paqueteria.Core.Entidades.Sistema;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class BitacoraSistemaRepositorio(AppDbContext context) : EntityRepositorio<BitacoraSistema>(context) , IBitacoraSistemaRepositorio
{

}