using Paqueteria.Aplicacion.Comun.Mapeador;
using Paqueteria.Comun.Dtos.Choferes;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Choferes;

public static class ChoferMapeador
{
    extension(Chofer chofer)
    {
        public ChoferRespuestaDto MapeaRespuestaDto()
        {
            var direccion = chofer.Direccion?.MapeaRespuestaDto();
            
            return new ChoferRespuestaDto
            (
                chofer.Id,
                chofer.Nombre,
                chofer.ApellidoPaterno,
                chofer.ApellidoMaterno,
                direccion,
                chofer.Telefono,
                chofer.NumCamion,
                chofer.NumContenedor,
                chofer.NumContenedor2
            );
        }
    }
}