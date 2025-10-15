using Api.Services.Interfaces;
using Application.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;

namespace Api.Services.Implementations;

public class HistorialInventarioService : IHistorialInventarioService
{
    private readonly IUnitOfWork _unitOfWork;

    public HistorialInventarioService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<int> AddAsync(HistorialInventario historial, CancellationToken ct = default)
    {
        var id = await _unitOfWork.HistorialInventario.AddAsync(historial, ct);
        await _unitOfWork.SaveChanges(ct);
        return id;
    }
    public async Task<bool> UpdateAsync(HistorialInventario historial, CancellationToken ct = default)
    {
        var updated = await _unitOfWork.HistorialInventario.UpdateAsync(historial, ct);
        if (updated)
            await _unitOfWork.SaveChanges(ct);
        return updated;
    }
    public async Task<bool> DeleteAsync(IdVO id, CancellationToken ct = default)
    {
        var deleted = await _unitOfWork.HistorialInventario.DeleteAsync(id, ct);
        if (deleted)
            await _unitOfWork.SaveChanges(ct);
        return deleted;
    }

    public async Task<HistorialInventario?> GetByIdAsync(IdVO id, CancellationToken ct = default)
        => await _unitOfWork.HistorialInventario.GetByIdAsync(id, ct);

    public async Task<IReadOnlyList<HistorialInventario>> GetAllAsync(CancellationToken ct = default)
        => await _unitOfWork.HistorialInventario.GetAllAsync(ct);

    public async Task<IReadOnlyList<HistorialInventario>> GetByRepuestoIdAsync(IdVO repuestoId, CancellationToken ct = default)
        => await _unitOfWork.HistorialInventario.GetByRepuestoIdAsync(repuestoId, ct);

    public async Task<IReadOnlyList<HistorialInventario>> GetByAdminIdAsync(IdVO adminId, CancellationToken ct = default)
        => await _unitOfWork.HistorialInventario.GetByAdminIdAsync(adminId, ct);
}
