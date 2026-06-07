using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Audit;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class ChoferService(IUnitOfWork unit, IUserContextService userContext, IAuditLogService auditLog) : IChoferService
{
    public async Task<Result<IReadOnlyList<ChoferResponseDto>>> GetAllAsync()
    {

        var choferes = ( await unit.Choferes.GetAllAsync(userContext.EmpresaId) )
            .Select( ChoferResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<ChoferResponseDto>>.Success(choferes);
        
    }

    public async Task<Result<ChoferResponseDto>> GetByIdAsync(Guid choferId)
    {

        var chofer = await unit.Choferes.GetByIdAsync(choferId, userContext.EmpresaId);

        if (chofer is null)
            return Result<ChoferResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<ChoferResponseDto>.Success(ChoferResponseDto.FromEntity(chofer));
    }

    public async Task<Result<ChoferResponseDto>> CreateAsync(ChoferCreateDto dto)
    {
        var chofer = await unit.Choferes.AddAsync(dto.ToEntity(userContext.EmpresaId));

        await unit.CompleteAsync();

        auditLog.LogTrack(new AuditLogEvent(
            UsuarioId: userContext.UserId,
            EmpresaId: userContext.EmpresaId,
            SucursalId: userContext.SucursalId,
            Accion: "Activar",
            Modulo: "Clientes",
            Detalle: $"Se activó el cliente con ID {chofer.Id}.",
            IpAddress: userContext.IpAddress
        ));

        return Result<ChoferResponseDto>.Success(ChoferResponseDto.FromEntity(chofer));
    }

    public async Task<Result> UpdateAsync(ChoferUpdateDto dto)
    {

        var chofer = await unit.Choferes.GetByIdAsync(dto.ChoferId,  userContext.EmpresaId);

        if (chofer is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(chofer);
        await unit.CompleteAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid choferId)
    {
        var chofer = (await unit.Choferes.GetByIdAsync(choferId,  userContext.EmpresaId));

        if (chofer is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Choferes.Delete(chofer);

        var result = await unit.CompleteAsync();
        if (result <= 0)
            return Result.Failure(ErrorCodes.Generic.NoEliminado);

        return Result.Success();
    }
    
    public async Task<Result> DesactivarAsync(Guid choferId)
    {
        var chofer = await unit.Choferes.GetByIdAsync(choferId, userContext.EmpresaId);
        if (chofer == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        chofer.Estatus = EstatusBasico.Inactivo;
        await unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> ActivarAsync(Guid choferId)
    {
        var chofer = await unit.Choferes.GetByIdAsync(choferId, userContext.EmpresaId);
        if (chofer == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        chofer.Estatus = EstatusBasico.Activo;
        await unit.CompleteAsync();
        
        return Result.Success();
    }
}