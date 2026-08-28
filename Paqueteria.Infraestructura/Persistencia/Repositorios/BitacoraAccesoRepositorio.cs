using Paqueteria.Core.Entidades.Sistema;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class BitacoraAccesoRepositorio(AppDbContext context) : EntityRepositorio<BitacoraAcceso>(context) , IBitacoraAccesoRepositorio
{

}