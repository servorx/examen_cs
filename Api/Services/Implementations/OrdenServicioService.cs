using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class OrdenServicioService : IOrdenServicioService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrdenServicioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<OrdenServicio?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.OrdenServicio.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<OrdenServicio>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.OrdenServicio.GetAllAsync(ct);

    public async Task<int> AddAsync(OrdenServicio ordenServicio, CancellationToken ct = default)
    {
        if (ordenServicio.FechaEntregaEstimada.Value < ordenServicio.FechaIngreso.Value)
            throw new Exception("La fecha de entrega estimada no puede ser menor a la fecha de ingreso.");

        var id = await _unitOfWork.OrdenServicio.AddAsync(ordenServicio, ct);
        await _unitOfWork.SaveChanges(ct);

        return id;
    }

    public async Task<bool> UpdateAsync(OrdenServicio ordenServicio, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.OrdenServicio.GetByIdAsync(ordenServicio.Id, ct);
        if (existing == null)
            return false;

        if (ordenServicio.FechaEntregaEstimada.Value < ordenServicio.FechaIngreso.Value)
            throw new Exception("La fecha de entrega estimada no puede ser menor a la fecha de ingreso.");

        var updated = await _unitOfWork.OrdenServicio.UpdateAsync(ordenServicio, ct);
        await _unitOfWork.SaveChanges(ct);

        return updated;
    }

    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var existing = await _unitOfWork.OrdenServicio.GetByIdAsync(id, ct);
        if (existing == null)
            return false;

        var deleted = await _unitOfWork.OrdenServicio.DeleteAsync(id, ct);
        await _unitOfWork.SaveChanges(ct);

        return deleted;
    }

    public async Task<IReadOnlyList<OrdenServicio>> GetByVehiculoAsync(IdVO vehiculoId, CancellationToken ct = default)
        => await _unitOfWork.OrdenServicio.GetByVehiculoIdAsync(vehiculoId, ct);

    public async Task<IReadOnlyList<OrdenServicio>> GetByMecanicoAsync(IdVO mecanicoId, CancellationToken ct = default)
        => await _unitOfWork.OrdenServicio.GetByMecanicoIdAsync(mecanicoId, ct);
}
