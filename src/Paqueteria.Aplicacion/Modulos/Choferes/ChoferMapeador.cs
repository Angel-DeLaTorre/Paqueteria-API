using Paqueteria.Application.Comun.Mapeador;
using Paqueteria.Application.Modulos.Choferes.Dtos;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Choferes;

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