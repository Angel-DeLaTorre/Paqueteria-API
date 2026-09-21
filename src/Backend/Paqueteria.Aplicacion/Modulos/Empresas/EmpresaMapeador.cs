using Paqueteria.Aplicacion.Comun.Mapeador;
using Paqueteria.Comun.Dtos.Empresas;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Empresas;

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