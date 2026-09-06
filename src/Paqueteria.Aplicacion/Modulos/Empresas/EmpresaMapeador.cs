using Paqueteria.Application.Comun.Mapeador;
using Paqueteria.Application.Modulos.Empresas.Dtos;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Empresas;

public static class EmpresaMapeador
{
    extension(Empresa empresa)
    {
        public EmpresaRespuestaDto MapeaRespuestaDto()
        {
            var direccionRespuesta = empresa.Direccion?.MapeaRespuestaDto();
            
            return new EmpresaRespuestaDto
            (
                empresa.Id,
                empresa.Nombre,
                empresa.NombreCorto,
                empresa.Rfc,
                direccionRespuesta,
                empresa.FechaAlta
            );
        }
    }
}