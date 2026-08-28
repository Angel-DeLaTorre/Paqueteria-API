using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Folios;

public class FolioServicio (
    IUnitOfWork unit, 
    IUsuarioContextoServicio contextoUsuario
) : IFolioServicio
{
    public async Task<string> GenerarSiguienteFolioAsync(Guid sucursalId, TipoFolio tipo)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) 
            throw new Exception("La sucursal no existe.");

        var controlFolio = await unit.FoliosSucursal.ObtenerConBloqueoAsync(sucursalId, tipo);
        int siguienteNumero;

        if (controlFolio == null)
        {
            siguienteNumero = 1;
            controlFolio = FolioSucursal.Crear(sucursalId, tipo, siguienteNumero);
            await unit.FoliosSucursal.AgregarAsync(controlFolio);
        }
        else
        {
            controlFolio.UltimoConsecutivo += 1;
            siguienteNumero = controlFolio.UltimoConsecutivo;
        }

        return tipo switch
        {
            TipoFolio.Asignacion => $"ASIG-{sucursal.Codigo}-{siguienteNumero:D6}",
            TipoFolio.Guia => $"{sucursal.Codigo}-{siguienteNumero:D6}",
            _ => $"{siguienteNumero:D6}"
        };
    }
}