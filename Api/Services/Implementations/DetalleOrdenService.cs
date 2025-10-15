using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Api.Services.Implementations;

public class DetalleOrdenService : IDetalleOrdenService
{
    private readonly IUnitOfWork _unitOfWork;

    public DetalleOrdenService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DetalleOrden?> GetByIdAsync(IdVO ordenServicioId, IdVO repuestoId, CancellationToken ct = default)
        => await _unitOfWork.DetalleOrden.GetByIdsAsync(ordenServicioId, repuestoId, ct);

    public async Task<IReadOnlyList<DetalleOrden>> GetByRepuestoIdAsync(IdVO repuestoId, CancellationToken ct = default)
        => await _unitOfWork.DetalleOrden.GetByRepuestoIdAsync(repuestoId, ct);

    public async Task<IReadOnlyList<DetalleOrden>> GetByOrdenIdAsync(IdVO ordenServicioId, CancellationToken ct = default)
        => await _unitOfWork.DetalleOrden.GetByOrdenServicioIdAsync(ordenServicioId, ct);

    public async Task<int> AddAsync(DetalleOrden detalle, CancellationToken ct = default)
    {
        var result = await _unitOfWork.DetalleOrden.AddAsync(detalle, ct);
        await _unitOfWork.SaveChanges(ct);
        return result;
    }

    public async Task<bool> UpdateAsync(DetalleOrden detalle, CancellationToken ct = default)
    {
        var updated = await _unitOfWork.DetalleOrden.UpdateAsync(detalle, ct);
        if (updated)
            await _unitOfWork.SaveChanges(ct);

        return updated;
    }

    public async Task<bool> DeleteAsync(IdVO ordenServicioId, IdVO repuestoId, CancellationToken ct = default)
    {
        var deleted = await _unitOfWork.DetalleOrden.DeleteAsync(ordenServicioId, repuestoId, ct);
        if (deleted)
            await _unitOfWork.SaveChanges(ct);

        return deleted;
    }
}
